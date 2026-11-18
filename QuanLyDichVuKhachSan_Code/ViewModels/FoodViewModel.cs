using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using QuanLyDichVuKhachSan.Helpers;
using QuanLyDichVuKhachSan.Models;
using QuanLyDichVuKhachSan.Services;

namespace QuanLyDichVuKhachSan.ViewModels
{
    public class FoodViewModel : BaseViewModel
    {
        public DataService Data => DataService.Instance;
        public AuthService Auth => AuthService.Instance;

        public bool IsAdmin => Auth.IsAdmin;
        public bool IsReceptionist => Auth.IsReceptionist;

        // Quản lý tìm kiếm mặt hàng realtime (Debounced 180ms)
        private string _searchFoodQuery = "";
        public string SearchFoodQuery
        {
            get => _searchFoodQuery;
            set
            {
                if (SetProperty(ref _searchFoodQuery, value))
                {
                    Debounce("FoodSearch", ApplyFoodFilter, 180);
                }
            }
        }

        // Bộ lọc danh mục dạng pill: Tất cả / Đồ ăn / Thức uống
        private string _selectedFoodCategory = "Tất cả";

        public bool IsAllFoodCategory
        {
            get => _selectedFoodCategory == "Tất cả";
            set { if (value) SetFoodCategory("Tất cả"); }
        }

        public bool IsFoodOnlyCategory
        {
            get => _selectedFoodCategory == "Đồ ăn";
            set { if (value) SetFoodCategory("Đồ ăn"); }
        }

        public bool IsDrinkOnlyCategory
        {
            get => _selectedFoodCategory == "Thức uống";
            set { if (value) SetFoodCategory("Thức uống"); }
        }

        private void SetFoodCategory(string category)
        {
            if (_selectedFoodCategory == category) return;

            _selectedFoodCategory = category;
            OnPropertyChanged(nameof(IsAllFoodCategory));
            OnPropertyChanged(nameof(IsFoodOnlyCategory));
            OnPropertyChanged(nameof(IsDrinkOnlyCategory));
            ApplyFoodFilter();
        }

        public ObservableRangeCollection<FoodItem> FilteredFoodOnlyItems { get; } = new();
        public ObservableRangeCollection<FoodItem> FilteredDrinkOnlyItems { get; } = new();

        public int AllFoodItemCount { get; private set; }
        public int FoodOnlyItemCount { get; private set; }
        public int DrinkOnlyItemCount { get; private set; }

        public string AllCategoryDisplay => $"Tất cả · {AllFoodItemCount}";
        public string FoodCategoryDisplay => $"Đồ ăn · {FoodOnlyItemCount}";
        public string DrinkCategoryDisplay => $"Thức uống · {DrinkOnlyItemCount}";

        public bool HasFoodResults => FilteredFoodOnlyItems.Count > 0;
        public bool HasDrinkResults => FilteredDrinkOnlyItems.Count > 0;
        public bool HasFilteredItems => HasFoodResults || HasDrinkResults;

        // Số lượng món hiển thị theo từng nhóm (đã áp dụng tìm kiếm)
        public string FoodGroupCountText => $"{FilteredFoodOnlyItems.Count} món";
        public string DrinkGroupCountText => $"{FilteredDrinkOnlyItems.Count} món";

        // Quản lý Order mới (Lễ tân / Mini-bar)
        private string _orderCustomerName = "";
        public string OrderCustomerName
        {
            get => _orderCustomerName;
            set => SetProperty(ref _orderCustomerName, value);
        }

        private string _orderRoomNumber = "";
        public string OrderRoomNumber
        {
            get => _orderRoomNumber;
            set
            {
                if (SetProperty(ref _orderRoomNumber, value))
                {
                    AutoFillCustomerInfo(value);
                    OnPropertyChanged(nameof(CanChooseRoomBill));
                    OnPropertyChanged(nameof(PaymentSummaryText));
                    if (!string.IsNullOrWhiteSpace(value))
                    {
                        OrderPaymentType = FoodPaymentType.RoomBill;
                    }
                    else
                    {
                        OrderPaymentType = FoodPaymentType.SeparateBill;
                    }
                }
            }
        }

        public bool CanChooseRoomBill => !string.IsNullOrWhiteSpace(OrderRoomNumber);

        private void AutoFillCustomerInfo(string roomNumber)
        {
            string norm = DataService.NormalizeRoomNumber(roomNumber);
            if (!string.IsNullOrEmpty(norm))
            {
                var room = Data.HotelRooms.FirstOrDefault(r =>
                    DataService.NormalizeRoomNumber(r.RoomNumber) == norm ||
                    string.Equals(r.RoomNumber, norm, StringComparison.OrdinalIgnoreCase));
                if (room != null && !string.IsNullOrWhiteSpace(room.CustomerName))
                {
                    OrderCustomerName = room.CustomerName;
                }
                else
                {
                    OrderCustomerName = "";
                }
            }
            else
            {
                OrderCustomerName = "";
            }
        }

        private FoodPaymentType _orderPaymentType = FoodPaymentType.DirectPayment;
        public FoodPaymentType OrderPaymentType
        {
            get => _orderPaymentType;
            set
            {
                if (SetProperty(ref _orderPaymentType, value))
                {
                    OnPropertyChanged(nameof(IsDirectPayment));
                    OnPropertyChanged(nameof(IsRoomBill));
                    OnPropertyChanged(nameof(IsCashPayment));
                    OnPropertyChanged(nameof(IsBankTransferPayment));
                    OnPropertyChanged(nameof(PaymentSummaryText));
                }
            }
        }

        public bool IsDirectPayment => OrderPaymentType == FoodPaymentType.DirectPayment;

        public bool IsRoomBill
        {
            get => OrderPaymentType == FoodPaymentType.ChargeToRoom;
            set
            {
                if (value && CanChooseRoomBill)
                {
                    OrderPaymentType = FoodPaymentType.ChargeToRoom;
                }
            }
        }

        private string _paymentMethod = "Tiền mặt";
        public string PaymentMethod
        {
            get => _paymentMethod;
            set
            {
                if (SetProperty(ref _paymentMethod, value))
                {
                    OnPropertyChanged(nameof(IsCashPayment));
                    OnPropertyChanged(nameof(IsBankTransferPayment));
                    OnPropertyChanged(nameof(PaymentSummaryText));
                }
            }
        }

        public bool IsCashPayment
        {
            get => OrderPaymentType == FoodPaymentType.DirectPayment && PaymentMethod == "Tiền mặt";
            set
            {
                if (value)
                {
                    OrderPaymentType = FoodPaymentType.DirectPayment;
                    PaymentMethod = "Tiền mặt";
                }
            }
        }

        public bool IsBankTransferPayment
        {
            get => OrderPaymentType == FoodPaymentType.DirectPayment && PaymentMethod == "Chuyển khoản";
            set
            {
                if (value)
                {
                    OrderPaymentType = FoodPaymentType.DirectPayment;
                    PaymentMethod = "Chuyển khoản";
                }
            }
        }

        private string _orderNote = "";
        public string OrderNote
        {
            get => _orderNote;
            set => SetProperty(ref _orderNote, value);
        }

        private bool _isOrderNoteVisible = false;
        public bool IsOrderNoteVisible
        {
            get => _isOrderNoteVisible;
            set
            {
                if (SetProperty(ref _isOrderNoteVisible, value))
                {
                    OnPropertyChanged(nameof(OrderNoteToggleText));
                }
            }
        }

        public string OrderNoteToggleText => IsOrderNoteVisible
            ? "− Ẩn ghi chú đơn hàng"
            : "+ Thêm ghi chú đơn hàng";

        public ObservableCollection<FoodOrderItem> CurrentCartItems { get; } = new();

        public decimal CartTotal => CurrentCartItems.Sum(x => x.TotalPrice);
        public int CartLineCount => CurrentCartItems.Sum(x => x.Quantity);
        public int CartDistinctCount => CurrentCartItems.Count;
        public string CartSummaryText => $"{CartLineCount} sản phẩm · {CartDistinctCount} mặt hàng";
        public bool HasCartItems => CurrentCartItems.Count > 0;

        public string PaymentSummaryText
        {
            get
            {
                if (OrderPaymentType == FoodPaymentType.ChargeToRoom)
                {
                    string room = DataService.NormalizeRoomNumber(OrderRoomNumber);
                    return string.IsNullOrWhiteSpace(room)
                        ? "Ghi nợ vào phòng"
                        : $"Ghi nợ vào phòng P.{room}";
                }

                return PaymentMethod == "Chuyển khoản"
                    ? "Thanh toán chuyển khoản"
                    : "Thanh toán tiền mặt";
            }
        }

        // Commands
        public ICommand AddToCartCommand { get; }
        public ICommand QuickAddSingleCommand { get; }
        public ICommand IncreaseQtyCommand { get; }
        public ICommand DecreaseQtyCommand { get; }
        public ICommand RemoveFromCartCommand { get; }
        public ICommand IncreaseCartItemCommand { get; }
        public ICommand DecreaseCartItemCommand { get; }
        public ICommand ToggleOrderNoteCommand { get; }
        public ICommand PlaceOrderCommand { get; }
        public ICommand PrintBillCommand { get; }

        public FoodViewModel()
        {
            AddToCartCommand = new RelayCommand(AddToCart);
            QuickAddSingleCommand = new RelayCommand(QuickAddSingle);
            IncreaseQtyCommand = new RelayCommand(p => { if (p is FoodItem food) food.SelectedQty++; });
            DecreaseQtyCommand = new RelayCommand(p => { if (p is FoodItem food && food.SelectedQty > 1) food.SelectedQty--; });
            RemoveFromCartCommand = new RelayCommand(RemoveFromCart);
            IncreaseCartItemCommand = new RelayCommand(IncreaseCartItem);
            DecreaseCartItemCommand = new RelayCommand(DecreaseCartItem);
            ToggleOrderNoteCommand = new RelayCommand(_ => IsOrderNoteVisible = !IsOrderNoteVisible);
            PlaceOrderCommand = new RelayCommand(PlaceOrder);
            PrintBillCommand = new RelayCommand(PrintBill);

            CurrentCartItems.CollectionChanged += (s, e) =>
            {
                RaiseCartSummaryChanged();
                RefreshCartIndicators();
            };

            ApplyFoodFilter();

            Data.FoodItems.CollectionChanged += (s, e) =>
            {
                if (e.NewItems != null)
                {
                    foreach (FoodItem food in e.NewItems)
                    {
                        AttachFoodItemListeners(food);
                    }
                }

                ApplyFoodFilter();
                RefreshCartIndicators();
            };

            foreach (var food in Data.FoodItems)
            {
                AttachFoodItemListeners(food);
            }

            Auth.AuthStateChanged += () =>
            {
                OnPropertyChanged(nameof(IsAdmin));
                OnPropertyChanged(nameof(IsReceptionist));
            };
        }

        private void AttachFoodItemListeners(FoodItem food)
        {
            food.PropertyChanged += (s, e) =>
            {
                if (e.PropertyName == nameof(FoodItem.IsActive) ||
                    e.PropertyName == nameof(FoodItem.Status) ||
                    e.PropertyName == nameof(FoodItem.Name) ||
                    e.PropertyName == nameof(FoodItem.Price) ||
                    e.PropertyName == nameof(FoodItem.StockQuantity) ||
                    e.PropertyName == nameof(FoodItem.Category))
                {
                    ApplyFoodFilter();
                }
            };
        }

        public void ApplyFoodFilter()
        {
            string rawQ = (SearchFoodQuery ?? "").Trim();
            string cleanQ = TextSearchHelper.ToSearchKey(rawQ);

            // Lấy tất cả các món đang phục vụ
            var activeItems = Data.FoodItems.Where(x => x.IsActive).ToList();

            AllFoodItemCount = activeItems.Count;
            FoodOnlyItemCount = activeItems.Count(x => !x.IsDrink);
            DrinkOnlyItemCount = activeItems.Count(x => x.IsDrink);

            OnPropertyChanged(nameof(AllFoodItemCount));
            OnPropertyChanged(nameof(FoodOnlyItemCount));
            OnPropertyChanged(nameof(DrinkOnlyItemCount));
            OnPropertyChanged(nameof(AllCategoryDisplay));
            OnPropertyChanged(nameof(FoodCategoryDisplay));
            OnPropertyChanged(nameof(DrinkCategoryDisplay));

            foreach (var item in activeItems)
            {
                item.IsSearchMatched = !string.IsNullOrWhiteSpace(cleanQ) &&
                                       TextSearchHelper.MatchPrepared(item.Name, cleanQ);
            }

            IEnumerable<FoodItem> query = activeItems;

            if (_selectedFoodCategory == "Đồ ăn")
            {
                query = query.Where(x => !x.IsDrink);
            }
            else if (_selectedFoodCategory == "Thức uống")
            {
                query = query.Where(x => x.IsDrink);
            }

            if (!string.IsNullOrWhiteSpace(cleanQ))
            {
                query = query.Where(x => x.IsSearchMatched);
            }

            var items = query
                .OrderBy(x => x.Id)
                .ToList();

            // Tách riêng 2 nhóm để hiển thị ngăn nắp theo hàng, có đường kẻ phân cách khi xem "Tất cả"
            FilteredFoodOnlyItems.ReplaceRange(items.Where(x => !x.IsDrink));
            FilteredDrinkOnlyItems.ReplaceRange(items.Where(x => x.IsDrink));

            OnPropertyChanged(nameof(HasFoodResults));
            OnPropertyChanged(nameof(HasDrinkResults));
            OnPropertyChanged(nameof(HasFilteredItems));
            OnPropertyChanged(nameof(FoodGroupCountText));
            OnPropertyChanged(nameof(DrinkGroupCountText));
        }

        private void RaiseCartSummaryChanged()
        {
            OnPropertyChanged(nameof(CartTotal));
            OnPropertyChanged(nameof(CartLineCount));
            OnPropertyChanged(nameof(CartDistinctCount));
            OnPropertyChanged(nameof(CartSummaryText));
            OnPropertyChanged(nameof(HasCartItems));
        }

        private void RefreshCartIndicators()
        {
            var ids = CurrentCartItems.Select(x => x.FoodItemId).ToHashSet();
            foreach (var food in Data.FoodItems)
            {
                food.IsInCart = ids.Contains(food.Id);
            }
        }

        private void QuickAddSingle(object? parameter)
        {
            if (parameter is FoodItem food)
            {
                AddFoodWithQuantity(food, 1);
            }
        }

        private void AddToCart(object? parameter)
        {
            if (parameter is FoodItem food)
            {
                int qty = Math.Max(1, food.SelectedQty);
                AddFoodWithQuantity(food, qty);
                food.SelectedQty = 1;
            }
        }

        private void AddFoodWithQuantity(FoodItem food, int qty)
        {
            if (!food.IsAvailable)
            {
                MessageBox.Show($"Mặt hàng [{food.Name}] hiện không sẵn sàng phục vụ!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            var existing = CurrentCartItems.FirstOrDefault(x => x.FoodItemId == food.Id);
            int currentInCart = existing != null ? existing.Quantity : 0;
            int requestedTotal = currentInCart + qty;

            if (requestedTotal > food.StockQuantity)
            {
                MessageBox.Show($"Không thể thêm {qty} {food.RetailUnit}. Tồn kho chỉ còn {food.StockQuantity} {food.RetailUnit}!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            if (existing != null)
            {
                existing.Quantity += qty;
            }
            else
            {
                var newItem = new FoodOrderItem
                {
                    FoodItemId = food.Id,
                    FoodItemName = food.Name,
                    RetailUnit = food.RetailUnit,
                    Price = food.Price,
                    CostPrice = food.AverageCostPrice,
                    Quantity = qty
                };
                newItem.PropertyChanged += (s, e) =>
                {
                    if (e.PropertyName == nameof(FoodOrderItem.Quantity))
                    {
                        RaiseCartSummaryChanged();
                    }
                };
                CurrentCartItems.Add(newItem);
            }

            RaiseCartSummaryChanged();
        }

        private void IncreaseCartItem(object? parameter)
        {
            if (parameter is not FoodOrderItem item) return;

            var food = Data.FoodItems.FirstOrDefault(x => x.Id == item.FoodItemId);
            if (food != null && item.Quantity + 1 > food.StockQuantity)
            {
                MessageBox.Show($"Không thể tăng thêm. Tồn kho chỉ còn {food.StockQuantity} {food.RetailUnit}!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            item.Quantity++;
        }

        private void DecreaseCartItem(object? parameter)
        {
            if (parameter is FoodOrderItem item && item.Quantity > 1)
            {
                item.Quantity--;
            }
        }

        /// <summary>
        /// Áp dụng số lượng người dùng nhập trực tiếp vào giỏ hàng.
        /// Tự chuẩn hóa về tối thiểu 1 và không vượt quá tồn kho hiện tại.
        /// </summary>
        public void CommitCartItemQuantity(FoodOrderItem item, int requestedQuantity)
        {
            if (item == null) return;

            var food = Data.FoodItems.FirstOrDefault(x => x.Id == item.FoodItemId);
            int maxStock = food?.StockQuantity ?? int.MaxValue;

            if (requestedQuantity < 1)
                requestedQuantity = 1;

            if (requestedQuantity > maxStock)
            {
                requestedQuantity = Math.Max(1, maxStock);
                MessageBox.Show(
                    $"Số lượng vượt quá tồn kho. Đã điều chỉnh về {requestedQuantity} {item.RetailUnit}.",
                    "Thông báo", MessageBoxButton.OK, MessageBoxImage.Warning);
            }

            item.Quantity = requestedQuantity;
            RaiseCartSummaryChanged();
        }

        private void RemoveFromCart(object? parameter)
        {
            if (parameter is FoodOrderItem item)
            {
                CurrentCartItems.Remove(item);
                RaiseCartSummaryChanged();
            }
        }

        private void PlaceOrder(object? parameter)
        {
            if (CurrentCartItems.Count == 0)
            {
                MessageBox.Show("Vui lòng chọn ít nhất 1 mặt hàng vào giỏ hàng!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            // Kiểm tra lại số lượng trực tiếp trong giỏ trước khi tạo đơn.
            foreach (var item in CurrentCartItems)
            {
                var stockFood = Data.FoodItems.FirstOrDefault(x => x.Id == item.FoodItemId);
                if (stockFood == null || !stockFood.IsAvailable)
                {
                    MessageBox.Show(
                        $"Mặt hàng [{item.FoodItemName}] hiện không còn sẵn sàng phục vụ. Vui lòng xóa khỏi giỏ.",
                        "Thông báo", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                if (item.Quantity < 1 || item.Quantity > stockFood.StockQuantity)
                {
                    MessageBox.Show(
                        $"Số lượng [{item.FoodItemName}] không hợp lệ. Tồn kho còn {stockFood.StockQuantity} {stockFood.RetailUnit}.",
                        "Thông báo", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }
            }

            if (!string.IsNullOrWhiteSpace(OrderCustomerName) && OrderCustomerName.Any(char.IsDigit))
            {
                MessageBox.Show("Họ & Tên khách hàng không được chứa chữ số!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            string custName = string.IsNullOrWhiteSpace(OrderCustomerName) ? "Khách vãng lai" : OrderCustomerName.Trim();
            string roomNum = DataService.NormalizeRoomNumber(OrderRoomNumber);

            string actor = Data.GetCurrentActorName();
            var order = new FoodOrder
            {
                CustomerName = custName,
                RoomNumber = roomNum,
                PaymentType = OrderPaymentType,
                PaymentMethod = IsDirectPayment ? PaymentMethod : "Ghi nợ vào phòng",
                TotalAmount = CartTotal,
                Note = OrderNote?.Trim() ?? "",
                DaThanhToan = IsDirectPayment,
                RecordedBy = actor,
                PaidByStaffName = IsDirectPayment ? actor : "",
                ThoiGianThanhToan = IsDirectPayment ? DateTime.Now : null
            };

            foreach (var item in CurrentCartItems)
            {
                order.Items.Add(new FoodOrderItem
                {
                    FoodItemId = item.FoodItemId,
                    FoodItemName = item.FoodItemName,
                    RetailUnit = item.RetailUnit,
                    Price = item.Price,
                    CostPrice = item.CostPrice,
                    Quantity = item.Quantity
                });
            }

            // Hiện hóa đơn trước
            var win = new Views.InvoiceBillWindow(order);
            bool? res = win.ShowDialog();

            // Chỉ khi bấm "Đóng & Hoàn Tất" (res == true && IsConfirmed == true) mới lưu xuống DB và xóa giỏ hàng
            if (res == true && win.IsConfirmed)
            {
                Data.AddFoodOrder(order);
                CurrentCartItems.Clear();
                OrderCustomerName = "";
                OrderRoomNumber = "";
                OrderNote = "";
                OnPropertyChanged(nameof(CartTotal));
                ApplyFoodFilter();
            }
            // Nếu bấm Hủy Bỏ hoặc dấu X: Giữ nguyên giỏ hàng để tiếp tục chỉnh sửa
        }

        private void PrintBill(object? parameter)
        {
            if (parameter is FoodOrder order)
            {
                var win = new Views.InvoiceBillWindow(order);
                win.ShowDialog();
            }
        }
    }
}
