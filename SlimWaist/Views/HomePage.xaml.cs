using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Maui.Core;
using CommunityToolkit.Maui.Views;
using Microcharts;
using SkiaSharp;
using SlimWaist.Extentions;
using SlimWaist.Languages;
using SlimWaist.Models;
using SlimWaist.Popups;
using SlimWaist.ViewModels;
using System.Drawing;
using System.Globalization;
using System.Threading.Tasks;
using UraniumUI.Pages;

namespace SlimWaist.Views;

public partial class HomePage : UraniumContentPage
{
    private readonly HomeVM _homeVM;

    bool WantToExit = false;

    IDispatcherTimer timer;

    public HomePage(HomeVM homeVM)
    {
        InitializeComponent();

        _homeVM = homeVM;

        MyDatePicker.MinimumDate = new DateTime(2025, 1, 1);

        MyDatePicker.MaximumDate= DateTime.Now.AddDays(30);

        BindingContext = _homeVM;
    }

    protected async override void OnNavigatedTo(NavigatedToEventArgs args)
    {
        base.OnNavigatedTo(args);


        if (HomeVM.CurrentDayDiet.IsExistsInDb)
        {
            MyDatePicker.Date = new DateTime(
HomeVM.CurrentDayDiet.DayDietDate.Year
, HomeVM.CurrentDayDiet.DayDietDate.Month
, HomeVM.CurrentDayDiet.DayDietDate.Day);
        }

        WantToExit = false;

        await _homeVM.init();

    }

    protected override bool OnBackButtonPressed()
    {
        if (!WantToExit)
        {
            CancellationTokenSource cancellationTokenSource = new CancellationTokenSource();

            Toast.Make(AppResource.ResourceManager.GetString("Clickagaintoexit",CultureInfo.CurrentCulture), ToastDuration.Short, 14).Show(cancellationTokenSource.Token);

            WantToExit = true;

            return true;
        }


#if ANDROID
            App.Current.CloseWindow(App.Current.MainPage.Window);
            App.Current.Quit();
#endif
        return false;

        // Return true to prevent back button 

    }

    private void TapGestureRecognizer_Tapped(object sender, TappedEventArgs e)
    {
        if (HomeVM.CurrentMembership.IsExistsInDb == false)
        {
            var popup = new NeedLoginPopup();

            this.ShowPopup(popup);
        }
    }

    private void TapGestureRecognizer_Tapped_1(object sender, TappedEventArgs e)
    {
        if (HomeVM.CurrentMembership.IsExistsInDb==false)
        {
            var popup = new NeedLoginPopup();

            this.ShowPopup(popup);
        }
    }

    private void picker_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (HomeVM.CurrentMembership.IsExistsInDb == false)
        {
            var popup = new NeedLoginPopup();

            this.ShowPopup(popup);
        }

    }
}