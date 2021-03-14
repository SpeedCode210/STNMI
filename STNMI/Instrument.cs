namespace STNMI
{
    public class Instrument
    {
        public string Name { get;private set; }
        public string Clef { get; private set; }
        public double Coeff { get;private set; }

        public Instrument(string name, double coeff,string clef = "treble")
        {
            Name = name;
            Coeff = coeff;
            Clef = clef;
        }

        public override string ToString()
        {
            return this.Name;
        }
    }
}
