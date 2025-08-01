using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using SlimWaist.Models;
using SlimWaist.Views;

namespace SlimWaist.ViewModels
{
    public partial class AllMealsVM(DayMealsVM dayMealsVM) : BaseVM
    {
        private readonly DayMealsVM _dayMealsVM = dayMealsVM;
        [ObservableProperty]
        private List<Meal> _allMeals;
        [ObservableProperty]
        private List<Meal> _breakfastMeals;
        [ObservableProperty]
        private List<Meal> _lunchMeals;
        [ObservableProperty]
        private List<Meal> _dinnerMeals;
        [ObservableProperty]

        private List<Meal> _SelectedMeals;
        [ObservableProperty]
        private int _selectedTab = 1;

        public async Task Init()
        {
            AllMeals = await App._dataContext.LoadAsync<Meal>();
            //AllMeals.ForEach(x => x.IsSelected = false);

            var breakfastMeals = await App._dataContext.LoadAsync<Meal>();
            BreakfastMeals = breakfastMeals.Where(x => x.MealTypeId == 0).ToList();
            var lunchMeals = await App._dataContext.LoadAsync<Meal>();
            LunchMeals = lunchMeals.Where(x => x.MealTypeId == 1).ToList();
            var dinnerMeals = await App._dataContext.LoadAsync<Meal>();
            DinnerMeals = dinnerMeals.Where(x => x.MealTypeId == 2).ToList();
            SelectedMeals = new List<Meal>();
        }

        [RelayCommand]
        private async Task CreateDayPlan()
        {
            if (SelectedMeals.Count > 0)
            {
                DayMealsPage.Meals = SelectedMeals;

                await GoToAsyncWithStack(nameof(DayMealsPage), true);
            }
        }

        [RelayCommand]
        private async Task UpdateSelectedMeal(Meal meal)
        {
            //if (meal.IsSelected == true)
            //{
            //    meal.IsSelected = false;
            //}
            //else
            //{
            //    meal.IsSelected = true;
            //}
            await App._dataContext.UpdateMeal(meal);

            //if (meal.IsSelected == true)
            //{

            //    SelectedMeals.Add(meal);
            //    //if (CheckedDrugs.Count > 0)
            //    //{
            //    //    IsLabelVisible = true;
            //    //    IsButtonVisible = true;
            //    //}
            //    //CountCheckedDrugs = CheckedDrugs.Count.ToString();
            //}
            //else
            //{
            //    SelectedMeals.Remove(meal);
            //    //if (CheckedDrugs.Count < 1)
            //    //{
            //    //    IsLabelVisible = false;
            //    //    IsButtonVisible = false;
            //    //}

            //    //CountCheckedDrugs = CheckedDrugs.Count.ToString();
            //}
        }
        [RelayCommand]
        private async Task SelectedTabChanged()
        {
            AllMeals = await App._dataContext.LoadAsync<Meal>();
            var breakfastMeals = await App._dataContext.LoadAsync<Meal>();
            BreakfastMeals = breakfastMeals.Where(x => x.MealTypeId == 0).ToList();
            var lunchMeals = await App._dataContext.LoadAsync<Meal>();
            LunchMeals = lunchMeals.Where(x => x.MealTypeId == 1).ToList();
            var dinnerMeals = await App._dataContext.LoadAsync<Meal>();
            DinnerMeals = dinnerMeals.Where(x => x.MealTypeId == 2).ToList();
        }

        [RelayCommand]
        private async Task GoToMealDetailsPage(Meal meal)
        {
            var parameter = new Dictionary<string, object>
            {
                [nameof(MealDetailVM.Meal)] = meal,
            };
            await GoToAsyncWithStackAndParameter(nameof(MealDetailPage), animate: true, parameter);

        }

    }
}
