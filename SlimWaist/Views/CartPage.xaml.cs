using SlimWaist.ViewModels;
using UraniumUI.Pages;

namespace SlimWaist.Views;

public partial class CartPage : UraniumContentPage
{
    private readonly CartVM _cartVM;

    public CartPage(CartVM cartVM)
    {
        InitializeComponent();

        _cartVM = cartVM;

        BindingContext = _cartVM;
    }
    protected async override void OnAppearing()
    {
        await _cartVM.Init();
    }
    protected override bool OnBackButtonPressed()
    {
#if ANDROID
        Shell.Current.GoToAsync($"//{nameof(HomePage)}", animate: true);
#endif
        return true;
    }
}