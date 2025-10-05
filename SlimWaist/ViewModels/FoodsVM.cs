using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using SlimWaist.Languages;
using SlimWaist.Models;
using SlimWaist.Views;
using System.Globalization;
using System.Reflection.Metadata;

namespace SlimWaist.ViewModels
{
    public partial class FoodsVM(DataContext dataContext) : BaseVM
    {
        private readonly DataContext _dataContext = dataContext;
        
        [ObservableProperty]
        private string _mealTypeName;

        private List<Food> FoodsFromDatabase;

        [ObservableProperty]
        private List<Food> _foods;

        [ObservableProperty]
        private List<Meal> _meals;

        [ObservableProperty]
        private List<Meal> _mealsFromDatabase;

        [ObservableProperty]
        private string _mealType;

        [ObservableProperty]
        private string _Nutritions;

        [ObservableProperty]
        private string _readyMeals;

        [ObservableProperty]
        private string _searchName1;

        [ObservableProperty]
        private string _searchName2;

        [ObservableProperty]
        private Food _selectedFood;

        [ObservableProperty]
        private bool _isBottomSheetPresented;

        [ObservableProperty]
        private string _foodName;

        [ObservableProperty]
        private string _quantity;

        [ObservableProperty]
        private string _foodCalories;

        [ObservableProperty]
        private string _totalMealCalories;

        [ObservableProperty]
        private string _foodCarb;

        [ObservableProperty]
        private string _foodProtien;

        [ObservableProperty]
        private string _foodFat;

        [ObservableProperty]
        private string _foodCategory;

        [ObservableProperty]
        private string _foodFibers;

        [ObservableProperty]
        private bool _isMealDetailExist = false;

        [ObservableProperty]
        private bool _isNutritionsChecked=true;

        [ObservableProperty]
        private bool _isReadymealsChecked=false;

        public async Task Init()
        {
            MealTypeName = App.mealTypes.Where(x => x.MealTypeId == HomeVM.CurrentMeal.MealTypeId).FirstOrDefault()?.MealTypeName ?? "";

            FoodsFromDatabase = await _dataContext.GetAsync<Food>();

            //need definition in the database to make the query awaitable
            MealsFromDatabase = _dataContext.Database.Table<Meal>().Where(x=>x.MealName!=string.Empty).ToList();

            var MealsFromDatabase2 = _dataContext.Database.Table<Meal>().ToList();
            Foods = FoodsFromDatabase;

            Meals = MealsFromDatabase;

            IsBottomSheetPresented = false;
            

            if (HomeVM.CurrentMembership.CultureInfo=="ar-SA")
            {
                Nutritions = "المغذيات";
                ReadyMeals = "وجبات جاهزة";
            }
            else
            {
                Nutritions = "Nutritions";
                ReadyMeals = "Ready meals";
            }

            UpdateTotalMealCalories();
        }

        private async Task UpdateTotalMealCalories()
        {
            TotalMealCalories = "0";

            var mealDetails = _dataContext.Database.Table<MealDetail>().Where(x => x.MealId == HomeVM.CurrentMeal.MealId).ToList();

            if (mealDetails.Count > 0)
            {
                foreach (var mealDetail in mealDetails)
                {
                    var oneFoodMealCalories = _dataContext.Database.Table<Food>().Where(x => x.FoodId == mealDetail.FoodId).Select(x => x.FoodCalories).FirstOrDefault();

                    TotalMealCalories = (Convert.ToDouble(TotalMealCalories) + Math.Round((Convert.ToDouble(oneFoodMealCalories) * Convert.ToDouble(mealDetail.Quantity) / 100), 1)).ToString("F1");
                }
            }

            HomeVM.CurrentMeal.TotalMealCalories = TotalMealCalories;

            await _dataContext.UpdateAsync<Meal>(HomeVM.CurrentMeal);

        }

        [RelayCommand]
        private async Task AddReadyMealToMeal(Meal meal)
        {
            if (HomeVM.CurrentDayDiet.IsExistsInDb==false)
            {
                HomeVM.CurrentDayDiet.IsExistsInDb = true;

                await _dataContext.InsertAsync<DayDiet>(HomeVM.CurrentDayDiet);
            }

            if (HomeVM.CurrentMeal.IsExistsInDb==false)
            {
                var CurrentDayDietId = _dataContext.Database.Table<DayDiet>()
                                        .Where(x => x.MembershipId == HomeVM.CurrentMembership.Id)
                                        .Where(x => x.DayDietDate == HomeVM.CurrentDayDiet.DayDietDate).Select(x => x.DayDietId).FirstOrDefault();

                HomeVM.CurrentMeal.DayDietId = CurrentDayDietId;

                HomeVM.CurrentMeal.IsExistsInDb = true;
                HomeVM.CurrentMeal.DayDietDate = HomeVM.CurrentDayDiet.DayDietDate;
                await App._dataContext.InsertAsync<Meal>(HomeVM.CurrentMeal);
            }

            
            var AddNewMealDetails = _dataContext.Database.Table<MealDetail>()
                        .Where(x => x.MealId == meal.MealId).ToList();

            var existingMealDetails = _dataContext.Database.Table<MealDetail>()
                        .Where(x => x.MealId == HomeVM.CurrentMeal.MealId ).ToList();
            foreach (MealDetail existingMealDetail in existingMealDetails)
            {
                foreach (MealDetail addNewMealDetail in AddNewMealDetails)
                {
                    if (existingMealDetail.FoodId==addNewMealDetail.FoodId)
                    {
                        existingMealDetail.Quantity = existingMealDetail.Quantity + addNewMealDetail.Quantity;
                        await _dataContext.UpdateAsync<MealDetail>(existingMealDetail);
                    }
                    else
                    {
                        await _dataContext.InsertAsync<MealDetail>(addNewMealDetail);
                    }
                }
            }

            await UpdateTotalMealCalories();

            await ShowToastAsync(AppResource.ResourceManager.GetString("Addedsuccessfully", CultureInfo.CurrentCulture) ?? "");

            await App._dataContext.UpdateAsync<Meal>(HomeVM.CurrentMeal);
#if ANDROID
                    Shell.Current.GoToAsync($"//{nameof(HomePage)}/{nameof(MealPage)}", animate: true);
#endif

        }

        [RelayCommand]
        private async Task AddFoodToMeal()
        {
            if (HomeVM.CurrentDayDiet.IsExistsInDb==false)
            {
                HomeVM.CurrentDayDiet.IsExistsInDb = true;

                await _dataContext.InsertAsync<DayDiet>(HomeVM.CurrentDayDiet);
            }

            if (HomeVM.CurrentMeal.IsExistsInDb==false)
            {
                var CurrentDayDietId = _dataContext.Database.Table<DayDiet>()
                                        .Where(x => x.MembershipId == HomeVM.CurrentMembership.Id)
                                        .Where(x => x.DayDietDate == HomeVM.CurrentDayDiet.DayDietDate).Select(x => x.DayDietId).FirstOrDefault();

                HomeVM.CurrentMeal.DayDietId = CurrentDayDietId;

                HomeVM.CurrentMeal.IsExistsInDb = true;

                await App._dataContext.InsertAsync<Meal>(HomeVM.CurrentMeal);
            }

            var existingMealDetail = _dataContext.Database.Table<MealDetail>()
                        .Where(x => x.MealId == HomeVM.CurrentMeal.MealId &&
                         x.FoodId == SelectedFood.FoodId).FirstOrDefault();

            if (existingMealDetail is not null)
            {
                if (Convert.ToInt32(Quantity) > 0)
                {
                    existingMealDetail.Quantity = Convert.ToInt32(Quantity);

                    await App._dataContext.UpdateAsync(existingMealDetail);

                    IsBottomSheetPresented = false;

                    await UpdateTotalMealCalories();

                    await ShowToastAsync(AppResource.ResourceManager.GetString("Updatedsuccessfully", CultureInfo.CurrentCulture) ?? "");

#if ANDROID
                    Shell.Current.GoToAsync($"//{nameof(HomePage)}/{nameof(MealPage)}", animate: true);
#endif

                }
                else
                {
                    await App._dataContext.DeleteAsync<MealDetail>(existingMealDetail);

                    //delete meal

                    var mealDetails = _dataContext.Database.Table<MealDetail>().Where(x => x.MealId == HomeVM.CurrentMeal.MealId).ToList();
                   
                    //delete day

                    if (mealDetails.Count < 1)
                    {
                        HomeVM.CurrentMeal.IsExistsInDb = false;

                        await App._dataContext.DeleteAsync<Meal>(HomeVM.CurrentMeal);
                    
                        switch (HomeVM.CurrentMeal.MealTypeId)
                        {
                            case 0:HomeVM.ExistingBreakfastMeal.IsExistsInDb = false;
                                break;
                            case 1:HomeVM.ExistingLunchMeal.IsExistsInDb = false;
                                break;
                            case 2:HomeVM.ExistingDinnerMeal.IsExistsInDb = false;
                                break;
                            case 3:HomeVM.ExistingSnaksMeal.IsExistsInDb = false;
                                break;
                        }

                        if (HomeVM.ExistingBreakfastMeal.IsExistsInDb == false &&
                            HomeVM.ExistingLunchMeal.IsExistsInDb == false &&
                            HomeVM.ExistingDinnerMeal.IsExistsInDb == false &&
                            HomeVM.ExistingSnaksMeal.IsExistsInDb == false)
                        {
                            HomeVM.CurrentDayDiet.IsExistsInDb = false;
                            await App._dataContext.DeleteAsync<DayDiet>(HomeVM.CurrentDayDiet);
                        }
                    }

                    IsBottomSheetPresented = false;

                    await UpdateTotalMealCalories();

                    await ShowToastAsync(AppResource.ResourceManager.GetString("Deletedsuccessfully", CultureInfo.CurrentCulture) ?? "");

#if ANDROID
                    Shell.Current.GoToAsync($"//{nameof(HomePage)}/{nameof(MealPage)}", animate: true);
#endif


                }

            }
            else
            {
                var mealDetail = new MealDetail()
                {
                    DayDietId = HomeVM.CurrentDayDiet.DayDietId,
                    FoodId = SelectedFood.FoodId,
                    MealId = HomeVM.CurrentMeal.MealId,
                    Quantity = Convert.ToDouble(Quantity)
                };

                await App._dataContext.InsertAsync<MealDetail>(mealDetail);
                
                await UpdateTotalMealCalories();

                IsBottomSheetPresented = false;

                await ShowToastAsync(AppResource.ResourceManager.GetString("Addedsuccessfully", CultureInfo.CurrentCulture) ?? "");
#if ANDROID
                Shell.Current.GoToAsync($"//{nameof(HomePage)}/{nameof(MealPage)}", animate: true);
#endif


            }


        }

        [RelayCommand]
        private async Task Search()
        {
            if (IsNutritionsChecked)
            {
                if (!string.IsNullOrEmpty(SearchName1))
                {
                    Foods = FoodsFromDatabase.Where(x => x.FoodName.Contains(SearchName1)).ToList();
                }
                else
                {
                    Foods = FoodsFromDatabase;
                }

            }
            else if (IsReadymealsChecked)
            {
                if (!string.IsNullOrEmpty(SearchName2))
                {
                    Meals = MealsFromDatabase.Where(x => x.MealName.Contains(SearchName2)).ToList();
                }
                else
                {
                    Meals = MealsFromDatabase;
                }
            }
        }

        partial void OnIsNutritionsCheckedChanged(bool value)
        {
            SearchName1 = "";
            SearchName2 = "";
            Search();

        }

        //no need only one enough
        //partial void OnIsReadymealsCheckedChanged(bool value)
        //{
        //    SearchName1 = "";
        //    SearchName2 = "";
        //    Search();
        //}

        [RelayCommand]
        private async Task DeleteMealDetail()
        {
            if (!string.IsNullOrEmpty(SearchName1))
            {
                Foods = FoodsFromDatabase.Where(x => x.FoodName.Contains(SearchName1)).ToList();
            }
            else
            {
                Foods = FoodsFromDatabase;
            }
        }

        [RelayCommand]
        private async Task ShowBottomSheet(Food food)
        {
            IsMealDetailExist = false;

            SelectedFood = food;

            FoodName = SelectedFood.FoodName ?? "";

            FoodCategory = SelectedFood.FoodCategory ?? "";

            var existingMeal = HomeVM.CurrentMeal;

            if (existingMeal is not null)
            {
                var existingMealDetail = _dataContext.Database.Table<MealDetail>()
                    .Where(x => x.MealId == HomeVM.CurrentMeal.MealId &&
                     x.FoodId == SelectedFood.FoodId).FirstOrDefault();

                if (existingMealDetail is not null)
                {
                    IsMealDetailExist = true;
                    Quantity = existingMealDetail.Quantity.ToString();
                    FoodCalories = Math.Round((SelectedFood.FoodCalories * Convert.ToDouble(Quantity) / 100), 1).ToString("F1");
                    FoodCarb = Math.Round((SelectedFood.FoodCarb * Convert.ToDouble(Quantity) / 100), 1).ToString("F1");
                    FoodProtien = Math.Round((SelectedFood.FoodProtien * Convert.ToDouble(Quantity) / 100), 1).ToString("F1");
                    FoodFat = Math.Round((SelectedFood.FoodFat * Convert.ToDouble(Quantity) / 100), 1).ToString("F1");
                    FoodFibers = Math.Round((SelectedFood.FoodFibers * Convert.ToDouble(Quantity) / 100), 1).ToString("F1");
                    FoodCarb = Math.Round((SelectedFood.FoodCarb * Convert.ToDouble(Quantity) / 100), 1).ToString("F1");
                }
                else
                {
                    Quantity = null;
                    FoodCalories = "0.0";
                    FoodCarb = "0.0";
                    FoodProtien = "0.0";
                    FoodFat = "0.0";
                    FoodFibers = "0.0";
                }

            }
            else
            {
                Quantity = null;
                FoodCalories = "0.0";
                FoodCarb = "0.0";
                FoodProtien = "0.0";
                FoodFat = "0.0";
                FoodFibers = "0.0";
            }

            IsBottomSheetPresented = true;
        }
        
        [RelayCommand]
        private async Task HideBottomSheet()
        {
            IsBottomSheetPresented=false;
        }

        [RelayCommand]
        private void ChangeQuantity()
        {
            if (!string.IsNullOrWhiteSpace(Quantity))
            {
                FoodCalories = Math.Round((SelectedFood.FoodCalories * Convert.ToDouble(Quantity) / 100), 1).ToString("F1");
                FoodCarb = Math.Round((SelectedFood.FoodCarb * Convert.ToDouble(Quantity) / 100), 1).ToString("F1");
                FoodProtien = Math.Round((SelectedFood.FoodProtien * Convert.ToDouble(Quantity) / 100), 1).ToString("F1");
                FoodFat = Math.Round((SelectedFood.FoodFat * Convert.ToDouble(Quantity) / 100), 1).ToString("F1");
                FoodFibers = Math.Round((SelectedFood.FoodFibers * Convert.ToDouble(Quantity) / 100), 1).ToString("F1");
            }
            else
            {
                FoodCalories = "0.0";
                FoodCarb = "0.0";
                FoodProtien = "0.0";
                FoodFat = "0.0";
                FoodFibers = "0.0";
            }
        }

        [RelayCommand]
        private async Task GoToReadyMealPage(Meal meal)
        {
#if ANDROID
Dictionary<string, object> parameter = new Dictionary<string, object>
{
    [nameof(Meal)] = meal
};

            await Shell.Current.GoToAsync($"//{nameof(HomePage)}/{nameof(MealPage)}/{nameof(ReadyMealPage)}", animate: true,parameter);
#endif

        }
        [RelayCommand]
        private async Task GoToMealPage()
        {
#if ANDROID
            await Shell.Current.GoToAsync($"//{nameof(HomePage)}/{nameof(MealPage)}", animate: true);
#endif
        }

        [RelayCommand]
        private async Task GoToItemPage(Food food)
        {
            //CartItem cartItem = new CartItem();
            //if (IsBreakfast)
            //{
            //    cartItem.MealType = Breakfast;
            //    cartItem.FoodId = food.FoodId;
            //    cartItem.FoodName = food.FoodName;
            //    cartItem.FoodCategory = food.FoodCategory;
            //    cartItem.FoodCalories = food.FoodCalories;
            //    cartItem.FoodCarb = food.FoodCarb;
            //    cartItem.FoodProtien = food.FoodProtien;
            //    cartItem.FoodFat = food.FoodFat;
            //    cartItem.FoodFibers = food.FoodFibers;
            //}
            //else if (IsLunch)
            //{
            //    cartItem.MealType = Lunch;
            //    cartItem.FoodId = food.FoodId;
            //    cartItem.FoodName = food.FoodName;
            //    cartItem.FoodCategory = food.FoodCategory;
            //    cartItem.FoodCalories = food.FoodCalories;
            //    cartItem.FoodCarb = food.FoodCarb;
            //    cartItem.FoodProtien = food.FoodProtien;
            //    cartItem.FoodFat = food.FoodFat;
            //    cartItem.FoodFibers = food.FoodFibers;

            //}
            //else
            //{
            //    cartItem.MealType = Dinner;
            //    cartItem.FoodId = food.FoodId;
            //    cartItem.FoodName = food.FoodName;
            //    cartItem.FoodCategory = food.FoodCategory;
            //    cartItem.FoodCalories = food.FoodCalories;
            //    cartItem.FoodCarb = food.FoodCarb;
            //    cartItem.FoodProtien = food.FoodProtien;
            //    cartItem.FoodFat = food.FoodFat;
            //    cartItem.FoodFibers = food.FoodFibers;
            //}

            //var parameter = new Dictionary<string, object>
            //{
            //    [nameof(ItemVM.CartItem)] = cartItem,
            //};
            //await GoToAsyncWithStackAndParameter(nameof(ItemPage), animate: true, parameter);
        }

        [RelayCommand]
        private async Task GoToAddFoodPage()
        {
#if ANDROID
            //await Shell.Current.GoToAsync($"//{nameof(HomePage)}/{nameof(MealPage)}/{nameof(AddFoodPage)}", animate: true);
#endif

            //await GoToAsyncWithStack(nameof(AddFoodPage), true);
        }
    }
}
