using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;

namespace STNMI
{
    public partial class EngineSettings : ScrollViewer
    {
        public EngineSettings()
        {
            InitializeComponent();
            AutoGamme.IsOn = Parametres.Default.AutoDetectGamme;
            AutoGamme.Toggled += AutoGamme_Toggled;
            AutoLength.IsOn = Parametres.Default.AutoDetectLength;
            AutoLength.Toggled += AutoLength_Toggled;
            AutoModul.IsOn = Parametres.Default.AutoDetectModulation;
            AutoModul.Toggled += AutoModul_Toggled;
            DossierTompon.Text = Parametres.Default.TempFolder;
            DossierTompon.TextChanged += DossierTompon_TextChanged;
        }

        private void DossierTompon_TextChanged(object sender, TextChangedEventArgs e)
        {
            Parametres.Default.TempFolder = DossierTompon.Text;
            Parametres.Save();
        }

        private void AutoModul_Toggled(object sender, RoutedEventArgs e)
        {
            Parametres.Default.AutoDetectModulation = AutoModul.IsOn;
            Parametres.Save();
        }

        private void AutoLength_Toggled(object sender, RoutedEventArgs e)
        {
            Parametres.Default.AutoDetectLength = AutoLength.IsOn;
            Parametres.Save();
        }

        private void AutoGamme_Toggled(object sender, RoutedEventArgs e)
        {
            Parametres.Default.AutoDetectGamme = AutoGamme.IsOn;
            Parametres.Save();
        }
    }
}
