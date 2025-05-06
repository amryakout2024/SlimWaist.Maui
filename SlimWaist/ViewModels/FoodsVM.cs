using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using SlimWaist.Models;
using SlimWaist.Views;

namespace SlimWaist.ViewModels
{
    public partial class FoodsVM(CartVM cartVM) : BaseVM
    {
        private readonly CartVM _cartVM = cartVM;
        private List<Food> FoodsFromDatabase;

        [ObservableProperty]
        private List<Food> _foods;

        [ObservableProperty]
        private string _mealType;

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

        [ObservableProperty]
        private string _searchName;

        [ObservableProperty]
        private string _breakfast = "إفطار";
        [ObservableProperty]
        private string _lunch = "غداء";
        [ObservableProperty]
        private string _dinner = "عشاء";

        [ObservableProperty]
        private bool _isBreakfast = true;
        [ObservableProperty]
        private bool _isLunch = false;
        [ObservableProperty]
        private bool _isDinner = false;

        //[ObservableProperty]
        //private bool _isFoodSelected;

        [ObservableProperty]
        private Food _selectedFood;

        [ObservableProperty]
        private string _foodName;

        public async Task Init()
        {
            FoodsFromDatabase = await App.dataContext.LoadAsync<Food>();

            //IsFoodSelected = false;

            Foods = FoodsFromDatabase;

        }

        [RelayCommand]
        private async Task MealChanged()
        {
            List<CartItem> allcartitems = await App.dataContext.LoadAsync<CartItem>();

            if (allcartitems.Count > 0)
            {
                MealType = allcartitems[0].MealType;

                if (IsBreakfast && MealType != Breakfast)
                {
                    await ShowAlertAsync($"توجد وجبه {MealType} في السلة يرجي حفظها اولا او افراغها ");
                }
                if (IsLunch && MealType != Lunch)
                {
                    await ShowAlertAsync($"توجد وجبه {MealType} في السلة يرجي حفظها اولا او افراغها");
                }
                if (IsDinner && MealType != Dinner)
                {
                    await ShowAlertAsync($"توجد وجبه {MealType} في السلة يرجي حفظها اولا او افراغها");
                }
                if (MealType == Breakfast) IsBreakfast = true;
                else if (MealType == Lunch) IsLunch = true;
                else IsDinner = true;

            }

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
            CartItem cartItem = new CartItem();
            if (IsBreakfast)
            {
                cartItem.MealType = Breakfast;
                cartItem.FoodId = food.FoodId;
                cartItem.FoodName = food.FoodName;
                cartItem.FoodCalories = food.FoodCalories;
                cartItem.FoodCarb = food.FoodCarb;
                cartItem.FoodProtien = food.FoodProtien;
                cartItem.FoodFat = food.FoodFat;
                cartItem.FoodFibers = food.FoodFibers;
            }
            else if (IsLunch)
            {
                cartItem.MealType = Lunch;
                cartItem.FoodId = food.FoodId;
                cartItem.FoodName = food.FoodName;
                cartItem.FoodCalories = food.FoodCalories;
                cartItem.FoodCarb = food.FoodCarb;
                cartItem.FoodProtien = food.FoodProtien;
                cartItem.FoodFat = food.FoodFat;
                cartItem.FoodFibers = food.FoodFibers;

            }
            else
            {
                cartItem.MealType = Dinner;
                cartItem.FoodId = food.FoodId;
                cartItem.FoodName = food.FoodName;
                cartItem.FoodCalories = food.FoodCalories;
                cartItem.FoodCarb = food.FoodCarb;
                cartItem.FoodProtien = food.FoodProtien;
                cartItem.FoodFat = food.FoodFat;
                cartItem.FoodFibers = food.FoodFibers;

            }

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


        [RelayCommand]
        private async Task GoToAddFoodPage()
        {
            await GoToAsyncWithStack(nameof(AddFoodPage), true);
        }


        //[RelayCommand]
        //private async Task GoToHomePage()
        //{
        //    await GoToAsyncWithStack(nameof(HomePage), true);
        //}

    }
}
