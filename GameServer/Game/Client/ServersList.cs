using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Common;
using libcomservice.Data;

namespace libcomservice.REQUEST
{
    public struct Servers
    {
        public int No;
        public int UserNum;
        public string ServerIP;
        public ushort ServerPort;
        public int ServerPart;
        public string ServerName;
        public string ServerDesc;
        public int MaxNum;
        public int ExtraFlag;
        public int ServerType;
    }

    public class ListeningServers
    {
        public Servers[] List;

        public ListeningServers()
        {
            Querys.Execute_ListServers(this);
        }

        public void SendList(Session right,PacketRead rs)
        {
            PacketWrite ks = new PacketWrite();
            ks.Int(List.Length);
            for (int i = 0; i < List.Length; i++)
            {
                ks.Int(i+1);
                ks.Int(i+1);
                ks.UnicodeStr(List[i].ServerName);
                ks.Str(List[i].ServerIP);
                ks.UShort(List[i].ServerPort);
                ks.Int(0);
                ks.Int(500);
                ks.Int(327);
                ks.Int(-1);
                ks.Int(-1);
                ks.Str(List[i].ServerIP);
                ks.Int(0);
                ks.Int(0);
            }
            right.SendPacket(ks, 156);
        }
    }
}
