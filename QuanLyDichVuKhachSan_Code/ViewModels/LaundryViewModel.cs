using System;
using System.Collections.ObjectModel;
using System.Linq;
using QuanLyDichVuKhachSan.Models;

namespace QuanLyDichVuKhachSan.ViewModels
{
    public class LaundryViewModel : BaseViewModel
    {
        private ObservableCollection<LaundryOrder> _laundryOrders = new ObservableCollection<LaundryOrder>();
        public ObservableCollection<LaundryOrder> LaundryOrders
        {
            get => _laundryOrders;
            set => SetProperty(ref _laundryOrders, value);
        }

        private LaundryOrder? _selectedOrder;
        public LaundryOrder? SelectedOrder
        {
            get => _selectedOrder;
            set => SetProperty(ref _selectedOrder, value);
        }

        public LaundryViewModel()
        {
            LoadData();
        }

        public void LoadData()
        {
        }

        public void SelectOrderByCode(string? orderCode, string? roomNumber)
        {
            if (string.IsNullOrWhiteSpace(orderCode) && string.IsNullOrWhiteSpace(roomNumber))
                return;

            var target = LaundryOrders.FirstOrDefault(x => 
                (!string.IsNullOrEmpty(orderCode) && x.OrderCode.Equals(orderCode, StringComparison.OrdinalIgnoreCase)) ||
                (!string.IsNullOrEmpty(roomNumber) && x.RoomNumber.Equals(roomNumber, StringComparison.OrdinalIgnoreCase))
            );

            if (target != null)
            {
                SelectedOrder = target;
            }
        }
    }
}