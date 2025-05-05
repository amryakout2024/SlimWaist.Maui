using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Maui.Core;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using SlimWaist.Models;
using SlimWaist.Views;
using System.Collections.ObjectModel;

namespace SlimWaist.ViewModels
{
    public partial class ProfileVM(DataContext dataContext) : BaseVM
    {
        private readonly DataContext _dataContext = dataContext;
        
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
        private int _genderIndex;

        [ObservableProperty]
        private int _bodyActivityIndex;

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
        private double _waistCircumferenceMeasurement;

        public async Task init()
        {
            //Preferences.Set("Email", "");

            //File.Delete(DataContext.DbPath);

            setting = _dataContext.Database.Table<Setting>().FirstOrDefault();

            BodyActivities = App.BodyActivities;

            memberships = await _dataContext.LoadAsync<Membership>();

            MemberShip = memberships.Where(x => x.Id == App.setting.CurrentMemberShipId).FirstOrDefault();

            Name = MemberShip?.Name ?? "";

            Email = MemberShip?.Email ?? "";

            Weight = MemberShip?.Weight.ToString() ?? "";

            Height = MemberShip?.Height.ToString() ?? "";

            BirthDate = new DateTime(MemberShip.BirthDateYear, MemberShip.BirthDateMounth, MemberShip.BirthDateDay);

            BodyActivityIndex = MemberShip.BodyActivityIndex;

            IsMale = (MemberShip.GenderIndex== 0) ? true : false;

            WaistCircumferenceMeasurement=MemberShip.WaistCircumferenceMeasurement;
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

            MemberShip.BodyActivityIndex = BodyActivityIndex;

            GenderIndex = (IsMale == true) ? 0 : 1;

            MemberShip.GenderIndex = GenderIndex;

            MemberShip.WaistCircumferenceMeasurement = WaistCircumferenceMeasurement;

            await _dataContext.UpdateAsync<Membership>(MemberShip);

            await Toast.Make("تم التحديث", ToastDuration.Short).Show();
        }

        [RelayCommand]
        private async void LogOut()
        {
            setting.SavedMemberShipId = 0;

            await _dataContext.UpdateAsync<Setting>(setting);

            await GoToAsyncWithShell(nameof(LoginPage), true);
        }

        [RelayCommand]
        private async Task GoBack()
        {

        }

    }
}
//[QueryProperty(nameof(Membership), nameof(Membership))]
