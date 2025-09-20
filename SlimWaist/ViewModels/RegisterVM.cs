using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Maui.Core;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Firebase.Auth;
using SlimWaist.Languages;
using SlimWaist.Models;
using SlimWaist.Models.Dto;
using SlimWaist.Views;
using System.Globalization;

namespace SlimWaist.ViewModels
{
    public partial class RegisterVM(DataContext dataContext,FirebaseAuthClient firebaseAuthClient) : BaseVM
    {
        private readonly DataContext _dataContext = dataContext;
        private readonly FirebaseAuthClient _firebaseAuthClient = firebaseAuthClient;

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
        private bool _isMale;

        [ObservableProperty]
        private List<BodyActivity> _bodyActivities;

        [ObservableProperty]
        private string _waistCircumferenceMeasurement ;

        public async Task init()
        {
            Email = "amrnewstory@gmail.com";

            Password = "123456";

            IsMale = true;

            BirthDate = DateTime.Now;

            BodyActivities=App.BodyActivities;

            SelectedBodyActivity = BodyActivities.FirstOrDefault();
        }

        [RelayCommand]
        private async Task SaveNewMemberShip()
        {
            if (Connectivity.Current.NetworkAccess == NetworkAccess.Internet)
            {
                try
                {
                    await _firebaseAuthClient.CreateUserWithEmailAndPasswordAsync(Email, Password);
                    await Toast.Make("success", ToastDuration.Short).Show();

                }
                catch (FirebaseAuthException ex)
                {
                    await Shell.Current.DisplayAlert(
                        AppResource.ResourceManager.GetString("Error", CultureInfo.CurrentCulture) ?? ""
                        , AppResource.ResourceManager.GetString("Emailexistsbefore", CultureInfo.CurrentCulture) ?? ""
                        , AppResource.ResourceManager.GetString("Ok", CultureInfo.CurrentCulture) ?? "");
                }

            }
            else
            {
                await Toast.Make("Please check your internet connection", ToastDuration.Short).Show();
            }



            //GenderId = IsMale ? 1 : 2;

            //var IsRegisteredEmailBefore = await _dataContext.FindEmailAsync(Email);

            //if (!IsRegisteredEmailBefore)
            //{
            //    await _dataContext.InsertAsync(new Membership()
            //    {
            //        Email = Email,
            //        Password = Password,
            //        Name = Name,
            //        Weight = Convert.ToDouble(Weight),
            //        WeightDate = DateTime.Now,
            //        Height = Convert.ToDouble(Height),
            //        BirthDate = new DateTime(BirthDate.Year,BirthDate.Month,BirthDate.Day),
            //        BodyActivityId = SelectedBodyActivity.BodyActivityId,
            //        GenderId = GenderId,
            //        WaistCircumferenceMeasurement = Convert.ToDouble(WaistCircumferenceMeasurement),IsExistsInDb=true,CultureInfo=App.setting.CultureInfo
            //    });

            //    CancellationTokenSource cancellationTokenSource = new CancellationTokenSource();

            //    await Toast.Make(AppResource.ResourceManager.GetString("Membershipregisteredsuccessfully"), ToastDuration.Short).Show(cancellationTokenSource.Token);

            //    await GoToAsyncWithShell(nameof(LoginPage), true);

            //}
            //else
            //{
            //    await Shell.Current.DisplayAlert(AppResource.ResourceManager.GetString("Error", CultureInfo.CurrentCulture) ?? "", AppResource.ResourceManager.GetString("Emailexistsbefore", CultureInfo.CurrentCulture) ?? "", AppResource.ResourceManager.GetString("Ok", CultureInfo.CurrentCulture) ?? "");
            //}

        }
    }
}
