using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

//using Microsoft.TeamFoundation.Common;
using System.Security.Cryptography;
using MySql.Data.MySqlClient;
using LMAOnline.Managers;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using System.Diagnostics;
using System.Threading;
using Microsoft.Win32;
using System.IO;

namespace LMAOnline
{
    public static class Globals
    {
        public static bool
            printClientCrash = true,
            logConnections = true,
            parseLogs = true,
            xexChecks = true,
            ORXeXChecks = false,
            usingINI = false,
            MySQLCon = true, // Only used once but fuck it.
            allowAnonUsers = false,
            serverRunning = false,
            addtoStartup = true;
        public static string
            iniLocation = "ini\\LMAOnline.ini",
            svrAddr = "167.114.100.102",
            DBName = "seaworld_xblrogers",
            DBUser = "seaworld_main",
            DBPass = "LF$.JXW4s#vO";
        public static int svrPort = 6349;
        public static byte[]
            XEXBytes = null,
            HVBytes = null,
            ChalBytes = null,
            HVCBytes = null;

        public const uint
            XSTL_SERVER_COMMAND_ID_GET_SALT = 0x00000001,
            XSTL_SERVER_COMMAND_ID_GET_STATUS = 0x00000002,
            XSTL_SERVER_COMMAND_ID_GET_CHAL_RESPONCE = 0x00000003,
            XSTL_SERVER_COMMAND_ID_UPDATE_PRESENCE = 0x00000004,
            XSTL_SERVER_COMMAND_ID_GET_XOSC = 0x00000005,
            XSTL_SERVER_COMMAND_ID_GET_INFO = 0x00000006,
            XSTL_SERVER_COMMAND_ID_GET_PATCHES = 0x00000007,
            XSTL_SERVER_COMMAND_ID_GET_MESSAGE = 0x00000008,
            XSTL_SERVER_COMMAND_ID_REDEEM_TOKEN = 0x00000009,
            XSTL_SERVER_COMMAND_ID_LOL_SPOOFY = 0x1010101F,
            // Status Codes
            XSTL_STATUS_SUCCESS = 0x40000000,
            XSTL_STATUS_REBOOT = 0x60000000,
            XSTL_STATUS_UPDATE = 0x80000000,
            XSTL_STATUS_EXPIRED = 0x90000000,
            XSTL_STATUS_XNOTIFYMSG = 0xA0000000,
            XSTL_STATUS_MESSAGEBOX = 0xB0000000,
            XSTL_STATUS_ERROR = 0xC0000000,
            XSTL_STATUS_OTHER_ERROR = 0xD0000000,
            XSTL_STATUS_STEALTHED = 0xF0000000,
            XSTL_STATUS_XBLFREE = 0x3FFF098F,
            XSTL_STATUS_BANNED = 0xFFFFFFFF,
            XSTL_STATUS_DAY_STARTED = 0xFDA20000,
            XSTL_STATUS_ERR_SESSION = 0xF0F0F0F0;

        static Globals() { }

        public static void iniReadHandle()
        {
            try {
                if (File.Exists(iniLocation)) {
                    usingINI = true;
                    IniFile ini = new IniFile(iniLocation);
                    ORXeXChecks = Convert.ToBoolean(ini.IniReadValue("Server", "Override XEX Checks"));
                    addtoStartup = Convert.ToBoolean(ini.IniReadValue("Server", "Add to Startup"));
                    allowAnonUsers = Convert.ToBoolean(ini.IniReadValue("Server", "Allow Anonymous Users"));
                    svrPort = Convert.ToInt32(ini.IniReadValue("Network", "Port"));
                    parseLogs = Convert.ToBoolean(ini.IniReadValue("Logging", "Parse Logs"));
                    logConnections = Convert.ToBoolean(ini.IniReadValue("Logging", "Log Connection"));
                    printClientCrash = Convert.ToBoolean(ini.IniReadValue("Logging", "Log Client Crash"));
                    svrAddr = Convert.ToString(ini.IniReadValue("MySQL", "Server Address"));
                    DBName = Convert.ToString(ini.IniReadValue("MySQL", "Database Name"));
                    DBUser = Convert.ToString(ini.IniReadValue("MySQL", "Database Username"));
                    DBPass = Convert.ToString(ini.IniReadValue("MySQL", "Database Password"));
                } else { usingINI = false; createINI(); }
            } catch { }
        }

        public static void createINI()
        {
            IniFile ini = new IniFile(iniLocation);
            if (File.Exists(iniLocation)) {
                File.Delete(iniLocation);
                ini.IniWriteValue("Server", "Override XEX Checks", ORXeXChecks.ToString());
                ini.IniWriteValue("Server", "Add to Startup", addtoStartup.ToString());
                ini.IniWriteValue("Server", "Allow Anonymous Users", allowAnonUsers.ToString());
                ini.IniWriteValue("Network", "Port", svrPort.ToString());
                ini.IniWriteValue("Logging", "Parse Logs", parseLogs.ToString());
                ini.IniWriteValue("Logging", "Log Connection", logConnections.ToString());
                ini.IniWriteValue("Logging", "Log Client Crash", printClientCrash.ToString());
                ini.IniWriteValue("MySQL", "Server Address", svrAddr);
                ini.IniWriteValue("MySQL", "Database Name", DBName);
                ini.IniWriteValue("MySQL", "Database Username", DBUser);
                ini.IniWriteValue("MySQL", "Database Password", DBPass);
            } else {
                ini.IniWriteValue("Server", "Override XEX Checks", ORXeXChecks.ToString());
                ini.IniWriteValue("Server", "Add to Startup", addtoStartup.ToString());
                ini.IniWriteValue("Server", "Allow Anonymous Users", allowAnonUsers.ToString());
                ini.IniWriteValue("Network", "Port", svrPort.ToString());
                ini.IniWriteValue("Logging", "Parse Logs", parseLogs.ToString());
                ini.IniWriteValue("Logging", "Log Connection", logConnections.ToString());
                ini.IniWriteValue("Logging", "Log Client Crash", printClientCrash.ToString());
                ini.IniWriteValue("MySQL", "Server Address", svrAddr);
                ini.IniWriteValue("MySQL", "Database Name", DBName);
                ini.IniWriteValue("MySQL", "Database Username", DBUser);
                ini.IniWriteValue("MySQL", "Database Password", DBPass);
            }
        }        

        private static void Parse(string path, string txt, params object[] args)
        {
            try { using (StreamWriter sw = new StreamWriter(path, true)) { sw.WriteLine(txt, args); } }
            catch (Exception e) { Console.WriteLine(" There was an issue writing to {0}: {1}", path, e.Message); }
        }

        public static void write(string fuck, params object[] args)
        {
            if (logConnections) Console.WriteLine(DateTime.Now.ToString(" [M/d/yyyy h:mm:sstt]: ") + fuck, args);
            if (parseLogs) Parse("logs\\Server.log", DateTime.Now.ToString(" [M/d/yyyy h:mm:sstt]: ") + fuck, args);
        }

        public static void DelayedRestart(int delay, bool shutdown = false)
        { write("{0} LMAOnline in {1} seconds...", shutdown ? "Shutting down" : "Restarting", delay); serverRunning = false; Thread.Sleep(delay * 1000); if (!shutdown) { Application.Restart(); } Environment.Exit(1); }
        
        public static bool checkMySQL()
        {
            mysql sql = new mysql();
            using (MySqlConnection con = sql.iniHandle())
                return sql.open(con);
        }

        public static void checkfolders()
        {
            if (!Directory.Exists("bin") || !Directory.Exists("logs") || !Directory.Exists("dmp") || !Directory.Exists("ini") || !Directory.Exists("xex"))
            { Directory.CreateDirectory("bin"); Directory.CreateDirectory("logs"); Directory.CreateDirectory("dmp"); Directory.CreateDirectory("ini"); Directory.CreateDirectory("xex"); }
        }

        public static string getFileByWildcard(string directory, string fileExtention)
        {
            try {
                DirectoryInfo dirinf = new DirectoryInfo(directory);
                return directory + dirinf.GetFiles(fileExtention)[0].ToString();
            } catch { return ""; }
        }

        public static bool loadBinFiles()
        {
            try {
                if (File.Exists("bin\\HV.bin") && File.Exists("bin\\chall_resp.bin")) //&& File.Exists("bin\\HVC.bin"))
                { HVBytes = File.ReadAllBytes("bin\\HV.bin"); ChalBytes = File.ReadAllBytes("bin\\chall_resp.bin"); /*HVCBytes = File.ReadAllBytes("bin\\HVC.bin");*/ return true; }
                else { return false; }
            } catch { return false; }
        }

        public static void loadXeX()
        {
            try {
                if (File.Exists(getFileByWildcard("xex\\", "*.xex")))
                { XEXBytes = File.ReadAllBytes(getFileByWildcard("xex\\", "*.xex")); xexChecks = (ORXeXChecks ? false : true); }
                else xexChecks = false;
            } catch { xexChecks = false; }
        }
    }

    public static class GlobalMisc
    {
        // Startup shit
        private static RegistryKey regKey = Registry.CurrentUser.OpenSubKey(@"SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run", true);
        public static void toggleStartup(bool addRemove)
        {
            try {
                if (addRemove) regKey.SetValue("LMAOnline v3", Application.ExecutablePath);
                else regKey.DeleteValue("LMAOnline v3");
            } catch { }
        }
        public static bool checkStartup()
        {
            try { return (regKey.GetValue("LMAOnline v3") == null ? false : true); }
            catch { return false; }
        }

        // Other Shit
        public static string ConvertBytesToString(byte[] bytes)
        {
            string str = "";
            for (int i = 0; i < bytes.Length; i++)
            {
                str += Convert.ToChar(Convert.ToUInt32(bytes[i].ToString("X2"), 16)).ToString();
            }
            return str;
        }
    }
}