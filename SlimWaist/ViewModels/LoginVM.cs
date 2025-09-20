using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Maui.Core;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Firebase.Auth;
using SlimWaist.Languages;
using SlimWaist.Models;
using SlimWaist.Views;
using System.Globalization;

namespace SlimWaist.ViewModels
{
    public partial class LoginVM(DataContext dataContext,FirebaseAuthClient firebaseAuthClient) : BaseVM
    {
        private readonly DataContext _dataContext = dataContext;
        private readonly FirebaseAuthClient _firebaseAuthClient = firebaseAuthClient;
        [ObservableProperty]
        private string _email;

        [ObservableProperty]
        private string _password;

        [ObservableProperty]
        private bool _isPassword;

        [ObservableProperty]
        private bool _isCheckBoxChecked;

        public async Task init()
        {
            Email = "amrnewstory@gmail.com";

            Password = "123456";

            IsCheckBoxChecked = false;

            IsPassword = true;
        }

        [RelayCommand]
        private async Task Login()
        {
            if (Connectivity.Current.NetworkAccess == NetworkAccess.Internet)
            {
                try
                {
                    await _firebaseAuthClient.SignInWithEmailAndPasswordAsync(Email, Password);

                    await Toast.Make("success", ToastDuration.Short).Show();

                }
                catch (FirebaseAuthException ex)
                {
                    await Shell.Current.DisplayAlert("", ex.Message, "ok");
                    //await Shell.Current.DisplayAlert(
                    //    AppResource.ResourceManager.GetString("Error", CultureInfo.CurrentCulture) ?? ""
                    //    , AppResource.ResourceManager.GetString("Emailorpasswordisnotcorrect", CultureInfo.CurrentCulture) ?? ""
                    //    , AppResource.ResourceManager.GetString("Ok", CultureInfo.CurrentCulture) ?? "");
                }

            }
            else
            {
                await Toast.Make("Please check your internet connection", ToastDuration.Short).Show();
            }


            //var memberships = _dataContext.Database.Table<Membership>().ToList();
            //Membership membership = _dataContext.Database.Table<Membership>().Where(x => x.Email == Email&&x.Password==Password).FirstOrDefault()??new Membership();

            //if (membership.IsExistsInDb)
            //{
            //    HomeVM.CurrentMembership = membership;

            //    App.setting.CultureInfo = HomeVM.CurrentMembership.CultureInfo;

            //    if (IsCheckBoxChecked)
            //    {
            //        App.setting.SavedMembershipId = membership.Id;
            //    }

            //    await _dataContext.UpdateAsync<Setting>(App.setting);

            //    await GoToAsyncWithShell(nameof(HomePage), true);
            //}
            //else
            //{
            //    await Shell.Current.DisplayAlert(
            //        AppResource.ResourceManager.GetString("Error", CultureInfo.CurrentCulture) ?? ""
            //        , AppResource.ResourceManager.GetString("Emailorpasswordisnotcorrect", CultureInfo.CurrentCulture) ?? ""
            //        , AppResource.ResourceManager.GetString("Ok", CultureInfo.CurrentCulture) ?? "");
            //}
        }

        [RelayCommand]
        private async Task RegisterNewMemberShip()
        {
            await GoToAsyncWithStack(nameof(RegisterPage), animate: true);
        }
    }
}

