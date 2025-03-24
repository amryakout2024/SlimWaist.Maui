using SlimWaist.Models;
using SlimWaist.ViewModels;
using SlimWaist.Views;

namespace SlimWaist
{
    public partial class App : Application
    {
        private readonly AppShellVM _appShellVM;

        private readonly DataContext _dataContext;
        //private readonly TabbedFoodsVM _tabbedFoodsVM;

        public App(AppShellVM appShellVM, DataContext dataContext)
        {
            InitializeComponent();

            _appShellVM = appShellVM;

            _dataContext = dataContext;

            MainPage = new AppShell(_appShellVM);

            InitializeDatabase();

            //Helpers.ReadExcel.ReadExcelSheet();

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
            catch (Exception e)
            {
                await _dataContext.init();
            }

        }


    }
}
