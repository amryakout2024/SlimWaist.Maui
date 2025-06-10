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
    public partial class MealVM(DataContext dataContext) : BaseVM
    {
        private readonly DataContext _dataContext = dataContext;

        [ObservableProperty]
        private string _mealTypeName;

        [ObservableProperty]
        private List<MealDetail> _mealDetails;

        [ObservableProperty]
        private string _datedayname;

        [ObservableProperty]
        private string? _totalEnergy;

        public async Task init()
        {
            MealTypeName = App.mealTypes.Where(x => x.MealTypeId == HomeVM.CurrentMeal.MealTypeId).FirstOrDefault()?.MealTypeName??"";

            MealDetails = _dataContext.Database.Table<MealDetail>().Where(x => x.MealId ==HomeVM.CurrentMeal.MealId).ToList();

            var existingMeal = _dataContext.Database.Table<Meal>().ToList().Where(x => x.MealId == HomeVM.CurrentMeal.MealId).FirstOrDefault();

            if (existingMeal is not null)
            {
                //var existingMealDetail = _dataContext.Database.Table<MealDetail>()
                //    .Where(x => x.MealId == HomeVM.CurrentMeal.MealId &&
                //     x.FoodId == SelectedFood.FoodId).FirstOrDefault();

                //if (existingMealDetail is not null)
                //{
                //    if (Convert.ToInt32(Quantity) > 0)
                //    {
                //        existingMealDetail.Quantity = Convert.ToInt32(Quantity);

                //        await App.dataContext.UpdateAsync(existingMealDetail);

                //        IsBottomSheetPresented = false;

                //        await ShowToastAsync(AppResource.ResourceManager.GetString("Updatedsuccessfully", CultureInfo.CurrentCulture) ?? "");
                //    }
                //    else
                //    {
                //        await App.dataContext.DeleteAsync<MealDetail>(existingMealDetail);

                //        IsBottomSheetPresented = false;

                //        await ShowToastAsync(AppResource.ResourceManager.GetString("Deletedsuccessfully", CultureInfo.CurrentCulture) ?? "");
                //    }

                //}
                //else
                //{
                //    var mealDetail = new MealDetail()
                //    {
                //        FoodId = SelectedFood.FoodId,
                //        MealId = HomeVM.CurrentMeal.MealId,
                //        Quantity = Convert.ToDouble(Quantity)
                //    };

                //    await App.dataContext.InsertAsync<MealDetail>(mealDetail);

                //    IsBottomSheetPresented = false;

                //    await ShowToastAsync(AppResource.ResourceManager.GetString("Addedsuccessfully", CultureInfo.CurrentCulture) ?? "");
                //}

            }

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
