using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;
using QuanLyDichVuKhachSan.Models;
using QuanLyDichVuKhachSan.Services;

namespace QuanLyDichVuKhachSan.Views
{
    public partial class CancelBatchDialogWindow : Window, INotifyPropertyChanged
    {
        private readonly ObservableCollection<CancelBatchRow> _rows = new();
        private bool _syncingSelection;

        public int BatchCount => _rows.Count;

        /// <summary>Các lô người dùng đã tick để hủy (chỉ có giá trị sau khi bấm Hủy Lô).</summary>
        public List<InventoryBatch> SelectedBatches { get; private set; } = new();

        /// <summary>Lý do hủy do người dùng chọn.</summary>
        public string Reason { get; private set; } = string.Empty;

        /// <summary>Ghi chú thêm (không bắt buộc).</summary>
        public string Note { get; private set; } = string.Empty;

        private bool? _areAllSelected;
        public bool? AreAllSelected
        {
            get => _areAllSelected;
            set
            {
                if (_areAllSelected == value) return;
                _areAllSelected = value;
                OnPropertyChanged();

                if (_syncingSelection || !value.HasValue) return;

                _syncingSelection = true;
                foreach (var row in _rows)
                    row.IsSelected = value.Value;
                _syncingSelection = false;
            }
        }

        public CancelBatchDialogWindow(IReadOnlyList<InventoryBatch> batches)
        {
            InitializeComponent();
            DataContext = this;

            var stockLookup = DataService.Instance.FoodItems
                .GroupBy(x => x.Id)
                .ToDictionary(g => g.Key, g => g.First().StockQuantity);

            foreach (var batch in batches)
            {
                int stock = stockLookup.TryGetValue(batch.FoodItemId, out int qty) ? qty : 0;
                var row = new CancelBatchRow(batch, stock);
                row.PropertyChanged += Row_PropertyChanged;
                _rows.Add(row);
            }

            BatchGrid.ItemsSource = _rows;
            OnPropertyChanged(nameof(BatchCount));
            AreAllSelected = false;
            RefreshState();
        }

        private void Row_PropertyChanged(object? sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName != nameof(CancelBatchRow.IsSelected)) return;

            if (!_syncingSelection)
            {
                _syncingSelection = true;
                int total = _rows.Count;
                int selected = _rows.Count(x => x.IsSelected);
                AreAllSelected = total > 0 && selected == total
                    ? true
                    : selected == 0 ? false : (bool?)null;
                _syncingSelection = false;
            }

            RefreshState();
        }

        private void RefreshState()
        {
            int selectedCount = _rows.Count(x => x.IsSelected);
            int selectedUnits = _rows.Where(x => x.IsSelected).Sum(x => x.Batch.TotalRetailUnits);

            TxtSelection.Text = selectedCount == 0
                ? "Chưa chọn lô nào"
                : $"Đã chọn {selectedCount}/{_rows.Count} lô • Tổng {selectedUnits:N0} đơn vị lẻ sẽ bị trừ khỏi kho";

            BtnConfirm.IsEnabled = selectedCount > 0 && CmbReason.SelectedIndex >= 0;

            var warnings = new List<string>();
            foreach (var group in _rows.Where(x => x.IsSelected).GroupBy(x => x.Batch.FoodItemId))
            {
                var first = group.First();
                int units = group.Sum(x => x.Batch.TotalRetailUnits);
                int stock = first.CurrentStock;
                string unit = first.Batch.RetailUnit;
                string name = first.Batch.FoodItemName;

                if (units > stock)
                {
                    warnings.Add($"⚠ {name}: hủy {units:N0} {unit} vượt tồn kho hiện tại ({stock:N0} {unit}) — tồn kho sẽ được đưa về 0.");
                }
                else if (units == stock)
                {
                    warnings.Add($"⚠ {name}: hủy toàn bộ {units:N0} {unit} đang có — tồn kho sẽ về 0.");
                }
                else
                {
                    warnings.Add($"• {name}: hủy {units:N0} {unit}, tồn kho còn lại dự kiến {stock - units:N0} {unit}.");
                }
            }

            if (warnings.Count > 0)
            {
                TxtWarning.Text = string.Join(Environment.NewLine, warnings);
                WarningBorder.Visibility = Visibility.Visible;
            }
            else
            {
                WarningBorder.Visibility = Visibility.Collapsed;
            }
        }

        private void CmbReason_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (!IsLoaded) return;
            RefreshState();
        }

        private void BtnConfirm_Click(object sender, RoutedEventArgs e)
        {
            var selected = _rows.Where(x => x.IsSelected).ToList();
            if (selected.Count == 0)
            {
                MessageBox.Show("Vui lòng chọn ít nhất một lô hàng để hủy.",
                    "Thông báo", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            if (CmbReason.SelectedIndex < 0 ||
                CmbReason.SelectedItem is not ComboBoxItem reasonItem ||
                string.IsNullOrWhiteSpace(reasonItem.Content?.ToString()))
            {
                MessageBox.Show("Vui lòng chọn lý do hủy.",
                    "Thông báo", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            Reason = reasonItem.Content!.ToString()!.Trim();
            Note = TxtNote.Text?.Trim() ?? string.Empty;
            SelectedBatches = selected.Select(x => x.Batch).ToList();

            DialogResult = true;
            Close();
        }

        private void BtnClose_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            Close();
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        private void OnPropertyChanged([CallerMemberName] string? propertyName = null)
            => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

        /// <summary>
        /// Dòng hiển thị trong bảng hủy lô: bọc InventoryBatch và bổ sung tồn kho hiện tại của mặt hàng.
        /// </summary>
        public class CancelBatchRow : INotifyPropertyChanged
        {
            public InventoryBatch Batch { get; }
            public int CurrentStock { get; }

            public bool IsSelected
            {
                get => Batch.IsSelected;
                set
                {
                    if (Batch.IsSelected == value) return;
                    Batch.IsSelected = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(IsSelected)));
                }
            }

            public string ReceiptCode => Batch.ReceiptCode;
            public string FoodItemName => Batch.FoodItemName;
            public string BatchNumber => string.IsNullOrWhiteSpace(Batch.BatchNumber) ? "—" : Batch.BatchNumber;
            public string ExpiryDateDisplay => Batch.ExpiryDate?.ToString("dd/MM/yyyy") ?? "Không có";
            public int Quantity => Batch.Quantity;
            public string ImportUnit => Batch.ImportUnit;
            public int TotalRetailUnits => Batch.TotalRetailUnits;

            public CancelBatchRow(InventoryBatch batch, int currentStock)
            {
                Batch = batch;
                CurrentStock = currentStock;
            }

            public event PropertyChangedEventHandler? PropertyChanged;
        }
    }
}
