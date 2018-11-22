// Default includes
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
// System Includes
using System.IO;
using System.Threading;
using System.Diagnostics;
using System.Windows.Forms;
using System.Globalization;
using System.Threading.Tasks;
using System.Security.Cryptography;
using System.Text.RegularExpressions;
// Project Includes
using GSN.ini;
using GSN.SQL;
// Other
using MySql.Data.MySqlClient;

namespace GSN.Globals
{
    public class GlobalFunc
    {
        #region INI Read/Create
        public static void iniReadHandle() 
        {
            try {
                if (File.Exists(GlobalVar.s_iniLocation)) {
                    GlobalVar.b_usingINI = true;
                    INI ini = new INI(GlobalVar.s_iniLocation);
                    GlobalVar.b_overRideChecks = Convert.ToBoolean(ini.IniReadValue("Server", "Override XEX Checks"));
                    GlobalVar.b_getFileByWild = Convert.ToBoolean(ini.IniReadValue("XEX", "Get File By Wildcard (*.xex)"));
                    GlobalVar.s_xexName = Convert.ToString(ini.IniReadValue("XEX", "Specify XEX Name(Useless when wildcard is enabled.)"));
                    GlobalVar.i_svrPort = Convert.ToInt32(ini.IniReadValue("Network", "Port"));
                    GlobalVar.b_parseLogs = Convert.ToBoolean(ini.IniReadValue("Logging", "Parse Logs"));
                    GlobalVar.b_logConnections = Convert.ToBoolean(ini.IniReadValue("Logging", "Log Connection"));
                    GlobalVar.b_printCrash = Convert.ToBoolean(ini.IniReadValue("Logging", "Log Client Crash"));
                    GlobalVar.s_svrAddr = Convert.ToString(ini.IniReadValue("MySQL", "Server Address"));
                    GlobalVar.s_dbName = Convert.ToString(ini.IniReadValue("MySQL", "Database Name"));
                    GlobalVar.s_dbUser = Convert.ToString(ini.IniReadValue("MySQL", "Database Username"));
                    GlobalVar.s_dbPass = Convert.ToString(ini.IniReadValue("MySQL", "Database Password"));
                } else { GlobalVar.b_usingINI = false; createINI(); }
            } catch { }
        }

        public static void createINI()
        {
            INI ini = new INI(GlobalVar.s_iniLocation);
            if (File.Exists(GlobalVar.s_iniLocation)) File.Delete(GlobalVar.s_iniLocation);
            ini.IniWriteValue("Server", "Override XEX Checks", GlobalVar.b_overRideChecks.ToString());
            ini.IniWriteValue("Network", "Port", GlobalVar.i_svrPort.ToString());
            ini.IniWriteValue("XEX", "Get File By Wildcard (*.xex)", GlobalVar.b_getFileByWild.ToString());
            ini.IniWriteValue("XEX", "Specify XEX Name(Useless when wildcard is enabled.)", GlobalVar.s_xexName);
            ini.IniWriteValue("Logging", "Parse Logs", GlobalVar.b_parseLogs.ToString());
            ini.IniWriteValue("Logging", "Log Connection", GlobalVar.b_logConnections.ToString());
            ini.IniWriteValue("Logging", "Log Client Crash", GlobalVar.b_printCrash.ToString());
            ini.IniWriteValue("Logging", "Log Errors/Warnings etc", GlobalVar.b_printErrors.ToString());
            ini.IniWriteValue("MySQL", "Server Address", GlobalVar.s_svrAddr);
            ini.IniWriteValue("MySQL", "Database Name", GlobalVar.s_dbName);
            ini.IniWriteValue("MySQL", "Database Username", GlobalVar.s_dbUser);
            ini.IniWriteValue("MySQL", "Database Password", GlobalVar.s_dbPass);
        }
        #endregion

        #region Write to Console
        public static void Parse(string path, string text, params object[] args)
        {
            try { using (StreamWriter sw = new StreamWriter(path, true)) { sw.WriteLine(text, args); } }
            catch (Exception e) { Console.WriteLine(" There was an issue writing to \"{0}\": {1}", path, e.Message); }
        }

        public static void Write(string text, params object[] args)
        {
            if (GlobalVar.b_logConnections) Console.WriteLine(DateTime.Now.ToString(" [M/d/yyyy h:mm:sstt]: ") + text, args);
            if (GlobalVar.b_parseLogs) Parse("logs\\Server.log", DateTime.Now.ToString(" [M/d/yyyy h:mm:sstt]: ") + text, args);
        }

        public static void WriteError(ConsoleColor color, string text, string ColoredText, params object[] args)
        {
            if (GlobalVar.b_logConnections && GlobalVar.b_printErrors)
            {
                Console.Write(DateTime.Now.ToString(" [M/d/yyyy h:mm:sstt]: ") + text);
                Console.ForegroundColor = color;
                Console.Write(ColoredText, args);
                Console.ForegroundColor = ConsoleColor.Gray;
            }

            if (GlobalVar.b_parseLogs) Parse("logs\\Server.log", String.Format("{0}{1} {2}", DateTime.Now.ToString(" [M/d/yyyy h:mm:sstt]: "), text, ColoredText), args);
        }

        public static void DelayedRestart(int delaySec, bool shutdown = false)
        {
            GlobalVar.b_serverRunning = false;
            Write("{0} GSN v1.0.0.2 in {1} second{2}...", shutdown ? "Shutting down" : "Restarting", delaySec, (delaySec > 1 ? "s" : ""));
            Thread.Sleep(delaySec * 1000);
            if (shutdown) Environment.Exit(1);
            else Application.Restart();
        }
        #endregion
    }

    public class CheckFunc
    {
        public static void checkfolders()
        {
            if (!Directory.Exists("bin") || !Directory.Exists("logs") || !Directory.Exists("dmp") || !Directory.Exists("ini") || !Directory.Exists("xex"))
            { Directory.CreateDirectory("bin"); Directory.CreateDirectory("logs"); Directory.CreateDirectory("dmp"); Directory.CreateDirectory("ini"); Directory.CreateDirectory("xex"); }
        }

        public static bool LoadBinFiles()
        {
            try {
                if (File.Exists("bin\\HV.bin") && File.Exists("bin\\chall_resp.bin") && File.Exists("bin\\XAM_PATCHES_RETAIL.bin")) {
                    GlobalVar.by_hvBytes = File.ReadAllBytes("bin\\HV.bin");
                    GlobalVar.by_chalBytes = File.ReadAllBytes("bin\\chall_resp.bin");
                    GlobalVar.by_xamBytes = File.ReadAllBytes("bin\\XAM_PATCHES_RETAIL.bin");
                    return true;
                } return false;
            } catch { return false; }
        }

        public static string getFileByWildcard(string directory, string fileExtention)
        {
            try {
                DirectoryInfo dirinf = new DirectoryInfo(directory);
                return directory + dirinf.GetFiles(fileExtention)[0].ToString();
            } catch { return ""; }
        }

        public static void loadXeX()
        {
            try {
                if (File.Exists(getFileByWildcard("xex\\", "*.xex")))
                { GlobalVar.by_xexBytes = File.ReadAllBytes(getFileByWildcard("xex\\", "*.xex")); GlobalVar.b_xexChecks = (GlobalVar.b_overRideChecks ? false : true); }
                else GlobalVar.b_xexChecks = false;
            } catch { GlobalVar.b_xexChecks = false; }
        }

        public static bool CheckMySQL()
        {
            Handle SQL = new Handle();
            using (MySqlConnection con = SQL.iniHandle())
                return SQL.Open(con);
        }
    }

    public class CPUKeyCheck
    {
        // Written by SWiZZY
        private static CPUKeyCheckErrors _lastError;

        private static bool CompareByteArrays(IList<byte> a1, IList<byte> a2)
        {
            if (a1 == a2)
                return true;
            if (a1 == null || a2 == null || a1.Count != a2.Count)
                return false;
            for (var i = 0; i < a1.Count; i++)
                if (a1[i] != a2[i])
                    return false;
            return true;
        }

        private enum CPUKeyCheckErrors
        {
            Success,
            TooShort,
            TooLong,
            BadKeyData1,
            BadKeyData2,
            BadHamming,
            BadECD,
            Unkown = int.MaxValue
        }

        internal static string GetLastError()
        {
            switch (_lastError)
            {
                case CPUKeyCheckErrors.Success:
                    return "No Error";
                case CPUKeyCheckErrors.TooShort:
                    return "Key is too short!";
                case CPUKeyCheckErrors.TooLong:
                    return "Key is too long!";
                case CPUKeyCheckErrors.BadKeyData1:
                case CPUKeyCheckErrors.BadKeyData2:
                    return "Key contains invalid characters!";
                case CPUKeyCheckErrors.BadHamming:
                    return "Key don't have 53 bits set (for the UUID part)!";
                case CPUKeyCheckErrors.BadECD:
                    return "Key ECD doesn't match";
                default:
                    return "Undefined error";
            }
        }

        static uint CountBits(UInt64 n)
        {
            uint c;
            for (c = 0; n > 0; c++)
                n &= n - 1;
            return c;
        }

        static bool CalcCPUKeyECD(ref byte[] key)
        {
            if (key.Length != 0x10)
                return false;
            uint acc1 = 0, acc2 = 0;
            for (int cnt = 0; cnt < 0x80; cnt++, acc1 >>= 1)
            {
                var bTmp = key[cnt >> 3];
                var dwTmp = (uint)((bTmp >> (cnt & 7)) & 1);
                if (cnt < 0x6A)
                {
                    acc1 = dwTmp ^ acc1;
                    if ((acc1 & 1) > 0)
                        acc1 = acc1 ^ 0x360325;
                    acc2 = dwTmp ^ acc2;
                }
                else if (cnt < 0x7F)
                {
                    if (dwTmp != (acc1 & 1))
                        key[(cnt >> 3)] = (byte)((1 << (cnt & 7)) ^ (bTmp & 0xFF));
                    acc2 = (acc1 & 1) ^ acc2;
                }
                else if (dwTmp != acc2)
                    key[0xF] = (byte)((0x80 ^ bTmp) & 0xFF);
            }
            return true;
        }

        private static byte[] Keytoarray(string key)
        {
            if (string.IsNullOrEmpty(key))
                return null;
            var ret = new byte[key.Length / 2];
            for (var i = 0; i < key.Length; i += 2)
                ret[i / 2] = byte.Parse(key.Substring(i, 2), NumberStyles.HexNumber);
            return ret;
        }

        internal static bool VerifyKey(string key)
        {
            _lastError = CPUKeyCheckErrors.Success;
            if (key.Length < 32) {
                _lastError = CPUKeyCheckErrors.TooShort;
                return false;
            }
            if (key.Length > 32) {
                _lastError = CPUKeyCheckErrors.TooLong;
                return false;
            }
            UInt64 tmp1, tmp2;
            if (!UInt64.TryParse(key.Substring(0, 16), NumberStyles.AllowHexSpecifier, null, out tmp1)) {
                _lastError = CPUKeyCheckErrors.BadKeyData1;
                return false;
            }
            if (!UInt64.TryParse(key.Substring(16), NumberStyles.AllowHexSpecifier, null, out tmp2)) {
                _lastError = CPUKeyCheckErrors.BadKeyData2;
                return false;
            }
            var hamming = CountBits(tmp1);
            hamming += CountBits(tmp2 & 0xFFFFFFFFFF030000);
            if (hamming != 53) {
                _lastError = CPUKeyCheckErrors.BadHamming;
                return false;
            }
            var keydata = Keytoarray(key);
            var keytmp = new byte[keydata.Length];
            Buffer.BlockCopy(keydata, 0, keytmp, 0, keydata.Length);
            if (!CalcCPUKeyECD(ref keytmp)) {
                _lastError = CPUKeyCheckErrors.Unkown;
                return false;
            }
            if (!CompareByteArrays(keydata, keytmp)) {
                _lastError = CPUKeyCheckErrors.BadECD;
                return false;
            } return true;
        }

        private static bool IsHexString(string input)
        {
            return Regex.IsMatch(input, "^[0-9A-Fa-f]+$");
        }

        private static bool IsValidKey(string key)
        {
            if (key != null)
            {
                key = key.Trim();
                if (key.Length == 0x20)
                {
                    if (IsHexString(key))
                        return true;
                }
            }
            return false;
        }
    }

    public static class BitsNBytes
    {
        public static byte[] RandomBytes(int len)
        {
            Random rnd = new Random();
            byte[] buffer = new byte[len];
            rnd.NextBytes(buffer);
            return buffer;
        }

        public static bool ByteCompare(byte[] a, byte[] b)
        {
            if (a.Length != b.Length) return false;
            for (int i = 0; i < a.Length; i++)
                if (a[i] != b[i])
                    return false;
            return true;
        }

        public static byte[] FillBlock(byte Single, int Length)
        {
            if (Length < 1) return null;
            byte[] Block = new byte[Length];
            for (int i = 0; i < Length; i++)
                Block[i] = Single;
            return Block;
        }
    }

    public static class ConvertEx
    {
        public static string ToHex(this byte[] Input)
        {
            string str = "";
            for (int i = 0; i < Input.Length; i++)
                str = str + Input[i].ToString("X2");
            return str;
        }

        public static string ToHex(this string ASCIIString)
        {
            string hex = "";
            foreach (char c in ASCIIString) {
                int tmp = c;
                hex += String.Format("{0:x2}", (uint)System.Convert.ToUInt32(tmp.ToString()));
            } return hex;
        }

        public static string ToString(this byte[] Input)
        {
            string str = "";
            for (int i = 0; i < Input.Length; i++) {
                str += Convert.ToChar(Convert.ToUInt32(Input[i].ToString("X2"), 16)).ToString();
            } return str;
        }

        public static byte[] ToByte(this string HexString)
        {
            byte[] buffer = new byte[HexString.Length / 2];
            for (int i = 0; i < HexString.Length; i += 2) {
                try { buffer[i / 2] = Convert.ToByte(HexString.Substring(i, 2), 0x10); }
                catch { buffer[i / 2] = 0; }
            } return buffer;
        }
    }
}
