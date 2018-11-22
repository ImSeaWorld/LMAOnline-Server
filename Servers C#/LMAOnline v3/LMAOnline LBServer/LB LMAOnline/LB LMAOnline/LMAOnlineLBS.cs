using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using LB_LMAOnline.Managers;
using LB_LMAOnline.Forms;

namespace LB_LMAOnline
{
    class LMAOnlineLBS
    {
        private static TcpClient client;

        private const uint
            XSTL_STATUS_SUCCESS = 0x40000000,
            XSTL_STATUS_UPDATE = 0x80000000,
            XSTL_STATUS_EXPIRED = 0x90000000,
            XSTL_STATUS_XNOTIFYMSG = 0xA0000000,
            XSTL_STATUS_MESSAGEBOX = 0xB0000000,
            XSTL_STATUS_ERROR = 0xC0000000,
            XSTL_STATUS_STEALTHED = 0xF0000000;

        [STAThread]
        static void Main(string[] args)
        {
            Console.Title = "LB Server - LMAOnline v3";
            Console.BackgroundColor = ConsoleColor.Black;
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine(
                "\n\n    +=============================================================+\n" +
                "    |/////////////////////////////////////////////////////////////|\n" +
                "    |/////////// LBS For LMAOnline V3 Has Been Started ///////////|\n" +
                "    |///////////////////// This Shit's Funny /////////////////////|\n" +
                "    |/////////////////////////////////////////////////////////////|\n" + 
                "    +=============================================================+\n" +
                "    |                        Quick Commands                       |\n" +
                "    +=============================================================+\n" +
                "    |            Submit INI = editINI    Param[(str)loc]          |\n" +
                "    |         Quick Restart = qrestart   Param[(int)sec]          |\n" +
                "    |       Test Connection = test                                |\n" +
                "    |       Shutdown Server = shutdown                            |\n" +
                "    |       INI Editor(GUI) = nano                                |\n" +
                "    |              Get Logs = rlogs                               |\n" +
                "    |               Get INI = rini                                |\n" +
                "    +=============================================================+\n\n");

            Again:
            Console.Write("\n      Command: ");
            string command = Console.ReadLine();

            try
            {
                client = new TcpClient(Globals.IPAddr, Globals.SvrPort);
                NetworkStream nStream = client.GetStream();
                EndianIO readerIO = new EndianIO(nStream, EndianStyle.BigEndian) { Writer = new EndianWriter(nStream, EndianStyle.BigEndian) };
                EndianIO writerIO = new EndianIO(nStream, EndianStyle.BigEndian);
                Thread.Sleep(1000);
                writerIO.Writer.Write("2F9A59018B92AA2C3B375D0D66CF8255");
                switch (command.Split(' ')[0])
                {
                    case "qrestart":
                        if (!(command.Split(' ').Length > 1))
                        { Write("Invalid secondary param! \"Param[(int)sec]\""); goto Again; }
                        writerIO.Writer.Write(command);
                        Write("Resp Back: {0}", readerIO.Reader.ReadUInt32() == XSTL_STATUS_SUCCESS ? "OK" : "ERR");
                        break;
                    case "shutdown":
                        writerIO.Writer.Write(command);
                        Write("Resp Back: {0}", readerIO.Reader.ReadUInt32() == XSTL_STATUS_SUCCESS ? "OK" : "ERR");
                        break;
                    case "rlogs":
                        writerIO.Writer.Write(command);
                        receiveLogs(readerIO, writerIO);
                        break;
                    case "rini":
                        writerIO.Writer.Write(command);
                        receiveINI(readerIO, writerIO);
                        break;
                    case "editINI":
                        if (!(command.Split(' ').Length > 1))
                        { Write("Invalid secondary param! \"Param[(str)loc]\""); goto Again; }
                        sendINI(readerIO, writerIO, command.Split(' ')[1]);
                        break;
                    case "test":
                        writerIO.Writer.Write(command);
                        Write(readerIO.Reader.ReadUInt32() == XSTL_STATUS_SUCCESS ? "It's AIVE! PRAISE ALLAH!" : "ded as fuk.");
                        break;
                    case "nano"://that linux reference tho
                        Write("Enjoy the GUI fgt!");
                        frm_iniEdit iniEditor = new frm_iniEdit();
                        iniEditor.ShowDialog();
                        goto Again;
                    default:
                        Write("\"{0}\" is not a valid command!", command);
                        break;
                }
                client.Close(); readerIO.Close(); writerIO.Close();
            } catch (SocketException sockEx) { Write("Socket Error: {0}", sockEx.Message); client.Close(); }
            goto Again;
        }

        private static void receiveLogs(EndianIO readerIO, EndianIO writerIO)
        {
            if (!Directory.Exists("logs\\"))
                Directory.CreateDirectory("logs\\");

            int logs = readerIO.Reader.ReadInt32();
            byte[] logBytes = readerIO.Reader.ReadBytes(logs);
            File.WriteAllBytes("logs\\recLMAO.log", logBytes);
            Write(readerIO.Reader.ReadUInt32() == XSTL_STATUS_SUCCESS ? "Logs received!" : "Couldn't receive logs!");
        }

        private static void receiveINI(EndianIO readerIO, EndianIO writerIO)
        {
            if (!Directory.Exists("ini\\"))
                Directory.CreateDirectory("ini\\");
            int iniSize = readerIO.Reader.ReadInt32();
            byte[] inibytes = readerIO.Reader.ReadBytes(iniSize);
            File.WriteAllBytes("ini\\recLMAO.ini", inibytes);
            Write(readerIO.Reader.ReadUInt32() == XSTL_STATUS_SUCCESS ? "INI received!" : "Couldn't receive INI!");
        }

        public static void sendINI(EndianIO readerIO, EndianIO writerIO, string location)
        {
            if (!File.Exists(location))
            { Write("File doesn't exist!"); return; }

            if (verifyINI(location)) {
                writerIO.Writer.Write("editINI");
                writerIO.Writer.Write(File.ReadAllBytes(location).Length);
                writerIO.Writer.Write(File.ReadAllBytes(location));
                Write("Response: {0}", readerIO.Reader.ReadUInt32() == XSTL_STATUS_SUCCESS ? "OK... Rebooted server." : "ERR");
            } else { Write("Unable to verify INI file!"); return; }
        }

        private static bool verifyINI(string location)
        {
            try {
                IniFile ini = new IniFile(location);
                Convert.ToBoolean(ini.IniReadValue("Server", "Override XEX Checks"));
                Convert.ToBoolean(ini.IniReadValue("Server", "Add to Startup"));
                Convert.ToBoolean(ini.IniReadValue("Server", "Allow Anonymous Users"));
                Convert.ToInt32(ini.IniReadValue("Network", "Port"));
                Convert.ToBoolean(ini.IniReadValue("Logging", "Parse Logs"));
                Convert.ToBoolean(ini.IniReadValue("Logging", "Log Connection"));
                Convert.ToBoolean(ini.IniReadValue("Logging", "Log Client Crash"));
                Convert.ToString(ini.IniReadValue("MySQL", "Server Address"));
                Convert.ToString(ini.IniReadValue("MySQL", "Database Name"));
                Convert.ToString(ini.IniReadValue("MySQL", "Database Username"));
                Convert.ToString(ini.IniReadValue("MySQL", "Database Password"));
                return true;
            } catch { return false; }
        }

        public static byte[] HexStringToBytes(string HexString)
        {
            byte[] buffer = new byte[HexString.Length / 2];
            for (int i = 0; i < HexString.Length; i += 2) {
                try { buffer[i / 2] = Convert.ToByte(HexString.Substring(i, 2), 0x10); }
                catch { buffer[i / 2] = 0; }
            } return buffer;
        }

        private static void Write(string text, params object[] args)
        { Console.WriteLine(String.Format("        -- {0}", text), args); }
    }
}
