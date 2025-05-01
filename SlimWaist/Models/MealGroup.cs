namespace SlimWaist.Models
{
    public class MealGroup : List<MealDetail>
    {

        public int MealId { get; set; }

        public string? MealName { get; set; }

        public string? MealType { get; set; }

        public bool IsSelected { get; set; } = false;

        public double TotalCalories { get; set; }

        public double TotalFoodCarb { get; set; }

        public double TotalFoodProtien { get; set; }

        public double TotalFoodFat { get; set; }

        public double TotalFoodFibers { get; set; }


        public MealGroup(int mealId, string mealName, string mealType, bool isSelected, double totalCalories, double totalFoodCarb, double totalFoodProtien, double totalFoodFat, double totalFoodFibers, List<MealDetail> mealDetails) : base(mealDetails)
        {
            MealId = mealId;

            MealName = mealName;

            MealType = mealType;

            IsSelected = isSelected;

            TotalCalories = totalCalories;

            TotalFoodCarb = totalFoodCarb;

            TotalFoodProtien = totalFoodProtien;

            TotalFoodFat = totalFoodFat;

            TotalFoodFibers = totalFoodFibers;
        }

    }
}
