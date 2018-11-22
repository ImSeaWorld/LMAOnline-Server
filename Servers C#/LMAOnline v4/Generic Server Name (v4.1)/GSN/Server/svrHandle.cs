using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.IO;
using System.Net;
using System.Threading;
using System.Diagnostics;
using System.Net.Sockets;
using GSN.Managers;
using System.Security.Cryptography;

namespace GSN.Server
{
    public class svrHandle
    {
        private Thread clientListener;
        private TcpListener listener = new TcpListener(IPAddress.Any, Globals.i_svrPort);
        // These need to be threadstatic due to thread overlapping.
        [ThreadStatic] private int buffCount = 0;
        [ThreadStatic] private string IPAddr;
        [ThreadStatic] private byte[] mainBuff, structBuff, tmpBuff;

        public svrHandle()
        {
            try {
                clientListener = new Thread(new ThreadStart(listenForClients));
                clientListener.Start();
                Globals.b_serverRunning = true;
            } catch { GlobalFunc.Write("Failed to start the listen thread!"); GlobalFunc.DelayedRestart(3); }
        }

        private void listenForClients()
        {
            try {
                this.listener.Start();
                while (true) {
                    Thread.Sleep(100);
                    conMySQL.updateEnabledConsoles();
                    if (!Globals.b_serverRunning) return;
                    if (this.listener.Pending()) new Thread(new ParameterizedThreadStart(clientHandle)).Start(listener.AcceptTcpClient());
                }
            } catch (Exception ex) { Globals.b_serverRunning = false; Console.Clear(); GlobalFunc.Write("Error listenForClients! Reason: {0}", ex.Message); GlobalFunc.DelayedRestart(3); }
        }

        private void clientHandle(object clientObj)
        {
            TcpClient Client = (TcpClient)clientObj;
            TmpEntry entry = new TmpEntry();
            NetworkStream nStream = Client.GetStream();
            PoorManStream pmStream = new PoorManStream(nStream);
            IPAddr = Client.Client.RemoteEndPoint.ToString();

            //try {
                GlobalFunc.Write("{0} [{1}] PING", "Client", IPAddr);
                entry.client_ip = IPAddr.Split(':')[0];
                mainBuff = new byte[8];
                structBuff = new byte[0x10];
                if (nStream.Read(mainBuff, 0, 8) != 8) { Client.Close(); GlobalFunc.Write("Client [{0}] Main Buffer Overflow", IPAddr); return; }
                EndianIO mainIO = new EndianIO(mainBuff, EndianStyle.BigEndian);
                uint CommandID = mainIO.Reader.ReadUInt32();
                buffCount = mainIO.Reader.ReadInt32();
                if (buffCount >= 0x8000) { Client.Close(); GlobalFunc.Write("Client [{0}] Main Buffer Overflow 0x8000 - Ret 0x{1}", IPAddr, buffCount.ToString("X")); return; }
                mainBuff = new byte[buffCount];
                EndianIO writerIO = new EndianIO(pmStream, EndianStyle.BigEndian);
                EndianIO readerIO = new EndianIO(mainBuff, EndianStyle.BigEndian) { Writer = new EndianWriter(pmStream, EndianStyle.BigEndian) };
                EndianWriter structIO = new EndianIO(structBuff, EndianStyle.BigEndian).Writer;

                if (pmStream.Read(mainBuff, 0, buffCount) != buffCount) { Client.Close(); GlobalFunc.Write("Client [{0}] Unexpected Buffer Size", IPAddr); return; }

                GlobalFunc.Write("COMMAND ID: {0}", Enum.GetName(typeof(cmdCode), Convert.ToUInt32(CommandID)));

                switch (Convert.ToUInt32(CommandID)) {
                    case (uint)cmdCode.GET_SESSION:
                        GET_SESSION(ref entry, readerIO, writerIO);
                        break;
                    case (uint)cmdCode.GET_STATUS:
                        GET_STATUS(ref entry, readerIO, writerIO);
                        break;
                    case (uint)cmdCode.GET_CHAL_RESPONSE:
                        GET_CHAL_RESPONSE(ref entry, readerIO, writerIO);
                        break;
                    case (uint)cmdCode.UPDATE_PRESENCE:
                        UPDATE_PRESENCE(ref entry, readerIO, writerIO);
                        break;
                    case (uint)cmdCode.GET_XOSC:
                        GET_XOSC(ref entry, readerIO, writerIO);
                        break;
                    case (uint)cmdCode.GET_INFO:
                        break;
                    case (uint)cmdCode.SND_SPOOFY: // No need right now
                        break;
                    case (uint)cmdCode.GET_MESSAGE:
                        GET_MESSAGE(ref entry, readerIO, writerIO, structIO);
                        break;
                    case (uint)cmdCode.GET_PATCHES:
                        GET_PATCH_DATA(ref entry, readerIO, writerIO);
                        break;
                    case (uint)cmdCode.GET_GUIDE_INFO:
                        GET_GUIDE_INFO(ref entry, readerIO, writerIO, structIO);
                        break;
                    default:
                        GlobalFunc.Write("Client [{0}] INVALID COMMAND CODE! Client Closed.", IPAddr);
                        Client.Close();
                        break;
                }
                writerIO.Close();
                readerIO.Close();
                structIO.Close();
            //} catch (Exception ex) {
            //    if (Globals.b_printCrash)
            //        GlobalFunc.Write("Client [{0}] Client Crash! Closed Connection. Detailed: {1} || {2}", IPAddr, ex.Source, ex.Message);
            //    Client.Close();
            //}
        }

        private void GET_SESSION(ref TmpEntry entry, EndianIO readerIO, EndianIO writerIO)
        {
            bool conType = Convert.ToBoolean(readerIO.Reader.ReadInt32());
            entry.client_cpukey = readerIO.Reader.ReadBytes(0x10).BytesToHexString();

            if (conMySQL.getConsole(ref entry)) {
                entry.client_ip = IPAddr;
                entry.client_session = Security.RandomBytes(0x10).BytesToHexString();
                conMySQL.setConsole(ref entry);
                GlobalFunc.Write(entry.client_session);

                //if (entry.client_daysUsed >= 1 && entry.client_ip != entry.client_db_curIP) {
                //    entry.client_db_lastIP = entry.client_db_curIP;
                //    conMySQL.addNewIP(ref entry);
                //} else if (entry.client_db_curIP == "")
                //    conMySQL.addNewIP(ref entry);

                if (entry.client_banned) {
                    GlobalFunc.Write("Client [{0}] Banned Client Connected\r\n          - Name: {1}\r\n          - CPUkey: {2}", IPAddr, entry.client_name, entry.client_cpukey);
                    writerIO.Writer.Write((uint)respCode.RESP_STATUS_BANNED);
                    conMySQL.setConsole(ref entry);
                    return;
                }

                if (entry.client_enabled) {
                    GlobalFunc.Write("Client [{0}] Authorized Client's {1}\r\n          - Name: {2}\r\n          - CPUkey: {3}", IPAddr, conType ? "Devkit" : "Jtag/RGH", entry.client_name, entry.client_cpukey);
                    writerIO.Writer.Write((uint)respCode.RESP_STATUS_STEALTHED);
                    writerIO.Writer.Write(Misc.HexStringToBytes(entry.client_session));
                } else { 
                    if (conMySQL.autoUpdateTime(ref entry)) {
                        //if (entry.client_days <= 100) {
                            GlobalFunc.Write("Client [{0}] Day Incrimented On Time[24 Hr Timer]\r\n          - Name: {1}\r\n          - CPUKey: {2}", IPAddr, entry.client_name, entry.client_cpukey);
                            writerIO.Writer.Write((uint)respCode.RESP_STATUS_DAY_STARTED);
                        //} else writerIO.Writer.Write((uint)respCode.RESP_STATUS_STEALTHED);
                        writerIO.Writer.Write(Misc.HexStringToBytes(entry.client_session));
                    } else {
                        GlobalFunc.Write("Client [{0}] Time Expired!\r\n          - Name: {1}\r\n          - CPUKey: {2}", IPAddr, entry.client_name, entry.client_cpukey);
                        writerIO.Writer.Write((uint)respCode.RESP_STATUS_EXPIRED);
                    }
                } conMySQL.setConsole(ref entry);
            } else {
                GlobalFunc.Write("Client [{0}] Failed! CPUKey - {1}", IPAddr, entry.client_cpukey);
                writerIO.Writer.Write((uint)respCode.RESP_STATUS_EXPIRED);
            }
        }

        private void GET_STATUS(ref TmpEntry entry, EndianIO readerIO, EndianIO writerIO)
        {
            entry.client_session = readerIO.Reader.ReadBytes(0x10).BytesToHexString();
            entry.client_xexHash = readerIO.Reader.ReadBytes(0x10);

            GlobalFunc.Write("Session Rec: {0}", entry.client_session);
            if (conMySQL.getConsole(ref entry, true)) {
                if (!Globals.b_xexChecks) {
                    GlobalFunc.Write("Client [{0}] Hash Checks Disabled!", IPAddr);
                    writerIO.Writer.Write((uint)respCode.RESP_STATUS_SUCCESS);
                }

                if (!entry.client_enabled) {
                    GlobalFunc.Write("Client [{0}] Time Expired! Haulted Checks\r\n          - Name: {1}\r\n          - CPUKey: {2}", IPAddr, entry.client_name, entry.client_cpukey);
                    writerIO.Writer.Write((uint)respCode.RESP_STATUS_EXPIRED);
                    return;
                }

                if (entry.client_xexChecks) {
                    if (Misc.CompareBytes(entry.client_xexHash, Security.HMACSHA1(Globals.by_xexBytes, Misc.HexStringToBytes(entry.client_session), 0, 16))) {
                        GlobalFunc.Write("Client [{0}] Passed Hash Checks!\r\n          - Name: {1}\r\n          - CPUKey: {2}", IPAddr, entry.client_name, entry.client_cpukey);
                        writerIO.Writer.Write((uint)respCode.RESP_STATUS_SUCCESS);
                    } else {
                        GlobalFunc.Write("Client [{0}] Failed Hash Checks! Sending Update...\r\n          - Name: {1}\r\n          - CPUKey: {2}", IPAddr, entry.client_name, entry.client_cpukey);
                        writerIO.Writer.Write((uint)respCode.RESP_STATUS_UPDATE);
                        writerIO.Writer.Write(Globals.by_xexBytes.Length);
                        writerIO.Writer.Write(Globals.by_xexBytes);
                    }
                } else {
                    GlobalFunc.Write("Client [{0}] Administrator Omitted Checks For:\r\n          - Name: {1}\r\n          - CPUKey: {2}", IPAddr, entry.client_name, entry.client_cpukey);
                    writerIO.Writer.Write((uint)respCode.RESP_STATUS_SUCCESS);
                }
            } else {
                GlobalFunc.Write("Failed to authorize IP: {0}", IPAddr);
                writerIO.Writer.Write((uint)respCode.RESP_STATUS_ERROR);
            }
        }

        private void GET_CHAL_RESPONSE(ref TmpEntry entry, EndianIO readerIO, EndianIO writerIO)
        {
            SHA1CryptoServiceProvider sha1 = new SHA1CryptoServiceProvider();
            bool Crl = false, Fcrt = false, Type1kv = false;
            uint[] chalVars = { 0x23289D3/*Num 9*/, 0xD83E/*Num10*/, 0x304000D/*Num11*/, 0x1F8FC0D/*num11-this = (type1KV)*/ };
            entry.client_session = readerIO.Reader.ReadBytes(0x10).BytesToHexString();
            entry.client_salt = readerIO.Reader.ReadBytes(0x10).BytesToHexString();
            entry.client_ecc_salt = readerIO.Reader.ReadBytes(0x2);

            GlobalFunc.Write("ECC Salt: {0}", entry.client_ecc_salt.BytesToHexString());

            if (buffCount > 0x20) {
                Crl = readerIO.Reader.ReadBoolean();
                Fcrt = readerIO.Reader.ReadBoolean();
                Type1kv = readerIO.Reader.ReadBoolean();
            }

            if (conMySQL.getConsole(ref entry, true)) {
                if (entry.client_enabled) {
                    EndianIO seekIO = new EndianIO(Globals.by_chalBytes, EndianStyle.BigEndian);
                    seekIO.Stream.Seek(0x2EL, SeekOrigin.Begin);
                    seekIO.Writer.Write((ushort)(Type1kv ? (chalVars[1] & -33) : chalVars[1]));
                    seekIO.Stream.Seek(0x38L, SeekOrigin.Begin);
                    seekIO.Writer.Write((Crl ? ((Fcrt ? (uint)0x1000000 : 0x10000) | chalVars[0]) : chalVars[0]));
                    seekIO.Writer.Write((Type1kv ? chalVars[2] - chalVars[3] : chalVars[2]));
                    //seekIO.Stream.Seek(0x50, SeekOrigin.Begin);
                    //seekIO.Writer.Write(hashECC(entry.client_ecc_salt));
                    seekIO.Stream.Seek(0xEC, SeekOrigin.Begin);
                    // HV Salt Begin
                    sha1.TransformBlock(Misc.HexStringToBytes(entry.client_salt), 0, 0x10, null, 0);
                    sha1.TransformBlock(Globals.by_hvBytes, 0x34, 0x40, null, 0);
                    sha1.TransformBlock(Globals.by_hvBytes, 0x78, 0xf88, null, 0);
                    sha1.TransformBlock(Globals.by_hvBytes, 0x100c0, 0x40, null, 0);
                    sha1.TransformBlock(Globals.by_hvBytes, 0x10350, 0xDF0, null, 0);
                    sha1.TransformBlock(Globals.by_hvBytes, 0x16D20, 0x2E0, null, 0);
                    sha1.TransformBlock(Globals.by_hvBytes, 0x20000, 0xffc, null, 0);
                    sha1.TransformFinalBlock(Globals.by_hvBytes, 0x30000, 0xffc);
                    entry.client_hvSalt = sha1.Hash;
                    // HV Salt End
                    seekIO.Writer.Write(entry.client_hvSalt);
                    seekIO.Writer.Flush();
                    seekIO.Close();
                    // End SeekIO
                    buffCount = 256;
                    mainBuff = new byte[buffCount];
                    Buffer.BlockCopy(Globals.by_chalBytes, 0x20, mainBuff, 0x20, 0xE0);//Padding
                    EndianIO statusIO = new EndianIO(mainBuff, EndianStyle.BigEndian);
                    statusIO.Writer.Write((uint)respCode.RESP_STATUS_SUCCESS);
                    writerIO.Writer.Write(mainBuff);
                    File.WriteAllBytes("dmp\\resp.dmp", mainBuff);
                    GlobalFunc.Write("Client [{0}] HVSalt Sent\r\n          - Name: {1}\r\n          - CPUKey: {2}", IPAddr, entry.client_name, entry.client_cpukey);
                } else writerIO.Writer.Write((uint)respCode.RESP_STATUS_EXPIRED);
            } else writerIO.Writer.Write((uint)respCode.RESP_STATUS_ERROR);
        }

        private byte[] hashECC(byte[] salt)
        {
            byte[] cache = File.ReadAllBytes("bin/cache.bin");
            Random rnd = new Random();
            rnd.NextBytes(salt);
            SHA1Managed hash = new SHA1Managed();
            hash.TransformBlock(salt, 0, 0x2, null, 0);
            hash.TransformBlock(Globals.by_hvBytes, 0x34, 0xC, null, 0);
            hash.TransformBlock(Globals.by_hvBytes, 0x40, 0x30, null, 0);
            hash.TransformBlock(Globals.by_hvBytes, 0x70, 0x4, null, 0);
            hash.TransformBlock(Globals.by_hvBytes, 0x78, 0x8, null, 0);
            hash.TransformBlock(cache, 0x2, 0x3FE, null, 0);
            hash.TransformBlock(Globals.by_hvBytes, 0x100C0, 0x40, null, 0);
            hash.TransformBlock(Globals.by_hvBytes, 0x10350, 0x30, null, 0);
            hash.TransformBlock(Globals.by_hvBytes, 0x40E, 0x176, null, 0);
            hash.TransformBlock(Globals.by_hvBytes, 0x16100, 0x40, null, 0);
            hash.TransformBlock(Globals.by_hvBytes, 0x16D20, 0x60, null, 0);
            hash.TransformBlock(cache, 0x5B6, 0x24A, null, 0);
            hash.TransformBlock(cache, 0x800, 0x400, null, 0);
            hash.TransformFinalBlock(cache, 0xC00, 0x400);
            return new HMACSHA1(salt).ComputeHash(hash.Hash);
        }

        private void UPDATE_PRESENCE(ref TmpEntry entry, EndianIO readerIO, EndianIO writerIO)
        {
            entry.client_session = readerIO.Reader.ReadBytes(0x10).BytesToHexString();
            entry.client_title = readerIO.Reader.ReadBytes(0x4).BytesToHexString();
            entry.client_gt = GlobalFunc.ConvertBytesToString(readerIO.Reader.ReadBytes(0x15));

            if (conMySQL.getConsole(ref entry, true)) {
                if (entry.client_enabled) { writerIO.Writer.Write((uint)respCode.RESP_STATUS_SUCCESS); }
                else { writerIO.Writer.Write(conMySQL.autoUpdateTime(ref entry) ? (uint)respCode.RESP_STATUS_DAY_STARTED : (uint)respCode.RESP_STATUS_EXPIRED); }
                conMySQL.setConsole(ref entry);
                return;
            } else writerIO.Writer.Write((uint)respCode.RESP_STATUS_ERROR);
        }

        private void GET_XOSC(ref TmpEntry entry, EndianIO readerIO, EndianIO writerIO)
        {
            SHA1CryptoServiceProvider sha1 = new SHA1CryptoServiceProvider();
            bool Crl = false, Fcrt = false, Type1kv = false; long HVProtectedFlags;
            uint[] XOSCVars = { 0x23289D3/*Num9*/, 0xD83E/*Num10*/, 0x2BF/*Num15*/ };
            // Temp Vars
            byte[] buff1 = null, buff2 = null, buff3 = null, inputBuffer = null, dest = null, XOSCDmp = null;
            byte buff4; ushort num1, num2;
            // Temp Vars End
            entry.client_session = readerIO.Reader.ReadBytes(0x10).BytesToHexString();
            entry.client_execuResult = readerIO.Reader.ReadUInt32();
            entry.client_excuID = readerIO.Reader.ReadBytes(0x18);
            HVProtectedFlags = readerIO.Reader.ReadInt64();
            Crl = readerIO.Reader.ReadBoolean();
            Fcrt = readerIO.Reader.ReadBoolean();
            Type1kv = readerIO.Reader.ReadBoolean();

            if (conMySQL.getConsole(ref entry, true)) {
                if (entry.client_enabled) {
                    buffCount = 16384; // 0x4000
                    mainBuff = new byte[buffCount];
                    Buffer.BlockCopy(entry.client_kv, 0, mainBuff, 0, buffCount);
                    inputBuffer = Security.SHA1(Misc.HexStringToBytes(entry.client_cpukey));
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
                    sha1.TransformBlock(entry.client_kv, 0x1C, 0xD4, null, 0);
                    sha1.TransformBlock(entry.client_kv, 0x100, 0x1CF8, null, 0);
                    sha1.TransformFinalBlock(entry.client_kv, 0x1EF8, 0x2108);
                    Buffer.BlockCopy(sha1.Hash, 0, dest, 0, 0x10);
                    buffCount = 736; // 2E0
                    mainBuff = new byte[buffCount];
                    writerIO.Writer.Write((uint)0);
                    writerIO.Writer.Write((uint)0x90002);
                    writerIO.Writer.Write((ulong)XOSCVars[2]);
                    writerIO.Writer.Write((ulong)0L);
                    writerIO.Writer.Write(entry.client_execuResult);
                    writerIO.Writer.Write(Misc.HexStringToBytes("00000000C8003003AAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAA00000000"));
                    writerIO.Writer.Write(entry.client_execuResult == 0 ? entry.client_excuID : Misc.HexStringToBytes("AAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAA"));
                    writerIO.Writer.Write(Misc.HexStringToBytes("2121212121212121212121212121212121212121212121212121212121212121527A5A4BD8F505BB94305A1779729F3B000000"));
                    writerIO.Writer.Write(buff4);
                    writerIO.Writer.Write(Misc.HexStringToBytes(entry.client_execuResult == 0 ? "0000000000000000" : "AAAAAAAAAAAAAAAA"));
                    writerIO.Writer.Write(Misc.HexStringToBytes("AAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAA"));
                    writerIO.Writer.Write(buff1);
                    writerIO.Writer.Write(buff1);
                    writerIO.Writer.Write(buff3);
                    writerIO.Writer.Write((ushort)170);
                    writerIO.Writer.Write(Type1kv ? ((ushort)(XOSCVars[1] & -33)) : XOSCVars[1]);
                    writerIO.Writer.Write(num1);
                    writerIO.Writer.Write(num2);
                    writerIO.Writer.Write(Misc.HexStringToBytes("000000000000000000070000"));
                    writerIO.Writer.Write((Crl ? (XOSCVars[0] | (Fcrt ? (uint)0x1000000 : 0x10000)) : XOSCVars[0]));
                    writerIO.Writer.Write(Misc.HexStringToBytes("AAAAAAAA000000000000000000000000AAAAAAAA000D0008000000080000000000000000000000000000000000000000000000000000000000000000"));
                    writerIO.Writer.Write((4 | (((HVProtectedFlags & 1) == 1) ? 1 : 0)));
                    writerIO.Writer.Write(buff2);
                    writerIO.Writer.Write(Misc.HexStringToBytes("0000000000000000000000000000000000000000000000000000000000000000000000000000000000000040000207000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000AAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAA0000000000000000000000000000000000200000000000000000000000000006AAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAA5F534750AAAAAAAA"));
                    File.WriteAllBytes("dmp\\XOSC.dmp", XOSCDmp);
                    if (writerIO.Position != 0x2E0L)
                        GlobalFunc.Write("Client [{0}] Failed XOSC Dump! - CPUKey: {1}", IPAddr, entry.client_cpukey);
                    writerIO.Writer.Write(XOSCDmp);
                }
                else writerIO.Writer.Write((uint)respCode.RESP_STATUS_EXPIRED);
            }
            else writerIO.Writer.Write((uint)respCode.RESP_STATUS_ERROR);
        }

        private void GET_MESSAGE(ref TmpEntry entry, EndianIO readerIO, EndianIO writerIO, EndianWriter structIO)
        {
            string message = "";
            //string[] timeLeft = Misc.DateTimetoExact(DateTime.Now, entry.client_dateExpire.ToLocalTime());
            TimeSpan timeLeft = Misc.DateTimetoExact(DateTime.Now, entry.client_dateExpire.ToLocalTime());
            entry.client_cpukey = readerIO.Reader.ReadBytes(0x10).BytesToHexString();

            if (Globals.b_allowAnonymousUsers) return;
            else {
                if (conMySQL.getConsole(ref entry)) {
                    if (entry.client_dateExpire >= DateTime.Now)
                        message = entry.client_days >= 99999 ?
                            String.Format("LMAOnline - Welcome {0}!\r\nYou have lifetime! Use it wisely!\0", entry.client_name) :
                            String.Format("LMAOnline - Welcome {0}!\r\nTime Left: {1} Hour(s) - {2} Minute(s) [{3} Days Reserved]\0", entry.client_name, timeLeft.Hours, timeLeft.Minutes, entry.client_days);
                } else return;
            }

            tmpBuff = new byte[255];
            structBuff = new byte[(sizeof(uint) * 4) + 255];
            Buffer.BlockCopy(Encoding.ASCII.GetBytes(message), 0, tmpBuff, 0, message.Length);
            structIO = new EndianIO(structBuff, EndianStyle.BigEndian).Writer;
            structIO.Write(tmpBuff);
            writerIO.Writer.Write(structBuff);
        }

        private void GET_PATCH_DATA(ref TmpEntry entry, EndianIO readerIO, EndianIO writerIO)
        {
            Random rnd = new Random();
            entry.client_cpukey = readerIO.Reader.ReadBytes(0x10).BytesToHexString();
            string TitleID = readerIO.Reader.ReadBytes(0x4).BytesToHexString();
            bool isDevkit = Convert.ToBoolean(readerIO.Reader.ReadInt32());

            if (conMySQL.getConsole(ref entry))
            {
                GlobalFunc.Write("Title ID: 0x{0}", TitleID);
                GlobalFunc.Write("isDevKit: {0}", isDevkit);
                GlobalFunc.Write("CPUKey: {0}", entry.client_cpukey);

                writerIO.Writer.Write((uint)patchesCode.STATUS_GOOD);

                int obOrdinal = rnd.Next(0, 8);
                byte[] encData = xorBlock(Globals.by_xamBytes, 0, Globals.by_xamBytes.Length, Misc.HexStringToBytes(entry.client_session)[obOrdinal]);
                writerIO.Writer.Write(obOrdinal);
                writerIO.Writer.Write(encData.Length);
                writerIO.Writer.Write(encData);
            }
        }

        private void GET_GUIDE_INFO(ref TmpEntry entry, EndianIO readerIO, EndianIO writerIO, EndianWriter structIO)
        {
            entry.client_session = readerIO.Reader.ReadBytes(0x10).BytesToHexString();

            if (conMySQL.getConsole(ref entry, true)) {
                if (entry.client_dateExpire >= DateTime.Now) {
                    GlobalFunc.Write(entry.client_name);
                    TimeSpan timeLeft = Misc.DateTimetoExact(entry.client_dateExpire.ToLocalTime(), DateTime.Now.ToLocalTime());
                    //string s_timeLeft = "fgt\0";//entry.client_days >= 500 ? "Life in prison.\0" : String.Format("D:{0} H:{1} M:{2}\0", entry.client_days, timeLeft.Hours, timeLeft.Minutes);//String.Format("Days {0} | Hours {1} | Minutes {2} | Seconds {3}\0", entry.client_days, timeLeft.Hours, timeLeft.Minutes, timeLeft.Seconds);

                    int maxStatusLen = 20, maxTimeLen = 50;
                    byte[] StatusBuff = new byte[maxStatusLen];
                    byte[] timeBuff = new byte[maxTimeLen];
                    //byte[] serialBuff = new byte[maxSerialLen];

                    string Status = "Authenticated\0";
                    string s_timeLeft = entry.client_days >= 99999 ? "Life in prison.\0" : String.Format("D:{0} H:{1} M:{2}\0", entry.client_days, timeLeft.Hours, timeLeft.Minutes);

                    Buffer.BlockCopy(Encoding.ASCII.GetBytes(Status), 0, StatusBuff, 0, Status.Length);
                    Buffer.BlockCopy(Encoding.ASCII.GetBytes(s_timeLeft), 0, timeBuff, 0, s_timeLeft.Length);

                    structBuff = new byte[(sizeof(uint) * 4) + StatusBuff.Length + timeBuff.Length];
                    structIO = new EndianIO(structBuff, EndianStyle.BigEndian).Writer;
                    structIO.Write(StatusBuff);
                    structIO.Write(timeBuff);
                    writerIO.Writer.Write(structBuff);
                    GlobalFunc.Write("GET_GUIDE_INFO: Sent");
                }
            }
        }

        public static byte[] xorBlock(byte[] buffer, int startIndex, int size, byte xorValue)
        {
            byte[] tmp = new byte[buffer.Length];
            Buffer.BlockCopy(buffer, 0x00, tmp, 0x00, buffer.Length);
            for (int i = startIndex; i < startIndex + size; i++) {
                tmp[i] ^= xorValue;
            } return tmp;
        }
    }
}
