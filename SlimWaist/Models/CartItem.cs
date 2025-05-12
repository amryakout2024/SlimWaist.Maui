using CommunityToolkit.Mvvm.ComponentModel;
using SQLite;

namespace SlimWaist.Models
{
    public partial class CartItem : ObservableObject
    {
        [PrimaryKey, AutoIncrement]
        public int CartId { get; set; }
        public string? MealType { get; set; }
        public int FoodId { get; set; }
        public string? FoodName { get; set; }
        public string? FoodCategory { get; set; }
        public int Quantity { get; set; }

        public double FoodCalories { get; set; }
        public double FoodCarb { get; set; }
        public double FoodProtien { get; set; }
        public double FoodFat { get; set; }
        public double FoodFibers { get; set; }

        //public double TotalCalories => FoodCalories * Quantity/100;
        //public double TotalFoodCarb => FoodCarb * Quantity/100;
        //public double TotalFoodProtien => FoodProtien * Quantity/ 100;
        //public double TotalFoodFat => FoodFat * Quantity/100;
        //public double TotalFoodFibers => FoodFibers * Quantity / 100;

        //[ObservableProperty, NotifyPropertyChangedFor(
        //    nameof(TotalCalories)
        //    ,nameof(TotalFoodCarb)
        //    ,nameof(TotalFoodProtien)
        //    ,nameof(TotalFoodFat)
        //    ,nameof(TotalFoodFibers))]
        //private int _quantity;


    }
}