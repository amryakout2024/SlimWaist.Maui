using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microcharts;
using Microcharts.Maui;
using SkiaSharp;
using SlimWaist.Languages;
using SlimWaist.Models;
using SlimWaist.Views;
using System.Collections.ObjectModel;
using System.Globalization;

namespace SlimWaist.ViewModels
{
    public partial class HomeVM(DataContext dataContext) : BaseVM
    {
        private readonly DataContext _dataContext = dataContext;

        [ObservableProperty]
        private List<BodyActivity> _bodyActivities;

        [ObservableProperty]
        private string _email;

        [ObservableProperty]
        private int _dateday;

        [ObservableProperty]
        private string _datedayname;

        [ObservableProperty]
        private int _datemonth;

        [ObservableProperty]
        private int _dateyear;

        [ObservableProperty]
        private string? _name;

        [ObservableProperty]
        private string? _birthDate;

        [ObservableProperty]
        private int _genderIndex;

        [ObservableProperty]
        private string? _weight;

        [ObservableProperty]
        private string? _height;

        [ObservableProperty]
        private string? _bMI;

        [ObservableProperty]
        private string? _idealWeight;

        [ObservableProperty]
        private string? _targetedWeight;

        [ObservableProperty]
        private string? _modifiedWeight;

        [ObservableProperty]
        private string? _waistCircumferenceMeasurement;

        [ObservableProperty]
        private string? _bodyActivity;

        [ObservableProperty]
        private string? _totalEnergy;
        [ObservableProperty]
        private string? _totalConsumedEnergy;
        [ObservableProperty]
        private string? _totalRemainingEnergy;


        [ObservableProperty]
        private string? _totalBreakfastEnergy;
        [ObservableProperty]
        private string? _totalLunchEnergy;
        [ObservableProperty]
        private string? _totalDinnerEnergy;
        [ObservableProperty]
        private string? _totalSnaksEnergy;

        [ObservableProperty]
        private bool _isBottomSheetPresented;

        [ObservableProperty]
        private bool _isTabbarVisible;

        private int ObesityDegreeId;

        [ObservableProperty]
        private string? _obesityDegreeName;

        private int WaistCircumferenceId;

        [ObservableProperty]
        private string? _waistCircumferenceName;

        [ObservableProperty]
        private double _percentConsumed;

        [ObservableProperty]
        private ObservableCollection<RegimeList> _regimeLists;

        Setting setting;

        ChartEntry[] chartEntries;
        [ObservableProperty]
        private Chart _chart;

        [ObservableProperty]
        private bool _isKetoChecked;

        [ObservableProperty]
        private bool _isLowCarbChecked;

        [ObservableProperty]
        private bool _isServingSizeChecked;

        [ObservableProperty]
        private List<Diet> _diets;

        [ObservableProperty]
        private Diet _selectedDiet;

        public static Meal? CurrentMeal { get; set;}=new Meal();
        public static DayDiet? CurrentDayDiet { get; set;}=new DayDiet();

        private Meal ExistingBreakfastMeal=new Meal();
        private Meal ExistingLunchMeal=new Meal();
        private Meal ExistingDinnerMeal=new Meal();
        private Meal ExistingSnaksMeal=new Meal();
        private List<DayDiet> ExistingDayDiets=new List<DayDiet>();
        private DayDiet ExistingDayDiet;

        private DateTime SelectedDateFromUser { get; set; }

        private DateTime ExistingDayDietDate { get; set; }

        public async Task init()
        {
            //Preferences.Set("Email", "");

            TotalEnergy = "0";
            TotalRemainingEnergy = TotalEnergy;
            TotalConsumedEnergy = "0";
            TotalBreakfastEnergy = "0";
            TotalLunchEnergy = "0";
            TotalDinnerEnergy = "0";
            TotalSnaksEnergy = "0";
            IsBottomSheetPresented = false;
            IsTabbarVisible = true;


            BodyActivities = App.BodyActivities;
            Name = App.currentMembership?.Name ?? "";
            Weight = App.currentMembership?.Weight.ToString() ?? "";            Height = App.currentMembership?.Height.ToString() ?? "";
            BirthDate = App.currentMembership?.BirthDateDay.ToString() ?? "";
            BodyActivity = BodyActivities.Where(x => x.BodyActivityId == App.currentMembership.BodyActivityId).FirstOrDefault().BodyActivityName;
            WaistCircumferenceMeasurement = App.currentMembership.WaistCircumferenceMeasurement.ToString();
            TargetedWeight = IdealWeight;

            Diets = await App.dataContext.LoadAsync<Diet>();
            
            SelectedDateFromUser=new DateTime(
                Convert.ToInt32(DateTime.Now.Year)
                , Convert.ToInt32(DateTime.Now.Month)
                , Convert.ToInt32(DateTime.Now.Day));
            
            Dateday=SelectedDateFromUser.Day;
            Datemonth=SelectedDateFromUser.Month;
            Dateyear=SelectedDateFromUser.Year;
    
            BmiCalculator();
            IdealWeightCalculator();
            ModifiedWeightCalculator();
            TotalEnergyCalculator(App.currentMembership.BodyActivityId);
            WaistCircumferenceEvaluationCalculator();
            ObesityDegreeCalculator();

            CurrentDayDiet = _dataContext.Database.Table<DayDiet>()
                .Where(x => x.MembershipId == App.currentMembership.Id)
                .Where(x=>x.DayDietDate==SelectedDateFromUser).FirstOrDefault()??new DayDiet();
            var dayDietCount = _dataContext.Database.Table<DayDiet>().ToList().Count();
            if (CurrentDayDiet.IsExistsInDb)
            {
                SelectedDiet = Diets.Where(x => x.DietId == CurrentDayDiet.DietId).FirstOrDefault();
            }
            else
            {
                CurrentDayDiet.DayDietId = (dayDietCount == 0) ? 1 : _dataContext.Database.Table<DayDiet>().ToList().Select(x => x.DayDietId).ToList().Max() + 1;
                CurrentDayDiet.MembershipId = App.currentMembership.Id;
                CurrentDayDiet.DayDietDate = new DateTime(
                    SelectedDateFromUser.Year
                    , SelectedDateFromUser.Month
                    , SelectedDateFromUser.Day);
            }

            var mealsCount = _dataContext.Database.Table<Meal>().ToList().Count();
            CurrentMeal = new Meal()
            {
                MealId =(mealsCount==0)?1: _dataContext.Database.Table<Meal>().ToList().Select(x => x.MealId).ToList().Max() + 1,
                MembershipId=App.currentMembership.Id,
                DayDietId=CurrentDayDiet.DayDietId
            };

            ExistingBreakfastMeal = _dataContext.Database.Table<Meal>()
                .Where(x => x.MembershipId == App.currentMembership.Id
                && x.DayDietId == CurrentDayDiet.DayDietId).FirstOrDefault() ?? new Meal();
            ExistingLunchMeal = _dataContext.Database.Table<Meal>()
                .Where(x => x.MembershipId == App.currentMembership.Id
                && x.DayDietId == CurrentDayDiet.DayDietId).FirstOrDefault() ?? new Meal();
            ExistingDinnerMeal = _dataContext.Database.Table<Meal>()
                .Where(x => x.MembershipId == App.currentMembership.Id
                && x.DayDietId == CurrentDayDiet.DayDietId).FirstOrDefault() ?? new Meal();
            ExistingSnaksMeal = _dataContext.Database.Table<Meal>()
                .Where(x => x.MembershipId == App.currentMembership.Id
                && x.DayDietId == CurrentDayDiet.DayDietId).FirstOrDefault() ?? new Meal();


            if (ExistingBreakfastMeal.IsExistsInDb == true)
            {
                var mealDetails = _dataContext.Database.Table<MealDetail>().Where(x => x.MealId == ExistingBreakfastMeal.MealId).ToList() ?? new List<MealDetail>();

                if (mealDetails.Count > 0)
                {
                    foreach (var mealDetail in mealDetails.Where(x => x.MealId == ExistingBreakfastMeal.MealId).ToList())
                    {
                        var foodFromMealDetail = _dataContext.Database.Table<Food>().Where(x => x.FoodId == mealDetail.FoodId).FirstOrDefault();

                        TotalBreakfastEnergy = (Convert.ToDouble(TotalBreakfastEnergy)+ Math.Round((foodFromMealDetail.FoodCalories * Convert.ToDouble(mealDetail.Quantity) / 100), 1)).ToString("F1");
                    }
                }
            }
            if (ExistingLunchMeal.IsExistsInDb == true)
            {
                var mealDetails = _dataContext.Database.Table<MealDetail>().Where(x => x.MealId == ExistingLunchMeal.MealId).ToList() ?? new List<MealDetail>();

                if (mealDetails.Count > 0)
                {
                    foreach (var mealDetail in mealDetails.Where(x => x.MealId == ExistingLunchMeal.MealId).ToList())
                    {
                        var foodFromMealDetail = _dataContext.Database.Table<Food>().Where(x => x.FoodId == mealDetail.FoodId).FirstOrDefault();

                        TotalLunchEnergy = (Convert.ToDouble(TotalLunchEnergy) + Math.Round((foodFromMealDetail.FoodCalories * Convert.ToDouble(mealDetail.Quantity) / 100), 1)).ToString("F1");
                    }
                }
            }
            if (ExistingDinnerMeal.IsExistsInDb == true)
            {
                var mealDetails = _dataContext.Database.Table<MealDetail>().Where(x => x.MealId == ExistingDinnerMeal.MealId).ToList() ?? new List<MealDetail>();

                if (mealDetails.Count > 0)
                {
                    foreach (var mealDetail in mealDetails.Where(x => x.MealId == ExistingDinnerMeal.MealId).ToList())
                    {
                        var foodFromMealDetail = _dataContext.Database.Table<Food>().Where(x => x.FoodId == mealDetail.FoodId).FirstOrDefault();

                        TotalDinnerEnergy = (Convert.ToDouble(TotalDinnerEnergy) + Math.Round((foodFromMealDetail.FoodCalories * Convert.ToDouble(mealDetail.Quantity) / 100), 1)).ToString("F1");
                    }
                }
            }
            if (ExistingSnaksMeal.IsExistsInDb == true)
            {
                var mealDetails = _dataContext.Database.Table<MealDetail>().Where(x => x.MealId == ExistingSnaksMeal.MealId).ToList() ?? new List<MealDetail>();

                if (mealDetails.Count > 0)
                {
                    foreach (var mealDetail in mealDetails.Where(x => x.MealId == ExistingSnaksMeal.MealId).ToList())
                    {
                        var foodFromMealDetail = _dataContext.Database.Table<Food>().Where(x => x.FoodId == mealDetail.FoodId).FirstOrDefault();

                        TotalSnaksEnergy = (Convert.ToDouble(TotalSnaksEnergy) + Math.Round((foodFromMealDetail.FoodCalories * Convert.ToDouble(mealDetail.Quantity) / 100), 1)).ToString("F1");
                    }
                }
            }

            TotalConsumedEnergy = Math.Round(
                Convert.ToDouble(TotalBreakfastEnergy) +
                Convert.ToDouble(TotalLunchEnergy) +
                Convert.ToDouble(TotalDinnerEnergy) +
                Convert.ToDouble(TotalSnaksEnergy),1).ToString("F1");
            TotalRemainingEnergy = Math.Round(
                Convert.ToDouble(TotalEnergy) -
                Convert.ToDouble(TotalConsumedEnergy), 1).ToString("F1");

            var PercentRemain = (float)((Convert.ToDouble(TotalRemainingEnergy) / Convert.ToDouble(TotalEnergy) * 100));
            PercentConsumed = Math.Round((Convert.ToDouble(TotalConsumedEnergy) / Convert.ToDouble(TotalEnergy) * 100),1);

            chartEntries = new ChartEntry[]
            {
                
                //red first
                new ChartEntry((float)PercentConsumed)
                {
                    Color=SKColor.Parse("#a10a14"),
                },
                new ChartEntry(PercentRemain)
                {
                    Color=SKColor.Parse("#199c77"),
                }

            };
            Chart = new DonutChart()
            {
                Entries = chartEntries,
                IsAnimated = true,
                LabelTextSize = 14,
                AnimationDuration = TimeSpan.FromSeconds(4),
            };
        }

        [RelayCommand]
        private async Task GotoBreakfastMealPage()
        {
            if (SelectedDiet!=null)
            {
                if (CurrentDayDiet.IsExistsInDb==false)
                {
                    CurrentDayDiet.DietId = SelectedDiet.DietId ;
                }
                if (ExistingBreakfastMeal.IsExistsInDb == true)
                {
                    CurrentMeal = ExistingBreakfastMeal;
                }
                else
                {
                    CurrentMeal.MealTypeId = 0;
                }

#if ANDROID
            await Shell.Current.GoToAsync($"//{nameof(HomePage)}/{nameof(MealPage)}", animate: true);
#endif
            }
            else
            {
                await ShowToastAsync(AppResource.ResourceManager.GetString("Pleaseselectdiettype", CultureInfo.CurrentCulture) ?? "");

            }

        }
        
        [RelayCommand]
        private async Task GotoLunchMealPage()
        {
            if (SelectedDiet != null)
            {
                if (CurrentDayDiet.IsExistsInDb == false)
                {
                    CurrentDayDiet.DietId = SelectedDiet.DietId;
                }
                if (ExistingLunchMeal.IsExistsInDb == true)
                {
                    CurrentMeal = ExistingLunchMeal;
                }
                else
                {
                    CurrentMeal.MealTypeId = 1;
                }
#if ANDROID
            await Shell.Current.GoToAsync($"//{nameof(HomePage)}/{nameof(MealPage)}", animate: true);
#endif
            }
            else
            {
                await ShowToastAsync(AppResource.ResourceManager.GetString("Pleaseselectdiettype", CultureInfo.CurrentCulture) ?? "");
            }


        }
        
        [RelayCommand]
        private async Task GotoDinnerMealPage()
        {
            if (SelectedDiet != null)
            {
                if (CurrentDayDiet.IsExistsInDb == false)
                {
                    CurrentDayDiet.DietId = SelectedDiet.DietId;
                }
                if (ExistingDinnerMeal.IsExistsInDb == true)
                {
                    CurrentMeal = ExistingDinnerMeal;
                }
                else
                {
                    CurrentMeal.MealTypeId = 2;
                }
#if ANDROID
            await Shell.Current.GoToAsync($"//{nameof(HomePage)}/{nameof(MealPage)}", animate: true);
#endif
            }
            else
            {
                await ShowToastAsync(AppResource.ResourceManager.GetString("Pleaseselectdiettype", CultureInfo.CurrentCulture) ?? "");
            }


        }
        
        [RelayCommand]
        private async Task GotoSnaksMealPage()
        {
            if (SelectedDiet != null)
            {
                if (CurrentDayDiet.IsExistsInDb == false)
                {
                    CurrentDayDiet.DietId = SelectedDiet.DietId;
                }
                if (ExistingSnaksMeal.IsExistsInDb == true)
                {
                    CurrentMeal = ExistingSnaksMeal;
                }
                else
                {
                    CurrentMeal.MealTypeId = 3;
                }
#if ANDROID
            await Shell.Current.GoToAsync($"//{nameof(HomePage)}/{nameof(MealPage)}", animate: true);
#endif
            }
            else
            {
                await ShowToastAsync(AppResource.ResourceManager.GetString("Pleaseselectdiettype", CultureInfo.CurrentCulture) ?? "");
            }


        }

        [RelayCommand]
        private async Task GotoBodyMassAnalysisPage()
        {
            await GoToAsyncWithStack(nameof(BodyMassAnalysisPage), true);
        }

        [RelayCommand]
        private void SaveDiet()
        {

        }
        partial void OnIsKetoCheckedChanged(bool value)
        {
            if (IsKetoChecked)
            {
                IsLowCarbChecked = false;
                IsServingSizeChecked = false;
            }
        }
        partial void OnIsLowCarbCheckedChanged(bool value)
        {
            if (IsLowCarbChecked)
            {
                IsKetoChecked = false;
                IsServingSizeChecked = false;
            }
        }
        partial void OnIsServingSizeCheckedChanged(bool value)
        {
            if (IsServingSizeChecked)
            {
                IsKetoChecked = false;
                IsLowCarbChecked = false;
            }
        }
        [RelayCommand]
        private async Task GoToDietsPage()
        {
            await GoToAsyncWithStack(nameof(DietsPage), true);
        }

        [RelayCommand]
        private async Task ShowBottomSheet()
        {
            IsBottomSheetPresented = true;
        }

        partial void OnIsBottomSheetPresentedChanged(bool value)
        {
           IsTabbarVisible=IsBottomSheetPresented? false:true;
        }

        [RelayCommand]
        private async void GoToSettingPage()
        {
            await GoToAsyncWithStack(nameof(SettingPage), true);
        }

        [RelayCommand]
        private async Task GoProfilePage()
        {
            await GoToAsyncWithStack(nameof(ProfilePage), true);
        }

        private void BmiCalculator()
        {
            double mi = (Convert.ToDouble(Weight)) / ((Convert.ToDouble(Height) / 100) * (Convert.ToDouble(Height) / 100));

            BMI = Math.Round(mi, 2).ToString();
        }

        private void IdealWeightCalculator()
        {
            if (GenderIndex == 1)
            {
                double iw = ((((Convert.ToDouble(Height)) - 152.4) / 2.5) * 1.7) + 49;
                IdealWeight = Math.Round(iw, 2).ToString();
            }
            if (GenderIndex == 0)
            {
                double iw = ((((Convert.ToDouble(Height)) - 152.4) / 2.5) * 1.9) + 52;
                IdealWeight = Math.Round(iw, 2).ToString();
            }

        }

        private void ModifiedWeightCalculator()
        {
            double mi = (Convert.ToDouble(IdealWeight)) + (0.4 * ((Convert.ToDouble(Weight) - Convert.ToDouble(IdealWeight))));
            ModifiedWeight = Math.Round(mi, 2).ToString();
        }

        private void BodyActivityCalculator()
        {
            double bm = Convert.ToDouble(BMI);
            if (bm <= 18.5)
            {
                BodyActivity = "خامل";
            }
            else if (bm <= 18.5)
            {
                BodyActivity = "قليل النشاط";
            }
            else if (bm <= 18.5)
            {
                BodyActivity = "نشط";
            }
            else if (bm <= 18.5)
            {
                BodyActivity = "نشط جدا";
            }
        }

        private void TotalEnergyCalculator(int bodyActivityIndex)
        {
            double BodyActivityDouble = 0;

            if (bodyActivityIndex == 0)
            {

                if (Convert.ToDouble(BMI) < 18.5)
                {
                    BodyActivityDouble = 35;
                }
                else if (Convert.ToDouble(BMI) >= 18.5 && Convert.ToDouble(BMI) <= 24.9)
                {
                    BodyActivityDouble = 30;
                }
                else if (Convert.ToDouble(BMI) > 25 && Convert.ToDouble(BMI) <= 29.9)
                {
                    BodyActivityDouble = 20;
                }
                else if (Convert.ToDouble(BMI) >= 30)
                {
                    BodyActivityDouble = 15;
                }

            }
            else if (bodyActivityIndex == 1)
            {
                if (Convert.ToDouble(BMI) < 18.5)
                {
                    BodyActivityDouble = 40;
                }
                else if (Convert.ToDouble(BMI) >= 18.5 && Convert.ToDouble(BMI) <= 24.9)
                {
                    BodyActivityDouble = 35;
                }
                else if (Convert.ToDouble(BMI) > 25 && Convert.ToDouble(BMI) <= 29.9)
                {
                    BodyActivityDouble = 25;
                }
                else if (Convert.ToDouble(BMI) >= 30)
                {
                    BodyActivityDouble = 20;
                }

            }
            else if (bodyActivityIndex == 2)
            {
                if (Convert.ToDouble(BMI) < 18.5)
                {
                    BodyActivityDouble = 45;
                }
                else if (Convert.ToDouble(BMI) >= 18.5 && Convert.ToDouble(BMI) <= 24.9)
                {
                    BodyActivityDouble = 40;
                }
                else if (Convert.ToDouble(BMI) > 25 && Convert.ToDouble(BMI) <= 29.9)
                {
                    BodyActivityDouble = 30;
                }
                else if (Convert.ToDouble(BMI) >= 30)
                {
                    BodyActivityDouble = 25;
                }

            }
            else if (bodyActivityIndex == 3)
            {
                if (Convert.ToDouble(BMI) < 18.5)
                {
                    BodyActivityDouble = 50;
                }
                else if (Convert.ToDouble(BMI) >= 18.5 && Convert.ToDouble(BMI) <= 24.9)
                {
                    BodyActivityDouble = 45;
                }
                else if (Convert.ToDouble(BMI) > 25 && Convert.ToDouble(BMI) <= 29.9)
                {
                    BodyActivityDouble = 35;
                }
                else if (Convert.ToDouble(BMI) >= 30)
                {
                    BodyActivityDouble = 30;
                }

            }

            TotalEnergy = Math.Round((Convert.ToDouble(ModifiedWeight) * BodyActivityDouble), 2).ToString();

        }

        private void WaistCircumferenceEvaluationCalculator() 
        {
            if (App.currentMembership.WaistCircumferenceMeasurement < 94)
                WaistCircumferenceId = 1;
            else if (App.currentMembership.WaistCircumferenceMeasurement >=94 && App.currentMembership.WaistCircumferenceMeasurement <= 101)
                WaistCircumferenceId = 2;
            else if (App.currentMembership.WaistCircumferenceMeasurement >101)
                WaistCircumferenceId = 3;

            WaistCircumferenceName = App.waistCircumferences.Where(x => x.WaistCircumferenceId == WaistCircumferenceId).FirstOrDefault().WaistCircumferenceName;
        }

        private void ObesityDegreeCalculator()
        {
            if(Convert.ToDouble(BMI)>=18 &&Convert.ToDouble(BMI) <=24)
                ObesityDegreeId = 1;
            if(Convert.ToDouble(BMI)>24 &&Convert.ToDouble(BMI) <=29)
                ObesityDegreeId = 2;
            if(Convert.ToDouble(BMI)>29 &&Convert.ToDouble(BMI) <=34)
                ObesityDegreeId = 3;
            if(Convert.ToDouble(BMI)>34 &&Convert.ToDouble(BMI) <=39)
                ObesityDegreeId = 4;
            if(Convert.ToDouble(BMI)>39)
                ObesityDegreeId = 5;

            ObesityDegreeName = App.obesityDegrees.Where(x => x.ObesityDegreeId == ObesityDegreeId).FirstOrDefault().ObesityDegreeName;
        }

    }
}
//[QueryProperty(nameof(App.currentMembership), nameof(App.currentMembership))]
