using Microsoft.Maui.Controls.Handlers.Compatibility;
using Microsoft.Maui.Controls.Platform.Compatibility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SlimWaist.Platforms.Android
{
    public class TabbarBadgeRenderer:ShellRenderer
    {
        //this class for create tracker
        protected override IShellBottomNavViewAppearanceTracker CreateBottomNavViewAppearanceTracker(ShellItem shellItem)
        {
            return new BadgeShellBottomNavViewAppearanceTracker(this, shellItem);
        }
    }
}
