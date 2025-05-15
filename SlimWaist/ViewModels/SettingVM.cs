
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using SlimWaist.Models;
using SlimWaist.Views;

namespace SlimWaist.ViewModels
{
    public partial class SettingVM() : BaseVM
    {
        [ObservableProperty]
        private string? _name;

        [ObservableProperty]
        private bool _isCultureInfoArrowArabic;
       
        public async Task init()
        {
            Name = App.currentMembership?.Name ?? "";

            if (App.currentMembership.CultureInfo=="ar-SA")
            {
                IsCultureInfoArrowArabic = true;
            }
            else
            {
                IsCultureInfoArrowArabic = false;
            }
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

    }
}
//[QueryProperty(nameof(Membership), nameof(Membership))]
