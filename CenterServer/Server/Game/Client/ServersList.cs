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

    public class SERVER_LIST
    {
        public Servers[] List;

        public SERVER_LIST()
        {
            Querys.Execute_ListServers(this);
        }

        public void Send_List(Session p)
        {
            PacketWrite pw = new PacketWrite();
            pw.Int(List.Length);
            for (int i = 0; i < List.Length; i++)
            {
                pw.Int(i+1);
                pw.Int(i+1);
                pw.UnicodeStr(List[i].ServerName);
                pw.Str(List[i].ServerIP);
                pw.UShort(List[i].ServerPort);
                pw.Int(0);
                pw.Int(500);
                pw.Int(327);
                pw.Int(-1);
                pw.Int(-1);
                pw.Str(List[i].ServerIP);
                pw.Int(0);
                pw.Int(0);
            }
            p.SESSION_SEND(pw.Get_Packet(), 4);
        }
    }
}
