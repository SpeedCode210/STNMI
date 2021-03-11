using System;

namespace FrequencyToNoteConverter
{
    public static class Converter
    {

        private static Octave[] octaves = new Octave[]
        {
            new(15.434375,31.784375,0),
            new(31.784375,63.56875,1),
            new(63.56875,127.1375,2),
            new(127.1375,254.275,3),
            new(254.275,508.55,4),
            new(508.55,1017.1,5),
            new(1017.1,2034.2,6),
            new(2034.2,4068.4,7),
            new(4068.4,8136.8,8),
            new(8136.8,162737.6,9)
        };

        private static Note[] notes = new Note[]
        {
            new(15.434375,16.8375," C"),
            new(16.8375,17.840625," ^C"),
            new(17.840625,18.9," D"),
            new(18.9,20.021875," _E"),
            new(20.021875,21.2125," E"),
            new(21.2125,23.126," F"),
            new(23.126,23.8125," ^F"),
            new(23.8125,25.228125," G"),
            new(25.228125,26.728125," ^G"),
            new(26.728125,28.31875," A"),
            new(28.31875,30.003125," _B"),
            new(30.003125,31.784375," B")
        };

        public static string Convert(double frequency)
        {
            if (frequency != 0)
            {
                // Trouver octave de la note (0 à 8) 
                int frequencyOctave = -1;
                foreach (var octave in octaves)
                {
                    if (octave.CompareFrequency(frequency))
                    {
                        frequencyOctave = octave.OctaveNum;
                        break;
                    }
                }



                // Trouver nom de la note
                string noteName = "";
                double baseFrenquency = frequency / Math.Pow(2, frequencyOctave);
                foreach (var note in notes)
                {
                    if (note.CompareFrequency(baseFrenquency))
                    {
                        noteName = note.Name;
                        break;
                    }
                }

                string fullName = "";

                if (frequencyOctave == 4)
                    fullName = noteName;
                else if (frequencyOctave < 4)
                    fullName = noteName + (new String(',', 4 - frequencyOctave));
                else
                    fullName = noteName + (new String('\'', frequencyOctave - 4));

                return fullName;
            }
            else 
                return "z";
        }

    }
}
