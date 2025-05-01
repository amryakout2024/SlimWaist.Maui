using SQLite;

namespace SlimWaist.Models
{
    public class MealDetail
    {
        [PrimaryKey, AutoIncrement]

        public int MealDetailId { get; set; }

        public string? MealName { get; set; }

        public string? MealType { get; set; }

        public int FoodId { get; set; }

        public string? FoodName { get; set; }

        public int Quantity { get; set; }

        //public double TotalCalories { get; set; }

        //public double TotalFoodCarb { get; set; }

        //public double TotalFoodProtien { get; set; }

        //public double TotalFoodFat { get; set; }

        //public double TotalFoodFibers { get; set; }

        public double FoodCalories { get; set; }

        public double FoodCarb { get; set; }

        public double FoodProtien { get; set; }

        public double FoodFat { get; set; }

        public double FoodFibers { get; set; }

    }
}
