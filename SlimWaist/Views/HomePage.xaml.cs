using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Maui.Core;
using SlimWaist.Extentions;
using SlimWaist.Models;
using SlimWaist.ViewModels;
using System.Globalization;
using UraniumUI.Pages;

namespace SlimWaist.Views;

public partial class HomePage : UraniumContentPage
{
    private readonly HomeVM _homeVM;
    private readonly DataContext _dataContext;
    private List<Membership> memberships = new List<Membership>();
    private Membership membership;
    private Setting setting;
    private List<Setting> settings = new List<Setting>();

    bool WantToExit = false;


    public HomePage(HomeVM homeVM, DataContext dataContext)
    {
        InitializeComponent();

        _homeVM = homeVM;

        _dataContext = dataContext;

        //_dataContext.ChangeFlowDirection(this);
        //var dd = ChangeDirections.instance.FlowDirection;

        BindingContext = _homeVM;

        if (App.setting.CultureInfo == "ar-SA")
        {
            CultureInfo.CurrentCulture = new CultureInfo("ar-SA");

            Translator.instance.CultureInfo = new CultureInfo("ar-SA");

            ChangeDirections.instance.FlowDirection = FlowDirection.RightToLeft;
        }
        else
        {
            CultureInfo.CurrentCulture = new CultureInfo("en-US");

            Translator.instance.CultureInfo = new CultureInfo("en-US");

            ChangeDirections.instance.FlowDirection = FlowDirection.LeftToRight;
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