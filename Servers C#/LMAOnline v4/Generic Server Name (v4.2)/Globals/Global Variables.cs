 using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Net.Sockets;

namespace GSN.Globals
{
    public enum respCode : uint
    {
        RESP_BYPASS         = 0x20000000,
        RESP_SUCCESS        = 0x40000000,
        RESP_REBOOT         = 0x60000000,
        RESP_UPDATE         = 0x80000000,
        RESP_EXPIRED        = 0x90000000,
        RESP_XNOTIFYMSG     = 0xA0000000,
        RESP_MESSAGEBOX     = 0xB0000000,
        RESP_ERROR          = 0xC0000000,
        RESP_STEALTHED      = 0xF0000000,
        RESP_BANNED         = 0x0042414E,
        RESP_DAY_STARTED    = 0xFDA20000,
        RESP_ERR_SESSION    = 0x5E551000,
        RESP_SUSPENDED      = 0x1F0F1F0F
    };

    public enum cmdCode : uint
    {
        GET_CHAL_RESPONSE   = 0x4348414C,
        GET_GUIDE_INFO      = 0x52474944,
        GET_SESSION         = 0x48534553,
        GET_PATCHES         = 0x50544348,
        GET_MESSAGE         = 0x524D5347,
        GET_STATUS          = 0x53544154,
        GET_XOSC            = 0x43534F58,
        GET_INFO            = 0x494E464F,
        UPDATE_PRESENCE     = 0x50524E43,
        SND_SPOOFY          = 0x434F4F4E
    };

    public enum patchesCode : uint
    {
        STATUS_DISABLED     = 0x504D494C,
        STATUS_GOOD         = 0x4F4B4559,
        STATUS_UNKNOWN      = 0x4944464B,
        STATUS_EXPIRED      = 0x44454144,
        STATUS_BANNED       = 0xFEEBDAED
    };

    public enum presenceCode : uint
    {
        RUN_XEX             = 0x52584558,
        TGL_DRIVE           = 0x54445256,
        INC_MSG             = 0x494D5347
    };

    public static class GlobalVar
    {
        public static NetworkStream nStream;
        public static DateTime TimeStarted;
        public static bool // All bools needed.
            b_logConnections = true,
            b_printCrash = true,
            b_printErrors = true,
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
}
