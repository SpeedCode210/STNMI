using Melanchall.DryWetMidi.Devices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace STNMI
{
    public delegate void Notify();

    static class ScoreData
    {
        public static event Notify WriteCompleted;

        public static string auteur = "Auteur";
        public static string titre = "Titre";

        public static string enTete = "X:1\nT: Titre\nC:Auteur\nL:1/32\nK:C clef=treble\n%%MIDI program 1 n\n";
        public static Instrument[] instruments = new Instrument[]
        {
            new("Piano",1,1,"treble",26,4200),
            new("Violon",1,41,"treble",180,2100),
            new("Alto",1,42,"alto"),
            new("Violoncelle",1,43,"bass"),
            new("Contrebasse",2,44,"bass"),
            new("Contrebasson",2,44),
            new("Guitare",2,25,"treble",60,1000),
            new("Flûte à bec",0.5,75),
            new("Flûte traversière",1,74),
            new("Flûte piccolo",0.5,73),
            new("Saxophone",1,40),
            new("Trombone",1,58),
            new("Trompette",1,57),
            new("Hautbois",2,69),
            new("Harpe",1,47),
            new("Clarinette en Sib",1.1224620248,72),
            new("Clarinette en La",1.189207115,72),
            new("Cor",1.498307,58)


        };

        public static string score = "";
        public static string gammeScore = "";

        public static Instrument currentInstrument;

        public static Gamme currentGamme;

        public static int currentMIDIDevice;

        public static int tempo;

        public static OutputDevice outputDevice;

        public static void ReloadEnTete()
        {
            enTete = "X:1\nT: " + titre
                + "\nC:" + auteur 
                + "\nL:1/16"
                + "\nK:" + currentGamme.Key
                + "\nQ:" + tempo
                + "\n%%MIDI program " + currentInstrument.MIDI
                + "\nV:1 clef=" + currentInstrument.Clef
                + " \n";
        }

        private static int index2 = 1;
        public static void Write(string a)
        {

            ReloadEnTete();

            if (index2 <= 24) index2++;
            else index2 = 1;

            if (index2 == 24)
                a = a + "\n";
            score += a;

            gammeScore = currentGamme.Convert(score);
            WriteCompleted?.Invoke();
        }

        public static void Reset()
        {
            index2 = 1;
            currentGamme.Reset();
        }
    }
}
