using System;
using System.IO;

using LMAOnline.Managers;

namespace LMAOnline
{
    internal class PoorManStream : Stream
    {
        private byte[] shitkey = new byte[] { 0x75, 0x5b, 0xd7, 12, 0x34, 3, 0xc7, 90, 0xc1, 0xd5, 0x52, 0xd6, 0x54, 0x6f, 6, 0xc7 };
        public Stream stream;

        public PoorManStream(Stream s)
        {
            this.stream = s;
        }

        public override void Flush()
        {
            this.stream.Flush();
        }

        public override int Read(byte[] buffer, int offset, int count)
        {
            byte[] buffer2 = new byte[count];
            int num = this.stream.Read(buffer2, 0, count);
            while (num != count)
            {
                int num2 = this.stream.Read(buffer2, num, count - num);
                if (num2 <= 0) break;
                num += num2;
            }
            Security.RC4(ref buffer2, this.shitkey);
            Buffer.BlockCopy(buffer2, 0, buffer, offset, num);
            return num;
        }

        public override long Seek(long offset, SeekOrigin origin)
        {
            return this.stream.Seek(offset, origin);
        }

        public override void SetLength(long value)
        {
            this.stream.SetLength(value);
        }

        public override void Write(byte[] buffer, int offset, int count)
        {
            byte[] dst = new byte[count];
            Buffer.BlockCopy(buffer, offset, dst, 0, count);
            Security.RC4(ref dst, this.shitkey);
            this.stream.Write(dst, 0, count);
        }

        public override bool CanRead
        {
            get { return this.stream.CanRead; }
        }

        public override bool CanSeek
        {
            get { return this.stream.CanSeek; }
        }

        public override bool CanWrite
        {
            get { return this.stream.CanWrite; }
        }

        public override long Length
        {
            get { return this.stream.Length; }
        }

        public override long Position
        {
            get { return this.stream.Position; }
            set { this.stream.Position = this.Position; }
        }
    }
}

