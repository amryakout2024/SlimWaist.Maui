using CommunityToolkit.Mvvm.Input;
using SlimWaist.Views;

namespace SlimWaist.ViewModels
{
    public partial class AppShellVM : BaseVM
    {
        [RelayCommand]
        private async void GoBack()
        {
            await Shell.Current.DisplayAlert("خروج", "هل تريد الخروج من البرنامج ?", "Ok");
        }

        [RelayCommand]
        private async Task GoToHomePage()
        {
            await GoToAsyncWithStack(nameof(HomePage), true);
        }

    }
}
