using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Maui.Core;
using CommunityToolkit.Maui.Views;
using Microcharts;
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

        //timer = Application.Current?.Dispatcher.CreateTimer();

        //timer.Interval = TimeSpan.FromSeconds(7);

        //timer.Tick += Timer_Tick;
    }

    protected async override void OnNavigatedTo(NavigatedToEventArgs args)
    {
        base.OnNavigatedTo(args);

        await _homeVM.init();

        var d = App.CurrentDayDiet;

        if (App.CurrentDayDiet.IsExistsInDb)
        {
            MyDatePicker.Date = new DateTime(
App.CurrentDayDiet.DayDietDate.Year
, App.CurrentDayDiet.DayDietDate.Month
, App.CurrentDayDiet.DayDietDate.Day);
        }
        else
        {
            MyDatePicker.Date = DateTime.Now;

        }

        WantToExit = false;

    }

    protected override bool OnBackButtonPressed()
    {
        if (!WantToExit)
        {
            CancellationTokenSource cancellationTokenSource = new CancellationTokenSource();

            Toast.Make("اضغط مرة اخري اذا كنت تريد الخروج !", ToastDuration.Short, 14).Show(cancellationTokenSource.Token);

            WantToExit = true;

            return true;
        }

        return false;

        // Return true to prevent back button 

    }

    private void MyDatePicker_DateSelected(object sender, DateChangedEventArgs e)
    {

    }
}