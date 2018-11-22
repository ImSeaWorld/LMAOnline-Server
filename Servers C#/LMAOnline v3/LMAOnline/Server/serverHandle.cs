using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.IO;
using System.Net;
using System.Threading;
using System.Diagnostics;
using System.Net.Sockets;
using LMAOnline.Managers;
using MySql.Data.MySqlClient;
using System.Security.Cryptography;

namespace LMAOnline
{
    public class serverHandle
    {
        public Thread clientListener;
        private TcpListener listener = new TcpListener(IPAddress.Any, Globals.svrPort);
        [ThreadStatic] private int BufferCount = 0;
        [ThreadStatic] private string IPAddr;
        [ThreadStatic] private byte[] mainBuff, tmpBuff, myCockBuff;

        public serverHandle()
        {
            try {
                clientListener = new Thread(new ThreadStart(listenForClients));
                clientListener.Start();
            } catch { }
        }

        private void listenForClients()
        {
            try {
                this.listener.Start();
                while (true) {
                    Thread.Sleep(100);
                    ConsoleMySQL.updateEnabledConsoles();
                    if (!Globals.serverRunning)
                        return;
                    if (this.listener.Pending())
                        new Thread(new ParameterizedThreadStart(clientHandle)).Start(listener.AcceptTcpClient());
                }
            } catch (Exception ex) { Console.Clear(); Globals.write("Error listenForClients! Reason: {0}", ex.Message); Globals.DelayedRestart(3); }
        }

        private void clientHandle(object clientObj)
        {
            TcpClient Client = (TcpClient)clientObj;
            TmpEntry entry = new TmpEntry();
            NetworkStream nStream = Client.GetStream();
            PoorManStream pmStream = new PoorManStream(nStream);
            IPAddr = Client.Client.RemoteEndPoint.ToString();

            try {
                Globals.write("{0} [{1}] PING", Globals.allowAnonUsers ? "Anon" : "Client", IPAddr);
                entry.ClientIP = IPAddr.Split(':')[0];
                BufferCount = 8;
                mainBuff = new byte[BufferCount];
                myCockBuff = new byte[0x10];
                if (nStream.Read(mainBuff, 0, 8) != 8) { Client.Close(); Globals.write("Niggered 1 - BuffCount: {0}", BufferCount); return; }
                EndianIO mainIO = new EndianIO(mainBuff, EndianStyle.BigEndian);
                uint CommandID = mainIO.Reader.ReadUInt32();
                BufferCount = mainIO.Reader.ReadInt32();
                if (BufferCount >= 0x8000) { Client.Close(); Globals.write("Niggered 2 - BuffCount: {0}", BufferCount); return; }
                mainBuff = new byte[BufferCount];
                EndianIO writerIO = new EndianIO(pmStream, EndianStyle.BigEndian);
                EndianIO readerIO = new EndianIO(mainBuff, EndianStyle.BigEndian) { Writer = new EndianWriter(pmStream, EndianStyle.BigEndian) };
                EndianWriter structIO = new EndianIO(myCockBuff, EndianStyle.BigEndian).Writer;
                if (pmStream.Read(mainBuff, 0, BufferCount) != BufferCount) { Client.Close(); Globals.write("Niggered 3 [{0}]", IPAddr); return; }

                switch (CommandID) {
                    case Globals.XSTL_SERVER_COMMAND_ID_GET_SALT:
                        getSalt(ref entry, readerIO, writerIO);
                        break;
                    case Globals.XSTL_SERVER_COMMAND_ID_GET_STATUS:
                        xexChecks(ref entry, readerIO, writerIO);
                        break;
                    case Globals.XSTL_SERVER_COMMAND_ID_GET_CHAL_RESPONCE:
                        //HVC(ref entry, readerIO, writerIO);
                        getChallResp(ref entry, readerIO, writerIO); // Updated to HVC method
                        break;
                    case Globals.XSTL_SERVER_COMMAND_ID_UPDATE_PRESENCE:
                        updatePresence(ref entry, readerIO, writerIO);
                        break;
                    case Globals.XSTL_SERVER_COMMAND_ID_GET_XOSC:
                        getXOSC(ref entry, readerIO, writerIO);
                        break;
                    case Globals.XSTL_SERVER_COMMAND_ID_GET_INFO:
                        returnInfo(ref entry, readerIO, writerIO);
                        break;
                    case Globals.XSTL_SERVER_COMMAND_ID_REDEEM_TOKEN:
                        con_redeem(ref entry, readerIO, writerIO, structIO);
                        break;
                    case Globals.XSTL_SERVER_COMMAND_ID_LOL_SPOOFY:
                        HandleSpoofy(ref entry);
                        break;
                    case Globals.XSTL_SERVER_COMMAND_ID_GET_MESSAGE:
                        retMessage(ref entry, readerIO, writerIO, structIO);
                        break;
                    case Globals.XSTL_SERVER_COMMAND_ID_GET_PATCHES:
                        patchData(ref entry, readerIO, writerIO);
                        break;
                    default:
                        Globals.write("Client [{0}] Some fucker sent an invalid request! Gottem!", IPAddr);
                        Client.Close(); // Invalid or unknown response. Fuck'em.
                        break;
                }

                //if (BufferCount >= 0x10)
                //    ConsoleMySQL.saveConsole(ref entry);
                try { mainIO.Close(); readerIO.Close(); writerIO.Close(); structIO.Close(); } catch { } // Since we can't always do this...
            } catch (Exception ex) {
                if (Globals.printClientCrash)
                    Globals.write("Client [{0}] Crashed! Closed connection... Detailed: {1} || {2}", IPAddr, ex.Source, ex.Message);
                Client.Close();
            }
        }

        private void getSalt(ref TmpEntry entry, EndianIO readerIO, EndianIO writerIO)
        {
            entry.ClientConType = Convert.ToBoolean(readerIO.Reader.ReadInt32());
            entry.CPUKey = Misc.BytesToHexString(readerIO.Reader.ReadBytes(0x10));

            //if (BufferCount >= 0x14) entry.KVDat = readerIO.Reader.ReadBytes(BufferCount - 0x14); // BufferCount > 20

            if (!Globals.allowAnonUsers) {
                if (ConsoleMySQL.getConsole(ref entry)) {
                    entry.ClientSession = Misc.BytesToHexString(Security.RandomBytes(0x10));
                    ConsoleMySQL.saveConsole(ref entry);
                    if (entry.ClientBanned) {
                        Globals.write("Client [{0}] Banned CPUKey: {1}", IPAddr, entry.CPUKey);
                        ConsoleMySQL.updateOtherConsoles(ref entry, true);
                        writerIO.Writer.Write(Globals.XSTL_STATUS_BANNED);
                        return;
                    }
                    if (entry.ClientEnabled) {
                        Globals.write("Client [{0}] Authorized {1} CPUKey: {2}", IPAddr, !entry.ClientConType ? "Jtag" : "Devkit", entry.CPUKey);
                        writerIO.Writer.Write(Globals.XSTL_STATUS_STEALTHED);
                        writerIO.Writer.Write(Misc.HexStringToBytes(entry.ClientSession));
                    } else {
                        if (ConsoleMySQL.autoUpdateTime(ref entry)) {
                            if (entry.daysLeft <= 499) {
                                Globals.write("Client [{0}] Started their next 24 hours! - CPUKey: {1}", IPAddr, entry.CPUKey);
                                writerIO.Writer.Write(Globals.XSTL_STATUS_DAY_STARTED);
                            } else writerIO.Writer.Write(Globals.XSTL_STATUS_STEALTHED);
                            writerIO.Writer.Write(Misc.HexStringToBytes(entry.ClientSession));
                        } else {
                            Globals.write("Client [{0}] Expired CPUKey: {1}", IPAddr, entry.CPUKey);
                            writerIO.Writer.Write(Globals.XSTL_STATUS_EXPIRED);
                        }
                    } ConsoleMySQL.saveConsole(ref entry);
                } else {
                    Globals.write("Client [{0}] Failed Authorization! CPUKey: {1}", IPAddr, entry.CPUKey);
                    ConsoleMySQL.updateOtherConsoles(ref entry);
                    writerIO.Writer.Write(Globals.XSTL_STATUS_ERROR);
                    return;
                }
            } else {
                if (AnonSQL.getConsoleAnon(ref entry)) {
                    entry.ClientSession = Misc.BytesToHexString(Security.RandomBytes(0x10));
                    if (entry.ClientBanned) {
                        Globals.write("Anon [{0}] Banned CPUKey: {1}", IPAddr, entry.CPUKey);
                        ConsoleMySQL.updateOtherConsoles(ref entry, true);
                        writerIO.Writer.Write(Globals.XSTL_STATUS_BANNED);
                        return;
                    }
                    Globals.write("Anon [{0}] Authorized {1} CPUKey: {2}", IPAddr, entry.ClientConType ? "Devkit" : "Jtag", entry.CPUKey);
                    writerIO.Writer.Write(Globals.XSTL_STATUS_XBLFREE);
                    writerIO.Writer.Write(Misc.HexStringToBytes(entry.ClientSession));
                } else {
                    AnonSQL.addAnon(ref entry);
                    Globals.write("Anon [{0}] New User! CPUKey: {1}", IPAddr, entry.CPUKey);
                    writerIO.Writer.Write(Globals.XSTL_STATUS_XBLFREE);
                    writerIO.Writer.Write(Misc.HexStringToBytes(entry.ClientSession));
                }
            }
        }

        private void xexChecks(ref TmpEntry entry, EndianIO readerIO, EndianIO writerIO)
        {
            entry.CPUKey = Misc.BytesToHexString(readerIO.Reader.ReadBytes(0x10));
            entry.xexHash = readerIO.Reader.ReadBytes(0x10);

            if (Globals.xexChecks) {
                if (!Globals.allowAnonUsers) {
                    if (ConsoleMySQL.getConsole(ref entry)) {
                        if (!entry.ClientEnabled)
                        { Globals.write("Client [{0}] Expired! Haulted XEX Checks! - CPUKey: {1}", IPAddr, entry.CPUKey); writerIO.Writer.Write(Globals.XSTL_STATUS_EXPIRED); return; }

                        if (entry.ClientXeXChecks) {
                            if (Misc.CompareBytes(entry.xexHash, Security.HMACSHA1(Globals.XEXBytes, Misc.HexStringToBytes(entry.ClientSession), 0, 16))) {
                                Globals.write("Client [{0}] Passed XEX Checks! - CPUKey: {1}", IPAddr, entry.CPUKey);
                                writerIO.Writer.Write(Globals.XSTL_STATUS_SUCCESS);
                                return;
                            } else {
                                Globals.write("Client [{0}] Failed XEX Checks... Updating now. - CPUKey: {1}", IPAddr, entry.CPUKey);
                                writerIO.Writer.Write(Globals.XSTL_STATUS_UPDATE);
                                writerIO.Writer.Write(Globals.XEXBytes.Length);
                                writerIO.Writer.Write(Globals.XEXBytes);
                                return;
                            }
                        } else { Globals.write("Client [{0}] Omit From Checks. CPUKey: {1}", IPAddr, entry.CPUKey); writerIO.Writer.Write(Globals.XSTL_STATUS_SUCCESS); return; }
                    }
                } else {
                    if (AnonSQL.getConsoleAnon(ref entry)) {
                        if (entry.ClientBanned)
                        { Globals.write("Anon [{0}] This kid's banned. CPUKey: {1}", IPAddr, entry.CPUKey); writerIO.Writer.Write(Globals.XSTL_STATUS_ERROR); return; }

                        if (Misc.CompareBytes(entry.xexHash, Security.HMACSHA1(Globals.XEXBytes, Misc.HexStringToBytes(entry.ClientSession), 0, 16))) {
                            Globals.write("Anon [{0}] Passed XEX Checks! - CPUKey: {1}", IPAddr, entry.CPUKey);
                            writerIO.Writer.Write(Globals.XSTL_STATUS_SUCCESS);
                            return;
                        } else {
                            Globals.write("Anon [{0}] Failed XEX Checks... Updating now. - CPUKey: {1}", IPAddr, entry.CPUKey);
                            writerIO.Writer.Write(Globals.XSTL_STATUS_UPDATE);
                            writerIO.Writer.Write(Globals.XEXBytes.Length);
                            writerIO.Writer.Write(Globals.XEXBytes);
                            return;
                        }
                    }
                }
            } else { Globals.write("{0} [{1}] XeXChecks Disabled! CPUKey: {2}", Globals.allowAnonUsers ? "Anon" : "Client", IPAddr, entry.CPUKey); writerIO.Writer.Write(Globals.XSTL_STATUS_SUCCESS); }
        }

        private byte[] hashECC(byte[] salt)
        {
            byte[] cache = File.ReadAllBytes("cacheLocation"), 
                hv = File.ReadAllBytes("hvLocation");
            SHA1Managed hash = new SHA1Managed();
            hash.TransformBlock(salt, 0, 0x2, null, 0);
            hash.TransformBlock(hv, 0x34, 0xC, null, 0);
            hash.TransformBlock(hv, 0x40, 0x30, null, 0);
            hash.TransformBlock(hv, 0x70, 0x4, null, 0);
            hash.TransformBlock(hv, 0x78, 0x8, null, 0);
            hash.TransformBlock(cache, 0x2, 0x3FE, null, 0);
            hash.TransformBlock(hv, 0x100C0, 0x40, null, 0);
            hash.TransformBlock(hv, 0x10350, 0x30, null, 0);
            hash.TransformBlock(hv, 0x40E, 0x176, null, 0);
            hash.TransformBlock(hv, 0x16100, 0x40, null, 0);
            hash.TransformBlock(hv, 0x16D20, 0x60, null, 0);
            hash.TransformBlock(cache, 0x5B6, 0x24A, null, 0);
            hash.TransformBlock(cache, 0x800, 0x400, null, 0);
            hash.TransformFinalBlock(cache, 0xC00, 0x400);
            return new HMACSHA1(salt).ComputeHash(hash.Hash);
        }

        private void getChallResp(ref TmpEntry entry, EndianIO readerIO, EndianIO writerIO)
        {
            SHA1CryptoServiceProvider sha1 = new SHA1CryptoServiceProvider();
            bool Crl = false, Fcrt = false, Type1kv = false;
            uint[] chalVars = { 0x23289d3/*Num 9*/, 0xd83e/*Num10*/, 0x304000d/*Num11*/, 0x1F8FC0D/*num11-this (type1KV)*/ };
            entry.ClientSession = Misc.BytesToHexString(readerIO.Reader.ReadBytes(0x10));
            entry.ClientSalt = Misc.BytesToHexString(readerIO.Reader.ReadBytes(0x10));

            if (BufferCount > 0x20) {
                Crl = readerIO.Reader.ReadBoolean();
                Fcrt = readerIO.Reader.ReadBoolean();
                Type1kv = readerIO.Reader.ReadBoolean();
            }

            if (ConsoleMySQL.getConsole(ref entry, true)) {
                if (entry.ClientEnabled)
                {
                    EndianIO seekIO = new EndianIO(Globals.ChalBytes, EndianStyle.BigEndian);
                    seekIO.Stream.Seek(0x2EL, SeekOrigin.Begin);
                    seekIO.Writer.Write((Type1kv ? (ushort)(chalVars[1] & -33) : chalVars[1]));
                    seekIO.Stream.Seek(0x38L, SeekOrigin.Begin);
                    seekIO.Writer.Write((Crl ? (chalVars[0] | (Fcrt ? 0x1000000 : 0x10000)) : chalVars[0]));
                    seekIO.Writer.Write((Type1kv ? chalVars[2] - chalVars[3] : chalVars[2]));
                    seekIO.Stream.Seek(0xEC, SeekOrigin.Begin);
                    // HV Salt Begin
                    sha1.TransformBlock(Misc.HexStringToBytes(entry.ClientSalt), 0, 0x10, null, 0);
                    sha1.TransformBlock(Globals.HVBytes, 0x34, 0x40, null, 0);
                    sha1.TransformBlock(Globals.HVBytes, 0x78, 0xf88, null, 0);
                    sha1.TransformBlock(Globals.HVBytes, 0x100c0, 0x40, null, 0);
                    sha1.TransformBlock(Globals.HVBytes, 0x10350, 0xDF0, null, 0);
                    sha1.TransformBlock(Globals.HVBytes, 0x16D20, 0x2E0, null, 0);
                    sha1.TransformBlock(Globals.HVBytes, 0x20000, 0xffc, null, 0);
                    sha1.TransformFinalBlock(Globals.HVBytes, 0x30000, 0xffc);
                    entry.ClientHVSalt = sha1.Hash;
                    // HV Salt End
                    seekIO.Writer.Write(entry.ClientHVSalt);
                    seekIO.Writer.Flush();
                    seekIO.Close();
                    // End SeekIO
                    BufferCount = 256;
                    mainBuff = new byte[BufferCount];
                    Buffer.BlockCopy(Globals.ChalBytes, 0x20, mainBuff, 0x20, 0xE0);//Padding
                    EndianIO statusIO = new EndianIO(mainBuff, EndianStyle.BigEndian);
                    statusIO.Writer.Write(Globals.XSTL_STATUS_STEALTHED);
                    writerIO.Writer.Write(mainBuff);
                    File.WriteAllBytes("dmp\\resp.dmp", mainBuff);
                    Globals.write("Client [{0}] HVSalt Sent - CPUKey: {1}", IPAddr, entry.CPUKey);
                } else writerIO.Writer.Write(Globals.XSTL_STATUS_EXPIRED);
            } else writerIO.Writer.Write(Globals.XSTL_STATUS_ERROR);
        }

        private void updatePresence(ref TmpEntry entry, EndianIO readerIO, EndianIO writerIO)
        {
            entry.ClientSession = Misc.BytesToHexString(readerIO.Reader.ReadBytes(0x10));
            entry.ClientTitle = Misc.BytesToHexString(readerIO.Reader.ReadBytes(0x4));
            entry.ClientGT = GlobalMisc.ConvertBytesToString(readerIO.Reader.ReadBytes(0x15));
            //Globals.write("updateShit Session: {0}", entry.ClientSession);
            if (ConsoleMySQL.getConsole(ref entry, true)) {
                if (entry.ClientEnabled)
                    writerIO.Writer.Write(Globals.XSTL_STATUS_SUCCESS);
                else {
                    if (ConsoleMySQL.autoUpdateTime(ref entry)) {
                        writerIO.Writer.Write(Globals.XSTL_STATUS_SUCCESS);
                    } else writerIO.Writer.Write(Globals.XSTL_STATUS_EXPIRED);
                }
                ConsoleMySQL.saveConsole(ref entry);
            } else writerIO.Writer.Write(Globals.XSTL_STATUS_ERROR);
        }

        private void getXOSC(ref TmpEntry entry, EndianIO readerIO, EndianIO writerIO)
        {
            SHA1CryptoServiceProvider sha1 = new SHA1CryptoServiceProvider();
            bool Crl = false, Fcrt = false, Type1kv = false; long HVProtectedFlags;
            uint[] XOSCVars = { 0x23289d3/*Num9*/, 0xd83e/*Num10*/, 0x2bf/*Num15*/ };
            // Temp Vars
            byte[] buff1 = null, buff2 = null, buff3 = null, inputBuffer = null, dest = null, XOSCDmp = null;
            byte buff4; ushort num1, num2;
            // Temp Vars End
            entry.ClientSession = Misc.BytesToHexString(readerIO.Reader.ReadBytes(0x10));
            entry.ClientExecutionResult = readerIO.Reader.ReadUInt32();
            entry.ClientExecutionID = readerIO.Reader.ReadBytes(0x18);
            HVProtectedFlags = readerIO.Reader.ReadInt64();
            Crl = readerIO.Reader.ReadBoolean();
            Fcrt = readerIO.Reader.ReadBoolean();
            Type1kv = readerIO.Reader.ReadBoolean();
            
            if (ConsoleMySQL.getConsole(ref entry, true)) {
                if (entry.ClientEnabled) {
                    BufferCount = 0x4000;
                    mainBuff = new byte[BufferCount];
                    Buffer.BlockCopy(entry.KVDat, 0, mainBuff, 0, BufferCount);
                    inputBuffer = Security.SHA1(Misc.HexStringToBytes(entry.CPUKey));
                    readerIO.Stream.Seek(0xC89L, SeekOrigin.Begin);
                    buff4 = readerIO.Reader.ReadByte();                             //Buffer 4 Byte
                    readerIO.Stream.Seek(0xC8AL, SeekOrigin.Begin); // Disk Drive Data
                    buff1 = readerIO.Reader.ReadBytes(0x24);                        //Buffer 1 Byte[0x24]
                    readerIO.Stream.Seek(0x9CAL, SeekOrigin.Begin);
                    buff2 = readerIO.Reader.ReadBytes(5);                           //Buffer 2 Byte[0x5]
                    readerIO.Stream.Seek(0xB0L, SeekOrigin.Begin); // KV Serial Number
                    buff3 = readerIO.Reader.ReadBytes(0xC);                         //Buffer 2 Byte[0xC]
                    readerIO.Stream.Seek(200L, SeekOrigin.Begin);
                    num1 = readerIO.Reader.ReadUInt16();                            //Num 1 ushort
                    readerIO.Stream.Seek(0x1CL, SeekOrigin.Begin);
                    num2 = readerIO.Reader.ReadUInt16();                            //Num 2 ushort
                    // Begin KV Data Entry
                    sha1.TransformBlock(inputBuffer, 0, 0x10, null, 0);
                    sha1.TransformBlock(entry.KVDat, 0x1C, 0xD4, null, 0);
                    sha1.TransformBlock(entry.KVDat, 0x100, 0x1CF8, null, 0);
                    sha1.TransformFinalBlock(entry.KVDat, 0x1EF8, 0x2108);
                    Buffer.BlockCopy(sha1.Hash, 0, dest, 0, 0x10);
                    BufferCount = 0x2E0;
                    mainBuff = new byte[BufferCount];
                    writerIO.Writer.Write((uint)0);
                    writerIO.Writer.Write((uint)0x90002);
                    writerIO.Writer.Write((ulong)XOSCVars[2]);
                    writerIO.Writer.Write((ulong)0L);
                    writerIO.Writer.Write(entry.ClientExecutionResult);
                    writerIO.Writer.Write(Misc.HexStringToBytes("00000000C8003003AAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAA00000000"));
                    writerIO.Writer.Write(entry.ClientExecutionResult == 0 ? entry.ClientExecutionID : Misc.HexStringToBytes("AAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAA"));
                    writerIO.Writer.Write(Misc.HexStringToBytes("2121212121212121212121212121212121212121212121212121212121212121527A5A4BD8F505BB94305A1779729F3B000000"));
                    writerIO.Writer.Write(buff4);
                    writerIO.Writer.Write(Misc.HexStringToBytes(entry.ClientExecutionResult == 0 ? "0000000000000000" : "AAAAAAAAAAAAAAAA"));
                    writerIO.Writer.Write(Misc.HexStringToBytes("AAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAA"));
                    writerIO.Writer.Write(buff1);
                    writerIO.Writer.Write(buff1);
                    writerIO.Writer.Write(buff3);
                    writerIO.Writer.Write((ushort)170);
                    writerIO.Writer.Write(Type1kv ? ((ushort)(XOSCVars[1] & -33)) : XOSCVars[1]);
                    writerIO.Writer.Write(num1);
                    writerIO.Writer.Write(num2);
                    writerIO.Writer.Write(Misc.HexStringToBytes("000000000000000000070000"));
                    writerIO.Writer.Write((Crl ? (XOSCVars[0] | (Fcrt ? 0x1000000 : 0x10000)) : XOSCVars[0]));
                    writerIO.Writer.Write(Misc.HexStringToBytes("AAAAAAAA000000000000000000000000AAAAAAAA000D0008000000080000000000000000000000000000000000000000000000000000000000000000"));
                    writerIO.Writer.Write((4 | (((HVProtectedFlags & 1) == 1) ? 1 : 0)));
                    writerIO.Writer.Write(buff2);
                    writerIO.Writer.Write(Misc.HexStringToBytes("0000000000000000000000000000000000000000000000000000000000000000000000000000000000000040000207000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000AAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAA0000000000000000000000000000000000200000000000000000000000000006AAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAA5F534750AAAAAAAA"));
                    File.WriteAllBytes("dmp\\XOSC.dmp", XOSCDmp);
                    if (writerIO.Position != 0x2E0L)
                        Globals.write("Client [{0}] Failed XOSC Dump! - CPUKey: {1}", IPAddr, entry.CPUKey);
                    writerIO.Writer.Write(XOSCDmp);
                } else writerIO.Writer.Write(Globals.XSTL_STATUS_EXPIRED);
            } else writerIO.Writer.Write(Globals.XSTL_STATUS_ERROR);
        }

        private void returnInfo(ref TmpEntry entry, EndianIO readerIO, EndianIO writerIO) // Nolonger used.
        {
            entry.CPUKey = Misc.BytesToHexString(readerIO.Reader.ReadBytes(0x10));
            if (ConsoleMySQL.getConsole(ref entry))
            {
                if (entry.ClientEnabled)
                {
                    string retName = entry.ClientName + "\0";
                    uint size = (sizeof(uint) * 4) + 35;
                    byte[] nameBuff = new byte[35];
                    mainBuff = new byte[size];
                    Buffer.BlockCopy(Encoding.ASCII.GetBytes(retName), 0, nameBuff, 0, retName.Length);
                    EndianWriter structWriter = new EndianIO(mainBuff, EndianStyle.BigEndian).Writer;
                    structWriter.Write(entry.daysLeft);
                    structWriter.Write(nameBuff);
                    writerIO.Writer.Write(mainBuff);
                }
            }
        }

        private void con_redeem(ref TmpEntry entry, EndianIO readerIO, EndianIO writerIO, EndianWriter structIO)
        {
            // Grabbing the token from the request.
            string recCpuKey = Misc.BytesToHexString(readerIO.Reader.ReadBytes(0x10)); // Cross check the CPUKey here with the one in the DB.
            string recSession = Misc.BytesToHexString(readerIO.Reader.ReadBytes(0x10)); // Receiving their sent session to cross check.
            entry.inToken = Misc.BytesToHexString(readerIO.Reader.ReadBytes(0x19)); // We'll need to change the length later.
            if (ConsoleMySQL.getConsole(ref entry))
            {
                if (entry.ClientEnabled)
                {
                    if (ConsoleMySQL.getToken(ref entry))
                    {
                        entry.daysLeft = !entry.TokenUsed ? entry.daysLeft + entry.TokenDays : entry.daysLeft;
                        string retMsgSuccess = String.Format("Token has been successfully redeemed!\nCPUKey: {0}\nRedeemed {1} Days\n{2}\0", entry.CPUKey, entry.TokenDays, (entry.daysLeft >= 500 ? "You still have lifetime." : "You have " + entry.daysLeft + " days on reserve.")),
                            retMsgUsed = String.Format("Token seems to already be used.\n\nIf you feel this is a mistake, please message staff ASAP!\n\nYour CPUKey: {0}\0", entry.CPUKey);
                        uint size = (sizeof(uint) * 4) + Convert.ToUInt32((!entry.TokenUsed ? retMsgSuccess.Length : retMsgUsed.Length) + 2);
                        byte[] tmpBuff = new byte[size];
                        mainBuff = new byte[size];
                        Buffer.BlockCopy(Encoding.ASCII.GetBytes(retMsgSuccess), 0, tmpBuff, 0, (!entry.TokenUsed ? retMsgSuccess.Length : retMsgUsed.Length) + 2);
                        structIO.Write(Globals.XSTL_STATUS_SUCCESS);
                        structIO.Write(!entry.TokenUsed ? retMsgSuccess : retMsgUsed);
                        writerIO.Writer.Write(mainBuff);
                        Globals.write("Client [{0}] CPUKey: {1}\n{2} Token: {3}", IPAddr, entry.CPUKey, (!entry.TokenUsed ? "Redeemed" : "Tried Redeeming"), entry.outToken);
                        if (!entry.TokenUsed) { ConsoleMySQL.saveToken(ref entry); ConsoleMySQL.saveConsole(ref entry); }
                        return;
                    } structIO.Write(Globals.XSTL_STATUS_ERROR);
                }
            }
        }

        private void HandleSpoofy(ref TmpEntry entry)
        {
            Globals.write("Spoofy [{0}] Faggot Detected! CPUKey: {1}", IPAddr, entry.CPUKey);
            ConsoleMySQL.updateOtherConsoles(ref entry, true); // Adds Spoofy to ban list lol
        }

        private void retMessage(ref TmpEntry entry, EndianIO readerIO, EndianIO writerIO, EndianWriter structIO)
        {
            string message = "";
            string[] timeLeft = Misc.DateTimetoExact(entry.ClientTime, DateTime.Now);
            entry.CPUKey = Misc.BytesToHexString(readerIO.Reader.ReadBytes(0x10));

            if (!Globals.allowAnonUsers) {
                if (ConsoleMySQL.getConsole(ref entry)) { 
                    if (entry.ClientEnabled) { 
                        message = entry.daysLeft >= 500 ? 
                            String.Format("XBLRogers - Welcome {0}!\r\nYou have lifetime! Use it wisely!", entry.ClientName) :
                            String.Format("XBLRogers - Welcome {0}!\r\nTime Left: {1} Hour(s) - {2} Minute(s) [{3} Days Reserved]\0", entry.ClientName, timeLeft[1], timeLeft[2], entry.daysLeft); 
                    } 
                } else return;
            } else {
                if (AnonSQL.getConsoleAnon(ref entry)) { 
                    if (entry.ClientEnabled)
                        message = "XBLRogers Free\r\nEnjoy free mode while you can!";
                } else return;
            }
            if (entry.ClientName == "SeaSalad") { message = "Lol. Sala, you're a fucking faggot.\r\nEnjoy Mr. Shitty Britches!"; }
            tmpBuff = new byte[225];
            myCockBuff = new byte[(sizeof(uint) * 4) + 225];
            Buffer.BlockCopy(Encoding.ASCII.GetBytes(message), 0, tmpBuff, 0, message.Length);
            structIO = new EndianIO(myCockBuff, EndianStyle.BigEndian).Writer;
            structIO.Write(tmpBuff);
            writerIO.Writer.Write(myCockBuff);
        }

        private void HVC(ref TmpEntry entry, EndianIO readerIO, EndianIO writerIO)
        {
            entry.CPUKey = Misc.BytesToHexString(readerIO.Reader.ReadBytes(0x10));

            if (ConsoleMySQL.getConsole(ref entry)) {
                if (entry.ClientEnabled) {
                    //if (Globals.HVCBytes != null && Globals.HVBytes != null) writerIO.Writer.Write(Globals.XSTL_STATUS_OTHER_ERROR);
                    //else {
                        writerIO.Writer.Write(Globals.XSTL_STATUS_SUCCESS);
                        writerIO.Writer.Write(Globals.HVBytes.Length);
                        writerIO.Writer.Write(Globals.HVCBytes.Length);
                        writerIO.Writer.Write(Globals.HVBytes);
                        writerIO.Writer.Write(Globals.HVCBytes);
                        Globals.write("Client [{0}] HVC Successfully Sent! Name: {1}", IPAddr, entry.ClientName);
                        return;
                    //} return;
                } else { writerIO.Writer.Write(Globals.XSTL_STATUS_EXPIRED); Globals.write("Expired"); }
            } else { writerIO.Writer.Write(Globals.XSTL_STATUS_ERROR); Globals.write("Error"); }
            Globals.write("the fuck.");
        }

        private void patchData(ref TmpEntry entry, EndianIO readerIO, EndianIO writerIO)
        {
            entry.CPUKey = Misc.BytesToHexString(readerIO.Reader.ReadBytes(0x10));
            if (ConsoleMySQL.getConsole(ref entry))
            {
                if (entry.ClientEnabled)
                {
                    writerIO.Writer.Write(File.ReadAllBytes("bin\\xam_retail.bin")); Globals.write("Sent patch data successfully.");
                }
            }
        }
    }
}