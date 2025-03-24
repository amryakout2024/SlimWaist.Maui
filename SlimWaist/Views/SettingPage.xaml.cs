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

namespace SlimWaist.Views;

public partial class SettingPage : UraniumContentPage
{
	private readonly SettingVM _settingVM;

	private readonly DataContext _dataContext;

    public SettingPage(SettingVM settingVM,DataContext dataContext)
	{
		InitializeComponent();

		_settingVM = settingVM;
        _dataContext = dataContext;
        
		BindingContext = _settingVM;


        SetBannerId();
    }
    private void SetBannerId()
    {
//#if ANDROID
//		myAds.AdsId="ca-app-pub-3829937021524038/7874998548";
//#endif
	}

    protected async override void OnBindingContextChanged()
    {
        base.OnBindingContextChanged();

        await _dataContext.init();

        await _settingVM.init();

    }

    protected override bool OnBackButtonPressed()
	{
        Shell.Current.GoToAsync($"//{nameof(HomePage)}",animate:true);

        return true;
	}
}