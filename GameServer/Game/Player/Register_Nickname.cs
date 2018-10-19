using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Common;
using System.Data;

namespace libcomservice.REQUEST
{
    public class Nick_Manager
    {
        public void Register(Session p, PacketRead r)
        {
            string userNickname = r.UnicodeString();

            DataSet Query = new DataSet();
            GameServer.Sql.Exec(Query, "SELECT * FROM NickNames  WHERE nick = '{0}'", userNickname);
            if (Query.Tables[0].Rows.Count == 0)
            {
                p.PInfo.m_strNickName = userNickname;
                PacketWrite pw = new PacketWrite();
                pw.Int(0);
                pw.Str(userNickname);
                p.SendPacket(pw, 136);

                DataSet DBAcess = new DataSet();
                GameServer.Sql.Exec(DBAcess, "INSERT INTO NickNames(Login,nick) VALUES('{0}','{1}')",p.PInfo.m_strLogin, userNickname);
            }
            else
            {
                PacketWrite pw = new PacketWrite();
                pw.HexArray("FF FF FF FD 00 00 00 00");
                p.SendPacket(pw, 136);
            }
        }
    }
}
