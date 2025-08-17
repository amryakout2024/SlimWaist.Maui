using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using SlimWaist.Languages;
using SlimWaist.Models;
using SlimWaist.Views;
using System.Globalization;

//using Refit;

namespace SlimWaist.ViewModels
{
    public partial class CartVM(ItemVM itemVM) : BaseVM
    {
        //private readonly IPopupService _popupService = popupService;
        [ObservableProperty]
        private List<CartItem> _cartItems;

        [ObservableProperty]
        private bool _isPresented;

        [ObservableProperty]
        private string _cartTotalCalories;
        private readonly ItemVM _itemVM = itemVM;

        public static event EventHandler<int>? TotalCartCountChanged;

        public async Task Init()
        {
            CartItems = await App._dataContext.LoadAsync<CartItem>();

            //CartTotalCalories = Math.Round(CartItems.Select(x => x.FoodCalories).Sum(), 1).ToString();

            await NotifyCartCountChange();

        }

        private async Task NotifyCartCountChange()
        {
            //App.CartItems = await App._dataContext.LoadAsync<CartItem>();

            //App.TotalCartCount = App.CartItems.Count();

            ////when make invoke it fires the event that implemented in the BadgeShellBottomNavViewAppearanceTracker class
            ////invoke only fire not create it as method

            //TotalCartCountChanged?.Invoke(null, App.TotalCartCount);
        }

        [RelayCommand]
        private async Task ClearCart()
        {
            await App._dataContext.ClearAllAsync<CartItem>();

            await Init();
        }

        [RelayCommand]
        private async Task AddCartItem()
        {
            if (CartItems.Count > 0)
            {
                string result = "";

                switch (HomeVM.CurrentMembership.CultureInfo)
                {
                    case "ar-SA":
                        result = await Shell.Current.DisplayPromptAsync(
                        "",
                        "حفظ الوجبة",
                        accept: "حفظ",
                        cancel: "إلغاء", "ادخل اسم الوجبة");
                        break;

                    default:
                        result = await Shell.Current.DisplayPromptAsync(
                        "",
                        "Save meal",
                        accept: "Save",
                        cancel: "Cancel", "Enter meal name");
                        break;
                };

                if (!string.IsNullOrEmpty(result))
                {
                    var previousmeal = await App._dataContext.FindMealAsync(result);

                    if (!previousmeal)
                    {
                        //await App._dataContext.InsertAsync<Meal>(new Meal()
                        //{
                        //    MealName = result,
                        //    MealType = CartItems.FirstOrDefault()?.MealType,
                        //    TotalMealCalories = Math.Round(CartItems.Select(x => x.FoodCalories).Sum(), 1),
                        //    TotalFoodCarb = Math.Round(CartItems.Select(x => x.FoodCarb).Sum(), 1),
                        //    TotalFoodProtien = Math.Round(CartItems.Select(x => x.FoodProtien).Sum(), 1),
                        //    TotalFoodFat = Math.Round(CartItems.Select(x => x.FoodFat).Sum(), 1),
                        //    TotalFoodFibers = Math.Round(CartItems.Select(x => x.FoodFibers).Sum(), 1)
                        //});

                        //foreach (var cartitem in CartItems)
                        //{
                        //    await App._dataContext.InsertAsync<MealDetail>(new MealDetail()
                        //    {
                        //        MealName = result,
                        //        MealType = cartitem.MealType,
                        //        FoodId = cartitem.FoodId,
                        //        FoodName = cartitem.FoodName,
                        //        Quantity = cartitem.Quantity,
                        //        FoodCalories = cartitem.FoodCalories,
                        //        FoodCarb = cartitem.FoodCarb,
                        //        FoodProtien = cartitem.FoodProtien,
                        //        FoodFat = cartitem.FoodFat,
                        //        FoodFibers = cartitem.FoodFibers
                        //    });

                        //}

                        await ClearCart();

                        await ShowToastAsync(AppResource.ResourceManager.GetString("Addedsuccessfully",CultureInfo.CurrentCulture));

                    }
                    else
                    {
                        await ShowToastAsync(AppResource.ResourceManager.GetString("Mealnameexistsbefore", CultureInfo.CurrentCulture));
                    }

                    await NotifyCartCountChange();

                }

            }
            else
            {
                await ShowAlertAsync("", AppResource.ResourceManager.GetString("Noitemstobeadded", CultureInfo.CurrentCulture));
            }

        }

        [RelayCommand]
        private async Task GoToItemPage(CartItem cartItem)
        {
            var parameter = new Dictionary<string, object>
            {
                [nameof(ItemVM.CartItem)] = cartItem,
            };
            await GoToAsyncWithStackAndParameter(nameof(ItemPage), animate: true, parameter);
        }

    }

}
