using SlimWaist.ViewModels;
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
        Shell.Current.GoToAsync($"//{nameof(HomePage)}/{nameof(MealPage)}", animate: true);
#endif
        return true;
    }

}