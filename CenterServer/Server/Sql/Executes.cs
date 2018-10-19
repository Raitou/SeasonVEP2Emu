using libcomservice.REQUEST;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace libcomservice.Data
{
    internal static class Querys
    {
        internal static void Execute_ListServers(SERVER_LIST Server)
        {
            try
            {
                DataSet db_Acess = new DataSet();
                Ultilize.DATA.Exec(db_Acess, "SELECT * FROM  ConnectStatusDB");
                Server.List = new Servers[db_Acess.Tables[0].Rows.Count];
                for (int i = 0; i < db_Acess.Tables[0].Rows.Count; i++)
                {
                    Server.List[i].No = Convert.ToInt32(db_Acess.Tables[0].Rows[i]["NO"].ToString());
                    Server.List[i].UserNum = Convert.ToInt32(db_Acess.Tables[0].Rows[i]["UserNum"].ToString());
                    Server.List[i].ServerIP = db_Acess.Tables[0].Rows[i]["ServerIP"].ToString();
                    Server.List[i].ServerPort = Convert.ToUInt16(db_Acess.Tables[0].Rows[i]["ServerPort"].ToString());
                    Server.List[i].ServerPart = Convert.ToInt32(db_Acess.Tables[0].Rows[i]["ServerPart"].ToString());
                    Server.List[i].ExtraFlag = Convert.ToInt32(db_Acess.Tables[0].Rows[i]["ExtraFlag"].ToString());
                    Server.List[i].MaxNum = Convert.ToInt32(db_Acess.Tables[0].Rows[i]["MaxNum"].ToString());
                    Server.List[i].ServerName = db_Acess.Tables[0].Rows[i]["ServerName"].ToString();
                    Server.List[i].ServerDesc = string.Format("Grand Chase Ernas {0}", i);
                    Server.List[i].ServerType = Convert.ToInt32(db_Acess.Tables[0].Rows[i]["ServerCode"].ToString());
                }
            }
            catch (Exception ex)
            {
                Log.Write("{0}:\n{1} \n{2}", ex.GetType(), ex.Message, ex.StackTrace);
            }
        }

        private static void Get_UserNick(Session p, string Login)
        {
            DataSet query = new DataSet();
            Ultilize.DATA.Exec(query, "SELECT nick FROM NickNames  WHERE Login = '{0}'", Login);
            if (query.Tables[0].Rows.Count > 0)
            {
                p.PInfo.UserNick = query.Tables[0].Rows[0]["nick"].ToString();
            }
        }

        internal static int Execute_VerifyAccount(Session PLAYER, string Login, string Passwd)
        {
            DataSet DBAcess = new DataSet();
            Ultilize.DATA.Exec(DBAcess, "SELECT   * FROM users  WHERE Login = '{0}' AND Passwd = '{1}'", Login, Passwd);
            if (DBAcess.Tables[0].Rows.Count > 0)
            {
                PLAYER.PInfo.AccountId = Convert.ToInt32(DBAcess.Tables[0].Rows[0]["LoginUID"].ToString());
                PLAYER.PInfo.IPAddress = DBAcess.Tables[0].Rows[0]["IPAddress"].ToString();
                Get_UserNick(PLAYER, Login);
                if (PLAYER.PInfo.AuthLevel == -1)
                {
                    return 14;
                }
                else
                {
                    return 0;
                }
            }
            else
                return 3;
        }
    }
}
