namespace STNMI
{
    public static class Gammes
    {
        public static Gamme[] gammes = new Gamme[]
{
            new("Do majeur/La mineur", "C")
            {
                Notes = new()
                {
                    {"a","C" },
                    {"b","^C" },
                    {"c","D" },
                    {"d","_E" },
                    {"e","E" },
                    {"f","F" },
                    {"g","^F" },
                    {"h","G" },
                    {"i","^G" },
                    {"j","A" },
                    {"k","_B" },
                    {"l","B" }
                }
            },
            //Gammes bémolisées
            new("Fa majeur/Ré mineur", "F")
            {
                Notes = new()
                {
                    {"a","C" },
                    {"b","^C" },
                    {"c","D" },
                    {"d","_E" },
                    {"e","E" },
                    {"f","F" },
                    {"g","^F" },
                    {"h","G" },
                    {"i","_A" },
                    {"j","A" },
                    {"k","B" },
                    {"l","=B" }
                }
            },
            new("Sib majeur/Sol mineur", "Gm")
            {
                Notes = new()
                {
                    {"a","C" },
                    {"b","^C" },
                    {"c","D" },
                    {"d","E" },
                    {"e","=E" },
                    {"f","F" },
                    {"g","^F" },
                    {"h","G" },
                    {"i","_A" },
                    {"j","A" },
                    {"k","B" },
                    {"l","=B" }
                }
            },
            new("Mib majeur/Do mineur", "Cm")
            {
                Notes = new()
                {
                    {"a","C" },
                    {"b","^C" },
                    {"c","D" },
                    {"d","E" },
                    {"e","=E" },
                    {"f","F" },
                    {"g","^F" },
                    {"h","G" },
                    {"i","A" },
                    {"j","=A" },
                    {"k","B" },
                    {"l","=B" }
                }
            },
            new("Lab majeur/Fa mineur", "Fm")
            {
                Notes = new()
                {
                    {"a","C" },
                    {"b","D" },
                    {"c","=D" },
                    {"d","E" },
                    {"e","=E" },
                    {"f","F" },
                    {"g","^F" },
                    {"h","G" },
                    {"i","A" },
                    {"j","=A" },
                    {"k","B" },
                    {"l","=B" }
                }
            },
            new("Réb majeur/Sib mineur", "Db")
            {
                Notes = new()
                {
                    {"a","C" },
                    {"b","D" },
                    {"c","=D" },
                    {"d","E" },
                    {"e","=E" },
                    {"f","F" },
                    {"g","_G" },
                    {"h","=G" },
                    {"i","A" },
                    {"j","=A" },
                    {"k","B" },
                    {"l","=B" }
                }
            },
            new("Solb majeur/Mib mineur", "Gb")
            {
                Notes = new()
                {
                    {"a","=C" },
                    {"b","D" },
                    {"c","=D" },
                    {"d","E" },
                    {"e","=E" },
                    {"f","F" },
                    {"g","_G" },
                    {"h","=G" },
                    {"i","A" },
                    {"j","=A" },
                    {"k","B" },
                    {"l","C" }
                }
            },
            new("Dob majeur/Lab mineur", "Cb")
            {
                Notes = new()
                {
                    {"a","=C" },
                    {"b","D" },
                    {"c","=D" },
                    {"d","E" },
                    {"e","F" },
                    {"f","=F" },
                    {"g","_G" },
                    {"h","=G" },
                    {"i","A" },
                    {"j","=A" },
                    {"k","B" },
                    {"l","C" }
                }
            },
            //Gammes diésées
            new("Sol majeur/Mi mineur", "G")
            {
                Notes = new()
                {
                    {"a","C" },
                    {"b","^C" },
                    {"c","D" },
                    {"d","^D" },
                    {"e","E" },
                    {"f","=F" },
                    {"g","F" },
                    {"h","G" },
                    {"i","^G" },
                    {"j","A" },
                    {"k","_B" },
                    {"l","B" }
                }
            },
            new("Ré majeur/Si mineur", "D")
            {
                Notes = new()
                {
                    {"a","=C" },
                    {"b","C" },
                    {"c","D" },
                    {"d","^D" },
                    {"e","E" },
                    {"f","=F" },
                    {"g","F" },
                    {"h","G" },
                    {"i","^G" },
                    {"j","A" },
                    {"k","^A" },
                    {"l","B" }
                }
            },
            new("La majeur/Fa mineur", "A")
            {
                Notes = new()
                {
                    {"a","=C" },
                    {"b","C" },
                    {"c","D" },
                    {"d","^D" },
                    {"e","E" },
                    {"f","^E" },
                    {"g","F" },
                    {"h","=G" },
                    {"i","G" },
                    {"j","A" },
                    {"k","^A" },
                    {"l","B" }
                }
            },
            new("Mi majeur/Do# mineur", "E")
            {
                Notes = new()
                {
                    {"a","^B" },
                    {"b","C" },
                    {"c","=D" },
                    {"d","D" },
                    {"e","E" },
                    {"f","=F" },
                    {"g","F" },
                    {"h","=G" },
                    {"i","G" },
                    {"j","A" },
                    {"k","^A" },
                    {"l","B" }
                }
            },
            new("Si majeur/Sol# mineur", "B")
            {
                Notes = new()
                {
                    {"a","=C" },
                    {"b","C" },
                    {"c","=D" },
                    {"d","D" },
                    {"e","E" },
                    {"f","=F" },
                    {"g","F" },
                    {"h","^^F" },
                    {"i","G" },
                    {"j","=A" },
                    {"k","A" },
                    {"l","B" }
                }
            },
            new("Fa# majeur/Ré# mineur", "F#")
            {
                Notes = new()
                {
                    {"a","=C" },
                    {"b","C" },
                    {"c","^^C" },
                    {"d","D" },
                    {"e","=E" },
                    {"f","E" },
                    {"g","F" },
                    {"h","=G" },
                    {"i","G" },
                    {"j","=A" },
                    {"k","A" },
                    {"l","B" }
                }
            },
            new("Do# majeur/La# mineur", "C#")
            {
                Notes = new()
                {
                    {"a","B" },
                    {"b","C" },
                    {"c","=D" },
                    {"d","D" },
                    {"e","=E" },
                    {"f","E" },
                    {"g","F" },
                    {"h","=G" },
                    {"i","G" },
                    {"j","^^G" },
                    {"k","A" },
                    {"l","=B" }
                }
            }
        };
    }
}
