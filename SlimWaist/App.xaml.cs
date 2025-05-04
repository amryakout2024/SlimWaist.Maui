using Microsoft.Maui.Controls.PlatformConfiguration;
using SlimWaist.Extentions;
using SlimWaist.Models;
using System.Globalization;

namespace SlimWaist
{
    public partial class App : Application
    {
        private readonly DataContext _dataContext;
        public static Setting setting;
        public static string ValidateForNullOrEmptyMessage;
        public static string ValidateForIntegerNumberMessage;
        public static string ValidateForDecimalNumberMessage;
        public static List<BodyActivity> BodyActivities;
        public static List<Gender> Genders;

        public App(DataContext dataContext)
        {
            InitializeComponent();

            _dataContext = dataContext;

            InitializeDatabase();

            setting = _dataContext.Database.Table<Setting>().FirstOrDefault();

            if (setting != null)
            {
                if (setting.CultureInfo == "ar-SA")
                {
                    CultureInfo.CurrentCulture = new CultureInfo("ar-SA");

                    Translator.instance.CultureInfo = new CultureInfo("ar-SA");

                    ChangeDirections.instance.FlowDirection = FlowDirection.RightToLeft;

                    ValidateForNullOrEmptyMessage = "ادخل القيمة";

                    ValidateForIntegerNumberMessage = "ادخل رقم صحيح";

                    ValidateForDecimalNumberMessage = "ادخل رقم صحيح او عشري واحد فقط";

                    BodyActivities = new List<BodyActivity>() {

                        new BodyActivity()
                        {
                            BodyActivityId=1,
                            BodyActivityName="خامل"
                        },
                        new BodyActivity()
                        {
                            BodyActivityId=2,
                            BodyActivityName="منخفض النشاط"
                        },
                        new BodyActivity()
                        {
                            BodyActivityId=3,
                            BodyActivityName="نشط"
                        },
                        new BodyActivity()
                        {
                            BodyActivityId=4,
                            BodyActivityName="نشط جدا"
                        },
                    };

                    Genders = new List<Gender>()
                    {
                        new Gender()
                        {
                            GenderId=1,
                            GenderName="ذكر"
                        },
                        new Gender()
                        {
                            GenderId=2,
                            GenderName="أنثي"
                        }
                    };
                }
                else
                {
                    CultureInfo.CurrentCulture = new CultureInfo("en-US");

                    Translator.instance.CultureInfo = new CultureInfo("en-US");

                    ChangeDirections.instance.FlowDirection = FlowDirection.LeftToRight;

                    ValidateForNullOrEmptyMessage = "Enter Value";

                    ValidateForIntegerNumberMessage = "Enter integer number";

                    ValidateForDecimalNumberMessage = "Enter integer number or one decimal only";

                    BodyActivities = new List<BodyActivity>() {

                        new BodyActivity()
                        {
                            BodyActivityId=1,
                            BodyActivityName="Inactive"
                        },
                        new BodyActivity()
                        {
                            BodyActivityId=2,
                            BodyActivityName="Lightly  active"
                        },
                        new BodyActivity()
                        {
                            BodyActivityId=3,
                            BodyActivityName="Moderately  Active"
                        },
                        new BodyActivity()
                        {
                            BodyActivityId=4,
                            BodyActivityName="Very active"
                        },
                    };
                   
                    Genders = new List<Gender>()
                    {
                        new Gender()
                        {
                            GenderId=1,
                            GenderName="Male"
                        },
                        new Gender()
                        {
                            GenderId=2,
                            GenderName="Female"
                        }
                    };

                }
            }

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
            catch (Exception)
            {
                await _dataContext.init();
            }

        }


    }
}
