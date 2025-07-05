using SlimWaist.ViewModels;
using UraniumUI.Pages;

namespace SlimWaist.Views;

public partial class TablesPage : UraniumContentPage
{
    private readonly TablesVM _tablesVM;

    public TablesPage(TablesVM tablesVM)
    {
        InitializeComponent();

        _tablesVM = tablesVM;

        BindingContext = _tablesVM;
    }
    protected async override void OnAppearing()
    {
        await _tablesVM.Init();
    }
    protected override bool OnBackButtonPressed()
    {
#if ANDROID
        Shell.Current.GoToAsync($"//{nameof(HomePage)}", animate: true);
#endif
        return true;
    }
}