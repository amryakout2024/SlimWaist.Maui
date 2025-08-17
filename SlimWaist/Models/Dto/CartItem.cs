using CommunityToolkit.Mvvm.ComponentModel;
using SQLite;

namespace SlimWaist.Models
{
    public partial class CartItem : ObservableObject
    {
        [PrimaryKey, AutoIncrement]
        public int CartId { get; set; }

        //public string? MealType { get; set; }
        public int FoodId { get; set; }
        public string? FoodName { get; set; }
        public string? FoodCategory { get; set; }
        public double Quantity { get; set; }

        public double TotalFoodCalories { get; set; }
        public double TotalFoodCarb { get; set; }
        public double TotalFoodProtien { get; set; }
        public double TotalFoodFat { get; set; }
        public double TotalFoodFibers { get; set; }

        //public double TotalMealCalories { get => FoodCalories * _quantity / 100; set; }
        //public double TotalFoodCarb => FoodCarb * _quantity / 100;
        //public double TotalFoodProtien => FoodProtien * _quantity / 100;
        //public double TotalFoodFat => FoodFat * _quantity / 100;
        //public double TotalFoodFibers => FoodFibers * _quantity / 100;

        //[ObservableProperty, NotifyPropertyChangedFor(
        //    nameof(TotalMealCalories)
        //    , nameof(TotalFoodCarb)
        //    , nameof(TotalFoodProtien)
        //    , nameof(TotalFoodFat)
        //    , nameof(TotalFoodFibers))]
        //private double _quantity;


    }
}