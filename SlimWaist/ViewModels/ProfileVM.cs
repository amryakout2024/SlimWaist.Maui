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
        private ObservableCollection<RegimeList> _regimeLists;

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

            Name = App.currentMembership?.Name ?? "";

            Email = App.currentMembership?.Email ?? "";

            Weight = App.currentMembership?.Weight.ToString() ?? "";

            Height = App.currentMembership?.Height.ToString() ?? "";

            BirthDate = new DateTime(App.currentMembership.BirthDateYear, App.currentMembership.BirthDateMounth, App.currentMembership.BirthDateDay);

            SelectedBodyActivity = BodyActivities.Where(x=>x.BodyActivityId==App.currentMembership.BodyActivityId).FirstOrDefault();

            IsMale = (App.currentMembership.GenderId== 1) ? true : false;

            WaistCircumferenceMeasurement=App.currentMembership.WaistCircumferenceMeasurement.ToString();
        }


        [RelayCommand]
        private async Task GoToDietsPage()
        {
            await GoToAsyncWithStack(nameof(DietsPage), true);
        }

        [RelayCommand]
        private async Task UpdateMembership()
        {
            App.currentMembership.Email = Email ?? "";

            App.currentMembership.Name = Name;

            App.currentMembership.Height = Convert.ToDouble(Height);

            App.currentMembership.Weight = Convert.ToDouble(Weight);

            App.currentMembership.BirthDateYear = BirthDate.Year;

            App.currentMembership.BirthDateMounth=BirthDate.Month;

            App.currentMembership.BirthDateDay=BirthDate.Day;

            App.currentMembership.BodyActivityId = SelectedBodyActivity.BodyActivityId;

            GenderId = (IsMale == true) ? 1 : 2;

            App.currentMembership.GenderId = GenderId;

            App.currentMembership.WaistCircumferenceMeasurement =Convert.ToDouble( WaistCircumferenceMeasurement);

            await App.dataContext.UpdateAsync<Membership>(App.currentMembership);

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
//[QueryProperty(nameof(App.currentMembership), nameof(App.currentMembership))]
