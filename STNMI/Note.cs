namespace FrequencyToNoteConverter
{
    class Note
    {
        public string Name {get; private set; }
        public double Min { get; private set; }
        public double Max { get; private set; }

        public Note(double min, double max, string name)
        {
            Min = min;
            Max = max;
            Name = name;
        }

        public bool CompareFrequency(double frequency)
        {
            return frequency >= Min && frequency < Max;
        }


    }
}
