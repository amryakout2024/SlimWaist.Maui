using Google.Android.Material.Badge;
using Google.Android.Material.BottomNavigation;
using Microsoft.Maui.Controls.Platform.Compatibility;
using Microsoft.Maui.Platform;
using SlimWaist.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SlimWaist.Platforms.Android
{
    public class BadgeShellBottomNavViewAppearanceTracker : ShellBottomNavViewAppearanceTracker
    {
        // this class is the tracker itself
        private BadgeDrawable _badgeDrawable;

        public BadgeShellBottomNavViewAppearanceTracker(IShellContext shellContext, ShellItem shellItem) : base(shellContext, shellItem)
        {

        }

        public override void SetAppearance(BottomNavigationView bottomView, IShellAppearanceElement appearance)
        {
            //fired with each shell page navigation
            //so only when null we make the update

            base.SetAppearance(bottomView, appearance);

            if (_badgeDrawable is null)
            {
                const int cartTabbarItemIndex = 3;

                _badgeDrawable = bottomView.GetOrCreateBadge(cartTabbarItemIndex);
                UpdateBadgeVisibilityAccordingToCartCount(App.TotalCartCount);

                //create the event implementation of the itemVM
                ItemVM.TotalCartCountChanged += ItemVM_TotalCartCountChanged;
                CartVM.TotalCartCountChanged += CartVM_TotalCartCountChanged;
            }

        }

        private void CartVM_TotalCartCountChanged(object? sender, int newCartCount)
        {
            //new cart count come after firing the event in itemVM
            UpdateBadgeVisibilityAccordingToCartCount(newCartCount);
        }

        private void ItemVM_TotalCartCountChanged(object? sender, int newCartCount)
        {
            //new cart count come after firing the event in itemVM
            UpdateBadgeVisibilityAccordingToCartCount(newCartCount);
        }

        private void UpdateBadgeVisibilityAccordingToCartCount(int cartCount)
        {
            if (cartCount <= 0)
            {
                _badgeDrawable.SetVisible(false);
            }
            else
            {
                _badgeDrawable.Number = cartCount;

                _badgeDrawable.VerticalOffset = 15;

                _badgeDrawable.HorizontalOffset = -15;

                _badgeDrawable.BackgroundColor = Colors.DarkGreen.ToPlatform();
                
                _badgeDrawable.BadgeTextColor = Colors.White.ToPlatform();

                //_badgeDrawable.SetVisible(true);
            }
        }

        protected override void Dispose(bool disposing)
        {
            //fired only on start the app
            ItemVM.TotalCartCountChanged -= ItemVM_TotalCartCountChanged;
            CartVM.TotalCartCountChanged -= CartVM_TotalCartCountChanged;
            base.Dispose(disposing);
        }

    }
}
