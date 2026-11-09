using System;
using System.Windows;
using System.Windows.Input;
using QuanLyDichVuKhachSan.Services;
using QuanLyDichVuKhachSan.ViewModels;

namespace QuanLyDichVuKhachSan
{
    public partial class MainWindow : Window
    {
        public MainViewModel ViewModel { get; }
        public LoginViewModel LoginVM { get; }

        private bool _isFullScreen = false;
        private WindowStyle _savedWindowStyle = WindowStyle.SingleBorderWindow;
        private WindowState _savedWindowState = WindowState.Maximized;
        private ResizeMode _savedResizeMode = ResizeMode.CanResize;

        public MainWindow()
        {
            InitializeComponent();
            ViewModel = new MainViewModel();
            LoginVM = new LoginViewModel();

            DataContext = ViewModel;
            LoginControl.DataContext = LoginVM;

            // Khởi tạo ở kích thước nhỏ gọn cho màn hình đăng nhập
            ApplyLoginWindowSize();

            // Nạp dữ liệu nền bất đồng bộ ngay khi mở ứng dụng (giúp mở app nhanh hơn)
            _ = DataService.Instance.InitializeAsync();

            LoginVM.LoginSucceeded += OnLoginSucceeded;
            ViewModel.RequestTabTransition += ExecuteProgressiveTabSwitch;

            ViewModel.RequestLogout += () =>
            {
                if (_isFullScreen)
                {
                    ExitFullScreen();
                }
                ApplyLoginWindowSize();
            };
        }

        private async void OnLoginSucceeded()
        {
            ApplyWorkWindowSize();

            try
            {
                bool initialized = await DataService.Instance.InitializeAsync();
                if (!initialized)
                {
                    HandleInitializationFailure(DataService.Instance.InitializationError ?? "Không rõ nguyên nhân.");
                    return;
                }

                ViewModel.RefreshAuthState();
                ExecuteProgressiveTabSwitch("RoomMap", () => ViewModel.RefreshTabData("RoomMap"));
            }
            catch (Exception ex)
            {
                HandleInitializationFailure(ex.Message);
            }
        }

        private void HandleInitializationFailure(string message)
        {
            DataService.Instance.ResetInitialization();
            AuthService.Instance.Logout();
            ApplyLoginWindowSize();

            MessageBox.Show(
                $"Không thể tải dữ liệu từ CSDL.\n\nChi tiết: {message}\n\nVui lòng kiểm tra kết nối SQL Server rồi đăng nhập lại.",
                "Lỗi dữ liệu",
                MessageBoxButton.OK,
                MessageBoxImage.Error);
        }

        private FrameworkElement? GetViewForTab(string tabName)
        {
            return tabName switch
            {
                "RoomMap" => ViewRoomMap,
                "Statistics" => ViewStatistics,
                "Staff" => ViewStaff,
                "Customers" => ViewCustomers,
                "Inventory" => ViewInventory,
                "ServicesConfig" => ViewServicesConfig,
                "Food" => ViewFood,
                "Event" => ViewEvent,
                "Vehicle" => ViewVehicle,
                "Parking" => ViewParking,
                "Laundry" => ViewLaundry,
                "Transactions" => ViewTransactions,
                _ => null
            };
        }

        private void ExecuteProgressiveTabSwitch(string tabName, Action? deferredRefresh)
        {
            var targetView = GetViewForTab(tabName);
            if (targetView != null)
            {
                // PHA 1 (0ms tức thì): Chuẩn bị vùng dữ liệu để khung Tiêu đề & Đề mục xuất hiện ngay lập tức
                var regions = Helpers.TabProgressiveRender.PrepareStage1(targetView);

                // Cập nhật tab hiện hành ngay lập tức (0ms) -> Vẽ ngay Tiêu đề, Header, Thẻ KPI & Khung đề mục
                ViewModel.CurrentTab = tabName;

                // PHA 2 (Ngay sau khi Tiêu đề & Đề mục đã hiển thị trên màn hình):
                // Đổ dữ liệu lên giao diện với hiệu ứng trượt mượt mà
                Helpers.TabProgressiveRender.ScheduleStage2(targetView, regions, deferredRefresh);
            }
            else
            {
                ViewModel.CurrentTab = tabName;
                deferredRefresh?.Invoke();
            }
        }

        private void MainWindow_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            // Bắt tổ hợp phím Alt + Enter hoặc F11 để chuyển đổi toàn màn hình
            bool isAlt = (Keyboard.Modifiers & ModifierKeys.Alt) == ModifierKeys.Alt;
            bool isEnter = e.Key == Key.Return || e.Key == Key.Enter || e.SystemKey == Key.Return || e.SystemKey == Key.Enter;

            if ((isAlt && isEnter) || e.Key == Key.F11)
            {
                ToggleFullScreen();
                e.Handled = true;
            }
        }

        private void BtnFullScreen_Click(object sender, RoutedEventArgs e)
        {
            ToggleFullScreen();
        }

        public void ToggleFullScreen()
        {
            if (ViewModel == null || !ViewModel.IsLoggedIn)
            {
                return;
            }

            if (!_isFullScreen)
            {
                EnterFullScreen();
            }
            else
            {
                ExitFullScreen();
            }
        }

        private void EnterFullScreen()
        {
            _savedWindowStyle = this.WindowStyle;
            _savedWindowState = this.WindowState;
            _savedResizeMode = this.ResizeMode;

            this.WindowState = WindowState.Normal;
            this.WindowStyle = WindowStyle.None;
            this.ResizeMode = ResizeMode.NoResize;
            this.WindowState = WindowState.Maximized;

            _isFullScreen = true;
            UpdateFullScreenButtonState();
        }

        private void ExitFullScreen()
        {
            this.WindowState = WindowState.Normal;
            this.WindowStyle = _savedWindowStyle;
            this.ResizeMode = _savedResizeMode;
            this.WindowState = _savedWindowState;

            _isFullScreen = false;
            UpdateFullScreenButtonState();
        }

        private void UpdateFullScreenButtonState()
        {
            if (BtnFullScreen != null)
            {
                BtnFullScreen.Content = _isFullScreen ? "🗗 Cửa Sổ" : "⛶ Toàn Màn Hình";
                BtnFullScreen.ToolTip = _isFullScreen
                    ? "Quay về chế độ cửa sổ có thanh tiêu đề (Alt + Enter hoặc F11)"
                    : "Chuyển sang chế độ toàn màn hình không có thanh tiêu đề (Alt + Enter hoặc F11)";
            }
        }

        private void ApplyLoginWindowSize()
        {
            this.WindowStyle = WindowStyle.SingleBorderWindow;
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
            _isFullScreen = false;
            UpdateFullScreenButtonState();
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
