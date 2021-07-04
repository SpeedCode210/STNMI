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

        public static string enTete = "X:1\nT: Titre\nC:Auteur\nL:1/32\nK:C clef=treble\nM:4/4\n%%MIDI program 1 n\n";
        public static Instrument[] instruments = new Instrument[]
        {
            new("Piano",1,1,"treble",26,4200),
            new("Violon",1,41,"treble",180,2100),
            new("Alto",1,42,"alto",125,1350),
            new("Violoncelle",1,43,"bass",60,680),
            new("Contrebasse",2,44,"bass",20,600),
            new("Guitare",2,25,"treble",60,1000),
            new("Ukulele",1,25,"treble",250,900),
            new("Flûte à bec soprano",0.5,75,"treble",500,3200),
            new("Flûte à bec alto",1,75,"treble",345,2200),
            new("Flûte traversière",1,74,"treble",240,2150),
            new("Flûte piccolo",0.5,73,"treble",580,4250),
            new("Saxophone Soprano",1.12246568745,64,"treble",200,1400),
            new("Saxophone Alto",1.68177095359,65,"treble",130,900),
            new("Saxophone Ténor",2.2448232688,66,"treble",100,670),
            new("Saxophone Barython",3.36340009173,67,"treble",62,445),
            new("Basson",1,70,"bass",105,900),
            new("Contrebasson",2,70,"bass",50,500),
            new("Banjo",2,105,"treble",125,720),
            new("Accordéon",1,15,"treble",26,2100),
            new("Trompette en Ut",1,57,"treble",180,1050),
            new("Trompette en Sib",1.1224620248,57,"treble",180,1050),
            new("Hautbois",1,69,"treble",240,1700),
            new("Mandoline",1,104,"treble",180,2100),
            new("Mandole",2,104,"treble",90,1050),
            new("Harpe",1,47,"treble",60,3200),
            new("Harmonica",1,22,"treble",500,4200),
            new("Clarinette en Sib",1.1224620248,72),
            new("Clarinette en La",1.189207115,72),
            new("Cor d'harmonie",1.498307,58,"treble",85,720),
            new("Cor anglais",1.498307,69,"treble",160,1600),
            new("Autre",1,1,"treble",20,10000)
        };

        public static string score = "";
        public static string gammeScore = "";

        public static Instrument currentInstrument;

        public static Gamme currentGamme;

        public static int tempo;

        public static OutputDevice outputDevice;

        public static string meter = "4/4";

        public static void ReloadEnTete()
        {
            enTete = "X:1\nT: " + titre
                + "\nC:" + auteur 
                + "\nL:1/16"
                + "\nM:" + meter
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
