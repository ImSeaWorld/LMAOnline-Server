using System;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;
using System.Threading;

namespace GSN.Managers
{
    public static class Security
    {
        private static ulong clearDoubleRight(ulong inputLong, int bits)
        {
            ulong num = 0L;
            for (int i = 0; i < bits; i++)
                num |= (ulong)1L << (0x3f - i);
            return (inputLong & num);
        }

        public static byte[] HMACSHA1(byte[] Data, byte[] Key, int Offset, int Length)
        {
            byte[] destinationArray = new byte[Data.Length];
            Array.Copy(Data, Offset, destinationArray, 0, Data.Length);
            byte[] array = new System.Security.Cryptography.HMACSHA1(Key).ComputeHash(destinationArray);
            Array.Resize<byte>(ref array, 0x10);
            return array;
        }

        private static ulong loadDouble(ref byte[] data, int address)
        {
            ulong num = 0L;
            for (int i = 0; i < 8; i++) {
                num = num << 8;
                num |= data[address + i];
            } return num;
        }

        private static uint loadWord(ref byte[] data, int address)
        {
            uint num = 0;
            for (int i = 0; i < 4; i++) {
                num = num << 8;
                num |= data[address + i];
            } return num;
        }

        public static byte[] RandomBytes(int Count)
        {
            Random random = new Random(0x2048);
            Random random2 = new Random(0x4096);
            Random random3 = new Random(0x8192);
            Random random4 = new Random(0xffff);
            byte[] buffer = new byte[Count];
            byte[] buffer2 = new byte[Count];
            byte[] buffer3 = new byte[Count];
            byte[] buffer4 = new byte[Count];
            byte[] buffer5 = new byte[Count];
            for (int i = 0; i < 10; i++)
            {
                random.NextBytes(buffer2);
                random2.NextBytes(buffer3);
                random3.NextBytes(buffer4);
                random4.NextBytes(buffer5);
                Thread.Sleep(1);
                random = new Random(Environment.TickCount + 0x1024);
                Thread.Sleep(1);
                random = new Random(Environment.TickCount + 0x2048);
                Thread.Sleep(1);
                random = new Random(Environment.TickCount + 0x4096);
                Thread.Sleep(1);
                random = new Random(Environment.TickCount + 0x8192);
                for (int j = 0; j < Count; j++)
                {
                    buffer[j] = (byte) (buffer[j] ^ buffer2[j]);
                    buffer[j] = (byte) (buffer[j] ^ buffer3[j]);
                    buffer[j] = (byte) (buffer[j] ^ buffer4[j]);
                    buffer[j] = (byte) (buffer[j] ^ buffer5[j]);
                    byte[] bytes = BitConverter.GetBytes(Environment.TickCount);
                    buffer[j] = (byte) (buffer[j] ^ bytes[0]);
                    buffer[j] = (byte) (buffer[j] ^ bytes[1]);
                    buffer[j] = (byte) (buffer[j] ^ bytes[2]);
                    buffer[j] = (byte) (buffer[j] ^ bytes[3]);
                    buffer[j] = (byte) (buffer[j] ^ bytes[3]);
                    buffer[j] = (byte) (buffer[j] ^ bytes[2]);
                    buffer[j] = (byte) (buffer[j] ^ bytes[1]);
                    buffer[j] = (byte) (buffer[j] ^ bytes[0]);
                }
            }
            return buffer;
        }

        public static void RC4(ref byte[] Data, byte[] Key)
        {
            byte num;
            int num2;
            byte[] buffer = new byte[0x100];
            byte[] buffer2 = new byte[0x100];
            for (num2 = 0; num2 < 0x100; num2++)
            {
                buffer[num2] = (byte) num2;
                buffer2[num2] = Key[num2 % Key.GetLength(0)];
            }
            int index = 0;
            for (num2 = 0; num2 < 0x100; num2++)
            {
                index = ((index + buffer[num2]) + buffer2[num2]) % 0x100;
                num = buffer[num2];
                buffer[num2] = buffer[index];
                buffer[index] = num;
            }
            num2 = index = 0;
            for (int i = 0; i < Data.GetLength(0); i++)
            {
                num2 = (num2 + 1) % 0x100;
                index = (index + buffer[num2]) % 0x100;
                num = buffer[num2];
                buffer[num2] = buffer[index];
                buffer[index] = num;
                int num5 = (buffer[num2] + buffer[index]) % 0x100;
                Data[i] = (byte) (Data[i] ^ buffer[num5]);
            }
        }

        public static void reverse(ref byte[] Key)
        {
            int num = 8;
            int num2 = Key.GetLength(0) / 2;
            for (int i = 1; i <= num2; i++)
            {
                int index = i - 1;
                int num5 = ((Key.GetLength(0) - ((index / num) * num)) - num) + (index % num);
                byte num6 = (byte) Key.GetValue(num5);
                Key.SetValue(RuntimeHelpers.GetObjectValue(Key.GetValue(index)), num5);
                Key.SetValue(num6, index);
            }
        }

        private static ulong rotateDoubleLeft(ulong inputLong, int rotate)
        {
            return ((inputLong << rotate) | (inputLong >> (0x40 - rotate)));
        }

        public static byte[] SHA1(byte[] Buffer)
        {
            return System.Security.Cryptography.SHA1.Create().ComputeHash(Buffer);
        }

        public static byte[] SignData(RSAParameters rsaParams, byte[] Data)
        {
            byte[] rgbHash = SHA1(Data);
            RSACryptoServiceProvider key = new RSACryptoServiceProvider();
            key.ImportParameters(rsaParams);
            RSAPKCS1SignatureFormatter formatter = new RSAPKCS1SignatureFormatter(key);
            formatter.SetHashAlgorithm("SHA1");
            byte[] array = formatter.CreateSignature(rgbHash);
            Array.Reverse(array);
            return array;
        }

        private static void storeDouble(ref byte[] data, int address, ulong inLong)
        {
            data[address + 7] = (byte) (inLong & ((ulong) 0xffL));
            data[address + 6] = (byte) ((inLong & 0xff00L) >> 8);
            data[address + 5] = (byte) ((inLong & 0xff0000L) >> 0x10);
            data[address + 4] = (byte) ((inLong & 0xff000000L) >> 0x18);
            data[address + 3] = (byte) ((inLong & 0xff00000000L) >> 0x20);
            data[address + 2] = (byte) ((inLong & 0xff0000000000L) >> 40);
            data[address + 1] = (byte) ((inLong & 0xff000000000000L) >> 0x30);
            data[address] = (byte)((inLong & 0xFF00000000000000L) >> 0x38);
        }

        public static bool VerifySignature(RSAParameters rsaParams, byte[] Signature, byte[] Data)
        {
            byte[] rgbHash = SHA1(Data);
            RSACryptoServiceProvider key = new RSACryptoServiceProvider();
            key.ImportParameters(rsaParams);
            RSAPKCS1SignatureDeformatter deformatter = new RSAPKCS1SignatureDeformatter(key);
            deformatter.SetHashAlgorithm("SHA1");
            Array.Reverse(Signature);
            bool flag = deformatter.VerifySignature(rgbHash, Signature);
            Array.Reverse(Signature);
            return flag;
        }

        public static byte[] XeCryptRotSum(byte[] r3, byte[] r4, int r5)
        {
            int address = 0;
            ulong inputLong = 0L;
            ulong inLong = loadDouble(ref r3, 0);
            ulong num4 = loadDouble(ref r3, 8);
            ulong num5 = loadDouble(ref r3, 0x10);
            ulong num6 = loadDouble(ref r3, 0x18);
            if (r5 != 0) {
                for (int i = r5; i > 0; i--) {
                    ulong num8 = loadDouble(ref r4, address);
                    inputLong = num8 + num4;
                    num4 = 1L;
                    if (inputLong >= num8) num4 = 0L;
                    num6 -= num8;
                    inLong = num4 + inLong;
                    num4 = rotateDoubleLeft(inputLong, 0x1d);
                    if (num6 <= num8) num8 = 0L;
                    else num8 = 1L;
                    num5 -= num8;
                    num6 = rotateDoubleLeft(num6, 0x1f);
                    address += 8;
                }
                storeDouble(ref r3, 0, inLong);
                storeDouble(ref r3, 8, num4);
                storeDouble(ref r3, 0x10, num5);
                storeDouble(ref r3, 0x18, num6);
            } return r3;
        }

        public static byte[] XeCryptRotSumSha(byte[] r3, int r4)
        {
            uint num = (uint) (r4 >> 3);
            byte[] inputBuffer = XeCryptRotSum(new byte[0x20], r3, (int) num);
            System.Security.Cryptography.SHA1 sha = new SHA1Managed();
            sha.TransformBlock(inputBuffer, 0, 0x20, null, 0);
            sha.TransformBlock(inputBuffer, 0, 0x20, null, 0);
            sha.TransformBlock(r3, 0, r4, null, 0);
            for (int i = 0; i < 0x20; i++)
                inputBuffer[i] = inputBuffer[i];
            sha.TransformBlock(inputBuffer, 0, 0x20, null, 0);
            sha.TransformFinalBlock(inputBuffer, 0, 0x20);
            return sha.Hash;
        }
    }
}

