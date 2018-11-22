using System;
using System.IO;
using System.Text;

namespace LMAOnline.Managers
{
   public class EndianWriter : BinaryWriter
    {
        private readonly EndianStyle _endianStyle;

        public EndianWriter(Stream Stream, EndianStyle EndianStyle) : base(Stream)
        {
            this._endianStyle = EndianStyle;
        }

        public void Seek(long position)
        {
            base.BaseStream.Position = position;
        }

        public void SeekNWrite(long position, int Value)
        {
            base.BaseStream.Position = position;
            base.Write(Value);
        }

        public override void Write(double value)
        {
            this.Write(value, this._endianStyle);
        }

        public override void Write(short value)
        {
            this.Write(value, this._endianStyle);
        }

        public override void Write(int value)
        {
            this.Write(value, this._endianStyle);
        }

        public override void Write(long value)
        {
            this.Write(value, this._endianStyle);
        }

        public override void Write(float value)
        {
            this.Write(value, this._endianStyle);
        }

        public override void Write(ushort value)
        {
            this.Write(value, this._endianStyle);
        }

        public override void Write(uint value)
        {
            this.Write(value, this._endianStyle);
        }

        public override void Write(ulong value)
        {
            this.Write(value, this._endianStyle);
        }

        public void Write(double value, EndianStyle EndianStyle)
        {
            byte[] bytes = BitConverter.GetBytes(value);
            if (EndianStyle == EndianStyle.BigEndian)
            {
                Array.Reverse(bytes);
            }
            base.Write(bytes);
        }

        public void Write(short value, EndianStyle EndianStyle)
        {
            byte[] bytes = BitConverter.GetBytes(value);
            if (EndianStyle == EndianStyle.BigEndian)
            {
                Array.Reverse(bytes);
            }
            base.Write(bytes);
        }

        public void Write(int value, EndianStyle EndianStyle)
        {
            byte[] bytes = BitConverter.GetBytes(value);
            if (EndianStyle == EndianStyle.BigEndian)
            {
                Array.Reverse(bytes);
            }
            base.Write(bytes);
        }

        public void Write(long value, EndianStyle EndianStyle)
        {
            byte[] bytes = BitConverter.GetBytes(value);
            if (EndianStyle == EndianStyle.BigEndian)
            {
                Array.Reverse(bytes);
            }
            base.Write(bytes);
        }

        public void Write(float value, EndianStyle EndianStyle)
        {
            byte[] bytes = BitConverter.GetBytes(value);
            if (EndianStyle == EndianStyle.BigEndian)
            {
                Array.Reverse(bytes);
            }
            base.Write(bytes);
        }

        public void Write(ushort value, EndianStyle EndianStyle)
        {
            byte[] bytes = BitConverter.GetBytes(value);
            if (EndianStyle == EndianStyle.BigEndian)
            {
                Array.Reverse(bytes);
            }
            base.Write(bytes);
        }

        public void Write(uint value, EndianStyle EndianStyle)
        {
            byte[] bytes = BitConverter.GetBytes(value);
            if (EndianStyle == EndianStyle.BigEndian)
            {
                Array.Reverse(bytes);
            }
            base.Write(bytes);
        }

        public void Write(ulong value, EndianStyle EndianStyle)
        {
            byte[] bytes = BitConverter.GetBytes(value);
            if (EndianStyle == EndianStyle.BigEndian)
            {
                Array.Reverse(bytes);
            }
            base.Write(bytes);
        }

        public void WriteInt24(int value)
        {
            this.WriteInt24(value, this._endianStyle);
        }

        public void WriteInt24(int value, EndianStyle EndianStyle)
        {
            byte[] bytes = BitConverter.GetBytes(value);
            Array.Resize<byte>(ref bytes, 3);
            if (EndianStyle == EndianStyle.BigEndian)
            {
                Array.Reverse(bytes);
            }
            base.Write(bytes);
        }

        public void WriteString(string value)
        {
            char[] chars = value.ToCharArray();
            base.Write(chars);
        }

        public void WriteUnicodeString(string Value)
        {
            byte[] bytes = Encoding.BigEndianUnicode.GetBytes(Value);
            base.Write(bytes);
            base.Write(new byte[2]);
        }

        public void WriteUnicodeString(string String, int length)
        {
            this.WriteUnicodeString(String, length, this._endianStyle);
        }

        public void WriteUnicodeString(string String, int length, EndianStyle endianStyle)
        {
            int num = String.Length;
            for (int i = 0; i < num; i++)
            {
                if (i > length)
                {
                    break;
                }
                ushort num3 = String[i];
                this.Write(num3, endianStyle);
            }
            int num4 = (length - num) * 2;
            if (num4 > 0)
            {
                this.Write(new byte[num4]);
            }
        }
    }
}

