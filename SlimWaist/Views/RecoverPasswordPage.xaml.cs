using SlimWaist.ViewModels;
using UraniumUI.Pages;

namespace SlimWaist.Views;

public partial class RecoverPasswordPage : UraniumContentPage
{
    private readonly RecoverPasswordVM _recoverPasswordVM;

    public RecoverPasswordPage(RecoverPasswordVM recoverPasswordVM)
	{
		InitializeComponent();

        _recoverPasswordVM = recoverPasswordVM;

        BindingContext = _recoverPasswordVM;
    }

    protected override bool OnBackButtonPressed()
    {
        Shell.Current.GoToAsync($"//{nameof(LoginPage)}", animate: true);

        return true;
    }

}