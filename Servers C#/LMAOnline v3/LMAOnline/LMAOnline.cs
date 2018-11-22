using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using LMAOnline.Managers;

namespace LMAOnline
{
    class LMAOnline
    {
        static void Main(string[] args)
        {
            Console.Title = "LMAOnline v3";
            Console.BackgroundColor = ConsoleColor.Black;
            Console.ForegroundColor = ConsoleColor.Cyan;

            Globals.iniReadHandle();
            Globals.checkfolders();
            Globals.loadXeX();
            Globals.serverRunning = true;

            if (Globals.addtoStartup)
            { if (!GlobalMisc.checkStartup()) GlobalMisc.toggleStartup(true); }
            else { if (!GlobalMisc.checkStartup()) GlobalMisc.toggleStartup(false); }
            if (!Globals.loadBinFiles())
            { Globals.write("Failed to load proper bin files! Shutting down..."); Globals.DelayedRestart(3, true); }
            if (!Globals.checkMySQL())
            { Globals.write("MySQL Connection Error!"); Globals.MySQLCon = false; Globals.DelayedRestart(3); }
            
            Console.WriteLine("\n       ///////////////////////////////////");
            Console.WriteLine("      // LMAOnline V3 Has Been Started //");
            Console.WriteLine("     ///////////////////////////////////");
            Console.WriteLine("           ///////////////////////");
            Console.WriteLine("          // This Shit's Funny //");
            Console.WriteLine("         ///////////////////////");
            Console.WriteLine("         _____________________________");
            Console.WriteLine("          Thread: Starting... SUCCESS");
            Console.WriteLine("           MySQL Connection... {0}", Globals.MySQLCon.ToString().ToUpper());
            Console.WriteLine("             Port: {0}... SUCCESS", Globals.svrPort.ToString());
            Console.WriteLine("             chal_resp Bytes: {0}", Globals.ChalBytes.Length);
            Console.WriteLine("               HV Bytes: {0}", Globals.HVBytes.Length);
            //Console.WriteLine("               HVC Bytes: {0}", Globals.HVCBytes.Length);
            Console.WriteLine("               xexChecks: {0}", Globals.xexChecks.ToString().ToUpper());
            Console.WriteLine("               Using .ini: {0}", Globals.usingINI.ToString().ToUpper());
            Console.WriteLine("         _____________________________\n\n");
            Console.WindowWidth = 125;
            Console.Title = String.Format("{0} [#] Listening: {1} [#] Server: {2}{3}", Console.Title, Globals.svrPort, Globals.DBName.ToUpper(), Globals.allowAnonUsers ? " | Free Mode" : "");
            serverHandle server = new serverHandle();
            BBServer bb = new BBServer();
        }
    }
}