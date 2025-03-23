using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using SlimWaist.Models;
using SlimWaist.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Maui.Storage;
using Microsoft.Maui.ApplicationModel.Communication;

namespace SlimWaist.ViewModels
{
    public partial class LoginVM(DataContext dataContext) : BaseVM
    {
        [ObservableProperty]
        private string _email;

        [ObservableProperty]
        private string _password;

        [ObservableProperty]
        private bool _isPassword;

        [ObservableProperty]
        private bool _isCheckBoxChecked;

        private readonly DataContext _dataContext = dataContext;

        public async Task init()
        {
            Email = "amrnewstory@gmail.com";
            Password = "1";

            IsCheckBoxChecked = false;

            IsPassword = true;

            CheckLoginSaved();

        }

        private async void CheckLoginSaved()
        {
            //Preferences.Set("Email", "");

            //File.Delete(DataContext.DbPath);

            var Email = Preferences.Get("Email", "");

            if (!string.IsNullOrEmpty(Email))
            {

                await GoToAsyncWithShell(nameof(HomePage),true);
            }
        }

        [RelayCommand]
        private async Task RegisterNewMemberShip()
        {
            await GoToAsyncWithStack(nameof(RegisterPage), animate: true);
            //await GoToAsyncWithStack(nameof(SignUpPage), animate: true);
        }

        [RelayCommand]
        private async Task Login()
        {
            var IsRegisteredEmailBefore = _dataContext.FindEmailAsync(Email);

            if (IsRegisteredEmailBefore.Result)
            {
                var IsPassWordMatchWithEmail = _dataContext.MatchEmailWithPassWordAsync(Email, Password);

                if (IsPassWordMatchWithEmail.Result)
                {
                    //await Shell.Current.GoToAsync(nameof(HomePage), animate: true);

                    if (IsCheckBoxChecked)
                    {
                        await _dataContext.InsertAsync<Setting>(new Setting()
                        {
                            IsLoginSaved = true,

                            Email = Email,

                            Password = Password
                        });
                    }

                    Preferences.Set("Email", Email);

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

