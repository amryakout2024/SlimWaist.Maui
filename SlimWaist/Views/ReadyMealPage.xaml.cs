
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

public partial class ReadyMealPage : UraniumContentPage
{
    private readonly ReadyMealVM _readyMealVM;

    public ReadyMealPage(ReadyMealVM readyMealVM)
    {
        InitializeComponent();

        _readyMealVM = readyMealVM;

        BindingContext = _readyMealVM;

    }

    protected async override void OnAppearing()
    {
        base.OnAppearing();

        await _readyMealVM.init();
    }

    //protected async override void OnNavigatedTo(NavigatedToEventArgs args)
    //{
    //    base.OnNavigatedTo(args);
    //}

    protected override bool OnBackButtonPressed()
    {
#if ANDROID
             Shell.Current.GoToAsync($"//{nameof(HomePage)}/{nameof(MealPage)}/{nameof(FoodsPage)}", animate: true);
#endif

        //#if ANDROID
        //        Shell.Current.GoToAsync("///..", animate: true);
        //#endif
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