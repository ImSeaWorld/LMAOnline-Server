using System;

namespace LMAOnline.Managers
{
    public static class BitHelper
    {
        public static byte[] ReadData(byte[] data, uint offset, uint length)
        {
            if (data.Length < length)
            {
                Console.WriteLine("Read data may have encountered an error");
            }
            byte[] destinationArray = new byte[length];
            Array.Copy(data, (long)offset, destinationArray, 0L, (long)length);
            return destinationArray;
        }

        public static short ReadInt16(byte[] data, uint offset)
        {
            byte[] array = new byte[2];
            for (uint i = 0; i < 2; i++)
            {
                array[i] = data[offset + i];
            }
            Array.Reverse(array);
            return BitConverter.ToInt16(array, 0);
        }

        public static int ReadInt32(byte[] data, uint offset)
        {
            byte[] array = new byte[4];
            for (uint i = 0; i < 4; i++)
            {
                array[i] = data[offset + i];
            }
            Array.Reverse(array);
            return BitConverter.ToInt32(array, 0);
        }

        public static long ReadInt64(byte[] data, uint offset)
        {
            byte[] array = new byte[8];
            for (uint i = 0; i < 8; i++)
            {
                array[i] = data[offset + i];
            }
            Array.Reverse(array);
            return BitConverter.ToInt64(array, 0);
        }

        public static ushort ReadUInt16(byte[] data, uint offset)
        {
            byte[] array = new byte[2];
            for (uint i = 0; i < 2; i++)
            {
                array[i] = data[offset + i];
            }
            Array.Reverse(array);
            return BitConverter.ToUInt16(array, 0);
        }

        public static uint ReadUInt32(byte[] data, uint offset)
        {
            byte[] array = new byte[4];
            for (uint i = 0; i < 4; i++)
            {
                array[i] = data[offset + i];
            }
            Array.Reverse(array);
            return BitConverter.ToUInt32(array, 0);
        }

        public static ulong ReadUInt64(byte[] data, uint offset)
        {
            byte[] array = new byte[8];
            for (uint i = 0; i < 8; i++)
            {
                array[i] = data[offset + i];
            }
            Array.Reverse(array);
            return BitConverter.ToUInt64(array, 0);
        }

        public static void WriteInt32(byte[] Data, uint Offset, int Value)
        {
            byte[] bytes = BitConverter.GetBytes(Value);
            Array.Reverse(bytes);
            for (int i = 0; i < 4; i++)
            {
                Data[(int)((IntPtr)(Offset + i))] = bytes[i];
            }
        }

        public static void WriteUInt16(byte[] Data, uint Offset, ushort Value)
        {
            byte[] bytes = BitConverter.GetBytes(Value);
            Array.Reverse(bytes);
            for (int i = 0; i < 2; i++)
            {
                Data[(int)((IntPtr)(Offset + i))] = bytes[i];
            }
        }

        public static void WriteUInt32(byte[] Data, uint Offset, uint Value)
        {
            byte[] bytes = BitConverter.GetBytes(Value);
            Array.Reverse(bytes);
            for (int i = 0; i < 4; i++)
            {
                Data[(int)((IntPtr)(Offset + i))] = bytes[i];
            }
        }

        public static void WriteUInt64(byte[] Data, uint Offset, ulong Value)
        {
            byte[] bytes = BitConverter.GetBytes(Value);
            Array.Reverse(bytes);
            for (int i = 0; i < 8; i++)
            {
                Data[(int)((IntPtr)(Offset + i))] = bytes[i];
            }
        }
    }
}

