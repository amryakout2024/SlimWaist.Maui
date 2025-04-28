using CommunityToolkit.Maui;
using SlimWaist.Models;
using SlimWaist.ViewModels;
using SlimWaist.Views;
using InputKit.Handlers;
using Microsoft.Extensions.Logging;
using UraniumUI;
using Microsoft.Maui.Hosting;
using CommunityToolkit.Maui.ApplicationModel;
using SlimWaist.Helpers;
using SlimWaist.Models;

using SlimWaist;
using SlimWaist.Extentions;




#if ANDROID
using System.Net.Security;
using Xamarin.Android.Net;
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
                .UseSentry(options => 
                {
                    options.Dsn = "https://cf56e869a0b99378014d4959f18b6619@o4509224782528512.ingest.de.sentry.io/4509224784822352";
                })
                .ConfigureMauiHandlers(handlers =>
                {
                    handlers.AddInputKitHandlers();
                })
                .ConfigureMauiHandlers(h =>
                {
#if ANDROID
                                //h.AddHandler<Shell, TabbarBadgeRenderer>();
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
            //builder.Services.AddSingleton<IBadge>(Badge.Default);
            builder.Services.AddSingleton<LoginPage>()
                            .AddSingleton<LoginVM>();

            builder.Services.AddSingleton<RegisterPage>()
                            .AddSingleton<RegisterVM>();

            builder.Services.AddSingleton<HomePage>()
                            .AddSingleton<HomeVM>();

            builder.Services.AddSingleton<RegimesListPage>()
                            .AddSingleton<RegimesListVM>();

            builder.Services.AddSingleton<SignUpPage>()
                            .AddSingleton<SignUpVM>();

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

            builder.Services.AddSingleton<ItemPage>()
                           .AddSingleton<ItemVM>();

            builder.Services.AddSingleton<AllMealsPage>()
                            .AddSingleton<AllMealsVM>();

            builder.Services.AddSingleton<MealDetailPage>()
                            .AddSingleton<MealDetailVM>();

            builder.Services.AddSingleton<DayMealsPage>()
                            .AddSingleton<DayMealsVM>();

            builder.Services.AddSingleton<SettingPage>()
                            .AddSingleton<SettingVM>();

            builder.Services.AddSingleton<ProfilePage>()
                            .AddSingleton<ProfileVM>();

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
