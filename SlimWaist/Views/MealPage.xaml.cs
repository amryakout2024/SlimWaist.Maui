using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Maui.Core;
using CommunityToolkit.Maui.Views;
using Microcharts;
using Microsoft.Maui.Controls.PlatformConfiguration;
using SkiaSharp;
using SlimWaist.Extentions;
using SlimWaist.Models;
using SlimWaist.Popups;
using SlimWaist.ViewModels;
using System.Drawing;
using System.Globalization;
using System.Threading.Tasks;
using UraniumUI.Pages;

namespace SlimWaist.Views;

public partial class MealPage : UraniumContentPage
{
    private readonly MealVM _mealVM;

    public MealPage(MealVM mealVM)
    {
        InitializeComponent();

        _mealVM = mealVM;

        BindingContext = _mealVM;

    }

    protected async override void OnAppearing()
    {
        base.OnAppearing();

        await _mealVM.init();
    }

    //protected async override void OnNavigatedTo(NavigatedToEventArgs args)
    //{
    //    base.OnNavigatedTo(args);
    //}

    protected override bool OnBackButtonPressed()
    {

#if ANDROID
        Shell.Current.GoToAsync($"//{nameof(HomePage)}", animate: true);
#endif
        return true;

    }

//    private void TapGestureRecognizer_Tapped(object sender, TappedEventArgs e)
//    {
//        tf.Focus();
//        //HideSoftInputOnTapped = false;

//#if ANDROID
//Android.Views.InputMethods.InputMethodManager inputManager = (Android.Views.InputMethods.InputMethodManager)(Microsoft.Maui.ApplicationModel.Platform.CurrentActivity).GetSystemService(Android.App.Application.InputMethodService);

//        inputManager.ShowSoftInput((sender as Entry).Handler.PlatformView as AndroidX.AppCompat.Widget.AppCompatEditText, Android.Views.InputMethods.ShowFlags.Forced);
        
//        ////inputManager.ToggleSoftInput(Android.Views.InputMethods.InputMethodManager.ShowForced, Android.Views.InputMethods.HideSoftInputFlags.ImplicitOnly);

//        ////var imm = (Android.Views.InputMethods.InputMethodManager)MauiApplication.Current.GetSystemService(Android.Content.Context.InputMethodService);
//        ////if (imm != null)
//        ////{
//        ////    var activity = Microsoft.Maui.ApplicationModel.Platform.CurrentActivity;
//        ////    Android.OS.IBinder wToken = activity.CurrentFocus?.WindowToken;
//        ////    imm.HideSoftInputFromWindow(wToken, 0);
//        ////}
//#endif
//    }
}