using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using libcomservice;

namespace Common
{
    public class PacketWrite
    {
        private int Position { get; set; }
        private List<byte> ByteList;

        private byte[] ConcatBytes
        {
            get
            {
                return ByteList.ToArray();
            }
            set
            {
                ByteList = new List<byte>(value);
            }
        }

        public PacketWrite()
        {
            ByteList = new List<byte>();
            Position = 0;
        }

        public byte[] Get_Packet()
        {
            return ConcatBytes;
        }

        public int Get_Position()
        {
            return Position;
        }

        private void Inc(byte[] data)
        {
            ByteList.AddRange(data);
            Position += data.Length;
        }

        public void Byte(byte valuebyte)
        {
            Inc(new byte[] { valuebyte });
        }

        public void Bool(bool boolean)
        {
            Inc(new byte[] { Convert.ToByte(boolean) });
        }

        public void Short(short int16)
        {
            Inc(BigEndian.GetBytes(int16));
        }

        public void UShort(ushort uint16)
        {
            Inc(BigEndian.GetUInt16(uint16));
        }

        public void Int(int int32)
        {
            Inc(BigEndian.GetBytes(int32));
        }

        public void UInt(uint uint32)
        {
            Inc(BigEndian.GetBytes(uint32));
        }

        public void Long(long Long)
        {
            Inc(BigEndian.GetBytes(Long));
        }

        public void UnicodeStr(string ustr)
        {
            Int(ustr.Length * 2);
            Inc(Encoding.Unicode.GetBytes(ustr));
        }

        public void Str(string str)
        {
            Int(str.Length);
            Inc(Encoding.ASCII.GetBytes(str));
        }

        public void WriteIP(string ServerIP)
        {
            byte[] _serverIP = System.Net.IPAddress.Parse(ServerIP).GetAddressBytes();
            Array.Reverse(_serverIP);
            ArrayBytes(_serverIP);
        }

        public void ArrayBytes(byte[] BYTES)
        {
            Inc(BYTES);
        }

        public void HexArray(string _bytes)
        {
            _bytes = _bytes.Replace(" ", "");
            for (int i = 0; i < _bytes.Length / 2; i++)
            {
                Byte((byte)Convert.ToByte(_bytes.Substring(i * 2, 2), 16));
            }   
        }
    }
}
