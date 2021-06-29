using System.Windows;
using System.Windows.Controls;

namespace STNMI
{
    public partial class UpdateSettings : ScrollViewer
    {
        public UpdateSettings()
        {
            InitializeComponent();
            AutoUpdates.IsOn = Parametres.Default.AutoUpdates;
            AutoUpdates.Toggled += AutoUpdates_Toggled;
            CurrentVersion.Content = AppData.GetAppVersion();
            SearchUpdates.Click += SearchUpdates_Click;
        }

        private void SearchUpdates_Click(object sender, RoutedEventArgs e)
        {
            //throw new System.NotImplementedException();
        }

        private void AutoUpdates_Toggled(object sender, RoutedEventArgs e)
        {
            Parametres.Default.AutoUpdates = AutoUpdates.IsOn;
            Parametres.Save();
        }
    }
}
