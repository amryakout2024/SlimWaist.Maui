using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using SlimWaist.Languages;
using SlimWaist.Models;
using SlimWaist.Views;
using System.Globalization;

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

            DayDiets = _dataContext.Database.Table<DayDiet>().Where(x => x.MembershipId == HomeVM.CurrentMembership.Id).ToList();

            PreviousDayDiets = DayDiets.Where(x=> x.DayDietDate<dateToday).ToList();

            NextDayDiets=DayDiets.Where(x=>x.DayDietDate>dateToday || x.DayDietDate == dateToday).ToList();
        }


        [RelayCommand]
        private async Task GoToHomePage(DayDiet dayDiet)
        {
            //HomeVM.SelectedDate = dayDiet.DayDietDate;
            HomeVM.CurrentDayDiet = dayDiet;

            await GoToAsyncWithShell(nameof(HomePage), animate: true);

            App.IsFromTablesPage = false;

        }

        [RelayCommand]
        private async Task DeleteDayDiet(DayDiet dayDiet)
        {
            if (HomeVM.CurrentDayDiet.DayDietId==dayDiet.DayDietId)
            {
                var dt = HomeVM.CurrentDayDiet.DayDietDate;

                HomeVM.CurrentDayDiet = new DayDiet();

                HomeVM.CurrentDayDiet.DayDietDate = new DateTime(dt.Year,dt.Month,dt.Day);
            }
            await _dataContext.DeleteAsync<DayDiet>(dayDiet);

            await Init();

            await ShowToastAsync(AppResource.ResourceManager.GetString("Deletedsuccessfully", CultureInfo.CurrentCulture) ?? "");
        }

        //[RelayCommand]
        //private async Task SelectedTabChanged()
        //{
        //    //AllMeals = await App._dataContext.LoadAsync<Meal>();
        //    //BreakfastMeals = AllMeals.Where(x => x.MealTypeId == 0).ToList();
        //    //LunchMeals = AllMeals.Where(x => x.MealTypeId == 1).ToList();
        //    //DinnerMeals = AllMeals.Where(x => x.MealTypeId == 2).ToList();
        //}

    }
}
