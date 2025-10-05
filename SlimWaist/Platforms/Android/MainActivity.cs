using Android.App;
using Android.Content.PM;
using Android.OS;
using Bumptech.Glide.Request.Target;
using System.Globalization;

namespace SlimWaist
{
    [Activity(Theme = "@style/Maui.SplashTheme", MainLauncher = true, LaunchMode = LaunchMode.SingleTop, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation | ConfigChanges.UiMode | ConfigChanges.ScreenLayout | ConfigChanges.SmallestScreenSize | ConfigChanges.Density)]
    public class MainActivity() : MauiAppCompatActivity
    {

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            // Disable font scaling
            //Resources.Configuration.FontScale = 1.0f;



            var currentLanguage = Resources.Configuration.Locales.Get(0);

            //Forece app to run as RTL

            var firstTwoLetterFromDeviceLanguage = currentLanguage.Language;

            if (firstTwoLetterFromDeviceLanguage == "ar")
            {
                if (App.AppCultureInfoName == "ar-SA")
                {
                    Window.DecorView.LayoutDirection = (Android.Views.LayoutDirection)LayoutDirection.RightToLeft;
                }
                else
                {
                    Window.DecorView.LayoutDirection = (Android.Views.LayoutDirection)LayoutDirection.Unknown;
                }

            }
            else
            {
                if (App.AppCultureInfoName == "ar-SA")
                {
                    Window.DecorView.LayoutDirection = (Android.Views.LayoutDirection)LayoutDirection.LeftToRight;
                }
                else
                {
                    Window.DecorView.LayoutDirection = (Android.Views.LayoutDirection)LayoutDirection.RightToLeft;
                }

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
