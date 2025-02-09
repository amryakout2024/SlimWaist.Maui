using SlimWaist.ViewModels;

namespace SlimWaist.Views;

public partial class SlimWaistDayPage : ContentPage
{
    private readonly SlimWaistDayVM _SlimWaistDayVM;

    public SlimWaistDayPage(SlimWaistDayVM SlimWaistDayVM)
	{
		InitializeComponent();

        _SlimWaistDayVM = SlimWaistDayVM;

        BindingContext = _SlimWaistDayVM;
    }
}