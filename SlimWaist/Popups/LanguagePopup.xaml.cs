using CommunityToolkit.Maui.Views;
using SlimWaist.Extentions;
using SlimWaist.Models;
#if ANDROID
using Android.App;
using Android.Content;
using Android.Content.PM;
using Java.Lang;
#endif

namespace SlimWaist.Popups;

public partial class LanguagePopup : Popup
{
    private List<Membership> memberships;
    private Membership membership;
    private Setting setting;
    private List<Setting> settings;

    public LanguagePopup()
    {
        InitializeComponent();

        Dispatcher.DispatchAsync(async () =>
        {
            memberships = await App._dataContext.GetAsync<Membership>();

            settings = await App._dataContext.GetAsync<Setting>();

            setting = settings.Where(x => x.Id == 1).FirstOrDefault();

            membership = memberships.Where(x => x.Id ==setting.SavedMembershipId).FirstOrDefault();

            if (membership.CultureInfo == "ar-SA")
            {
                arabic.IsChecked = true;
                english.IsChecked = false;
            }
            else
            {
                arabic.IsChecked = false;
                english.IsChecked = true;
            }


        });

    }

    private async void arabic_Checked(object sender, EventArgs e)
    {
        if (membership.CultureInfo == "en-US")
        {
            //ChangeDirections.instance.FlowDirection = FlowDirection.RightToLeft;

            //CultureInfo.CurrentCulture = new CultureInfo("ar-SA");

            membership.CultureInfo = "ar-SA";

            setting.CultureInfo = "ar-SA";

            await App._dataContext.UpdateAsync<Membership>(membership);

            await App._dataContext.UpdateAsync<Setting>(setting);


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

            //CultureInfo.CurrentCulture = new CultureInfo("en-US");

            membership.CultureInfo = "en-US";

            setting.CultureInfo = "en-US";

            await App._dataContext.UpdateAsync<Membership>(membership);

            await App._dataContext.UpdateAsync<Setting>(setting);

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