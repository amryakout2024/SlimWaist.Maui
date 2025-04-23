using SlimWaist.Extentions;
using SlimWaist.Models;
using SlimWaist.ViewModels;
using SlimWaist.Views;
using System.Globalization;

namespace SlimWaist
{
    public partial class App : Application
    {
        private readonly DataContext _dataContext;

        public App(DataContext dataContext)
        {
            InitializeComponent();

            _dataContext = dataContext;

            InitializeDatabase();

            //_dataContext.LoadMemebershipAndSetting();

            MainPage = new AppShell(_dataContext);

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
