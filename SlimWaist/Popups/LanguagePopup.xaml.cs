using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Maui.Core;
using CommunityToolkit.Maui.Views;
using Microsoft.Maui.Controls.Compatibility;
using SlimWaist.Extentions;
using SlimWaist.Models;
using SlimWaist.Views;
using System.Diagnostics;
using System.Globalization;
#if ANDROID
using Android.App;
using Android.Content;
using Android.Content.PM;
using Java.Lang;
#endif

namespace SlimWaist.Popups;

public partial class LanguagePopup : Popup
{
    private readonly DataContext _dataContext;

    private List<Membership> memberships;
    private Membership membership;
    private Setting setting;
    private List<Setting> settings;

    public LanguagePopup(DataContext dataContext)
	{
		InitializeComponent();

        _dataContext = dataContext;

        Dispatcher.DispatchAsync(async () =>
        {
            memberships = await _dataContext.LoadAsync<Membership>();

            settings = await _dataContext.LoadAsync<Setting>();

            setting = settings.Where(x => x.Id == 1).FirstOrDefault();

            membership = memberships.Where(x => x.Id == setting.CurrentMemberShipId).FirstOrDefault();

            if (membership.CultureInfo == "ar-SA")
            {
                arabic.IsChecked = true;
                english.IsChecked = false;
            }
            else
            {
                arabic.IsChecked=false;
                english.IsChecked = true;
            }


        });

    }

    private async void arabic_Checked(object sender, EventArgs e)
    {
        if (membership.CultureInfo == "en-US")
        {
            ChangeDirections.instance.FlowDirection = FlowDirection.RightToLeft;

            CultureInfo.CurrentCulture = new CultureInfo("ar-SA");

            membership.CultureInfo = "ar-SA";

            await _dataContext.UpdateAsync<Membership>(membership);

#if ANDROID   
   var context = Platform.AppContext;
      PackageManager packageManager = context.PackageManager;
      Intent intent = packageManager.GetLaunchIntentForPackage(context.PackageName);
      ComponentName componentName = intent.Component;
      Intent mainIntent = Intent.MakeRestartActivityTask(componentName);
      mainIntent.SetPackage(context.PackageName);
      context.StartActivity(mainIntent);
      Runtime.GetRuntime().Exit(0);

#endif

            this.Close();

            // Works for me, but I replaced
            //Runtime.GetRuntime().Exit(0);`  
            //with
            //Platform.CurrentActivity.FinishAffinity(); // Close all activities properly
        }
    }

    private async void english_Checked(object sender, EventArgs e)
    {
        if (membership.CultureInfo == "ar-SA")
        {
            ChangeDirections.instance.FlowDirection = FlowDirection.LeftToRight;

            CultureInfo.CurrentCulture = new CultureInfo("en-US");

            membership.CultureInfo = "en-US";

            await _dataContext.UpdateAsync<Membership>(membership);

#if ANDROID
   var context = Platform.AppContext;
      PackageManager packageManager = context.PackageManager;
      Intent intent = packageManager.GetLaunchIntentForPackage(context.PackageName);
      ComponentName componentName = intent.Component;
      Intent mainIntent = Intent.MakeRestartActivityTask(componentName);
      mainIntent.SetPackage(context.PackageName);
      context.StartActivity(mainIntent);
      Runtime.GetRuntime().Exit(0);
#endif

            this.Close();
        }
    }
}