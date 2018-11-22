namespace Stolen
{
    using Microsoft.Win32;
    using System;
    using System.IO;
    using System.IO.Compression;

    public static class Misc
    {
        public static IntPtr MainHandle;

        public static string BytesToHexString(byte[] Buffer)
        {
            string str = "";
            for (int i = 0; i < Buffer.Length; i++)
            {
                str = str + Buffer[i].ToString("X2");
            }
            return str;
        }

        //public static byte[] HexStringToByteArray

        public static bool CompareBytes(byte[] target1, byte[] target2)
        {
            if (target1.Length != target2.Length)
            {
                return false;
            }
            for (int i = 0; i < target1.Length; i++)
            {
                if (target1[i] != target2[i])
                {
                    return false;
                }
            }
            return true;
        }

        public static byte[] Compress(byte[] buffer)
        {
            MemoryStream stream = new MemoryStream();
            MemoryStream stream2 = new MemoryStream(buffer);
            DeflateStream stream3 = new DeflateStream(stream, CompressionMode.Compress);
            byte[] buffer2 = new byte[0x1000];
            int count = 0;
            while ((count = stream2.Read(buffer2, 0, buffer2.Length)) != 0)
            {
                stream3.Write(buffer2, 0, count);
            }
            stream3.Close();
            return stream.ToArray();
        }

        public static string ComputeElapsedTime(DateTime publishTime)
        {
            publishTime = publishTime.ToUniversalTime();
            TimeSpan span = DateTime.Now.ToUniversalTime().Subtract(publishTime);
            int totalSeconds = (int) span.TotalSeconds;
            int totalMinutes = (int) span.TotalMinutes;
            int totalHours = (int) span.TotalHours;
            int totalDays = (int) span.TotalDays;
            if (totalMinutes == 0)
            {
                return string.Format("{0} second(s) ago. ", totalSeconds);
            }
            if (totalHours == 0)
            {
                return string.Format("{0} minute(s) ago. ", totalMinutes);
            }
            if (totalDays == 0)
            {
                return string.Format("{0} hour(s) ago. ", totalHours);
            }
            if (totalDays <= 6)
            {
                return string.Format("{0} day(s) ago. ", totalDays);
            }
            if ((totalDays >= 7) && (totalDays < 30))
            {
                return string.Format("{0} week(s) ago.", totalDays / 7);
            }
            if ((totalDays >= 30) && (totalDays < 0x111c))
            {
                return string.Format("{0} month(s) ago. ", totalDays / 30);
            }
            if (totalDays >= 0x111c)
            {
                return string.Format("{0} year(s) ago. ", totalDays / 0x111c);
            }
            return "Error computing time.";
        }

        public static void CreateFolderRecursive(string Path)
        {
            string[] strArray = Path.Split(new string[] { @"\" }, StringSplitOptions.None);
            string path = "";
            foreach (string str2 in strArray)
            {
                path = path + str2 + @"\";
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }
            }
        }

        public static byte[] Decompress(byte[] buffer)
        {
            MemoryStream stream = new MemoryStream();
            MemoryStream stream2 = new MemoryStream(buffer);
            DeflateStream stream3 = new DeflateStream(stream2, CompressionMode.Decompress);
            byte[] buffer2 = new byte[0x1000];
            int count = 0;
            while ((count = stream3.Read(buffer2, 0, buffer2.Length)) != 0)
            {
                stream.Write(buffer2, 0, count);
            }
            return stream.ToArray();
        }

        public static string FormatSize(long FileSize)
        {
            if (FileSize >= Math.Pow(2.0, 80.0))
            {
                return string.Format("{0} YB", Math.Round((double) (((double) FileSize) / Math.Pow(2.0, 80.0)), 2));
            }
            if (FileSize >= Math.Pow(2.0, 70.0))
            {
                return string.Format("{0} ZB", Math.Round((double) (((double) FileSize) / Math.Pow(2.0, 70.0)), 2));
            }
            if (FileSize >= Math.Pow(2.0, 60.0))
            {
                return string.Format("{0} EB", Math.Round((double) (((double) FileSize) / Math.Pow(2.0, 60.0)), 2));
            }
            if (FileSize >= Math.Pow(2.0, 50.0))
            {
                return string.Format("{0} PB", Math.Round((double) (((double) FileSize) / Math.Pow(2.0, 50.0)), 2));
            }
            if (FileSize >= Math.Pow(2.0, 40.0))
            {
                return string.Format("{0} TB", Math.Round((double) (((double) FileSize) / Math.Pow(2.0, 40.0)), 2));
            }
            if (FileSize >= Math.Pow(2.0, 30.0))
            {
                return string.Format("{0} GB", Math.Round((double) (((double) FileSize) / Math.Pow(2.0, 30.0)), 2));
            }
            if (FileSize >= Math.Pow(2.0, 20.0))
            {
                return string.Format("{0} MB", Math.Round((double) (((double) FileSize) / Math.Pow(2.0, 20.0)), 2));
            }
            if (FileSize >= Math.Pow(2.0, 10.0))
            {
                return string.Format("{0} KB", Math.Round((double) (((double) FileSize) / Math.Pow(2.0, 10.0)), 2));
            }
            return string.Format("{0} Bytes", FileSize);
        }

        public static DateTime GetDateTime(byte[] dateTime)
        {
            return GetDateTime(dateTime, true);
        }

        public static DateTime GetDateTime(int dateTime)
        {
            if (dateTime == 0)
            {
                return DateTime.MinValue;
            }
            int second = (dateTime & 0x1f) << 1;
            int minute = (dateTime >> 5) & 0x3f;
            int hour = (dateTime >> 11) & 0x1f;
            int day = (dateTime >> 0x10) & 0x1f;
            int month = (dateTime >> 0x15) & 15;
            int year = ((dateTime >> 0x19) & 0x7f) + 0x7bc;
            try
            {
                DateTime time2 = new DateTime(year, month, day, hour, minute, second);
                return time2.ToLocalTime();
            }
            catch
            {
                return DateTime.MinValue;
            }
        }

        public static DateTime GetDateTime(byte[] dateTime, bool bigEndian)
        {
            return GetDateTime(GetInt32(dateTime, bigEndian));
        }

        public static long GetDirectorySize(string dir)
        {
            string[] files = Directory.GetFiles(dir, "*.*");
            string[] directories = Directory.GetDirectories(dir);
            long num = 0L;
            foreach (string str in files)
            {
                FileInfo info = new FileInfo(str);
                num += info.Length;
            }
            foreach (string str2 in directories)
            {
                num += GetDirectorySize(str2);
            }
            return num;
        }

        public static string GetFileTypeString(string theExtension)
        {
            RegistryKey classesRoot = Registry.ClassesRoot;
            RegistryKey key2 = classesRoot.OpenSubKey(theExtension);
            try
            {
                if (key2 != null)
                {
                    string name = key2.GetValue("").ToString();
                    return classesRoot.OpenSubKey(name).GetValue("").ToString();
                }
                return (theExtension + " File");
            }
            catch
            {
                return (theExtension + " File");
            }
        }

        public static int GetInt32(byte[] data)
        {
            return GetInt32(data, true);
        }

        public static int GetInt32(byte[] data, bool bigEndian)
        {
            if (bigEndian)
            {
                return ((((data[0] << 0x18) | (data[1] << 0x10)) | (data[2] << 8)) | data[3]);
            }
            return ((((data[3] << 0x18) | (data[2] << 0x10)) | (data[1] << 8)) | data[0]);
        }

        public static string GetTempPath()
        {
            string folderPath = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
            if (!Directory.Exists(folderPath + @"\Valhalla\"))
            {
                CreateFolderRecursive(folderPath + @"\Valhalla\");
            }
            return (folderPath + @"\Valhalla\");
        }

        public static byte[] HexStringToBytes(string HexString)
        {
            byte[] buffer = new byte[HexString.Length / 2];
            for (int i = 0; i < HexString.Length; i += 2)
            {
                try
                {
                    buffer[i / 2] = Convert.ToByte(HexString.Substring(i, 2), 0x10);
                }
                catch
                {
                    buffer[i / 2] = 0;
                }
            }
            return buffer;
        }

        public static bool IsEmpty(byte[] Buffer)
        {
            for (int i = 0; i < Buffer.Length; i++)
            {
                if (Buffer[i] != 0)
                {
                    return false;
                }
            }
            return true;
        }

        public static byte[] ReadAllBytes(Stream source)
        {
            int num2;
            byte[] buffer = new byte[0x1000];
            int offset = 0;
            while ((num2 = source.Read(buffer, offset, buffer.Length - offset)) > 0)
            {
                offset += num2;
                if (offset == buffer.Length)
                {
                    int num3 = source.ReadByte();
                    if (num3 != -1)
                    {
                        byte[] buffer2 = new byte[buffer.Length * 2];
                        Buffer.BlockCopy(buffer, 0, buffer2, 0, buffer.Length);
                        Buffer.SetByte(buffer2, offset, (byte) num3);
                        buffer = buffer2;
                        offset++;
                    }
                }
                byte[] dst = buffer;
                if (buffer.Length != offset)
                {
                    dst = new byte[offset];
                    Buffer.BlockCopy(buffer, 0, dst, 0, offset);
                }
                return dst;
            }
            return null;
        }

        public static byte[] SetDateTime(DateTime value)
        {
            return SetDateTime(value, true);
        }

        public static byte[] SetDateTime(DateTime value, bool bigEndian)
        {
            return SetInt32(SetDateTimeInt(value), bigEndian);
        }

        public static int SetDateTimeInt(DateTime dateTime)
        {
            dateTime = dateTime.ToUniversalTime();
            int second = dateTime.Second;
            int minute = dateTime.Minute;
            int hour = dateTime.Hour;
            int day = dateTime.Day;
            int month = dateTime.Month;
            int num6 = dateTime.Year - 0x7bc;
            second = second >> 1;
            return ((((((num6 << 0x19) | (month << 0x15)) | (day << 0x10)) | (hour << 11)) | (minute << 5)) | second);
        }

        public static byte[] SetInt32(int value)
        {
            return SetInt32(value, true);
        }

        public static byte[] SetInt32(int value, bool bigEndian)
        {
            if (bigEndian)
            {
                return new byte[] { ((byte) ((value >> 0x18) & 0xff)), ((byte) ((value >> 0x10) & 0xff)), ((byte) ((value >> 8) & 0xff)), ((byte) (value & 0xff)) };
            }
            return new byte[] { ((byte) (value & 0xff)), ((byte) ((value >> 8) & 0xff)), ((byte) ((value >> 0x10) & 0xff)), ((byte) ((value >> 0x18) & 0xff)) };
        }
    }
}

