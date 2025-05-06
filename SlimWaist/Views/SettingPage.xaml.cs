//using ZXing.Net.Maui;
using CommunityToolkit.Maui.Views;
using SlimWaist.Models;
using SlimWaist.Popups;
using SlimWaist.ViewModels;
using UraniumUI.Pages;

namespace SlimWaist.Views;

public partial class SettingPage : UraniumContentPage
{
    private readonly SettingVM _settingVM;

    private List<Membership> memberships;
    private Membership membership;
    private Setting setting;
    private List<Setting> settings;

    public SettingPage(SettingVM settingVM)
    {
        InitializeComponent();

        _settingVM = settingVM;

        //App.dataContext.ChangeFlowDirection(this);

        BindingContext = _settingVM;

        SetBannerId();
    }
    private void SetBannerId()
    {
        //#if ANDROID
        //		myAds.AdsId="ca-app-pub-3829937021524038/7874998548";
        //#endif
    }

    protected async override void OnNavigatedTo(NavigatedToEventArgs args)
    {
        base.OnNavigatedTo(args);

        await _settingVM.init();

    }

    protected override bool OnBackButtonPressed()
    {
        Shell.Current.GoToAsync($"//{nameof(HomePage)}", animate: true);

        return true;
    }

    private void TapGestureRecognizer_Tapped(object sender, TappedEventArgs e)
    {
        var popup = new LanguagePopup();

        this.ShowPopup(popup);

    }
}