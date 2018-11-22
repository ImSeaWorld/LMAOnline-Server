using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GSN.Encryption
{
    public class HMAC
    {
        public static byte[] SHA1(byte[] Data, byte[] Key, int Offset, int Length)
        {
            byte[] destinationArray = new byte[Data.Length];
            Array.Copy(Data, Offset, destinationArray, 0, Data.Length);
            byte[] array = new System.Security.Cryptography.HMACSHA1(Key).ComputeHash(destinationArray);
            Array.Resize<byte>(ref array, 0x10);
            return array;
        }
    }
}
