using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Maui.Core;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Firebase.Auth;
using Firebase.Database;
using SlimWaist.Helpers;
using SlimWaist.Languages;
using SlimWaist.Models;
using SlimWaist.Models.FirebaseDto;
using SlimWaist.Views;
using System.Globalization;
using System.Xml.Linq;

namespace SlimWaist.ViewModels
{
    public partial class LoginVM(DataContext dataContext) : BaseVM
    {
        private readonly DataContext _dataContext = dataContext;
        private FirebaseAuthHelper firebaseAuthHelper=new FirebaseAuthHelper();
        private FirebaseDbHelper firebaseDbHelper=new FirebaseDbHelper();

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
            //Email = "amrnewstory@gmail.com";

            //Password = "123456";

            IsCheckBoxChecked = false;

            IsPassword = true;
        }

        [RelayCommand]
        private async Task Login()
        {
            try
            {
                if (Connectivity.Current.NetworkAccess == NetworkAccess.Internet)
                {

                }
                else
                {
                    await Toast.Make("Please check your internet connection", ToastDuration.Short).Show();
                }

                Membership membership = _dataContext.Database.Table<Membership>().Where(x => x.Email == Email && x.Password == Password).FirstOrDefault() ?? new Membership();

                if (membership.IsExistsInDb)
                {
                    HomeVM.CurrentMembership = membership;

                    App.setting.CultureInfo = HomeVM.CurrentMembership.CultureInfo;


                    await _dataContext.UpdateAsync<Setting>(App.setting);

                    if (IsCheckBoxChecked)
                    {
                        Preferences.Set("Email", Email);

                        App.setting.SavedMembershipId = membership.Id;
                    }

                    await GoToAsyncWithShell(nameof(HomePage), true);

                }
                else
                {

                    var isAuthenticatedSuccess = await firebaseAuthHelper.SignInWithEmail(Email, Password);

                    if (isAuthenticatedSuccess)
                    {
                        var user = (await firebaseDbHelper.GetUsersAsync()).Where(x => x.Email == Email).FirstOrDefault();

                        var membershipFromFirebase = (await firebaseDbHelper.GetAsync<Membership>(user.UserKey)).FirstOrDefault();

                        await _dataContext.InsertAsync<Membership>(membershipFromFirebase);

                        HomeVM.CurrentMembership = membershipFromFirebase;

                        if (IsCheckBoxChecked)
                        {
                            Preferences.Set("Email", Email);

                            App.setting.SavedMembershipId = membership.Id;
                        }

                        await GoToAsyncWithShell(nameof(HomePage), true);
                    }
                    else
                    {
                        await Shell.Current.DisplayAlert(
                        AppResource.ResourceManager.GetString("Error", CultureInfo.CurrentCulture) ?? ""
                        , AppResource.ResourceManager.GetString("Emailorpasswordisnotcorrect", CultureInfo.CurrentCulture) ?? ""
                        , AppResource.ResourceManager.GetString("Ok", CultureInfo.CurrentCulture) ?? "");

                    }

                }


            }
            catch (Exception ex)
            {
                await Shell.Current.DisplayAlert(null,ex.Message, AppResource.ResourceManager.GetString("Ok", CultureInfo.CurrentCulture) ?? "");
            }
        }

        [RelayCommand]
        private async Task RegisterNewMemberShip()
        {
            await GoToAsyncWithStack(nameof(RegisterPage), animate: true);
        }
    }
}

