using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using GSN.SQL;
using GSN.Server;
using GSN.Globals;
using GSN.IO.Endian;

namespace GSN.Server.Packets
{
    public class SESSION
    {
        public static void Get(ref Tmp.Entry Entry, EndianIO WriterIO, EndianIO ReaderIO)
        {
            bool conType = Convert.ToBoolean(ReaderIO.Reader.ReadInt32());
            Entry.CPUKey = ReaderIO.Reader.ReadBytes(0x10).ToHex();

            if (ClientSQL.Get(ref Entry)) {
                Entry.Session = BitsNBytes.RandomBytes(0x10).ToHex();
                if (Entry.Enabled) {
                    GlobalFunc.Write("Client [{0}] {1}'s {2} Authorized!", Entry.IP, Entry.Name, conType ? "Devkit" : "Jtag/RGH");
                    WriterIO.Writer.Write((uint)respCode.RESP_STEALTHED);
                    WriterIO.Writer.Write(Entry.Session.ToByte());
                } else {
                    if (ClientSQL.Increment(ref Entry, true)) {
                        if (Entry.AutoIncrement) {
                            if (ClientSQL.Increment(ref Entry)) {
                                if (Entry.noto_AskDayStart && Entry.noto_DayStarted) {
                                    // ask about starting the day
                                    if (/*Asking result*/ true == false)
                                        WriterIO.Writer.Write((uint)respCode.RESP_EXPIRED);
                                    WriterIO.Writer.Write((uint)respCode.RESP_DAY_STARTED);
                                } else if (Entry.noto_DayStarted) { 
                                    if (Entry.Lifetime) WriterIO.Writer.Write((uint)respCode.RESP_STEALTHED); // Responds without notification
                                    else WriterIO.Writer.Write((uint)respCode.RESP_DAY_STARTED);
                                } else if (!Entry.noto_DayStarted) {
                                    WriterIO.Writer.Write((uint)respCode.RESP_STEALTHED); // Responds without notification
                                } WriterIO.Writer.Write(Entry.Session.ToByte());
                            } else {
                                GlobalFunc.WriteError(ConsoleColor.DarkGray, "Client [{0}]", "{1}'s Time Expired!", Entry.IP, Entry.Name);
                                WriterIO.Writer.Write((uint)respCode.RESP_EXPIRED);
                            }
                        } else {
                            GlobalFunc.WriteError(ConsoleColor.DarkGray, "Client [{0}]", "{1} opted out of incrementing a day.", Entry.IP, Entry.Name);
                            WriterIO.Writer.Write((uint)respCode.RESP_REBOOT);
                        }
                    }
                }
            } else {
                // who duh fuk u be
            }
        }
    }
}
