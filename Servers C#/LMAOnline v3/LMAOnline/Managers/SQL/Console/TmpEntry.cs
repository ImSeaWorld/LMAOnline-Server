using System;
using System.Net;
using System.Threading;
using System.Runtime.InteropServices;

namespace LMAOnline.Managers
{
    [StructLayout(LayoutKind.Sequential)]
    public struct TmpEntry
    {
        [ThreadStatic]
        public string
            CPUKey, ClientSession, ClientSalt, ClientIP, ClientTitle, ClientGT, ClientName, inToken, outToken;
        [ThreadStatic]
        public bool
            ClientEnabled, ClientBanned, ClientBypass, ClientConType, ClientXeXChecks, TokenUsed;
        [ThreadStatic]
        public byte[] KVDat, xexHash, ClientExecutionID, ClientHVSalt;
        [ThreadStatic]
        public uint ClientExecutionResult;
        [ThreadStatic]
        public int daysLeft, TokenDays, TokenID;
        [ThreadStatic]
        public DateTime ClientTime;
    }
}