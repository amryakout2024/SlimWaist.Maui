using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using SlimWaist.Languages;
using SlimWaist.Models;
using System.Globalization;
using System.Resources;

namespace SlimWaist.ViewModels
{
    [QueryProperty(nameof(CartItem), nameof(CartItem))]
    public partial class ItemVM() : BaseVM
    {
        [ObservableProperty]
        private CartItem _cartItem;

        [ObservableProperty]
        private string _foodName;

        [ObservableProperty]
        private string _quantity;

        [ObservableProperty]
        private string _foodCalories;

        [ObservableProperty]
        private string _foodCarb;

        [ObservableProperty]
        private string _foodProtien;

        [ObservableProperty]
        private string _foodFat;

        [ObservableProperty]
        private string _foodCategory;

        [ObservableProperty]
        private string _foodFibers;

        public static event EventHandler<int>? TotalCartCountChanged;

        public async Task Init()
        {
            FoodName = CartItem.FoodName ?? "";

            FoodCategory = CartItem.FoodCategory ?? "";

            var existingItem =App.CartItems.FirstOrDefault(ci => ci.FoodId == CartItem.FoodId);

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
        }

        [RelayCommand]
        private void ChangeQuantity()
        {
            if(!string.IsNullOrWhiteSpace(Quantity))
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

        [RelayCommand]
        private async Task GoBack()
        {
            await Shell.Current.GoToAsync("..");
        }

        [RelayCommand]
        private async Task AddItemToCart()
        {
            var existingItem = App.CartItems.FirstOrDefault(ci => ci.FoodId == CartItem.FoodId);

            if (existingItem is not null)
            {
                if (Convert.ToInt32(Quantity) > 0)
                {
                    existingItem.Quantity = Convert.ToInt32(Quantity);

                    await App.dataContext.UpdateCartItem(existingItem);

                    await ShowToastAsync(AppResource.ResourceManager.GetString("Updatedsuccessfully", CultureInfo.CurrentCulture) ?? "");

                    await Shell.Current.GoToAsync("..");
                }
                else
                {
                    await App.dataContext.DeleteAsync<CartItem>(existingItem);

                    await ShowToastAsync(AppResource.ResourceManager.GetString("Deletedsuccessfully", CultureInfo.CurrentCulture) ?? "");

                    await Shell.Current.GoToAsync("..");

                }

            }
            else
            {
                var cartItem = new CartItem
                {
                    MealType = this.CartItem.MealType,
                    FoodId = CartItem.FoodId,
                    FoodName = FoodName,
                    FoodCategory = FoodCategory,
                    FoodCalories = Math.Round(Convert.ToDouble(FoodCalories), 1),
                    FoodCarb = Math.Round(Convert.ToDouble(FoodCarb), 1),
                    FoodProtien = Math.Round(Convert.ToDouble(FoodProtien), 1),
                    FoodFat = Math.Round(Convert.ToDouble(FoodFat), 1),
                    FoodFibers = Math.Round(Convert.ToDouble(FoodFibers), 1),
                    Quantity = Convert.ToInt32(Quantity)
                };

                await App.dataContext.InsertAsync<CartItem>(cartItem);

                await ShowToastAsync(AppResource.ResourceManager.GetString("Addedsuccessfully", CultureInfo.CurrentCulture) ?? "");

                await Shell.Current.GoToAsync("..");
            }


            await NotifyCartCountChange();
        }

        [RelayCommand]
        private async Task NotifyCartCountChange()
        {
            App.CartItems = await App.dataContext.LoadAsync<CartItem>();

            App.TotalCartCount = App.CartItems.Count();

            //when make invoke it fires the event that implemented in the BadgeShellBottomNavViewAppearanceTracker class
            //invoke only fire not create it as method

            TotalCartCountChanged?.Invoke(null, App.TotalCartCount);
        }


    }
}
