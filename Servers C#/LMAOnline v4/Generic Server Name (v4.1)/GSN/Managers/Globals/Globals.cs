using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.IO;
using System.Threading;
using System.Diagnostics;
using System.Windows.Forms;
using System.Security.Cryptography;
using MySql.Data.MySqlClient;

namespace GSN.Managers
{
    public enum respCode : uint
    {
        RESP_STATUS_BYPASS = 0x20000000,
        RESP_STATUS_SUCCESS = 0x40000000,
        RESP_STATUS_REBOOT = 0x60000000,
        RESP_STATUS_UPDATE = 0x80000000,
        RESP_STATUS_EXPIRED = 0x90000000,
        RESP_STATUS_XNOTIFYMSG = 0xA0000000,
        RESP_STATUS_MESSAGEBOX = 0xB0000000,
        RESP_STATUS_ERROR = 0xC0000000,
        RESP_STATUS_STEALTHED = 0xF0000000,
        RESP_STATUS_BANNED = 0x0042414E,
        RESP_STATUS_DAY_STARTED = 0xFDA20000,
        RESP_STATUS_ERR_SESSION = 0x5E551000,
        RESP_STATUS_SUSPENDED = 0x1F0F1F0F
    };

    public enum cmdCode : uint
    {
        GET_SESSION = 0x48534553,
        GET_STATUS = 0x53544154,
        GET_CHAL_RESPONSE = 0x4348414C,
        UPDATE_PRESENCE = 0x50524E43,
        GET_XOSC = 0x43534F58,
        GET_INFO = 0x494E464F,
        GET_PATCHES = 0x50544348,
        GET_MESSAGE = 0x524D5347,
        SND_SPOOFY = 0x434F4F4E,
        GET_GUIDE_INFO = 0x52474944
    };

    public enum patchesCode : uint
    {
        STATUS_DISABLED = 0x504D494C,
        STATUS_GOOD = 0x4F4B4559,
        STATUS_UNKNOWN = 0x4944464B,
        STATUS_EXPIRED = 0x44454144,
        STATUS_BANNED = 0xFEEBDAED
    };

    public enum presenceCode : uint
    {
        RUN_XEX = 0x52584558,
        TGL_DRIVE = 0x54445256,
        INC_MSG = 0x494D5347
    };

    // Variables
    public static class Globals
    {
        public static DateTime TimeStarted;
        public static bool // All bools needed.
            b_logConnections = true,
            b_printCrash = true,
            b_parseLogs = true,
            b_xexChecks = true,
            b_overRideChecks = false,
            b_serverRunning = false,
            b_usingINI = false,
            b_getFileByWild = false,
            b_allowAnonymousUsers = false;

        public static string // All strings used
            // Other info we get from the DB
            s_xNotify = "",
            s_MOTD = "",
            // Other shit.
            s_xexName = "SeaWorld.xex",
            s_iniLocation = "ini\\LMAOnline.ini",
            // Static DB info
            s_svrAddr = "192.168.1.1",
            s_dbName = "SW",
            s_dbUser = "SW",
            s_dbPass = "SW";

        public static int i_svrPort = 1337;

        public static byte[] // Modules in they're OG form in bytes.
            by_xexBytes = null,
            by_hvBytes = null,
            by_hvcBytes = null,
            by_chalBytes = null,
            by_xamBytes = null,
            // Now for offsets
            by_gtavBytes = null,
            by_codBytes = null; // We'll find out which ones we need this for later
    }

    public static class GlobalFunc
    {
        static GlobalFunc() { }

        #region INI Read/Create
        public static void iniReadHandle()
        {
            try {
                if (File.Exists(Globals.s_iniLocation)) {
                    Globals.b_usingINI = true;
                    INIHandle ini = new INIHandle(Globals.s_iniLocation);
                    Globals.b_overRideChecks = Convert.ToBoolean(ini.IniReadValue("Server", "Override XEX Checks"));
                    Globals.b_getFileByWild = Convert.ToBoolean(ini.IniReadValue("XEX", "Get File By Wildcard (*.xex)"));
                    Globals.s_xexName = Convert.ToString(ini.IniReadValue("XEX", "Specify XEX Name(Useless when wildcard is enabled.)"));
                    Globals.i_svrPort = Convert.ToInt32(ini.IniReadValue("Network", "Port"));
                    Globals.b_parseLogs = Convert.ToBoolean(ini.IniReadValue("Logging", "Parse Logs"));
                    Globals.b_logConnections = Convert.ToBoolean(ini.IniReadValue("Logging", "Log Connection"));
                    Globals.b_printCrash = Convert.ToBoolean(ini.IniReadValue("Logging", "Log Client Crash"));
                    Globals.s_svrAddr = Convert.ToString(ini.IniReadValue("MySQL", "Server Address"));
                    Globals.s_dbName = Convert.ToString(ini.IniReadValue("MySQL", "Database Name"));
                    Globals.s_dbUser = Convert.ToString(ini.IniReadValue("MySQL", "Database Username"));
                    Globals.s_dbPass = Convert.ToString(ini.IniReadValue("MySQL", "Database Password"));
                } else { Globals.b_usingINI = false; createINI(); }
            } catch { }
        }

        public static void createINI()
        {
            INIHandle ini = new INIHandle(Globals.s_iniLocation);
            if (File.Exists(Globals.s_iniLocation)) File.Delete(Globals.s_iniLocation);
            ini.IniWriteValue("Server", "Override XEX Checks", Globals.b_overRideChecks.ToString());
            ini.IniWriteValue("Network", "Port", Globals.i_svrPort.ToString());
            ini.IniWriteValue("XEX", "Get File By Wildcard (*.xex)", Globals.b_getFileByWild.ToString());
            ini.IniWriteValue("XEX", "Specify XEX Name(Useless when wildcard is enabled.)", Globals.s_xexName);
            ini.IniWriteValue("Logging", "Parse Logs", Globals.b_parseLogs.ToString());
            ini.IniWriteValue("Logging", "Log Connection", Globals.b_logConnections.ToString());
            ini.IniWriteValue("Logging", "Log Client Crash", Globals.b_printCrash.ToString());
            ini.IniWriteValue("MySQL", "Server Address", Globals.s_svrAddr);
            ini.IniWriteValue("MySQL", "Database Name", Globals.s_dbName);
            ini.IniWriteValue("MySQL", "Database Username", Globals.s_dbUser);
            ini.IniWriteValue("MySQL", "Database Password", Globals.s_dbPass);
        }      
        #endregion

        public static void Parse(string path, string text, params object[] args) 
        {
            try { using (StreamWriter sw = new StreamWriter(path, true)) { sw.WriteLine(text, args); } }
            catch (Exception e) { Console.WriteLine(" There was an issue writing to \"{0}\": {1}", path, e.Message); }
        }

        public static void Write(string text, params object[] args)
        {
            if (Globals.b_logConnections) Console.WriteLine(DateTime.Now.ToString(" [M/d/yyyy h:mm:sstt]: ") + text, args);
            if (Globals.b_parseLogs) Parse("logs\\Server.log", DateTime.Now.ToString(" [M/d/yyyy h:mm:sstt]: ") + text, args);
        }

        public static void WriteError(ConsoleColor color, string text, string ColoredText, params object[] args)
        {
            if (Globals.b_logConnections)
            {
                Console.Write(DateTime.Now.ToString(" [M/d/yyyy h:mm:sstt]: ") + text);
                Console.ForegroundColor = color;
                Console.Write(ColoredText, args);
                Console.ForegroundColor = ConsoleColor.Gray;
            }

            if (Globals.b_parseLogs) Parse("logs\\Server.log", String.Format("{0}{1} {2}", DateTime.Now.ToString(" [M/d/yyyy h:mm:sstt]: "), text, ColoredText), args);
        }

        public static void DelayedRestart(int delaySec, bool shutdown = false)
        {
            Globals.b_serverRunning = false;
            Write("{0} LMAOnline v4 in {1} second(s)...", shutdown ? "Shutting down" : "Restarting", delaySec);
            Thread.Sleep(delaySec * 1000);
            if (shutdown) Environment.Exit(1);
            else Application.Restart();
        }

        public static void checkfolders()
        {
            if (!Directory.Exists("bin") || !Directory.Exists("logs") || !Directory.Exists("dmp") || !Directory.Exists("ini") || !Directory.Exists("xex"))
            { Directory.CreateDirectory("bin"); Directory.CreateDirectory("logs"); Directory.CreateDirectory("dmp"); Directory.CreateDirectory("ini"); Directory.CreateDirectory("xex"); }
        }

        public static bool CheckMySQL()
        {
            MySQLHandle SQL = new MySQLHandle();
            using (MySqlConnection con = SQL.iniHandle())
                return SQL.Open(con);
        }

        public static bool LoadBinFiles()
        {
            try {
                if (File.Exists("bin\\HV.bin") && File.Exists("bin\\chall_resp.bin") && File.Exists("bin\\XAM_PATCHES_RETAIL.bin")) {
                    Globals.by_hvBytes = File.ReadAllBytes("bin\\HV.bin"); 
                    Globals.by_chalBytes = File.ReadAllBytes("bin\\chall_resp.bin"); 
                    Globals.by_xamBytes = File.ReadAllBytes("bin\\XAM_PATCHES_RETAIL.bin"); 
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
                { Globals.by_xexBytes = File.ReadAllBytes(getFileByWildcard("xex\\", "*.xex")); Globals.b_xexChecks = (Globals.b_overRideChecks ? false : true); }
                else Globals.b_xexChecks = false;
            } catch { Globals.b_xexChecks = false; }
        }

        private static string cmdExec(string cmd)
        {
            var process = new Process()
            {
                StartInfo = new ProcessStartInfo("cmd")
                {
                    UseShellExecute = false,
                    RedirectStandardInput = true,
                    RedirectStandardOutput = true,
                    CreateNoWindow = true,
                    Arguments = string.Format("/c \"{0}\"", cmd)
                }
            };
            process.Start();
            return process.StandardOutput.ReadToEnd();
        }

        public static bool AllowAppThroughFirewall()
        {
            Random rnd = new Random();
            if (cmdExec(String.Format("netsh advfirewall firewall add rule name=\"{1}\" dir=in action=allow program=\"{0}\" enable=yes", System.Reflection.Assembly.GetExecutingAssembly().Location, "LMAOnline - " + rnd.Next(0, 9999))) == "Ok.") {
                return true;
            } return false;
        }

        public static string ConvertBytesToString(byte[] bytes)
        {
            string str = "";
            for (int i = 0; i < bytes.Length; i++) {
                str += Convert.ToChar(Convert.ToUInt32(bytes[i].ToString("X2"), 16)).ToString();
            } return str;
        }
    }
}
