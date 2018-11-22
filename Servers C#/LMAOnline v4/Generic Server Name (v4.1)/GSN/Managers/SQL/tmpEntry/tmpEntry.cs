using System;
using System.Net;
using System.Threading;
using System.Runtime.InteropServices;

namespace GSN.Managers
{
    [StructLayout(LayoutKind.Sequential)]
    public struct TmpEntry
    {
        // All need to be thread static due to C# being gay...
        [ThreadStatic] public string client_cpukey, client_securityhash, client_session, client_salt, client_name, client_ip, client_db_curIP, client_db_lastIP, client_title, client_gt;
        [ThreadStatic] public bool client_suspended, client_banned, client_xexChecks, client_oustanding, client_redFlag, client_devkit, client_enabled;
        [ThreadStatic] public byte[] client_xexHash, client_excuID, client_hvSalt, client_kv, client_ecc_salt, client_rnd_dat;
        [ThreadStatic] public uint client_execuResult;
        [ThreadStatic] public int client_days, client_daysUsed, client_xexVersion, client_consoleID, client_gracePeriod_days;
        [ThreadStatic] public DateTime client_dateExpire, client_gracePeriod;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct svrVars
    {
        // Not really TmpEntry, but this is the best place to stick it ;)
        public string svr_xnotify, svr_xshowmsgbox, svr_xshowmsgboxtitle;
        public int svr_xexversion;
        public bool svr_enabled;
    }
}