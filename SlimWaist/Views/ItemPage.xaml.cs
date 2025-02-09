using SlimWaist.ViewModels;
using UraniumUI.Pages;

namespace SlimWaist.Views;

public partial class ItemPage : UraniumContentPage
{
    private readonly ItemVM _itemVM;

    public ItemPage(ItemVM itemVM)
	{
		InitializeComponent();

        _itemVM = itemVM;

        BindingContext = _itemVM;

    }
    protected async override void OnNavigatedTo(NavigatedToEventArgs args)
    {
        await _itemVM.Init();

        //base.OnNavigatedTo(args);
    }
    protected override bool OnBackButtonPressed()
    {
//#if ANDROID
//        Shell.Current.GoToAsync($"//{nameof(HomePage)}", animate: true);
//#endif
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