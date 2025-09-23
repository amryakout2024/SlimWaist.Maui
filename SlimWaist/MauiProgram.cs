using CommunityToolkit.Maui;
using CommunityToolkit.Maui.ApplicationModel;
using InputKit.Handlers;
using Microcharts.Maui;
using Microsoft.Extensions.Logging;
using SkiaSharp.Views.Maui.Controls.Hosting;
using SlimWaist.Extentions;
using SlimWaist.Models;
using SlimWaist.Validations;
using SlimWaist.ViewModels;
using SlimWaist.Views;
using UraniumUI;
using Firebase.Auth;
using Firebase.Auth.Providers;
using Firebase.Auth.Repository;
using Firebase.Database;


#if ANDROID
using System.Net.Security;
using Xamarin.Android.Net;
using SlimWaist.Platforms.Android;
#elif IOS
using Security;
#endif

namespace SlimWaist
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .UseUraniumUI()
                .UseMauiCommunityToolkit()
                .UseUraniumUIMaterial()
                .UseMicrocharts()
                .UseSkiaSharp()
                .ConfigureMauiHandlers(handlers =>
                {
                    handlers.AddInputKitHandlers();
                })
                .ConfigureMauiHandlers ( h =>
                {
#if ANDROID
                    h.AddHandler<Shell, TabbarBadgeRenderer>();
#endif
                })
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                });

#if DEBUG
            builder.Logging.AddDebug();
#endif
            // ,UserRepository=new FileUserRepository("SlimWaist") to save the user offline in local repository

            builder.Services.AddSingleton(new FirebaseClient("https://calfit-weightloss-default-rtdb.firebaseio.com/"));

            builder.Services.AddSingleton(new FirebaseAuthClient(new FirebaseAuthConfig
            {
                ApiKey = "AIzaSyBS4rcRRgwKq_gmjiQ1M0e8A96PvR9vFH8",
                AuthDomain = "calfit-weightloss.firebaseapp.com",
                Providers = new FirebaseAuthProvider[]
                {
                    new EmailProvider()
                }
            }));


            builder.Services.AddTransient<LoginPage>()
                            .AddTransient<LoginVM>();

            builder.Services.AddTransient<RegisterPage>()
                            .AddTransient<RegisterVM>();

            builder.Services.AddSingleton<HomePage>()
                            .AddSingleton<HomeVM>();

            builder.Services.AddSingleton<DietsPage>()
                            .AddSingleton<DietsVM>();

            //builder.Services.AddSingleton<SlimWaistDayPage>()
            //                .AddSingleton<SlimWaistDayVM>();

            builder.Services.AddSingleton<FoodsPage>()
                            .AddSingleton<FoodsVM>();

            builder.Services.AddSingleton<AddFoodPage>()
                            .AddSingleton<AddFoodVM>();

            builder.Services.AddSingleton<CartPage>()
                            .AddSingleton<CartVM>();

            builder.Services.AddSingleton<BodyMassAnalysisPage>()
                            .AddSingleton<BodyMassAnalysisVM>();

            builder.Services.AddSingleton<ItemPage>()
                           .AddSingleton<ItemVM>();

            builder.Services.AddSingleton<AllMealsPage>()
                            .AddSingleton<AllMealsVM>();

            builder.Services.AddSingleton<TablesPage>()
                            .AddSingleton<TablesVM>();

            builder.Services.AddSingleton<MealDetailPage>()
                            .AddSingleton<MealDetailVM>();

            builder.Services.AddSingleton<DayMealsPage>()
                            .AddSingleton<DayMealsVM>();

            builder.Services.AddSingleton<SettingPage>()
                            .AddSingleton<SettingVM>();

            builder.Services.AddSingleton<ProfilePage>()
                            .AddSingleton<ProfileVM>();

            builder.Services.AddSingleton<MealPage>()
                            .AddSingleton<MealVM>();

            builder.Services.AddSingleton<ReadyMealPage>()
                            .AddSingleton<ReadyMealVM>();

            //builder.Services.AddSingleton<TabbarBadgeRenderer>();
            builder.Services.AddSingleton<DataContext>();
            builder.Services.AddSingleton<Setting>();
            builder.Services.AddSingleton<ChangeDirections>();
            builder.Services.AddSingleton<FlowDirectionsExtention>();
            //builder.Services.AddTransient<PopupEntry,CartVM>();
            builder.Services.AddSingleton<AppShellVM>();
            builder.Services.AddSingleton<ValidateForIntegerNumber>();

            return builder.Build();
        }
    }
}
//.ConfigureMopups()
//builder.Services.AddMopupsDialogs();
                //.UseSentry(options =>
                // {
                //     options.Dsn = "https://cf56e869a0b99378014d4959f18b6619@o4509224782528512.ingest.de.sentry.io/4509224784822352";
                // })
