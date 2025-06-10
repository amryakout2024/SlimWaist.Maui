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

        public async Task init()
        {
            //Preferences.Set("Email", "");

            Diets = await App.dataContext.LoadAsync<Diet>();


            Dateday = Convert.ToInt32( DateTime.Now.Day);
            Datemonth = Convert.ToInt32(DateTime.Now.Month);
            Dateyear = Convert.ToInt32(DateTime.Now.Year);

            IsBottomSheetPresented = false;
            IsTabbarVisible = true;
            BodyActivities = App.BodyActivities;

            Name = App.currentMembership?.Name ?? "";

            Weight = App.currentMembership?.Weight.ToString() ?? "";

            Height = App.currentMembership?.Height.ToString() ?? "";

            BirthDate = App.currentMembership?.BirthDateDay.ToString() ?? "";
            
            BodyActivity = BodyActivities.Where(x=>x.BodyActivityId== App.currentMembership.BodyActivityId).FirstOrDefault().BodyActivityName;

            WaistCircumferenceMeasurement = App.currentMembership.WaistCircumferenceMeasurement.ToString();
            
            BmiCalculator();

            IdealWeightCalculator();

            TargetedWeight = IdealWeight;

            ModifiedWeightCalculator();

            //BodyActivityCalculator();

            TotalEnergyCalculator(App.currentMembership.BodyActivityId);

            WaistCircumferenceEvaluationCalculator();

            ObesityDegreeCalculator();

            chartEntries = new ChartEntry[]
            {
                new ChartEntry((float)Math.Round(Convert.ToDouble(Weight),2))
                {
                    Label=AppResource.ResourceManager.GetString( "Weight",CultureInfo.CurrentCulture),
                    ValueLabel=Weight,
                    Color=SKColor.Parse("#127a0f"),
                    TextColor=SKColor.Parse("#127a0f")
                },
                new ChartEntry((float) Math.Round(Convert.ToDouble(IdealWeight),2))
                {
                    Label=AppResource.ResourceManager.GetString( "Idealweight",CultureInfo.CurrentCulture),
                    ValueLabel=IdealWeight,
                    Color=SKColor.Parse("#127a0f"),
                    TextColor=SKColor.Parse("#127a0f")
                },
                new ChartEntry((float)Math.Round( Convert.ToDouble(ModifiedWeight),2))
                {
                    Label=AppResource.ResourceManager.GetString( "Modifiedweight",CultureInfo.CurrentCulture),
                    ValueLabel=ModifiedWeight,
                    Color=SKColor.Parse("#127a0f"),
                    TextColor=SKColor.Parse("#127a0f")
                },
            };

            Chart = new BarChart()
            {
                Entries = chartEntries,
                IsAnimated = true,
                LabelTextSize = 14,
                ValueLabelTextSize = 14,
                SerieLabelTextSize = 14,
                LabelOrientation = Orientation.Vertical,
                AnimationDuration = TimeSpan.FromSeconds(4),
            };

            var mealsCount = _dataContext.Database.Table<Meal>().ToList().Count();
            var v1 = _dataContext.Database.Table<MealDetail>().ToList().Count();

            CurrentMeal = new Meal()
            {
                MealId =(mealsCount==0)?1: _dataContext.Database.Table<Meal>().ToList().Select(x => x.MealId).ToList().Max() + 1,
                MembershipId=App.currentMembership.Id,
                MealDateDay=Dateday,
                MealDateMonth=Datemonth,
                MealDateYear=Dateyear
            };

        }

        [RelayCommand]
        private async Task GotoBreakfastMealPage()
        {
            var existingMeal = _dataContext.Database.Table<Meal>()
                .Where(x => x.MembershipId == App.currentMembership.Id
                && x.MealDateDay == Dateday
                && x.MealDateMonth == Datemonth
                && x.MealDateYear == Dateyear
                && x.MealTypeId==0).FirstOrDefault();

            if (existingMeal is not null)
            {
                CurrentMeal = existingMeal;
            }
            else
            {
                CurrentMeal.MealTypeId = 0;
            }

#if ANDROID
            await Shell.Current.GoToAsync($"//{nameof(HomePage)}/{nameof(MealPage)}", animate: true);
#endif
        }
        [RelayCommand]
        private async Task GotoLunchMealPage()
        {
            var existingMeal = _dataContext.Database.Table<Meal>()
                .Where(x => x.MembershipId == App.currentMembership.Id
                && x.MealDateDay == Dateday
                && x.MealDateMonth == Datemonth
                && x.MealDateYear == Dateyear
                && x.MealTypeId == 1).FirstOrDefault();

            if (existingMeal is not null)
            {
                CurrentMeal = existingMeal;
            }
            else
            {
                CurrentMeal.MealTypeId = 1;
            }
#if ANDROID
            await Shell.Current.GoToAsync($"//{nameof(HomePage)}/{nameof(MealPage)}", animate: true);
#endif
        }
        [RelayCommand]
        private async Task GotoDinnerMealPage()
        {
            var existingMeal = _dataContext.Database.Table<Meal>()
                .Where(x => x.MembershipId == App.currentMembership.Id
                && x.MealDateDay == Dateday
                && x.MealDateMonth == Datemonth
                && x.MealDateYear == Dateyear
                && x.MealTypeId == 2).FirstOrDefault();

            if (existingMeal is not null)
            {
                CurrentMeal = existingMeal;
            }
            else
            {
                CurrentMeal.MealTypeId = 2;
            }
#if ANDROID
            await Shell.Current.GoToAsync($"//{nameof(HomePage)}/{nameof(MealPage)}", animate: true);
#endif
        }
        [RelayCommand]
        private async Task GotoSnaksMealPage()
        {
            var existingMeal = _dataContext.Database.Table<Meal>()
                .Where(x => x.MembershipId == App.currentMembership.Id
                && x.MealDateDay == Dateday
                && x.MealDateMonth == Datemonth
                && x.MealDateYear == Dateyear
                && x.MealTypeId == 3).FirstOrDefault();

            if (existingMeal is not null)
            {
                CurrentMeal = existingMeal;
            }
            else
            {
                CurrentMeal.MealTypeId = 3;
            }
#if ANDROID
            await Shell.Current.GoToAsync($"//{nameof(HomePage)}/{nameof(MealPage)}", animate: true);
#endif
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
