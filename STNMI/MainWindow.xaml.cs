using Guitar_Tuner;
using System;
using System.Diagnostics;
using System.IO;
using Melanchall.DryWetMidi.Devices;
using Melanchall.DryWetMidi.Core;
using System.Threading;
using System.Windows;
using System.Windows.Forms;
using Meziantou.WpfFontAwesome;
using System.Threading.Tasks;
using ModernWpf;

namespace STNMI
{
    public partial class MainWindow : Window
    {

        private Sound sound = new();


        private bool effacer = false;



        public MainWindow()
        {
            InitializeComponent();
            instrums.ItemsSource = ScoreData.instruments;
            ScoreData.currentInstrument = ScoreData.instruments[Parametres.Default.DefaultInstrument];
            instrums.SelectedIndex = Parametres.Default.DefaultInstrument;
            instrums.SelectionChanged += Instrums_SelectionChanged;
            gamms.ItemsSource = Gammes.gammes;
            ScoreData.currentGamme = Gammes.gammes[0];
            gamms.SelectedIndex = 0;
            gamms.SelectionChanged += Gamms_SelectionChanged;
            micro.ItemsSource = Sound.GetDevices();
            micro.SelectedIndex = 0;
            midiSorts.ItemsSource = OutputDevice.GetAll();
            midiSorts.SelectionChanged += MidiSorts_SelectionChanged;
            midiSorts.SelectedIndex = 0;
            auteur.TextChanged += Auteur_TextChanged;
            titre.TextChanged += Titre_TextChanged;
            tempo.ValueChanged += Tempo_ValueChanged;
            try
            {
                Directory.CreateDirectory(Path.GetTempPath()+"STNMI");
            }
            catch { }
            this.Closing += MainWindow_Closing;
            ScoreData.WriteCompleted += Write;
            ThemeManager.SetRequestedTheme(this,(ElementTheme)Parametres.Default.Theme);
            if (!Parametres.Default.ReglagesEnabled) 
            { 
                IconeAngle.SolidIcon = FontAwesomeSolidIcon.AngleUp;
                Reglages.Height = 0;
            }
            auteur.Text = Parametres.Default.Author;
        }

        private void Tempo_ValueChanged(ModernWpf.Controls.NumberBox sender, ModernWpf.Controls.NumberBoxValueChangedEventArgs args)
        {
            ScoreData.tempo = (int)tempo.Value;
            ScoreData.ReloadEnTete();
        }

        private void Titre_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {
            ScoreData.titre = titre.Text;
            ScoreData.ReloadEnTete();
        }

        private void Auteur_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {
            ScoreData.auteur = auteur.Text;
            ScoreData.ReloadEnTete();
        }

        private void MainWindow_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            sound.isActive = false;
            System.Windows.Application.Current.Shutdown();
        }

        private void MidiSorts_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            ScoreData.currentMIDIDevice = midiSorts.SelectedIndex;
            ScoreData.outputDevice = OutputDevice.GetById(ScoreData.currentMIDIDevice);
        }

        private void Gamms_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            ScoreData.currentGamme = Gammes.gammes[gamms.SelectedIndex];
            entry.Text = ScoreData.currentGamme.Convert(ScoreData.score);
            ScoreData.ReloadEnTete();
        }

        private void Instrums_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            ScoreData.currentInstrument = ScoreData.instruments[instrums.SelectedIndex];
            ScoreData.ReloadEnTete();
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            Start.IsEnabled = false;
            Stop.IsEnabled = true;
            if (effacer)
            {
                ScoreData.score = "";
                entry.Clear();
                effacer = !effacer;
            }
            int i = micro.SelectedIndex;
            sound.isActive = true;
            Thread th1 = null;
            th1 = new(() =>
            {

                sound.StartDetect(i, ref th1);
            });
            th1.Start();
        }

        private void Write()
        {
            App.Current.Dispatcher.Invoke(() =>
            {
                entry.Text = ScoreData.gammeScore;
                SaveFile();
            });
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            Start.IsEnabled = true;
            Stop.IsEnabled = false;
            try
            {
                sound.isActive = false;
                SaveFile();
                effacer = true;
                ScoreData.Reset();
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
        }
        int index = 1;
        private async void SaveFile()
        {
            try
            {
                var i = index;
                if (i < 20)
                    index++;
                else
                    index = 1;
                i = index;
                await File.WriteAllTextAsync(Path.GetTempPath() + "STNMI\\text.abc", ScoreData.enTete + ScoreData.gammeScore);
                await ProcessAsyncHelper.ExecuteShellCommand("abcm2ps", "-g "+ Path.GetTempPath() + "STNMI\\text.abc -O " + Path.GetTempPath() + "STNMI\\Out" + i, 5000);
                var uri = new Uri(Path.GetTempPath() + "STNMI\\Out" + i + "001" + ".svg");
                img.Source = uri;
            }
            catch { }
        }

        private void Button_Click_4(object sender, RoutedEventArgs e)
        {
            Start.IsEnabled = true;
            Stop.IsEnabled = true;
            try
            {
                sound.isActive = false;
                SaveFile();
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
        }


        public void playSimpleSound(string wavFile)
        {
            PlayMIDI.IsEnabled = false;
            var th2 = new Thread(() =>
            {
                var midiFile = MidiFile.Read(wavFile);
                midiFile.Play(ScoreData.outputDevice);
            });
            th2.Start();

            PlayMIDI.IsEnabled = true;
        }


        private async void PlayMIDI_Click(object sender, RoutedEventArgs e)
        {
            await ProcessAsyncHelper.ExecuteShellCommand("abc2midi", Path.GetTempPath() + "STNMI\\text.abc -o " + Path.GetTempPath() + "STNMI\\play.mid", 10000);
            playSimpleSound( Path.GetTempPath() + "STNMI\\play.mid");
        }

        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private async void Save_Click(object sender, RoutedEventArgs e)
        {
            // Displays a SaveFileDialog so the user can save the Image
            // assigned to Button2.
            SaveFileDialog saveFileDialog1 = new SaveFileDialog();
            saveFileDialog1.Filter = "PDF Document|*.pdf|PNG Image|*.png|MIDI|*.mid|ABC Music Notation|*.abc";
            saveFileDialog1.Title = "Enregister sous";
            saveFileDialog1.ShowDialog();

            // If the file name is not an empty string open it for saving.
            if (saveFileDialog1.FileName != "")
            {
                switch (saveFileDialog1.FilterIndex)
                {
                    case 1:
                        await ProcessAsyncHelper.ExecuteShellCommand("abcm2ps", "-g " + Path.GetTempPath() + "STNMI\\text.abc -O " + Path.GetTempPath() + "STNMI\\Out", 5000);
                        await ProcessAsyncHelper.ExecuteShellCommand(AppDomain.CurrentDomain.BaseDirectory + "Inkscape/bin/inkscape.exe", "-p " + Path.GetTempPath() + "STNMI\\Out001.svg --export-filename=" + saveFileDialog1.FileName + " --export-dpi=300", 10000);
                        break;

                    case 2:
                        await ProcessAsyncHelper.ExecuteShellCommand("abcm2ps", "-g " + Path.GetTempPath() + "STNMI\\text.abc -O " + Path.GetTempPath() + "STNMI\\Out", 5000);
                        await ProcessAsyncHelper.ExecuteShellCommand(AppDomain.CurrentDomain.BaseDirectory + "Inkscape/bin/inkscape.exe", "-p " + Path.GetTempPath() + "STNMI\\Out001.svg --export-filename=" + saveFileDialog1.FileName + " --export-dpi=300", 10000);
                        break;
                    case 3:
                        await ProcessAsyncHelper.ExecuteShellCommand("abc2midi", Path.GetTempPath() + "STNMI\\text.abc -o " + saveFileDialog1.FileName, 10000);
                        break;
                    case 4:
                        await File.WriteAllTextAsync(saveFileDialog1.FileName, ScoreData.enTete + ScoreData.gammeScore);
                        break;
                }


            }
        }

        private bool isAnimatingReglages = false;
        private async void ReglagesButton_Click(object sender, RoutedEventArgs e)
        {
            if (isAnimatingReglages) return;
            isAnimatingReglages = true;
            if (IconeAngle.SolidIcon == FontAwesomeSolidIcon.AngleDown)
            {
                IconeAngle.SolidIcon = FontAwesomeSolidIcon.AngleUp;
                while (Reglages.Height > 0)
                {
                    Reglages.Height -= 20;
                    await Task.Delay(10);
                }
               
            }
            else
            {

                IconeAngle.SolidIcon = FontAwesomeSolidIcon.AngleDown;
                while (Reglages.Height < 300)
                {
                    Reglages.Height += 20;
                    await Task.Delay(10);
                }

            }
            isAnimatingReglages = false;
        }

        private void About_Click(object sender, RoutedEventArgs e)
        {
            new About().Show();
        }

        private void Settings_Click(object sender, RoutedEventArgs e)
        {
            new Settings(this).Show();
        }

        private void Update_Click(object sender, RoutedEventArgs e)
        {
            new Settings(this,3).Show();
        }
    }
}
