using System.Collections.Generic;

namespace STNMI
{
    public class Gamme
    {
        public string Name { get; protected set; }
        public string Key { get; protected set; }
        public Dictionary<string, string> Notes {get;set;}

        public Gamme(string name,string key)
        {
            Name = name;
            Key = key;
        }

        public virtual string Convert(string s)
        {
            var str = s;
            foreach(var a in Notes)
            {
                str = str.Replace(a.Key, a.Value);
            }
            return str;
        }

        public virtual void Reset()
        {
            return;
        }

        public override string ToString()
        {
            return this.Name;
        }
    }
}
