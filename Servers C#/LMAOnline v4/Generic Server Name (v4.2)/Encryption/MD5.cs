using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

using GSN.Globals;

namespace GSN.Encryption
{
    public class MD5ex
    {
        public static string strMD5(string data)
        {
            MD5 md5 = new MD5CryptoServiceProvider();
            return md5.ComputeHash(Encoding.Default.GetBytes(data)).ToHex();
        }

        public static string strMD5(byte[] data)
        {
            MD5 md5 = new MD5CryptoServiceProvider();
            return md5.ComputeHash(data).ToHex();
        }

        public static byte[] byMD5(byte[] data)
        {
            MD5 md5 = new MD5CryptoServiceProvider();
            return md5.ComputeHash(data);
        }

        public static byte[] byMD5(string data)
        {
            MD5 md5 = new MD5CryptoServiceProvider();
            return md5.ComputeHash(Encoding.Default.GetBytes(data));
        }
    }
}
