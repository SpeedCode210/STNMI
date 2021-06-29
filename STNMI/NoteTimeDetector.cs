using System;
using System.Diagnostics;

namespace STNMI
{
    public static class NoteTimeDetector
    {
        public static int Detect(long avant, long apres)
        {
            long milliseconds = apres - avant;
            float ms = milliseconds;
            float result = ((float)ScoreData.tempo) * ms / 60000f;
            if (result > 2.7f)
                return (int)Math.Round(result * 4);
            else if (result > 2.25f)
                return 10;
            else if (result > 1.75f)
                return 8;
            else if (result > 1.25f)
                return 6;
            else if (result > 0.8f)
                return 4;
            else if (result > 0.6f)
                return 3;
            else if (result > 0.4f)
                return 2;
            else if (result > 0.2f)
                return 1;
            else
                return 0;
        }
    }
}
