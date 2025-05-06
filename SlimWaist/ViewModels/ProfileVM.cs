using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Maui.Core;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using SlimWaist.Models;
using SlimWaist.Views;
using System.Collections.ObjectModel;

namespace SlimWaist.ViewModels
{
    public partial class ProfileVM() : BaseVM
    {        
        [ObservableProperty]
        private Membership? _memberShip;

        [ObservableProperty]
        private Membership? _memberShipFromQueryProperty;

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

        List<Membership> memberships;

        Setting setting;

        [ObservableProperty]
        private string _waistCircumferenceMeasurement;

        public async Task init()
        {
            //Preferences.Set("Email", "");

            //File.Delete(dataContext.DbPath);

            setting = App.dataContext.Database.Table<Setting>().FirstOrDefault();

            BodyActivities = App.BodyActivities;

            memberships = await App.dataContext.LoadAsync<Membership>();

            MemberShip = memberships.Where(x => x.Id == App.setting.SavedMembershipId).FirstOrDefault();

            Name = MemberShip?.Name ?? "";

            Email = MemberShip?.Email ?? "";

            Weight = MemberShip?.Weight.ToString() ?? "";

            Height = MemberShip?.Height.ToString() ?? "";

            BirthDate = new DateTime(MemberShip.BirthDateYear, MemberShip.BirthDateMounth, MemberShip.BirthDateDay);

            SelectedBodyActivity = BodyActivities.Where(x=>x.BodyActivityId==MemberShip.BodyActivityId).FirstOrDefault();

            IsMale = (MemberShip.GenderId== 1) ? true : false;

            WaistCircumferenceMeasurement=MemberShip.WaistCircumferenceMeasurement.ToString();
        }


        [RelayCommand]
        private async Task GoToDietsPage()
        {
            await GoToAsyncWithStack(nameof(DietsPage), true);
        }

        [RelayCommand]
        private async Task UpdateMemberShip()
        {
            MemberShip.Email = Email ?? "";

            MemberShip.Name = Name;

            MemberShip.Height = Convert.ToDouble(Height);

            MemberShip.Weight = Convert.ToDouble(Weight);

            MemberShip.BirthDateYear = BirthDate.Year;

            MemberShip.BirthDateMounth=BirthDate.Month;

            MemberShip.BirthDateDay=BirthDate.Day;

            MemberShip.BodyActivityId = SelectedBodyActivity.BodyActivityId;

            GenderId = (IsMale == true) ? 1 : 2;

            MemberShip.GenderId = GenderId;

            MemberShip.WaistCircumferenceMeasurement =Convert.ToDouble( WaistCircumferenceMeasurement);

            await App.dataContext.UpdateAsync<Membership>(MemberShip);

            await Toast.Make("تم التحديث", ToastDuration.Short).Show();
        }

        [RelayCommand]
        private async void LogOut()
        {
            setting.SavedMembershipId = 0;

            await App.dataContext.UpdateAsync<Setting>(setting);

            await GoToAsyncWithShell(nameof(LoginPage), true);
        }

        [RelayCommand]
        private async Task GoBack()
        {

        }

    }
}
//[QueryProperty(nameof(Membership), nameof(Membership))]
