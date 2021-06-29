
using ModernWpf;
using ModernWpf.Controls;
using System.Windows;
using System.Windows.Controls;

namespace STNMI
{
    public partial class ApparenceSettings : ScrollViewer
    {
        private Settings ParentWindow;
        public ApparenceSettings(Settings w)
        {
            InitializeComponent();
            Theme.SelectedIndex = Parametres.Default.Theme;
            Theme.SelectionChanged += Theme_SelectionChanged;
            ParentWindow = w;
            Lang.SelectedIndex = 0; //Temporaire
            DefaultInstrument.ItemsSource = ScoreData.instruments;
            DefaultInstrument.SelectedIndex = Parametres.Default.DefaultInstrument;
            DefaultInstrument.SelectionChanged += DefaultInstrument_SelectionChanged;
            ReglagesEnabled.IsOn = Parametres.Default.ReglagesEnabled;
            ReglagesEnabled.Toggled += ReglagesEnabled_Toggled;
            DefaultAuthor.Text = Parametres.Default.Author;
            DefaultAuthor.TextChanged += DefaultAuthor_TextChanged;
            Lang.SelectedIndex = Parametres.Default.Language;
            Lang.SelectionChanged += Lang_SelectionChanged;
        }

        private void Lang_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Parametres.Default.Language = Lang.SelectedIndex;
            Parametres.Save();
        }

        private void DefaultAuthor_TextChanged(object sender, TextChangedEventArgs e)
        {
            Parametres.Default.Author = DefaultAuthor.Text;
            Parametres.Save();
        }

        private void ReglagesEnabled_Toggled(object sender, RoutedEventArgs e)
        {
            Parametres.Default.ReglagesEnabled = ReglagesEnabled.IsOn;
            Parametres.Save();
        }

        private void DefaultInstrument_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Parametres.Default.DefaultInstrument = DefaultInstrument.SelectedIndex;
            Parametres.Save();
        }

        private void Theme_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Parametres.Default.Theme = Theme.SelectedIndex;
            Parametres.Save();
            ThemeManager.SetRequestedTheme(ParentWindow, (ElementTheme)Parametres.Default.Theme);
            ThemeManager.SetRequestedTheme(ParentWindow.ParentWindow, (ElementTheme)Parametres.Default.Theme);
        }

        
    }
}
