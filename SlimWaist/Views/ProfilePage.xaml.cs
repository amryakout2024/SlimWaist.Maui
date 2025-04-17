using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Maui.Core;
using CommunityToolkit.Maui.Views;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using SlimWaist.Models;
using SlimWaist.Popups;
using SlimWaist.ViewModels;
using SlimWaist.Views;
using System.Collections.ObjectModel;
using System.Globalization;
using UraniumUI.Material;
using UraniumUI.Material.Controls;
using UraniumUI.Pages;

namespace SlimWaist.Views
{
    public partial class ProfilePage : UraniumContentPage
    {
        private readonly ProfileVM _profileVM;

        private readonly DataContext _dataContext;

        public ProfilePage(ProfileVM profileVM, DataContext dataContext)
        {
            InitializeComponent();

            _profileVM=profileVM;

            _dataContext = dataContext;

            BindingContext = _profileVM;

            SetBannerId();

        }
        private void SetBannerId()
        {
            //#if ANDROID
            //		myAds.AdsId="ca-app-pub-3829937021524038/7874998548";
            //#endif
        }

        ////protected async override void OnBindingContextChanged()
        ////{
        ////    base.OnBindingContextChanged();

        ////    await _profileVM.init();

        ////}

        protected async override void OnNavigatedTo(NavigatedToEventArgs args)
        {
            base.OnNavigatedTo(args);

            await _profileVM.init();

        }
        protected override bool OnBackButtonPressed()
        {

            Shell.Current.GoToAsync($"//{nameof(SettingPage)}", animate: true);

            return true;
        }

        private void TapGestureRecognizer_Tapped(object sender, TappedEventArgs e)
        {
            var popup = new PasswordPopup(_dataContext);

            this.ShowPopup(popup);
        }
    }
}