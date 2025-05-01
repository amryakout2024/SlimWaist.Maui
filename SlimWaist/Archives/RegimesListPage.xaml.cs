using SlimWaist.ViewModels;
using UraniumUI.Pages;

namespace SlimWaist.Views;

public partial class RegimesListPage : UraniumContentPage
{
    private readonly RegimesListVM _regimesListVM;

    public RegimesListPage(RegimesListVM regimesListVM)
    {
        InitializeComponent();

        _regimesListVM = regimesListVM;

        BindingContext = _regimesListVM;
    }
    protected async override void OnAppearing()
    {
        await _regimesListVM.Init();
    }
    protected override bool OnBackButtonPressed()
    {
#if ANDROID

        Shell.Current.GoToAsync($"//{nameof(HomePage)}", animate: true);
        //Navigation.PushAsync(new MainPage());

#endif

        return true;
    }
    private void Button_Clicked(object sender, EventArgs e)
    {

    }
}