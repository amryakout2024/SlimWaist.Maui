using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using SlimWaist.Models;
using SlimWaist.Views;

//using Refit;

namespace SlimWaist.ViewModels
{
    public partial class CartVM(DataContext dataContext) : BaseVM
    {
        private readonly DataContext _dataContext = dataContext;
        //private readonly IPopupService _popupService = popupService;
        [ObservableProperty]
        private List<CartItem> _cartItems;

        [ObservableProperty]
        private bool _isPresented;

        [ObservableProperty]
        private string _cartTotalCalories;

        public static event EventHandler<int>? TotalCartCountChanged;

        public async Task Init()
        {
            CartItems = await _dataContext.LoadAsync<CartItem>();

            CartTotalCalories = Math.Round(CartItems.Select(x => x.FoodCalories).Sum(), 1).ToString();

            //NotifyCartCountChange();

        }

        [RelayCommand]
        private async Task ClearCart()
        {
            await _dataContext.ClearAllAsync<CartItem>();

            await Init();
        }

        [RelayCommand]
        private async Task AddCartItem()
        {
            if (CartItems.Count > 0)
            {
                string result = await Shell.Current.DisplayPromptAsync(
                                "حفظ الوجبة",
                                "اختر اسم للوجبة",
                                accept: "حفظ",
                                cancel: "إلغاء");

                if (!string.IsNullOrEmpty(result))
                {
                    var previousmeal = await _dataContext.FindMealAsync(result);

                    if (!previousmeal)
                    {
                        await _dataContext.InsertAsync<Meal>(new Meal()
                        {
                            MealName = result,
                            MealType = CartItems.FirstOrDefault()?.MealType,
                            TotalCalories = Math.Round(CartItems.Select(x => x.FoodCalories).Sum(), 1),
                            TotalFoodCarb = Math.Round(CartItems.Select(x => x.FoodCarb).Sum(), 1),
                            TotalFoodProtien = Math.Round(CartItems.Select(x => x.FoodProtien).Sum(), 1),
                            TotalFoodFat = Math.Round(CartItems.Select(x => x.FoodFat).Sum(), 1),
                            TotalFoodFibers = Math.Round(CartItems.Select(x => x.FoodFibers).Sum(), 1)
                        });

                        foreach (var cartitem in CartItems)
                        {
                            await _dataContext.InsertAsync<MealDetail>(new MealDetail()
                            {
                                MealName = result,
                                MealType = cartitem.MealType,
                                FoodId = cartitem.FoodId,
                                FoodName = cartitem.FoodName,
                                Quantity = cartitem.Quantity,
                                FoodCalories = cartitem.FoodCalories,
                                FoodCarb = cartitem.FoodCarb,
                                FoodProtien = cartitem.FoodProtien,
                                FoodFat = cartitem.FoodFat,
                                FoodFibers = cartitem.FoodFibers
                            });

                        }

                        await ClearCart();

                        await ShowToastAsync("تم حفظ الوجبة بنجاح");

                    }
                    else
                    {
                        await ShowToastAsync("اسم الوجبة موجود مسبقا");
                    }
                }

            }
            else
            {
                await ShowAlertAsync("لا يوجد اصناف لحفظها");
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

            //FoodName = food.FoodName;
            ////Quantity = "0";
            //FoodCalories = "0";
            //FoodCarb = "0";
            //FoodProtien = "0";
            //FoodFat = "0";
            //FoodFibers = "0";

            //IsFoodSelected = true;
        }

        //private async Task NotifyCartCountChange()
        //{
        //    await Init();
        //    TotalCartCount = CartItems.Count();
        //    TotalCartCountChanged?.Invoke(null, TotalCartCount);
        //}

        //public int GetItemCartCount(int foodId)
        //{
        //    var existingItem = CartItems.FirstOrDefault(i => i.FoodId == foodId);
        //    return existingItem?.Quantity ?? 0;
        //}


        private async Task ClearCartInternalAsync(bool fromPlaceOrder)
        {
            if (!fromPlaceOrder && CartItems.Count == 0)
            {
                await ShowAlertAsync("Empty Cart", "There are no items in the cart");
                return;
            }

            //if (fromPlaceOrder  // If we are coming from PLaceOrder, we will not display this confirm dialog
            //    || await ConfirmAsync("Clear Cart?", "Do you really want to clear all the items from the cart?"))
            //{
            //    await _databaseService.ClearCartAsync();
            //    CartItems.Clear();

            //    if(!fromPlaceOrder)
            //        await ShowToastAsync("Cart cleared");

            //    NotifyCartCountChange();
            //}
        }

        [RelayCommand]
        private async Task RemoveCartItemAsync(int cartItemId)
        {
            //if (await ConfirmAsync("Remove item from Cart?", "Do you really want to delet this item from the cart?"))
            //{
            //    var existingItem = CartItems.FirstOrDefault(i => i.Id == cartItemId);
            //    if (existingItem is null)
            //        return;

            //    CartItems.Remove(existingItem);

            //    var dbCartItem = await _databaseService.GetCartItemAsync(cartItemId);
            //    if (dbCartItem is null)
            //        return;

            //    await _databaseService.DeleteCartItem(dbCartItem);

            //    await ShowToastAsync("Icecream removed from cart");
            //    NotifyCartCountChange();
            //}
        }


    }

}
