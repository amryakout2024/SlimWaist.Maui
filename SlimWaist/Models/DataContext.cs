using SlimWaist.Models.Dto;
using SQLite;
using System.Resources;

namespace SlimWaist.Models
{
    public class DataContext
    {
        public const string DbName = "SlimWaist81";

        public static string DbPath = Path.Combine(FileSystem.Current.AppDataDirectory, DbName);

        public SQLiteConnection Database =
            new SQLiteConnection(
            DbPath, SQLiteOpenFlags.Create |
            SQLiteOpenFlags.ReadWrite |
            SQLiteOpenFlags.SharedCache);

        public async Task init()
        {
            try
            {
                //create tables

                Database.CreateTable<Membership>();

                Database.CreateTable<Setting>();

                Database.CreateTable<Food>();

                Database.CreateTable<CartItem>();

                Database.CreateTable<Meal>();

                Database.CreateTable<MealDetail>();
                
                Database.CreateTable<DayDiet>();

                ///////inserting data////////

                Database.Insert(new Membership()
                {
                    Id= 1,
                    Name = "عمرو ياقوت",
                    Email = "amrnewstory@gmail.com",
                    Password = "1",
                    Height = 180,
                    Weight = 90,
                    WeightDate=DateTime.Now,
                    BirthDate= new DateTime(1990, 8, 19),
                    GenderId = 1,
                    BodyActivityId=2,
                    WaistCircumferenceMeasurement=100,
                    CultureInfo = "en-US",
                    IsExistsInDb=true
                });

                Database.Insert(new Setting() 
                { 
                   Id = 1,
                });


                List<Food> foods = new List<Food>()
                {
                    new Food{FoodId=1,FoodCategory="الفواكة",FoodName="اناناس",FoodCalories=55,FoodProtien=0.5,FoodFat=0.2,FoodFibers=0.5,FoodCarb=12.7},
new Food{FoodId=2,FoodCategory="الفواكة",FoodName="برقوق",FoodCalories=50,FoodProtien=0.7,FoodFat=0.2,FoodFibers=0.6,FoodCarb=11.3},
new Food{FoodId=3,FoodCategory="الفواكة",FoodName="رمان",FoodCalories=68,FoodProtien=0.7,FoodFat=0.4,FoodFibers=2.1,FoodCarb=15.5},
new Food{FoodId=4,FoodCategory="الفواكة",FoodName="تين شوكى",FoodCalories=31,FoodProtien=1,FoodFat=0.9,FoodFibers=6.5,FoodCarb=4.8},
new Food{FoodId=5,FoodCategory="الفواكة",FoodName="قراصيا",FoodCalories=299,FoodProtien=2.3,FoodFat=0.5,FoodFibers=2.5,FoodCarb=71.2},
new Food{FoodId=6,FoodCategory="الفواكة",FoodName="زبيب",FoodCalories=320,FoodProtien=2.4,FoodFat=0.3,FoodFibers=1.6,FoodCarb=76.9},
new Food{FoodId=7,FoodCategory="الفواكة",FoodName="توت عليق راسبري",FoodCalories=60,FoodProtien=1.3,FoodFat=0.5,FoodFibers=1.5,FoodCarb=12.5},
new Food{FoodId=8,FoodCategory="الفواكة",FoodName="فراولة",FoodCalories=34,FoodProtien=0.8,FoodFat=0.4,FoodFibers=1.2,FoodCarb=6.7},
new Food{FoodId=9,FoodCategory="الفواكة",FoodName="جميز",FoodCalories=66,FoodProtien=0.9,FoodFat=0.3,FoodFibers=0.5,FoodCarb=15},
new Food{FoodId=10,FoodCategory="الفواكة",FoodName="بطيخ",FoodCalories=26,FoodProtien=0.4,FoodFat=0.1,FoodFibers=0.4,FoodCarb=5.9},
new Food{FoodId=11,FoodCategory="الفواكة",FoodName="تفاح",FoodCalories=57,FoodProtien=0.4,FoodFat=0.2,FoodFibers=0.8,FoodCarb=13.5},
new Food{FoodId=12,FoodCategory="الفواكة",FoodName="مشمش",FoodCalories=58,FoodProtien=0.9,FoodFat=0.4,FoodFibers=0.7,FoodCarb=12.7},
new Food{FoodId=13,FoodCategory="الفواكة",FoodName="مشمشيه",FoodCalories=282,FoodProtien=4.5,FoodFat=0.6,FoodFibers=3.2,FoodCarb=64.6},
new Food{FoodId=14,FoodCategory="الفواكة",FoodName="أفوكادو",FoodCalories=165,FoodProtien=2.2,FoodFat=14,FoodFibers=1.5,FoodCarb=7.5},
new Food{FoodId=15,FoodCategory="الفواكة",FoodName="موز",FoodCalories=95,FoodProtien=1.3,FoodFat=0.3,FoodFibers=0.6,FoodCarb=21.7},
new Food{FoodId=16,FoodCategory="الفواكة",FoodName="كانتلوب",FoodCalories=33,FoodProtien=0.8,FoodFat=0.1,FoodFibers=0.4,FoodCarb=7.3},
new Food{FoodId=17,FoodCategory="الفواكة",FoodName="قشطه فاكهه",FoodCalories=85,FoodProtien=2.9,FoodFat=0.5,FoodFibers=1.4,FoodCarb=17.3},
new Food{FoodId=18,FoodCategory="الفواكة",FoodName="بلح",FoodCalories=115,FoodProtien=0.9,FoodFat=0.2,FoodFibers=0.9,FoodCarb=27.3},
new Food{FoodId=19,FoodCategory="الفواكة",FoodName="عجوه",FoodCalories=315,FoodProtien=2.3,FoodFat=0.5,FoodFibers=2.2,FoodCarb=75.3},
new Food{FoodId=20,FoodCategory="الفواكة",FoodName="تين ",FoodCalories=72,FoodProtien=1.3,FoodFat=0.4,FoodFibers=1.5,FoodCarb=15.8},
new Food{FoodId=21,FoodCategory="الفواكة",FoodName="تينيه",FoodCalories=293,FoodProtien=4.1,FoodFat=1.1,FoodFibers=5.7,FoodCarb=66.6},
new Food{FoodId=22,FoodCategory="الفواكة",FoodName="جريب فروت ",FoodCalories=43,FoodProtien=0.6,FoodFat=0.1,FoodFibers=0.4,FoodCarb=9.8},
new Food{FoodId=23,FoodCategory="الفواكة",FoodName="عنب بناتي",FoodCalories=77,FoodProtien=0.6,FoodFat=0.6,FoodFibers=0.7,FoodCarb=17.2},
new Food{FoodId=24,FoodCategory="الفواكة",FoodName="جوافه",FoodCalories=62,FoodProtien=0.8,FoodFat=0.5,FoodFibers=3.4,FoodCarb=13.5},
new Food{FoodId=25,FoodCategory="الفواكة",FoodName="حرنكش",FoodCalories=75,FoodProtien=2.7,FoodFat=0.3,FoodFibers=1,FoodCarb=15.3},
new Food{FoodId=26,FoodCategory="الفواكة",FoodName="نبق",FoodCalories=89,FoodProtien=1.4,FoodFat=0.2,FoodFibers=1.4,FoodCarb=20.3},
new Food{FoodId=27,FoodCategory="الفواكة",FoodName="كيوي",FoodCalories=54,FoodProtien=1.9,FoodFat=0.6,FoodFibers=1.6,FoodCarb=10.2},
new Food{FoodId=28,FoodCategory="الفواكة",FoodName="ليمون أضاليا",FoodCalories=32,FoodProtien=0.8,FoodFat=0.2,FoodFibers=0.6,FoodCarb=6.7},
new Food{FoodId=29,FoodCategory="الفواكة",FoodName="ليمون حلو",FoodCalories=37,FoodProtien=0.6,FoodFat=0.5,FoodFibers=0.4,FoodCarb=7.6},
new Food{FoodId=30,FoodCategory="الفواكة",FoodName="ليمون بلدي",FoodCalories=33,FoodProtien=0.6,FoodFat=0.3,FoodFibers=0.7,FoodCarb=6.9},
new Food{FoodId=31,FoodCategory="الفواكة",FoodName="بشمله",FoodCalories=51,FoodProtien=0.3,FoodFat=0.6,FoodFibers=0.7,FoodCarb=11},
new Food{FoodId=32,FoodCategory="الفواكة",FoodName="يوسفي",FoodCalories=48,FoodProtien=0.7,FoodFat=0.2,FoodFibers=0.9,FoodCarb=10.8},
new Food{FoodId=33,FoodCategory="الفواكة",FoodName="مانجو",FoodCalories=68,FoodProtien=0.8,FoodFat=0.3,FoodFibers=1.1,FoodCarb=15.5},
new Food{FoodId=34,FoodCategory="الفواكة",FoodName="شمام",FoodCalories=30,FoodProtien=0.8,FoodFat=0.2,FoodFibers=0.6,FoodCarb=6.3},
new Food{FoodId=35,FoodCategory="الفواكة",FoodName="توت",FoodCalories=76,FoodProtien=1.6,FoodFat=0.5,FoodFibers=1.2,FoodCarb=16.3},
new Food{FoodId=36,FoodCategory="الفواكة",FoodName="برتقال",FoodCalories=56,FoodProtien=1.1,FoodFat=0.3,FoodFibers=0.6,FoodCarb=12.1},
new Food{FoodId=37,FoodCategory="الفواكة",FoodName="باباز",FoodCalories=44,FoodProtien=0.6,FoodFat=0.2,FoodFibers=0.6,FoodCarb=10},
new Food{FoodId=38,FoodCategory="الفواكة",FoodName="خوخ",FoodCalories=51,FoodProtien=0.7,FoodFat=0.2,FoodFibers=0.7,FoodCarb=11.7},
new Food{FoodId=39,FoodCategory="الفواكة",FoodName="كمثري",FoodCalories=58,FoodProtien=0.3,FoodFat=0.2,FoodFibers=1.3,FoodCarb=13.7},
new Food{FoodId=40,FoodCategory="الفواكة",FoodName="كاكا",FoodCalories=85,FoodProtien=0.9,FoodFat=0.5,FoodFibers=1,FoodCarb=19.3},
new Food{FoodId=41,FoodCategory="لحوم",FoodName="بسطرمة بالغلاف",FoodCalories=201,FoodProtien=28.6,FoodFat=5.8,FoodFibers=1.6,FoodCarb=8.5},
new Food{FoodId=42,FoodCategory="لحوم",FoodName="لحم بسطرمة ",FoodCalories=156,FoodProtien=29.9,FoodFat=4,FoodFibers=0,FoodCarb=0},
new Food{FoodId=43,FoodCategory="لحوم",FoodName="مخ بقرى نئ",FoodCalories=121,FoodProtien=10.4,FoodFat=8.5,FoodFibers=0,FoodCarb=0.6},
new Food{FoodId=44,FoodCategory="لحوم",FoodName="مخ بقرى مقلى ",FoodCalories=251,FoodProtien=13.7,FoodFat=13.9,FoodFibers=0,FoodCarb=17.7},
new Food{FoodId=45,FoodCategory="لحوم",FoodName="برجر بقرى نئ",FoodCalories=256,FoodProtien=18,FoodFat=20,FoodFibers=0,FoodCarb=1},
new Food{FoodId=46,FoodCategory="لحوم",FoodName="برجر بقرى مشوى ",FoodCalories=263,FoodProtien=21.1,FoodFat=19,FoodFibers=0,FoodCarb=2},
new Food{FoodId=47,FoodCategory="لحوم",FoodName="فرانكفورت بقرى ",FoodCalories=224,FoodProtien=15.2,FoodFat=16.1,FoodFibers=1,FoodCarb=4.6},
new Food{FoodId=48,FoodCategory="لحوم",FoodName="لحم راس بقرى نئ",FoodCalories=212,FoodProtien=20.2,FoodFat=14.5,FoodFibers=0,FoodCarb=0.1},
new Food{FoodId=49,FoodCategory="لحوم",FoodName="لحم راس بقرى مسلوق",FoodCalories=239,FoodProtien=25.1,FoodFat=15.2,FoodFibers=0,FoodCarb=0.4},
new Food{FoodId=50,FoodCategory="لحوم",FoodName="قلب بقرى ",FoodCalories=112,FoodProtien=16.5,FoodFat=4.3,FoodFibers=0,FoodCarb=1.7},
new Food{FoodId=51,FoodCategory="لحوم",FoodName="كلاوى بقرى ",FoodCalories=121,FoodProtien=16.5,FoodFat=5.8,FoodFibers=0,FoodCarb=0.8},
new Food{FoodId=52,FoodCategory="لحوم",FoodName="كوارع بقرى ",FoodCalories=137,FoodProtien=23,FoodFat=5,FoodFibers=0,FoodCarb=0},
new Food{FoodId=53,FoodCategory="لحوم",FoodName="كبدة بقرى ",FoodCalories=132,FoodProtien=19.5,FoodFat=4,FoodFibers=0,FoodCarb=4.4},
new Food{FoodId=54,FoodCategory="لحوم",FoodName="لانشون بقرى ",FoodCalories=155,FoodProtien=16.3,FoodFat=7.5,FoodFibers=0.1,FoodCarb=5.6},
new Food{FoodId=55,FoodCategory="لحوم",FoodName="فشة بقرى ",FoodCalories=88,FoodProtien=17.1,FoodFat=2.2,FoodFibers=0,FoodCarb=0},
new Food{FoodId=56,FoodCategory="لحوم",FoodName="لحم بقرى ",FoodCalories=186,FoodProtien=19.6,FoodFat=11.9,FoodFibers=0,FoodCarb=0},
new Food{FoodId=57,FoodCategory="لحوم",FoodName="لحم بقرى مفروم ",FoodCalories=216,FoodProtien=18,FoodFat=16,FoodFibers=0,FoodCarb=0},
new Food{FoodId=58,FoodCategory="لحوم",FoodName="كفتة بقرى مقلية ",FoodCalories=245,FoodProtien=17.5,FoodFat=18.1,FoodFibers=0.2,FoodCarb=2.9},
new Food{FoodId=59,FoodCategory="لحوم",FoodName="كفتة بقرى مشوية ",FoodCalories=271,FoodProtien=21.2,FoodFat=18.2,FoodFibers=0,FoodCarb=5.5},
new Food{FoodId=60,FoodCategory="لحوم",FoodName="كفتة بقرى أرز ",FoodCalories=149,FoodProtien=10.6,FoodFat=7,FoodFibers=0.6,FoodCarb=11},
new Food{FoodId=61,FoodCategory="لحوم",FoodName="كبيبة مقلية ",FoodCalories=237,FoodProtien=11.5,FoodFat=8.8,FoodFibers=0.7,FoodCarb=28},
new Food{FoodId=62,FoodCategory="لحوم",FoodName="بيف بقرى ",FoodCalories=159,FoodProtien=14,FoodFat=11,FoodFibers=0.3,FoodCarb=0.9},
new Food{FoodId=63,FoodCategory="لحوم",FoodName="بولوبيف معلب",FoodCalories=213,FoodProtien=24.8,FoodFat=11.8,FoodFibers=0,FoodCarb=1.9},
new Food{FoodId=64,FoodCategory="لحوم",FoodName="سجق بقرى",FoodCalories=312,FoodProtien=15,FoodFat=24,FoodFibers=0,FoodCarb=9},
new Food{FoodId=65,FoodCategory="لحوم",FoodName="سجق بقرى مشوى",FoodCalories=291,FoodProtien=17,FoodFat=18.5,FoodFibers=0,FoodCarb=14},
new Food{FoodId=66,FoodCategory="لحوم",FoodName="طحال بقرى",FoodCalories=97,FoodProtien=18,FoodFat=2.1,FoodFibers=0,FoodCarb=1.5},
new Food{FoodId=67,FoodCategory="لحوم",FoodName="لسان بقرى ",FoodCalories=201,FoodProtien=16.8,FoodFat=14.6,FoodFibers=0,FoodCarb=0.6},
new Food{FoodId=68,FoodCategory="لحوم",FoodName="لسان بقرى مسلوق",FoodCalories=298,FoodProtien=23.4,FoodFat=22.1,FoodFibers=0,FoodCarb=1.3},
new Food{FoodId=69,FoodCategory="لحوم",FoodName="لحم جاموس ",FoodCalories=210,FoodProtien=18.8,FoodFat=15,FoodFibers=0,FoodCarb=0},
new Food{FoodId=70,FoodCategory="لحوم",FoodName="كبد جمل",FoodCalories=143,FoodProtien=20.7,FoodFat=4.4,FoodFibers=0,FoodCarb=5.2},
new Food{FoodId=71,FoodCategory="لحوم",FoodName="لحم جمل",FoodCalories=108,FoodProtien=19,FoodFat=3.6,FoodFibers=0,FoodCarb=0},
new Food{FoodId=72,FoodCategory="لحوم",FoodName="فرانكفورت دجاج ",FoodCalories=221,FoodProtien=18.1,FoodFat=15,FoodFibers=1.1,FoodCarb=3.3},
new Food{FoodId=73,FoodCategory="لحوم",FoodName="قوانص دجاج",FoodCalories=106,FoodProtien=20.5,FoodFat=2.3,FoodFibers=0,FoodCarb=0.7},
new Food{FoodId=74,FoodCategory="لحوم",FoodName="كبد دجاج",FoodCalories=122,FoodProtien=17.6,FoodFat=4.6,FoodFibers=0,FoodCarb=2.5},
new Food{FoodId=75,FoodCategory="لحوم",FoodName="لانشون دجاج ",FoodCalories=144,FoodProtien=18,FoodFat=5.5,FoodFibers=1.5,FoodCarb=5.5},
new Food{FoodId=76,FoodCategory="لحوم",FoodName="دجاج مسلوق",FoodCalories=193,FoodProtien=21.6,FoodFat=11.7,FoodFibers=0,FoodCarb=0.3},
new Food{FoodId=77,FoodCategory="لحوم",FoodName="دجاج مقلى ",FoodCalories=316,FoodProtien=22.4,FoodFat=22.2,FoodFibers=0,FoodCarb=6.6},
new Food{FoodId=78,FoodCategory="لحوم",FoodName="دجاج مشوى",FoodCalories=213,FoodProtien=21.2,FoodFat=13.8,FoodFibers=0,FoodCarb=0.9},
new Food{FoodId=79,FoodCategory="لحوم",FoodName="لحم اوراك دجاج (دارك)",FoodCalories=121,FoodProtien=19,FoodFat=5,FoodFibers=0,FoodCarb=0},
new Food{FoodId=80,FoodCategory="لحوم",FoodName="لحم صدور (لايت)",FoodCalories=109,FoodProtien=20.5,FoodFat=3,FoodFibers=0,FoodCarb=0},
new Food{FoodId=81,FoodCategory="لحوم",FoodName="لحم دجاج بالجلد",FoodCalories=214,FoodProtien=17.4,FoodFat=16,FoodFibers=0,FoodCarb=0},
new Food{FoodId=82,FoodCategory="لحوم",FoodName="لحم بط بالجلد",FoodCalories=315,FoodProtien=15.4,FoodFat=28.2,FoodFibers=0,FoodCarb=0},
new Food{FoodId=83,FoodCategory="لحوم",FoodName="لحم ماعز",FoodCalories=165,FoodProtien=18.4,FoodFat=10.2,FoodFibers=0,FoodCarb=0},
new Food{FoodId=84,FoodCategory="لحوم",FoodName="لحم اوز بالجلد",FoodCalories=346,FoodProtien=16,FoodFat=31.3,FoodFibers=0,FoodCarb=0},
new Food{FoodId=85,FoodCategory="لحوم",FoodName="كلاوى ضانى",FoodCalories=109,FoodProtien=16,FoodFat=5,FoodFibers=0,FoodCarb=0},
new Food{FoodId=86,FoodCategory="لحوم",FoodName="كبد ضانى ",FoodCalories=26,FoodProtien=19.9,FoodFat=2.8,FoodFibers=0,FoodCarb=5.2},
new Food{FoodId=87,FoodCategory="لحوم",FoodName="لحم ضاني",FoodCalories=249,FoodProtien=16.5,FoodFat=20.3,FoodFibers=0,FoodCarb=0},
new Food{FoodId=88,FoodCategory="لحوم",FoodName="كباب ضاني",FoodCalories=273,FoodProtien=23,FoodFat=19.3,FoodFibers=0,FoodCarb=1.8},
new Food{FoodId=89,FoodCategory="لحوم",FoodName="لحم حمام",FoodCalories=251,FoodProtien=20,FoodFat=19,FoodFibers=0,FoodCarb=0},
new Food{FoodId=90,FoodCategory="لحوم",FoodName="لحم ارانب",FoodCalories=133,FoodProtien=20.7,FoodFat=5.6,FoodFibers=0,FoodCarb=0},
new Food{FoodId=91,FoodCategory="لحوم",FoodName="فرانكفورت رومي",FoodCalories=187,FoodProtien=19,FoodFat=9.9,FoodFibers=1.1,FoodCarb=5.5},
new Food{FoodId=92,FoodCategory="لحوم",FoodName="لانشون رومي",FoodCalories=129,FoodProtien=20,FoodFat=3.5,FoodFibers=1.1,FoodCarb=4.4},
new Food{FoodId=93,FoodCategory="لحوم",FoodName="لحم رومي بالجلد",FoodCalories=151,FoodProtien=21,FoodFat=7.4,FoodFibers=0,FoodCarb=0},
new Food{FoodId=94,FoodCategory="لحوم",FoodName="لحم بتلو",FoodCalories=123,FoodProtien=18.7,FoodFat=5.4,FoodFibers=0,FoodCarb=0},
new Food{FoodId=95,FoodCategory="حلويات ",FoodName="عسلية ",FoodCalories=387,FoodProtien=1.6,FoodFat=0.7,FoodFibers=0,FoodCarb=93.6},
new Food{FoodId=96,FoodCategory="حلويات ",FoodName="بونبون",FoodCalories=392,FoodProtien=0,FoodFat=0,FoodFibers=0,FoodCarb=98},
new Food{FoodId=97,FoodCategory="حلويات ",FoodName="حلاوة فولية (سودانى )",FoodCalories=444,FoodProtien=7.8,FoodFat=12,FoodFibers=0.9,FoodCarb=76.1},
new Food{FoodId=98,FoodCategory="حلويات ",FoodName="حلاوة حمصية",FoodCalories=387,FoodProtien=6.5,FoodFat=1.7,FoodFibers=0.9,FoodCarb=86.5},
new Food{FoodId=99,FoodCategory="حلويات ",FoodName="جيلى كولا",FoodCalories=353,FoodProtien=2.6,FoodFat=0.3,FoodFibers=0,FoodCarb=84.9},
new Food{FoodId=100,FoodCategory="حلويات ",FoodName="كوز عسل",FoodCalories=387,FoodProtien=2.4,FoodFat=0.5,FoodFibers=0,FoodCarb=93.3},
new Food{FoodId=101,FoodCategory="حلويات ",FoodName="ملبن ",FoodCalories=382,FoodProtien=0.8,FoodFat=0.5,FoodFibers=0,FoodCarb=93.5},
new Food{FoodId=102,FoodCategory="حلويات ",FoodName="نوجا بالمكسرات",FoodCalories=397,FoodProtien=7.7,FoodFat=3.3,FoodFibers=0,FoodCarb=84.2},
new Food{FoodId=103,FoodCategory="حلويات ",FoodName="حلاوة سمسمية ",FoodCalories=443,FoodProtien=6,FoodFat=12,FoodFibers=1.3,FoodCarb=77.7},
new Food{FoodId=104,FoodCategory="حلويات ",FoodName="توفى",FoodCalories=392,FoodProtien=4,FoodFat=4,FoodFibers=0,FoodCarb=85},
new Food{FoodId=105,FoodCategory="حلويات ",FoodName="لبان ",FoodCalories=320,FoodProtien=0,FoodFat=0,FoodFibers=0,FoodCarb=80},
new Food{FoodId=106,FoodCategory="حلويات ",FoodName="شوكولاتة باللبن",FoodCalories=513,FoodProtien=8,FoodFat=27,FoodFibers=1,FoodCarb=59.5},
new Food{FoodId=107,FoodCategory="حلويات ",FoodName="شوكولاتة بجوز الهند",FoodCalories=469,FoodProtien=4,FoodFat=20,FoodFibers=1.3,FoodCarb=68.2},
new Food{FoodId=108,FoodCategory="حلويات ",FoodName="شوكولاتة بالمكسرات",FoodCalories=530,FoodProtien=9.3,FoodFat=30,FoodFibers=1.1,FoodCarb=55.6},
new Food{FoodId=109,FoodCategory="حلويات ",FoodName="عسل جلوكوز",FoodCalories=317,FoodProtien=10,FoodFat=10,FoodFibers=0,FoodCarb=79.3},
new Food{FoodId=110,FoodCategory="حلويات ",FoodName="حلاوة طحينية",FoodCalories=520,FoodProtien=11.2,FoodFat=27.5,FoodFibers=1.2,FoodCarb=57},
new Food{FoodId=111,FoodCategory="حلويات ",FoodName="حلاوة طحينية بالمكسرات ",FoodCalories=519,FoodProtien=12.1,FoodFat=28,FoodFibers=2,FoodCarb=54.6},
new Food{FoodId=112,FoodCategory="حلويات ",FoodName="عسل نحل",FoodCalories=329,FoodProtien=0.3,FoodFat=0,FoodFibers=0,FoodCarb=82},
new Food{FoodId=113,FoodCategory="حلويات ",FoodName="مربى ",FoodCalories=283,FoodProtien=0.7,FoodFat=0.1,FoodFibers=0.3,FoodCarb=69.7},
new Food{FoodId=114,FoodCategory="حلويات ",FoodName="بودرة جيلى ",FoodCalories=390,FoodProtien=10,FoodFat=10,FoodFibers=0,FoodCarb=87.4},
new Food{FoodId=115,FoodCategory="حلويات ",FoodName="عسل اسود",FoodCalories=280,FoodProtien=0,FoodFat=0,FoodFibers=0,FoodCarb=70},
new Food{FoodId=116,FoodCategory="حلويات ",FoodName="بقلاوة",FoodCalories=450,FoodProtien=4.8,FoodFat=23,FoodFibers=0.5,FoodCarb=56},
new Food{FoodId=117,FoodCategory="حلويات ",FoodName="بقلاوة بالمكسرات",FoodCalories=531,FoodProtien=6.3,FoodFat=34.3,FoodFibers=0.9,FoodCarb=49.3},
new Food{FoodId=118,FoodCategory="حلويات ",FoodName="بلح الشام",FoodCalories=420,FoodProtien=5.3,FoodFat=12.7,FoodFibers=0.4,FoodCarb=71.2},
new Food{FoodId=119,FoodCategory="حلويات ",FoodName="بسبوسة ",FoodCalories=355,FoodProtien=4.9,FoodFat=6,FoodFibers=0.7,FoodCarb=70.3},
new Food{FoodId=120,FoodCategory="حلويات ",FoodName="بسبوسة بالمكسرات",FoodCalories=375,FoodProtien=5.4,FoodFat=8,FoodFibers=0.8,FoodCarb=70.3},
new Food{FoodId=121,FoodCategory="حلويات ",FoodName="عيش السرايا بالقشطة",FoodCalories=426,FoodProtien=2.5,FoodFat=12.5,FoodFibers=0.5,FoodCarb=75.9},
new Food{FoodId=122,FoodCategory="حلويات ",FoodName="غريبة ",FoodCalories=533,FoodProtien=5.5,FoodFat=31,FoodFibers=0.7,FoodCarb=58},
new Food{FoodId=123,FoodCategory="حلويات ",FoodName="كحك العيد",FoodCalories=518,FoodProtien=7,FoodFat=27.6,FoodFibers=0.5,FoodCarb=60.5},
new Food{FoodId=124,FoodCategory="حلويات ",FoodName="قطايف",FoodCalories=348,FoodProtien=5,FoodFat=12.8,FoodFibers=0.6,FoodCarb=53.1},
new Food{FoodId=125,FoodCategory="حلويات ",FoodName="كنافة بالكريمة ",FoodCalories=361,FoodProtien=3,FoodFat=14.5,FoodFibers=0.5,FoodCarb=54.5},
new Food{FoodId=126,FoodCategory="حلويات ",FoodName="كنافة بالمكسرات ",FoodCalories=487,FoodProtien=5.8,FoodFat=23.7,FoodFibers=0.8,FoodCarb=62.5},
new Food{FoodId=127,FoodCategory="حلويات ",FoodName="لقمة القاضى ",FoodCalories=396,FoodProtien=3.9,FoodFat=9.5,FoodFibers=0.5,FoodCarb=73.7},
new Food{FoodId=128,FoodCategory="حلويات ",FoodName="معمول ",FoodCalories=500,FoodProtien=6,FoodFat=24.3,FoodFibers=1,FoodCarb=64.3},
new Food{FoodId=129,FoodCategory="حلويات ",FoodName="سكر قصب",FoodCalories=398,FoodProtien=0,FoodFat=0,FoodFibers=0,FoodCarb=99.5},
new Food{FoodId=130,FoodCategory="حلويات ",FoodName="سكر فركتوز",FoodCalories=396,FoodProtien=0,FoodFat=0,FoodFibers=0,FoodCarb=98.9},
new Food{FoodId=131,FoodCategory="حلويات ",FoodName="محليات صناعية ",FoodCalories=399,FoodProtien=0,FoodFat=0.9,FoodFibers=0,FoodCarb=97.6},
new Food{FoodId=132,FoodCategory="حلويات ",FoodName="صوص شوكولاتة ",FoodCalories=400,FoodProtien=1.9,FoodFat=20.7,FoodFibers=0,FoodCarb=51.5},
new Food{FoodId=133,FoodCategory="بيض",FoodName="بيض دجاج كامل",FoodCalories=149,FoodProtien=12.6,FoodFat=10.8,FoodFibers=0,FoodCarb=0.3},
new Food{FoodId=134,FoodCategory="بيض",FoodName="بياض بيض دجاج",FoodCalories=46,FoodProtien=11,FoodFat=0.1,FoodFibers=0,FoodCarb=0.2},
new Food{FoodId=135,FoodCategory="بيض",FoodName="صفار بيض دجاج",FoodCalories=343,FoodProtien=16.5,FoodFat=30.2,FoodFibers=0,FoodCarb=1.2},
new Food{FoodId=136,FoodCategory="بيض",FoodName="بيض بط كامل",FoodCalories=177,FoodProtien=13.5,FoodFat=13.3,FoodFibers=0,FoodCarb=0.7},
new Food{FoodId=137,FoodCategory="بيض",FoodName="بيض أومليت",FoodCalories=148,FoodProtien=9.9,FoodFat=10.5,FoodFibers=0.5,FoodCarb=3.5},
new Food{FoodId=138,FoodCategory="أسماك",FoodName="سمك دينيس",FoodCalories=100,FoodProtien=19.4,FoodFat=2.4,FoodFibers=0,FoodCarb=0.3},
new Food{FoodId=139,FoodCategory="أسماك",FoodName="سمك مرجان",FoodCalories=79,FoodProtien=17.6,FoodFat=1,FoodFibers=0,FoodCarb=0},
new Food{FoodId=140,FoodCategory="أسماك",FoodName="سمك بياض",FoodCalories=102,FoodProtien=18.7,FoodFat=3,FoodFibers=0,FoodCarb=0},
new Food{FoodId=141,FoodCategory="أسماك",FoodName="سمك قرموط",FoodCalories=81,FoodProtien=16.4,FoodFat=1.5,FoodFibers=0,FoodCarb=0.5},
new Food{FoodId=142,FoodCategory="أسماك",FoodName="سمك قشر بياض",FoodCalories=112,FoodProtien=18.1,FoodFat=4,FoodFibers=0,FoodCarb=0.8},
new Food{FoodId=143,FoodCategory="أسماك",FoodName="ملوحه",FoodCalories=152,FoodProtien=21.7,FoodFat=7,FoodFibers=0,FoodCarb=0.5},
new Food{FoodId=144,FoodCategory="أسماك",FoodName="كابوريا",FoodCalories=96,FoodProtien=18,FoodFat=1.5,FoodFibers=0,FoodCarb=2.6},
new Food{FoodId=145,FoodCategory="أسماك",FoodName="سمك تعابين",FoodCalories=166,FoodProtien=19,FoodFat=10,FoodFibers=0,FoodCarb=0},
new Food{FoodId=146,FoodCategory="أسماك",FoodName="أصابع سمك مقلي",FoodCalories=252,FoodProtien=12,FoodFat=13.4,FoodFibers=0,FoodCarb=20.9},
new Food{FoodId=147,FoodCategory="أسماك",FoodName=" رنجه مدخنه",FoodCalories=200,FoodProtien=20,FoodFat=12.9,FoodFibers=0,FoodCarb=0.9},
new Food{FoodId=148,FoodCategory="أسماك",FoodName="سمك مكرونه نئ",FoodCalories=95,FoodProtien=19.4,FoodFat=1.5,FoodFibers=0,FoodCarb=0.9},
new Food{FoodId=149,FoodCategory="أسماك",FoodName="سمك مكرونه مقلي",FoodCalories=248,FoodProtien=23.2,FoodFat=14.4,FoodFibers=0,FoodCarb=6.5},
new Food{FoodId=150,FoodCategory="أسماك",FoodName="سمك مكاريل",FoodCalories=215,FoodProtien=20,FoodFat=15,FoodFibers=0,FoodCarb=0.1},
new Food{FoodId=151,FoodCategory="أسماك",FoodName="سمك بوري نئ",FoodCalories=133,FoodProtien=19,FoodFat=6.3,FoodFibers=0,FoodCarb=0},
new Food{FoodId=152,FoodCategory="أسماك",FoodName="سمك بوري مشوي",FoodCalories=151,FoodProtien=21.5,FoodFat=6.7,FoodFibers=0,FoodCarb=1.2},
new Food{FoodId=153,FoodCategory="أسماك",FoodName="فسيخ",FoodCalories=154,FoodProtien=20.8,FoodFat=7.8,FoodFibers=0,FoodCarb=0.1},
new Food{FoodId=154,FoodCategory="أسماك",FoodName="سردين نئ",FoodCalories=156,FoodProtien=19.2,FoodFat=8.8,FoodFibers=0,FoodCarb=0},
new Food{FoodId=155,FoodCategory="أسماك",FoodName="سردين معلب",FoodCalories=227,FoodProtien=22,FoodFat=15.4,FoodFibers=0,FoodCarb=0.1},
new Food{FoodId=156,FoodCategory="أسماك",FoodName="سردين مشوي",FoodCalories=180,FoodProtien=22.5,FoodFat=9.2,FoodFibers=0,FoodCarb=1.7},
new Food{FoodId=157,FoodCategory="أسماك",FoodName="سردين مملح",FoodCalories=218,FoodProtien=23.3,FoodFat=13.5,FoodFibers=0,FoodCarb=0.8},
new Food{FoodId=158,FoodCategory="أسماك",FoodName="جمبري",FoodCalories=88,FoodProtien=19.5,FoodFat=0.9,FoodFibers=0,FoodCarb=0.5},
new Food{FoodId=159,FoodCategory="أسماك",FoodName="جمبري مسلوق",FoodCalories=116,FoodProtien=25,FoodFat=1.1,FoodFibers=0,FoodCarb=1.5},
new Food{FoodId=160,FoodCategory="أسماك",FoodName="سمك موسي نئ",FoodCalories=88,FoodProtien=18,FoodFat=1.6,FoodFibers=0,FoodCarb=0.3},
new Food{FoodId=161,FoodCategory="أسماك",FoodName="سمك موسي مقلي",FoodCalories=262,FoodProtien=21.5,FoodFat=15.2,FoodFibers=0,FoodCarb=9.8},
new Food{FoodId=162,FoodCategory="أسماك",FoodName="سمك بلطي نئ",FoodCalories=94,FoodProtien=19,FoodFat=2,FoodFibers=0,FoodCarb=0},
new Food{FoodId=163,FoodCategory="أسماك",FoodName="سمك بلطي مقلي",FoodCalories=233,FoodProtien=20,FoodFat=15,FoodFibers=0,FoodCarb=4.4},
new Food{FoodId=164,FoodCategory="أسماك",FoodName="سمك بلطي مشوي",FoodCalories=121,FoodProtien=22.5,FoodFat=2.6,FoodFibers=0,FoodCarb=1.9},
new Food{FoodId=165,FoodCategory="أسماك",FoodName="سمك تونه معلبه",FoodCalories=267,FoodProtien=24.6,FoodFat=18.5,FoodFibers=0,FoodCarb=0.4},
new Food{FoodId=166,FoodCategory="ألبان ومنتجات الألبان",FoodName="زبده غير مملحه",FoodCalories=242,FoodProtien=0.7,FoodFat=82,FoodFibers=0,FoodCarb=0.3},
new Food{FoodId=167,FoodCategory="ألبان ومنتجات الألبان",FoodName="سمنه",FoodCalories=886,FoodProtien=0.2,FoodFat=99.5,FoodFibers=0,FoodCarb=0},
new Food{FoodId=168,FoodCategory="ألبان ومنتجات الألبان",FoodName="جبن كاميمبرت",FoodCalories=301,FoodProtien=20,FoodFat=23.9,FoodFibers=0,FoodCarb=1.5},
new Food{FoodId=169,FoodCategory="ألبان ومنتجات الألبان",FoodName="جبن شيدر",FoodCalories=406,FoodProtien=26,FoodFat=32.9,FoodFibers=0,FoodCarb=1.5},
new Food{FoodId=170,FoodCategory="ألبان ومنتجات الألبان",FoodName="جبن جودا",FoodCalories=371,FoodProtien=24.5,FoodFat=30,FoodFibers=0,FoodCarb=0.8},
new Food{FoodId=171,FoodCategory="ألبان ومنتجات الألبان",FoodName="جبن قريش",FoodCalories=100,FoodProtien=13,FoodFat=3.5,FoodFibers=0,FoodCarb=4.1},
new Food{FoodId=172,FoodCategory="ألبان ومنتجات الألبان",FoodName="جبن قريش منخفض الدسم",FoodCalories=91,FoodProtien=19,FoodFat=0.5,FoodFibers=0,FoodCarb=2.7},
new Food{FoodId=173,FoodCategory="ألبان ومنتجات الألبان",FoodName="جبن قريش مش",FoodCalories=93,FoodProtien=14,FoodFat=3,FoodFibers=0,FoodCarb=2.4},
new Food{FoodId=174,FoodCategory="ألبان ومنتجات الألبان",FoodName="جبن موتزاريلا",FoodCalories=265,FoodProtien=27,FoodFat=16.5,FoodFibers=0,FoodCarb=2.2},
new Food{FoodId=175,FoodCategory="ألبان ومنتجات الألبان",FoodName="جبن بارميزان",FoodCalories=399,FoodProtien=35,FoodFat=27.5,FoodFibers=0,FoodCarb=2.9},
new Food{FoodId=176,FoodCategory="ألبان ومنتجات الألبان",FoodName="جبن مثلثات",FoodCalories=324,FoodProtien=19,FoodFat=25,FoodFibers=0,FoodCarb=5.7},
new Food{FoodId=177,FoodCategory="ألبان ومنتجات الألبان",FoodName="جبن كريمي",FoodCalories=289,FoodProtien=15.9,FoodFat=22.9,FoodFibers=0,FoodCarb=4.8},
new Food{FoodId=178,FoodCategory="ألبان ومنتجات الألبان",FoodName="جبن أبيض",FoodCalories=256,FoodProtien=16.3,FoodFat=19.8,FoodFibers=0,FoodCarb=3.2},
new Food{FoodId=179,FoodCategory="ألبان ومنتجات الألبان",FoodName="جبن أبيض نص دسم ",FoodCalories=151,FoodProtien=13,FoodFat=8.9,FoodFibers=0,FoodCarb=4.9},
new Food{FoodId=180,FoodCategory="ألبان ومنتجات الألبان",FoodName="جبن ريكفورد",FoodCalories=361,FoodProtien=22.3,FoodFat=29,FoodFibers=0,FoodCarb=2.7},
new Food{FoodId=181,FoodCategory="ألبان ومنتجات الألبان",FoodName="كريمه لباني",FoodCalories=353,FoodProtien=3,FoodFat=36.4,FoodFibers=0,FoodCarb=3.3},
new Food{FoodId=182,FoodCategory="ألبان ومنتجات الألبان",FoodName="لبن رايب",FoodCalories=62,FoodProtien=3.8,FoodFat=3.2,FoodFibers=0,FoodCarb=4.5},
new Food{FoodId=183,FoodCategory="ألبان ومنتجات الألبان",FoodName="لبن رايب منخفض الدسم",FoodCalories=46,FoodProtien=4.9,FoodFat=0.8,FoodFibers=0,FoodCarb=4.9},
new Food{FoodId=184,FoodCategory="ألبان ومنتجات الألبان",FoodName="لبن جاموس",FoodCalories=102,FoodProtien=4.2,FoodFat=7.1,FoodFibers=0,FoodCarb=5.4},
new Food{FoodId=185,FoodCategory="ألبان ومنتجات الألبان",FoodName="لبن بقري",FoodCalories=64,FoodProtien=3.3,FoodFat=3.7,FoodFibers=0,FoodCarb=4.4},
new Food{FoodId=186,FoodCategory="ألبان ومنتجات الألبان",FoodName="لبن الأم",FoodCalories=70,FoodProtien=1.2,FoodFat=3.9,FoodFibers=0,FoodCarb=7.5},
new Food{FoodId=187,FoodCategory="ألبان ومنتجات الألبان",FoodName="لبن بالشوكولاته",FoodCalories=80,FoodProtien=3.2,FoodFat=1.7,FoodFibers=0,FoodCarb=12.9},
new Food{FoodId=188,FoodCategory="ألبان ومنتجات الألبان",FoodName="لبن كامل الدسم",FoodCalories=64,FoodProtien=3.3,FoodFat=3.5,FoodFibers=0,FoodCarb=4.7},
new Food{FoodId=189,FoodCategory="ألبان ومنتجات الألبان",FoodName="لبن بالفواكه",FoodCalories=76,FoodProtien=3.2,FoodFat=1.7,FoodFibers=0,FoodCarb=12},
new Food{FoodId=190,FoodCategory="ألبان ومنتجات الألبان",FoodName="لبن نص دسم ",FoodCalories=48,FoodProtien=3.4,FoodFat=1.5,FoodFibers=0,FoodCarb=5.1},
new Food{FoodId=191,FoodCategory="ألبان ومنتجات الألبان",FoodName="لبن خالي الدسم",FoodCalories=41,FoodProtien=3.5,FoodFat=0.6,FoodFibers=0,FoodCarb=5.3},
new Food{FoodId=192,FoodCategory="ألبان ومنتجات الألبان",FoodName="لبن جاف خالي الدسم",FoodCalories=362,FoodProtien=37,FoodFat=1.5,FoodFibers=0,FoodCarb=50},
new Food{FoodId=193,FoodCategory="ألبان ومنتجات الألبان",FoodName="لبن جاف كامل الدسم",FoodCalories=497,FoodProtien=26.7,FoodFat=26.2,FoodFibers=0,FoodCarb=38.7},
new Food{FoodId=194,FoodCategory="ألبان ومنتجات الألبان",FoodName="زبادي",FoodCalories=64,FoodProtien=3.4,FoodFat=3.2,FoodFibers=0,FoodCarb=5.5},
new Food{FoodId=195,FoodCategory="ألبان ومنتجات الألبان",FoodName="زبادي منخفض الدسم",FoodCalories=50,FoodProtien=4,FoodFat=0.9,FoodFibers=0,FoodCarb=6.4},
new Food{FoodId=196,FoodCategory="ألبان ومنتجات الألبان",FoodName="زبادي بالفواكه",FoodCalories=68,FoodProtien=3,FoodFat=1.2,FoodFibers=0,FoodCarb=11.2},
new Food{FoodId=197,FoodCategory="عصائر ومشروبات",FoodName="بيريل خالي الكحول",FoodCalories=16,FoodProtien=0.2,FoodFat=0,FoodFibers=0,FoodCarb=3.8},
new Food{FoodId=198,FoodCategory="عصائر ومشروبات",FoodName="مشروب شعير غازي",FoodCalories=42,FoodProtien=0,FoodFat=0,FoodFibers=0,FoodCarb=10.5},
new Food{FoodId=199,FoodCategory="عصائر ومشروبات",FoodName="مشروبات غازيه ",FoodCalories=42,FoodProtien=0,FoodFat=0,FoodFibers=0,FoodCarb=10.6},
new Food{FoodId=200,FoodCategory="عصائر ومشروبات",FoodName="مشروبات  غازيه دايت",FoodCalories=1,FoodProtien=0,FoodFat=0,FoodFibers=0,FoodCarb=0.2},
new Food{FoodId=201,FoodCategory="عصائر ومشروبات",FoodName="مياه غازيه",FoodCalories=40,FoodProtien=0,FoodFat=0,FoodFibers=0,FoodCarb=10},
new Food{FoodId=202,FoodCategory="عصائر ومشروبات",FoodName="بودره كاكاو سريع الذوبان",FoodCalories=412,FoodProtien=6.2,FoodFat=6,FoodFibers=0.9,FoodCarb=83.4},
new Food{FoodId=203,FoodCategory="عصائر ومشروبات",FoodName="بودره قهوه سريعه الذوبان ",FoodCalories=360,FoodProtien=20.5,FoodFat=0.5,FoodFibers=0,FoodCarb=68.3},
new Food{FoodId=204,FoodCategory="عصائر ومشروبات",FoodName="بودره فواكه سريع الذوبان تانك",FoodCalories=392,FoodProtien=1.1,FoodFat=0,FoodFibers=0,FoodCarb=96.9},
new Food{FoodId=205,FoodCategory="عصائر ومشروبات",FoodName=" شاي جاف",FoodCalories=396,FoodProtien=1,FoodFat=0,FoodFibers=0,FoodCarb=97.9},
new Food{FoodId=206,FoodCategory="عصائر ومشروبات",FoodName="عصير تفاح غير محلي",FoodCalories=48,FoodProtien=0.1,FoodFat=0,FoodFibers=0,FoodCarb=12},
new Food{FoodId=207,FoodCategory="عصائر ومشروبات",FoodName="عصير  تفاح محلي",FoodCalories=56,FoodProtien=0,FoodFat=0,FoodFibers=0,FoodCarb=14},
new Food{FoodId=208,FoodCategory="عصائر ومشروبات",FoodName="عصير عنب محلي",FoodCalories=66,FoodProtien=0.3,FoodFat=0,FoodFibers=0,FoodCarb=16.2},
new Food{FoodId=209,FoodCategory="عصائر ومشروبات",FoodName="عصير جريب فروت غير محلي",FoodCalories=39,FoodProtien=0.5,FoodFat=0,FoodFibers=0.1,FoodCarb=9.2},
new Food{FoodId=210,FoodCategory="عصائر ومشروبات",FoodName="عصير برتقال غير محلي",FoodCalories=46,FoodProtien=0.6,FoodFat=0.2,FoodFibers=0.1,FoodCarb=10.4},
new Food{FoodId=211,FoodCategory="عصائر ومشروبات",FoodName="عصير برتقال محلي",FoodCalories=56,FoodProtien=0.1,FoodFat=0,FoodFibers=0,FoodCarb=14},
new Food{FoodId=212,FoodCategory="عصائر ومشروبات",FoodName="عصير خوخ غير محلي",FoodCalories=47,FoodProtien=0.5,FoodFat=0.2,FoodFibers=0.5,FoodCarb=10.9},
new Food{FoodId=213,FoodCategory="عصائر ومشروبات",FoodName="عصير رمان غير محلي",FoodCalories=62,FoodProtien=1,FoodFat=0,FoodFibers=0,FoodCarb=14.4},
new Food{FoodId=214,FoodCategory="عصائر ومشروبات",FoodName="عصير قصب",FoodCalories=45,FoodProtien=0,FoodFat=0,FoodFibers=0,FoodCarb=11.3},
new Food{FoodId=215,FoodCategory="عصائر ومشروبات",FoodName="عصير طماطم غير محلي",FoodCalories=24,FoodProtien=0.2,FoodFat=0,FoodFibers=0.1,FoodCarb=85},
new Food{FoodId=216,FoodCategory="عصائر ومشروبات",FoodName="نكتار مشمش",FoodCalories=59,FoodProtien=0.5,FoodFat=0.1,FoodFibers=0.2,FoodCarb=14},
new Food{FoodId=217,FoodCategory="عصائر ومشروبات",FoodName="نكتار كوكتيل فواكه",FoodCalories=61,FoodProtien=0.1,FoodFat=0.1,FoodFibers=0,FoodCarb=15},
new Food{FoodId=218,FoodCategory="عصائر ومشروبات",FoodName="نكتار جوافه",FoodCalories=60,FoodProtien=0.1,FoodFat=0,FoodFibers=0.1,FoodCarb=14.9},
new Food{FoodId=219,FoodCategory="عصائر ومشروبات",FoodName="نكتار مانجو",FoodCalories=62,FoodProtien=0.2,FoodFat=0.1,FoodFibers=0.1,FoodCarb=15.1},
new Food{FoodId=220,FoodCategory="حبوب",FoodName="شعير",FoodCalories=335,FoodProtien=10.7,FoodFat=1.5,FoodFibers=6.5,FoodCarb=69.6},
new Food{FoodId=221,FoodCategory="حبوب",FoodName="ذره بيضاء",FoodCalories=364,FoodProtien=10.2,FoodFat=4,FoodFibers=1.9,FoodCarb=71.9},
new Food{FoodId=222,FoodCategory="حبوب",FoodName="ذره صفراء",FoodCalories=368,FoodProtien=12.7,FoodFat=5.5,FoodFibers=2.9,FoodCarb=67},
new Food{FoodId=223,FoodCategory="حبوب",FoodName="كورن فليكس",FoodCalories=373,FoodProtien=6.2,FoodFat=2.1,FoodFibers=2,FoodCarb=82.3},
new Food{FoodId=224,FoodCategory="حبوب",FoodName="دقيق ذره",FoodCalories=359,FoodProtien=8.9,FoodFat=2.5,FoodFibers=1,FoodCarb=75.2},
new Food{FoodId=225,FoodCategory="حبوب",FoodName="حبوب ذره معلبه",FoodCalories=100,FoodProtien=2.8,FoodFat=0.9,FoodFibers=0.8,FoodCarb=20.1},
new Food{FoodId=226,FoodCategory="حبوب",FoodName="كوز ذره مشوي",FoodCalories=161,FoodProtien=4,FoodFat=1,FoodFibers=3,FoodCarb=34.1},
new Food{FoodId=227,FoodCategory="حبوب",FoodName="فيشار",FoodCalories=451,FoodProtien=9.1,FoodFat=16.7,FoodFibers=2.5,FoodCarb=66},
new Food{FoodId=228,FoodCategory="حبوب",FoodName="كاراتيه",FoodCalories=503,FoodProtien=7.8,FoodFat=25.7,FoodFibers=1.5,FoodCarb=60.1},
new Food{FoodId=229,FoodCategory="حبوب",FoodName="نشا ذره ",FoodCalories=380,FoodProtien=0.2,FoodFat=1,FoodFibers=0,FoodCarb=94.5},
new Food{FoodId=230,FoodCategory="حبوب",FoodName="مكرونه",FoodCalories=361,FoodProtien=12,FoodFat=1.1,FoodFibers=0.4,FoodCarb=75.8},
new Food{FoodId=231,FoodCategory="حبوب",FoodName="مكرونه بالصلصه",FoodCalories=169,FoodProtien=5,FoodFat=3,FoodFibers=0.7,FoodCarb=30.5},
new Food{FoodId=232,FoodCategory="حبوب",FoodName="مكرونه بالصلصه باللحم",FoodCalories=175,FoodProtien=6.8,FoodFat=5.6,FoodFibers=0.6,FoodCarb=24.4},
new Food{FoodId=233,FoodCategory="حبوب",FoodName="شعريه محمره",FoodCalories=218,FoodProtien=5.1,FoodFat=6,FoodFibers=0.3,FoodCarb=36},
new Food{FoodId=234,FoodCategory="حبوب",FoodName="حبوب أرز طويله",FoodCalories=357,FoodProtien=8.7,FoodFat=0.8,FoodFibers=0.3,FoodCarb=78.7},
new Food{FoodId=235,FoodCategory="حبوب",FoodName="حبوب أرز قصيره",FoodCalories=351,FoodProtien=7.3,FoodFat=0.7,FoodFibers=0.3,FoodCarb=78.8},
new Food{FoodId=236,FoodCategory="حبوب",FoodName="دقيق أرز",FoodCalories=354,FoodProtien=7.3,FoodFat=0.5,FoodFibers=0.3,FoodCarb=80},
new Food{FoodId=237,FoodCategory="حبوب",FoodName="أرز محمر",FoodCalories=214,FoodProtien=3.6,FoodFat=3.9,FoodFibers=0.3,FoodCarb=41.1},
new Food{FoodId=238,FoodCategory="حبوب",FoodName="أرز محمر بالشعريه",FoodCalories=219,FoodProtien=3.5,FoodFat=4.5,FoodFibers=0.3,FoodCarb=41.1},
new Food{FoodId=239,FoodCategory="حبوب",FoodName="أرز محمر بالبصل",FoodCalories=203,FoodProtien=3.1,FoodFat=4.6,FoodFibers=0.6,FoodCarb=37.2},
new Food{FoodId=240,FoodCategory="حبوب",FoodName="أرز بالخضروات",FoodCalories=420,FoodProtien=16.2,FoodFat=10,FoodFibers=0.5,FoodCarb=66.4},
new Food{FoodId=241,FoodCategory="حبوب",FoodName="كشري مصري ",FoodCalories=172,FoodProtien=6.5,FoodFat=5.2,FoodFibers=1.1,FoodCarb=24.7},
new Food{FoodId=242,FoodCategory="حبوب",FoodName="كشري مصري معلب",FoodCalories=157,FoodProtien=4.5,FoodFat=3.7,FoodFibers=0.9,FoodCarb=26.3},
new Food{FoodId=243,FoodCategory="حبوب",FoodName="أرز باللبن",FoodCalories=129,FoodProtien=3.3,FoodFat=2.6,FoodFibers=0.5,FoodCarb=23.1},
new Food{FoodId=244,FoodCategory="حبوب",FoodName="أ{ز كرسبي ",FoodCalories=379,FoodProtien=9,FoodFat=0.7,FoodFibers=0.3,FoodCarb=84.2},
new Food{FoodId=245,FoodCategory="حبوب",FoodName="دقيق سميد",FoodCalories=360,FoodProtien=12,FoodFat=1.8,FoodFibers=0.4,FoodCarb=74},
new Food{FoodId=246,FoodCategory="حبوب",FoodName="ذره رفيعه",FoodCalories=366,FoodProtien=10.9,FoodFat=3.6,FoodFibers=2.2,FoodCarb=72.6},
new Food{FoodId=247,FoodCategory="حبوب",FoodName="رده",FoodCalories=313,FoodProtien=14.4,FoodFat=3.9,FoodFibers=10,FoodCarb=55},
new Food{FoodId=248,FoodCategory="حبوب",FoodName="حبوب قمح",FoodCalories=344,FoodProtien=12,FoodFat=1.6,FoodFibers=2.4,FoodCarb=70.3},
new Food{FoodId=249,FoodCategory="حبوب",FoodName="بليله قمح معلب",FoodCalories=95,FoodProtien=2.4,FoodFat=0.6,FoodFibers=0.6,FoodCarb=20},
new Food{FoodId=250,FoodCategory="حبوب",FoodName="دقيق قمح كامل",FoodCalories=350,FoodProtien=11.8,FoodFat=1.3,FoodFibers=1.1,FoodCarb=72.7},
new Food{FoodId=251,FoodCategory="حبوب",FoodName="دقيق أبيض ",FoodCalories=354,FoodProtien=10,FoodFat=1,FoodFibers=0.4,FoodCarb=76.3},
new Food{FoodId=252,FoodCategory="حبوب",FoodName="دقيق بالمغذيات للأطفال",FoodCalories=413,FoodProtien=17,FoodFat=10,FoodFibers=2.2,FoodCarb=63.8},
new Food{FoodId=253,FoodCategory="حبوب",FoodName="كسكسي",FoodCalories=213,FoodProtien=5.8,FoodFat=0.7,FoodFibers=0.3,FoodCarb=45.8},
new Food{FoodId=254,FoodCategory="حبوب",FoodName="كسكسي بالسكر",FoodCalories=283,FoodProtien=5.8,FoodFat=9.8,FoodFibers=0.5,FoodCarb=42.9},
new Food{FoodId=255,FoodCategory="حبوب",FoodName="برغل نئ",FoodCalories=352,FoodProtien=12.5,FoodFat=1.8,FoodFibers=1,FoodCarb=71.4},
new Food{FoodId=256,FoodCategory="حبوب",FoodName="فريك نئ",FoodCalories=351,FoodProtien=11.6,FoodFat=1.7,FoodFibers=2.1,FoodCarb=72.4},
new Food{FoodId=257,FoodCategory="حبوب",FoodName="فريك مطهي",FoodCalories=199,FoodProtien=5.3,FoodFat=4,FoodFibers=1,FoodCarb=35.4},
new Food{FoodId=258,FoodCategory="حبوب",FoodName="فريك مطهي باللحم",FoodCalories=213,FoodProtien=7.8,FoodFat=6.4,FoodFibers=0.6,FoodCarb=31},
new Food{FoodId=259,FoodCategory="مخبوزات",FoodName="بسكويت مغطي بالشوكولانه",FoodCalories=517,FoodProtien=10.5,FoodFat=27.5,FoodFibers=1,FoodCarb=56.8},
new Food{FoodId=260,FoodCategory="مخبوزات",FoodName="بسكويت محشي كريمه",FoodCalories=509,FoodProtien=7.3,FoodFat=25,FoodFibers=0.6,FoodCarb=63.6},
new Food{FoodId=261,FoodCategory="مخبوزات",FoodName="بسكويت محشي بلح",FoodCalories=453,FoodProtien=6.4,FoodFat=15.8,FoodFibers=1,FoodCarb=71.2},
new Food{FoodId=262,FoodCategory="مخبوزات",FoodName="بسكويت دايجيستف",FoodCalories=468,FoodProtien=7.5,FoodFat=20,FoodFibers=3,FoodCarb=64.6},
new Food{FoodId=263,FoodCategory="مخبوزات",FoodName="بسكويت نايس",FoodCalories=454,FoodProtien=9.8,FoodFat=15,FoodFibers=0.9,FoodCarb=70},
new Food{FoodId=264,FoodCategory="مخبوزات",FoodName="بسكويت ساده",FoodCalories=438,FoodProtien=9.6,FoodFat=11.8,FoodFibers=0.8,FoodCarb=73.3},
new Food{FoodId=265,FoodCategory="مخبوزات",FoodName="بسكويت معزز بالحديد",FoodCalories=440,FoodProtien=9.4,FoodFat=12,FoodFibers=0.8,FoodCarb=73.5},
new Food{FoodId=266,FoodCategory="مخبوزات",FoodName="بسكويت مالح توك",FoodCalories=438,FoodProtien=10.1,FoodFat=13.7,FoodFibers=3,FoodCarb=68.6},
new Food{FoodId=267,FoodCategory="مخبوزات",FoodName="بسكويت مالح بريتزل",FoodCalories=398,FoodProtien=9.8,FoodFat=3,FoodFibers=3,FoodCarb=82.9},
new Food{FoodId=268,FoodCategory="مخبوزات",FoodName="بسكويت ويفر مغطي بالشوكولاته",FoodCalories=535,FoodProtien=6.4,FoodFat=29.8,FoodFibers=6,FoodCarb=60.4},
new Food{FoodId=269,FoodCategory="مخبوزات",FoodName="بسكويت ويفر محشي بالكريمه",FoodCalories=515,FoodProtien=6.3,FoodFat=25.4,FoodFibers=6,FoodCarb=65.2},
new Food{FoodId=270,FoodCategory="مخبوزات",FoodName="خبز بلدي",FoodCalories=254,FoodProtien=8.8,FoodFat=1,FoodFibers=1.3,FoodCarb=52.5},
new Food{FoodId=271,FoodCategory="مخبوزات",FoodName="خبز  بلدي قمح وذره",FoodCalories=256,FoodProtien=9,FoodFat=1.3,FoodFibers=1.4,FoodCarb=52},
new Food{FoodId=272,FoodCategory="مخبوزات",FoodName="خبز سن",FoodCalories=369,FoodProtien=12.3,FoodFat=2.1,FoodFibers=3.3,FoodCarb=75.2},
new Food{FoodId=273,FoodCategory="مخبوزات",FoodName="خبز كيزر بالسمسم",FoodCalories=273,FoodProtien=12.5,FoodFat=3,FoodFibers=1.1,FoodCarb=49},
new Food{FoodId=274,FoodCategory="مخبوزات",FoodName="خبز فلاحي ذره وقمح 1:1",FoodCalories=355,FoodProtien=9.9,FoodFat=2.5,FoodFibers=1.1,FoodCarb=73.2},
new Food{FoodId=275,FoodCategory="مخبوزات",FoodName="خبز فلاحي ذره وقمح 1:2",FoodCalories=360,FoodProtien=10.1,FoodFat=2.8,FoodFibers=1.9,FoodCarb=73.7},
new Food{FoodId=276,FoodCategory="مخبوزات",FoodName="خبز فلاحي ذره وقمح 2:1",FoodCalories=351,FoodProtien=9.8,FoodFat=2.9,FoodFibers=2,FoodCarb=71.3},
new Food{FoodId=277,FoodCategory="مخبوزات",FoodName="بقسماط مطحون",FoodCalories=356,FoodProtien=10.3,FoodFat=2,FoodFibers=2,FoodCarb=74.2},
new Food{FoodId=278,FoodCategory="مخبوزات",FoodName="خبز فرنسي",FoodCalories=285,FoodProtien=8.7,FoodFat=1,FoodFibers=0.3,FoodCarb=60.3},
new Food{FoodId=279,FoodCategory="مخبوزات",FoodName="خبز لبناني",FoodCalories=311,FoodProtien=9.2,FoodFat=0.7,FoodFibers=0.3,FoodCarb=66.9},
new Food{FoodId=280,FoodCategory="مخبوزات",FoodName="خبز لبناني أسمر",FoodCalories=293,FoodProtien=9.4,FoodFat=2.1,FoodFibers=2.5,FoodCarb=59.1},
new Food{FoodId=281,FoodCategory="مخبوزات",FoodName="خبز شامي",FoodCalories=292,FoodProtien=8.6,FoodFat=0.9,FoodFibers=0.2,FoodCarb=62.3},
new Food{FoodId=282,FoodCategory="مخبوزات",FoodName="خبز شمسي",FoodCalories=259,FoodProtien=9.2,FoodFat=1.1,FoodFibers=0.6,FoodCarb=53.1},
new Food{FoodId=283,FoodCategory="مخبوزات",FoodName="توست أسمر",FoodCalories=257,FoodProtien=8.9,FoodFat=1.9,FoodFibers=2.4,FoodCarb=51.1},
new Food{FoodId=284,FoodCategory="مخبوزات",FoodName="توست أبيض",FoodCalories=270,FoodProtien=8.4,FoodFat=1.5,FoodFibers=0.5,FoodCarb=55.6},
new Food{FoodId=285,FoodCategory="مخبوزات",FoodName="بقسماط أصابع بالسمسم",FoodCalories=379,FoodProtien=11.7,FoodFat=2.9,FoodFibers=1.3,FoodCarb=56.5},
new Food{FoodId=286,FoodCategory="مخبوزات",FoodName="فايش",FoodCalories=398,FoodProtien=12,FoodFat=7.5,FoodFibers=0.9,FoodCarb=70.6},
new Food{FoodId=287,FoodCategory="مخبوزات",FoodName="كب كيك",FoodCalories=379,FoodProtien=7,FoodFat=18.4,FoodFibers=0.3,FoodCarb=46.3},
new Food{FoodId=288,FoodCategory="مخبوزات",FoodName="كيك اسفنجي",FoodCalories=290,FoodProtien=7.5,FoodFat=4.2,FoodFibers=0.2,FoodCarb=55.6},
new Food{FoodId=289,FoodCategory="مخبوزات",FoodName="كرواسون",FoodCalories=476,FoodProtien=5.5,FoodFat=34,FoodFibers=0.3,FoodCarb=37.1},
new Food{FoodId=290,FoodCategory="مخبوزات",FoodName="فطيره بالبلح",FoodCalories=361,FoodProtien=9.5,FoodFat=10.9,FoodFibers=1.3,FoodCarb=56.1},
new Food{FoodId=291,FoodCategory="مخبوزات",FoodName="فطير مشلتت",FoodCalories=402,FoodProtien=6.7,FoodFat=30,FoodFibers=0.5,FoodCarb=26.2},
new Food{FoodId=292,FoodCategory="مخبوزات",FoodName="فطير ",FoodCalories=341,FoodProtien=7.6,FoodFat=12,FoodFibers=0.4,FoodCarb=50.7},
new Food{FoodId=293,FoodCategory="مخبوزات",FoodName=" رقاق باللحم",FoodCalories=279,FoodProtien=8.5,FoodFat=18.6,FoodFibers=1,FoodCarb=19.4},
new Food{FoodId=294,FoodCategory="مخبوزات",FoodName="بيتزا  بالجبن",FoodCalories=243,FoodProtien=12,FoodFat=9.9,FoodFibers=1,FoodCarb=26.5},
new Food{FoodId=295,FoodCategory="مخبوزات",FoodName="بيتزا  باللحم",FoodCalories=267,FoodProtien=13.4,FoodFat=9.3,FoodFibers=1,FoodCarb=32.4},
new Food{FoodId=296,FoodCategory="مخبوزات",FoodName="بيتزا فواكه بحر",FoodCalories=252,FoodProtien=10,FoodFat=8,FoodFibers=0.8,FoodCarb=34.1},
new Food{FoodId=297,FoodCategory="مخبوزات",FoodName="بيتزا خضروات",FoodCalories=230,FoodProtien=5.9,FoodFat=7.1,FoodFibers=1,FoodCarb=35.6},
new Food{FoodId=298,FoodCategory="مخبوزات",FoodName="سندوتش جبن ابيض",FoodCalories=180,FoodProtien=7.5,FoodFat=4,FoodFibers=1,FoodCarb=28.5},
new Food{FoodId=299,FoodCategory="مخبوزات",FoodName="سندوتش جبن اصفر",FoodCalories=262,FoodProtien=10.5,FoodFat=7,FoodFibers=1.2,FoodCarb=39.3},
new Food{FoodId=300,FoodCategory="مخبوزات",FoodName="سندوتش مخ مقلي ",FoodCalories=232,FoodProtien=10.1,FoodFat=11.5,FoodFibers=0.3,FoodCarb=21.9},
new Food{FoodId=301,FoodCategory="مخبوزات",FoodName="سندوتش دجاج مقلي",FoodCalories=259,FoodProtien=9.6,FoodFat=10,FoodFibers=1,FoodCarb=32.7},
new Food{FoodId=302,FoodCategory="مخبوزات",FoodName="سندوتش باذنجان مقلي",FoodCalories=123,FoodProtien=4,FoodFat=5.5,FoodFibers=0.8,FoodCarb=31.9},
new Food{FoodId=303,FoodCategory="مخبوزات",FoodName="سندوتش كبده مقلي",FoodCalories=238,FoodProtien=14.4,FoodFat=6.8,FoodFibers=0.6,FoodCarb=29.7},
new Food{FoodId=304,FoodCategory="مخبوزات",FoodName="سندوتش جمبري مقلي",FoodCalories=252,FoodProtien=10.2,FoodFat=10.5,FoodFibers=0.7,FoodCarb=29.1},
new Food{FoodId=305,FoodCategory="مخبوزات",FoodName="سندوتش طعميه",FoodCalories=205,FoodProtien=6.1,FoodFat=5.8,FoodFibers=1.3,FoodCarb=32.2},
new Food{FoodId=306,FoodCategory="مخبوزات",FoodName="سندوتش دجاج مشوي",FoodCalories=223,FoodProtien=11.4,FoodFat=8,FoodFibers=0.9,FoodCarb=26.4},
new Food{FoodId=307,FoodCategory="مخبوزات",FoodName="سندوتش كفته مشويه",FoodCalories=224,FoodProtien=12,FoodFat=7.5,FoodFibers=0.6,FoodCarb=27.2},
new Food{FoodId=308,FoodCategory="مخبوزات",FoodName="سندوتش برجر",FoodCalories=216,FoodProtien=8.8,FoodFat=7.5,FoodFibers=0.8,FoodCarb=28.4},
new Food{FoodId=309,FoodCategory="مخبوزات",FoodName="سندوتش سوسيس وهوت دوج",FoodCalories=221,FoodProtien=8.8,FoodFat=6.5,FoodFibers=0.5,FoodCarb=31.9},
new Food{FoodId=310,FoodCategory="مخبوزات",FoodName="سندوتش شاورما لحم",FoodCalories=220,FoodProtien=10.7,FoodFat=7,FoodFibers=0.7,FoodCarb=28.6},
new Food{FoodId=311,FoodCategory="مخبوزات",FoodName="سندوتش فول",FoodCalories=173,FoodProtien=5,FoodFat=2.2,FoodFibers=1.5,FoodCarb=33.3},
new Food{FoodId=312,FoodCategory="درنيات",FoodName="حب العزيز نئ",FoodCalories=413,FoodProtien=5.7,FoodFat=16.2,FoodFibers=9.3,FoodCarb=61.1},
new Food{FoodId=313,FoodCategory="درنيات",FoodName="حب العزيز منقوع",FoodCalories=278,FoodProtien=4,FoodFat=9.3,FoodFibers=5,FoodCarb=44.6},
new Food{FoodId=314,FoodCategory="درنيات",FoodName="قلقاس نئ",FoodCalories=77,FoodProtien=2.1,FoodFat=2,FoodFibers=0.8,FoodCarb=16.6},
new Food{FoodId=315,FoodCategory="درنيات",FoodName="قلقاس مطهي",FoodCalories=91,FoodProtien=2.5,FoodFat=4.4,FoodFibers=0.3,FoodCarb=10.3},
new Food{FoodId=316,FoodCategory="درنيات",FoodName="بطاطس نئيه",FoodCalories=73,FoodProtien=1.6,FoodFat=1,FoodFibers=0.6,FoodCarb=16.4},
new Food{FoodId=317,FoodCategory="درنيات",FoodName="بطاطس شيبسي",FoodCalories=557,FoodProtien=5.5,FoodFat=37.5,FoodFibers=1.6,FoodCarb=49.4},
new Food{FoodId=318,FoodCategory="درنيات",FoodName="بطاطس محمره",FoodCalories=281,FoodProtien=4.1,FoodFat=14.5,FoodFibers=1,FoodCarb=33.4},
new Food{FoodId=319,FoodCategory="درنيات",FoodName="بطاطس مطبوخه بالطماطم",FoodCalories=104,FoodProtien=1.2,FoodFat=3.5,FoodFibers=0.4,FoodCarb=16.9},
new Food{FoodId=320,FoodCategory="درنيات",FoodName="بطاطس بيوريه باللبن",FoodCalories=80,FoodProtien=2.2,FoodFat=1.9,FoodFibers=0.4,FoodCarb=13.6},
new Food{FoodId=321,FoodCategory="درنيات",FoodName="بطاطس بيوريه باللحم والبيض",FoodCalories=121,FoodProtien=6,FoodFat=7,FoodFibers=0.5,FoodCarb=8.4},
new Food{FoodId=322,FoodCategory="درنيات",FoodName="بطاطس محشيه لحم",FoodCalories=115,FoodProtien=3.1,FoodFat=5,FoodFibers=0.6,FoodCarb=14.5},
new Food{FoodId=323,FoodCategory="درنيات",FoodName="بطاطا حلوه",FoodCalories=112,FoodProtien=1.8,FoodFat=0.3,FoodFibers=0.9,FoodCarb=25.5},
new Food{FoodId=324,FoodCategory="بقوليات",FoodName="فول جاف",FoodCalories=326,FoodProtien=24.1,FoodFat=1.5,FoodFibers=6.9,FoodCarb=54},
new Food{FoodId=325,FoodCategory="بقوليات",FoodName="بصاره",FoodCalories=130,FoodProtien=6,FoodFat=4,FoodFibers=3,FoodCarb=17.4},
new Food{FoodId=326,FoodCategory="بقوليات",FoodName="فول جاف مدشوش",FoodCalories=346,FoodProtien=25.9,FoodFat=2,FoodFibers=2.8,FoodCarb=56.2},
new Food{FoodId=327,FoodCategory="بقوليات",FoodName="فول مدمس معلب",FoodCalories=132,FoodProtien=6.1,FoodFat=5,FoodFibers=2.1,FoodCarb=15.7},
new Food{FoodId=328,FoodCategory="بقوليات",FoodName="فول مدمس",FoodCalories=98,FoodProtien=5.6,FoodFat=7,FoodFibers=2,FoodCarb=17.2},
new Food{FoodId=329,FoodCategory="بقوليات",FoodName="فول نابت",FoodCalories=145,FoodProtien=10.4,FoodFat=7,FoodFibers=2.9,FoodCarb=24.2},
new Food{FoodId=330,FoodCategory="بقوليات",FoodName="فول نابت مطهي",FoodCalories=114,FoodProtien=8.8,FoodFat=6,FoodFibers=1,FoodCarb=18.4},
new Food{FoodId=331,FoodCategory="بقوليات",FoodName="طعميه مقليه",FoodCalories=355,FoodProtien=10.9,FoodFat=20.1,FoodFibers=1.5,FoodCarb=32.6},
new Food{FoodId=332,FoodCategory="بقوليات",FoodName="فاصوليا حمراء",FoodCalories=323,FoodProtien=23.1,FoodFat=1.3,FoodFibers=5.8,FoodCarb=54.8},
new Food{FoodId=333,FoodCategory="بقوليات",FoodName="فاصوليا بيضاء جافه",FoodCalories=333,FoodProtien=22.1,FoodFat=1.4,FoodFibers=4.2,FoodCarb=58},
new Food{FoodId=334,FoodCategory="بقوليات",FoodName="فاصوليا بيضاء مطهيه",FoodCalories=134,FoodProtien=8.9,FoodFat=2.4,FoodFibers=1.9,FoodCarb=19.3},
new Food{FoodId=335,FoodCategory="بقوليات",FoodName="فاصوليا بيضاء مطهيه باللحم",FoodCalories=152,FoodProtien=11.4,FoodFat=5.6,FoodFibers=2,FoodCarb=14},
new Food{FoodId=336,FoodCategory="بقوليات",FoodName="فول مجفف",FoodCalories=352,FoodProtien=21.4,FoodFat=2,FoodFibers=4,FoodCarb=62.1},
new Food{FoodId=337,FoodCategory="بقوليات",FoodName="حمص شام جاف",FoodCalories=352,FoodProtien=20.3,FoodFat=4.2,FoodFibers=5.3,FoodCarb=58.3},
new Food{FoodId=338,FoodCategory="بقوليات",FoodName="حمص شام مطهي",FoodCalories=138,FoodProtien=7,FoodFat=1.6,FoodFibers=2.1,FoodCarb=23.9},
new Food{FoodId=339,FoodCategory="بقوليات",FoodName="حمص طبيخ",FoodCalories=354,FoodProtien=19.6,FoodFat=4.4,FoodFibers=3.4,FoodCarb=59},
new Food{FoodId=340,FoodCategory="بقوليات",FoodName="لوبيا جافه",FoodCalories=330,FoodProtien=23,FoodFat=1.2,FoodFibers=4.7,FoodCarb=56.7},
new Food{FoodId=341,FoodCategory="بقوليات",FoodName="لوبيا مطهيه",FoodCalories=131,FoodProtien=8.5,FoodFat=3.5,FoodFibers=2,FoodCarb=16.4},
new Food{FoodId=342,FoodCategory="بقوليات",FoodName="لوبيا مطهيه باللحم",FoodCalories=146,FoodProtien=11,FoodFat=5.5,FoodFibers=2.5,FoodCarb=13},
new Food{FoodId=343,FoodCategory="بقوليات",FoodName="حلبه جافه",FoodCalories=356,FoodProtien=25.6,FoodFat=6.7,FoodFibers=6.5,FoodCarb=48.3},
new Food{FoodId=344,FoodCategory="بقوليات",FoodName="عدس بجبه جلف",FoodCalories=340,FoodProtien=22.4,FoodFat=1.1,FoodFibers=3.8,FoodCarb=60},
new Food{FoodId=345,FoodCategory="بقوليات",FoodName="عدس بجبه مطهي",FoodCalories=151,FoodProtien=5.8,FoodFat=5.6,FoodFibers=2.3,FoodCarb=19.3},
new Food{FoodId=346,FoodCategory="بقوليات",FoodName="عدس أصفر جاف",FoodCalories=340,FoodProtien=22.9,FoodFat=0.7,FoodFibers=2.1,FoodCarb=60.5},
new Food{FoodId=347,FoodCategory="بقوليات",FoodName="كشري أصفر بالأرز",FoodCalories=140,FoodProtien=6.2,FoodFat=5.2,FoodFibers=1.2,FoodCarb=17.1},
new Food{FoodId=348,FoodCategory="بقوليات",FoodName="شوربه عدس",FoodCalories=68,FoodProtien=4.8,FoodFat=1.5,FoodFibers=0.7,FoodCarb=8.8},
new Food{FoodId=349,FoodCategory="بقوليات",FoodName="ترمس جاف",FoodCalories=355,FoodProtien=38,FoodFat=9,FoodFibers=11,FoodCarb=30.5},
new Food{FoodId=350,FoodCategory="بقوليات",FoodName="ترمس منقوع بالملح",FoodCalories=111,FoodProtien=9.6,FoodFat=2.7,FoodFibers=3,FoodCarb=12},
new Food{FoodId=351,FoodCategory="بقوليات",FoodName="بسله جافه",FoodCalories=345,FoodProtien=22.1,FoodFat=2.1,FoodFibers=3.9,FoodCarb=59.4},
new Food{FoodId=352,FoodCategory="بقوليات",FoodName="فول صويا",FoodCalories=426,FoodProtien=31.7,FoodFat=19.9,FoodFibers=4.3,FoodCarb=30.1},
new Food{FoodId=353,FoodCategory="بقوليات",FoodName="دقيق فول صويا",FoodCalories=362,FoodProtien=44.5,FoodFat=3.9,FoodFibers=2.8,FoodCarb=37.1},
new Food{FoodId=354,FoodCategory="بذور ومكسرات",FoodName="لوز جاف",FoodCalories=640,FoodProtien=17.6,FoodFat=55.8,FoodFibers=2.5,FoodCarb=16.8},
new Food{FoodId=355,FoodCategory="بذور ومكسرات",FoodName="جوز هند جاف",FoodCalories=689,FoodProtien=6,FoodFat=65,FoodFibers=4,FoodCarb=20},
new Food{FoodId=356,FoodCategory="بذور ومكسرات",FoodName="بندق جاف",FoodCalories=633,FoodProtien=18.6,FoodFat=55.7,FoodFibers=3.7,FoodCarb=14.4},
new Food{FoodId=357,FoodCategory="بذور ومكسرات",FoodName="فول سوداني",FoodCalories=585,FoodProtien=26.4,FoodFat=44.9,FoodFibers=2.9,FoodCarb=18.8},
new Food{FoodId=358,FoodCategory="بذور ومكسرات",FoodName="صنوبر جاف",FoodCalories=614,FoodProtien=30.2,FoodFat=50.2,FoodFibers=1,FoodCarb=10.3},
new Food{FoodId=359,FoodCategory="بذور ومكسرات",FoodName="فستق جاف",FoodCalories=631,FoodProtien=20.9,FoodFat=54.1,FoodFibers=1.8,FoodCarb=15.1},
new Food{FoodId=360,FoodCategory="بذور ومكسرات",FoodName="لب قرع مملح",FoodCalories=591,FoodProtien=23.9,FoodFat=48,FoodFibers=4.5,FoodCarb=15.8},
new Food{FoodId=361,FoodCategory="بذور ومكسرات",FoodName="سمسم",FoodCalories=600,FoodProtien=19,FoodFat=51.8,FoodFibers=5.5,FoodCarb=14.5},
new Food{FoodId=362,FoodCategory="بذور ومكسرات",FoodName="طحينه سمسم",FoodCalories=660,FoodProtien=20,FoodFat=60,FoodFibers=4,FoodCarb=10},
new Food{FoodId=363,FoodCategory="بذور ومكسرات",FoodName="عين جمل أو جوز",FoodCalories=695,FoodProtien=14.7,FoodFat=64.9,FoodFibers=2.2,FoodCarb=13},
new Food{FoodId=364,FoodCategory="بذور ومكسرات",FoodName="لب بطيخ مملح",FoodCalories=592,FoodProtien=34.7,FoodFat=48,FoodFibers=3,FoodCarb=5.3},
new Food{FoodId=365,FoodCategory="خضروات",FoodName="خرشوف",FoodCalories=45,FoodProtien=3.5,FoodFat=0.2,FoodFibers=2,FoodCarb=7.4},
new Food{FoodId=366,FoodCategory="خضروات",FoodName="سلطه خرشوف",FoodCalories=89,FoodProtien=2.7,FoodFat=4.7,FoodFibers=1.4,FoodCarb=9},
new Food{FoodId=367,FoodCategory="خضروات",FoodName="فول حراتي",FoodCalories=79,FoodProtien=5.7,FoodFat=0.4,FoodFibers=2.4,FoodCarb=13.2},
new Food{FoodId=368,FoodCategory="خضروات",FoodName="فاصوليا خضراء",FoodCalories=30,FoodProtien=1.8,FoodFat=0.2,FoodFibers=1.1,FoodCarb=5.2},
new Food{FoodId=369,FoodCategory="خضروات",FoodName="فاصوليا خضراء مطهيه",FoodCalories=61,FoodProtien=1.9,FoodFat=3.2,FoodFibers=1.1,FoodCarb=6.2},
new Food{FoodId=370,FoodCategory="خضروات",FoodName="بنجر",FoodCalories=45,FoodProtien=1.5,FoodFat=0.1,FoodFibers=1.1,FoodCarb=9.5},
new Food{FoodId=371,FoodCategory="خضروات",FoodName="بروكلي",FoodCalories=38,FoodProtien=3.5,FoodFat=0.3,FoodFibers=1.5,FoodCarb=5.4},
new Food{FoodId=372,FoodCategory="خضروات",FoodName="كرنب بروكسل",FoodCalories=50,FoodProtien=4.6,FoodFat=0.4,FoodFibers=1.3,FoodCarb=6.9},
new Food{FoodId=373,FoodCategory="خضروات",FoodName="كرنب أحمر",FoodCalories=31,FoodProtien=1.6,FoodFat=0.3,FoodFibers=1.5,FoodCarb=5.4},
new Food{FoodId=374,FoodCategory="خضروات",FoodName="كرنب أبيض",FoodCalories=27,FoodProtien=1.3,FoodFat=0.2,FoodFibers=1.2,FoodCarb=5},
new Food{FoodId=375,FoodCategory="خضروات",FoodName="محشى كرنب بالأرز",FoodCalories=127,FoodProtien=3.5,FoodFat=4,FoodFibers=1.2,FoodCarb=19.2},
new Food{FoodId=376,FoodCategory="خضروات",FoodName="محشي كرنب بالأرز واللحم",FoodCalories=140,FoodProtien=5,FoodFat=6.2,FoodFibers=1.2,FoodCarb=16.1},
new Food{FoodId=377,FoodCategory="خضروات",FoodName="جزر",FoodCalories=36,FoodProtien=1.2,FoodFat=0.2,FoodFibers=1,FoodCarb=7.4},
new Food{FoodId=378,FoodCategory="خضروات",FoodName="قرنبيط نئ",FoodCalories=28,FoodProtien=2.3,FoodFat=0.3,FoodFibers=1,FoodCarb=4.1},
new Food{FoodId=379,FoodCategory="خضروات",FoodName="قرنبيط مطهي",FoodCalories=49,FoodProtien=2,FoodFat=2.1,FoodFibers=0.8,FoodCarb=5.4},
new Food{FoodId=380,FoodCategory="خضروات",FoodName="قرنبيط مطهي باللحم",FoodCalories=62,FoodProtien=3.7,FoodFat=3.1,FoodFibers=0.7,FoodCarb=4.9},
new Food{FoodId=381,FoodCategory="خضروات",FoodName="كرفس",FoodCalories=52,FoodProtien=0.9,FoodFat=0.2,FoodFibers=0.7,FoodCarb=11.7},
new Food{FoodId=382,FoodCategory="خضروات",FoodName="سلق",FoodCalories=27,FoodProtien=2.3,FoodFat=0.4,FoodFibers=0.9,FoodCarb=3.5},
new Food{FoodId=383,FoodCategory="خضروات",FoodName="حمص أخضر",FoodCalories=139,FoodProtien=4.7,FoodFat=1.3,FoodFibers=1.7,FoodCarb=27},
new Food{FoodId=384,FoodCategory="خضروات",FoodName="هندباء شيكوريا ",FoodCalories=27,FoodProtien=1.8,FoodFat=0.4,FoodFibers=0.9,FoodCarb=4},
new Food{FoodId=385,FoodCategory="خضروات",FoodName="كزبره",FoodCalories=34,FoodProtien=3.7,FoodFat=0.6,FoodFibers=1.7,FoodCarb=3.5},
new Food{FoodId=386,FoodCategory="خضروات",FoodName="لوبيا خضراء",FoodCalories=50,FoodProtien=3.5,FoodFat=0.3,FoodFibers=1.6,FoodCarb=8.4},
new Food{FoodId=387,FoodCategory="خضروات",FoodName="خيار ",FoodCalories=16,FoodProtien=0.7,FoodFat=0.1,FoodFibers=0.6,FoodCarb=3.1},
new Food{FoodId=388,FoodCategory="خضروات",FoodName="خيار مخلل",FoodCalories=22,FoodProtien=1.1,FoodFat=0,FoodFibers=0.4,FoodCarb=4.5},
new Food{FoodId=389,FoodCategory="خضروات",FoodName="شبت",FoodCalories=36,FoodProtien=2.9,FoodFat=0.5,FoodFibers=1.2,FoodCarb=5},
new Food{FoodId=390,FoodCategory="خضروات",FoodName="باذنجان ابيض واسود",FoodCalories=28,FoodProtien=1.7,FoodFat=0.2,FoodFibers=1.2,FoodCarb=4.9},
new Food{FoodId=391,FoodCategory="خضروات",FoodName="باذنجان رومي",FoodCalories=27,FoodProtien=1.8,FoodFat=0.1,FoodFibers=1.4,FoodCarb=4.8},
new Food{FoodId=392,FoodCategory="خضروات",FoodName="باذنجان رومي مطهي",FoodCalories=73,FoodProtien=1.6,FoodFat=5.3,FoodFibers=0.8,FoodCarb=4.7},
new Food{FoodId=393,FoodCategory="خضروات",FoodName="باذنجان رومي مطهي باللحم",FoodCalories=84,FoodProtien=3.6,FoodFat=6,FoodFibers=0.7,FoodCarb=4},
new Food{FoodId=394,FoodCategory="خضروات",FoodName="باذنجان مقلي",FoodCalories=175,FoodProtien=1.5,FoodFat=17,FoodFibers=1,FoodCarb=3.9},
new Food{FoodId=395,FoodCategory="خضروات",FoodName="باذنجان محشي بالأرز",FoodCalories=126,FoodProtien=3,FoodFat=4,FoodFibers=1,FoodCarb=19.5},
new Food{FoodId=396,FoodCategory="خضروات",FoodName="شمر",FoodCalories=37,FoodProtien=2.6,FoodFat=0.5,FoodFibers=1.5,FoodCarb=5.4},
new Food{FoodId=397,FoodCategory="خضروات",FoodName="أوراق حلبه",FoodCalories=45,FoodProtien=3.5,FoodFat=0.5,FoodFibers=1.4,FoodCarb=6.5},
new Food{FoodId=398,FoodCategory="خضروات",FoodName="جرجير",FoodCalories=32,FoodProtien=2.5,FoodFat=0.5,FoodFibers=1,FoodCarb=4.4},
new Food{FoodId=399,FoodCategory="خضروات",FoodName="ثوم",FoodCalories=142,FoodProtien=5.6,FoodFat=0.3,FoodFibers=1.2,FoodCarb=29.1},
new Food{FoodId=400,FoodCategory="خضروات",FoodName="ورق عنب",FoodCalories=80,FoodProtien=4,FoodFat=0.6,FoodFibers=2.3,FoodCarb=14.7},
new Food{FoodId=401,FoodCategory="خضروات",FoodName="ورق عنب معلب",FoodCalories=71,FoodProtien=7.3,FoodFat=0.6,FoodFibers=3,FoodCarb=9.1},
new Food{FoodId=402,FoodCategory="خضروات",FoodName="ورق عنب محشي بالأرز",FoodCalories=131,FoodProtien=4,FoodFat=4.2,FoodFibers=1.5,FoodCarb=19.4},
new Food{FoodId=403,FoodCategory="خضروات",FoodName="ملوخيه",FoodCalories=54,FoodProtien=5,FoodFat=1,FoodFibers=1.5,FoodCarb=6.3},
new Food{FoodId=404,FoodCategory="خضروات",FoodName="ملوخيه مطهيه",FoodCalories=91,FoodProtien=3.5,FoodFat=6,FoodFibers=1.5,FoodCarb=5.7},
new Food{FoodId=405,FoodCategory="خضروات",FoodName="كرات",FoodCalories=18,FoodProtien=1.8,FoodFat=0.3,FoodFibers=0.9,FoodCarb=2},
new Food{FoodId=406,FoodCategory="خضروات",FoodName="خس بلدي",FoodCalories=15,FoodProtien=0.9,FoodFat=0.2,FoodFibers=0.5,FoodCarb=2.3},
new Food{FoodId=407,FoodCategory="خضروات",FoodName="خس كابوتشا",FoodCalories=16,FoodProtien=1.1,FoodFat=0.3,FoodFibers=0.7,FoodCarb=2.1},
new Food{FoodId=408,FoodCategory="خضروات",FoodName="خس محشي أرز ",FoodCalories=124,FoodProtien=3.1,FoodFat=4,FoodFibers=1,FoodCarb=18.8},
new Food{FoodId=409,FoodCategory="خضروات",FoodName="خس محشي أرز ولحمه",FoodCalories=147,FoodProtien=5,FoodFat=7,FoodFibers=0.9,FoodCarb=16},
new Food{FoodId=410,FoodCategory="خضروات",FoodName="خبيزه ",FoodCalories=55,FoodProtien=5,FoodFat=0.6,FoodFibers=1.3,FoodCarb=7.5},
new Food{FoodId=411,FoodCategory="خضروات",FoodName="خبيزه مطهيه",FoodCalories=99,FoodProtien=3,FoodFat=5.6,FoodFibers=0.9,FoodCarb=9.1},
new Food{FoodId=412,FoodCategory="خضروات",FoodName="نعناع",FoodCalories=52,FoodProtien=3.7,FoodFat=1.2,FoodFibers=2.8,FoodCarb=6.1},
new Food{FoodId=413,FoodCategory="خضروات",FoodName="مشروم",FoodCalories=31,FoodProtien=2.5,FoodFat=1,FoodFibers=0.5,FoodCarb=5.1},
new Food{FoodId=414,FoodCategory="خضروات",FoodName="مشروم معلب",FoodCalories=27,FoodProtien=3.2,FoodFat=1,FoodFibers=0.5,FoodCarb=3.4},
new Food{FoodId=415,FoodCategory="خضروات",FoodName="باميه",FoodCalories=47,FoodProtien=2,FoodFat=0.2,FoodFibers=1,FoodCarb=9.3},
new Food{FoodId=416,FoodCategory="خضروات",FoodName="باميه مطهيه",FoodCalories=72,FoodProtien=2.8,FoodFat=3.6,FoodFibers=0.7,FoodCarb=7.2},
new Food{FoodId=417,FoodCategory="خضروات",FoodName="باميه مطهيه باللحم",FoodCalories=89,FoodProtien=5.1,FoodFat=0.5,FoodFibers=0.6,FoodCarb=5.8},
new Food{FoodId=418,FoodCategory="خضروات",FoodName="زيتون أسود مخلل",FoodCalories=158,FoodProtien=1.5,FoodFat=12,FoodFibers=1,FoodCarb=10.9},
new Food{FoodId=419,FoodCategory="خضروات",FoodName="زيتون أخضر مخلل",FoodCalories=88,FoodProtien=1,FoodFat=8,FoodFibers=1,FoodCarb=2.9},
new Food{FoodId=420,FoodCategory="خضروات",FoodName="بصل أخضر",FoodCalories=49,FoodProtien=1.1,FoodFat=0.2,FoodFibers=0.9,FoodCarb=10.7},
new Food{FoodId=421,FoodCategory="خضروات",FoodName="بصل ",FoodCalories=60,FoodProtien=1.2,FoodFat=0.2,FoodFibers=0.7,FoodCarb=13.3},
new Food{FoodId=422,FoodCategory="خضروات",FoodName="بصل مخلل",FoodCalories=53,FoodProtien=0.5,FoodFat=0.2,FoodFibers=1.4,FoodCarb=12.2},
new Food{FoodId=423,FoodCategory="خضروات",FoodName="بقدونس",FoodCalories=50,FoodProtien=3.3,FoodFat=0.4,FoodFibers=1.3,FoodCarb=8.2},
new Food{FoodId=424,FoodCategory="خضروات",FoodName="بسله خضراء",FoodCalories=89,FoodProtien=6.2,FoodFat=0.4,FoodFibers=2.1,FoodCarb=15.1},
new Food{FoodId=425,FoodCategory="خضروات",FoodName="بسله خضراء مطهيه بالجزر",FoodCalories=62,FoodProtien=4.1,FoodFat=3,FoodFibers=1.2,FoodCarb=4.7},
new Food{FoodId=426,FoodCategory="خضروات",FoodName="فلفل اخضر",FoodCalories=24,FoodProtien=1.3,FoodFat=0.3,FoodFibers=1.5,FoodCarb=4.1},
new Food{FoodId=427,FoodCategory="خضروات",FoodName="فلفل الوان",FoodCalories=34,FoodProtien=1.2,FoodFat=0.3,FoodFibers=1.2,FoodCarb=6.6},
new Food{FoodId=428,FoodCategory="خضروات",FoodName="قرع عسل",FoodCalories=26,FoodProtien=1,FoodFat=0.1,FoodFibers=1.1,FoodCarb=5.2},
new Food{FoodId=429,FoodCategory="خضروات",FoodName="رجله",FoodCalories=31,FoodProtien=2,FoodFat=0.5,FoodFibers=1,FoodCarb=4.5},
new Food{FoodId=430,FoodCategory="خضروات",FoodName="فجل احمر",FoodCalories=19,FoodProtien=1.2,FoodFat=0.1,FoodFibers=0.8,FoodCarb=3.2},
new Food{FoodId=431,FoodCategory="خضروات",FoodName="فجل ابيض",FoodCalories=27,FoodProtien=1.4,FoodFat=0.1,FoodFibers=1,FoodCarb=5},
new Food{FoodId=432,FoodCategory="خضروات",FoodName="فجل ابيض ورق وجذور",FoodCalories=26,FoodProtien=2,FoodFat=0.4,FoodFibers=1.1,FoodCarb=3.7},
new Food{FoodId=433,FoodCategory="خضروات",FoodName="سبانخ",FoodCalories=22,FoodProtien=2.5,FoodFat=0.3,FoodFibers=0.7,FoodCarb=2.4},
new Food{FoodId=434,FoodCategory="خضروات",FoodName="سبانخ مطهيه",FoodCalories=57,FoodProtien=2,FoodFat=3.9,FoodFibers=1,FoodCarb=3.4},
new Food{FoodId=435,FoodCategory="خضروات",FoodName="سبانخ مطهيه باللحم",FoodCalories=75,FoodProtien=4,FoodFat=5.2,FoodFibers=0.7,FoodCarb=3.1},
new Food{FoodId=436,FoodCategory="خضروات",FoodName="كوسا",FoodCalories=24,FoodProtien=1.3,FoodFat=0.2,FoodFibers=0.8,FoodCarb=4.2},
new Food{FoodId=437,FoodCategory="خضروات",FoodName="كوسا مطهيه",FoodCalories=55,FoodProtien=1.9,FoodFat=3.4,FoodFibers=0.3,FoodCarb=4.3},
new Food{FoodId=438,FoodCategory="خضروات",FoodName="كوسا مطهيه باللحم",FoodCalories=67,FoodProtien=3.4,FoodFat=4.6,FoodFibers=0.5,FoodCarb=2.9},
new Food{FoodId=439,FoodCategory="خضروات",FoodName="كوسا محشيه بالأرز",FoodCalories=103,FoodProtien=2.1,FoodFat=2.7,FoodFibers=1,FoodCarb=17.6},
new Food{FoodId=440,FoodCategory="خضروات",FoodName="كوسا محشيه بالأرز واللحم",FoodCalories=112,FoodProtien=3,FoodFat=4,FoodFibers=0.9,FoodCarb=16},
new Food{FoodId=441,FoodCategory="خضروات",FoodName="طماطم",FoodCalories=20,FoodProtien=1.1,FoodFat=0.3,FoodFibers=0.6,FoodCarb=3.1},
new Food{FoodId=442,FoodCategory="خضروات",FoodName="كاتشب ",FoodCalories=126,FoodProtien=1.6,FoodFat=0.6,FoodFibers=0.3,FoodCarb=28.6},
new Food{FoodId=443,FoodCategory="خضروات",FoodName="صلصه طماطم",FoodCalories=78,FoodProtien=3.8,FoodFat=0.4,FoodFibers=0.5,FoodCarb=14.8},
new Food{FoodId=444,FoodCategory="خضروات",FoodName="لفت",FoodCalories=27,FoodProtien=1.2,FoodFat=0.2,FoodFibers=0.7,FoodCarb=5.2},
new Food{FoodId=445,FoodCategory="خضروات",FoodName="طرشي",FoodCalories=16,FoodProtien=1.1,FoodFat=0,FoodFibers=0.4,FoodCarb=3}

                };

                foreach (Food food in foods)
                {
                    Database.Insert(food);
                }

            }
            catch (Exception e)
            {

            }

        }

        public async Task<List<T>> LoadAsync<T>() where T : new()
        {
            List<T> values = new List<T>();

            await CheckDatabaseInitialization();

            values = Database.Table<T>().ToList();

            return values;
        }
        public async Task<List<T>> ClearAllAsync<T>() where T : new()
        {
            await CheckDatabaseInitialization();

            Database.DeleteAll<T>();

            return Database.Table<T>().ToList();
        }

        public async Task InsertAsync<T>(T t) where T : new()
        {
            await CheckDatabaseInitialization();

            Database.Insert(t);
        }
        public async Task UpdateAsync<T>(T t) where T : new()
        {
            await CheckDatabaseInitialization();

            Database.Update(t);
        }

        public async Task DeleteAsync<T>(T t) where T : new()
        {
            await CheckDatabaseInitialization();

            Database.Delete(t);
        }

        public async Task<bool> FindEmailAsync(string email)
        {
            try
            {
                Membership MemberShip = Database.Table<Membership>().Where(x => x.Email == email).FirstOrDefault();

                if (MemberShip == null)
                {
                    return false;
                }
                else
                {
                    return true;
                }

            }
            catch (Exception e)
            {
                await init();

                return false;
            }
        }

        public async Task<bool> MatchEmailWithPassWordAsync(string email, string passWord)
        {
            try
            {
                Membership MemberShip = Database.Table<Membership>().Where(x => x.Email == email && x.Password == passWord).FirstOrDefault();

                if (MemberShip == null)
                {
                    return false;
                }
                else
                {
                    return true;
                }

            }
            catch (Exception e)
            {
                await init();

                return false;
            }
        }

        private async Task CheckDatabaseInitialization()
        {
            try
            {
                var BodyActivityTest = Database.Table<BodyActivity>().FirstOrDefault();

                if (BodyActivityTest == null)
                {
                    await init();
                }
            }
            catch (Exception e)
            {
                await init();
            }
        }

        public async Task<Membership> FindMemberShipByEmailAsync(string email)
        {
            Membership MemberShip;
            try
            {
                MemberShip = Database.Table<Membership>().Where(x => x.Email == email).FirstOrDefault();
            }
            catch (Exception)
            {
                MemberShip = new Membership();
                await init();
            }
            return MemberShip;
        }

        public async Task<bool> FindMealAsync(string mealname)
        {
            await CheckDatabaseInitialization();

            List<Meal> allmeals = await LoadAsync<Meal>();

            var value = allmeals.Where(x => x.MealName == mealname).FirstOrDefault();

            if (value != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public async Task<bool> FindAsync<T>(string valueID) where T : new()
        {
            await CheckDatabaseInitialization();

            var value = Database.Table<T>().Where(x => x.ToString() == valueID).FirstOrDefault();

            if (value == null)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public async Task UpdateCartItem(CartItem cartItem) => Database.Update(cartItem);

        public async Task UpdateMeal(Meal meal) => Database.Update(meal);

        private async Task<TResult> ExecuteAsync<TResult>(Func<Task<TResult>> actionOnDb)
        {
            await init();
            return await actionOnDb.Invoke();
        }

        //public async Task<int> AddCartItem(CartItemEntity entity) =>
        //    await ExecuteAsync(async () => await Database.InsertAsync(entity));


        //public async Task DeleteCartItem(CartItemEntity entity)
        //=> await ExecuteAsync(async () => await Database.DeleteAsync(entity));

        //public async Task<CartItemEntity> GetCartItemAsync(int id) =>
        //    await ExecuteAsync(async () => await Database.GetAsync<CartItemEntity>(id));

        //public async Task<List<CartItemEntity>> GetAllCartItemsAsync() =>
        //    await ExecuteAsync(async () => await Database.Table<CartItemEntity>().ToListAsync());

        //public async Task<int> ClearCartAsync() =>
        //    await ExecuteAsync(async () => await Database.DeleteAllAsync<CartItemEntity>());

        //public async ValueTask DisposeAsync() =>
        //    await _connection?.CloseAsync();
    }
}
