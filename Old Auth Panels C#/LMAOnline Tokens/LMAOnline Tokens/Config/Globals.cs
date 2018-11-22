using System;
using System.Net;
using System.Runtime.InteropServices;

namespace LMAOnline_Tokens.Config
{
    [StructLayout(LayoutKind.Sequential)]
    public struct Globals
    {
        public int id;
        public bool used;
        public string token;
        public string creator;
        public int days;
    }
}

