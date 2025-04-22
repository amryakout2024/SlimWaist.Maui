using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Maui.Behaviors;
using CommunityToolkit.Maui.Core;
using SlimWaist.Extentions;
using SlimWaist.Models;
using SlimWaist.ViewModels;
using System.Globalization;
using System.Threading;
using UraniumUI.Pages;

namespace SlimWaist.Views;

public partial class HomePage : UraniumContentPage
{
    private readonly HomeVM _homeVM;
    private readonly DataContext _dataContext;
    private List<Membership> memberships=new List<Membership>();
    private Membership membership;
    private Setting setting;
    private List<Setting> settings=new List<Setting>();

    bool WantToExit =false;


    public HomePage(HomeVM homeVM,DataContext dataContext)
	{
		InitializeComponent();

        _homeVM = homeVM;

        _dataContext = dataContext;

        BindingContext = _homeVM;

        if (DataContext.membership.CultureInfo == "ar-SA")
        {
            ChangeDirections.instance.FlowDirection = FlowDirection.RightToLeft;

            CultureInfo.CurrentCulture = new CultureInfo("ar-SA");

            this.FlowDirection = FlowDirection.RightToLeft;


        }
        else
        {
            ChangeDirections.instance.FlowDirection = FlowDirection.RightToLeft;

            CultureInfo.CurrentCulture = new CultureInfo("ar-SA");

            this.FlowDirection = FlowDirection.LeftToRight;
        }

    }

    protected async override void OnAppearing()
    {
        WantToExit = false;

        await _homeVM.init();
    }

    protected override bool OnBackButtonPressed()
    {       
        if (!WantToExit)
        {
            CancellationTokenSource cancellationTokenSource = new CancellationTokenSource();

            Toast.Make("اضغط مرة اخري اذا كنت تريد الخروج !", ToastDuration.Short, 14).Show(cancellationTokenSource.Token);
           
            WantToExit = true;

            return true;
        }

        return false;

        // Return true to prevent back button 

    }
}