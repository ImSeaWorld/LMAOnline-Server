using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using GSN.Server;
using GSN.Globals;

namespace GSN
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Title = "LMAOnline v4.2 - GSN Edition";
            Console.BackgroundColor = ConsoleColor.Black;
            Console.ForegroundColor = ConsoleColor.White;

            GlobalFunc.iniReadHandle();
            CheckFunc.checkfolders();
            CheckFunc.loadXeX();

            if (!CheckFunc.LoadBinFiles())
            { GlobalFunc.Write("Failed to load proper bin files!"); GlobalFunc.DelayedRestart(3, true); return; }
            if (!CheckFunc.CheckMySQL())
            { GlobalFunc.Write("MySQL Connection Error!"); GlobalFunc.DelayedRestart(3); return; }

            Console.Clear();
            Console.WriteLine("\n       ///////////////////////////////////");
            Console.WriteLine("      // LMAOnline V4 Has Been Started //");
            Console.WriteLine("     ///////////////////////////////////");
            Console.WriteLine("           ///////////////////////");
            Console.WriteLine("          // This Shit's Funny //");
            Console.WriteLine("         ///////////////////////");
            Console.WriteLine("         _____________________________");
            Console.WriteLine("          Thread: Starting... SUCCESS");
            Console.WriteLine("             Port: {0}... SUCCESS", GlobalVar.i_svrPort.ToString());
            Console.WriteLine("             chal_resp Bytes: {0}", GlobalVar.by_chalBytes.Length);
            Console.WriteLine("               HV Bytes: {0}", GlobalVar.by_hvBytes.Length);
            Console.WriteLine("               xexChecks: {0}", GlobalVar.b_overRideChecks.ToString().ToUpper());
            Console.WriteLine("               Using .ini: {0}", GlobalVar.b_usingINI.ToString().ToUpper());
            Console.WriteLine("         _____________________________\n\n");
            Console.WindowWidth = 100;
            Console.WindowHeight = Console.LargestWindowHeight;
            Console.Title = String.Format("{0} [#] Listening: {1} [#] Server: {2}", Console.Title, GlobalVar.i_svrPort, GlobalVar.s_dbName.ToUpper());
            new Handle();
            GlobalVar.TimeStarted = DateTime.Now;
        }
    }
}
