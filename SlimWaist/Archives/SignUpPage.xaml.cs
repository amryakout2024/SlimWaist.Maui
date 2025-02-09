using SlimWaist.ViewModels;
using UraniumUI.Pages;

namespace SlimWaist.Views;

public partial class SignUpPage : UraniumContentPage
{
    private readonly SignUpVM _signUpVM;

    public SignUpPage(SignUpVM signUpVM)
	{
		InitializeComponent();
        
        _signUpVM = signUpVM;

        BindingContext = _signUpVM;
    }

    protected async override void OnAppearing()
    {
        await _signUpVM.init();
    }

}