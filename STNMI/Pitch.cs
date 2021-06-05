using NAudio.Wave;
using System;
using System.Diagnostics;

namespace Guitar_Tuner
{
    public class Pitch
    {
        IWaveProvider source;
        WaveBuffer waveBuffer;
        Autocorrelator pitchDetector;

        public Pitch(IWaveProvider src)
        {
            if (src.WaveFormat.SampleRate != 44100)
            {
                throw new ArgumentException("Source must be at 44.1kHz");
            }

            if (src.WaveFormat.Encoding != WaveFormatEncoding.IeeeFloat)
            {
                throw new ArgumentException("Source must be IEEE floating point audio data");
            }

            if (src.WaveFormat.Channels != 1)
            {
                throw new ArgumentException("Source must be a mono input source");
            }

            source = src;
            pitchDetector = new Autocorrelator(source.WaveFormat.SampleRate);
            waveBuffer = new WaveBuffer(8192);
        }

        public float Get(byte[] buffer)
        {
            if (waveBuffer == null || waveBuffer.MaxSize < buffer.Length)
            {
                waveBuffer = new WaveBuffer(buffer);
            }

            int bytesRead = source.Read(waveBuffer, 0, buffer.Length);

            if (bytesRead > 0)
            {
                bytesRead = buffer.Length;
            }

            int frames = bytesRead / sizeof(float);
            return pitchDetector.DetectPitch(waveBuffer.FloatBuffer, frames);
        }
    }
}
