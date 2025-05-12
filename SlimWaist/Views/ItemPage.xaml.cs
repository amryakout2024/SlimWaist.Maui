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
        base.OnNavigatedTo(args);

        await _itemVM.Init();
    }
    protected override bool OnBackButtonPressed()
    {
        Shell.Current.GoToAsync($"//{nameof(FoodsPage)}", animate: true);
       
        return true;
    }

}