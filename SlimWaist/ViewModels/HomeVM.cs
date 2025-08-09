using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microcharts;
using Microcharts.Maui;
using SkiaSharp;
using SlimWaist.Languages;
using SlimWaist.Models;
using SlimWaist.Models.Dto;
using SlimWaist.Views;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Globalization;
using System.Threading.Tasks;

namespace SlimWaist.ViewModels
{
    public partial class HomeVM(DataContext dataContext) : BaseVM
    {
        //load data
        [ObservableProperty]
        private List<Diet> _diets;
        [ObservableProperty]
        private List<BodyActivity> _bodyActivities;

        //private properties
        private readonly DataContext _dataContext = dataContext;
        private ChartEntry[] chartEntries;


        //static properties
        public static Membership CurrentMembership=new Membership();
        public static DayDiet? CurrentDayDiet { get; set; } = new DayDiet();

        public static Meal? CurrentMeal { get; set; } = new Meal();

        //ObservableProperty
        [ObservableProperty]
        private Chart _chart;
        [ObservableProperty]
        private float _percentConsumed;
        [ObservableProperty]
        private float _percentRemain;
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

        [ObservableProperty]
        private bool _isMembershipExists;




        public static Meal ExistingBreakfastMeal = new Meal();
        public static Meal ExistingLunchMeal = new Meal();
        public static Meal ExistingDinnerMeal = new Meal();
        public static Meal ExistingSnaksMeal = new Meal();
       
        
        
        private List<DayDiet> ExistingDayDiets = new List<DayDiet>();
        private DayDiet ExistingDayDiet;
        private bool isDateChanged = false;
        private bool isFirstCheckForExistingDayDiet = false;
        private DateTime ExistingDayDietDate { get; set; }
        private List<MealDetail> mealDetails { get; set; }
        private List<MealDetail> breakfasts { get; set; }
        private List<MealDetail> lunchs { get; set; }
        private List<MealDetail> dinners { get; set; }
        private List<MealDetail> snakss { get; set; }
        private List<Meal> meals { get; set; }
        private double BMI { get; set; }
        private double IdealWeight { get; set; }
        private double ModifiedWeight { get; set; }

        


        [ObservableProperty]
        private Diet _selectedDiet;

        [ObservableProperty]
        private string? _bodyActivity;







        [ObservableProperty]
        private int _selectedIndex;

        Setting setting;



        
        [ObservableProperty]
        private DateTime _selectedDate= new DateTime(
                                DateTime.Now.Year
                                , DateTime.Now.Month
                                , DateTime.Now.Day);



        public async Task init()
        {
            await CheckLoginSaved();

            Diets = App.Diets;
            BodyActivities = App.BodyActivities;

            TotalEnergy = "0";
            TotalRemainingEnergy = "0";
            TotalConsumedEnergy = "0";
            TotalBreakfastEnergy = "0";
            TotalLunchEnergy = "0";
            TotalDinnerEnergy = "0";
            TotalSnaksEnergy = "0";
            PercentConsumed = 0;
            PercentRemain = 100;
            IsBottomSheetPresented = false;
            IsTabbarVisible = true;

            if (CurrentMembership.IsExistsInDb)
            {
                IsMembershipExists = true;
                BmiCalculator();

                IdealWeightCalculator();

                ModifiedWeightCalculator();

                TotalEnergyCalculator(HomeVM.CurrentMembership.BodyActivityId);

                HomeVM.CurrentDayDiet = _dataContext.Database.Table<DayDiet>()
.Where(x => x.MembershipId == CurrentMembership.Id)
.Where(x => x.DayDietDate == SelectedDate).FirstOrDefault() ?? new DayDiet() {MembershipId=CurrentMembership.Id,
    DayDietDate = SelectedDate };


                if (HomeVM.CurrentDayDiet.IsExistsInDb)
                {
                    BodyActivity = BodyActivities.Where(x => x.BodyActivityId == CurrentMembership.BodyActivityId).FirstOrDefault().BodyActivityName;

                    SelectedDate = HomeVM.CurrentDayDiet.DayDietDate;

                    meals = _dataContext.Database.Table<Meal>().Where(
                        x => x.DayDietId == HomeVM.CurrentDayDiet.DayDietId).ToList() ?? new List<Meal>();

                    mealDetails = _dataContext.Database.Table<MealDetail>().Where(
                        x => x.DayDietId == HomeVM.CurrentDayDiet.DayDietId).ToList() ?? new List<MealDetail>();

                    SelectedDiet = Diets.Where(x => x.DietId == HomeVM.CurrentDayDiet.DietId).FirstOrDefault();

                    ExistingBreakfastMeal = meals.Where(x => x.MealTypeId == 0).FirstOrDefault() ?? new Meal();
                    ExistingLunchMeal = meals.Where(x => x.MealTypeId == 1).FirstOrDefault() ?? new Meal();
                    ExistingDinnerMeal = meals.Where(x => x.MealTypeId == 2).FirstOrDefault() ?? new Meal();
                    ExistingSnaksMeal = meals.Where(x => x.MealTypeId == 3).FirstOrDefault() ?? new Meal();

                    if (ExistingBreakfastMeal.IsExistsInDb == true)
                    {
                        breakfasts = mealDetails.Where(x => x.MealId == ExistingBreakfastMeal.MealId).ToList() ?? new List<MealDetail>();

                        if (breakfasts.Count > 0)
                        {
                            foreach (var mealDetail in breakfasts.Where(x => x.MealId == ExistingBreakfastMeal.MealId).ToList())
                            {
                                var foodFromMealDetail = _dataContext.Database.Table<Food>().Where(x => x.FoodId == mealDetail.FoodId).FirstOrDefault();

                                TotalBreakfastEnergy = (Convert.ToDouble(TotalBreakfastEnergy) + Math.Round((foodFromMealDetail.FoodCalories * Convert.ToDouble(mealDetail.Quantity) / 100), 1)).ToString("F1");
                            }
                        }
                    }
                    if (ExistingLunchMeal.IsExistsInDb == true)
                    {
                        lunchs = mealDetails.Where(x => x.MealId == ExistingLunchMeal.MealId).ToList() ?? new List<MealDetail>();

                        if (lunchs.Count > 0)
                        {
                            foreach (var mealDetail in lunchs.Where(x => x.MealId == ExistingLunchMeal.MealId).ToList())
                            {
                                var foodFromMealDetail = _dataContext.Database.Table<Food>().Where(x => x.FoodId == mealDetail.FoodId).FirstOrDefault();

                                TotalLunchEnergy = (Convert.ToDouble(TotalLunchEnergy) + Math.Round((foodFromMealDetail.FoodCalories * Convert.ToDouble(mealDetail.Quantity) / 100), 1)).ToString("F1");
                            }
                        }
                    }
                    if (ExistingDinnerMeal.IsExistsInDb == true)
                    {
                        dinners = mealDetails.Where(x => x.MealId == ExistingDinnerMeal.MealId).ToList() ?? new List<MealDetail>();

                        if (dinners.Count > 0)
                        {
                            foreach (var mealDetail in dinners.Where(x => x.MealId == ExistingDinnerMeal.MealId).ToList())
                            {
                                var foodFromMealDetail = _dataContext.Database.Table<Food>().Where(x => x.FoodId == mealDetail.FoodId).FirstOrDefault();

                                TotalDinnerEnergy = (Convert.ToDouble(TotalDinnerEnergy) + Math.Round((foodFromMealDetail.FoodCalories * Convert.ToDouble(mealDetail.Quantity) / 100), 1)).ToString("F1");
                            }
                        }
                    }
                    if (ExistingSnaksMeal.IsExistsInDb == true)
                    {
                        snakss = mealDetails.Where(x => x.MealId == ExistingSnaksMeal.MealId).ToList() ?? new List<MealDetail>();

                        if (snakss.Count > 0)
                        {
                            foreach (var mealDetail in snakss.Where(x => x.MealId == ExistingSnaksMeal.MealId).ToList())
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
                        Convert.ToDouble(TotalSnaksEnergy), 1).ToString("F1");

                    TotalRemainingEnergy = Math.Round(
                        Convert.ToDouble(TotalEnergy) -
                        Convert.ToDouble(TotalConsumedEnergy), 1).ToString("F1");
                   
                    PercentRemain = (float)((Convert.ToDouble(TotalRemainingEnergy) / Convert.ToDouble(TotalEnergy) * 100));

                    PercentConsumed = (float)Math.Round((Convert.ToDouble(TotalConsumedEnergy) / Convert.ToDouble(TotalEnergy) * 100), 1);

                }
                else
                {
                    HomeVM.CurrentDayDiet.MembershipId = CurrentMembership.Id;
                    HomeVM.CurrentDayDiet.DayDietDate = SelectedDate;

                    ExistingBreakfastMeal = new Meal();
                    ExistingLunchMeal = new Meal();
                    ExistingDinnerMeal = new Meal();
                    ExistingSnaksMeal = new Meal();
                }

            }
            else
            {
                IsMembershipExists = false;

            }

            chartEntries = new ChartEntry[]
            {
                
                //red first
                new ChartEntry(PercentConsumed)
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




            //if (isFirstCheckForExistingDayDiet==false)
            //{
            //    SelectedDate = new DateTime(
            //                DateTime.Now.Year
            //                , DateTime.Now.Month
            //                , DateTime.Now.Day);

            //    HomeVM.CurrentDayDiet = _dataContext.Database.Table<DayDiet>()
            //        .Where(x => x.MembershipId == CurrentMembership.Id)
            //        .Where(x => x.DayDietDate == SelectedDate).FirstOrDefault() ?? new DayDiet();
            //    isFirstCheckForExistingDayDiet = true;
            //}

        }

        private void BmiCalculator()
        {
            double mi = (Convert.ToDouble(CurrentMembership.Weight)) / ((Convert.ToDouble(CurrentMembership.Height) / 100) * (Convert.ToDouble(CurrentMembership.Height) / 100));

            BMI = Math.Round(mi, 2);
        }

        private void IdealWeightCalculator()
        {
            if (CurrentMembership.GenderId == 0)
            {
                //male
                double iw = ((((Convert.ToDouble(CurrentMembership.Height )) - 152.4) / 2.5) * 1.7) + 49;
                IdealWeight = Math.Round(iw, 2);
            }
            if (CurrentMembership.GenderId == 1)
            {
                //female
                double iw = ((((Convert.ToDouble(CurrentMembership.Height)) - 152.4) / 2.5) * 1.9) + 52;
                IdealWeight = Math.Round(iw, 2);
            }

        }

        private void ModifiedWeightCalculator()
        {
            double mi = (Convert.ToDouble(IdealWeight)) + (0.4 * ((Convert.ToDouble(CurrentMembership. Weight) - Convert.ToDouble(IdealWeight))));
            ModifiedWeight = Math.Round(mi, 2);
        }

        private void TotalEnergyCalculator(int bodyActivityIndex)
        {
            double BodyActivityDouble = 0;

            if (CurrentMembership.BodyActivityId == 1)
            {

                if (BMI < 18.5)
                {
                    BodyActivityDouble = 35;
                }
                else if (BMI >= 18.5 && BMI <= 24.9)
                {
                    BodyActivityDouble = 30;
                }
                else if (BMI > 25 && BMI <= 29.9)
                {
                    BodyActivityDouble = 20;
                }
                else if (BMI >= 30)
                {
                    BodyActivityDouble = 15;
                }

            }
            else if (CurrentMembership.BodyActivityId==2)
            {
                if (BMI < 18.5)
                {
                    BodyActivityDouble = 40;
                }
                else if (BMI >= 18.5 && BMI <= 24.9)
                {
                    BodyActivityDouble = 35;
                }
                else if (BMI > 25 && BMI <= 29.9)
                {
                    BodyActivityDouble = 25;
                }
                else if (BMI >= 30)
                {
                    BodyActivityDouble = 20;
                }

            }
            else if (CurrentMembership.BodyActivityId==3)
            {
                if (BMI < 18.5)
                {
                    BodyActivityDouble = 45;
                }
                else if (BMI >= 18.5 && BMI <= 24.9)
                {
                    BodyActivityDouble = 40;
                }
                else if (BMI > 25 && BMI <= 29.9)
                {
                    BodyActivityDouble = 30;
                }
                else if (BMI >= 30)
                {
                    BodyActivityDouble = 25;
                }

            }
            else if (CurrentMembership.BodyActivityId==4)
            {
                if (BMI < 18.5)
                {
                    BodyActivityDouble = 50;
                }
                else if (BMI >= 18.5 && BMI <= 24.9)
                {
                    BodyActivityDouble = 45;
                }
                else if (BMI > 25 && BMI <= 29.9)
                {
                    BodyActivityDouble = 35;
                }
                else if (BMI >= 30)
                {
                    BodyActivityDouble = 30;
                }

            }

            TotalEnergy = Math.Round((Convert.ToDouble(ModifiedWeight) * BodyActivityDouble), 2).ToString();

        }

        private async Task CheckLoginSaved()
        {
            Preferences.Set("Email", "");

            if (App.setting.SavedMembershipId != 0)
            {
                CurrentMembership = _dataContext.Database.Table<Membership>().Where(x => x.Id == App.setting.SavedMembershipId).FirstOrDefault();

            }
        }

        [RelayCommand]
        private async Task GotoLoginPage()
        {
           await Shell.Current.GoToAsync($"//{nameof(LoginPage)}", animate: true);

        }
        [RelayCommand]
        private async Task LogOut()
        {
            CurrentMembership = new Membership();
            CurrentDayDiet= new DayDiet();
            App.setting.SavedMembershipId = 0;
            await _dataContext.UpdateAsync<Setting>(App.setting);
            SelectedDiet = null;
            await init();
        }

        partial void OnIsBottomSheetPresentedChanged(bool value)
        {
            IsTabbarVisible = IsBottomSheetPresented ? false : true;

            //reset picker to the first choice if cancel the bottom sheet without changing the diet type
            //better than cancel botton because its firred if the user press on the screen to hide the botton sheet
            if (CurrentDayDiet.IsExistsInDb)
            {
                if (IsBottomSheetPresented == false)
                {
                    if ((SelectedIndex + 1) != CurrentDayDiet.DietId)
                    {
                        SelectedIndex = CurrentDayDiet.DietId - 1;
                    }

                }
            }
        }

        partial void OnSelectedIndexChanged(int value)
        {
            if (CurrentMembership.IsExistsInDb)
            {
                if (CurrentDayDiet.IsExistsInDb)
                {
                    if ((SelectedIndex + 1) != CurrentDayDiet.DietId)
                    {
                        if (IsBottomSheetPresented == false)
                        {
                            IsBottomSheetPresented = true;
                        }
                    }
                }
                else
                {
                    IsBottomSheetPresented = true;
                }

            }
        }
      
        [RelayCommand]
        private async Task DatePicked(DateTime dateTime)
        {
            SelectedDate = dateTime;

        ////    if (App.IsFromTablesPage==false)
        ////    {
        ////        HomeVM.CurrentDayDiet = _dataContext.Database.Table<DayDiet>()
        ////.Where(x => x.MembershipId == CurrentMembership.Id)
        ////.Where(x => x.DayDietDate == SelectedDate).FirstOrDefault() ?? new DayDiet() { DayDietDate = SelectedDate };

        ////        await init();
        ////    }
            await init();
            //if (HomeVM.CurrentDayDiet.IsExistsInDb)
            //{
            //    SelectedDiet = Diets.Where(x => x.DietId == HomeVM.CurrentDayDiet.DietId).FirstOrDefault();
            //}
            //else
            //{
            //    //SelectedDiet = null;
            //}
        }
        
        [RelayCommand]
        private async Task DeleteDayDiet()
        {
            if (HomeVM.CurrentDayDiet.IsExistsInDb)
            {
                if ((SelectedIndex + 1) != HomeVM.CurrentDayDiet.DietId)
                {
                    HomeVM.CurrentDayDiet.IsExistsInDb = false;

                    await _dataContext.DeleteAsync<DayDiet>(HomeVM.CurrentDayDiet);

                    if (ExistingBreakfastMeal.IsExistsInDb == true)
                    {
                        foreach (var mealDetail in breakfasts)
                        {
                            await _dataContext.DeleteAsync<MealDetail>(mealDetail);
                        }

                        ExistingBreakfastMeal.IsExistsInDb = false;

                        await _dataContext.DeleteAsync<Meal>(ExistingBreakfastMeal);
                    }
                    if (ExistingLunchMeal.IsExistsInDb == true)
                    {
                        foreach (var mealDetail in lunchs)
                        {
                            await _dataContext.DeleteAsync<MealDetail>(mealDetail);
                        }

                        ExistingLunchMeal.IsExistsInDb = false;

                        await _dataContext.DeleteAsync<Meal>(ExistingLunchMeal);
                    }
                    if (ExistingDinnerMeal.IsExistsInDb == true)
                    {
                        foreach (var mealDetail in dinners)
                        {
                            await _dataContext.DeleteAsync<MealDetail>(mealDetail);
                        }
                        ExistingDinnerMeal.IsExistsInDb = false;

                        await _dataContext.DeleteAsync<Meal>(ExistingDinnerMeal);
                    }
                    if (ExistingSnaksMeal.IsExistsInDb == true)
                    {
                        foreach (var mealDetail in snakss)
                        {
                            await _dataContext.DeleteAsync<MealDetail>(mealDetail);
                        }

                        ExistingSnaksMeal.IsExistsInDb = false;

                        await _dataContext.DeleteAsync<Meal>(ExistingSnaksMeal);
                    }


                    IsBottomSheetPresented = false;

                    await init();

                    SelectedDiet = Diets.Where(x => x.DietId == SelectedIndex+1).FirstOrDefault();
                }

            }


        }

        [RelayCommand]
        private async Task HideBottomSheet()
        {
            IsBottomSheetPresented = false;
        }

        [RelayCommand]
        private async Task GotoBreakfastMealPage()
        {
            if (CurrentMembership.IsExistsInDb)
            {
                if (SelectedDiet != null)
                {
                    if (HomeVM.CurrentDayDiet.IsExistsInDb == false)
                    {
                        HomeVM.CurrentDayDiet.DietId = SelectedDiet.DietId;
                    }

                    if (ExistingBreakfastMeal.IsExistsInDb == true)
                    {
                        CurrentMeal.DayDietId = HomeVM.CurrentDayDiet.DayDietId;

                        CurrentMeal = ExistingBreakfastMeal;
                    }
                    else
                    {
                        CurrentMeal = new Meal()
                        {
                            MealTypeId = 0
                        };
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
            else
            {

            }


        }
        
        [RelayCommand]
        private async Task GotoLunchMealPage()
        {
            if (CurrentMembership.IsExistsInDb)
            {
                if (SelectedDiet != null)
                {
                    if (HomeVM.CurrentDayDiet.IsExistsInDb == false)
                    {
                        HomeVM.CurrentDayDiet.DietId = SelectedDiet.DietId;
                    }
                    if (ExistingLunchMeal.IsExistsInDb == true)
                    {
                        CurrentMeal.DayDietId = HomeVM.CurrentDayDiet.DayDietId;

                        CurrentMeal = ExistingLunchMeal;
                    }
                    else
                    {
                        CurrentMeal = new Meal()
                        {
                            MealTypeId = 1
                        };
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
            else
            {

            }



        }
        
        [RelayCommand]
        private async Task GotoDinnerMealPage()
        {
            if (CurrentMembership.IsExistsInDb)
            {
                if (SelectedDiet != null)
                {
                    if (HomeVM.CurrentDayDiet.IsExistsInDb == false)
                    {
                        HomeVM.CurrentDayDiet.DietId = SelectedDiet.DietId;
                    }
                    if (ExistingDinnerMeal.IsExistsInDb == true)
                    {
                        CurrentMeal.DayDietId = HomeVM.CurrentDayDiet.DayDietId;

                        CurrentMeal = ExistingDinnerMeal;
                    }
                    else
                    {
                        CurrentMeal = new Meal()
                        {
                            MealTypeId = 2
                        };
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
            else
            {

            }



        }
        
        [RelayCommand]
        private async Task GotoSnaksMealPage()
        {
            if (CurrentMembership.IsExistsInDb)
            {
                if (SelectedDiet != null)
                {
                    if (HomeVM.CurrentDayDiet.IsExistsInDb == false)
                    {
                        HomeVM.CurrentDayDiet.DietId = SelectedDiet.DietId;
                    }
                    if (ExistingSnaksMeal.IsExistsInDb == true)
                    {
                        CurrentMeal.DayDietId = HomeVM.CurrentDayDiet.DayDietId;

                        CurrentMeal = ExistingSnaksMeal;
                    }
                    else
                    {
                        CurrentMeal = new Meal()
                        {
                            MealTypeId = 3
                        };
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
            else
            {

            }



        }

        [RelayCommand]
        private async Task GotoBodyMassAnalysisPage()
        {
            if (CurrentMembership.IsExistsInDb)
            {
                await GoToAsyncWithStack(nameof(ProfilePage), true);
            }
            else
            {

            }
        }

        [RelayCommand]
        private async Task GoProfilePage()
        {
            if (CurrentMembership.IsExistsInDb)
            {
                await GoToAsyncWithStack(nameof(ProfilePage), true);
            }
            else
            {
                
            }
        }

    }
}
//[QueryProperty(nameof(CurrentMembership), nameof(CurrentMembership))]
