using SlimWaist.Models;
using SlimWaist.ViewModels;
using SlimWaist.Views;
using System.Globalization;

namespace SlimWaist
{
    public partial class AppShell : Shell
    {
        private readonly DataContext _dataContext;
        private List<Membership> memberships=new List<Membership>();
        private Membership membership;
        private Setting setting;
        private List<Setting> settings=new List<Setting>();

        public AppShell(DataContext dataContext)
        {
            InitializeComponent();

            RegisterRoutes();

            _dataContext = dataContext;

        }

        //protected async override void OnAppearing()
        //{
        //    base.OnAppearing();

        //    DataContext.memberships = await _dataContext.LoadAsync<Membership>();

        //    DataContext.settings = await _dataContext.LoadAsync<Setting>();

        //    DataContext.setting = DataContext.settings.Where(x => x.Id == 1).FirstOrDefault();

        //    DataContext.membership = DataContext.memberships.Where(x => x.Id ==DataContext.setting.CurrentMemberShipId).FirstOrDefault();
            
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
