using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using SlimWaist.Languages;
using SlimWaist.Models;
using SlimWaist.Views;
using System.Globalization;

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
        private string _mealType;

        [ObservableProperty]
        private string _searchName;

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
        private bool _isMealDetailExist=false;

        public async Task Init()
        {
            MealTypeName = App.mealTypes.Where(x => x.MealTypeId == HomeVM.CurrentMeal.MealTypeId).FirstOrDefault()?.MealTypeName ?? "";

            FoodsFromDatabase = await App.dataContext.LoadAsync<Food>();

            Foods = FoodsFromDatabase;

            IsBottomSheetPresented = false;

            TotalMealCalories = "0";

            var mealDetails = _dataContext.Database.Table<MealDetail>().Where(x => x.MealId == HomeVM.CurrentMeal.MealId).ToList();

            if (mealDetails.Count>0)
            {
                foreach (var mealDetail in mealDetails)
                {
                    var oneFoodMealCalories = _dataContext.Database.Table<Food>().Where(x => x.FoodId == mealDetail.FoodId).Select(x => x.FoodCalories).FirstOrDefault();

                    TotalMealCalories = (Convert.ToDouble(TotalMealCalories) + Math.Round((Convert.ToDouble(oneFoodMealCalories) * Convert.ToDouble(mealDetail.Quantity) / 100), 1)).ToString("F1");
                }
            }
        }

        [RelayCommand]
        private async Task AddFoodToMeal()
        {
            if (App.CurrentDayDiet.IsExistsInDb==false)
            {
                App.CurrentDayDiet.IsExistsInDb = true;

                await App.dataContext.InsertAsync<DayDiet>(App.CurrentDayDiet);
            }

            if (HomeVM.CurrentMeal.IsExistsInDb==false)
            {
                var CurrentDayDietId = _dataContext.Database.Table<DayDiet>()
                                        .Where(x => x.MembershipId == App.currentMembership.Id)
                                        .Where(x => x.DayDietDate == App.CurrentDayDiet.DayDietDate).Select(x => x.DayDietId).FirstOrDefault();

                HomeVM.CurrentMeal.DayDietId = CurrentDayDietId;

                HomeVM.CurrentMeal.IsExistsInDb = true;

                await App.dataContext.InsertAsync<Meal>(HomeVM.CurrentMeal);
            }

            var m = _dataContext.Database.Table<Meal>().ToList();

            var existingMealDetail = _dataContext.Database.Table<MealDetail>()
                        .Where(x => x.MealId == HomeVM.CurrentMeal.MealId &&
                         x.FoodId == SelectedFood.FoodId).FirstOrDefault();

            if (existingMealDetail is not null)
            {
                if (Convert.ToInt32(Quantity) > 0)
                {
                    existingMealDetail.Quantity = Convert.ToInt32(Quantity);

                    await App.dataContext.UpdateAsync(existingMealDetail);

                    IsBottomSheetPresented = false;
#if ANDROID
                    Shell.Current.GoToAsync($"//{nameof(HomePage)}/{nameof(MealPage)}", animate: true);
#endif

                    //await ShowToastAsync(AppResource.ResourceManager.GetString("Updatedsuccessfully", CultureInfo.CurrentCulture) ?? "");
                }
                else
                {
                    await App.dataContext.DeleteAsync<MealDetail>(existingMealDetail);

                    IsBottomSheetPresented = false;
#if ANDROID
                    Shell.Current.GoToAsync($"//{nameof(HomePage)}/{nameof(MealPage)}", animate: true);
#endif
                    //delete meal

                    var mealDetails = _dataContext.Database.Table<MealDetail>().Where(x => x.MealId == HomeVM.CurrentMeal.MealId).ToList();

                    if (mealDetails.Count < 1)
                    {
                        HomeVM.CurrentMeal.IsExistsInDb = false;

                        await App.dataContext.DeleteAsync<Meal>(HomeVM.CurrentMeal);
                    
                        //delete day
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
                            App.CurrentDayDiet.IsExistsInDb = false;
                            await App.dataContext.DeleteAsync<DayDiet>(App.CurrentDayDiet);
                        }
                    }


                    //await ShowToastAsync(AppResource.ResourceManager.GetString("Deletedsuccessfully", CultureInfo.CurrentCulture) ?? "");

                }

            }
            else
            {
                var mealDetail = new MealDetail()
                {
                    DayDietId = App.CurrentDayDiet.DayDietId,
                    FoodId = SelectedFood.FoodId,
                    MealId = HomeVM.CurrentMeal.MealId,
                    Quantity = Convert.ToDouble(Quantity)
                };

                await App.dataContext.InsertAsync<MealDetail>(mealDetail);

                IsBottomSheetPresented = false;
#if ANDROID
                Shell.Current.GoToAsync($"//{nameof(HomePage)}/{nameof(MealPage)}", animate: true);
#endif

                //await ShowToastAsync(AppResource.ResourceManager.GetString("Addedsuccessfully", CultureInfo.CurrentCulture) ?? "");
            }
        }
        [RelayCommand]
        private async Task Search()
        {
            if (!string.IsNullOrEmpty(SearchName))
            {
                Foods = FoodsFromDatabase.Where(x => x.FoodName.Contains(SearchName)).ToList();
            }
            else
            {
                Foods = FoodsFromDatabase;
            }
        }

        [RelayCommand]
        private async Task DeleteMealDetail()
        {
            if (!string.IsNullOrEmpty(SearchName))
            {
                Foods = FoodsFromDatabase.Where(x => x.FoodName.Contains(SearchName)).ToList();
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
        private async Task GoToMealPage()
        {
#if ANDROID
            await Shell.Current.GoToAsync($"//{nameof(HomePage)}/{nameof(MealPage)}", animate: true);
#endif
        }

        [RelayCommand]
        private async Task MealChanged()
        {
            List<CartItem> allcartitems = await App.dataContext.LoadAsync<CartItem>();

            //if (allcartitems.Count > 0)
            //{
            //    MealType = allcartitems[0].MealType;

            //    if (IsBreakfast && MealType != Breakfast)
            //    {
            //        await ShowAlertAsync($"توجد وجبه {MealType} في السلة يرجي حفظها اولا او افراغها ");
            //    }
            //    if (IsLunch && MealType != Lunch)
            //    {
            //        await ShowAlertAsync($"توجد وجبه {MealType} في السلة يرجي حفظها اولا او افراغها");
            //    }
            //    if (IsDinner && MealType != Dinner)
            //    {
            //        await ShowAlertAsync($"توجد وجبه {MealType} في السلة يرجي حفظها اولا او افراغها");
            //    }
            //    if (MealType == Breakfast) IsBreakfast = true;
            //    else if (MealType == Lunch) IsLunch = true;
            //    else IsDinner = true;

            //}

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
