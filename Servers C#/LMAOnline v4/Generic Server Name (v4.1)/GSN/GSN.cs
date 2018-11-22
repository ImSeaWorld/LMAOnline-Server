using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using GSN.Server;
using GSN.Managers;

// SeaWorld 3/10/2016
namespace GSN
{
    class GSN
    {
        static void Main(string[] args)
        {
            Console.Title = "LMAOnline v4 - GSN Edition";
            Console.BackgroundColor = ConsoleColor.Black;
            Console.ForegroundColor = ConsoleColor.Cyan;

            GlobalFunc.iniReadHandle();
            GlobalFunc.checkfolders();
            GlobalFunc.loadXeX();

            //if (!GlobalFunc.AllowAppThroughFirewall())
            //{ GlobalFunc.Write("Unable to add application to firewall. This may cause issues with other checks!"); }
            if (!GlobalFunc.LoadBinFiles())
            { GlobalFunc.Write("Failed to load proper bin files!"); GlobalFunc.DelayedRestart(3, true); return; }
            if (!GlobalFunc.CheckMySQL())
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
            Console.WriteLine("             Port: {0}... SUCCESS", Globals.i_svrPort.ToString());
            Console.WriteLine("             chal_resp Bytes: {0}", Globals.by_chalBytes.Length);
            Console.WriteLine("               HV Bytes: {0}", Globals.by_hvBytes.Length);
            //Console.WriteLine("               HVC Bytes: {0}", Globals.HVCBytes.Length);
            Console.WriteLine("               xexChecks: {0}", Globals.b_overRideChecks.ToString().ToUpper());
            Console.WriteLine("               Using .ini: {0}", Globals.b_usingINI.ToString().ToUpper());
            Console.WriteLine("         _____________________________\n\n");
            Console.WindowWidth = 100;
            Console.WindowHeight = Console.LargestWindowHeight;
            Console.Title = String.Format("{0} [#] Listening: {1} [#] Server: {2}", Console.Title, Globals.i_svrPort, Globals.s_dbName.ToUpper());
            svrHandle svr = new svrHandle();
            Globals.TimeStarted = DateTime.Now;
        }
    }
}
