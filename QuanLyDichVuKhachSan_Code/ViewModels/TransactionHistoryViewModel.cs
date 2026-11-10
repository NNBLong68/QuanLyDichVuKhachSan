using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using QuanLyDichVuKhachSan.Helpers;
using QuanLyDichVuKhachSan.Models;
using QuanLyDichVuKhachSan.Services;
using QuanLyDichVuKhachSan.Views;

namespace QuanLyDichVuKhachSan.ViewModels
{
    public class TransactionHistoryViewModel : BaseViewModel
    {
        public DataService Data => DataService.Instance;
        public AuthService Auth => AuthService.Instance;

        private string _searchQuery = "";
        public string SearchQuery
        {
            get => _searchQuery;
            set
            {
                if (SetProperty(ref _searchQuery, value))
                {
                    Debounce("TransactionSearch", ApplyFilter, 180);
                }
            }
        }

        private string _selectedCategoryFilter = "Tất cả";
        public string SelectedCategoryFilter
        {
            get => _selectedCategoryFilter;
            set
            {
                if (SetProperty(ref _selectedCategoryFilter, value))
                {
                    ApplyFilter();
                }
            }
        }

        public List<string> CategoryFilters { get; } = new()
        {
            "Tất cả",
            "Tiền phòng",
            "Ăn uống",
            "Sảnh & Sự kiện",
            "Thuê xe máy",
            "Bãi gửi xe",
            "Giặt ủi"
        };

        private DateTime _fromDate = DateTime.Today.AddDays(-7);
        public DateTime FromDate
        {
            get => _fromDate;
            set
            {
                if (SetProperty(ref _fromDate, value))
                {
                    ApplyFilter();
                }
            }
        }

        private DateTime _toDate = DateTime.Today;
        public DateTime ToDate
        {
            get => _toDate;
            set
            {
                if (SetProperty(ref _toDate, value))
                {
                    ApplyFilter();
                }
            }
        }

        private TransactionRecord? _selectedTransaction;
        public TransactionRecord? SelectedTransaction
        {
            get => _selectedTransaction;
            set => SetProperty(ref _selectedTransaction, value);
        }

        public ObservableRangeCollection<TransactionRecord> FilteredTransactions { get; } = new();

        public int TotalCount => FilteredTransactions.Count;
        public decimal TotalAmountSum => FilteredTransactions.Where(x => x.IsPaid).Sum(x => x.Amount);
        public string CurrentDutyStaffDisplay => Data.GetScheduledDutyStaffName();
        public string CurrentOperatorDisplay => Data.GetCurrentActorName();
        public string CurrentOperatorSubDisplay => AuthService.Instance.IsAdmin
            ? $"Ca trực lễ tân: {Data.GetScheduledDutyStaffName()}"
            : "Tài khoản: Lễ Tân (ghi nhận theo ca)";

        private int _refreshPending;
        private DateTime _lastKnownToday = DateTime.Today;
        private System.Windows.Threading.DispatcherTimer? _shiftTimer;

        public ICommand RefreshCommand { get; }
        public ICommand OpenDetailCommand { get; }
        public ICommand ResetDateCommand { get; }

        public TransactionHistoryViewModel()
        {
            RefreshCommand = new RelayCommand(_ => Refresh());
            OpenDetailCommand = new RelayCommand(p => OpenDetail(p));
            ResetDateCommand = new RelayCommand(_ =>
            {
                FromDate = DateTime.Today.AddDays(-30);
                ToDate = DateTime.Today;
            });

            SubscribeRealtimeUpdates();
            StartShiftTimer();
            Refresh();
        }

        private void SubscribeRealtimeUpdates()
        {
            Data.TransactionsChanged += ScheduleRefresh;
            AuthService.Instance.AuthStateChanged += OnAuthStateChanged;

            System.Collections.Specialized.NotifyCollectionChangedEventHandler onCollectionChanged = (_, __) => ScheduleRefresh();
            Data.FoodOrders.CollectionChanged += onCollectionChanged;
            Data.EventBookings.CollectionChanged += onCollectionChanged;
            Data.VehicleRentals.CollectionChanged += onCollectionChanged;
            Data.ParkingRecords.CollectionChanged += onCollectionChanged;
            Data.LaundryOrders.CollectionChanged += onCollectionChanged;
            Data.RoomInvoices.CollectionChanged += onCollectionChanged;
        }

        private void StartShiftTimer()
        {
            var dispatcher = System.Windows.Application.Current?.Dispatcher;
            if (dispatcher == null) return;

            _shiftTimer = new System.Windows.Threading.DispatcherTimer(
                TimeSpan.FromSeconds(30),
                System.Windows.Threading.DispatcherPriority.Background,
                (_, __) =>
                {
                    if (DateTime.Today != _lastKnownToday)
                    {
                        var previousToday = _lastKnownToday;
                        _lastKnownToday = DateTime.Today;
                        if (ToDate.Date == previousToday)
                        {
                            ToDate = _lastKnownToday;
                        }
                    }

                    NotifyActorProperties();
                },
                dispatcher);
            _shiftTimer.Start();
        }

        private void OnAuthStateChanged()
        {
            var dispatcher = System.Windows.Application.Current?.Dispatcher;
            if (dispatcher != null && !dispatcher.CheckAccess())
            {
                dispatcher.BeginInvoke(new Action(NotifyActorProperties));
                return;
            }

            NotifyActorProperties();
        }

        private void NotifyActorProperties()
        {
            OnPropertyChanged(nameof(CurrentDutyStaffDisplay));
            OnPropertyChanged(nameof(CurrentOperatorDisplay));
            OnPropertyChanged(nameof(CurrentOperatorSubDisplay));
        }

        private void ScheduleRefresh()
        {
            var dispatcher = System.Windows.Application.Current?.Dispatcher;
            if (dispatcher == null)
            {
                Refresh();
                NotifyActorProperties();
                return;
            }

            if (System.Threading.Interlocked.CompareExchange(ref _refreshPending, 1, 0) != 0)
            {
                return;
            }

            dispatcher.BeginInvoke(new Action(() =>
            {
                System.Threading.Interlocked.Exchange(ref _refreshPending, 0);
                Refresh();
                NotifyActorProperties();
            }), System.Windows.Threading.DispatcherPriority.Background);
        }

        public void Refresh()
        {
            ApplyFilter();
        }

        public void ApplyFilter()
        {
            var all = Data.GetAllTransactions();
            string rawQ = (SearchQuery ?? "").Trim();
            string cleanQ = TextSearchHelper.ToSearchKey(rawQ);

            DateTime start = FromDate.Date;
            DateTime end = ToDate.Date.AddDays(1).AddTicks(-1);

            var filteredList = new List<TransactionRecord>();

            foreach (var t in all)
            {
                // Lọc ngày
                if (t.PaymentTime < start || t.PaymentTime > end)
                {
                    continue;
                }

                // Lọc loại dịch vụ
                if (SelectedCategoryFilter != "Tất cả" && !t.ServiceCategory.Contains(SelectedCategoryFilter))
                {
                    continue;
                }

                // Lọc từ khóa tìm kiếm siêu tốc với TextSearchHelper
                if (!string.IsNullOrWhiteSpace(cleanQ))
                {
                    bool matchCode = (!string.IsNullOrWhiteSpace(t.TransactionCode) && t.TransactionCode.IndexOf(rawQ, StringComparison.OrdinalIgnoreCase) >= 0);
                    bool matchCustomer = TextSearchHelper.MatchPrepared(t.CustomerName, cleanQ);
                    bool matchRoom = TextSearchHelper.MatchPrepared(t.RoomNumber, cleanQ);
                    bool matchStaff = TextSearchHelper.MatchPrepared(t.PaidBy, cleanQ) || TextSearchHelper.MatchPrepared(t.CreatedBy, cleanQ);

                    if (!matchCode && !matchCustomer && !matchRoom && !matchStaff)
                    {
                        continue;
                    }
                }

                filteredList.Add(t);
            }

            FilteredTransactions.ReplaceRange(filteredList);

            OnPropertyChanged(nameof(TotalCount));
            OnPropertyChanged(nameof(TotalAmountSum));
        }

        private void OpenDetail(object? parameter)
        {
            var target = parameter as TransactionRecord ?? SelectedTransaction;
            if (target == null)
            {
                MessageBox.Show("Vui lòng chọn một giao dịch để xem chi tiết hóa đơn!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }

            var win = new InvoiceBillWindow(target);
            win.ShowDialog();
        }
    }
}
