using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using SlimWaist.Models;
using SlimWaist.Views;

namespace SlimWaist.ViewModels
{
    public partial class AllMealsVM(DataContext dataContext, DayMealsVM dayMealsVM) : BaseVM
    {
        private readonly DataContext _dataContext = dataContext;
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
            AllMeals = await _dataContext.LoadAsync<Meal>();
            AllMeals.ForEach(x => x.IsSelected = false);

            var breakfastMeals = await _dataContext.LoadAsync<Meal>();
            BreakfastMeals = breakfastMeals.Where(x => x.MealType == "إفطار").ToList();
            var lunchMeals = await _dataContext.LoadAsync<Meal>();
            LunchMeals = lunchMeals.Where(x => x.MealType == "غداء").ToList();
            var dinnerMeals = await _dataContext.LoadAsync<Meal>();
            DinnerMeals = dinnerMeals.Where(x => x.MealType == "عشاء").ToList();
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
            await _dataContext.UpdateMeal(meal);

            if (meal.IsSelected == true)
            {

                SelectedMeals.Add(meal);
                //if (CheckedDrugs.Count > 0)
                //{
                //    IsLabelVisible = true;
                //    IsButtonVisible = true;
                //}
                //CountCheckedDrugs = CheckedDrugs.Count.ToString();
            }
            else
            {
                SelectedMeals.Remove(meal);
                //if (CheckedDrugs.Count < 1)
                //{
                //    IsLabelVisible = false;
                //    IsButtonVisible = false;
                //}

                //CountCheckedDrugs = CheckedDrugs.Count.ToString();
            }
        }
        [RelayCommand]
        private async Task SelectedTabChanged()
        {
            AllMeals = await _dataContext.LoadAsync<Meal>();
            var breakfastMeals = await _dataContext.LoadAsync<Meal>();
            BreakfastMeals = breakfastMeals.Where(x => x.MealType == "إفطار").ToList();
            var lunchMeals = await _dataContext.LoadAsync<Meal>();
            LunchMeals = lunchMeals.Where(x => x.MealType == "غداء").ToList();
            var dinnerMeals = await _dataContext.LoadAsync<Meal>();
            DinnerMeals = dinnerMeals.Where(x => x.MealType == "عشاء").ToList();
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
