using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace libcomservice
{

    public static class BigEndian
    {
        public static short GetInt16(byte[] bytes, int index)
        {
            return (short)
                ((bytes[index + 1] << 8) | (bytes[index]));
        }

        public static int GetInt32(byte[] bytes, int index)
        {
            return (bytes[index] << 24) |
                   (bytes[index + 1] << 16) |
                   (bytes[index + 2] << 8) |
                   (bytes[index + 3]);
        }

        public static long GetInt64(byte[] bytes, int index)
        {
            return (bytes[index] << 56) |
                   (bytes[index + 1] << 48) |
                   (bytes[index + 2] << 40) |
                   (bytes[index + 3] << 32) |
                   (bytes[index + 4] << 24) |
                   (bytes[index + 5] << 16) |
                   (bytes[index + 6] << 8) |
                   (bytes[index + 7]);
        }

        public static byte[] GetUInt16(ushort int16)
        {
            byte[] bytes = new byte[sizeof(ushort)];

            bytes[0] = (byte)(int16 >> 8);
            bytes[1] = (byte)(int16);

            return bytes;
        }

        public static byte[] GetBytes(short int16)
        {
            byte[] bytes = new byte[sizeof(short)];

            bytes[0] = (byte)(int16 >> 8);
            bytes[1] = (byte)(int16);

            return bytes;
        }

        public static byte[] GetBytes(int int32)
        {
            byte[] bytes = new byte[sizeof(int)];

            bytes[0] = (byte)(int32 >> 24);
            bytes[1] = (byte)(int32 >> 16);
            bytes[2] = (byte)(int32 >> 8);
            bytes[3] = (byte)(int32);

            return bytes;
        }

        public static byte[] GetBytes(long int64)
        {
            byte[] bytes = new byte[sizeof(long)];

            bytes[0] = (byte)(int64 >> 56);
            bytes[1] = (byte)(int64 >> 48);
            bytes[2] = (byte)(int64 >> 40);
            bytes[3] = (byte)(int64 >> 32);
            bytes[4] = (byte)(int64 >> 24);
            bytes[5] = (byte)(int64 >> 16);
            bytes[6] = (byte)(int64 >> 8);
            bytes[7] = (byte)(int64);

            return bytes;
        }
    }
}
