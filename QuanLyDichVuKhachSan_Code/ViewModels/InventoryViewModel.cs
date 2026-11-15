using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using Microsoft.Win32;
using QuanLyDichVuKhachSan.Helpers;
using QuanLyDichVuKhachSan.Models;
using QuanLyDichVuKhachSan.Services;

namespace QuanLyDichVuKhachSan.ViewModels
{
    public class InventoryViewModel : BaseViewModel
    {
        public DataService Data => DataService.Instance;
        public AuthService Auth => AuthService.Instance;

        public bool IsAdmin => Auth.IsAdmin;

        // Quản lý Tab điều hướng: "Warehouse", "LowStock", "NearExpiry", "History"
        private string _activeTab = "Warehouse";
        public string ActiveTab
        {
            get => _activeTab;
            set
            {
                if (SetProperty(ref _activeTab, value))
                {
                    OnPropertyChanged(nameof(IsHistoryTabActive));
                }
            }
        }

        public bool IsHistoryTabActive => ActiveTab == "History";

        // =======================================================================
        // A. QUẢN LÝ DANH MỤC MẶT HÀNG (CATALOG) - Debounced 180ms
        // =======================================================================
        private string _catalogSearchText = "";
        public string CatalogSearchText
        {
            get => _catalogSearchText;
            set
            {
                if (SetProperty(ref _catalogSearchText, value))
                {
                    Debounce("CatalogSearch", ApplyCatalogFilter, 180);
                }
            }
        }

        public ObservableRangeCollection<FoodItem> FilteredCatalogItems { get; } = new();

        // Danh sách mặt hàng gần hết (tồn kho <= ngưỡng cảnh báo riêng)
        public ObservableRangeCollection<FoodItem> LowStockItems { get; } = new();

        // Danh sách lô hàng cận hạn sử dụng (HSD <= hôm nay + 7 ngày, bao gồm đã hết hạn)
        public ObservableRangeCollection<InventoryBatch> NearExpiryBatches { get; } = new();

        public bool HasLowStockItems => LowStockItems.Count > 0;
        public bool HasNearExpiryBatches => NearExpiryBatches.Count > 0;

        // Form thêm mặt hàng mới
        private string _newItemName = "";
        public string NewItemName
        {
            get => _newItemName;
            set => SetProperty(ref _newItemName, value);
        }

        private decimal _newItemPrice = 25000;
        public decimal NewItemPrice
        {
            get => _newItemPrice;
            set => SetProperty(ref _newItemPrice, value);
        }

        private string _newItemRetailUnit = "Lon";
        public string NewItemRetailUnit
        {
            get => _newItemRetailUnit;
            set => SetProperty(ref _newItemRetailUnit, value);
        }

        private string _newItemDefaultImportUnit = "Thùng";
        public string NewItemDefaultImportUnit
        {
            get => _newItemDefaultImportUnit;
            set => SetProperty(ref _newItemDefaultImportUnit, value);
        }

        private int _newItemDefaultConversionRate = 24;
        public int NewItemDefaultConversionRate
        {
            get => _newItemDefaultConversionRate;
            set => SetProperty(ref _newItemDefaultConversionRate, Math.Max(1, value));
        }

        private string _newItemCategory = "Đồ ăn";
        public string NewItemCategory
        {
            get => _newItemCategory;
            set => SetProperty(ref _newItemCategory, value);
        }

        public List<string> AvailableCategories { get; } = new() { "Đồ ăn", "Thức uống" };

        private int _newItemLowStockThreshold = 10;
        public int NewItemLowStockThreshold
        {
            get => _newItemLowStockThreshold;
            set => SetProperty(ref _newItemLowStockThreshold, Math.Max(0, value));
        }

        // =======================================================================
        // B. LỊCH SỬ NHẬP KHO (CẤP PHIẾU) & NHẬP KHO BẰNG EXCEL
        // =======================================================================
        public ObservableRangeCollection<InventoryReceipt> ReceiptHistory { get; } = new();
        public ObservableRangeCollection<InventoryReceipt> FilteredReceiptHistory { get; } = new();

        private string _historySearchText = "";
        public string HistorySearchText
        {
            get => _historySearchText;
            set
            {
                if (SetProperty(ref _historySearchText, value))
                {
                    Debounce("HistorySearch", ApplyHistoryFilter, 180);
                }
            }
        }

        private bool _isLoadingHistory;
        public bool IsLoadingHistory
        {
            get => _isLoadingHistory;
            set => SetProperty(ref _isLoadingHistory, value);
        }

        public int ReceiptHistoryCount => ReceiptHistory.Count;
        public bool HasReceiptHistory => ReceiptHistory.Count > 0;

        private string GenerateUniqueReceiptCode()
        {
            string datePrefix = $"PN-{DateTime.Now:yyyyMMdd}-";
            int maxIndex = 0;

            foreach (var code in ReceiptHistory.Select(x => x.ReceiptCode)
                         .Concat(Data.InventoryBatches.Select(x => x.ReceiptCode)))
            {
                if (string.IsNullOrWhiteSpace(code) || !code.StartsWith(datePrefix, StringComparison.OrdinalIgnoreCase))
                    continue;

                string suffix = code[datePrefix.Length..];
                if (int.TryParse(suffix, out int idx) && idx > maxIndex)
                    maxIndex = idx;
            }

            return $"{datePrefix}{maxIndex + 1:D2}";
        }

        public void ApplyHistoryFilter()
        {
            if (string.IsNullOrWhiteSpace(HistorySearchText))
            {
                FilteredReceiptHistory.ReplaceRange(ReceiptHistory.ToList());
                return;
            }

            var result = ReceiptHistory.Where(x =>
                TextSearchHelper.Match(x.ReceiptCode, HistorySearchText) ||
                TextSearchHelper.Match(x.Supplier, HistorySearchText) ||
                TextSearchHelper.Match(x.Note, HistorySearchText) ||
                TextSearchHelper.Match(x.ImportedBy, HistorySearchText)).ToList();

            FilteredReceiptHistory.ReplaceRange(result);
        }

        public async Task LoadReceiptHistoryAsync()
        {
            if (IsLoadingHistory) return;
            IsLoadingHistory = true;

            try
            {
                var receipts = await DatabaseService.Instance.LoadInventoryReceiptsFromDbAsync();
                ReceiptHistory.ReplaceRange(receipts);
                ApplyHistoryFilter();
                OnPropertyChanged(nameof(ReceiptHistoryCount));
                OnPropertyChanged(nameof(HasReceiptHistory));
            }
            finally
            {
                IsLoadingHistory = false;
            }
        }

        // =======================================================================
        // C. BÁO CÁO LỢI NHUẬN BÁN HÀNG KHO (PROFIT REPORT)
        // =======================================================================
        private DateTime _reportFromDate = new DateTime(DateTime.Today.Year, DateTime.Today.Month, 1);
        public DateTime ReportFromDate
        {
            get => _reportFromDate;
            set
            {
                if (SetProperty(ref _reportFromDate, value))
                {
                    _ = LoadProfitReportAsync();
                }
            }
        }

        private DateTime _reportToDate = DateTime.Today;
        public DateTime ReportToDate
        {
            get => _reportToDate;
            set
            {
                if (SetProperty(ref _reportToDate, value))
                {
                    _ = LoadProfitReportAsync();
                }
            }
        }

        public ObservableCollection<InventoryProfitReportItem> ProfitReportItems { get; } = new();

        public decimal TotalReportRevenue => ProfitReportItems.Sum(x => x.Revenue);
        public decimal TotalReportCost => ProfitReportItems.Sum(x => x.TotalCost);
        public decimal TotalReportGrossProfit => ProfitReportItems.Sum(x => x.GrossProfit);
        public double AverageReportProfitMargin => TotalReportRevenue > 0 ? (double)(TotalReportGrossProfit / TotalReportRevenue * 100) : 0;

        // KPI Thống kê
        public int TotalDryItemsCount => Data.FoodItems.Count;
        public int TotalStockUnits => Data.FoodItems.Sum(x => x.StockQuantity);
        public decimal TotalStockValue => Data.FoodItems.Sum(x => x.StockQuantity * x.Price);
        public int NearExpiryBatchesCount => Data.InventoryBatches.Count(x => x.IsNearExpiry);
        public int LowStockItemsCount => Data.FoodItems.Count(x => x.IsLowStock);
        public string AutoGeneratedNewItemCode => $"MH{((Data.FoodItems.Count > 0 ? Data.FoodItems.Max(x => x.Id) : 0) + 1):D3}";

        // Commands
        public ICommand SetTabCommand { get; }
        public ICommand DownloadTemplateCommand { get; }
        public ICommand ImportExcelCommand { get; }
        public ICommand DownloadReceiptExcelCommand { get; }
        public ICommand AddNewFoodItemCommand { get; }
        public ICommand RefreshCommand { get; }
        public ICommand LoadReportCommand { get; }
        public ICommand EditFoodItemCommand { get; }
        public ICommand TrashFoodItemCommand { get; }
        public ICommand RestoreFoodItemCommand { get; }
        public ICommand PermanentDeleteFoodItemCommand { get; }
        public event Action<string>? RequestSubTabTransition;

        // Thùng rác (Liên kết trực tiếp với DataService để lưu trữ bền vững)
        public ObservableCollection<FoodItem> TrashedItems => Data.TrashedFoodItems;
        public int TrashedItemsCount => TrashedItems.Count;
        public bool HasTrashedItems => TrashedItems.Count > 0;

        public InventoryViewModel()
        {
            SetTabCommand = new RelayCommand(p =>
            {
                if (p is string tab && _activeTab != tab)
                {
                    // Các tab đều lấy dữ liệu mới nhất trước khi hiển thị.
                    if (tab == "Warehouse" || tab == "LowStock" || tab == "NearExpiry")
                        RefreshAll();
                    else if (tab == "History")
                        _ = LoadReceiptHistoryAsync();

                    if (RequestSubTabTransition != null)
                        RequestSubTabTransition.Invoke(tab);
                    else
                        ActiveTab = tab;
                }
            });
            DownloadTemplateCommand = new RelayCommand(_ => DownloadExcelTemplate());
            ImportExcelCommand = new RelayCommand(_ => _ = ImportFromExcelAsync());
            DownloadReceiptExcelCommand = new RelayCommand(p => _ = DownloadReceiptExcelAsync(p));
            AddNewFoodItemCommand = new RelayCommand(_ => AddNewFoodItem());
            RefreshCommand = new RelayCommand(_ =>
            {
                RefreshAll();
                _ = LoadReceiptHistoryAsync();
            });
            LoadReportCommand = new RelayCommand(_ => _ = LoadProfitReportAsync());
            EditFoodItemCommand = new RelayCommand(p =>
            {
                if (p is FoodItem item)
                {
                    var dlg = new Views.EditFoodItemDialogWindow(item);
                    if (dlg.ShowDialog() == true)
                    {
                        RefreshAll();
                    }
                }
            });
            TrashFoodItemCommand = new RelayCommand(p => TrashFoodItem(p));
            RestoreFoodItemCommand = new RelayCommand(p => RestoreFoodItem(p));
            PermanentDeleteFoodItemCommand = new RelayCommand(p => PermanentDeleteFoodItem(p));

            RefreshAll();
            _ = LoadReceiptHistoryAsync();
        }

        private void TrashFoodItem(object? parameter)
        {
            if (parameter is not FoodItem item) return;
            var result = MessageBox.Show(
                $"Bạn có muốn chuyển mặt hàng [{item.ItemCode}] {item.Name} vào Thùng rác?\n\n" +
                $"• Mặt hàng sẽ bị ẩn khỏi danh mục kho và thực đơn.\n" +
                $"• Có thể khôi phục trong vòng 3 tháng.\n" +
                $"• Sau 3 tháng sẽ tự động xóa vĩnh viễn.",
                "Thông báo", MessageBoxButton.YesNo, MessageBoxImage.Question);

            if (result != MessageBoxResult.Yes) return;

            item.TrashedDate = DateTime.Now;
            item.Status = "Ngừng kinh doanh";
            Data.FoodItems.Remove(item);
            TrashedItems.Add(item);

            // Đánh dấu trong DB
            _ = DatabaseService.Instance.ExecuteNonQueryAsync(
                "UPDATE MonAn SET TrangThai = N'Đã xóa' WHERE Id = @Id",
                new Microsoft.Data.SqlClient.SqlParameter("@Id", item.Id)
            );

            NotifyTrashChanged();
            RefreshAll();
        }

        private void RestoreFoodItem(object? parameter)
        {
            if (parameter is not FoodItem item) return;
            item.TrashedDate = null;
            item.Status = "Đang phục vụ";
            TrashedItems.Remove(item);
            Data.FoodItems.Add(item);

            // Khôi phục trong DB
            _ = DatabaseService.Instance.ExecuteNonQueryAsync(
                "UPDATE MonAn SET TrangThai = N'Đang phục vụ' WHERE Id = @Id",
                new Microsoft.Data.SqlClient.SqlParameter("@Id", item.Id)
            );

            NotifyTrashChanged();
            RefreshAll();

            MessageBox.Show($"✅ Đã khôi phục mặt hàng [{item.ItemCode}] {item.Name} về danh mục kho!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private async void PermanentDeleteFoodItem(object? parameter)
        {
            if (parameter is not FoodItem item) return;
            var result = MessageBox.Show(
                $"⚠️ XÓA VĨNH VIỄN mặt hàng [{item.ItemCode}] {item.Name}?\n\n" +
                $"Hành động này KHÔNG THỂ hoàn tác!",
                "Thông báo", MessageBoxButton.YesNo, MessageBoxImage.Warning);

            if (result != MessageBoxResult.Yes) return;

            // Kiểm tra xem mặt hàng đã phát sinh giao dịch lịch sử (đơn ăn uống hoặc phiếu nhập kho) chưa
            string checkSql = "SELECT (SELECT COUNT(1) FROM ChiTietDonHangMonAn WHERE MonAnId = @Id) + " +
                              "(SELECT COUNT(1) FROM ChiTietPhieuNhapKho WHERE MonAnId = @Id);";
            var countObj = await DatabaseService.Instance.ExecuteScalarAsync(checkSql, new Microsoft.Data.SqlClient.SqlParameter("@Id", item.Id));
            int refCount = countObj != null && countObj != DBNull.Value ? Convert.ToInt32(countObj) : 0;

            if (refCount > 0)
            {
                // Có liên kết lịch sử kế toán: Cập nhật trạng thái 'Đã xóa' thay vì DELETE cứng để không vi phạm FK
                await DatabaseService.Instance.ExecuteNonQueryAsync(
                    "UPDATE MonAn SET TrangThai = N'Đã xóa' WHERE Id = @Id",
                    new Microsoft.Data.SqlClient.SqlParameter("@Id", item.Id)
                );
                TrashedItems.Remove(item);
                NotifyTrashChanged();
                RefreshAll();
                MessageBox.Show($"Mặt hàng [{item.ItemCode}] {item.Name} đã từng phát sinh giao dịch/nhập kho trong lịch sử nên hệ thống đã lưu trữ và ẩn vĩnh viễn khỏi danh mục (bảo toàn chứng từ kế toán)!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }

            TrashedItems.Remove(item);

            // Chưa có bất kỳ giao dịch nào: Cho phép xóa cứng
            await DatabaseService.Instance.ExecuteNonQueryAsync(
                "DELETE FROM MonAn WHERE Id = @Id",
                new Microsoft.Data.SqlClient.SqlParameter("@Id", item.Id)
            );

            NotifyTrashChanged();
            RefreshAll();
        }

        private void NotifyTrashChanged()
        {
            OnPropertyChanged(nameof(TrashedItems));
            OnPropertyChanged(nameof(TrashedItemsCount));
            OnPropertyChanged(nameof(HasTrashedItems));
        }

        public void RefreshAll()
        {
            // Auto-clean: Xóa vĩnh viễn các mặt hàng trong thùng rác quá 3 tháng (an toàn khóa ngoại)
            var expiredTrash = TrashedItems.Where(x => x.TrashedDate.HasValue && x.TrashedDate.Value.AddMonths(3) < DateTime.Now).ToList();
            foreach (var expired in expiredTrash)
            {
                TrashedItems.Remove(expired);
                _ = DatabaseService.Instance.ExecuteNonQueryAsync(
                    "IF NOT EXISTS (SELECT 1 FROM ChiTietDonHangMonAn WHERE MonAnId = @Id) AND NOT EXISTS (SELECT 1 FROM ChiTietPhieuNhapKho WHERE MonAnId = @Id) " +
                    "DELETE FROM MonAn WHERE Id = @Id; ELSE UPDATE MonAn SET TrangThai = N'Đã xóa' WHERE Id = @Id;",
                    new Microsoft.Data.SqlClient.SqlParameter("@Id", expired.Id)
                );
            }
            if (expiredTrash.Count > 0) NotifyTrashChanged();

            ApplyCatalogFilter();
            RefreshLowStockItems();
            RefreshNearExpiryBatches();
            OnPropertyChanged(nameof(TotalDryItemsCount));
            OnPropertyChanged(nameof(TotalStockUnits));
            OnPropertyChanged(nameof(TotalStockValue));
            OnPropertyChanged(nameof(NearExpiryBatchesCount));
            OnPropertyChanged(nameof(LowStockItemsCount));
            OnPropertyChanged(nameof(ReceiptHistoryCount));
            OnPropertyChanged(nameof(HasReceiptHistory));
        }

        private void RefreshLowStockItems()
        {
            var result = Data.FoodItems
                .Where(x => x.IsLowStock)
                .OrderBy(x => x.StockQuantity)
                .ThenBy(x => x.Name)
                .ToList();

            LowStockItems.ReplaceRange(result);
            OnPropertyChanged(nameof(HasLowStockItems));
        }

        private void RefreshNearExpiryBatches()
        {
            var result = Data.InventoryBatches
                .Where(x => x.IsNearExpiry)
                .OrderBy(x => x.ExpiryDate ?? DateTime.MaxValue)
                .ThenBy(x => x.FoodItemName)
                .ToList();

            NearExpiryBatches.ReplaceRange(result);
            OnPropertyChanged(nameof(HasNearExpiryBatches));
        }

        /// <summary>
        /// Đưa màn kho hàng về view mặc định (danh sách mặt hàng đang quản lý).
        /// </summary>
        public void ShowDefaultView()
        {
            if (ActiveTab == "Warehouse") return;

            if (RequestSubTabTransition != null)
                RequestSubTabTransition.Invoke("Warehouse");
            else
                ActiveTab = "Warehouse";
        }

        public void ApplyCatalogFilter()
        {
            var q = Data.FoodItems.AsEnumerable();
            if (!string.IsNullOrWhiteSpace(CatalogSearchText))
            {
                string raw = CatalogSearchText.Trim();
                string clean = TextSearchHelper.ToSearchKey(raw);
                q = q.Where(x => TextSearchHelper.MatchPrepared(x.Name, clean) || TextSearchHelper.MatchPrepared(x.RetailUnit, clean));
            }

            var result = q.OrderBy(x => x.IsLowStock ? 0 : 1).ThenBy(x => x.Id).ToList();
            FilteredCatalogItems.ReplaceRange(result);
        }

        private void DownloadExcelTemplate()
        {
            var dialog = new SaveFileDialog
            {
                Title = "Lưu file Excel mẫu nhập kho",
                Filter = "Excel Workbook (*.xlsx)|*.xlsx",
                FileName = $"MauNhapKho_{DateTime.Now:yyyyMMdd_HHmm}.xlsx",
                DefaultExt = ".xlsx",
                AddExtension = true
            };

            if (dialog.ShowDialog() != true) return;

            try
            {
                var foods = Data.FoodItems.ToList();
                InventoryExcelService.CreateTemplate(dialog.FileName, foods);

                MessageBox.Show(
                    $"✅ Đã tạo file Excel mẫu nhập kho!\n\n" +
                    $"• Đường dẫn: {dialog.FileName}\n" +
                    $"• Danh mục hiện có: {foods.Count} mặt hàng\n\n" +
                    $"Mở file, điền Nhà cung cấp + các dòng mặt hàng rồi dùng nút '📥 Nhập Kho Từ Excel' để nhập kho.",
                    "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Không thể tạo file Excel mẫu!\n\n{ex.Message}",
                    "Thông báo", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private async Task ImportFromExcelAsync()
        {
            var dialog = new OpenFileDialog
            {
                Title = "Chọn file Excel nhập kho",
                Filter = "Excel Workbook (*.xlsx)|*.xlsx",
                CheckFileExists = true,
                Multiselect = false
            };

            if (dialog.ShowDialog() != true) return;

            InventoryExcelImportResult importResult;
            var foods = Data.FoodItems.ToList();

            try
            {
                importResult = await Task.Run(() => InventoryExcelService.ReadImportFile(dialog.FileName, foods));
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    $"Không thể đọc file Excel!\n\n{ex.Message}\n\n" +
                    "Vui lòng kiểm tra lại định dạng file (.xlsx) và cấu trúc theo file mẫu.",
                    "Thông báo", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (importResult.Errors.Count > 0)
            {
                string details = string.Join("\n", importResult.Errors.Take(12).Select(x => "• " + x));
                if (importResult.Errors.Count > 12)
                    details += $"\n… và {importResult.Errors.Count - 12} lỗi khác.";

                MessageBox.Show(
                    $"❌ FILE EXCEL CHƯA HỢP LỆ ({importResult.Errors.Count} lỗi):\n\n{details}\n\n" +
                    "Toàn bộ file chưa được nhập kho. Vui lòng sửa hết lỗi rồi thử lại.",
                    "Thông báo", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            var confirm = MessageBox.Show(
                $"XÁC NHẬN NHẬP KHO TỪ EXCEL?\n\n" +
                $"• Nhà cung cấp: {importResult.SupplierName}\n" +
                $"• Số mặt hàng: {importResult.Lines.Count} dòng\n" +
                $"• Tổng tiền nhập: {importResult.TotalCost:N0} đ\n" +
                (string.IsNullOrWhiteSpace(importResult.ReceiptNote) ? "" : $"• Ghi chú: {importResult.ReceiptNote}\n") +
                "\nPhiếu nhập sẽ được tạo ngay sau khi xác nhận.",
                "Xác nhận nhập kho", MessageBoxButton.YesNo, MessageBoxImage.Question);

            if (confirm != MessageBoxResult.Yes) return;

            try
            {
                int staffId = Data.GetCurrentDutyStaffId();
                if (staffId <= 0) staffId = 1;

                var supplier = await DatabaseService.Instance.GetOrCreateSupplierAsync(importResult.SupplierName);
                if (supplier.Id <= 0)
                {
                    MessageBox.Show("Không thể tạo hoặc tìm Nhà cung cấp trong CSDL!",
                        "Thông báo", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                string receiptCode = GenerateUniqueReceiptCode();
                var saveResult = await DatabaseService.Instance.LapPhieuNhapKhoAsync(
                    receiptCode, supplier.Id, staffId, importResult.ReceiptNote, importResult.Lines);

                if (!saveResult.Success)
                {
                    MessageBox.Show("Có lỗi xảy ra khi lưu phiếu nhập kho xuống cơ sở dữ liệu!",
                        "Thông báo", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                string importedBy = Auth.CurrentUser?.FullName ?? "Admin";
                DateTime importTime = DateTime.Now;
                var newBatches = new List<InventoryBatch>();

                foreach (var draft in importResult.Lines)
                {
                    var batch = new InventoryBatch
                    {
                        ReceiptId = saveResult.ReceiptId,
                        ReceiptCode = saveResult.ReceiptCode,
                        FoodItemId = draft.FoodItemId,
                        FoodItemName = draft.FoodItemName,
                        RetailUnit = draft.RetailUnit,
                        ImportUnit = draft.ImportUnit,
                        ConversionRate = draft.ConversionRate,
                        Quantity = draft.Quantity,
                        ImportPrice = draft.ImportPrice,
                        BatchNumber = draft.BatchNumber,
                        ExpiryDate = draft.ExpiryDate,
                        ImportDate = importTime,
                        ImportedBy = importedBy,
                        Supplier = supplier.Name,
                        Note = draft.Note
                    };
                    newBatches.Add(batch);

                    // Cập nhật tồn kho & giá vốn bình quân trong bộ nhớ (CSDL đã cập nhật qua trigger).
                    var food = Data.FoodItems.FirstOrDefault(x => x.Id == draft.FoodItemId);
                    if (food != null)
                    {
                        int oldStock = food.StockQuantity;
                        decimal oldCost = food.AverageCostPrice;
                        int retailUnits = draft.TotalRetailUnits;
                        decimal newCostPerUnit = draft.CostPerRetailUnit;

                        decimal oldValue = oldStock * oldCost;
                        decimal newValue = retailUnits * newCostPerUnit;
                        int totalStock = oldStock + retailUnits;

                        food.AverageCostPrice = totalStock > 0 ? (oldValue + newValue) / totalStock : newCostPerUnit;
                        food.StockQuantity = totalStock;
                        food.Status = "Đang phục vụ";
                        food.NotifyStockChanged();
                    }
                }

                for (int i = newBatches.Count - 1; i >= 0; i--)
                    Data.InventoryBatches.Insert(0, newBatches[i]);

                if (!Data.Suppliers.Any(x => x.Id == supplier.Id))
                    Data.Suppliers.Add(supplier);

                // Sao lưu file Excel đã nhập vào thư mục của app và ghi đường dẫn vào CSDL.
                try
                {
                    string relativePath = InventoryExcelService.CopyToArchive(dialog.FileName, saveResult.ReceiptCode);
                    await DatabaseService.Instance.UpdateReceiptExcelPathAsync(saveResult.ReceiptId, relativePath);
                }
                catch (Exception archiveEx)
                {
                    System.Diagnostics.Debug.WriteLine($"[Archive Excel Error]: {archiveEx.Message}");
                }

                RefreshAll();
                await LoadReceiptHistoryAsync();

                MessageBox.Show(
                    $"✅ ĐÃ NHẬP KHO THÀNH CÔNG!\n\n" +
                    $"• Mã phiếu: {saveResult.ReceiptCode}\n" +
                    $"• Nhà cung cấp: {supplier.Name}\n" +
                    $"• Số mặt hàng: {importResult.Lines.Count} dòng\n" +
                    $"• Tổng tiền nhập: {importResult.TotalCost:N0} đ\n\n" +
                    "File Excel đã được lưu vào lịch sử nhập kho để tải lại khi cần.",
                    "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);

                if (_activeTab != "History")
                    SetTabCommand.Execute("History");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Có lỗi xảy ra trong quá trình nhập kho!\n\n{ex.Message}",
                    "Thông báo", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private async Task DownloadReceiptExcelAsync(object? parameter)
        {
            if (parameter is not InventoryReceipt receipt)
                return;

            var dialog = new SaveFileDialog
            {
                Title = "Tải file Excel phiếu nhập kho",
                Filter = "Excel Workbook (*.xlsx)|*.xlsx",
                FileName = $"PhieuNhap_{receipt.ReceiptCode}.xlsx",
                DefaultExt = ".xlsx",
                AddExtension = true
            };

            if (dialog.ShowDialog() != true) return;

            try
            {
                string archivePath = InventoryExcelService.GetAbsoluteArchivePath(receipt.FileExcelPath);

                if (!string.IsNullOrWhiteSpace(archivePath) && File.Exists(archivePath))
                {
                    File.Copy(archivePath, dialog.FileName, true);
                }
                else
                {
                    var lines = Data.InventoryBatches.Where(x => x.ReceiptId == receipt.Id).ToList();
                    if (lines.Count == 0)
                    {
                        MessageBox.Show(
                            "Không tìm thấy dữ liệu chi tiết của phiếu nhập này để xuất lại file Excel!",
                            "Thông báo", MessageBoxButton.OK, MessageBoxImage.Warning);
                        return;
                    }

                    var foods = Data.FoodItems.ToList();
                    await Task.Run(() => InventoryExcelService.ExportReceipt(dialog.FileName, receipt, lines, foods));
                }

                MessageBox.Show(
                    $"✅ Đã tải file Excel phiếu nhập kho!\n\n• Đường dẫn: {dialog.FileName}",
                    "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Không thể tải file Excel phiếu nhập!\n\n{ex.Message}",
                    "Thông báo", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private async void AddNewFoodItem()
        {
            if (string.IsNullOrWhiteSpace(NewItemName))
            {
                MessageBox.Show("Vui lòng nhập tên mặt hàng mới!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            if (string.IsNullOrWhiteSpace(NewItemDefaultImportUnit) || NewItemDefaultConversionRate <= 0)
            {
                MessageBox.Show("Vui lòng nhập ĐVT nhập mặc định và Hệ số quy đổi lớn hơn 0!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            var item = new FoodItem
            {
                Name = NewItemName.Trim(),
                Category = string.IsNullOrWhiteSpace(NewItemCategory) ? "Đồ ăn" : NewItemCategory.Trim(),
                Price = NewItemPrice,
                RetailUnit = string.IsNullOrWhiteSpace(NewItemRetailUnit) ? "Cái" : NewItemRetailUnit.Trim(),
                DefaultImportUnit = NewItemDefaultImportUnit.Trim(),
                DefaultConversionRate = NewItemDefaultConversionRate,
                AverageCostPrice = 0,
                StockQuantity = 0, // Tồn ban đầu luôn = 0
                LowStockThreshold = NewItemLowStockThreshold,
                Status = "Đang phục vụ"
            };

            int newId = await DatabaseService.Instance.AddNewFoodItemAsync(item);
            if (newId > 0)
            {
                item.Id = newId;
                Data.FoodItems.Add(item);
                RefreshAll();

                MessageBox.Show(
                    $"✅ Đã thêm mặt hàng mới: [{item.ItemCode}] {item.Name}!\n" +
                    $"• Mã mặt hàng tự sinh: {item.ItemCode}\n" +
                    $"• Phân loại: {item.Category} | Đơn vị bán lẻ: {item.RetailUnit}\n" +
                    $"• Giá bán lẻ: {item.Price:N0} đ\n" +
                    $"• ĐVT nhập mặc định: 1 {item.DefaultImportUnit} = {item.DefaultConversionRate} {item.RetailUnit}\n" +
                    $"• Trạng thái phục vụ: Còn hàng (Đang phục vụ)\n" +
                    $"• Tồn kho khởi tạo = 0 (Để nhập hàng, hãy dùng '📥 Nhập Kho Từ Excel' ở thanh công cụ phía trên)",
                    "Thông báo",
                    MessageBoxButton.OK,
                    MessageBoxImage.Information
                );

                NewItemName = "";
            }
            else
            {
                MessageBox.Show("Không thể thêm mặt hàng vào cơ sở dữ liệu!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        public async Task LoadProfitReportAsync()
        {
            var reports = await DatabaseService.Instance.GetInventoryProfitReportAsync(ReportFromDate, ReportToDate);
            ProfitReportItems.Clear();
            foreach (var r in reports)
            {
                ProfitReportItems.Add(r);
            }

            OnPropertyChanged(nameof(TotalReportRevenue));
            OnPropertyChanged(nameof(TotalReportCost));
            OnPropertyChanged(nameof(TotalReportGrossProfit));
            OnPropertyChanged(nameof(AverageReportProfitMargin));
        }
    }
}
