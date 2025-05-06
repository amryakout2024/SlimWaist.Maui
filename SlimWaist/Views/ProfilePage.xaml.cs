using CommunityToolkit.Maui.Views;
using SlimWaist.Models;
using SlimWaist.Popups;
using SlimWaist.ViewModels;
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

            _profileVM = profileVM;

            _dataContext = dataContext;

            BindingContext = _profileVM;

            datePickerField.MinimumDate = new DateTime(1930, 1, 1);

            datePickerField.MaximumDate = DateTime.Now;

            SetBannerId();
        }
        private void SetBannerId()
        {
            //#if ANDROID
            //		myAds.AdsId="ca-app-pub-3829937021524038/7874998548";
            //#endif
        }

        protected async override void OnAppearing()
        {
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