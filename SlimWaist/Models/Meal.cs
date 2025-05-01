using SQLite;

namespace SlimWaist.Models
{
    public class Meal
    {
        [PrimaryKey, AutoIncrement]

        public int MealId { get; set; }

        public string? MealName { get; set; }

        public string? MealType { get; set; }

        public bool IsSelected { get; set; } = false;

        public double TotalCalories { get; set; }

        public double TotalFoodCarb { get; set; }

        public double TotalFoodProtien { get; set; }

        public double TotalFoodFat { get; set; }

        public double TotalFoodFibers { get; set; }

    }
}
