using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using SlimWaist.Models;
using SlimWaist.Views;

namespace SlimWaist.ViewModels
{
    public partial class TablesVM(DataContext dataContext) : BaseVM
    {
        private readonly DataContext _dataContext = dataContext;

        [ObservableProperty]
        private List<DayDiet> _dayDiets = new List<DayDiet>();

        [ObservableProperty]
        private List<DayDiet> _previousDayDiets;

        [ObservableProperty]
        private List<DayDiet> _NextDayDiets;

        //private List<Meal> _SelectedMeals;

        [ObservableProperty]
        private int _selectedTab = 1;

        public async Task Init()
        {
            var dateToday = new DateTime(
                Convert.ToInt32(DateTime.Now.Year)
                , Convert.ToInt32(DateTime.Now.Month)
                , Convert.ToInt32(DateTime.Now.Day));

            DayDiets = _dataContext.Database.Table<DayDiet>().Where(x => x.MembershipId == App.currentMembership.Id).ToList();

            PreviousDayDiets = DayDiets.Where(x =>x.DayDietDate==dateToday || x.DayDietDate<dateToday).ToList();

            NextDayDiets=DayDiets.Where(x=>x.DayDietDate>dateToday).ToList();
        }

        //[RelayCommand]
        //private async Task UpdateSelectedMeal(Meal meal)
        //{
        //    //if (meal.IsSelected == true)
        //    //{
        //    //    meal.IsSelected = false;
        //    //}
        //    //else
        //    //{
        //    //    meal.IsSelected = true;
        //    //}
        //    await App.dataContext.UpdateMeal(meal);

        //    //if (meal.IsSelected == true)
        //    //{

        //    //    SelectedMeals.Add(meal);
        //    //    //if (CheckedDrugs.Count > 0)
        //    //    //{
        //    //    //    IsLabelVisible = true;
        //    //    //    IsButtonVisible = true;
        //    //    //}
        //    //    //CountCheckedDrugs = CheckedDrugs.Count.ToString();
        //    //}
        //    //else
        //    //{
        //    //    SelectedMeals.Remove(meal);
        //    //    //if (CheckedDrugs.Count < 1)
        //    //    //{
        //    //    //    IsLabelVisible = false;
        //    //    //    IsButtonVisible = false;
        //    //    //}

        //    //    //CountCheckedDrugs = CheckedDrugs.Count.ToString();
        //    //}
        //}
        
        //[RelayCommand]
        //private async Task SelectedTabChanged()
        //{
        //    //AllMeals = await App.dataContext.LoadAsync<Meal>();
        //    //BreakfastMeals = AllMeals.Where(x => x.MealTypeId == 0).ToList();
        //    //LunchMeals = AllMeals.Where(x => x.MealTypeId == 1).ToList();
        //    //DinnerMeals = AllMeals.Where(x => x.MealTypeId == 2).ToList();
        //}

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
