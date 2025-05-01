using CommunityToolkit.Mvvm.ComponentModel;
using SlimWaist.Models;

namespace SlimWaist.ViewModels
{
    [QueryProperty(nameof(Meal), nameof(Meal))]
    public partial class MealDetailVM(DataContext dataContext) : BaseVM
    {
        private readonly DataContext _dataContext = dataContext;

        [ObservableProperty]
        private Meal _meal;

        [ObservableProperty]
        private List<MealDetail> _mealDetails;

        public async Task Init()
        {
            var mealDetails = await _dataContext.LoadAsync<MealDetail>();

            MealDetails = mealDetails.Where(x => x.MealName == Meal.MealName).ToList();
        }
    }
}
