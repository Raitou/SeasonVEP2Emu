using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Net;
using System.Text;
using libcomservice;
using Zlib;

namespace Common
{
    public class PacketRead
    {        
        private byte[] Buffer { get; set; }
        private int Position = 7;

        public PacketRead(byte[] _buffer,int _position = 7,bool _check = false)
        {
            Position = _position;
            Buffer = _buffer;
            if (_check == true)
            {
                if (_buffer.Length > 7)
                {
                    if (_buffer[6] == 1)
                    {
                        Position = 0;

                        Jump(11);
                        ushort ZLIB_SIZE = (ushort)((Buffer[4] << 8) | (Buffer[5]));

                        byte[] CONTENT_COMPRESS = Buffer_Array_Bytes(ZLIB_SIZE - 4);

                        Position = 0;
                        Buffer = null;                        
                        Buffer = ZlibStream.UncompressBuffer(CONTENT_COMPRESS);
                    }
                    else
                    {
                        Position = 7;
                        Buffer = _buffer;
                    }
                }
            }            
        }

        public byte[] Get_Payload()
        {
            return Buffer;
        }

        public int Get_Position()
        {
            return Position;
        }

        public void SetPosition(int newpos)
        {
            Position = newpos;
        }

        public byte[] Buffer_Array_Bytes(int SIZE)
        {
            var CurrentBUFFER = new byte[SIZE];
            Array.Copy(Buffer, Position, CurrentBUFFER, 0, SIZE);
            Position += SIZE;
            return CurrentBUFFER;
        }

        public void Jump(int SIZE)
        {
            Position += SIZE;
        }

        public short GetPacketID()
        {
            return (short)((Buffer[0] << 8) | (Buffer[1]));
        }

        public byte Byte()
        {
            Position += 1;
            return Buffer[Position - 1];
        }

        public bool Bool()
        {
            Position += 1;
            if (Buffer[Position - 1] == 1)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public Int32 Int()
        {
            return BigEndian.GetInt32(Buffer_Array_Bytes(sizeof(int)), 0);
        }

        public UInt32 UInt()
        {
            return BigEndian.GetUInt32(Buffer_Array_Bytes(sizeof(int)), 0);
        }

        public short Short()
        {
            return BigEndian.GetInt16(Buffer_Array_Bytes(sizeof(Int16)), 0);
        }

        public ushort UShort()
        {
            return (ushort)BigEndian.GetInt16(Buffer_Array_Bytes(sizeof(UInt16)), 0);
        }

        public short Get_Short()
        {
            Position += 2;
            return (short)((Buffer[Position - 2] << 8) | (Buffer[Position - 1]));
        }

        public long Long()
        {
            return BigEndian.GetInt64(Buffer_Array_Bytes(sizeof(long)), 0);
        }

        public string UnicodeString()
        {
            int SizeUString = Int();
            return Encoding.Unicode.GetString(Buffer_Array_Bytes(SizeUString));
        }

        public string String()
        {
            int SizeString = Int();
            return Encoding.ASCII.GetString(Buffer_Array_Bytes(SizeString));
        }
    }
}
