using System.Collections.Generic;

namespace STNMI
{
    public class Gamme
    {
        public string Name { get; private set; }
        public string Key { get; private set; }
        public Dictionary<string, string> Notes {get;set;}

        public Gamme(string name,string key)
        {
            Name = name;
            Key = key;
        }

        public string Convert(string s)
        {
            var str = s;
            foreach(var a in Notes)
            {
                str = str.Replace(a.Key, a.Value);
            }
            return str;
        }

        public override string ToString()
        {
            return this.Name;
        }
    }
}
