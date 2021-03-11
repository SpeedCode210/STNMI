namespace FrequencyToNoteConverter
{

    class Octave
    {
        public double Max { get; private set; }
        public double Min { get; private set; }
        public int OctaveNum { get; private set; }

        public Octave(double min, double max, int octave)
        {
            Min = min;
            Max = max;
            OctaveNum = octave;
        }

        public bool CompareFrequency(double frequency)
        {
            return frequency >= Min && frequency < Max;
        }

    }
}
