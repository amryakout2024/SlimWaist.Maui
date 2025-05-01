using SlimWaist.ViewModels;

namespace SlimWaist.Views;

public partial class SlimWaistDaysListPage : ContentPage
{
    private readonly SlimWaistDaysListVM _SlimWaistDaysListVM;

    public SlimWaistDaysListPage(SlimWaistDaysListVM SlimWaistDaysListVM)
    {
        InitializeComponent();

        _SlimWaistDaysListVM = SlimWaistDaysListVM;

        BindingContext = _SlimWaistDaysListVM;

    }
}