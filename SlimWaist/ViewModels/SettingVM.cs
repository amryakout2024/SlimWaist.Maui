using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using SlimWaist.Models;
using SlimWaist.Views;

namespace SlimWaist.ViewModels
{
    public partial class SettingVM(DataContext dataContext, Setting setting) : BaseVM
    {
        private readonly DataContext _dataContext = dataContext;
        private readonly Setting _setting = setting;
        [ObservableProperty]
        private Membership? _memberShip;

        [ObservableProperty]
        private Membership? _memberShipFromQueryProperty;

        [ObservableProperty]
        private string _email;

        [ObservableProperty]
        private string? _name;

        [ObservableProperty]
        private string? _birthDate;

        [ObservableProperty]
        private string? _gender;

        [ObservableProperty]
        private string? _weight;

        [ObservableProperty]
        private string? _height;

        [ObservableProperty]
        private string? _bMI;

        [ObservableProperty]
        private string? _idealWeight;

        [ObservableProperty]
        private string? _modifiedWeight;

        [ObservableProperty]
        private string? _bodyActivity;

        [ObservableProperty]
        private BodyActivity? _selectedBodyActivity;

        [ObservableProperty]
        private string? _totalEnergy;

        Setting setting;

        public async Task init()
        {
            var memberShips = await _dataContext.LoadAsync<Membership>();

            var settings = await _dataContext.LoadAsync<Setting>();

            setting = settings.Where(x => x.Id == 1).FirstOrDefault();

            MemberShip = memberShips.Where(x => x.Id == setting.SavedMemberShipId).FirstOrDefault();

            Name = MemberShip?.Name ?? "";

            Weight = MemberShip?.Weight.ToString() ?? "";

            Height = MemberShip?.Height.ToString() ?? "";

            BirthDate = MemberShip?.BirthDate ?? "";

            Gender = MemberShip?.Gender ?? "";

            BMI = MemberShip?.BMI ?? "";

            IdealWeight = MemberShip?.IdealWeight ?? "";

            ModifiedWeight = MemberShip?.ModifiedWeight ?? "";

            TotalEnergy = MemberShip?.TotalEnergy ?? "";

        }

        [RelayCommand]
        private async Task GoToProfilePage()
        {
            await GoToAsyncWithStack(nameof(ProfilePage), true);
        }

        [RelayCommand]
        private async Task LogOut()
        {
            setting.SavedMemberShipId = 0;

            await _dataContext.UpdateAsync<Setting>(setting);

            await GoToAsyncWithShell(nameof(LoginPage), true);
        }

        [RelayCommand]
        private async Task ShowLanguagePopup()
        {

        }

    }
}
//[QueryProperty(nameof(Membership), nameof(Membership))]
