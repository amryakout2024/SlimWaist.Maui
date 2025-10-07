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
    private readonly DataContext _dataContext;

    public LoginPage(LoginVM loginVM,DataContext dataContext)
    {
        InitializeComponent();
        _loginVM = loginVM;

        _dataContext = dataContext;

        BindingContext = _loginVM;

        Connectivity.ConnectivityChanged += Connectivity_ConnectivityChanged;
    }

    void Connectivity_ConnectivityChanged(object sender, ConnectivityChangedEventArgs e)
    {
        if (e.NetworkAccess == NetworkAccess.ConstrainedInternet)
        {
            DisplayAlert("Warning", "Internet access is available but is limited.", "OK");
        }
        else if (e.NetworkAccess != NetworkAccess.Internet)
        {
            DisplayAlert("Warning", "Internet access has been lost.", "OK");
        }
    }
    protected async override void OnNavigatedTo(NavigatedToEventArgs args)
    {
        base.OnNavigatedTo(args);

        await _loginVM.init();
    }

    protected override bool OnBackButtonPressed()
    {
        Shell.Current.GoToAsync($"//{nameof(HomePage)}", animate: true);

        return true;
    }

    //change language to be removed in the future
    private async void TapGestureRecognizer_Tapped(object sender, TappedEventArgs e)
    {

        if (App.setting.CultureInfo == "ar-SA")
        {
            App.setting.CultureInfo = "en-US";

            await _dataContext.UpdateAsync<Setting>(App.setting);

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

            await _dataContext.UpdateAsync<Setting>(App.setting);


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