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

            MainPage = new AppShell(_dataContext);

            //Helpers.ReadExcel.ReadExcelSheet();
        }



    }
}
