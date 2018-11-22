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
    public class PRESENCE
    {
        public static void Update(ref Tmp.Entry Entry, EndianIO WriterIO, EndianIO ReaderIO)
        {
            Entry.Session = ReaderIO.Reader.ReadBytes(0x10).ToHex();
            Entry.Title = ReaderIO.Reader.ReadBytes(0x4).ToHex();
            Entry.GamerTag = ReaderIO.Reader.ReadBytes(0x15).ToString();

            if (ClientSQL.Get(ref Entry, true)) {
                if (Entry.Enabled) {
                    WriterIO.Writer.Write((uint)respCode.RESP_SUCCESS);
                } else {
                    if (ClientSQL.Increment(ref Entry, true)) {
                        if (Entry.AutoIncrement)
                            ClientSQL.Increment(ref Entry);
                        WriterIO.Writer.Write(Entry.AutoIncrement ? (Entry.noto_DayStarted ? (uint)respCode.RESP_DAY_STARTED : (uint)respCode.RESP_SUCCESS) : (uint)respCode.RESP_REBOOT);
                    } else {
                        WriterIO.Writer.Write((uint)respCode.RESP_ERROR);
                    }
                } ClientSQL.Set(ref Entry);
            }
        }
    }
}
