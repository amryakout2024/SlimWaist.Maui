using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using SlimWaist.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SlimWaist.ViewModels
{
    public partial class AddFoodVM(DataContext dataContext):BaseVM
    {
        private readonly DataContext _dataContext = dataContext;
        [ObservableProperty]
        private string _foodCategory ;

        [ObservableProperty]
        private string? _foodName ;

        [ObservableProperty]
        private string _foodCalories ;

        [ObservableProperty]
        private string _foodProtien ;

        [ObservableProperty]
        private string _foodFat ;

        [ObservableProperty]
        private string _foodFibers ;   
        
        [ObservableProperty]
        private string _foodCarb ;

        [RelayCommand]
        private async Task SaveNewFood()
        {
            await _dataContext.InsertAsync<Food>(new Food()
            {
                FoodCategory =FoodCategory,
                FoodName = FoodName,
                FoodCalories = Math.Round(Convert.ToDouble(FoodCalories), 1),
                FoodCarb= Math.Round(Convert.ToDouble(FoodCarb), 1),
                FoodProtien = Math.Round(Convert.ToDouble(FoodProtien), 1),
                FoodFat = Math.Round(Convert.ToDouble(FoodFat), 1),
                FoodFibers = Math.Round(Convert.ToDouble(FoodFibers), 1)
            });

            await ShowToastAsync("تم حفظ الغذاء بنجاح");

            await Shell.Current.GoToAsync("..");
        }

    }
}
