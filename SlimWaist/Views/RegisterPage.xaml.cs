using SlimWaist.ViewModels;
using UraniumUI.Pages;

namespace SlimWaist.Views;

public partial class RegisterPage : UraniumContentPage
{
    private readonly RegisterVM _registerVM;

    public RegisterPage(RegisterVM registerVM)
    {
        InitializeComponent();

        _registerVM = registerVM;

        BindingContext = _registerVM;

    }
    protected async override void OnAppearing()
    {
         _registerVM.init();
    }

}