using Android.App;
using Android.Content.PM;
using Android.Content.Res;
using Android.OS;
using SlimWaist.Models;

namespace SlimWaist
{
    [Activity(Theme = "@style/Maui.SplashTheme", MainLauncher = true, LaunchMode = LaunchMode.SingleTop, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation | ConfigChanges.UiMode | ConfigChanges.ScreenLayout | ConfigChanges.SmallestScreenSize | ConfigChanges.Density)]
    public class MainActivity : MauiAppCompatActivity
    {

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            // Disable font scaling
            Resources.Configuration.FontScale = 1.0f;
            // Your existing code goes here

            //Forece app to run as RTL


            if (DataContext.membership.CultureInfo == "ar-SA")
            {
                Window.DecorView.LayoutDirection = (Android.Views.LayoutDirection)LayoutDirection.LeftToRight;

            }
            else
            {
                Window.DecorView.LayoutDirection = (Android.Views.LayoutDirection)LayoutDirection.RightToLeft;
            }


        }

        //disable font change in user device
        //public override Resources? Resources
        //{
        //    get
        //    {
        //        Resources resource = base.Resources;
        //        Configuration configuration = new Configuration();
        //        configuration.SetToDefaults();
        //        if (Build.VERSION.SdkInt >= BuildVersionCodes.NMr1)
        //        {
        //            return CreateConfigurationContext(configuration).Resources;
        //        }
        //        else
        //        {
        //            resource.UpdateConfiguration(configuration, resource.DisplayMetrics);
        //            return resource;
        //        }
        //    }
        //}

        protected override void OnStop()
        {
            base.OnStop();

           
        }
    }
}
