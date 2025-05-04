using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Maui.Core;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using SlimWaist.Models;
using SlimWaist.Views;
using System.Globalization;

namespace SlimWaist.ViewModels
{
    public partial class RegisterVM(DataContext dataContext) : BaseVM
    {
        private readonly DataContext _dataContext = dataContext;

        [ObservableProperty]
        private string? _email;

        [ObservableProperty]
        private string? _password;

        [ObservableProperty]
        private string? _name;

        [ObservableProperty]
        private string? _weight;

        [ObservableProperty]
        private string? _height;

        [ObservableProperty]
        private DateTime _birthDate;

        [ObservableProperty]
        private int _genderIndex;

        [ObservableProperty]
        private int _bodyActivityIndex;

        [ObservableProperty]
        private bool _isPassword;

        [ObservableProperty]
        private bool _isCheckBoxChecked;

        [ObservableProperty]
        private bool _isMale;

        [ObservableProperty]
        private List<BodyActivity> _bodyActivities;


        public async Task init()
        {
            IsCheckBoxChecked = false;

            IsMale = true;

            IsPassword = true;

            BirthDate = DateTime.Now;

            BodyActivities=App.BodyActivities;

            BodyActivityIndex = 0;
        }

        [RelayCommand]
        private async Task SaveNewMemberShip()
        {
            GenderIndex = IsMale ? 0 : 1;

            if (!string.IsNullOrEmpty(Email) &&
                !string.IsNullOrEmpty(Password) &&
                !string.IsNullOrEmpty(Name) &&
                !string.IsNullOrEmpty(Weight) &&
                !string.IsNullOrEmpty(Height) &&
                !string.IsNullOrEmpty(BirthDate.ToString()))
            {
                var IsRegisteredEmailBefore = _dataContext.FindEmailAsync(Email);

                if (!IsRegisteredEmailBefore.Result)
                {
                    await _dataContext.InsertAsync(new Membership()
                    {
                        Email = Email,
                        Password = Password,
                        Name = Name,
                        Weight = Convert.ToDouble(Weight),
                        WeightDateDay=DateTime.Now.Day,
                        WeightDateMounth=DateTime.Now.Month,
                        WeightDateYear=DateTime.Now.Year,
                        Height = Convert.ToDouble(Height),                        
                        BirthDateDay=BirthDate.Day,
                        BirthDateMounth=BirthDate.Month,
                        BirthDateYear=BirthDate.Year,
                        BodyActivityIndex= BodyActivityIndex,
                        GenderIndex = GenderIndex,
                    });

                    CancellationTokenSource cancellationTokenSource = new CancellationTokenSource();

                    await Toast.Make("تم تسجيل العضوية بنجاح", ToastDuration.Short).Show(cancellationTokenSource.Token);

                    await GoToAsyncWithShell(nameof(LoginPage), true);

                }
                else
                {
                    await Shell.Current.DisplayAlert("خطأ", "الايميل مسجل مسبقا", "Ok");
                }
            }
        }
    }
}
