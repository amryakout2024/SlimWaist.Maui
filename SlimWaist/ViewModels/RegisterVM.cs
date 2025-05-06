using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Maui.Core;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using SlimWaist.Models;
using SlimWaist.Views;
using System.Globalization;

namespace SlimWaist.ViewModels
{
    public partial class RegisterVM() : BaseVM
    {
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
        private int _genderId;

        [ObservableProperty]
        private int _bodyActivityId;

        [ObservableProperty]
        private BodyActivity _selectedBodyActivity;

        [ObservableProperty]
        private bool _isPassword;

        [ObservableProperty]
        private bool _isCheckBoxChecked;

        [ObservableProperty]
        private bool _isMale;

        [ObservableProperty]
        private List<BodyActivity> _bodyActivities;

        [ObservableProperty]
        private string _waistCircumferenceMeasurement ;

        public async Task init()
        {
            IsCheckBoxChecked = false;

            IsMale = true;

            IsPassword = true;

            BirthDate = DateTime.Now;

            BodyActivities=App.BodyActivities;

            SelectedBodyActivity = BodyActivities.FirstOrDefault();
        }

        [RelayCommand]
        private async Task SaveNewMemberShip()
        {
            GenderId = IsMale ? 1 : 2;

            if (!string.IsNullOrEmpty(Email) &&
                !string.IsNullOrEmpty(Password) &&
                !string.IsNullOrEmpty(Name) &&
                !string.IsNullOrEmpty(Weight) &&
                !string.IsNullOrEmpty(Height) &&
                !string.IsNullOrEmpty(BirthDate.ToString()))
            {
                var IsRegisteredEmailBefore = App.dataContext.FindEmailAsync(Email);

                if (!IsRegisteredEmailBefore.Result)
                {
                    await App.dataContext.InsertAsync(new Membership()
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
                        BodyActivityId= SelectedBodyActivity.BodyActivityId,
                        GenderId = GenderId,
                        WaistCircumferenceMeasurement=Convert.ToDouble( WaistCircumferenceMeasurement)
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
