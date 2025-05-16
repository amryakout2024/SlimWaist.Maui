using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Maui.Core;
using Microcharts;
using SkiaSharp;
using SlimWaist.Extentions;
using SlimWaist.Models;
using SlimWaist.ViewModels;
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

        BindingContext = _homeVM;

        timer = Application.Current?.Dispatcher.CreateTimer();

        timer.Interval = TimeSpan.FromSeconds(7);

        timer.Tick += Timer_Tick;

    }

    private async void Timer_Tick(object? sender, EventArgs e)
    {
        //chartView.Chart = new RadialGaugeChart()
        //{
        //    Entries = chartEntries,
        //    IsAnimated = true,
        //    AnimationDuration = TimeSpan.FromSeconds(5),
        //};
        //await NoRegiemeLabel.ScaleTo(1.2, 1000, Easing.Linear);
        //await NoRegiemeLabel.ScaleTo(1, 1000, Easing.Linear);
    }

    protected async override void OnNavigatedTo(NavigatedToEventArgs args)
    {
        base.OnNavigatedTo(args);

        WantToExit = false;

        waistProgressBar.Progress = 0;
        bmiProgressBar.Progress = 0;

        WaistCircumferenceNameLabel.Text = "";
        obesityDegreeNameLabel.Text = "";

        WaistCircumferenceNameLabel.Scale = 0;
        obesityDegreeNameLabel.Scale = 0;

        await _homeVM.init();

        timer.Start();

        waistProgressBar.Loaded += WaistProgressBar_Loaded;

        bmiProgressBar.Loaded += BmiProgressBar_Loaded;
    }

    private async void BmiProgressBar_Loaded(object? sender, EventArgs e)
    {
        await Task.Delay(500);

        waistProgressBar.Progress = 0;

        double mi = (Convert.ToDouble(App.currentMembership.Weight)) / ((Convert.ToDouble(App.currentMembership.Height) / 100) * (Convert.ToDouble(App.currentMembership.Height) / 100));

        var BMI = Math.Round(mi, 2).ToString();

        var ObesityDegreeId = 0;

        if (Convert.ToDouble(BMI) >= 18 && Convert.ToDouble(BMI) <= 24)
        {
            bmiProgressBar.ProgressColor = Colors.Green;

            await bmiProgressBar.ProgressTo(0.2, 2000, Easing.Linear);

            obesityDegreeNameLabel.Text = App.obesityDegrees.Where(x => x.ObesityDegreeId == 1).FirstOrDefault().ObesityDegreeName;

        }
        else if (Convert.ToDouble(BMI) > 24 && Convert.ToDouble(BMI) <= 29)
        {
            bmiProgressBar.ProgressColor = Colors.Orange;

            await bmiProgressBar.ProgressTo(0.4, 2000, Easing.Linear);

            obesityDegreeNameLabel.Text = App.obesityDegrees.Where(x => x.ObesityDegreeId == 2).FirstOrDefault().ObesityDegreeName;

        }
        else if (Convert.ToDouble(BMI) > 29 && Convert.ToDouble(BMI) <= 34)
        {
            bmiProgressBar.ProgressColor = Colors.DarkOrange;

            await bmiProgressBar.ProgressTo(0.6, 2000, Easing.Linear);

            obesityDegreeNameLabel.Text = App.obesityDegrees.Where(x => x.ObesityDegreeId == 3).FirstOrDefault().ObesityDegreeName;

        }
        else if (Convert.ToDouble(BMI) > 34 && Convert.ToDouble(BMI) <= 39)
        {
            bmiProgressBar.ProgressColor = Colors.OrangeRed;

            await bmiProgressBar.ProgressTo(0.8, 2000, Easing.Linear);

            obesityDegreeNameLabel.Text = App.obesityDegrees.Where(x => x.ObesityDegreeId == 4).FirstOrDefault().ObesityDegreeName;

        }
        else if (Convert.ToDouble(BMI) > 39)
        {
            bmiProgressBar.ProgressColor = Colors.Red;

            await bmiProgressBar.ProgressTo(1, 2000, Easing.Linear);

            obesityDegreeNameLabel.Text = App.obesityDegrees.Where(x => x.ObesityDegreeId == 5).FirstOrDefault().ObesityDegreeName;

        }

        await obesityDegreeNameLabel.ScaleTo(1, 1000);
    }

    private async void WaistProgressBar_Loaded(object? sender, EventArgs e)
    {
        await Task.Delay(500);

        waistProgressBar.Progress = 0;

        if (App.currentMembership.WaistCircumferenceMeasurement < 94)
        {
            waistProgressBar.ProgressColor = Colors.Green;

            await waistProgressBar.ProgressTo(0.33, 2000, Easing.Linear);

            WaistCircumferenceNameLabel.Text = App.waistCircumferences.Where(x => x.WaistCircumferenceId == 1).Select(x => x.WaistCircumferenceName).FirstOrDefault().ToString();
        }
        else if (App.currentMembership.WaistCircumferenceMeasurement >= 94 && App.currentMembership.WaistCircumferenceMeasurement <= 101)
        {
            waistProgressBar.ProgressColor = Colors.DarkOrange;

            await waistProgressBar.ProgressTo(0.66, 2000, Easing.Linear);

            WaistCircumferenceNameLabel.Text = App.waistCircumferences.Where(x => x.WaistCircumferenceId == 2).Select(x => x.WaistCircumferenceName).FirstOrDefault().ToString();

        }
        else if (App.currentMembership.WaistCircumferenceMeasurement > 101)
        {
            waistProgressBar.ProgressColor = Colors.Red;

            await waistProgressBar.ProgressTo(1, 2000, Easing.Linear);

            WaistCircumferenceNameLabel.Text = App.waistCircumferences.Where(x => x.WaistCircumferenceId == 3).Select(x => x.WaistCircumferenceName).FirstOrDefault().ToString();
        }

        await WaistCircumferenceNameLabel.ScaleTo(1, 1000);
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

}