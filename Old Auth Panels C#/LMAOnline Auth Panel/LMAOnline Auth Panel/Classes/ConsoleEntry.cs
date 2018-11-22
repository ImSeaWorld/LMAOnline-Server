using System;
using System.Net;
using System.Runtime.InteropServices;

namespace LMAOnline.Classes
{
    [StructLayout(LayoutKind.Sequential)]
    public struct ConsoleEntry
    {
        public string cpukey;
        public string reason;
        public string salt;
        public IPAddress ip;
        public string sip;
        public string gamertag;
        public string titleid;
        public bool enabled;
        public bool haskv;
        public byte[] kvdata;
        public string name;
        public string email;
        public int expireTime;
        public string remainingTime;
        public string last_seen;
    }
}

