using System.Linq;
using System.Windows.Controls;
using System.Windows.Input;
using QuanLyDichVuKhachSan.Models;
using QuanLyDichVuKhachSan.ViewModels;

namespace QuanLyDichVuKhachSan.Views
{
    public partial class FoodServiceView : UserControl
    {
        public FoodServiceView()
        {
            InitializeComponent();
        }

        private void CartQuantity_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = e.Text.Any(c => !char.IsDigit(c));
        }

        private void CartQuantity_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter && sender is TextBox textBox)
            {
                CommitCartQuantity(textBox);
                e.Handled = true;
            }
        }

        private void CartQuantity_GotFocus(object sender, System.Windows.RoutedEventArgs e)
        {
            if (sender is TextBox textBox)
                textBox.SelectAll();
        }

        private void CartQuantity_LostFocus(object sender, System.Windows.RoutedEventArgs e)
        {
            if (sender is TextBox textBox)
                CommitCartQuantity(textBox);
        }

        private void CommitCartQuantity(TextBox textBox)
        {
            if (textBox.DataContext is not FoodOrderItem item)
                return;

            if (DataContext is not FoodViewModel viewModel)
                return;

            if (!int.TryParse((textBox.Text ?? string.Empty).Trim(), out int quantity) || quantity < 1)
                quantity = 1;

            viewModel.CommitCartItemQuantity(item, quantity);
            textBox.Text = item.Quantity.ToString();
        }
    }
}
