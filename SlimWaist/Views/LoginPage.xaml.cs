using CommunityToolkit.Maui.ApplicationModel;
using SlimWaist.Extentions;
using SlimWaist.Models;
using SlimWaist.ViewModels;
using System.Globalization;
using UraniumUI.Pages;
#if ANDROID
using Android.App;
using Android.Content;
using Android.Content.PM;
using Java.Lang;
#endif

namespace SlimWaist.Views;

public partial class LoginPage : UraniumContentPage
{
    private readonly LoginVM _loginVM;
    private readonly IBadge _badge;

    //,IBadge badge
    public LoginPage(LoginVM loginVM)
    {
        InitializeComponent();

        _loginVM = loginVM;

        BindingContext = _loginVM;

    }

    protected async override void OnAppearing()
    {
        await _loginVM.init();
    }

    private async void TapGestureRecognizer_Tapped(object sender, TappedEventArgs e)
    {
        if (App.setting.CultureInfo == "ar-SA")
        {
            App.setting.CultureInfo = "en-US";

            await App.dataContext.UpdateAsync<Setting>(App.setting);

#if ANDROID
            var context = Platform.AppContext;
            PackageManager packageManager = context.PackageManager;
            Intent intent = packageManager.GetLaunchIntentForPackage(context.PackageName);
            ComponentName componentName = intent.Component;
            Intent mainIntent = Intent.MakeRestartActivityTask(componentName);
            mainIntent.SetPackage(context.PackageName);
            context.StartActivity(mainIntent);
            Runtime.GetRuntime().Exit(0);
#endif
        }
        else
        {
            App.setting.CultureInfo = "ar-SA";

            await App.dataContext.UpdateAsync<Setting>(App.setting);


#if ANDROID
            var context = Platform.AppContext;
            PackageManager packageManager = context.PackageManager;
            Intent intent = packageManager.GetLaunchIntentForPackage(context.PackageName);
            ComponentName componentName = intent.Component;
            Intent mainIntent = Intent.MakeRestartActivityTask(componentName);
            mainIntent.SetPackage(context.PackageName);
            context.StartActivity(mainIntent);
            Runtime.GetRuntime().Exit(0);

#endif

            // Works for me, but I replaced
            //Runtime.GetRuntime().Exit(0);`  
            //with
            //Platform.CurrentActivity.FinishAffinity(); // Close all activities properly
        }


    }
}