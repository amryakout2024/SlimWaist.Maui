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
}