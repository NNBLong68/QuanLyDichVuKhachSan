using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
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

        // Danh sách lô đã hủy (hiển thị ở tab Nhập/Hủy)
        public ObservableCollection<InventoryBatch> CancelledBatches => Data.CancelledBatches;
        public int CancelledBatchesCount => CancelledBatches.Count;
        public bool HasCancelledBatches => CancelledBatches.Count > 0;

        public bool HasLowStockItems => LowStockItems.Count > 0;
        public bool HasNearExpiryBatches => NearExpiryBatches.Count > 0;

        private bool _suppressSelectionSync;
        public int SelectedBatchesCount => NearExpiryBatches.Count(x => x.IsSelected);
        public bool HasSelectedBatches => SelectedBatchesCount > 0;

        public bool? AreAllNearExpirySelected
        {
            get
            {
                int total = NearExpiryBatches.Count;
                int selected = NearExpiryBatches.Count(x => x.IsSelected);
                if (total == 0 || selected == 0) return false;
                return selected == total ? true : null;
            }
            set
            {
                if (!value.HasValue || _suppressSelectionSync) return;

                _suppressSelectionSync = true;
                foreach (var batch in NearExpiryBatches)
                    batch.IsSelected = value.Value;
                _suppressSelectionSync = false;

                NotifyBatchSelectionChanged();
            }
        }

        private void NotifyBatchSelectionChanged()
        {
            OnPropertyChanged(nameof(SelectedBatchesCount));
            OnPropertyChanged(nameof(HasSelectedBatches));
            OnPropertyChanged(nameof(AreAllNearExpirySelected));
            (CancelSelectedBatchesCommand as RelayCommand)?.RaiseCanExecuteChanged();
        }

        private void NearExpiryBatch_PropertyChanged(object? sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(InventoryBatch.IsSelected) && !_suppressSelectionSync)
                NotifyBatchSelectionChanged();
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

        public async Task LoadHistoryAsync()
        {
            if (IsLoadingHistory) return;
            IsLoadingHistory = true;

            try
            {
                var foods = Data.FoodItems.ToList();
                var receiptsTask = DatabaseService.Instance.LoadInventoryReceiptsFromDbAsync();
                var cancelledTask = Task.Run(() => DatabaseService.Instance.LoadCancelledInventoryBatchesFromDb(foods));

                await Task.WhenAll(receiptsTask, cancelledTask);

                ReceiptHistory.ReplaceRange(receiptsTask.Result);
                ApplyHistoryFilter();

                Data.CancelledBatches.Clear();
                foreach (var batch in cancelledTask.Result)
                    Data.CancelledBatches.Add(batch);

                OnPropertyChanged(nameof(ReceiptHistoryCount));
                OnPropertyChanged(nameof(HasReceiptHistory));
                OnPropertyChanged(nameof(CancelledBatchesCount));
                OnPropertyChanged(nameof(HasCancelledBatches));
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
        public int NearExpiryBatchesCount => Data.InventoryBatches.Count(x => x.IsNearExpiry && Data.HasRemainingStock(x.Id));
        public int LowStockItemsCount => Data.FoodItems.Count(x => x.IsLowStock);

        // Commands
        public ICommand SetTabCommand { get; }
        public ICommand DownloadTemplateCommand { get; }
        public ICommand ImportExcelCommand { get; }
        public ICommand DownloadReceiptExcelCommand { get; }
        public ICommand OpenAddFoodItemDialogCommand { get; }
        public ICommand RefreshCommand { get; }
        public ICommand LoadReportCommand { get; }
        public ICommand EditFoodItemCommand { get; }
        public ICommand TrashFoodItemCommand { get; }
        public ICommand RestoreFoodItemCommand { get; }
        public ICommand PermanentDeleteFoodItemCommand { get; }
        public ICommand CancelSelectedBatchesCommand { get; }
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
                        _ = LoadHistoryAsync();

                    if (RequestSubTabTransition != null)
                        RequestSubTabTransition.Invoke(tab);
                    else
                        ActiveTab = tab;
                }
            });
            DownloadTemplateCommand = new RelayCommand(_ => DownloadExcelTemplate());
            ImportExcelCommand = new RelayCommand(_ => _ = ImportFromExcelAsync());
            DownloadReceiptExcelCommand = new RelayCommand(p => _ = DownloadReceiptExcelAsync(p));
            OpenAddFoodItemDialogCommand = new RelayCommand(_ => OpenAddFoodItemDialog());
            RefreshCommand = new RelayCommand(_ =>
            {
                RefreshAll();
                _ = LoadHistoryAsync();
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
            CancelSelectedBatchesCommand = new RelayCommand(
                _ => OpenCancelBatchesDialog(NearExpiryBatches.Where(x => x.IsSelected).ToList(), true),
                _ => HasSelectedBatches);

            RefreshAll();
            _ = LoadHistoryAsync();
        }

        private void TrashFoodItem(object? parameter)
        {
            if (parameter is not FoodItem item) return;
            var result = MessageBox.Show(
                $"Bạn có muốn chuyển mặt hàng [{item.ItemCode}] {item.Name} vào Thùng rác?\n\n" +
                $"• Mặt hàng sẽ bị ẩn khỏi danh mục kho và thực đơn.\n" +
                $"• Có thể khôi phục trong vòng {FoodItem.TrashRetentionDays} ngày.\n" +
                $"• Sau {FoodItem.TrashRetentionDays} ngày sẽ tự động xóa vĩnh viễn.",
                "Thông báo", MessageBoxButton.YesNo, MessageBoxImage.Question);

            if (result != MessageBoxResult.Yes) return;

            item.TrashedDate = DateTime.Now;
            item.Status = "Ngừng kinh doanh";
            Data.FoodItems.Remove(item);
            TrashedItems.Add(item);

            // Đánh dấu trong DB kèm mốc thời gian xóa để tính hạn tự động dọn.
            _ = DatabaseService.Instance.ExecuteNonQueryAsync(
                "UPDATE MonAn SET TrangThai = N'Đã xóa', NgayXoa = @NgayXoa WHERE Id = @Id",
                new Microsoft.Data.SqlClient.SqlParameter("@NgayXoa", item.TrashedDate.Value),
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
                "UPDATE MonAn SET TrangThai = N'Đang phục vụ', NgayXoa = NULL WHERE Id = @Id",
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
                    "UPDATE MonAn SET TrangThai = N'Đã xóa', NgayXoa = '1900-01-01' WHERE Id = @Id",
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

        // =======================================================================
        // HỦY LÔ HÀNG (dùng chung cho menu chuột phải và tab Lô cận hạn)
        // =======================================================================

        /// <summary>
        /// Mở form hủy lô cho một mặt hàng cụ thể (gọi từ menu chuột phải ở tab Kho hàng).
        /// </summary>
        public void OpenCancelBatchesForFood(FoodItem? food)
        {
            if (food == null) return;

            var batches = Data.InventoryBatches
                .Where(b => b.FoodItemId == food.Id)
                .OrderBy(b => b.ExpiryDate ?? DateTime.MaxValue)
                .ThenBy(b => b.BatchNumber)
                .ToList();

            if (batches.Count == 0)
            {
                MessageBox.Show(
                    $"Mặt hàng [{food.ItemCode}] {food.Name} hiện không còn lô nào trong kho để hủy.",
                    "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }

            OpenCancelBatchesDialog(batches, false);
        }

        private async void OpenCancelBatchesDialog(IReadOnlyList<InventoryBatch> batches, bool preselect)
        {
            if (batches.Count == 0)
            {
                MessageBox.Show("Chưa chọn lô nào để hủy.", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }

            // Ghi nhớ lựa chọn hiện tại để khôi phục nếu người dùng đóng form mà không hủy.
            var previousSelection = batches.Select(b => b.IsSelected).ToList();
            bool cancelled = false;

            foreach (var batch in batches)
                batch.IsSelected = preselect;

            try
            {
                var dialog = new Views.CancelBatchDialogWindow(batches);
                if (Application.Current?.MainWindow is Window owner && owner.IsLoaded)
                    dialog.Owner = owner;

                if (dialog.ShowDialog() == true && dialog.SelectedBatches.Count > 0)
                {
                    cancelled = true;
                    await CancelBatchesAsync(dialog.SelectedBatches, dialog.Reason, dialog.Note);
                }
            }
            finally
            {
                for (int i = 0; i < batches.Count; i++)
                    batches[i].IsSelected = cancelled ? false : previousSelection[i];

                NotifyBatchSelectionChanged();
            }
        }

        private async Task CancelBatchesAsync(IReadOnlyList<InventoryBatch> batches, string reason, string note)
        {
            if (batches == null || batches.Count == 0)
            {
                MessageBox.Show("Chưa chọn lô nào để hủy.", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }

            string cancelledBy = Auth.CurrentUser?.FullName ?? string.Empty;
            if (string.IsNullOrWhiteSpace(cancelledBy))
                cancelledBy = Data.GetCurrentActorName();
            if (string.IsNullOrWhiteSpace(cancelledBy))
                cancelledBy = "Hệ thống";

            var result = await DatabaseService.Instance.CancelInventoryBatchesAsync(
                batches.Select(b => b.Id).ToList(), reason, note, cancelledBy);

            if (!result.Success)
            {
                MessageBox.Show($"Hủy lô thất bại!\n\n{result.Message}",
                    "Thông báo", MessageBoxButton.OK, MessageBoxImage.Error);
                await LoadHistoryAsync();
                return;
            }

            var cancelledIds = new HashSet<int>(result.CancelledBatchIds);
            var cancelledBatches = batches.Where(b => cancelledIds.Contains(b.Id)).ToList();

            Data.ApplyBatchCancellation(cancelledBatches, result.FoodUpdates, reason, note, cancelledBy);

            RefreshAll();
            await LoadHistoryAsync();

            MessageBox.Show($"✅ {result.Message}", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        public void RefreshAll()
        {
            // Làm mới HSD theo lô và tự động dọn mặt hàng đã nằm trong thùng rác quá 30 ngày.
            Data.RefreshFoodExpiryInfo();
            Data.RemoveExpiredTrashedItems();
            NotifyTrashChanged();

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
            OnPropertyChanged(nameof(CancelledBatchesCount));
            OnPropertyChanged(nameof(HasCancelledBatches));
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
            foreach (var old in NearExpiryBatches)
                old.PropertyChanged -= NearExpiryBatch_PropertyChanged;

            var result = Data.InventoryBatches
                .Where(x => x.IsNearExpiry && Data.HasRemainingStock(x.Id))
                .OrderBy(x => x.ExpiryDate ?? DateTime.MaxValue)
                .ThenBy(x => x.FoodItemName)
                .ToList();

            foreach (var batch in result)
                batch.PropertyChanged += NearExpiryBatch_PropertyChanged;

            NearExpiryBatches.ReplaceRange(result);
            OnPropertyChanged(nameof(HasNearExpiryBatches));
            NotifyBatchSelectionChanged();
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
                Title = "Lưu file Excel mẫu thêm mặt hàng & nhập kho",
                Filter = "Excel Workbook (*.xlsx)|*.xlsx",
                FileName = $"MauThemMatHang_NhapKho_{DateTime.Now:yyyyMMdd_HHmm}.xlsx",
                DefaultExt = ".xlsx",
                AddExtension = true
            };

            if (dialog.ShowDialog() != true) return;

            try
            {
                var foods = Data.FoodItems.ToList();
                InventoryExcelService.CreateTemplate(dialog.FileName, foods, Data.GetCurrentDutyStaffName());

                MessageBox.Show(
                    $"✅ Đã tạo file Excel mẫu!\n\n" +
                    $"• Đường dẫn: {dialog.FileName}\n" +
                    $"• Danh mục hiện có: {foods.Count} mặt hàng\n\n" +
                    "File mẫu gồm 2 phần:\n" +
                    "• Sheet 'MatHangMoi': khai báo mặt hàng mới (không bắt buộc).\n" +
                    "• Sheet 'PhieuNhapKho': nhập kho (không bắt buộc).\n\n" +
                    "Điền file rồi dùng nút '📥 Thêm từ Excel' trong màn Kho hàng.",
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
                Title = "Chọn file Excel thêm mặt hàng / nhập kho",
                Filter = "Excel Workbook (*.xlsx)|*.xlsx",
                CheckFileExists = true,
                Multiselect = false
            };

            if (dialog.ShowDialog() != true) return;

            InventoryExcelImportResult importResult;
            var foods = Data.FoodItems.Concat(Data.TrashedFoodItems).ToList();

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
                    "Toàn bộ file chưa được xử lý. Vui lòng sửa hết lỗi rồi thử lại.",
                    "Thông báo", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            bool hasNewItems = importResult.NewItems.Count > 0;
            bool hasReceiptLines = importResult.Lines.Count > 0;

            string confirmText = "XÁC NHẬN XỬ LÝ FILE EXCEL?\n\n";
            if (hasNewItems)
                confirmText += $"• Mặt hàng mới sẽ được tạo: {importResult.NewItems.Count} mặt hàng\n";

            if (hasReceiptLines)
            {
                confirmText +=
                    $"• Nhà cung cấp: {importResult.SupplierName}\n" +
                    $"• Người nhập: {importResult.ImporterName}\n" +
                    $"• Số dòng nhập kho: {importResult.Lines.Count} dòng\n" +
                    $"• Tổng tiền nhập: {importResult.TotalCost:N0} đ\n";
                if (!string.IsNullOrWhiteSpace(importResult.ReceiptNote))
                    confirmText += $"• Ghi chú: {importResult.ReceiptNote}\n";
            }

            if (importResult.Notes.Count > 0)
            {
                confirmText += "\nLưu ý:\n" +
                               string.Join("\n", importResult.Notes.Take(8).Select(x => "• " + x)) + "\n";
                if (importResult.Notes.Count > 8)
                    confirmText += $"… và {importResult.Notes.Count - 8} lưu ý khác.\n";
            }

            confirmText += hasReceiptLines
                ? "\nPhiếu nhập kho sẽ được tạo ngay sau khi xác nhận."
                : "\nCác mặt hàng mới sẽ được thêm vào danh mục kho ngay sau khi xác nhận.";

            var confirm = MessageBox.Show(confirmText, "Xác nhận xử lý Excel",
                MessageBoxButton.YesNo, MessageBoxImage.Question);

            if (confirm != MessageBoxResult.Yes) return;

            try
            {
                int createdItems = 0;

                // 1. Tạo các mặt hàng mới trước để lấy Id thật trong CSDL.
                if (hasNewItems)
                {
                    var newFoodItems = importResult.NewItems.Select(x => new FoodItem
                    {
                        Name = x.Name,
                        Category = x.Category,
                        Price = x.Price,
                        RetailUnit = x.RetailUnit,
                        DefaultImportUnit = x.DefaultImportUnit,
                        DefaultConversionRate = x.DefaultConversionRate,
                        AverageCostPrice = 0,
                        StockQuantity = 0,
                        LowStockThreshold = x.LowStockThreshold,
                        Status = "Đang phục vụ"
                    }).ToList();

                    var createResult = await DatabaseService.Instance.AddNewFoodItemsAsync(newFoodItems);
                    if (!createResult.Success)
                    {
                        MessageBox.Show(
                            $"Không thể tạo các mặt hàng mới trong CSDL!\n\n{createResult.Error}",
                            "Thông báo", MessageBoxButton.OK, MessageBoxImage.Error);
                        return;
                    }

                    for (int i = 0; i < newFoodItems.Count && i < createResult.Ids.Count; i++)
                    {
                        newFoodItems[i].Id = createResult.Ids[i];
                        Data.FoodItems.Add(newFoodItems[i]);
                    }

                    createdItems = newFoodItems.Count;

                    // Gán Id thật cho các dòng nhập kho đang tham chiếu mặt hàng mới.
                    foreach (var draft in importResult.Lines.Where(x => x.NewItemIndex >= 0))
                    {
                        if (draft.NewItemIndex < newFoodItems.Count)
                        {
                            var created = newFoodItems[draft.NewItemIndex];
                            draft.FoodItemId = created.Id;
                            draft.FoodItemName = created.Name;
                            draft.RetailUnit = created.RetailUnit;
                            draft.ImportUnit = string.IsNullOrWhiteSpace(created.DefaultImportUnit) ? "Thùng" : created.DefaultImportUnit;
                            draft.ConversionRate = created.DefaultConversionRate > 0 ? created.DefaultConversionRate : 1;
                        }
                    }
                }

                string importedReceiptCode = string.Empty;
                string importedSupplierName = string.Empty;
                int importedReceiptId = 0;

                // 2. Nếu có dòng nhập kho thì tạo phiếu nhập như luồng cũ.
                if (hasReceiptLines)
                {
                    int staffId = ResolveImporterStaffId(importResult.ImporterName);

                    var supplier = await DatabaseService.Instance.GetOrCreateSupplierAsync(importResult.SupplierName);
                    if (supplier.Id <= 0)
                    {
                        MessageBox.Show("Không thể tạo hoặc tìm Nhà cung cấp trong CSDL!",
                            "Thông báo", MessageBoxButton.OK, MessageBoxImage.Error);
                        return;
                    }

                    importedReceiptCode = GenerateUniqueReceiptCode();
                    var saveResult = await DatabaseService.Instance.LapPhieuNhapKhoAsync(
                        importedReceiptCode, supplier.Id, staffId, importResult.ReceiptNote, importResult.Lines,
                        importResult.ImporterName);

                    if (!saveResult.Success)
                    {
                        string partialNote = createdItems > 0
                            ? $"\n\n⚠️ {createdItems} mặt hàng mới đã được tạo trong danh mục kho, nhưng phiếu nhập kho chưa được lưu."
                            : string.Empty;
                        MessageBox.Show(
                            "Có lỗi xảy ra khi lưu phiếu nhập kho xuống cơ sở dữ liệu!" + partialNote,
                            "Thông báo", MessageBoxButton.OK, MessageBoxImage.Error);
                        RefreshAll();
                        return;
                    }

                    importedReceiptId = saveResult.ReceiptId;
                    importedReceiptCode = saveResult.ReceiptCode;
                    importedSupplierName = supplier.Name;

                    string importedBy = importResult.ImporterName;
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
                }

                RefreshAll();
                await LoadHistoryAsync();

                string successText = "✅ XỬ LÝ FILE EXCEL THÀNH CÔNG!\n\n";
                if (createdItems > 0)
                    successText += $"• Đã tạo mới: {createdItems} mặt hàng\n";

                if (hasReceiptLines && importedReceiptId > 0)
                {
                    successText +=
                        $"• Mã phiếu nhập: {importedReceiptCode}\n" +
                        $"• Nhà cung cấp: {importedSupplierName}\n" +
                        $"• Người nhập: {importResult.ImporterName}\n" +
                        $"• Số dòng nhập kho: {importResult.Lines.Count} dòng\n" +
                        $"• Tổng tiền nhập: {importResult.TotalCost:N0} đ\n\n" +
                        "File Excel đã được lưu vào lịch sử nhập kho để tải lại khi cần.";
                }
                else
                {
                    successText += "\nCác mặt hàng mới đã sẵn sàng trong danh mục kho (tồn kho = 0).";
                }

                MessageBox.Show(successText, "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);

                if (hasReceiptLines && _activeTab != "History")
                    SetTabCommand.Execute("History");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Có lỗi xảy ra trong quá trình nhập kho!\n\n{ex.Message}",
                    "Thông báo", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        /// <summary>
        /// Tìm nhân viên khớp với tên Người nhập trong file Excel (không phân biệt hoa/thường, dấu).
        /// Nếu tên tự do không khớp nhân viên nào, dùng nhân viên đang đăng nhập/trực ca để giữ khóa ngoại hợp lệ;
        /// tên hiển thị trên phiếu vẫn lấy đúng tên người dùng đã điền trong file.
        /// </summary>
        private int ResolveImporterStaffId(string importerName)
        {
            string key = TextSearchHelper.ToSearchKey(importerName);
            if (!string.IsNullOrWhiteSpace(key))
            {
                var matched = Data.StaffList.FirstOrDefault(x =>
                    !string.IsNullOrWhiteSpace(x.FullName) &&
                    string.Equals(TextSearchHelper.ToSearchKey(x.FullName), key, StringComparison.OrdinalIgnoreCase));

                if (matched != null && matched.Id > 0)
                    return matched.Id;
            }

            int fallbackId = Auth.CurrentUser?.StaffId ?? 0;
            if (fallbackId <= 0)
                fallbackId = Data.GetCurrentDutyStaffId();

            return fallbackId > 0 ? fallbackId : 1;
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

        private void OpenAddFoodItemDialog()
        {
            var dlg = new Views.AddFoodItemDialogWindow
            {
                Owner = Application.Current?.MainWindow
            };

            if (dlg.ShowDialog() != true || dlg.SavedItem == null)
                return;

            var item = dlg.SavedItem;
            if (!Data.FoodItems.Any(x => x.Id == item.Id))
                Data.FoodItems.Add(item);

            RefreshAll();

            MessageBox.Show(
                $"✅ Đã thêm mặt hàng mới: [{item.ItemCode}] {item.Name}!\n\n" +
                $"• Phân loại: {item.Category} | Đơn vị bán lẻ: {item.RetailUnit}\n" +
                $"• Giá bán lẻ: {item.Price:N0} đ\n" +
                $"• ĐVT nhập mặc định: 1 {item.DefaultImportUnit} = {item.DefaultConversionRate} {item.RetailUnit}\n" +
                $"• Tồn kho khởi tạo = 0\n\n" +
                "Để nhập hàng cho mặt hàng này, hãy dùng nút '📥 Thêm từ Excel' ở phía trên.",
                "Thông báo",
                MessageBoxButton.OK,
                MessageBoxImage.Information
            );
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
