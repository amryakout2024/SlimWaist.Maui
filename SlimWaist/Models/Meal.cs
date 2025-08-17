using SQLite;

namespace SlimWaist.Models
{
    public class Meal
    {
        [PrimaryKey, AutoIncrement]

        public int MealId { get; set; }

        public string? MealName { get; set; }

        //public int MembershipId { get; set; }

        public int DayDietId { get; set; }

        public DateTime DayDietDate { get; set; }

        public int MealTypeId { get; set; }

        public bool IsExistsInDb { get; set; }=false;

        public string? TotalMealCalories { get; set; }

        //public double TotalFoodCarb { get; set; } = 0;

        //public double TotalFoodProtien { get; set; } = 0;

        //public double TotalFoodFat { get; set; } = 0;

        //public double TotalFoodFibers { get; set; } = 0;

    }
}
