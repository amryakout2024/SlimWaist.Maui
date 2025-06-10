using SQLite;

namespace SlimWaist.Models
{
    public class MealDetail
    {
        [PrimaryKey, AutoIncrement]

        public int MealDetailId { get; set; }

        public int MealId { get; set; }

        public int FoodId { get; set; }

        public double Quantity { get; set; }

        //public double CalculatedCalories { get; set; }

        //public double CalculatedFoodCarb { get; set; }

        //public double CalculatedFoodProtien { get; set; }

        //public double CalculatedFoodFat { get; set; }

        //public double CalculatedFoodFibers { get; set; }

        //public double FoodCalories { get; set; }

        //public double FoodCarb { get; set; }

        //public double FoodProtien { get; set; }

        //public double FoodFat { get; set; }

        //public double FoodFibers { get; set; }

    }
}
