using Guitar_Tuner;
using System;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media.Imaging;

namespace STNMI
{
    public partial class MainWindow : Window
    {

        private Sound sound;
        private string enTete = "X:1\nT: Titre\nC:Auteur\nK:C clef=treble\n";
        private Instrument[] instruments = new Instrument[]
        {
            new("Piano",1),
            new("Violon",1),
            new("Alto",1),
            new("Violoncelle",1),
            new("Contrebasse",2),
            new("Contrebasson",2),
            new("Guitare",2),
            new("Flûte à bec",0.5),
            new("Flûte traversière",1),
            new("Flûte piccolo",0.5),
            new("Trombone",1),
            new("Hautbois",1),
            new("Harpe",1),
            new("Clarinette en Sib",1.1224620248),
            new("Clarinette en La",1.189207115),
            new("Cor",1.498307)


        };
        private bool effacer = false;

        public static Instrument currentInstrument;

        public MainWindow()
        {
            InitializeComponent();
            instrums.ItemsSource = instruments;
            currentInstrument = instruments[0];
            instrums.SelectedIndex = 0;
            instrums.SelectionChanged += Instrums_SelectionChanged;
            micro.ItemsSource = Sound.GetDevices();
            micro.SelectedIndex = 0;
            try
            {
                Directory.CreateDirectory(AppDomain.CurrentDomain.BaseDirectory + "temp");
            }
            catch{}
        }

        private void Instrums_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            currentInstrument = instruments[instrums.SelectedIndex];
            Debug.WriteLine(currentInstrument.Name);
            if (titre != null && auteur != null)
                enTete = "X:1\nT: " + titre.Text + "\nC:" + auteur.Text + "\nK:C clef=treble\n";
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            if (effacer)
            {
                entry.Clear();
                effacer = !effacer;
            }
            int i = micro.SelectedIndex;
            Task.Run(() =>
            {
                sound = new Sound();
                sound.StartDetect(i);
            });
        }

        int index2=1;
        public void Write(string a)
        {
            if (index2 <= 30)
                index2++;
            else
                index2 = 1;
            if (index2 == 28)
                a = a + "\n";
            entry.Text = entry.Text + a;
            SaveFile(entry.Text);
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            try
            {
                sound.isActive = false;
                SaveFile(entry.Text,true);
                effacer = true;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
        }
        int index = 1;
        int index3 = 1;
        private async void SaveFile(string s,bool b = false)
        {
            index3++;
            if(index3 % 2 == 0 || b) {
                var i = index;
                if (i < 20)
                    index++;
                else
                    index = 1;
                i = index;
                await File.WriteAllTextAsync("temp\\text.abc", enTete+s);
                BitmapImage bitmap = new();
                await Task.Run(() =>
                {
                    ExecuteCommandSync("abcm2ps -g temp\\text.abc");
                    string cmd = AppDomain.CurrentDomain.BaseDirectory + "Inkscape/bin/inkscape -p " + AppDomain.CurrentDomain.BaseDirectory + "Out001.svg --export-filename=" + AppDomain.CurrentDomain.BaseDirectory + "temp\\Out" + i + ".png --export-dpi=150";
                    Debug.WriteLine(cmd);
                    ExecuteCommandSync(cmd);
                });
                var uri = new Uri(AppDomain.CurrentDomain.BaseDirectory + "temp\\Out" + i + ".png");
                bitmap = new BitmapImage(uri);
                img.Source = bitmap;
            }
        }



        private static void ExecuteCommandSync(object command)
        {
            try
            {
                System.Diagnostics.ProcessStartInfo procStartInfo =
                    new System.Diagnostics.ProcessStartInfo("cmd", "/c " + command);

                procStartInfo.RedirectStandardOutput = true;
                procStartInfo.UseShellExecute = false;
                procStartInfo.CreateNoWindow = true;
                System.Diagnostics.Process proc = new System.Diagnostics.Process();
                proc.StartInfo = procStartInfo;
                proc.Start();
                string result = proc.StandardOutput.ReadToEnd();
                Debug.Write(result);
            }
            catch{}
        }

        private void titre_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {
            if(titre != null && auteur != null)
            enTete = "X:1\nT: " + titre.Text + "\nC:" + auteur.Text + "\nK:C clef=treble\n";
        }

        private void Button_Click_4(object sender, RoutedEventArgs e)
        {
            try
            {
                sound.isActive = false;
                SaveFile(entry.Text, true);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
        }
    }
}
