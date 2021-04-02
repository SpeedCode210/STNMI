using Guitar_Tuner;
using System;
using System.Diagnostics;
using System.IO;
using Melanchall.DryWetMidi.Devices;
using Melanchall.DryWetMidi.Core;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Forms;
using System.Windows.Media.Imaging;
using System.Windows.Threading;

namespace STNMI
{
    public partial class MainWindow : Window
    {

        private Sound sound = new();
        private string enTete = "X:1\nT: Titre\nC:Auteur\nK:C clef=treble\n%%MIDI program 1 n\n";
        private Instrument[] instruments = new Instrument[]
        {
            new("Piano",1,1),
            new("Violon",1,41),
            new("Alto",1,42,"alto"),
            new("Violoncelle",1,43,"bass"),
            new("Contrebasse",2,44,"bass"),
            new("Contrebasson",2,44),
            new("Guitare",2,25),
            new("Flûte à bec",0.5,75),
            new("Flûte traversière",1,74),
            new("Flûte piccolo",0.5,73),
            new("Trombone",1,58),
            new("Trompette",1,57),
            new("Hautbois",2,69),
            new("Harpe",1,47),
            new("Clarinette en Sib",1.1224620248,72),
            new("Clarinette en La",1.189207115,72),
            new("Cor",1.498307,58)


        };


        private bool effacer = false;

        private string score = "";

        public static Instrument currentInstrument;

        public static Gamme currentGamme;

        private int currentMIDIDevice;

        OutputDevice outputDevice;

        public MainWindow()
        {
            InitializeComponent();
            instrums.ItemsSource = instruments;
            currentInstrument = instruments[0];
            instrums.SelectedIndex = 0;
            instrums.SelectionChanged += Instrums_SelectionChanged;
            gamms.ItemsSource = Gammes.gammes;
            currentGamme = Gammes.gammes[0];
            gamms.SelectedIndex = 0;
            gamms.SelectionChanged += Gamms_SelectionChanged;
            micro.ItemsSource = Sound.GetDevices();
            micro.SelectedIndex = 0;
            midiSorts.ItemsSource = OutputDevice.GetAll();
            midiSorts.SelectionChanged += MidiSorts_SelectionChanged;
            midiSorts.SelectedIndex = 0;
            try
            {
                Directory.CreateDirectory(AppDomain.CurrentDomain.BaseDirectory + "temp");
            }
            catch { }
        }

        private void MidiSorts_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            currentMIDIDevice = midiSorts.SelectedIndex;
            outputDevice = OutputDevice.GetById(currentMIDIDevice);
        }

        private void Gamms_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            currentGamme = Gammes.gammes[gamms.SelectedIndex];
            Debug.WriteLine(currentGamme.Name);
            entry.Text = currentGamme.Convert(score);
            if (titre != null && auteur != null && currentInstrument != null)
                enTete = "X:1\nT: " + titre.Text + "\nC:" + auteur.Text + "\nK:" + currentGamme.Key + " clef=" + currentInstrument.Clef + "\n%%MIDI program " + currentInstrument.MIDI + " n\n";
        }

        private void Instrums_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            currentInstrument = instruments[instrums.SelectedIndex];
            Debug.WriteLine(currentInstrument.Name);
            if (titre != null && auteur != null && currentInstrument != null)
                enTete = "X:1\nT: " + titre.Text + "\nC:" + auteur.Text + "\nK:" + currentGamme.Key + " clef=" + currentInstrument.Clef + "\n%%MIDI program " + currentInstrument.MIDI + " n\n";
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            System.Windows.Application.Current.Shutdown();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            Start.IsEnabled = false;
            Stop.IsEnabled = true;
            if (effacer)
            {
                score = "";
                entry.Clear();
                effacer = !effacer;
            }
            int i = micro.SelectedIndex;
            Task.Run(() =>
            {
                sound.isActive = true;
                sound.StartDetect(i);
            });
        }

        int index2 = 1;
        public void Write(string a)
        {
            if (titre != null && auteur != null && currentInstrument != null)
                enTete = "X:1\nT: " + titre.Text + "\nC:" + auteur.Text + "\nK:" + currentGamme.Key + " clef=" + currentInstrument.Clef + "\n%%MIDI program " + currentInstrument.MIDI + " n\n";
            if (index2 <= 24)
                index2++;
            else
                index2 = 1;
            if (index2 == 28)
                a = a + "\n";
            score = score + a;
            entry.Text = currentGamme.Convert(score);
            //entry.Text = entry.Text + a;
            SaveFile(entry.Text);
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            Start.IsEnabled = true;
            Stop.IsEnabled = false;
            try
            {
                sound.isActive = false;
                SaveFile(entry.Text, true);
                effacer = true;
                Gammes.gammes[0] = new GammeAutomatique("Détection automatique");
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
        }
        int index = 1;
        int index3 = 1;
        private async void SaveFile(string s, bool b = false)
        {
            index3++;
            if (index3 % 2 == 0 || b)
            {
                var i = index;
                if (i < 20)
                    index++;
                else
                    index = 1;
                i = index;
                await File.WriteAllTextAsync("temp\\text.abc", enTete + s);
                await ProcessAsyncHelper.ExecuteShellCommand("abcm2ps", "-g temp\\text.abc", 5000);
                var proc = await ProcessAsyncHelper.ExecuteShellCommand(AppDomain.CurrentDomain.BaseDirectory + "Inkscape/bin/inkscape", "-p " + AppDomain.CurrentDomain.BaseDirectory + "Out001.svg --export-filename=" + AppDomain.CurrentDomain.BaseDirectory + "temp\\Out" + i + ".png --export-dpi=120", 5000);
                var uri = new Uri(AppDomain.CurrentDomain.BaseDirectory + "temp\\Out" + i + ".png");
                if (proc.Completed)
                {
                    BitmapImage bitmap = new();
                    bitmap = new BitmapImage(uri);
                    img.Source = bitmap;
                }
            }
        }



        private void titre_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {
            if (titre != null && auteur != null && currentInstrument != null)
                enTete = "X:1\nT: " + titre.Text + "\nC:" + auteur.Text + "\nK:" + currentGamme.Key + " clef=" + currentInstrument.Clef + "\n%%MIDI program " + currentInstrument.MIDI + " n\n";
        }

        private void Button_Click_4(object sender, RoutedEventArgs e)
        {
            Start.IsEnabled = true;
            Stop.IsEnabled = true;
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

        private void Button_Click_5(object sender, RoutedEventArgs e)
        {
            var abt = new about();
            abt.Show();
        }

        private async void Enregistrer_Click(object sender, RoutedEventArgs e)
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
                        await ProcessAsyncHelper.ExecuteShellCommand("abcm2ps", "-g temp\\text.abc", 5000);
                        await ProcessAsyncHelper.ExecuteShellCommand(AppDomain.CurrentDomain.BaseDirectory + "Inkscape/bin/inkscape","-p " + AppDomain.CurrentDomain.BaseDirectory + "Out001.svg --export-filename=" + saveFileDialog1.FileName + " --export-dpi=300",10000);
                        break;

                    case 2:
                        await ProcessAsyncHelper.ExecuteShellCommand("abcm2ps", "-g temp\\text.abc", 5000);
                        await ProcessAsyncHelper.ExecuteShellCommand(AppDomain.CurrentDomain.BaseDirectory + "Inkscape/bin/inkscape", "-p " + AppDomain.CurrentDomain.BaseDirectory + "Out001.svg --export-filename=" + saveFileDialog1.FileName + " --export-dpi=300", 10000);
                        break;
                    case 3:
                        await ProcessAsyncHelper.ExecuteShellCommand("abc2midi","temp\\text.abc -o " + saveFileDialog1.FileName,10000);
                        break;
                    case 4:
                        await File.WriteAllTextAsync(saveFileDialog1.FileName, enTete + entry.Text);
                        break;
                }


            }
        }


        public void playSimpleSound(string wavFile)
        {
            App.Current.Dispatcher.BeginInvoke(DispatcherPriority.Normal, (SendOrPostCallback)delegate
            {
                PlayMIDI.IsEnabled = false;
            }, null);
            var midiFile = MidiFile.Read(wavFile);

            midiFile.Play(outputDevice);
            App.Current.Dispatcher.BeginInvoke(DispatcherPriority.Normal, (SendOrPostCallback)delegate
            {
                PlayMIDI.IsEnabled = true;
            }, null);
        }


        private async void PlayMIDI_Click(object sender, RoutedEventArgs e)
        {
            await ProcessAsyncHelper.ExecuteShellCommand("abc2midi","temp\\text.abc -o temp\\play.mid",10000);
            playSimpleSound(AppDomain.CurrentDomain.BaseDirectory + "temp\\play.mid");
        }
    }
}
