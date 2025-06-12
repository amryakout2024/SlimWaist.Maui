using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microcharts;
using Microcharts.Maui;
using SkiaSharp;
using SlimWaist.Languages;
using SlimWaist.Models;
using SlimWaist.Views;
using System.Collections.ObjectModel;
using System.Globalization;

namespace SlimWaist.ViewModels
{
    public partial class MealVM(DataContext dataContext) : BaseVM
    {
        private readonly DataContext _dataContext = dataContext;

        [ObservableProperty]
        private string _mealTypeName;

        [ObservableProperty]
        private List<MealDetail> _mealDetails;

        [ObservableProperty]
        private List<CartItem> _cartItems;

        [ObservableProperty]
        private string _datedayname;

        [ObservableProperty]
        private string? _totalEnergy;

        [ObservableProperty]
        private string _foodName;

        [ObservableProperty]
        private string _quantity;

        [ObservableProperty]
        private string _foodCalories;

        [ObservableProperty]
        private string _totalMealCalories;

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

        public async Task init()
        {
            CartItems = new List<CartItem>();

            MealTypeName = App.mealTypes.Where(x => x.MealTypeId == HomeVM.CurrentMeal?.MealTypeId).FirstOrDefault()?.MealTypeName??"";

            MealDetails = _dataContext.Database.Table<MealDetail>().Where(x => x.MealId ==HomeVM.CurrentMeal.MealId).ToList()??new List<MealDetail>();

            var existingMeal = _dataContext.Database.Table<Meal>().ToList().Where(x => x.MealId == HomeVM.CurrentMeal?.MealId).FirstOrDefault();

            if (existingMeal is not null)
            {
                if (MealDetails.Count>0)
                {
                    await _dataContext.ClearAllAsync<CartItem>();

                    foreach (var mealDetail in MealDetails.Where(x=>x.MealId==existingMeal.MealId).ToList())
                    {
                        var existingFood = _dataContext.Database.Table<Food>().Where(x => x.FoodId == mealDetail.FoodId).FirstOrDefault();

                        await _dataContext.InsertAsync<CartItem>(new CartItem()
                        {
                            FoodId=existingFood.FoodId,
                            Quantity=mealDetail.Quantity,
                            FoodName=existingFood.FoodName,
                            FoodCategory=existingFood.FoodCategory,
                            TotalFoodCarb = Math.Round((existingFood.FoodCarb * mealDetail.Quantity / 100), 1),
                            TotalFoodProtien = Math.Round((existingFood.FoodProtien * mealDetail.Quantity / 100), 1),
                            TotalFoodFat = Math.Round((existingFood.FoodFat * mealDetail.Quantity / 100), 1),
                            TotalFoodFibers = Math.Round((existingFood.FoodFibers * mealDetail.Quantity / 100), 1),
                            TotalFoodCalories = Math.Round((existingFood.FoodCalories * mealDetail.Quantity / 100), 1)
                        });

                    }

                    CartItems = await _dataContext.LoadAsync<CartItem>();

                }

            }

        }

        [RelayCommand]
        private async Task GoToHomePage()
        {
#if ANDROID
            await Shell.Current.GoToAsync($"//{nameof(HomePage)}", animate: true);
#endif
        }

        [RelayCommand]
        private async Task GoToFoodsPage()
        {
#if ANDROID
            await Shell.Current.GoToAsync($"//{nameof(HomePage)}/{nameof(MealPage)}/{nameof(FoodsPage)}", animate: true);
#endif

            //await GoToAsyncWithStack($"//{nameof(HomePage)}/{nameof(MealPage)}/{nameof(FoodsPage)}", true);
        }

        [RelayCommand]
        private void SaveDiet()
        {

        }
    }
}
