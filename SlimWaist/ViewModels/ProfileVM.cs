using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Maui.Core;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using SlimWaist.Languages;
using SlimWaist.Models;
using SlimWaist.Views;
using System.Collections.ObjectModel;
using System.Globalization;

namespace SlimWaist.ViewModels
{
    public partial class ProfileVM() : BaseVM
    {        
        [ObservableProperty]
        private string _email;

        [ObservableProperty]
        private string? _name;

        [ObservableProperty]
        private DateTime _birthDate;

        [ObservableProperty]
        private string? _gender;

        [ObservableProperty]
        private bool _isMale;

        [ObservableProperty]
        private string? _weight;

        [ObservableProperty]
        private string? _height;

        [ObservableProperty]
        private int _genderId;

        [ObservableProperty]
        private int _bodyActivityId;

        [ObservableProperty]
        private BodyActivity _selectedBodyActivity;

        [ObservableProperty]
        private string? _totalEnergy;

        [ObservableProperty]
        private List<BodyActivity> _bodyActivities;

        [ObservableProperty]
        private bool _isShowChangePasswordGrid = false;

        Setting setting;

        [ObservableProperty]
        private string _waistCircumferenceMeasurement;

        public async Task init()
        {
            //Preferences.Set("Email", "");

            setting = App.dataContext.Database.Table<Setting>().FirstOrDefault();

            BodyActivities = App.BodyActivities;

            Name = HomeVM.CurrentMembership?.Name ?? "";

            Email = HomeVM.CurrentMembership?.Email ?? "";

            Weight = HomeVM.CurrentMembership?.Weight.ToString() ?? "";

            Height = HomeVM.CurrentMembership?.Height.ToString() ?? "";

            BirthDate = new DateTime(HomeVM.CurrentMembership.BirthDateYear, HomeVM.CurrentMembership.BirthDateMonth, HomeVM.CurrentMembership.BirthDateDay);

            SelectedBodyActivity = BodyActivities.Where(x=>x.BodyActivityId==HomeVM.CurrentMembership.BodyActivityId).FirstOrDefault();

            IsMale = (HomeVM.CurrentMembership.GenderId== 1) ? true : false;

            WaistCircumferenceMeasurement=HomeVM.CurrentMembership.WaistCircumferenceMeasurement.ToString();
        }


        [RelayCommand]
        private async Task GoToDietsPage()
        {
            await GoToAsyncWithStack(nameof(DietsPage), true);
        }

        [RelayCommand]
        private async Task UpdateMembership()
        {
            HomeVM.CurrentMembership.Email = Email ?? "";

            HomeVM.CurrentMembership.Name = Name;

            HomeVM.CurrentMembership.Height = Convert.ToDouble(Height);

            HomeVM.CurrentMembership.Weight = Convert.ToDouble(Weight);

            HomeVM.CurrentMembership.BirthDateYear = BirthDate.Year;

            HomeVM.CurrentMembership.BirthDateMonth=BirthDate.Month;

            HomeVM.CurrentMembership.BirthDateDay=BirthDate.Day;

            HomeVM.CurrentMembership.BodyActivityId = SelectedBodyActivity.BodyActivityId;

            GenderId = (IsMale == true) ? 1 : 2;

            HomeVM.CurrentMembership.GenderId = GenderId;

            HomeVM.CurrentMembership.WaistCircumferenceMeasurement =Convert.ToDouble( WaistCircumferenceMeasurement);

            await App.dataContext.UpdateAsync<Membership>(HomeVM.CurrentMembership);

            await Toast.Make(AppResource.ResourceManager.GetString("Updatedsuccessfully",CultureInfo.CurrentCulture), ToastDuration.Short).Show();
        }

        [RelayCommand]
        private async void LogOut()
        {
            App.setting.SavedMembershipId = 0;

            await App.dataContext.UpdateAsync<Setting>(App.setting);

            await GoToAsyncWithShell(nameof(LoginPage), true);
        }

        [RelayCommand]
        private async Task GoBack()
        {

        }

    }
}
//[QueryProperty(nameof(HomeVM.CurrentMembership), nameof(HomeVM.CurrentMembership))]
