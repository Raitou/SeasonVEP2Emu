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
        internal static void Execute_ListServers(ListeningServers Server)
        {
            try
            {
                DataSet db_Acess = new DataSet();
                GameServer.Sql.Exec(db_Acess, "SELECT * FROM  ConnectStatusDB");
                Server.List = new Servers[db_Acess.Tables[0].Rows.Count];
                for (int i = 0; i < db_Acess.Tables[0].Rows.Count; i++)
                {
                    Server.List[i].No = Convert.ToInt32(db_Acess.Tables[0].Rows[i]["NO"].ToString());                    
                    Server.List[i].ServerIP = db_Acess.Tables[0].Rows[i]["ServerIP"].ToString();
                    Server.List[i].ServerPort = Convert.ToUInt16(db_Acess.Tables[0].Rows[i]["ServerPort"].ToString());
                    Server.List[i].ServerPart = Convert.ToInt32(db_Acess.Tables[0].Rows[i]["ServerPart"].ToString());
                    Server.List[i].ExtraFlag = Convert.ToInt32(db_Acess.Tables[0].Rows[i]["ExtraFlag"].ToString());
                    Server.List[i].UserNum = Convert.ToInt32(db_Acess.Tables[0].Rows[i]["UserNum"].ToString());
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

        internal static int Execute_VerifyAccount(Session p, string login, string pass)
        {
            DataSet DBAcess = new DataSet();
            GameServer.Sql.Exec(DBAcess, "SELECT *  FROM users WHERE Login = '{0}' AND Passwd = '{1}'", login, pass);
            if (DBAcess.Tables[0].Rows.Count > 0)
            {
                p.PInfo.m_dwUserUID = Convert.ToInt32(DBAcess.Tables[0].Rows[0]["LoginUID"].ToString());
                p.PInfo.m_iGamePoint = Convert.ToInt32(DBAcess.Tables[0].Rows[0]["gamePoint"].ToString());
                p.PInfo.m_dwPlayTime = Convert.ToInt32(DBAcess.Tables[0].Rows[0]["playTime"].ToString());                
                p.PInfo.m_dwSlots = Convert.ToInt32(DBAcess.Tables[0].Rows[0]["slots"].ToString());
                p.PInfo.m_strGetIP = DBAcess.Tables[0].Rows[0]["IPAddress"].ToString();
                p.PInfo.m_strPasswd = DBAcess.Tables[0].Rows[0]["Passwd"].ToString();
                Get_UserNick(p, login);
                Get_InvetoryInfo(p, p.PInfo.m_dwUserUID);
                if (p.PInfo.m_dwAuthType == -1)
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

        private static void Get_UserNick(Session p, string Login)
        {
            DataSet query = new DataSet();
            GameServer.Sql.Exec(query, "SELECT nick FROM NickNames  WHERE Login = '{0}'", Login);
            if (query.Tables[0].Rows.Count > 0)
            {
                p.PInfo.m_strNickName = query.Tables[0].Rows[0]["nick"].ToString();
            }
        }

        private static void Get_InvetoryInfo(Session p, int dwID)
        {
            DataSet query = new DataSet();
            GameServer.Sql.Exec(query, "SELECT Size FROM GInventoryInfo  WHERE LoginUID = '{0}'", dwID);
            if (query.Tables[0].Rows.Count > 0)
            {
                p.PInfo.m_dwInvetoryInfo = Convert.ToInt32(query.Tables[0].Rows[0]["Size"].ToString());
            }
        }
        
        public static int Execute_SelectPriceByItemID(int ItemID)
        {
            DataSet Query = new DataSet();
            GameServer.Sql.Exec(Query, "SELECT Price  FROM  goodsinfolist WHERE GoodsID = '{0}'", ItemID);
            if (Query.Tables[0].Rows.Count > 0)
            {
                return Convert.ToInt32(Query.Tables[0].Rows[0]["Price"].ToString());
            }
            return 0;
        }

        public static byte Execute_SelectStatusByItemID(int ItemID)
        {
            DataSet Query = new DataSet();
            GameServer.Sql.Exec(Query, "SELECT Kind  FROM  goodsinfolist WHERE GoodsID = '{0}'", ItemID);
            if (Query.Tables[0].Rows.Count > 0)
            {
                if (Convert.ToInt32(Query.Tables[0].Rows[0]["Kind"].ToString()) == 777)
                    return 1;
                else
                    return 0;
            }
            return 0;
        }

        public static int CheckItemExist(string login, int value)
        {
            DataSet Query = new DataSet();
            GameServer.Sql.Exec(Query, "SELECT Period FROM  GoodsObjectlist WHERE OwnerLogin = '{0}' AND  ItemID = '{1}'", login, value);
            if (Query.Tables[0].Rows.Count > 0)
            {
                return Convert.ToInt32(Query.Tables[0].Rows[0]["Period"].ToString());
            }
            else
            {
                return 0;
            }
        }

        public static void Execute_UpdateQuantityShop(string login, int value, int quantity)
        {
            DataSet Query = new DataSet();
            GameServer.Sql.Exec(Query, "UPDATE GoodsObjectList SET  Period = '{0}' WHERE OwnerLogin = '{1}' AND ItemID = '{2}'", quantity, login, value);
        }

        public static void Execute_UpdateSLOTS(int loginid, int slotscount)
        {
            DataSet Query = new DataSet();
            GameServer.Sql.Exec(Query, "UPDATE users SET slots = '{0}' WHERE LoginUID = '{1}'", slotscount, loginid);
        }

        public static void Execute_UpdateVP(int AccountId, int value)
        {
            DataSet Query = new DataSet();
            GameServer.Sql.Exec(Query, "UPDATE VCGAVirtualCash SET VCPoint = '{0}' WHERE LoginUID = '{1}'", value, AccountId);
        }

        public static void Execute_UpdateGP(int AccountId, int value)
        {
            DataSet Query = new DataSet();
            GameServer.Sql.Exec(Query, "UPDATE users SET gamePoint = '{0}' WHERE LoginUID = '{1}'", value, AccountId);
        }

        public static void Execute_UpdateItem(string login, uint item,uint itemuid,int quantity)
        {
            DataSet Query = new DataSet();
            GameServer.Sql.Exec(Query, "UPDATE GoodsObjectList SET Quantity = '{0}' WHERE BuyerLogin = '{1}' AND ItemID = '{2}' AND ItemUID = '{3}'", quantity, login, item, itemuid);
        }

        public static int Execute_SelectVirtualPoint(int loginid)
        {
            DataSet Query = new DataSet();
            GameServer.Sql.Exec(Query, "SELECT VCPoint  FROM  VCGAVirtualCash WHERE LoginUID = '{0}'", loginid);
            if (Query.Tables[0].Rows.Count > 0)
            {
                return Convert.ToInt32(Query.Tables[0].Rows[0]["VCPoint"].ToString());
            }
            return 0;
        }

        public static void Execute_InsertItem(string Login, uint itemuid, uint itemid, int quantity, int period,byte islook)
        {
            DataSet Query = new DataSet();
            GameServer.Sql.Exec(Query, "INSERT INTO GoodsObjectlist (OwnerLogin,BuyerLogin,ItemID,RegDate,StartDate,EndDate,Quantity,Period,ItemUID,isLook)  VALUES ('{0}','{1}','{2}','00:00:00','00:00:00','00:00:00','{3}','{4}','{5}','{6}') ", Login, Login, itemid, quantity, period, itemuid,islook);
        }

        public static void Execute_UpdatePeriod()
        {
            DataSet Query = new DataSet();
            GameServer.Sql.Exec(Query, "SELECT Period FROM GoodsObjectlist");
            for (int x = 0; x < Query.Tables.Count; x++ )
            {
                if (Convert.ToInt32(Query.Tables[0].Rows[x]["Period"].ToString()) != -1 && Convert.ToInt32(Query.Tables[0].Rows[x]["Period"].ToString()) != 0)
                {
                    DataSet query1 = new DataSet();
                    GameServer.Sql.Exec(query1, "UPDATE GoodsObjectlist SET Period ='{0}' WHERE Period = '{1}'", Convert.ToInt32(Query.Tables[0].Rows[x]["Period"].ToString()) - 1, Convert.ToInt32(Query.Tables[0].Rows[x]["Period"].ToString()));
                }
                DataSet query0 = new DataSet();
                GameServer.Sql.Exec(query0, "DELETE FROM GoodsObjectlist WHERE Period = '0'");
            }
        }

        public static void Execute_RemoveItem(string login,uint itemuid)
        {
            DataSet Query = new DataSet();
            GameServer.Sql.Exec(Query, "DELETE FROM GoodsObjectlist WHERE BuyerLogin = '{0}' AND ItemUID = '{1}'", login, itemuid);
        }

        public static void Execute_InsertCharacter(string login, byte chartype, byte promotion, long exp, int level)
        {
            DataSet Query = new DataSet();
            GameServer.Sql.Exec(Query, "INSERT INTO Characters(Login,CharType,Promotion,Exp,Level,Win,Lose,EquipSize)VALUES('{0}','{1}','{2}','{3}','{4}','0','0','0')", login, chartype, promotion, exp, level);
        }

        public static void Execute_VerifyStages(Session p)
        {
            DataSet Query = new DataSet();
            GameServer.Sql.Exec(Query, "SELECT * FROM  UGGAUserGameClear WHERE LoginUID = '{0}' ORDER BY ModeID ASC", p.PInfo.m_dwUserUID);
            if (Query.Tables[0].Rows.Count > 0)
            {
                for (int i = 0; i < Query.Tables[0].Rows.Count; i++)
                {
                    p.PStages.ChargeStages(Convert.ToInt32(Query.Tables[0].Rows[i]["ModeID"].ToString()),Convert.ToByte(Query.Tables[0].Rows[i]["DificutyLV1"].ToString()),Convert.ToByte(Query.Tables[0].Rows[i]["DificutyLV2"].ToString()),Convert.ToByte(Query.Tables[0].Rows[i]["DificutyLV3"].ToString()));
                }
            }
            else
            {
                p.PStages.ChargeStages(7, 1, 1, 1);
            }
        }

        public static void VerifyGameClear(Session p, int ModeID, byte Lv1, byte Lv2, byte Lv3)
        {
            DataSet Parameter = new DataSet();
            GameServer.Sql.Exec(Parameter, "SELECT * FROM UGGAUserGameClear WHERE ModeID = '{0}' AND LoginUID ='{1}'", ModeID, p.PInfo.m_dwUserUID);
            if (Parameter.Tables[0].Rows.Count == 0)
            {
                DataSet newQuery = new DataSet();
                GameServer.Sql.Exec(newQuery, "INSERT INTO UGGAUserGameClear (  LoginUID,  ModeID,  DificutyLV1,  DificutyLV2,  DificutyLV3) VALUES  (    '{0}',    '{1}',    '{2}',    '{3}',    '{4}'  )", p.PInfo.m_dwUserUID, ModeID, Lv1, Lv2, Lv3);
                p.PStages.AddStage(p.PInfo.m_dwUserUID, ModeID, Lv1, Lv2, Lv3);
            }
        }

        public static void Execute_ClearST(int userId, byte charType)
        {
            DataSet Query = new DataSet();
            GameServer.Sql.Exec(Query, "DELETE FROM STGSkillTreeSet WHERE LoginUID = '{0}' AND CharType = '{1}'", userId, charType);
        }

        public static void Execute_InsertSP(int userId, byte charType, long exp)
        {
            DataSet Query = new DataSet();
            GameServer.Sql.Exec(Query, "INSERT INTO STGSkillTreeSP (LoginUID,CharType,SPExp,SPLv)  VALUES ('{0}','{1}','{2}','0')", userId, charType, exp);
        }

        public static void Execute_InsertST(int userId, byte charType, byte promotion, int stId, int groupId)
        {
            DataSet Query = new DataSet();
            GameServer.Sql.Exec(Query, "INSERT INTO STGSkillTreeSet (LoginUID,CharType,Promotion,SetID,SkillID)  VALUES ('{0}','{1}','{2}','{3}','{4}')", userId, charType, promotion, groupId, stId);
        }

        public static void Execute_InsertEquip(int userId, byte charType, int itemid ,int itemuid)
        {
            DataSet Query = new DataSet();
            GameServer.Sql.Exec(Query, "INSERT INTO GEIEquipItem (LoginUID,CharType,ItemUID,ItemID,ItemType) VALUES ('{0}','{1}','{2}','{3}','false' )", userId, charType, itemuid, itemid);
        }

        public static void Execute_InsertLook(int userId, byte charType, uint itemid, uint itemuid)
        {
            DataSet Query = new DataSet();
            GameServer.Sql.Exec(Query, "INSERT INTO GEIEEquipLook (LoginUID,CharType,ItemUID,ItemID) VALUES ('{0}','{1}','{2}','{3}' )", userId, charType, itemuid, itemid);
        }

        public static void Execute_RemoveEquip(int userId, byte charType, int itemid, int itemuid)
        {
            DataSet Query = new DataSet();
            GameServer.Sql.Exec(Query, "DELETE FROM GEIEquipItem WHERE LoginUID = '{0}' AND CharType = '{1}' AND ItemUID = '{2}' AND ItemID = '{3}'", userId, charType, itemuid, itemid);
        }

        public static void Execute_RemoveLook(int userId, byte charType, uint itemid, uint itemuid)
        {
            DataSet Query = new DataSet();
            GameServer.Sql.Exec(Query, "DELETE FROM GEIEEquipLook WHERE LoginUID = '{0}' AND CharType = '{1}' AND ItemUID = '{2}' AND ItemID = '{3}'", userId, charType, itemuid, itemid);
        }

        public static void Execute_UpdatePlayerTimer(int timer,int id)
        {
            DataSet Query = new DataSet();
            GameServer.Sql.Exec(Query, "UPDATE Users SET playTime = '{0}' WHERE LoginUID = '{1}'",timer,id);
        }

        public static void Execute_ResetCalendar()
        {
            DataSet Query = new DataSet();
            GameServer.Sql.Exec(Query, "UPDATE Users SET playTime = '0'");
        }

    }
}
