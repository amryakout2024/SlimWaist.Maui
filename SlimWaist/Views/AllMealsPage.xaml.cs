using SlimWaist.ViewModels;
using UraniumUI.Pages;

namespace SlimWaist.Views;

public partial class AllMealsPage : UraniumContentPage
{
    private readonly AllMealsVM _allMealsVM;

    public AllMealsPage(AllMealsVM allMealsVM)
    {
        InitializeComponent();

        _allMealsVM = allMealsVM;

        BindingContext = _allMealsVM;
    }
    protected async override void OnAppearing()
    {
        await _allMealsVM.Init();
    }
    protected override bool OnBackButtonPressed()
    {
#if ANDROID
        Shell.Current.GoToAsync($"//{nameof(HomePage)}", animate: true);
#endif
        return true;
    }
}