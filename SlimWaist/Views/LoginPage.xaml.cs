using CommunityToolkit.Maui.ApplicationModel;
using SlimWaist.Extentions;
using SlimWaist.Models;
using SlimWaist.ViewModels;
using System.Globalization;
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


        //if (DataContext.membership.CultureInfo == "ar-SA")
        //{
        //    ChangeDirections.instance.FlowDirection = FlowDirection.RightToLeft;

        //    CultureInfo.CurrentCulture = new CultureInfo("ar-SA");

        //    this.FlowDirection = FlowDirection.RightToLeft;


        //}
        //else
        //{
        //    ChangeDirections.instance.FlowDirection = FlowDirection.RightToLeft;

        //    CultureInfo.CurrentCulture = new CultureInfo("ar-SA");

        //    this.FlowDirection = FlowDirection.LeftToRight;
        //}
    }

    protected async override void OnAppearing()
    {

        await _loginVM.init();
    }
}