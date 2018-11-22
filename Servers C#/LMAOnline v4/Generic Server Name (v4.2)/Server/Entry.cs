using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GSN.Server
{
    public class Tmp
    {
        public static List<Entry> CachedEntry = null;
        public struct Entry
        {
            public string CPUKey, SecHash, Session, Salt, Name, IP, Title, GamerTag;
            public bool Suspended, Banned, Checks, Outstanding, RedFlag, DevKit, Enabled, Lifetime, AutoIncrement;
            public int Days, Used, Revision, ID, GP_Days;
            public DateTime Expire, GracePeriod;
            public byte[] ClientHash;
            // Notifications
            public bool noto_DayStarted, noto_AskDayStart;
        };
    }

    public class ModEntry
    {
        public static bool Exists(Tmp.Entry Client)
        {
            return Tmp.CachedEntry.Exists(i => i.CPUKey == Client.CPUKey);
        }        

        public static bool CacheEntry(Tmp.Entry Client)
        {
            if (!Exists(Client)) {
                Tmp.CachedEntry.Add(Client);
                return true;
            } return false;
        }

        public static bool CacheModify(Tmp.Entry Client)
        {
            if (Exists(Client)) {
                int Found = Tmp.CachedEntry.FindIndex(i => i.CPUKey == Client.CPUKey);
                Tmp.CachedEntry[Found] = Client;
                return true;
            } return false;
        }
    }
}
