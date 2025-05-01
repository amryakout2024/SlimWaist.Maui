using SQLite;

namespace SlimWaist.Models
{
    public class Food
    {
        [PrimaryKey, AutoIncrement]

        public int FoodId { get; set; }

        public string? FoodCategory { get; set; }

        public string? FoodName { get; set; }

        public double FoodCalories { get; set; }

        public double FoodCarb { get; set; }

        public double FoodProtien { get; set; }

        public double FoodFat { get; set; }

        public double FoodFibers { get; set; }

    }
}
