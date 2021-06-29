using ModernWpf;
using System.Diagnostics;
using System.Windows;
using System.Windows.Navigation;

namespace STNMI
{
    public partial class About : Window
    {
        public About()
        {
            InitializeComponent();
            ThemeManager.SetRequestedTheme(this, (ElementTheme)Parametres.Default.Theme);
            Version.Content = Version.Content.ToString().Replace("(Version)", AppData.GetAppVersion());
        }

        private void Hyperlink_RequestNavigate(object sender, RequestNavigateEventArgs e)
        {
            Process.Start(new ProcessStartInfo
            {
                FileName = e.Uri.AbsoluteUri,
                UseShellExecute = true
            });
            e.Handled = true;
        }
    }
}
