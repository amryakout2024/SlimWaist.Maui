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
    private readonly IBadge _badge;
    private Setting setting;

    //,IBadge badge
    public LoginPage(LoginVM loginVM, DataContext dataContext)
    {
        InitializeComponent();

        _loginVM = loginVM;

        _dataContext = dataContext;

        BindingContext = _loginVM;

    }

    protected async override void OnAppearing()
    {
        await _loginVM.init();

        setting = _dataContext.Database.Table<Setting>().FirstOrDefault();
    }

    private async void TapGestureRecognizer_Tapped(object sender, TappedEventArgs e)
    {
        if (App.setting.CultureInfo == "ar-SA")
        {
            ChangeDirections.instance.FlowDirection = FlowDirection.LeftToRight;

            CultureInfo.CurrentCulture = new CultureInfo("en-US");

            setting.CultureInfo = "en-US";

            await _dataContext.UpdateAsync<Setting>(setting);

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
            ChangeDirections.instance.FlowDirection = FlowDirection.RightToLeft;

            CultureInfo.CurrentCulture = new CultureInfo("ar-SA");

            setting.CultureInfo = "ar-SA";

            await _dataContext.UpdateAsync<Setting>(setting);


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