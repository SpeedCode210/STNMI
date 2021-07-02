using Guitar_Tuner;
using Melanchall.DryWetMidi.Devices;
using NAudio.Wave;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace STNMI
{
    public partial class AudioSettings : ScrollViewer
    {
        public AudioSettings()
        {
            InitializeComponent();

            Asio.IsOn = Parametres.Default.UsesAsio;
            Asio.Toggled += Asio_Toggled;
            AsioDriver.ItemsSource = AsioOut.GetDriverNames();
            AsioDriver.SelectedIndex = Parametres.Default.AsioDriver;
            AsioDriver.SelectionChanged += AsioDriver_SelectionChanged;
            if (Parametres.Default.UsesAsio)
            {
                var asioOut = new AsioOut(AsioOut.GetDriverNames()[AsioDriver.SelectedIndex]);
                var Inputs = new List<string>();
                for (int i = 0; i < asioOut.DriverInputChannelCount; i++)
                {
                    Inputs.Add(asioOut.AsioInputChannelName(i));
                }
                asioOut.Dispose();
                AudioIn.ItemsSource = Inputs;
            }
            else
            {
                AudioIn.ItemsSource = Sound.GetDevices();
            }
            AudioIn.SelectedIndex = Parametres.Default.defaultIn;
            AudioIn.SelectionChanged += AudioIn_SelectionChanged;
            AudioOut.ItemsSource = OutputDevice.GetAll();
            AudioOut.SelectedIndex = Parametres.Default.MidiOut;
            AudioOut.SelectionChanged += AudioOut_SelectionChanged;
        }

        private void AudioOut_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Parametres.Default.MidiOut = AudioOut.SelectedIndex;
            Parametres.Save();
            ScoreData.outputDevice = OutputDevice.GetById(Parametres.Default.MidiOut);
        }

        private void AudioIn_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Parametres.Default.defaultIn = AudioIn.SelectedIndex;
            Parametres.Save();
        }

        private void AsioDriver_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Parametres.Default.AsioDriver = AsioDriver.SelectedIndex;
            Parametres.Save();
            ReloadDevices();
        }

        private void Asio_Toggled(object sender, RoutedEventArgs e)
        {
            Parametres.Default.UsesAsio = Asio.IsOn;
            Parametres.Save();
            ReloadDevices();
        }

        private void ReloadDevices()
        {
            AudioIn.SelectionChanged -= AudioIn_SelectionChanged;
            if (Parametres.Default.UsesAsio)
            {
                var asioOut = new AsioOut(AsioOut.GetDriverNames()[AsioDriver.SelectedIndex]);
                var Inputs = new List<string>();
                for (int i = 0; i < asioOut.DriverInputChannelCount; i++)
                {
                    Inputs.Add(asioOut.AsioInputChannelName(i));
                }
                asioOut.Dispose();
                AudioIn.ItemsSource = Inputs;
            }
            else
            {
                AudioIn.ItemsSource = Sound.GetDevices();
            }
            AudioIn.SelectedIndex = Parametres.Default.defaultIn;
            AudioIn.SelectionChanged += AudioIn_SelectionChanged;

        }

    }
}
