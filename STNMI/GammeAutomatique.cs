using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace STNMI
{
    public class GammeAutomatique : Gamme
    {
        public static Dictionary<string, double> NbNotes = new Dictionary<string, double>()
        {
                { "a", 0 },
                { "b", 0 },
                { "c", 0 },
                { "d", 0 },
                { "e", 0 },
                { "f", 0 },
                { "g", 0 },
                { "h", 0 },
                { "i", 0 },
                { "j", 0 },
                { "k", 0 },
                { "l", 0 }
        };

        private enum gam
        {
            Auto, C, F, Gm, Cm, Fm, Db, Gb, Cb, G, D, A, E, B, Fd, Cd
        }

        public GammeAutomatique(string name) : base(name, "C")
        {
            Notes = new()
            {
                { "a", "C" },
                { "b", "^C" },
                { "c", "D" },
                { "d", "_E" },
                { "e", "E" },
                { "f", "F" },
                { "g", "^F" },
                { "h", "G" },
                { "i", "^G" },
                { "j", "A" },
                { "k", "_B" },
                { "l", "B" }
            };
        }

        public override string Convert(string s)
        {
            var str = s;

            if (NbNotes["l"] >= NbNotes["k"] && NbNotes["f"] >= NbNotes["g"]
                && NbNotes["a"] >= NbNotes["b"] && NbNotes["e"] >= NbNotes["d"])
                ChangeGamme(gam.C);


            else if (NbNotes["k"] >= NbNotes["l"] && NbNotes["e"] >= NbNotes["d"]
                && NbNotes["h"] >= NbNotes["g"] && NbNotes["f"] >= NbNotes["g"])
                ChangeGamme(gam.F);

            else if (NbNotes["g"] >= NbNotes["f"] && NbNotes["l"] >= NbNotes["k"]
                && NbNotes["a"] >= NbNotes["b"] && NbNotes["h"] >= NbNotes["i"])
                ChangeGamme(gam.G);


            else if (NbNotes["k"] >= NbNotes["l"] && NbNotes["d"] >= NbNotes["e"]
                && NbNotes["j"] >= NbNotes["i"] && NbNotes["c"] >= NbNotes["b"])
                ChangeGamme(gam.Gm);

            else if (NbNotes["b"] >= NbNotes["a"] && NbNotes["i"] >= NbNotes["h"]
                && NbNotes["c"] >= NbNotes["d"] && NbNotes["j"] >= NbNotes["k"])
                ChangeGamme(gam.A);


            else if (NbNotes["g"] >= NbNotes["f"] && NbNotes["b"] >= NbNotes["a"]
                && NbNotes["h"] >= NbNotes["i"] && NbNotes["c"] >= NbNotes["d"])
                ChangeGamme(gam.D);


            else if (NbNotes["d"] >= NbNotes["e"] && NbNotes["i"] >= NbNotes["j"]
                && NbNotes["c"] >= NbNotes["b"] && NbNotes["h"] >= NbNotes["g"])
                ChangeGamme(gam.Cm);

            else if (NbNotes["i"] >= NbNotes["h"] && NbNotes["d"] >= NbNotes["c"]
                && NbNotes["j"] >= NbNotes["k"] && NbNotes["e"] >= NbNotes["f"])
                ChangeGamme(gam.E);


            else if (NbNotes["i"] >= NbNotes["j"] && NbNotes["b"] >= NbNotes["c"]
                && NbNotes["h"] >= NbNotes["g"] && NbNotes["a"] >= NbNotes["l"])
                ChangeGamme(gam.Fm);

            else if (NbNotes["d"] >= NbNotes["c"] && NbNotes["k"] >= NbNotes["j"]
                && NbNotes["e"] >= NbNotes["f"] && NbNotes["l"] >= NbNotes["a"])
                ChangeGamme(gam.B);


            else if (NbNotes["b"] >= NbNotes["c"] && NbNotes["g"] >= NbNotes["h"]
                && NbNotes["a"] >= NbNotes["l"] && NbNotes["f"] >= NbNotes["e"])
                ChangeGamme(gam.Db);

            else if (NbNotes["k"] >= NbNotes["j"] && NbNotes["f"] >= NbNotes["e"]
                && NbNotes["l"] >= NbNotes["a"])
                ChangeGamme(gam.Fd);


            else if (NbNotes["g"] >= NbNotes["h"] && NbNotes["l"] >= NbNotes["a"]
                && NbNotes["f"] >= NbNotes["e"])
                ChangeGamme(gam.Gb);

            else if (NbNotes["f"] >= NbNotes["e"] && NbNotes["a"] >= NbNotes["l"])
                ChangeGamme(gam.Cd);


            else if (NbNotes["l"] >= NbNotes["a"] && NbNotes["e"] >= NbNotes["f"])
                ChangeGamme(gam.Cb);



            foreach (var a in Notes)
            {
                str = str.Replace(a.Key, a.Value);
            }
            return str;
        }

        private void ChangeGamme(gam g)
        {
            Key = Gammes.gammes[(int)g].Key;
            Notes = Gammes.gammes[(int)g].Notes;
            Debug.WriteLine(Key);
        }

        public override void Reset()
        {
            Notes = new()
            {
                { "a", "C" },
                { "b", "^C" },
                { "c", "D" },
                { "d", "_E" },
                { "e", "E" },
                { "f", "F" },
                { "g", "^F" },
                { "h", "G" },
                { "i", "^G" },
                { "j", "A" },
                { "k", "_B" },
                { "l", "B" }
            };

            Key = "C";

            NbNotes = new Dictionary<string, double>()
        {
                { "a", 0 },
                { "b", 0 },
                { "c", 0 },
                { "d", 0 },
                { "e", 0 },
                { "f", 0 },
                { "g", 0 },
                { "h", 0 },
                { "i", 0 },
                { "j", 0 },
                { "k", 0 },
                { "l", 0 }
        };
        }
    }
}
