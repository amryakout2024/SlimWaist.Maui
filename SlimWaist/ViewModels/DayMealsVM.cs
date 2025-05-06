using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using SlimWaist.Models;
using SlimWaist.Views;

namespace SlimWaist.ViewModels
{
    public partial class DayMealsVM() : BaseVM
    {
        [ObservableProperty]
        private List<MealGroup> _mealGroups;

        [ObservableProperty]
        private string _dayTotalCalories;

        public async Task Init()
        {
            MealGroups = new List<MealGroup>();

            var mealdetails = await App.dataContext.LoadAsync<MealDetail>();

            var meals = DayMealsPage.Meals;

            DayTotalCalories = Math.Round(meals.Select(x => x.TotalCalories).Sum(), 1).ToString() ?? "";

            foreach (Meal meal in meals)
            {
                MealGroups.Add(new MealGroup(meal.MealId,
                    meal.MealName,
                    meal.MealType,
                    meal.IsSelected,
                    meal.TotalCalories,
                    meal.TotalFoodCarb,
                    meal.TotalFoodProtien,
                    meal.TotalFoodFat,
                    meal.TotalFoodFibers,
                    mealdetails.Where(x => x.MealName == meal.MealName).ToList()));
            }
        }

        [RelayCommand]
        private async Task ShowMealDetail(Meal meal)
        {
            var m = meal;
        }
    }
}
