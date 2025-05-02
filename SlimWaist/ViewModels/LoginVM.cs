using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using SlimWaist.Models;
using SlimWaist.Views;

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

        Setting setting;

        public async Task init()
        {
            //Email = "amrnewstory@gmail.com";

            //Password = "1";

            IsCheckBoxChecked = false;

            IsPassword = true;

            CheckLoginSaved();

        }

        private async void CheckLoginSaved()
        {
            //Preferences.Set("Email", "");

            var settings = await _dataContext.LoadAsync<Setting>();

            setting = settings.Where(x => x.Id == 1).FirstOrDefault();

            if (setting?.SavedMemberShipId != 0)
            {
                await GoToAsyncWithShell(nameof(HomePage), true);
            }
        }

        [RelayCommand]
        private async Task RegisterNewMemberShip()
        {
            await GoToAsyncWithStack(nameof(RegisterPage), animate: true);
        }

        [RelayCommand]
        private async Task Login()
        {
            var IsRegisteredEmailBefore = await _dataContext.FindEmailAsync(Email);

            if (IsRegisteredEmailBefore)
            {
                var IsPassWordMatchWithEmail = await _dataContext.MatchEmailWithPassWordAsync(Email, Password);

                if (IsPassWordMatchWithEmail)
                {
                    var memberShips = await _dataContext.LoadAsync<Membership>();

                    if (IsCheckBoxChecked)
                    {
                        setting.SavedMemberShipId = memberShips.Where(x => x.Email == Email).Select(x => x.Id).FirstOrDefault();

                        setting.CurrentMemberShipId = memberShips.Where(x => x.Email == Email).Select(x => x.Id).FirstOrDefault();

                        await _dataContext.UpdateAsync<Setting>(setting);
                    }
                    else
                    {
                        setting.CurrentMemberShipId = memberShips.Where(x => x.Email == Email).Select(x => x.Id).FirstOrDefault();

                        await _dataContext.UpdateAsync<Setting>(setting);
                    }
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

