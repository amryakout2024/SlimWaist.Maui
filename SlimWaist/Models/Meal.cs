using SQLite;

namespace SlimWaist.Models
{
    public class Meal
    {
        [PrimaryKey, AutoIncrement]

        public int MealId { get; set; }

        public string? MealName { get; set; }

        public int MealTypeId { get; set; }

        public bool IsSelected { get; set; } = false;

        public double TotalCalories { get; set; } = 0;

        public double TotalFoodCarb { get; set; } = 0;

        public double TotalFoodProtien { get; set; } = 0;

        public double TotalFoodFat { get; set; } = 0;

        public double TotalFoodFibers { get; set; } = 0;

    }
}
