using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Maui.Core;
using SlimWaist.Extentions;
using SlimWaist.Models;
using SlimWaist.ViewModels;
using System.Globalization;
using UraniumUI.Pages;

namespace SlimWaist.Views;

public partial class HomePage : UraniumContentPage
{
    private readonly HomeVM _homeVM;

    bool WantToExit = false;


    public HomePage(HomeVM homeVM)
    {
        InitializeComponent();

        _homeVM = homeVM;

        BindingContext = _homeVM;
    }

    protected async override void OnAppearing()
    {
        WantToExit = false;

        await _homeVM.init();

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