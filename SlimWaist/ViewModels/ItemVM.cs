using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using SlimWaist.Models;

namespace SlimWaist.ViewModels
{
    [QueryProperty(nameof(CartItem), nameof(CartItem))]
    public partial class ItemVM(DataContext dataContext) : BaseVM
    {
        [ObservableProperty]
        private CartItem _cartItem;

        [ObservableProperty]
        private string _foodName;

        [ObservableProperty]
        private string _quantity;

        [ObservableProperty]
        private List<string> _categories;

        [ObservableProperty]
        private string _foodCalories;

        [ObservableProperty]
        private string _foodCarb;

        [ObservableProperty]
        private string _foodProtien;

        [ObservableProperty]
        private string _foodFat;

        [ObservableProperty]
        private string _foodFibers;

        private readonly DataContext _dataContext = dataContext;

        [ObservableProperty]
        private List<CartItem> _cartItems;

        private List<CartItem> CartItemsFromDatabase;

        public static int TotalCartCount { get; set; }

        public static event EventHandler<int>? TotalCartCountChanged;

        public async Task Init()
        {
            CartItems = await _dataContext.LoadAsync<CartItem>();

            FoodName = CartItem.FoodName;

            var existingItem = CartItems.FirstOrDefault(ci => ci.FoodId == CartItem.FoodId);

            if (existingItem is not null)
            {

                Quantity = existingItem.Quantity.ToString();

                FoodCalories = existingItem.FoodCalories.ToString("F1");

                FoodCarb = existingItem.FoodCarb.ToString("F1");

                FoodProtien = existingItem.FoodProtien.ToString("F1");

                FoodFat = existingItem.FoodFat.ToString("F1");

                FoodFibers = existingItem.FoodFibers.ToString("F1");

            }
            else
            {
                Quantity = null;

                FoodCalories = "0.0";
                FoodCarb = "0.0";
                FoodProtien = "0.0";
                FoodFat = "0.0";
                FoodFibers = "0.0";

            }
            NotifyCartCountChange();

        }

        [RelayCommand]
        private async Task GoBack()
        {
            await Shell.Current.GoToAsync("..");
        }

        [RelayCommand]
        private async Task AddItemToCart()
        {
            var existingItem = CartItems.FirstOrDefault(ci => ci.FoodId == CartItem.FoodId);

            if (existingItem is not null)
            {
                if (Convert.ToInt32(Quantity) > 0)
                {
                    existingItem.Quantity = Convert.ToInt32(Quantity);

                    await _dataContext.UpdateCartItem(existingItem);

                    //await GoToAsyncWithShell(nameof(FoodsPage), true);

                    await Shell.Current.GoToAsync("..");

                    await ShowToastAsync("تم تحديث الصنف في السلة");

                }
                else
                {
                    await _dataContext.DeleteAsync<CartItem>(existingItem);

                    //await GoToAsyncWithShell(nameof(FoodsPage), true);
                    await Shell.Current.GoToAsync("..");

                    await ShowToastAsync("تم ازالة الصنف من السلة");

                }

            }
            else
            {
                var cartItem = new CartItem
                {
                    MealType = this.CartItem.MealType,
                    FoodId = CartItem.FoodId,
                    FoodName = FoodName,
                    FoodCalories = Math.Round(Convert.ToDouble(FoodCalories), 1),
                    FoodCarb = Math.Round(Convert.ToDouble(FoodCarb), 1),
                    FoodProtien = Math.Round(Convert.ToDouble(FoodProtien), 1),
                    FoodFat = Math.Round(Convert.ToDouble(FoodFat), 1),
                    FoodFibers = Math.Round(Convert.ToDouble(FoodFibers), 1),
                    Quantity = Convert.ToInt32(Quantity)
                };

                await _dataContext.InsertAsync<CartItem>(cartItem);

                //await GoToAsyncWithShell(nameof(FoodsPage), true);
                await Shell.Current.GoToAsync("..");

                await ShowToastAsync("تم اضافه الصنف للسلة");
            }

            NotifyCartCountChange();
        }

        [RelayCommand]
        private async Task ChangeQuantity()
        {
            if (!string.IsNullOrEmpty(Quantity) && Convert.ToDouble(Quantity) > 0)
            {
                FoodCalories = Math.Round((CartItem.FoodCalories * Convert.ToDouble(Quantity) / 100), 1).ToString("F1");
                FoodCarb = Math.Round((CartItem.FoodCarb * Convert.ToDouble(Quantity) / 100), 1).ToString("F1");
                FoodProtien = Math.Round((CartItem.FoodProtien * Convert.ToDouble(Quantity) / 100), 1).ToString("F1");
                FoodFat = Math.Round((CartItem.FoodFat * Convert.ToDouble(Quantity) / 100), 1).ToString("F1");
                FoodFibers = Math.Round((CartItem.FoodFibers * Convert.ToDouble(Quantity) / 100), 1).ToString("F1");
                FoodCarb = Math.Round((CartItem.FoodCarb * Convert.ToDouble(Quantity) / 100), 1).ToString("F1");
            }
            else
            {
                FoodCalories = "0.0";
                FoodCarb = "0.0";
                FoodProtien = "0.0";
                FoodFat = "0.0";
                FoodFibers = "0.0";
            }
        }

        private async Task NotifyCartCountChange()
        {
            TotalCartCount = CartItems.Count();

            TotalCartCountChanged?.Invoke(null, TotalCartCount);
        }


    }
}
