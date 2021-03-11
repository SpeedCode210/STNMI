using NAudio.Wave;
using System;
using FrequencyToNoteConverter;
using STNMI;
using System.Windows.Threading;
using System.Threading;
using System.Diagnostics;
using System.Collections.Generic;

namespace Guitar_Tuner
{
    public class Sound
    {

        public bool isActive = true;
        BufferedWaveProvider bufferedWaveProvider = null;

        public static List<string> GetDevices()
        {
            List<string> strs = new();
            for (int i = 0; i < WaveInEvent.DeviceCount; i++)
            {
                strs.Add(WaveInEvent.GetCapabilities(i).ProductName);
            }
            return strs;
        }

        public int SelectInputDevice()
        {
            int inputDevice = 0;
            bool isValidChoice = false;

            do
            {
                Console.Clear();
                Console.WriteLine("Please select input or recording device: ");

                for (int i = 0; i < WaveInEvent.DeviceCount; i++)
                {
                    Console.WriteLine(i + ". " + WaveInEvent.GetCapabilities(i).ProductName);
                }

                Console.WriteLine();

                try
                {
                    if (int.TryParse(Console.ReadLine(), out inputDevice))
                    {
                        isValidChoice = true;
                        Console.WriteLine("You have chosen " + WaveInEvent.GetCapabilities(inputDevice).ProductName + ".\n");
                    }
                    else
                    {
                        isValidChoice = false;
                    }
                }
                catch
                {
                    throw new ArgumentException("Device # chosen is out of range.");
                }

            } while (isValidChoice == false);

            return inputDevice;
        }

        public void StartDetect(int inputDevice)
        {
            WaveInEvent waveIn = new WaveInEvent();

            waveIn.DeviceNumber = inputDevice;
            waveIn.WaveFormat = new WaveFormat(44100, 1);
            waveIn.DataAvailable += WaveIn_DataAvailable;

            bufferedWaveProvider = new BufferedWaveProvider(waveIn.WaveFormat);

            // begin record
            waveIn.StartRecording();

            IWaveProvider stream = new Wave16ToFloatProvider(bufferedWaveProvider);
            Pitch pitch = new Pitch(stream);

            byte[] buffer = new byte[8192];
            int bytesRead;

            Console.WriteLine("Play or sing a note! Press ESC to exit at any time. \n");
            var a = "";
            var b = 0;
            do
            {
                bytesRead = stream.Read(buffer, 0, buffer.Length);

                float freq = pitch.Get(buffer);

                if (freq != 0 && freq < 8820)
                {
                    Debug.WriteLine(freq + " | "+ freq * MainWindow.currentInstrument.Coeff);
                    var c = Converter.Convert(freq * MainWindow.currentInstrument.Coeff);
                    if (c == a)
                        b++;
                    else
                    {
                        if(b>0)
                            App.Current.Dispatcher.BeginInvoke(DispatcherPriority.Normal, (SendOrPostCallback)delegate
                            {
                                MainWindow wind = App.Current.MainWindow as MainWindow;

                                wind.Write(a);
                                b = 0;
                                a = c;
                            }, null);
                        else
                        {
                            b++;
                        }

                    }
                }

            } while (bytesRead != 0 && isActive);

            // stop recording
            waveIn.StopRecording();
            waveIn.Dispose();
        }

        void WaveIn_DataAvailable(object sender, WaveInEventArgs e)
        {
            if (bufferedWaveProvider != null)
            {
                bufferedWaveProvider.AddSamples(e.Buffer, 0, e.BytesRecorded);
                bufferedWaveProvider.DiscardOnBufferOverflow = true;
            }
        }


    }
}
