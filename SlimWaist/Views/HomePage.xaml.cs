using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Maui.Core;
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

        timer.Interval = TimeSpan.FromSeconds(20);

        timer.Tick += Timer_Tick;

    }

    private async void Timer_Tick(object? sender, EventArgs e)
    {
        await NoRegiemeLabel.ScaleTo(1.2, 1000, Easing.Linear);
        await NoRegiemeLabel.ScaleTo(1, 1000, Easing.Linear);
    }

    protected async override void OnAppearing()
    {
        WantToExit = false;

        await _homeVM.init();

        timer.Start();

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