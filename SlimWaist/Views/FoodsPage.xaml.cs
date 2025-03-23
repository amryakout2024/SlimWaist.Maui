using SlimWaist.Models;
using SlimWaist.ViewModels;
using System.Globalization;
using UraniumUI.Pages;

namespace SlimWaist.Views;

public partial class FoodsPage : UraniumContentPage
{
    private readonly FoodsVM _foodsVM;

    public FoodsPage(FoodsVM foodsVM)
	{
		InitializeComponent();

        _foodsVM = foodsVM;

        BindingContext = _foodsVM;

    }
    protected async override void OnAppearing()
    {
        await _foodsVM.Init();
    }
    protected override bool OnBackButtonPressed()
    {
#if ANDROID
        Shell.Current.GoToAsync($"//{nameof(HomePage)}", animate: true);
#endif
        return true;

        //        if (_foodsVM.IsFoodSelected)
        //        {
        //            _foodsVM.IsFoodSelected = false;

        //            return true;
        //        }
        //        else
        //        {

        //#if ANDROID
        //        Shell.Current.GoToAsync($"//{nameof(HomePage)}", animate: true);
        //#endif
        //        return true;

        //        }
    }

}