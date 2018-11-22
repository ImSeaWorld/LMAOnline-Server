namespace Stolen
{
    using System;
    using System.Collections.Generic;

    public static class Globals
    {
        private static bool isDebug = false;
        public static byte[] xexBytes = null;
        public static string XeXName = "XBLStealth.xex";
        public static Dictionary<string, string> ClientMessage1 = new Dictionary<string, string>();//Messagebox
        public static Dictionary<string, string> ClientMessage2 = new Dictionary<string, string>();//Xnotify
        static Globals()
        {
            isDebug = true;
        }

        public static bool IsDebug
        {
            get
            {
                return isDebug;
            }

        }
    }
}

