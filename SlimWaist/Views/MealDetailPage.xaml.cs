using SlimWaist.ViewModels;
using UraniumUI.Pages;

namespace SlimWaist.Views;

public partial class MealDetailPage : UraniumContentPage
{
    private readonly MealDetailVM _mealDetailVM;

    public MealDetailPage(MealDetailVM mealDetailVM)
    {
        InitializeComponent();

        _mealDetailVM = mealDetailVM;

        BindingContext = _mealDetailVM;
    }

    protected async override void OnNavigatedTo(NavigatedToEventArgs args)
    {
        await _mealDetailVM.Init();

        //base.OnNavigatedTo(args);
    }
    protected override bool OnBackButtonPressed()
    {
#if ANDROID
        Shell.Current.GoToAsync("..", animate: true);
#endif
        return true;

        //        if (_foodsVM.IsFoodSelected)
        //        {
        //            _foodsVM.IsFoodSelected = false;

        //            return true;
        //        }
        //        else
        //        {

        //#if ANDROID
        //        Shell.Current.GoToAsync($"//{nameof(HomePage)}", animate: true);
        //#endif
        //        return true;

        //        }
    }

}