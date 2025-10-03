using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Maui.Core;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Firebase.Auth;
using Firebase.Database;
using Firebase.Database.Query;
using SlimWaist.Helpers;
using SlimWaist.Languages;
using SlimWaist.Models;
using SlimWaist.Models.Dto;
using SlimWaist.Views;
using System.Globalization;

namespace SlimWaist.ViewModels
{
    public partial class RegisterVM(DataContext dataContext) : BaseVM
    {
        private readonly DataContext _dataContext = dataContext;
        private FirebaseAuthHelper firebaseAuthHelper = new FirebaseAuthHelper();
        private FirebaseDbHelper firebaseDbHelper = new FirebaseDbHelper();

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
            //Email = "amrnewstory@gmail.com";
            //Password = "123456";
            //Weight = "10";
            //Height = "100";
            //Name = "ss";
            //WaistCircumferenceMeasurement = "50";

            IsMale = true;

            BirthDate = DateTime.Now;

            BodyActivities=App.BodyActivities;

            SelectedBodyActivity = BodyActivities.FirstOrDefault();
        }

        [RelayCommand]
        private async Task SaveNewMemberShip()
        {
            //------need to handle weak password
            try
            {
                if (Connectivity.Current.NetworkAccess == NetworkAccess.Internet)
                {

                }
                else
                {
                    await Toast.Make("Please check your internet connection", ToastDuration.Short).Show();
                }

                var isRegisteredSuccessfully = await firebaseAuthHelper.SignUpWithEmail(Email, Password);

                if (isRegisteredSuccessfully)
                {
                    Models.User user = new Models.User()
                    {
                        UserKey = "0",
                        Email = Email
                    };

                    GenderId = IsMale ? 1 : 2;

                    Membership membership = new Membership()
                    {
                        Email = Email,
                        Password = Password,
                        Name = Name,
                        Weight = Convert.ToDouble(Weight),
                        WeightDate = DateTime.Now,
                        Height = Convert.ToDouble(Height),
                        BirthDate = new DateTime(BirthDate.Year, BirthDate.Month, BirthDate.Day),
                        BodyActivityId = SelectedBodyActivity.BodyActivityId,
                        GenderId = GenderId,
                        WaistCircumferenceMeasurement = Convert.ToDouble(WaistCircumferenceMeasurement),
                        IsExistsInDb = true,
                        CultureInfo = App.setting.CultureInfo
                    };

                    //save in calfit-storage database
                    var userKey = await firebaseDbHelper.InsertUserInCalfitStorageAndReturnKeyAsync<Models.User>(user);


                    if (!string.IsNullOrEmpty(userKey))
                    {

                        membership.UserKey = userKey;

                        user.UserKey = userKey;

                        //save in firebase-storage database
                        await firebaseDbHelper.InsertAsync<Membership>(userKey,membership);

                        //save in firebase-users database
                        await firebaseDbHelper.InsertUserInCalfitUsersAsync<Models.User>(user);

                        //save in local database sqlite
                        await _dataContext.InsertAsync(user);
                        await _dataContext.InsertAsync(membership);

                        await Toast.Make(AppResource.ResourceManager.GetString("Membershipregisteredsuccessfully") ?? "", ToastDuration.Short).Show();

                        //await GoToAsyncWithShell(nameof(LoginPage), true);

                    }
                    else
                    {
                        await Shell.Current.DisplayAlert(
                            AppResource.ResourceManager.GetString("Error", CultureInfo.CurrentCulture) ?? ""
                            , AppResource.ResourceManager.GetString("Cannotregister", CultureInfo.CurrentCulture) ?? ""
                            , AppResource.ResourceManager.GetString("Ok", CultureInfo.CurrentCulture) ?? "");

                    }

                }
                else
                {
                    await Shell.Current.DisplayAlert(
                        AppResource.ResourceManager.GetString("Error", CultureInfo.CurrentCulture) ?? ""
                        , AppResource.ResourceManager.GetString("Emailexistsbefore", CultureInfo.CurrentCulture) ?? ""
                        , AppResource.ResourceManager.GetString("Ok", CultureInfo.CurrentCulture) ?? "");
                }
            }
            catch (Exception ex)
            {
                await Shell.Current.DisplayAlert(
                    AppResource.ResourceManager.GetString("Error", CultureInfo.CurrentCulture) ?? ""
                    , ex.Message
                    , AppResource.ResourceManager.GetString("Ok", CultureInfo.CurrentCulture) ?? "");
            }

        }
    }
}
