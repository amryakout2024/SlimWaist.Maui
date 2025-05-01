using SlimWaist.ViewModels;
using UraniumUI.Pages;

namespace SlimWaist.Views;

public partial class AddFoodPage : UraniumContentPage
{
    private readonly AddFoodVM _addFoodVM;

    public AddFoodPage(AddFoodVM addFoodVM)
    {
        InitializeComponent();

        _addFoodVM = addFoodVM;

        BindingContext = _addFoodVM;

    }
    protected async override void OnAppearing()
    {
        //await _cartVM.Init();
    }
    protected override bool OnBackButtonPressed()
    {
#if ANDROID
        Shell.Current.GoToAsync("..", animate: true);
#endif
        return true;
    }

}