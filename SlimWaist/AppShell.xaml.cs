using SlimWaist.Extentions;
using SlimWaist.Models;
using SlimWaist.Views;
using System.Globalization;

namespace SlimWaist
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();

            RegisterRoutes();


            ////SentrySdk.CaptureMessage("Hello Sentry");
        }

        private readonly static Type[] _routablePageTypes =
        [
            typeof(RegisterPage),
            typeof(HomePage),
            typeof(FoodsPage),
            typeof(ItemPage),
            typeof(CartPage),
            typeof(MealDetailPage),
            typeof(DayMealsPage),
            typeof(AddFoodPage),
            typeof(DietsPage),
            typeof(SettingPage),
            typeof(ProfilePage),
            typeof(BodyMassAnalysisPage),
            typeof(MealPage),
            typeof(ReadyMealPage),
        ];

        private static void RegisterRoutes()
        {
            foreach (var pageType in _routablePageTypes)
            {
                Routing.RegisterRoute(pageType.Name, pageType);
            }
        }

    }
}
