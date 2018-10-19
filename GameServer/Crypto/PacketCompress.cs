using Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace libcomservice
{
    public static class PacketCompress
    {
        public static PacketWrite HalfCompress(byte[] Content)
        {
            byte[] _compress = Zlib.ZlibStream.CompressBuffer(Content);

            PacketWrite WB = new PacketWrite();

            WB.HexArray("00 00 00 00 00 00 00 00 00 00 00 01");
            WB.Int(_compress.Length + 4);
            WB.Byte((byte)1);
            WB.ArrayBytes(BitConverter.GetBytes(Content.Length));
            WB.ArrayBytes(_compress);
            return WB;
        }

        public static PacketWrite EnterChannelCompress(byte[] Content)
        {
            byte[] _compress = Zlib.ZlibStream.CompressBuffer(Content);

            PacketWrite WB = new PacketWrite();

            WB.HexArray("00 00 00 00");
            WB.Int(_compress.Length + 4);
            WB.Byte((byte)1);
            WB.ArrayBytes(BitConverter.GetBytes(Content.Length));
            WB.ArrayBytes(_compress);
            return WB;
        }
    }
}
