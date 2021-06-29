using ModernWpf;
using ModernWpf.Controls;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

namespace STNMI
{
    public partial class Settings : Window
    {
        private Dictionary<string, ScrollViewer> Elements = new();
        public Window ParentWindow;

        public Settings(Window w, int DefaultView = 0)
        {
            InitializeComponent();

            Elements = new()
            {
                { "Apparence", new ApparenceSettings(this) },
                { "Audio", new AudioSettings() },
                { "Engine", new EngineSettings() },
                { "Updates", new UpdateSettings() }
            };
            NavView.SelectedItem = NavView.MenuItems[DefaultView];
            NavView.Content = Elements[(NavView.MenuItems[DefaultView] as NavigationViewItemBase).Tag.ToString()];
            ParentWindow = w;
            ThemeManager.SetRequestedTheme(this, (ElementTheme)Parametres.Default.Theme);
        }

        private void NavView_ItemInvoked(NavigationView sender, NavigationViewItemInvokedEventArgs args)
        {
            var inv = args.InvokedItemContainer;
            NavView.Content = Elements[inv.Tag.ToString()];
        }
    }
}
