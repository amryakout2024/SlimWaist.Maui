using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Maui.Core;
using CommunityToolkit.Maui.Views;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using SlimWaist.Extentions;
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
        private List<Membership> memberships;
        private Membership membership;
        private Setting setting;
        private List<Setting> settings;

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

        protected async override void OnBindingContextChanged()
        {

#if ANDROID

            base.OnBindingContextChanged();

            memberships = await _dataContext.LoadAsync<Membership>();

            settings = await _dataContext.LoadAsync<Setting>();

            setting = settings.Where(x => x.Id == 1).FirstOrDefault() ?? new Setting();

            membership = memberships.Where(x => x.Id == setting.CurrentMemberShipId).FirstOrDefault() ?? new Membership();

            if (membership.CultureInfo == "ar-SA")
            {
                ChangeDirections.instance.FlowDirection = FlowDirection.RightToLeft;

                CultureInfo.CurrentCulture = new CultureInfo("ar-SA");
            }
            else
            {
                ChangeDirections.instance.FlowDirection = FlowDirection.LeftToRight;

                CultureInfo.CurrentCulture = new CultureInfo("en-US");
            }

            await _profileVM.init();
#endif

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