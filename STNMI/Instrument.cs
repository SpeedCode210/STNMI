namespace STNMI
{
    public class Instrument
    {
        public string Name { get;private set; }
        public double Coeff { get;private set; }

        public Instrument(string name, double coeff)
        {
            Name = name;
            Coeff = coeff;
        }

        public override string ToString()
        {
            return this.Name;
        }
    }
}
