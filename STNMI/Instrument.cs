namespace STNMI
{
    public class Instrument
    {
        public string Name { get;private set; }
        public string Clef { get; private set; }
        public double Coeff { get;private set; }
        public int MIDI { get; private set; }
        public int MinFreq { get; private set; }
        public int MaxFreq { get; private set; }

        public Instrument(string name, double coeff,int midi,string clef = "treble",int min = 20, int max = 10000)
        {
            Name = name;
            Coeff = coeff;
            Clef = clef;
            MIDI = midi;
            MinFreq = min;
            MaxFreq = max;
        }

        public bool TestFreq(float value)
        {
            return MinFreq < value && value < MaxFreq ;
        }

        public override string ToString()
        {
            return this.Name;
        }
    }
}
