using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using SlimWaist.Models;
using SlimWaist.Views;

namespace SlimWaist.ViewModels
{
    public partial class SettingVM() : BaseVM
    {

                
        [ObservableProperty]
        private Membership? _memberShip;

        [ObservableProperty]
        private string? _name;
       
        public async Task init()
        {
            var memberShips = await App.dataContext.LoadAsync<Membership>();

            MemberShip = memberShips.Where(x => x.Id ==App.setting.SavedMembershipId).FirstOrDefault();

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
            App.setting.SavedMembershipId = 0;

            await App.dataContext.UpdateAsync<Setting>(App.setting);

            await GoToAsyncWithShell(nameof(LoginPage), true);
        }

        [RelayCommand]
        private async Task ShowLanguagePopup()
        {

        }

    }
}
//[QueryProperty(nameof(Membership), nameof(Membership))]
