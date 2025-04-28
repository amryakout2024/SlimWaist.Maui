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
    private readonly DataContext _dataContext;
    private readonly IBadge _badge;
    //,IBadge badge
    public LoginPage(LoginVM loginVM,DataContext dataContext)
    {
        InitializeComponent();

        _loginVM = loginVM;
        _dataContext = dataContext;

        //_dataContext.ChangeFlowDirection(this);

        BindingContext = _loginVM;

        //if (DataContext.membership.CultureInfo == "ar-SA")
        //{
        //    //CultureInfo.CurrentCulture = new CultureInfo("ar-SA");

        //    ChangeDirections.instance.FlowDirection = FlowDirection.RightToLeft;

        //    //this.FlowDirection = FlowDirection.RightToLeft;

        //}
        //else
        //{
        //    //CultureInfo.CurrentCulture = new CultureInfo("en-US");

        //    ChangeDirections.instance.FlowDirection = FlowDirection.LeftToRight;

        //    //this.FlowDirection = FlowDirection.LeftToRight;
        //}
        //var dd = ChangeDirections.instance.FlowDirection;
        //if (DataContext.membership.CultureInfo == "ar-SA")
        //{
        //    ChangeDirections.instance.FlowDirection = FlowDirection.RightToLeft;

        //    //CultureInfo.CurrentCulture = new CultureInfo("ar-SA");

        //    //this.FlowDirection = FlowDirection.RightToLeft;


        //}
        //else
        //{
        //    ChangeDirections.instance.FlowDirection = FlowDirection.RightToLeft;

        //    //CultureInfo.CurrentCulture = new CultureInfo("ar-SA");

        //    //this.FlowDirection = FlowDirection.LeftToRight;
        //}

        //_badge = badge;
        //_badge.SetCount(6);

    }

    protected async override void OnAppearing()
    {

        await _loginVM.init();

        //if (DataContext.membership.CultureInfo == "ar-SA")
        //{
        //    //ChangeDirections.instance.FlowDirection = FlowDirection.RightToLeft;

        //    //CultureInfo.CurrentCulture = new CultureInfo("ar-SA");

        //    this.FlowDirection = FlowDirection.RightToLeft;


        //}
        //else
        //{
        //    //ChangeDirections.instance.FlowDirection = FlowDirection.RightToLeft;

        //    //CultureInfo.CurrentCulture = new CultureInfo("ar-SA");

        //    this.FlowDirection = FlowDirection.LeftToRight;
        //}

    }
}