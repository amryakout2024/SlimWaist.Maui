//using Google.Android.Material.Badge;
//using Google.Android.Material.BottomNavigation;
using Microsoft.Maui.Controls.Handlers.Compatibility;

namespace SlimWaist.Renders
{
#if ANDROID
    class TabbarRender : ShellRenderer
    {
        //protected override IShellBottomNavViewAppearanceTracker CreateBottomNavViewAppearanceTracker(ShellItem shellItem)
        //{
        //    return new BadgeShellBottomNavViewAppearanceTracker(this, shellItem);
        //}

        //class BadgeShellBottomNavViewAppearanceTracker : ShellBottomNavViewAppearanceTracker
        //{
        //    private BadgeDrawable _badgeDrawable;

        //    public BadgeShellBottomNavViewAppearanceTracker(IShellContext shellContext, ShellItem shellItem) : base(shellContext, shellItem)
        //    {

        //    }

        //    public override void SetAppearance(BottomNavigationView bottomView, IShellAppearanceElement appearance)
        //    {
        //        base.SetAppearance(bottomView, appearance);

        //        if (_badgeDrawable is null)
        //        {
        //            const int cartTabbarItemIndex = 1;
        //            _badgeDrawable = bottomView.GetOrCreateBadge(cartTabbarItemIndex);
        //            //UpdateBadgeVisibilityAccordingToCartCount(CartViewModel.TotalCartCount);
        //            //CartViewModel.TotalCartCountChanged += CartViewModel_TotalCartCountChanged;
        //        }
        //    }

        //    private void CartViewModel_TotalCartCountChanged(object? sender, int newCount) =>
        //        UpdateBadgeVisibilityAccordingToCartCount(newCount);

        //    private void UpdateBadgeVisibilityAccordingToCartCount(int count)
        //    {
        //        if (count <= 0)
        //        {
        //            _badgeDrawable.SetVisible(false);
        //        }
        //        else
        //        {
        //            _badgeDrawable.Number = count;
        //            _badgeDrawable.BackgroundColor = Colors.DeepPink.ToPlatform();
        //            _badgeDrawable.BadgeTextColor = Colors.White.ToPlatform();
        //            _badgeDrawable.SetVisible(true);
        //        }
        //    }
        //}

    }
#endif
}
