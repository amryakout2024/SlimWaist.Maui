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

        //memberships = DataContext.memberships;

        //settings=DataContext.settings;

        //setting = settings.Where(x => x.Id == 1).FirstOrDefault() ?? new Setting();

        //membership = memberships.Where(x => x.Id == setting.CurrentMemberShipId).FirstOrDefault() ?? new Membership();

        //if (membership.CultureInfo == "ar-SA")
        //{
        //    ChangeDirections.instance.FlowDirection = FlowDirection.RightToLeft;

        //    CultureInfo.CurrentCulture = new CultureInfo("ar-SA");
        //}
        //else
        //{
        //    ChangeDirections.instance.FlowDirection = FlowDirection.LeftToRight;

        //    CultureInfo.CurrentCulture = new CultureInfo("en-US");
        //}

//#if ANDROID

//        Dispatcher.DispatchAsync(async() =>
//        {

//            memberships = await _dataContext.LoadAsync<Membership>();

//            settings = await _dataContext.LoadAsync<Setting>();

//            setting = settings.Where(x => x.Id == 1).FirstOrDefault() ?? new Setting();

//            membership = memberships.Where(x => x.Id == setting.CurrentMemberShipId).FirstOrDefault() ?? new Membership();

//            if (membership.CultureInfo == "ar-SA")
//            {
//                ChangeDirections.instance.FlowDirection = FlowDirection.RightToLeft;

//                CultureInfo.CurrentCulture = new CultureInfo("ar-SA");
//            }
//            else
//            {
//                ChangeDirections.instance.FlowDirection = FlowDirection.LeftToRight;

//                CultureInfo.CurrentCulture = new CultureInfo("en-US");
//            }


//        });
//#endif

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