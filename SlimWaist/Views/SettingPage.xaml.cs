using SlimWaist.ViewModels;
using SlimWaist.Models;
using SlimWaist.Views;
using System.Collections.ObjectModel;
using System.Globalization;
using UraniumUI.Material;
using UraniumUI.Material.Controls;
using UraniumUI.Pages;
//using ZXing.Net.Maui;
using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Maui.Core;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Maui.Views;
using SlimWaist.Popups;
using SlimWaist.Extentions;

namespace SlimWaist.Views;

public partial class SettingPage : UraniumContentPage
{
	private readonly SettingVM _settingVM;

	private readonly DataContext _dataContext;
    private List<Membership> memberships;
    private Membership membership;
    private Setting setting;
    private List<Setting> settings;

    public SettingPage(SettingVM settingVM,DataContext dataContext)
	{
		InitializeComponent();

		_settingVM = settingVM;

        _dataContext = dataContext;

        //_dataContext.ChangeFlowDirection(this);

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
        Shell.Current.GoToAsync($"//{nameof(HomePage)}",animate:true);

        return true;
	}

    private void TapGestureRecognizer_Tapped(object sender, TappedEventArgs e)
    {
        var popup = new LanguagePopup(_dataContext);

        this.ShowPopup(popup);

    }
}