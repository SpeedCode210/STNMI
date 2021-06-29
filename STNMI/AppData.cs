using System.Reflection;

namespace STNMI
{
    public static class AppData
    {
        public static string GetAppVersion()
        {
            string Version = Assembly.GetExecutingAssembly().GetName().Version.ToString();
            string[] version = Version.Split(".");
            string name = "Stable";
            if (version[0] == "0" || Properties.Settings.Default.IsBeta)
            {
                name = "Beta";
                if (version[1] == "0")
                {
                    name = "Alpha";
                }
            }
            return string.Format("{0} {1}.{2}.{3}", name, version[0], version[1], version[2]);
        }
    }
}
