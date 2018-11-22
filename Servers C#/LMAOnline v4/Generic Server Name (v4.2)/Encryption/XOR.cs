using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GSN.Encryption
{
    public class XOR
    {
        public static byte[] byXOR(byte[] Data, byte Key)
        {
            try {
                byte[] buffer = new byte[Data.Length];
                for (int i = 0; i < Data.Length; i++)
                    buffer[i] = (byte)(Data[i] ^ Key);
                return buffer;
            } catch { return null; }
        }

        public static string strXOR(byte[] Data, byte Key)
        {
            byte[] buffer = byXOR(Data, Key);
            return (buffer != null ? Encoding.Default.GetString(buffer) : "");
        }
    }
}
