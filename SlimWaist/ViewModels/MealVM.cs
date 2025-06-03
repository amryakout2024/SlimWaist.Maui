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
    public partial class MealVM() : BaseVM
    {

        [ObservableProperty]
        private string _mealTypeName;


        [ObservableProperty]
        private List<BodyActivity> _bodyActivities;

        [ObservableProperty]
        private string _email;

        [ObservableProperty]
        private string _dateday;

        [ObservableProperty]
        private string _datedayname;

        [ObservableProperty]
        private string _datemonth;

        [ObservableProperty]
        private string _dateyear;

        [ObservableProperty]
        private string? _name;

        [ObservableProperty]
        private string? _birthDate;

        [ObservableProperty]
        private int _genderIndex;

        [ObservableProperty]
        private string? _weight;

        [ObservableProperty]
        private string? _height;

        [ObservableProperty]
        private string? _bMI;

        [ObservableProperty]
        private string? _idealWeight;

        [ObservableProperty]
        private string? _targetedWeight;

        [ObservableProperty]
        private string? _modifiedWeight;

        [ObservableProperty]
        private string? _waistCircumferenceMeasurement;

        [ObservableProperty]
        private string? _bodyActivity;

        [ObservableProperty]
        private string? _totalEnergy;

        [ObservableProperty]
        private bool _isBottomSheetPresented;

        [ObservableProperty]
        private bool _isTabbarVisible;

        private int ObesityDegreeId;

        [ObservableProperty]
        private string? _obesityDegreeName;

        private int WaistCircumferenceId;

        [ObservableProperty]
        private string? _waistCircumferenceName;

        [ObservableProperty]
        private List<Diet> _diets;


        public async Task init()
        {

            MealTypeName = App.mealTypes.Where(x => x.MealTypeId == HomeVM.CurrentMeal.MealTypeId).FirstOrDefault()?.MealTypeName??"";



            Diets = await App.dataContext.LoadAsync<Diet>();
            Dateday = DateTime.Now.Day.ToString();
            Datedayname = DateTime.Now.DayOfWeek.ToString();
            Datemonth = DateTime.Now.Month.ToString();
            Dateyear = DateTime.Now.Year.ToString();

            BodyActivities = App.BodyActivities;

            Name = App.currentMembership?.Name ?? "";

            Weight = App.currentMembership?.Weight.ToString() ?? "";

            Height = App.currentMembership?.Height.ToString() ?? "";

            BirthDate = App.currentMembership?.BirthDateDay.ToString() ?? "";
            
            BodyActivity = BodyActivities.Where(x=>x.BodyActivityId== App.currentMembership.BodyActivityId).FirstOrDefault().BodyActivityName;

            WaistCircumferenceMeasurement = App.currentMembership.WaistCircumferenceMeasurement.ToString();
            
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
        [RelayCommand]
        private async Task GoToDietsPage()
        {
            await GoToAsyncWithStack($"{nameof(DietsPage)}/{nameof(DietsPage)}", true);
        }

        [RelayCommand]
        private async void GoToSettingPage()
        {
            await GoToAsyncWithStack(nameof(SettingPage), true);
        }

        [RelayCommand]
        private async Task GoProfilePage()
        {
            await GoToAsyncWithStack(nameof(ProfilePage), true);
        }

    }
}
//[QueryProperty(nameof(App.currentMembership), nameof(App.currentMembership))]
