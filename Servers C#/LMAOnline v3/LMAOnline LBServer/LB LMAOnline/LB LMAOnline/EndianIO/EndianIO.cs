using System;
using System.IO;
using System.Runtime.CompilerServices;

namespace LB_LMAOnline.Managers
{
    public class EndianIO
    {
        private readonly EndianStyle _endianStyle;
        private readonly string _filePath;
        private readonly bool _isFile;

        public EndianIO(System.IO.Stream Stream, EndianStyle EndianStyle)
        {
            this._filePath = string.Empty;
            this._endianStyle = EndianStyle.LittleEndian;
            this._endianStyle = EndianStyle;
            this.Stream = Stream;
            this._isFile = false;
            this.Open();
        }

        public EndianIO(string FilePath, EndianStyle EndianStyle)
        {
            this._filePath = string.Empty;
            this._endianStyle = EndianStyle.LittleEndian;
            this._endianStyle = EndianStyle;
            this._filePath = FilePath;
            this._isFile = true;
            this.Open();
        }

        public EndianIO(byte[] Buffer, EndianStyle EndianStyle)
        {
            this._filePath = string.Empty;
            this._endianStyle = EndianStyle.LittleEndian;
            this._endianStyle = EndianStyle;
            this.Stream = new MemoryStream(Buffer);
            this._isFile = false;
            this.Open();
        }

        public EndianIO(string FilePath, EndianStyle EndianStyle, FileMode FileMode)
        {
            this._filePath = string.Empty;
            this._endianStyle = EndianStyle.LittleEndian;
            this._endianStyle = EndianStyle;
            this._filePath = FilePath;
            this._isFile = true;
            this.Open(FileMode);
        }

        public void Close()
        {
            if (this.Opened)
            {
                this.Stream.Close();
                this.Reader.Close();
                this.Writer.Close();
                this.Opened = false;
            }
        }

        public void Open()
        {
            this.Open(FileMode.Open);
        }

        public void Open(FileMode FileMode)
        {
            if (!this.Opened)
            {
                if (this._isFile)
                {
                    this.Stream = new FileStream(this._filePath, FileMode, FileAccess.ReadWrite);
                }
                this.Reader = new EndianReader(this.Stream, this._endianStyle);
                this.Writer = new EndianWriter(this.Stream, this._endianStyle);
                this.Opened = true;
            }
        }

        public byte[] ToArray()
        {
            return ((MemoryStream)this.Stream).ToArray();
        }

        public bool Opened { get; set; }

        public long Position
        {
            get
            {
                return this.Stream.Position;
            }
            set
            {
                this.Stream.Position = value;
            }
        }

        public EndianReader Reader { get; set; }

        public System.IO.Stream Stream { get; set; }

        public EndianWriter Writer { get; set; }
    }
}

