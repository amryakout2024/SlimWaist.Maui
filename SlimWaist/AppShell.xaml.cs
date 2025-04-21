using SlimWaist.Models;
using SlimWaist.ViewModels;
using SlimWaist.Views;
using System.Globalization;

namespace SlimWaist
{
    public partial class AppShell : Shell
    {
        private readonly DataContext _dataContext;
        private List<Membership> memberships;
        private Membership membership;
        private Setting setting;
        private List<Setting> settings;

        public AppShell(DataContext dataContext)
        {
            InitializeComponent();

            RegisterRoutes();

            _dataContext = dataContext;

            //Dispatcher.DispatchAsync(async () =>
            //{
            //    memberships = await _dataContext.LoadAsync<Membership>();

            //    settings = await _dataContext.LoadAsync<Setting>();

            //    setting = settings.Where(x => x.Id == 1).FirstOrDefault();

            //    membership = memberships.Where(x => x.Id == setting.CurrentMemberShipId).FirstOrDefault();

            //    if (membership.CultureInfo == "ar-SA")
            //    {
            //        this.FlowDirection = FlowDirection.RightToLeft;
            //    }
            //    else
            //    {
            //        this.FlowDirection = FlowDirection.LeftToRight;
            //    }

            //});


            //Routing.RegisterRoute(nameof(RegisterPage), typeof(RegisterPage));

        }
        //protected async override void OnAppearing()
        //{
        //    base.OnAppearing();

        //    DataContext.memberships = await _dataContext.LoadAsync<Membership>();

        //    DataContext.settings = await _dataContext.LoadAsync<Setting>();

        //    DataContext.setting = settings.Where(x => x.Id == 1).FirstOrDefault();

        //    DataContext.membership = memberships.Where(x => x.Id == setting.CurrentMemberShipId).FirstOrDefault();

        //    if (DataContext.membership.CultureInfo == "ar-SA")
        //    {
        //        this.FlowDirection = FlowDirection.RightToLeft;
        //    }
        //    else
        //    {
        //        this.FlowDirection = FlowDirection.LeftToRight;
        //    }

        //}
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
