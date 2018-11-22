using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using GSN.SQL;
using GSN.Server;
using GSN.Globals;
using GSN.IO.Endian;
using GSN.Encryption;

namespace GSN.Server.Packets
{
    public class STATUS
    {
        public static void Get(ref Tmp.Entry Entry, EndianIO WriterIO, EndianIO ReaderIO)
        {
            Entry.Session = ReaderIO.Reader.ReadBytes(0x10).ToHex();
            Entry.ClientHash = ReaderIO.Reader.ReadBytes(0x10);

            if (ClientSQL.Get(ref Entry, true)) {
                if (!Entry.Enabled) {
                    GlobalFunc.WriteError(ConsoleColor.DarkGray, "Client [{0}]", "{1} has expired.", Entry.IP, Entry.Name);
                    WriterIO.Writer.Write((uint)respCode.RESP_EXPIRED);
                }

                if (!GlobalVar.b_xexChecks) {
                    GlobalFunc.WriteError(ConsoleColor.Red, "[SERVER]", "XEX Checks are disabled!");
                    WriterIO.Writer.Write((uint)respCode.RESP_SUCCESS);
                }

                if (Entry.Checks) {
                    if (BitsNBytes.ByteCompare(Entry.ClientHash, HMAC.SHA1(GlobalVar.by_xexBytes, Entry.Session.ToByte(), 0, 16))) {
                        GlobalFunc.WriteError(ConsoleColor.Green, "Client [{0}]", "{1} passed checks.", Entry.IP, Entry.Name);
                        WriterIO.Writer.Write((uint)respCode.RESP_SUCCESS);
                    } else {
                        GlobalFunc.WriteError(ConsoleColor.Yellow, "Client [{0}]", "Out of date client! Sending update.", Entry.IP);
                        WriterIO.Writer.Write((uint)respCode.RESP_UPDATE);
                        WriterIO.Writer.Write(GlobalVar.by_xexBytes.Length);
                        WriterIO.Writer.Write(GlobalVar.by_xexBytes);
                    }
                } else {
                    GlobalFunc.WriteError(ConsoleColor.DarkYellow, "[ADMIN]", "Admin has omitted checks on user.");
                    WriterIO.Writer.Write((uint)respCode.RESP_SUCCESS);
                }
            } else {
                GlobalFunc.WriteError(ConsoleColor.Red, "[UNAUTHORIZED]", "Unknown client. IP: {0}", Entry.IP);
                WriterIO.Writer.Write((uint)respCode.RESP_ERROR);
            }
        }
    }
}
