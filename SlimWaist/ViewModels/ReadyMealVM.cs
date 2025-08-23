using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microcharts;
using Microcharts.Maui;
using Microsoft.Maui.Controls.Compatibility;
using SkiaSharp;
using SlimWaist.Languages;
using SlimWaist.Models;
using SlimWaist.Views;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Threading.Tasks;

namespace SlimWaist.ViewModels
{
        [QueryProperty(nameof(Meal), nameof(Meal))]

    public partial class ReadyMealVM(DataContext dataContext) : BaseVM
    {
        public Meal Meal { get; set; }

        private readonly DataContext _dataContext = dataContext;

        [ObservableProperty]
        private string _mealName;

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
        private string _foodCalories;

        [ObservableProperty]
        private string _totalMealCalories;
        [ObservableProperty]
        private string _totalMealCarbohydrates;
        [ObservableProperty]
        private string _totalMealProtiens;
        [ObservableProperty]
        private string _totalMealFats;
        [ObservableProperty]
        private string _totalMealFibers;

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

        [ObservableProperty]
        private string _mealSize;

        [ObservableProperty]
        private DateTime _dayDietDate;

        public async Task init()
        {
            CartItems = new List<CartItem>();

            MealName = "";
            MealSize = "0";
            TotalMealCalories = "0";
            TotalMealCarbohydrates = "0";
            TotalMealProtiens = "0";
            TotalMealFats = "0";
            TotalMealFibers = "0";

            DayDietDate = this. Meal.DayDietDate;
            MealDetails = _dataContext.Database.Table<MealDetail>().Where(x => x.MealId == Meal.MealId).ToList() ?? new List<MealDetail>();

            if (MealDetails.Count > 0)
            {
                if (!string.IsNullOrEmpty(Meal.MealName))
                {
                    MealName = Meal.MealName;
                }

                await _dataContext.ClearAllAsync<CartItem>();

                foreach (var mealDetail in MealDetails)
                {
                    var existingFood = _dataContext.Database.Table<Food>().Where(x => x.FoodId == mealDetail.FoodId).FirstOrDefault();

                    MealSize = Math.Round((Convert.ToDouble(MealSize) + Convert.ToDouble(mealDetail.Quantity)), 1).ToString("F0");
                    TotalMealCalories = (Convert.ToDouble(TotalMealCalories) + Math.Round((Convert.ToDouble(existingFood.FoodCalories) * Convert.ToDouble(mealDetail.Quantity) / 100), 1)).ToString("F1");
                    TotalMealCarbohydrates = Math.Round((Convert.ToDouble(TotalMealCarbohydrates) + (Convert.ToDouble(existingFood.FoodCarb) * Convert.ToDouble(mealDetail.Quantity) / 100)), 1).ToString("F1");
                    TotalMealProtiens = Math.Round((Convert.ToDouble(TotalMealProtiens) + (Convert.ToDouble(existingFood.FoodProtien) * Convert.ToDouble(mealDetail.Quantity) / 100)), 1).ToString("F1");
                    TotalMealFats = Math.Round((Convert.ToDouble(TotalMealFats) + (Convert.ToDouble(existingFood.FoodFat) * Convert.ToDouble(mealDetail.Quantity) / 100)), 1).ToString("F1");
                    TotalMealFibers = Math.Round((Convert.ToDouble(TotalMealFibers) + (Convert.ToDouble(existingFood.FoodFibers) * Convert.ToDouble(mealDetail.Quantity) / 100)), 1).ToString("F1");

                    await _dataContext.InsertAsync<CartItem>(new CartItem()
                    {
                        FoodId = existingFood.FoodId,
                        Quantity = mealDetail.Quantity,
                        FoodName = existingFood.FoodName,
                        FoodCategory = existingFood.FoodCategory,
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

        [RelayCommand]
        private async Task GoToFoodsPage()
        {
#if ANDROID
            await Shell.Current.GoToAsync($"//{nameof(HomePage)}/{nameof(MealPage)}/{nameof(FoodsPage)}", animate: true);
#endif

            //await GoToAsyncWithStack($"//{nameof(HomePage)}/{nameof(MealPage)}/{nameof(FoodsPage)}", true);
        }
    }
}
