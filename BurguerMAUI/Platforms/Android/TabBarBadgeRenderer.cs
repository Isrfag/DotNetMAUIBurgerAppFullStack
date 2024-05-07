using BurguerMAUI.ViewModels;
using Google.Android.Material.Badge;
using Google.Android.Material.BottomNavigation;
using Microsoft.Maui.Controls.Handlers.Compatibility;
using Microsoft.Maui.Controls.Platform.Compatibility;
using Microsoft.Maui.Platform;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BurguerMAUI.Platforms.Android.Resources
{
    public class TabBarBadgeRenderer : ShellRenderer
    {
        protected override IShellBottomNavViewAppearanceTracker CreateBottomNavViewAppearanceTracker(ShellItem shellItem)
        {
            return new BadgeShellBottomNavViewAppeareanceTracker(this, shellItem);
        }

        class BadgeShellBottomNavViewAppeareanceTracker : ShellBottomNavViewAppearanceTracker
        {
            private BadgeDrawable _badgeDrawable;
            public BadgeShellBottomNavViewAppeareanceTracker(IShellContext shellContext, ShellItem shellItem) : base(shellContext, shellItem)
            {
            }

            public override void SetAppearance(BottomNavigationView bottomView, IShellAppearanceElement appearance)
            {
                base.SetAppearance(bottomView, appearance);

                if (_badgeDrawable is null)
                {
                    const int cartTabbarItemIdex = 1;

                    _badgeDrawable = bottomView.GetOrCreateBadge(cartTabbarItemIdex);
                    UpdateBadge(CartViewModel.TotalCartCount);
                    CartViewModel.TotalCartCountChanged += CartViewModel_TotalCartCountChanged;
                }
            }

            private void CartViewModel_TotalCartCountChanged(object? sender, int newCount) => 
                UpdateBadge(newCount);

            private void UpdateBadge(int count)
            {
                if (count <= 0)
                {
                    _badgeDrawable.SetVisible(false);
                }
                else
                {
                    _badgeDrawable.Number = count;
                    _badgeDrawable.BackgroundColor = Colors.Orange.ToPlatform();
                    _badgeDrawable.BadgeTextColor = Colors.White.ToPlatform();
                    _badgeDrawable.SetVisible(true);
                }
            }


            protected override void Dispose(bool disposing)
            {
                CartViewModel.TotalCartCountChanged -= CartViewModel_TotalCartCountChanged; //  Quitamos este event handler
                base.Dispose(disposing);
            }
        }

    }
}
