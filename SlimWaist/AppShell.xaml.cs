using SlimWaist.ViewModels;
using SlimWaist.Views;

namespace SlimWaist
{
    public partial class AppShell : Shell
    {
        private readonly AppShellVM _appShellVM;

        public AppShell(AppShellVM appShellVM)
        {
            InitializeComponent();

            _appShellVM = appShellVM;

            BindingContext = _appShellVM;

            RegisterRoutes();

            Routing.RegisterRoute(nameof(RegisterPage), typeof(RegisterPage));

        }
        private readonly static Type[] _routablePageTypes =
        [
            typeof(RegisterPage),
            typeof(SignUpPage),
            typeof(HomePage),
            typeof(RegimesListPage),
            typeof(FoodsPage),
            typeof(ItemPage),
            typeof(CartPage),
            typeof(MealDetailPage),
            typeof(DayMealsPage),
            typeof(AddFoodPage),
            typeof(DietsPage),
            typeof(SettingPage),
            typeof(ProfilePage),
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
