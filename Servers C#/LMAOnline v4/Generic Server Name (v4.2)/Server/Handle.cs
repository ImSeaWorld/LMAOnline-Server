using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Net;
using System.Threading;
using System.Net.Sockets;

using GSN.SQL;
using GSN.Globals;
using GSN.IO.Endian;
using GSN.IO.PoorManStream;
using GSN.Server.Packets;

namespace GSN.Server
{
    public class Handle
    {
        public Thread ClientListener;
        private TcpListener Listener = new TcpListener(IPAddress.Any, GlobalVar.i_svrPort);

        public Handle()
        {
            try {
                ClientListener = new Thread(new ThreadStart(ListenThread));
                ClientListener.Start();
                GlobalVar.b_serverRunning = true;
            } catch { GlobalFunc.WriteError(ConsoleColor.Red, "[Handle]", "Failed to start the listener thread!"); GlobalFunc.DelayedRestart(3); }
        }

        private void ListenThread()
        {
            try {
                this.Listener.Start();
                while (true) {
                    Thread.Sleep(100);
                    ClientSQL.CronCheck();
                    if (!GlobalVar.b_serverRunning) return;
                    if (this.Listener.Pending()) new Thread(new ParameterizedThreadStart(HandleClient)).Start(Listener.AcceptTcpClient());
                }
            } catch (Exception ex) { GlobalVar.b_serverRunning = false; Console.Clear(); GlobalFunc.WriteError(ConsoleColor.Red, "[ERROR]", "Error ListenThread! Reason: {0}", ex.Message); GlobalFunc.DelayedRestart(3); }
        }

        private void HandleClient(object cObj)
        {
            TcpClient Client = (TcpClient)cObj;
            Tmp.Entry Entry = new Tmp.Entry();
            GlobalVar.nStream = Client.GetStream();

            byte[] Header = new byte[8];
            Entry.IP = Client.Client.RemoteEndPoint.ToString().Split(':')[0];
            GlobalFunc.Write("Client [{0}] - Connected", Entry.IP);

            using (PoorManStream pmStream = new PoorManStream(GlobalVar.nStream)) {
                using (EndianIO MainIO = new EndianIO(Header, EndianStyle.BigEndian)) {
                    if (GlobalVar.nStream.Read(Header, 0, 8) != 8) { GlobalFunc.WriteError(ConsoleColor.Red, "[SERVER]", "Header recieved unexpected size!"); Client.Close(); }
                    uint Command = MainIO.Reader.ReadUInt32();
                    int PacketSize = MainIO.Reader.ReadInt32();
                    byte[] Buffer = new byte[PacketSize];
                    using (EndianIO WriterIO = new EndianIO(pmStream, EndianStyle.BigEndian)) {
                        using (EndianIO ReaderIO = new EndianIO(Buffer, EndianStyle.BigEndian) { Writer = new EndianWriter(pmStream, EndianStyle.BigEndian) }) {
                            if (pmStream.Read(Buffer, 0, PacketSize) != PacketSize) { GlobalFunc.WriteError(ConsoleColor.Red, "[SERVER]", "Packet recieved unexpected size!"); Client.Close(); }

                            switch (Command) {
                                case (uint)cmdCode.GET_SESSION: SESSION.Get(ref Entry, WriterIO, ReaderIO); break;
                                case (uint)cmdCode.GET_STATUS: STATUS.Get(ref Entry, WriterIO, ReaderIO); break;
                                case (uint)cmdCode.GET_CHAL_RESPONSE: break;
                                case (uint)cmdCode.UPDATE_PRESENCE: PRESENCE.Update(ref Entry, WriterIO, ReaderIO); break;
                                case (uint)cmdCode.GET_XOSC: break;
                                case (uint)cmdCode.GET_INFO: break;
                                case (uint)cmdCode.SND_SPOOFY: break;
                                case (uint)cmdCode.GET_MESSAGE: break;
                                case (uint)cmdCode.GET_PATCHES: break;
                                case (uint)cmdCode.GET_GUIDE_INFO: break;
                                default: break;
                            }
                        }
                    }
                }
            }
        }
    }
}
