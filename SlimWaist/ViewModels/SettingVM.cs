using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using SlimWaist.Models;
using SlimWaist.Views;

namespace SlimWaist.ViewModels
{
    public partial class SettingVM(DataContext dataContext) : BaseVM
    {
        private readonly DataContext _dataContext = dataContext;
                
        [ObservableProperty]
        private Membership? _memberShip;

        [ObservableProperty]
        private string? _name;
       
        public async Task init()
        {
            var memberShips = await _dataContext.LoadAsync<Membership>();

            MemberShip = memberShips.Where(x => x.Id ==App.setting.SavedMemberShipId).FirstOrDefault();

            Name = MemberShip?.Name ?? "";
        }

        [RelayCommand]
        private async Task GoToProfilePage()
        {
            await GoToAsyncWithStack(nameof(ProfilePage), true);
        }

        [RelayCommand]
        private async Task LogOut()
        {
            App.setting.SavedMemberShipId = 0;

            await _dataContext.UpdateAsync<Setting>(App.setting);

            await GoToAsyncWithShell(nameof(LoginPage), true);
        }

        [RelayCommand]
        private async Task ShowLanguagePopup()
        {

        }

    }
}
//[QueryProperty(nameof(Membership), nameof(Membership))]
