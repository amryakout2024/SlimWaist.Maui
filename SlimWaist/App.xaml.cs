using SlimWaist.Models;

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
