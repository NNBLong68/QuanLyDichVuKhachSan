using System;
using System.Linq;
using System.Windows;
using QuanLyDichVuKhachSan.Helpers;
using QuanLyDichVuKhachSan.Models;
using QuanLyDichVuKhachSan.Services;

namespace QuanLyDichVuKhachSan.Views
{
    public partial class AddFoodItemDialogWindow : Window
    {
        public FoodItem? SavedItem { get; private set; }

        public AddFoodItemDialogWindow()
        {
            InitializeComponent();

            TxtRetailUnit.Text = "Lon";
            TxtPrice.Text = "25000";
            TxtDefaultImportUnit.Text = "Thùng";
            TxtDefaultConversionRate.Text = "24";
            TxtLowStockThreshold.Text = "10";
            CmbCategory.SelectedIndex = 0;
        }

        private void BtnCancel_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            Close();
        }

        private async void BtnSave_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(TxtName.Text))
            {
                MessageBox.Show("Vui lòng nhập tên mặt hàng mới!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Warning);
                TxtName.Focus();
                return;
            }

            string name = TxtName.Text.Trim();
            if (name.Length > 100)
            {
                MessageBox.Show("Tên mặt hàng không được vượt quá 100 ký tự!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Warning);
                TxtName.Focus();
                return;
            }

            string nameKey = TextSearchHelper.ToSearchKey(name);
            bool duplicated = DataService.Instance.FoodItems.Concat(DataService.Instance.TrashedFoodItems)
                .Any(x => string.Equals(TextSearchHelper.ToSearchKey(x.Name), nameKey, StringComparison.OrdinalIgnoreCase));
            if (duplicated)
            {
                MessageBox.Show($"Mặt hàng '{name}' đã tồn tại trong danh mục kho (hoặc thùng rác)!",
                    "Thông báo", MessageBoxButton.OK, MessageBoxImage.Warning);
                TxtName.Focus();
                return;
            }

            if (string.IsNullOrWhiteSpace(TxtRetailUnit.Text))
            {
                MessageBox.Show("Vui lòng nhập đơn vị bán lẻ (Lon, Chai, Gói...)!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Warning);
                TxtRetailUnit.Focus();
                return;
            }

            string defaultImportUnit = string.IsNullOrWhiteSpace(TxtDefaultImportUnit.Text) ? "Thùng" : TxtDefaultImportUnit.Text.Trim();

            if (!int.TryParse(TxtDefaultConversionRate.Text.Trim(), out int conversionRate) || conversionRate <= 0)
            {
                MessageBox.Show("Hệ số quy đổi lẻ phải là số nguyên lớn hơn 0 (Ví dụ: 24, 12, 6)!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Warning);
                TxtDefaultConversionRate.Focus();
                return;
            }

            string cleanPrice = (TxtPrice.Text ?? "").Replace(",", "").Replace(".", "").Replace(" ", "").Replace("đ", "");
            if (!decimal.TryParse(cleanPrice, out decimal price) || price < 0)
            {
                MessageBox.Show("Đơn giá bán lẻ không hợp lệ!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Warning);
                TxtPrice.Focus();
                return;
            }

            if (!int.TryParse(TxtLowStockThreshold.Text.Trim(), out int lowStockThreshold) || lowStockThreshold < 0)
            {
                MessageBox.Show("Ngưỡng cảnh báo tồn kho thấp phải là số nguyên lớn hơn hoặc bằng 0!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Warning);
                TxtLowStockThreshold.Focus();
                return;
            }

            var item = new FoodItem
            {
                Name = name,
                Category = CmbCategory.SelectedIndex == 1 ? "Thức uống" : "Đồ ăn",
                Price = price,
                RetailUnit = TxtRetailUnit.Text.Trim(),
                DefaultImportUnit = defaultImportUnit,
                DefaultConversionRate = conversionRate,
                AverageCostPrice = 0,
                StockQuantity = 0,
                LowStockThreshold = lowStockThreshold,
                Status = "Đang phục vụ"
            };

            BtnSave.IsEnabled = false;
            try
            {
                int newId = await DatabaseService.Instance.AddNewFoodItemAsync(item);
                if (newId <= 0)
                {
                    MessageBox.Show("Không thể thêm mặt hàng vào cơ sở dữ liệu!",
                        "Thông báo", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                item.Id = newId;
                SavedItem = item;
                DialogResult = true;
                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Không thể thêm mặt hàng!\n\n{ex.Message}",
                    "Thông báo", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            finally
            {
                BtnSave.IsEnabled = true;
            }
        }
    }
}
