using CommunityToolkit.Maui.ApplicationModel;
using SlimWaist.ViewModels;
using UraniumUI.Pages;

namespace SlimWaist.Views;

public partial class LoginPage : UraniumContentPage
{
    private readonly LoginVM _loginVM;
    private readonly IBadge _badge;
    //,IBadge badge
    public LoginPage(LoginVM loginVM)
    {
        InitializeComponent();

        _loginVM = loginVM;
        //_badge = badge;
        //_badge.SetCount(6);
        BindingContext = _loginVM;
    }

    protected async override void OnAppearing()
    {

        await _loginVM.init();
    }
}