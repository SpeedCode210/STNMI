using NAudio.Wave;
using System;
using FrequencyToNoteConverter;
using STNMI;
using System.Threading;
using System.Diagnostics;
using System.Collections.Generic;

namespace Guitar_Tuner
{
    public class Sound
    {

        public bool isActive = true;
        BufferedWaveProvider bufferedWaveProvider = null;
        WaveInEvent waveIn = new WaveInEvent();
        IWaveProvider stream;
        Pitch pitch;

        public static List<string> GetDevices()
        {
            List<string> strs = new();
            for (int i = 0; i < WaveInEvent.DeviceCount; i++)
            {
                strs.Add(WaveInEvent.GetCapabilities(i).ProductName);
            }
            return strs;
        }

        public void StartDetect(int inputDevice, ref Thread th)
        {
            waveIn = new WaveInEvent();

            waveIn.DeviceNumber = inputDevice;
            waveIn.WaveFormat = new WaveFormat(44100, 1);
            waveIn.DataAvailable += WaveIn_DataAvailable;

            bufferedWaveProvider = new BufferedWaveProvider(waveIn.WaveFormat);
            // begin record
            waveIn.StartRecording();

            stream = new Wave16ToFloatProvider(bufferedWaveProvider);
            pitch = new Pitch(stream);

        }

        string a = "";
        int b = 0;
        long milliseconds;

        private void WaveIn_DataAvailable(object sender, WaveInEventArgs e)
        {

            if (bufferedWaveProvider == null) return;

            bufferedWaveProvider.AddSamples(e.Buffer, 0, e.BytesRecorded);
            bufferedWaveProvider.DiscardOnBufferOverflow = true;

            if (!isActive)
            {
                try 
                {
                waveIn.StopRecording();
                waveIn.Dispose();
                waveIn.DataAvailable -= WaveIn_DataAvailable;
                }
                catch(Exception err)
                {
                    Debug.WriteLine(err);
                }
                        
                a = "";
                b = 0;
                milliseconds = 0;
                return;
            }

            byte[] buffer = new byte[8192];
            stream.Read(buffer, 0, buffer.Length);

            float freq = pitch.Get(buffer);
            if (freq==0)
            {
                if (a == "") return;
                var c = " z";
                if (c == a)
                    b++;
                else
                {
                    if (b > 0)
                    {
                        int temps = NoteTimeDetector.Detect(milliseconds,DateTimeOffset.Now.ToUnixTimeMilliseconds());
                        if((!String.IsNullOrEmpty(a)) && temps > 0 )
                            ScoreData.Write(a + temps);
                        milliseconds = DateTimeOffset.Now.ToUnixTimeMilliseconds();
                        b = 0;
                        a = c;
                    }
                    else
                    {
                        b++;
                    }

                }
            }
            else
            if (freq != 0 && ScoreData.currentInstrument.TestFreq(freq))
            {
                Debug.WriteLine(freq + " | " + freq * ScoreData.currentInstrument.Coeff);
                var c = Converter.Convert(freq * ScoreData.currentInstrument.Coeff);
                if (c == a)
                    b++;
                else
                {
                    if (b > 0)
                    {

                        int temps = NoteTimeDetector.Detect(milliseconds, DateTimeOffset.Now.ToUnixTimeMilliseconds());
                        try
                        {
                            var d = Converter.ConvertBase(freq * ScoreData.currentInstrument.Coeff);
                            GammeAutomatique.NbNotes[d]++;
                        }
                        catch (KeyNotFoundException err)
                        {
                            Debug.WriteLine(err);
                        }
                        if ((!String.IsNullOrEmpty(a)) && temps > 0)
                            ScoreData.Write(a + temps);
                        b = 0;
                        a = c;
                        milliseconds = DateTimeOffset.Now.ToUnixTimeMilliseconds();

                    }
                    else
                    {
                        b++;
                    }

                }
            }

        }


    }
}
