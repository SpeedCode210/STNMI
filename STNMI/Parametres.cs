using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace STNMI
{
    public static class Parametres
    {
        public static Config Default = JsonConvert.DeserializeObject<Config>(File.ReadAllText(AppDomain.CurrentDomain.BaseDirectory+"config.json"));
        public static void Save()
        {
            string dflt = JsonConvert.SerializeObject(Default);
            File.WriteAllTextAsync(AppDomain.CurrentDomain.BaseDirectory + "config.json", dflt);
        }
    }

    public class Config
    {
        public int Theme = 0;
        public int DefaultInstrument = 0;
        public bool ReglagesEnabled = true;
        public string Author = "STNMI";
        public int Language = 0;
        public bool AutoUpdates = true;
        public bool AutoDetectGamme = true;
        public bool AutoDetectModulation = false;
        public bool AutoDetectLength = true;
        public bool NoiseReducer = true;
        public string TempFolder = "[Default]";
        public bool UsesAsio = false;
        public int defaultIn = 0;
        public int MidiOut = 0;
        public int VolumeIn = 100;
        public int VolumeOut = 100;

    }
}
