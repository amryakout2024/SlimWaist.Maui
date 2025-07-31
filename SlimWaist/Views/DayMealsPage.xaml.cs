using SlimWaist.Models.Dto;
using SlimWaist.ViewModels;
using UraniumUI.Pages;

namespace SlimWaist.Views;

public partial class DayMealsPage : UraniumContentPage
{
    private readonly DayMealsVM _dayMealsVM;

    public static List<Meal> Meals = new List<Meal>();

    public DayMealsPage(DayMealsVM dayMealsVM)
    {
        InitializeComponent();

        _dayMealsVM = dayMealsVM;

        BindingContext = _dayMealsVM;
    }
    protected async override void OnAppearing()
    {
        await _dayMealsVM.Init();
    }
    protected override bool OnBackButtonPressed()
    {
#if ANDROID
        Shell.Current.GoToAsync("..", animate: true);
#endif
        return true;
    }

}