using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using SlimWaist.Models;
using SlimWaist.Views;

namespace SlimWaist.ViewModels
{
    public partial class LoginVM() : BaseVM
    {
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

            Password = "1";

            IsCheckBoxChecked = false;

            IsPassword = true;

            App.memberships = await App.dataContext.LoadAsync<Membership>();

            CheckLoginSaved();

        }

        private async void CheckLoginSaved()
        {
            //Preferences.Set("Email", "");
         
            //if (App.setting.SavedMembershipId != 0)
            //{
            //    App.currentMembership = App.memberships.Where(x => x.Id == App.setting.SavedMembershipId).FirstOrDefault();

            //    await GoToAsyncWithShell(nameof(HomePage), true);
            //}
        }

        [RelayCommand]
        private async Task RegisterNewMemberShip()
        {
            await GoToAsyncWithStack(nameof(RegisterPage), animate: true);
        }

        [RelayCommand]
        private async Task Login()
        {
            var IsRegisteredEmailBefore = await App.dataContext.FindEmailAsync(Email);

            if (IsRegisteredEmailBefore)
            {
                var IsPassWordMatchWithEmail = await App.dataContext.MatchEmailWithPassWordAsync(Email, Password);

                if (IsPassWordMatchWithEmail)
                {
                    App.currentMembership = App.memberships.Where(x => x.Email == Email).FirstOrDefault();

                    App.setting.CultureInfo= App.currentMembership.CultureInfo;

                    if (IsCheckBoxChecked)
                    {
                        App.setting.SavedMembershipId = App.currentMembership.Id;
                    }

                    await App.dataContext.UpdateAsync<Setting>(App.setting);

                    await App.dataContext.UpdateAsync<Membership>(App.currentMembership);
                    
                    await GoToAsyncWithShell(nameof(HomePage), true);
                }
                else
                {
                    await Shell.Current.DisplayAlert("خطأ", "كلمة السر غير صحيحة", "Ok");
                }

            }
            else
            {
                await Shell.Current.DisplayAlert("خطأ", "الايميل غير مسجل", "Ok");
            }

        }

    }
}

//await Shell.Current.Navigation.PushAsync(new HomePage(_regimeVM),animated:true);
//Dictionary<string, object> parameter = new Dictionary<string, object>
//{
//    [nameof(HomeVM.Membership)] = Membership
//};


//await Shell.Current.GoToAsync($"//{nameof(HomePage)}", animate: true, parameter);

