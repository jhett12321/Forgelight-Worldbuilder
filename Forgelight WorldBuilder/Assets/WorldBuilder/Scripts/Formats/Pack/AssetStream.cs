namespace WorldBuilder.Formats.Pack
{
    using System;
    using System.IO;

    public class AssetStream : Stream
    {
        public AssetRef AssetRef;
        public readonly Stream BaseStream;

        private bool leaveOpen;

        public override bool CanRead => BaseStream.CanRead;
        public override bool CanSeek => BaseStream.CanSeek;
        public override bool CanWrite => false;
        public override long Length => AssetRef.Size;

        public override long Position
        {
            get { return BaseStream.Position - AssetRef.AbsoluteOffset; }
            set { BaseStream.Position = value + AssetRef.AbsoluteOffset; }
        }

        public AssetStream(Stream baseStream, AssetRef assetRef, bool leaveOpen = false)
        {
            this.BaseStream = baseStream;
            this.AssetRef = assetRef;
            this.leaveOpen = leaveOpen;

            Initialize();
        }

        private void Initialize()
        {
            Seek(0, SeekOrigin.Begin);
        }

        public override int Read(byte[] buffer, int offset, int count)
        {
            long remaining = Length - Position;

            if (remaining < 0)
            {
                return 0;
            }

            if (remaining < count)
            {
                count = (int)remaining;
            }

            return BaseStream.Read(buffer, offset, count);
        }

        public override long Seek(long offset, SeekOrigin origin)
        {
            switch (origin)
            {
                case SeekOrigin.Begin:
                    offset += AssetRef.AbsoluteOffset;
                    break;
                case SeekOrigin.End:
                    origin = SeekOrigin.Begin;
                    offset = AssetRef.AbsoluteOffset + AssetRef.Size - offset;
                    break;
            }

            BaseStream.Seek(offset, origin);
            return Position;
        }

        public override void Flush()
        {
            throw new InvalidOperationException();
        }

        public override void SetLength(long value)
        {
            throw new InvalidOperationException();
        }

        public override void Write(byte[] buffer, int offset, int count)
        {
            throw new InvalidOperationException();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && !leaveOpen)
            {
                BaseStream?.Dispose();
            }

            base.Dispose(disposing);
        }
    }
}