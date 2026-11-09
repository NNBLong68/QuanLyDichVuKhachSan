using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows;
using QuanLyDichVuKhachSan.Services;
using QuanLyDichVuKhachSan.ViewModels;
using QuanLyDichVuKhachSan.Views;

namespace QuanLyDichVuKhachSan
{
    public partial class MainWindow : Window
    {
        public MainViewModel ViewModel { get; }
        public LoginViewModel LoginVM { get; }

        // Cache view theo tên tab: mỗi tab chỉ được tạo một lần,
        // các lần chuyển tab sau dùng lại instance cũ (giữ nguyên trạng thái).
        private readonly Dictionary<string, FrameworkElement> _viewCache = new();

        public MainWindow()
        {
            InitializeComponent();

            // MainViewModel lúc này rất nhẹ: chưa tạo ViewModel con, chưa nạp CSDL.
            ViewModel = new MainViewModel();
            LoginVM = new LoginViewModel();

            DataContext = ViewModel;
            LoginControl.DataContext = LoginVM;

            ApplyLoginWindowSize();

            // Nạp dữ liệu ngay từ lúc mở app, trên background thread.
            // Màn hình đăng nhập vẫn hiện ngay; nếu người dùng đăng nhập sớm
            // thì OnLoginSucceeded sẽ await chính task này.
            _ = DataService.Instance.InitializeAsync();

            LoginVM.LoginSucceeded += OnLoginSucceeded;
            ViewModel.RequestLogout += OnLogout;
            ViewModel.PropertyChanged += OnViewModelPropertyChanged;
        }

        private void OnViewModelPropertyChanged(object? sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(MainViewModel.CurrentTab) && ViewModel.IsWorkspaceVisible)
            {
                ShowTab(ViewModel.CurrentTab);
            }
        }

        private async void OnLoginSucceeded()
        {
            ApplyWorkWindowSize();

            try
            {
                bool initialized = await DataService.Instance.InitializeAsync();
                if (!initialized)
                {
                    HandleInitializationFailure(
                        DataService.Instance.InitializationError ?? "Không rõ nguyên nhân.");
                    return;
                }

                ViewModel.InitializeWorkspace();
                ShowTab(ViewModel.CurrentTab);
            }
            catch (Exception ex)
            {
                HandleInitializationFailure(ex.Message);
            }
        }

        private void HandleInitializationFailure(string message)
        {
            DataService.Instance.ResetInitialization();

            // Đưa người dùng về màn hình đăng nhập để có thể thử lại,
            // tránh bị kẹt ở màn hình "Đang tải dữ liệu..." không có nút bấm.
            AuthService.Instance.Logout();
            ApplyLoginWindowSize();

            MessageBox.Show(
                $"Không thể tải dữ liệu từ CSDL.\n\nChi tiết: {message}\n\nVui lòng kiểm tra kết nối SQL Server rồi đăng nhập lại.",
                "Lỗi dữ liệu",
                MessageBoxButton.OK,
                MessageBoxImage.Error);
        }

        private void OnLogout()
        {
            ApplyLoginWindowSize();
        }

        private void ShowTab(string tab)
        {
            if (string.IsNullOrWhiteSpace(tab)) return;

            if (!_viewCache.TryGetValue(tab, out var view))
            {
                view = CreateView(tab);
                _viewCache[tab] = view;
            }

            TabContentHost.Content = view;
        }

        private FrameworkElement CreateView(string tab)
        {
            return tab switch
            {
                "RoomMap" => new RoomMapView { DataContext = ViewModel.RoomMapVM },
                "Statistics" => new StatisticsView { DataContext = ViewModel.StatisticsVM },
                "Staff" => new StaffManagementView { DataContext = ViewModel.StaffVM },
                "Customers" => new CustomerListView { DataContext = ViewModel.CustomerVM },
                "Inventory" => new InventoryManagementView { DataContext = ViewModel.InventoryVM },
                "ServicesConfig" => new ServicesConfigView { DataContext = ViewModel.ServicesConfigVM },
                "Food" => new FoodServiceView { DataContext = ViewModel.FoodVM },
                "Event" => new EventServiceView { DataContext = ViewModel.EventVM },
                "Vehicle" => new VehicleServiceView { DataContext = ViewModel.VehicleVM },
                "Parking" => new ParkingServiceView { DataContext = ViewModel.ParkingVM },
                "Laundry" => new LaundryServiceView { DataContext = ViewModel.LaundryVM },
                "Transactions" => new TransactionHistoryView { DataContext = ViewModel.TransactionsVM },
                _ => throw new ArgumentOutOfRangeException(nameof(tab), tab, "Tab không hợp lệ.")
            };
        }

        private void ApplyLoginWindowSize()
        {
            this.WindowState = WindowState.Normal;
            this.ResizeMode = ResizeMode.CanMinimize;
            this.MinWidth = 460;
            this.MinHeight = 460;
            this.Width = 480;
            this.Height = 480;
            CenterWindowOnScreen();
        }

        private void ApplyWorkWindowSize()
        {
            this.MinWidth = 1100;
            this.MinHeight = 700;
            this.ResizeMode = ResizeMode.CanResize;
            this.WindowState = WindowState.Maximized;
        }

        private void CenterWindowOnScreen()
        {
            double screenWidth = SystemParameters.PrimaryScreenWidth;
            double screenHeight = SystemParameters.PrimaryScreenHeight;
            this.Left = (screenWidth - this.Width) / 2;
            this.Top = (screenHeight - this.Height) / 2;
        }
    }
}
