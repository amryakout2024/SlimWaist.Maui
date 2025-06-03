using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using SlimWaist.Models;
using SlimWaist.Views;
using System.Globalization;

namespace SlimWaist.ViewModels
{
    public partial class FoodsVM(CartVM cartVM) : BaseVM
    {
        private readonly CartVM _cartVM = cartVM;

        [ObservableProperty]
        private string _mealTypeName;

        private List<Food> FoodsFromDatabase;

        [ObservableProperty]
        private List<Food> _foods;

        [ObservableProperty]
        private string _mealType;

        [ObservableProperty]
        private string _searchName;

        [ObservableProperty]
        private Food _selectedFood;

        [ObservableProperty]
        private bool _isBottomSheetPresented;


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

        public async Task Init()
        {
            MealTypeName = App.mealTypes.Where(x => x.MealTypeId == HomeVM.CurrentMeal.MealTypeId).FirstOrDefault()?.MealTypeName ?? "";

            FoodsFromDatabase = await App.dataContext.LoadAsync<Food>();

            Foods = FoodsFromDatabase;

            IsBottomSheetPresented = false;

        }
        [RelayCommand]
        private void ChangeQuantity()
        {
            //if (!string.IsNullOrWhiteSpace(Quantity))
            //{
            //    FoodCalories = Math.Round((CartItem.FoodCalories * Convert.ToDouble(Quantity) / 100), 1).ToString("F1");
            //    FoodCarb = Math.Round((CartItem.FoodCarb * Convert.ToDouble(Quantity) / 100), 1).ToString("F1");
            //    FoodProtien = Math.Round((CartItem.FoodProtien * Convert.ToDouble(Quantity) / 100), 1).ToString("F1");
            //    FoodFat = Math.Round((CartItem.FoodFat * Convert.ToDouble(Quantity) / 100), 1).ToString("F1");
            //    FoodFibers = Math.Round((CartItem.FoodFibers * Convert.ToDouble(Quantity) / 100), 1).ToString("F1");
            //    FoodCarb = Math.Round((CartItem.FoodCarb * Convert.ToDouble(Quantity) / 100), 1).ToString("F1");
            //}
            //else
            //{
            //    FoodCalories = "0.0";
            //    FoodCarb = "0.0";
            //    FoodProtien = "0.0";
            //    FoodFat = "0.0";
            //    FoodFibers = "0.0";
            //}
        }
        [RelayCommand]
        private async Task AddItemToCart()
        {
            //var existingItem = App.CartItems.FirstOrDefault(ci => ci.FoodId == CartItem.FoodId);

            //if (existingItem is not null)
            //{
            //    if (Convert.ToInt32(Quantity) > 0)
            //    {
            //        existingItem.Quantity = Convert.ToInt32(Quantity);

            //        await App.dataContext.UpdateCartItem(existingItem);

            //        await ShowToastAsync(AppResource.ResourceManager.GetString("Updatedsuccessfully", CultureInfo.CurrentCulture) ?? "");

            //        await Shell.Current.GoToAsync("..");
            //    }
            //    else
            //    {
            //        await App.dataContext.DeleteAsync<CartItem>(existingItem);

            //        await ShowToastAsync(AppResource.ResourceManager.GetString("Deletedsuccessfully", CultureInfo.CurrentCulture) ?? "");

            //        await Shell.Current.GoToAsync("..");

            //    }

            //}
            //else
            //{
            //    var cartItem = new CartItem
            //    {
            //        MealType = this.CartItem.MealType,
            //        FoodId = CartItem.FoodId,
            //        FoodName = FoodName,
            //        FoodCategory = FoodCategory,
            //        FoodCalories = Math.Round(Convert.ToDouble(FoodCalories), 1),
            //        FoodCarb = Math.Round(Convert.ToDouble(FoodCarb), 1),
            //        FoodProtien = Math.Round(Convert.ToDouble(FoodProtien), 1),
            //        FoodFat = Math.Round(Convert.ToDouble(FoodFat), 1),
            //        FoodFibers = Math.Round(Convert.ToDouble(FoodFibers), 1),
            //        Quantity = Convert.ToInt32(Quantity)
            //    };

            //    await App.dataContext.InsertAsync<CartItem>(cartItem);

            //    await ShowToastAsync(AppResource.ResourceManager.GetString("Addedsuccessfully", CultureInfo.CurrentCulture) ?? "");

            //    await Shell.Current.GoToAsync("..");
            //}

        }

        [RelayCommand]
        private async Task ShowBottomSheet(Food food)
        {
            SelectedFood = food;

            FoodName = SelectedFood.FoodName ?? "";

            FoodCategory = SelectedFood.FoodCategory ?? "";

            //var existingItem = App.CartItems.FirstOrDefault(ci => ci.FoodId == CartItem.FoodId);

            //if (existingItem is not null)
            //{
            //    Quantity = existingItem.Quantity.ToString();

            //    FoodCalories = existingItem.FoodCalories.ToString("F1");

            //    FoodCarb = existingItem.FoodCarb.ToString("F1");

            //    FoodProtien = existingItem.FoodProtien.ToString("F1");

            //    FoodFat = existingItem.FoodFat.ToString("F1");

            //    FoodFibers = existingItem.FoodFibers.ToString("F1");
            //}
            //else
            //{
            //    Quantity = null;
            //    FoodCalories = "0.0";
            //    FoodCarb = "0.0";
            //    FoodProtien = "0.0";
            //    FoodFat = "0.0";
            //    FoodFibers = "0.0";
            //}

            IsBottomSheetPresented = true;
        }

        partial void OnIsBottomSheetPresentedChanged(bool value)
        {
            //IsTabbarVisible = IsBottomSheetPresented ? false : true;
        }

        [RelayCommand]
        private async Task GoToMealPage()
        {
#if ANDROID
            await Shell.Current.GoToAsync($"//{nameof(HomePage)}/{nameof(MealPage)}", animate: true);
#endif
        }

        [RelayCommand]
        private async Task MealChanged()
        {
            List<CartItem> allcartitems = await App.dataContext.LoadAsync<CartItem>();

            //if (allcartitems.Count > 0)
            //{
            //    MealType = allcartitems[0].MealType;

            //    if (IsBreakfast && MealType != Breakfast)
            //    {
            //        await ShowAlertAsync($"توجد وجبه {MealType} في السلة يرجي حفظها اولا او افراغها ");
            //    }
            //    if (IsLunch && MealType != Lunch)
            //    {
            //        await ShowAlertAsync($"توجد وجبه {MealType} في السلة يرجي حفظها اولا او افراغها");
            //    }
            //    if (IsDinner && MealType != Dinner)
            //    {
            //        await ShowAlertAsync($"توجد وجبه {MealType} في السلة يرجي حفظها اولا او افراغها");
            //    }
            //    if (MealType == Breakfast) IsBreakfast = true;
            //    else if (MealType == Lunch) IsLunch = true;
            //    else IsDinner = true;

            //}

        }

        [RelayCommand]
        private async Task Search()
        {
            if (!string.IsNullOrEmpty(SearchName))
            {
                Foods = FoodsFromDatabase.Where(x => x.FoodName.Contains(SearchName)).ToList();
            }
            else
            {
                Foods = FoodsFromDatabase;
            }
        }

        [RelayCommand]
        private async Task GoToItemPage(Food food)
        {
            //CartItem cartItem = new CartItem();
            //if (IsBreakfast)
            //{
            //    cartItem.MealType = Breakfast;
            //    cartItem.FoodId = food.FoodId;
            //    cartItem.FoodName = food.FoodName;
            //    cartItem.FoodCategory = food.FoodCategory;
            //    cartItem.FoodCalories = food.FoodCalories;
            //    cartItem.FoodCarb = food.FoodCarb;
            //    cartItem.FoodProtien = food.FoodProtien;
            //    cartItem.FoodFat = food.FoodFat;
            //    cartItem.FoodFibers = food.FoodFibers;
            //}
            //else if (IsLunch)
            //{
            //    cartItem.MealType = Lunch;
            //    cartItem.FoodId = food.FoodId;
            //    cartItem.FoodName = food.FoodName;
            //    cartItem.FoodCategory = food.FoodCategory;
            //    cartItem.FoodCalories = food.FoodCalories;
            //    cartItem.FoodCarb = food.FoodCarb;
            //    cartItem.FoodProtien = food.FoodProtien;
            //    cartItem.FoodFat = food.FoodFat;
            //    cartItem.FoodFibers = food.FoodFibers;

            //}
            //else
            //{
            //    cartItem.MealType = Dinner;
            //    cartItem.FoodId = food.FoodId;
            //    cartItem.FoodName = food.FoodName;
            //    cartItem.FoodCategory = food.FoodCategory;
            //    cartItem.FoodCalories = food.FoodCalories;
            //    cartItem.FoodCarb = food.FoodCarb;
            //    cartItem.FoodProtien = food.FoodProtien;
            //    cartItem.FoodFat = food.FoodFat;
            //    cartItem.FoodFibers = food.FoodFibers;
            //}

            //var parameter = new Dictionary<string, object>
            //{
            //    [nameof(ItemVM.CartItem)] = cartItem,
            //};
            //await GoToAsyncWithStackAndParameter(nameof(ItemPage), animate: true, parameter);
        }

        [RelayCommand]
        private async Task GoToAddFoodPage()
        {
#if ANDROID
            //await Shell.Current.GoToAsync($"//{nameof(HomePage)}/{nameof(MealPage)}/{nameof(AddFoodPage)}", animate: true);
#endif

            //await GoToAsyncWithStack(nameof(AddFoodPage), true);
        }
    }
}
