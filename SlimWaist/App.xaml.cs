using Microsoft.Maui.Controls.PlatformConfiguration;
using SlimWaist.Extentions;
using SlimWaist.Models;
using SlimWaist.Models.Dto;
using SlimWaist.Views;
using System.Globalization;

namespace SlimWaist
{
    public partial class App : Application
    {
        public static Setting setting;

        //??
        public static DataContext _dataContext;
        public static bool IsFromTablesPage { get; set; } = false;

        //Validation Messages
        public static string ValidateForNullOrEmptyMessage;
        public static string ValidateForIntegerNumberMessage;
        public static string ValidateForDecimalNumberMessage;
        public static string ValidateForEmailFormatMessage;
        public static string ValidateForFirebasePasswordMessage;

        //Dto
        public static List<BodyActivity> BodyActivities;
        public static List<Gender> Genders;
        public static List<Diet> Diets;
        public static List<ObesityDegree> obesityDegrees;
        public static List<WaistCircumference> waistCircumferences;
        public static List<MealType> mealTypes;

        public App(DataContext dataContext)
        {
            InitializeComponent();

            _dataContext = dataContext;

            InitializeDatabase();


            if (App.setting.CultureInfo == "ar-SA")
            {
                CultureInfo.CurrentCulture = new CultureInfo("ar-SA");

                Translator.instance.CultureInfo = new CultureInfo("ar-SA");

                ChangeDirections.instance.FlowDirection = FlowDirection.RightToLeft;

                ValidateForNullOrEmptyMessage = "ادخل القيمة";

                ValidateForIntegerNumberMessage = "ادخل رقم صحيح";

                ValidateForDecimalNumberMessage = "ادخل رقم صحيح او عشري واحد فقط";

                ValidateForEmailFormatMessage="ادخل ايميل صالح ";

                ValidateForFirebasePasswordMessage = "كلمة المرور يجب ان تكون 6 رموز علي الاقل";
                mealTypes = new List<MealType>()
                {
                    new MealType()
                    {
                        MealTypeId=0,
                        MealTypeName="إفطار"
                    },
                    new MealType()
                    {
                        MealTypeId=1,
                        MealTypeName="غداء"
                    },
                    new MealType()
                    {
                        MealTypeId=2,
                        MealTypeName="عشاء"
                    },
                    new MealType()
                    {
                        MealTypeId=3,
                        MealTypeName="سناكس"
                    },
                };
                Diets = new List<Diet>()
                {
                    new Diet()
                    {
                        DietId=1,
                        DietName="كيتو"
                    },
                    new Diet()
                    {
                        DietId=2,
                        DietName="منخفض السعرات"
                    },
                    new Diet()
                    {
                        DietId=3,
                        DietName="حصص غذائية"
                    }
                };
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

                obesityDegrees = new List<ObesityDegree>()
                    {
                        new ObesityDegree()
                        {
                            ObesityDegreeId = 1,
                            ObesityDegreeName="وزن طبيعي"
                        },
                        new ObesityDegree()
                        {
                            ObesityDegreeId=2,
                            ObesityDegreeName="زيادة وزن"
                        },
                        new ObesityDegree()
                        {
                            ObesityDegreeId=3,
                            ObesityDegreeName="سمنة درجة أولي"
                        },
                        new ObesityDegree()
                        {
                            ObesityDegreeId=4,
                            ObesityDegreeName="سمنة درجة ثانية"
                        },
                        new ObesityDegree()
                        {
                            ObesityDegreeId=5,
                            ObesityDegreeName="سمنة درجة ثالثة"
                        }
                };

                waistCircumferences = new List<WaistCircumference>()
                    {
                        new WaistCircumference()
                        {
                            WaistCircumferenceId=1,
                            WaistCircumferenceName="مقبول"
                        },
                        new WaistCircumference()
                        {
                            WaistCircumferenceId=2,
                            WaistCircumferenceName="خطر"
                        },
                        new WaistCircumference()
                        {
                            WaistCircumferenceId=3,
                            WaistCircumferenceName="خطرعالي"
                        },
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

                ValidateForEmailFormatMessage = "Invalid Email format";

                ValidateForFirebasePasswordMessage = "Password must be at least 6 characters";

                mealTypes = new List<MealType>()
                {
                    new MealType()
                    {
                        MealTypeId=0,
                        MealTypeName="Breakfast"
                    },
                    new MealType()
                    {
                        MealTypeId=1,
                        MealTypeName="Lunch"
                    },
                    new MealType()
                    {
                        MealTypeId=2,
                        MealTypeName="Dinner"
                    },
                    new MealType()
                    {
                        MealTypeId=3,
                        MealTypeName="Snaks"
                    },
                };

                Diets = new List<Diet>()
                {
                    new Diet()
                    {
                        DietId=1,
                        DietName="Keto"
                    },
                    new Diet()
                    {
                        DietId=2,
                        DietName="Low calories"
                    },
                    new Diet()
                    {
                        DietId=3,
                        DietName="Serving sizes"
                    }
                };

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
                            GenderId=0,
                            GenderName="Male"
                        },
                        new Gender()
                        {
                            GenderId=1,
                            GenderName="Female"
                        }
                    };

                obesityDegrees = new List<ObesityDegree>()
                    {
                        new ObesityDegree()
                        {
                            ObesityDegreeId = 1,
                            ObesityDegreeName="Normal weight"
                        },
                        new ObesityDegree()
                        {
                            ObesityDegreeId=2,
                            ObesityDegreeName="Over weight"
                        },
                        new ObesityDegree()
                        {
                            ObesityDegreeId=3,
                            ObesityDegreeName="Obese degree 1"
                        },
                        new ObesityDegree()
                        {
                            ObesityDegreeId=4,
                            ObesityDegreeName="Obese degree 2"
                        },
                        new ObesityDegree()
                        {
                            ObesityDegreeId=5,
                            ObesityDegreeName="Obese degree 3"
                        }
                };

                waistCircumferences = new List<WaistCircumference>()
                    {
                        new WaistCircumference()
                        {
                            WaistCircumferenceId=1,
                            WaistCircumferenceName="Accepted"
                        },
                        new WaistCircumference()
                        {
                            WaistCircumferenceId=2,
                            WaistCircumferenceName="Low risk"
                        },
                        new WaistCircumference()
                        {
                            WaistCircumferenceId=3,
                            WaistCircumferenceName="High risk"
                        },
                    };

            }

            MainPage = new AppShell();


            //myValidations.ReadExcel.ReadExcelSheet();
        }

        private async Task InitializeDatabase()
        {
            try
            {
                App.setting = _dataContext.Database.Table<Setting>().FirstOrDefault();
                
                if (App.setting == null)
                {
                    await _dataContext.init();
                }
            }
            catch (Exception)
            {
                await _dataContext.init();
            }
            finally
            {
                App.setting = _dataContext.Database.Table<Setting>().FirstOrDefault();
            }

        }


    }
}
