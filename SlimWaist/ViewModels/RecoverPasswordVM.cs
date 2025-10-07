using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Maui.Core;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using SlimWaist.Helpers;
using SlimWaist.Languages;
using SlimWaist.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SlimWaist.ViewModels
{
    public partial class RecoverPasswordVM(DataContext dataContext) : BaseVM
    {
        private readonly DataContext _dataContext = dataContext;

        private FirebaseAuthHelper firebaseAuthHelper = new FirebaseAuthHelper();

        private FirebaseDbHelper firebaseDbHelper = new FirebaseDbHelper();

        [ObservableProperty]
        private string _email= "amryakout2016@gmail.com";

        [RelayCommand]
        private async Task RecoverPassword()
        {
            try
            {
                Membership membership = _dataContext.Database.Table<Membership>().Where(x => x.Email == Email).FirstOrDefault() ?? new Membership();

                if (membership.IsExistsInDb)
                {
                    await EmailSender.SendEmail(Email, $"hello : {membership.Name}", $"your password is {membership.Password}");

                    await Toast.Make(AppResource.ResourceManager.GetString("Passwordsenttoyouremail", CultureInfo.CurrentCulture) ?? "", ToastDuration.Short).Show();
                }
                else
                {
                    var user = (await firebaseDbHelper.GetUsersAsync()).Where(x => x.Email == Email).FirstOrDefault() ?? new User();

                    if (!string.IsNullOrEmpty(user.Email))
                    {
                        var membershipFromFirebase = (await firebaseDbHelper.GetAsync<Membership>(user.UserKey)).FirstOrDefault();

                        await EmailSender.SendEmail(Email, $"hello : {membershipFromFirebase.Name}", $"your password is {membershipFromFirebase.Password}");

                        await Toast.Make(AppResource.ResourceManager.GetString("Passwordsenttoyouremail", CultureInfo.CurrentCulture) ?? "", ToastDuration.Short).Show();
                    }
                    else
                    {
                        await Shell.Current.DisplayAlert(
                            AppResource.ResourceManager.GetString("Error", CultureInfo.CurrentCulture) ?? ""
                            , AppResource.ResourceManager.GetString("Emailisnotcorrect", CultureInfo.CurrentCulture) ?? ""
                            , AppResource.ResourceManager.GetString("Ok", CultureInfo.CurrentCulture) ?? "");
                    }
                }
            }
            catch (Exception ex)
            {
                await Shell.Current.DisplayAlert(
                AppResource.ResourceManager.GetString("Error", CultureInfo.CurrentCulture) ?? ""
                , AppResource.ResourceManager.GetString("Emailisnotcorrect", CultureInfo.CurrentCulture) ?? ""
                , AppResource.ResourceManager.GetString("Ok", CultureInfo.CurrentCulture) ?? "");
            }
        }

    }
}
