using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using libcomservice;

namespace Common
{
    public class KSerializer
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

        public KSerializer()
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

        public void Put(byte valuebyte)
        {
            Inc(new byte[] { valuebyte });
        }

        public void Put(bool boolean)
        {
            Inc(new byte[] { Convert.ToByte(boolean) });
        }

        public void Put(short int16)
        {
            Inc(BigEndian.GetBytes(int16));
        }

        public void Put(ushort uint16)
        {
            Inc(BigEndian.GetUInt16(uint16));
        }

        public void Put(int int32)
        {
            Inc(BigEndian.GetBytes(int32));
        }

        public void Put(uint uint32)
        {
            Inc(BigEndian.GetBytes(uint32));
        }

        public void Put(long Long)
        {
            Inc(BigEndian.GetBytes(Long));
        }

        public void PutUnicode(string ustr)
        {
            Put(ustr.Length * 2);
            Inc(Encoding.Unicode.GetBytes(ustr));
        }

        public void Put(string str)
        {
            Put(str.Length);
            Inc(Encoding.ASCII.GetBytes(str));
        }

        public void PutIP(string ServerIP)
        {
            byte[] _serverIP = System.Net.IPAddress.Parse(ServerIP).GetAddressBytes();
            Array.Reverse(_serverIP);
            Put(_serverIP);
        }

        public void Put(byte[] BYTES)
        {
            Inc(BYTES);
        }

        public void PutHex(string _bytes)
        {
            _bytes = _bytes.Replace(" ", "");
            for (int i = 0; i < _bytes.Length / 2; i++)
            {
                Put((byte)Convert.ToByte(_bytes.Substring(i * 2, 2), 16));
            }   
        }
    }
}
