using CommunityToolkit.Mvvm.Input;
using SlimWaist.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SlimWaist.ViewModels
{
    public partial class AppShellVM:BaseVM
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
