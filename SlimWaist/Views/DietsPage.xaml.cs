using SlimWaist.ViewModels;

namespace SlimWaist.Views;

public partial class DietsPage : ContentPage
{
    private readonly DietsVM _dietsVM;

    public DietsPage(DietsVM dietsVM)
    {
        InitializeComponent();

        _dietsVM = dietsVM;

        BindingContext = _dietsVM;
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();

        await _dietsVM.init();
    }
}