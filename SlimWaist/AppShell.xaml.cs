using SlimWaist.Extentions;
using SlimWaist.Languages;
using SlimWaist.Models;
using SlimWaist.ViewModels;
using SlimWaist.Views;
using System.Globalization;

namespace SlimWaist
{
    public partial class AppShell : Shell
    {
        private readonly DataContext _dataContext;

        public static Setting setting = new Setting();

        public static string ValidateForNullOrEmptyMessage;

        public AppShell(DataContext dataContext)
        {
            InitializeComponent();

            _dataContext = dataContext;

            RegisterRoutes();

            ////SentrySdk.CaptureMessage("Hello Sentry");
        }

        protected async override void OnAppearing()
        {
            base.OnAppearing();

            await InitializeDatabase();

            setting = _dataContext.Database.Table<Setting>().FirstOrDefault();
            
            //var memberships = _dataContext.Database.Table<Membership>().ToList();
            
            //var membership = memberships.Where(x => x.Id == setting.CurrentMemberShipId).FirstOrDefault();

            if (setting != null)
            {
                if (setting.CultureInfo == "ar-SA")
                {
                    CultureInfo.CurrentCulture = new CultureInfo("ar-SA");

                    Translator.instance.CultureInfo = new CultureInfo("ar-SA");

                    ChangeDirections.instance.FlowDirection = FlowDirection.RightToLeft;

                    ValidateForNullOrEmptyMessage = "ادخل القيمة";
                }
                else
                {
                    CultureInfo.CurrentCulture = new CultureInfo("en-US");

                    Translator.instance.CultureInfo = new CultureInfo("en-US");
                    
                    ChangeDirections.instance.FlowDirection = FlowDirection.LeftToRight;

                    ValidateForNullOrEmptyMessage = "Enter Value";
                }
            }

        }

        private async Task InitializeDatabase()
        {
            try
            {
                var BodyActivityTest = _dataContext.Database.Table<BodyActivity>().FirstOrDefault();

                if (BodyActivityTest == null)
                {
                    await _dataContext.init();
                }
            }
            catch (Exception)
            {
                await _dataContext.init();
            }

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
