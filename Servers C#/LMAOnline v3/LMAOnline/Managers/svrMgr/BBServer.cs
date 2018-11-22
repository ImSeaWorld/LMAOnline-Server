using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.IO;
using System.Net;
using System.Threading;
using System.Net.Sockets;

namespace LMAOnline.Managers
{
    public class BBServer
    {
        private string
            authKey = "2F9A59018B92AA2C3B375D0D66CF8255",
            IPAddr;
        private byte[] mBuff;
        private TcpListener BBServerListener = new TcpListener(IPAddress.Any, Globals.svrPort-1);
        private Thread BBThread;

        public BBServer()
        {
            try {
                BBThread = new Thread(new ThreadStart(listen));
                BBThread.Start();
            } catch { }
        }

        private void listen()
        {
            try {
                this.BBServerListener.Start();
                while (true) {
                    Thread.Sleep(100);
                    if (!Globals.serverRunning)
                        return;
                    if (this.BBServerListener.Pending())
                        new Thread(new ParameterizedThreadStart(cmdHandle)).Start(BBServerListener.AcceptTcpClient());
                }
            } catch (Exception ex) { Console.Clear(); Globals.write("[BBServer] Error listening! Reason: {0}", ex.Message); Globals.DelayedRestart(3); }
        }

        private void cmdHandle(object oAdmin)
        {
            TcpClient Admin = (TcpClient)oAdmin;
            NetworkStream nStream = Admin.GetStream();
            IPAddr = Admin.Client.RemoteEndPoint.ToString().Split(':')[0];

            try
            {
                Globals.write("BBServer [{0}] PING", IPAddr);

                EndianIO writerIO = new EndianIO(nStream, EndianStyle.BigEndian);
                EndianIO readerIO = new EndianIO(nStream, EndianStyle.BigEndian) { Writer = new EndianWriter(nStream, EndianStyle.BigEndian) };

                string auth = readerIO.Reader.ReadString();

                if (authKey != auth)
                { Globals.write("BBServer [{0}] Non-Matching Authkey!\n{1}", IPAddr, auth); Admin.Close(); return; }
                Globals.write("BBServer [{0}] Admin Authed", IPAddr);
                string command = readerIO.Reader.ReadString();
                Globals.write("BBServer [{0}] Command: \"{1}\"", IPAddr, command);
                switch (command.Split(' ')[0])
                {
                    case "qrestart":
                        writerIO.Writer.Write(Globals.XSTL_STATUS_SUCCESS);
                        Globals.DelayedRestart(Convert.ToInt32(command.Split(' ')[1]));
                        break;
                    case "shutdown":
                        writerIO.Writer.Write(Globals.XSTL_STATUS_SUCCESS);
                        Globals.DelayedRestart(0, true);
                        break;
                    case "rlogs":
                        sendLogs(readerIO, writerIO);
                        break;
                    case "rini":
                        sendINI(readerIO, writerIO);
                        break;
                    case "editINI":
                        editINI(readerIO, writerIO);
                        break;
                    case "test":
                        writerIO.Writer.Write(Globals.XSTL_STATUS_SUCCESS);
                        break;
                }
            } catch (Exception ex) { Globals.write("BBServer Error: {0}", ex.Message); }
        }

        private void sendLogs(EndianIO readerIO, EndianIO writerIO)
        {
            if (!File.Exists("logs\\Server.log"))
            { writerIO.Writer.Write(Globals.XSTL_STATUS_ERROR); return; }
            byte[] logs = File.ReadAllBytes("logs\\Server.log");
            writerIO.Writer.Write(logs.Length);
            writerIO.Writer.Write(logs);
            writerIO.Writer.Write(Globals.XSTL_STATUS_SUCCESS);
            Globals.write("BBServer [{0}] Logs sent!", IPAddr);
        }

        private void sendINI(EndianIO readerIO, EndianIO writerIO)
        {
            if (!File.Exists("ini\\LMAOnline.ini"))
            { writerIO.Writer.Write(Globals.XSTL_STATUS_ERROR); return; }
            byte[] iniBytes = File.ReadAllBytes("ini\\LMAOnline.ini");
            writerIO.Writer.Write(iniBytes.Length);
            writerIO.Writer.Write(iniBytes);
            writerIO.Writer.Write(Globals.XSTL_STATUS_SUCCESS);
            Globals.write("BBServer [{0}] INI sent!", IPAddr);
        }

        private void editINI(EndianIO readerIO, EndianIO writerIO)
        {
            int buffCount = readerIO.Reader.ReadInt32();
            byte[] recINI = readerIO.Reader.ReadBytes(buffCount);
            if (!(buffCount > 0))
            { writerIO.Writer.Write(Globals.XSTL_STATUS_ERROR); return; }
            if (!Misc.CompareBytes(File.ReadAllBytes("ini\\LMAOnline.ini"), recINI))
            {
                if (File.Exists("ini\\LMAOnline.ini")) File.Delete("ini\\LMAOnline.ini");
                File.WriteAllBytes("ini\\LMAOnline.ini", recINI);
                Globals.iniReadHandle();
                Globals.write("BBServer [{0}] New INI Loaded!", IPAddr);
                writerIO.Writer.Write(Globals.XSTL_STATUS_SUCCESS);
            } else Globals.write("BBServer [{0}] Same INI dumbass.", IPAddr);
        }
    }
}
