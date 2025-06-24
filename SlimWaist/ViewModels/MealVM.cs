using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microcharts;
using Microcharts.Maui;
using Microsoft.Maui.Controls.Compatibility;
using SkiaSharp;
using SlimWaist.Languages;
using SlimWaist.Models;
using SlimWaist.Views;
using System.Collections.ObjectModel;
using System.Globalization;

namespace SlimWaist.ViewModels
{
    public partial class MealVM(DataContext dataContext) : BaseVM
    {
        private readonly DataContext _dataContext = dataContext;

        [ObservableProperty]
        private string _mealTypeName;

        [ObservableProperty]
        private List<MealDetail> _mealDetails;

        [ObservableProperty]
        private List<CartItem> _cartItems;

        [ObservableProperty]
        private string _datedayname;

        [ObservableProperty]
        private string? _totalEnergy;

        [ObservableProperty]
        private string _foodName;

        [ObservableProperty]
        private string _quantity;

        [ObservableProperty]
        private string _foodCalories;

        [ObservableProperty]
        private string _totalMealCalories;
        [ObservableProperty]
        private string _totalMealCarbohydrates;
        [ObservableProperty]
        private string _totalMealProtiens;
        [ObservableProperty]
        private string _totalMealFats;
        [ObservableProperty]
        private string _totalMealFibers;

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
        private string _mealSize;

        [ObservableProperty]
        private bool _isMealExists;
        [ObservableProperty]
        private Food _selectedFood;

        [ObservableProperty]
        private bool _isBottomSheetPresented;

        public async Task init()
        {
            CartItems = new List<CartItem>();
            IsMealExists = false;
            MealSize = "0";
            TotalMealCalories = "0";
            TotalMealCarbohydrates = "0";
            TotalMealProtiens = "0";
            TotalMealFats = "0";
            TotalMealFibers = "0";
            IsBottomSheetPresented = false;

            MealTypeName = App.mealTypes.Where(x => x.MealTypeId == HomeVM.CurrentMeal?.MealTypeId).FirstOrDefault()?.MealTypeName??"";

            MealDetails = _dataContext.Database.Table<MealDetail>().Where(x => x.MealId ==HomeVM.CurrentMeal.MealId).ToList()??new List<MealDetail>();

            var existingMeal = _dataContext.Database.Table<Meal>().ToList().Where(x => x.MealId == HomeVM.CurrentMeal?.MealId).FirstOrDefault();

            if (existingMeal is not null)
            {

                if (MealDetails.Count>0)
                {
                    IsMealExists = true;

                    await _dataContext.ClearAllAsync<CartItem>();

                    foreach (var mealDetail in MealDetails.Where(x=>x.MealId==existingMeal.MealId).ToList())
                    {
                        var existingFood = _dataContext.Database.Table<Food>().Where(x => x.FoodId == mealDetail.FoodId).FirstOrDefault();

                        await _dataContext.InsertAsync<CartItem>(new CartItem()
                        {
                            FoodId=existingFood.FoodId,
                            Quantity=mealDetail.Quantity,
                            FoodName=existingFood.FoodName,
                            FoodCategory=existingFood.FoodCategory,
                            TotalFoodCarb = Math.Round((existingFood.FoodCarb * mealDetail.Quantity / 100), 1),
                            TotalFoodProtien = Math.Round((existingFood.FoodProtien * mealDetail.Quantity / 100), 1),
                            TotalFoodFat = Math.Round((existingFood.FoodFat * mealDetail.Quantity / 100), 1),
                            TotalFoodFibers = Math.Round((existingFood.FoodFibers * mealDetail.Quantity / 100), 1),
                            TotalFoodCalories = Math.Round((existingFood.FoodCalories * mealDetail.Quantity / 100), 1)
                        });

                    }

                    CartItems = await _dataContext.LoadAsync<CartItem>();
                    
                    var mealDetails = _dataContext.Database.Table<MealDetail>().Where(x => x.MealId == HomeVM.CurrentMeal.MealId).ToList();

                    if (mealDetails.Count > 0)
                    {
                        foreach (var mealDetail in mealDetails)
                        {
                            var oneFoodMeal = _dataContext.Database.Table<Food>().Where(x => x.FoodId == mealDetail.FoodId).FirstOrDefault();
                            
                            MealSize = Math.Round((Convert.ToDouble(MealSize) + Convert.ToDouble(mealDetail.Quantity)), 1).ToString("F0");
                            TotalMealCalories = (Convert.ToDouble(TotalMealCalories) + Math.Round((Convert.ToDouble(oneFoodMeal.FoodCalories) * Convert.ToDouble(mealDetail.Quantity) / 100), 1)).ToString("F1");
                            TotalMealCarbohydrates = Math.Round((Convert.ToDouble(TotalMealCarbohydrates) + (Convert.ToDouble(oneFoodMeal.FoodCarb) * Convert.ToDouble(mealDetail.Quantity) / 100)), 1).ToString("F1");
                            TotalMealProtiens = Math.Round((Convert.ToDouble(TotalMealProtiens) + (Convert.ToDouble(oneFoodMeal.FoodProtien) * Convert.ToDouble(mealDetail.Quantity) / 100)), 1).ToString("F1");
                            TotalMealFats = Math.Round((Convert.ToDouble(TotalMealFats) + (Convert.ToDouble(oneFoodMeal.FoodFat) * Convert.ToDouble(mealDetail.Quantity) / 100)), 1).ToString("F1");
                            TotalMealFibers = Math.Round((Convert.ToDouble(TotalMealFibers) + (Convert.ToDouble(oneFoodMeal.FoodFibers) * Convert.ToDouble(mealDetail.Quantity) / 100)), 1).ToString("F1");
                        }
                    }

                }

            }

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
        private async Task DeleteMealDetail(CartItem cartItem)
        {
            var existingMealDetail = _dataContext.Database.Table<MealDetail>()
    .Where(x => x.MealId == HomeVM.CurrentMeal.MealId &&
     x.FoodId == cartItem.FoodId).FirstOrDefault();

            await App.dataContext.DeleteAsync<MealDetail>(existingMealDetail);

            await ShowToastAsync(AppResource.ResourceManager.GetString("Deletedsuccessfully", CultureInfo.CurrentCulture) ?? "");
         
            if (MealDetails.Count > 0)
            {
                await init();
            }
            else
            {
                IsMealExists = false;
            }

        }

        [RelayCommand]
        private async Task AddFoodToMeal()
        {
            var existingMeal = _dataContext.Database.Table<Meal>().ToList().Where(x => x.MealId == HomeVM.CurrentMeal.MealId).FirstOrDefault();

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

                    await ShowToastAsync(AppResource.ResourceManager.GetString("Updatedsuccessfully", CultureInfo.CurrentCulture) ?? "");
                }
                else
                {
                    await App.dataContext.DeleteAsync<MealDetail>(existingMealDetail);

                    IsBottomSheetPresented = false;
                   
                    await ShowToastAsync(AppResource.ResourceManager.GetString("Deletedsuccessfully", CultureInfo.CurrentCulture) ?? "");
                    if (MealDetails.Count<1)
                    {
                        IsMealExists = false;
                    }
                }

            }

            await init();
        }

        [RelayCommand]
        private async Task HideBottomSheet()
        {
            IsBottomSheetPresented = false;
        }

        [RelayCommand]
        private async Task ShowBottomSheet(CartItem cartItem)
        {
            SelectedFood = _dataContext.Database.Table<Food>().Where(x => x.FoodId == cartItem.FoodId).FirstOrDefault();

            FoodName = cartItem.FoodName ?? "";
            FoodCategory = cartItem.FoodCategory ?? "";
            Quantity = cartItem.Quantity.ToString();
            FoodCalories = cartItem.TotalFoodCalories.ToString();
            FoodCarb = cartItem.TotalFoodCarb.ToString();
            FoodProtien = cartItem.TotalFoodProtien.ToString();
            FoodFat = cartItem.TotalFoodFat.ToString();
            FoodFibers = cartItem.TotalFoodFibers.ToString();

            IsBottomSheetPresented = true;
        }

        [RelayCommand]
        private async Task GoToHomePage()
        {
#if ANDROID
            await Shell.Current.GoToAsync($"//{nameof(HomePage)}", animate: true);
#endif
        }

        [RelayCommand]
        private async Task GoToFoodsPage()
        {
#if ANDROID
            await Shell.Current.GoToAsync($"//{nameof(HomePage)}/{nameof(MealPage)}/{nameof(FoodsPage)}", animate: true);
#endif

            //await GoToAsyncWithStack($"//{nameof(HomePage)}/{nameof(MealPage)}/{nameof(FoodsPage)}", true);
        }

        [RelayCommand]
        private void SaveDiet()
        {

        }
    }
}
