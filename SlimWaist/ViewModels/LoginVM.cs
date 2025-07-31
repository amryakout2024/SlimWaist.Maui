using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using SlimWaist.Models;
using SlimWaist.Views;

namespace SlimWaist.ViewModels
{
    public partial class LoginVM(DataContext dataContext) : BaseVM
    {
        private readonly DataContext _dataContext = dataContext;
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

            //Password = "1";

            IsCheckBoxChecked = false;

            IsPassword = true;
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
                    //HomeVM.CurrentMembership = App.memberships.Where(x => x.Email == Email).FirstOrDefault();

                    App.setting.CultureInfo = HomeVM.CurrentMembership.CultureInfo;

                    if (IsCheckBoxChecked)
                    {
                        App.setting.SavedMembershipId = HomeVM.CurrentMembership.Id;
                    }

                    await _dataContext.UpdateAsync<Setting>(App.setting);

                    await _dataContext.UpdateAsync<Membership>(HomeVM.CurrentMembership);

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

        [RelayCommand]
        private async Task RegisterNewMemberShip()
        {
            await GoToAsyncWithStack(nameof(RegisterPage), animate: true);
        }
    }
}

