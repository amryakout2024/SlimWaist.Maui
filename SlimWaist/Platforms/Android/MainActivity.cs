using Android.App;
using Android.Content.PM;
using Android.Content.Res;
using Android.OS;

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
        }

        //disable font change in user device
        public override Resources? Resources
        {
            get
            {
                Resources resource = base.Resources;
                Configuration configuration = new Configuration();
                configuration.SetToDefaults();
                if (Build.VERSION.SdkInt >= BuildVersionCodes.NMr1)
                {
                    return CreateConfigurationContext(configuration).Resources;
                }
                else
                {
                    resource.UpdateConfiguration(configuration, resource.DisplayMetrics);
                    return resource;
                }
            }
        }
    }
}
