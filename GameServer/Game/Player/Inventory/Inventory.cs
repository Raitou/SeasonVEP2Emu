using Common;
using libcomservice.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace libcomservice.Game.Player.Inventory
{
    public struct sItem
    {
        public uint ItemUID { get; set; }
        public string ItemName { get; set; }
        public uint ItemID { get; set; }
        public int Period { get; set; }
        public byte ItemType { get; set; }
        public int Quantity { get; set; }
        public byte StrongLevel { get; set; }
        public byte RequiredLevel { get; set; }        
    }

    public struct ItemInfo
    {
        public int ItemUID { get; set; }
        public int ItemID { get; set; }
        public int Quantity { get; set; }
        public short Status { get; set; }
        public int ItemType { get; set; }
        public string login { get; set; }
    }

    public class Inventory
    {
        internal List<sItem> InventoryList = new List<sItem>();

        public void BundleSellItens(Session p,PacketRead r)
        {
            PacketWrite pw = new PacketWrite();  
            r.Int();
            int m_dwDeleteCount = r.Int();
            pw.Int(m_dwDeleteCount);
            for (int i = 0; i < m_dwDeleteCount; i += 1)
            {
                int unk  = r.Int();
                pw.Int(unk);
                uint m_dwUID = r.UInt();
                pw.UInt(m_dwUID);                
                p.PInfo.m_iGamePoint += SelectPrice(m_dwUID);
                RemoveItem(p.PInfo.m_strLogin, m_dwUID);          
            }
            int unk2 = r.Int();
            pw.Int(unk2);

            PacketWrite ks = new PacketWrite();           
            ks.Int(0);//dwOK
            ks.Int(p.PInfo.m_iGamePoint);
            ks.Long(0);
            ks.ArrayBytes(pw.Get_Packet());
            p.SendPacket(ks, 867);
        }

        public void InventoryItens(Session p)
        {
            LoadItens(p);
            PacketWrite pw = new PacketWrite();
            pw.Int(1);
            pw.Int(1);
            pw.Int(InventoryList.Count);
            for (int x = 0; x < InventoryList.Count; x++)
            {
                pw.UInt(InventoryList[x].ItemID);
                pw.Int(1);
                pw.UInt(InventoryList[x].ItemUID);
                pw.Int(InventoryList[x].Quantity);
                pw.Int(InventoryList[x].Quantity);
                pw.HexArray("00 00 FF FF 00 00");
                pw.Int(InventoryList[x].Period);
                pw.HexArray("00 00 00 00 59 22 21 B4 06 00 00 00 00 00 00 00 00 00 00 00");
                pw.Byte(InventoryList[x].ItemType);
                pw.HexArray("00 00 00 00 00 00 00 00 00 FF FF FF FF 7F DE 81 10 D0 FF 24 E3 00 00 FF 00 00 00 00 00 00 00 00 00");
            }            
            p.SendPacket(pw, 232);
        }

        internal void LoadItens(Session p)
        {
            DataSet q0 = new DataSet();
            GameServer.Sql.Exec(q0, "SELECT * FROM GoodsObjectList WHERE BuyerLogin = '{0}'", p.PInfo.m_strLogin);
            for (int i = 0; i < q0.Tables[0].Rows.Count; i++)
            {
                InventoryList.Add
                    (
                        new sItem
                        {
                            ItemID = Convert.ToUInt32(q0.Tables[0].Rows[i]["ItemID"].ToString()),
                            ItemType = Convert.ToByte(q0.Tables[0].Rows[i]["isLook"].ToString()),
                            ItemUID = Convert.ToUInt32(q0.Tables[0].Rows[i]["ItemUID"].ToString()),
                            Period = Convert.ToInt32(q0.Tables[0].Rows[i]["Period"].ToString()),
                            ItemName = LoadItemName(Convert.ToUInt32(q0.Tables[0].Rows[i]["ItemID"].ToString())),
                            StrongLevel = Convert.ToByte(q0.Tables[0].Rows[i]["StrongLevel"].ToString()),
                            Quantity = Convert.ToInt32(q0.Tables[0].Rows[i]["Quantity"].ToString()),
                            RequiredLevel = Convert.ToByte(q0.Tables[0].Rows[i]["RequiredLevel"].ToString())
                        }
                    );
            }
        }

        internal void UpdateItem(string login,uint itemid, int quantity, uint itemuid, byte reqlv = 0)
        {
            InventoryList.Add(
                        new sItem
                        {
                            ItemID = itemid,
                            ItemType = LoadItemType(itemid),
                            ItemUID = itemuid,
                            Period = LoadPeriod(itemid),
                            ItemName = LoadItemName(itemid),
                            StrongLevel = 0,
                            Quantity = quantity,
                            RequiredLevel = reqlv
                        }
                    );
            Querys.Execute_UpdateItem(login, itemid, itemuid, quantity);
        }

        internal void AddItem(string login, uint itemid,int quantity, uint itemuid,int period =-1, byte reqlv = 0)
        {
            int duration = period;
            byte itemtype  = LoadItemType(itemid);
            InventoryList.Add(
                        new sItem
                        {
                            ItemID = itemid,
                            ItemType = itemtype,
                            ItemUID = itemuid,
                            Period = duration,
                            ItemName = LoadItemName(itemid),
                            StrongLevel = 0,
                            Quantity = quantity,
                            RequiredLevel = reqlv
                        }
                    );
            Querys.Execute_InsertItem(login, itemuid, itemid, quantity, duration, itemtype);
        }

        internal void RemoveItem(string login,uint itemuid)
        {
            for (int x = 0; x < InventoryList.Count; x++)
            {
                if (InventoryList[x].ItemUID == itemuid)
                {
                    InventoryList.Remove(InventoryList[x]);
                }
            }
            Querys.Execute_RemoveItem(login, itemuid);
        }

        private byte LoadItemType(uint goodsid)
        {
            for (int x = 0; x < libcomservice.Request.LookItens.Items.Length; x++ )
            {
                if (libcomservice.Request.LookItens.Items[x] == goodsid)
                {
                    return 1;
                }
            }
            /*
            DataSet q0 = new DataSet();
            GameServer.Sql.Exec(q0, "SELECT Itemtype FROM GoodsInfoList WHERE GoodsID = '{0}'", goodsid);
            if (q0.Tables[0].Rows.Count > 0)
            {
                //return Convert.ToByte(q0.Tables[0].Rows[0]["Itemtype"].ToString());
                return 0;
            }*/
            return 0;
        }

        private int LoadPeriod(uint goodsid)
        {
            DataSet q0 = new DataSet();
            GameServer.Sql.Exec(q0, "SELECT Duration FROM GoodsInfoList WHERE GoodsID = '{0}'", goodsid);
            if (q0.Tables[0].Rows.Count > 0)
            {
                return Convert.ToByte(q0.Tables[0].Rows[0]["Duration"].ToString());
            }
            return -1;
        }

        private string LoadItemName(uint goodsid)
        {
            DataSet q0 = new DataSet();
            GameServer.Sql.Exec(q0, "SELECT GoodsName FROM GoodsInfoList WHERE GoodsID = '{0}'", goodsid);
            if (q0.Tables[0].Rows.Count > 0)
            {
                return q0.Tables[0].Rows[0]["GoodsName"].ToString();
            }
            return string.Empty;
        }

        private int SelectPrice(uint itemuid)
        {
            for (int x = 0; x < InventoryList.Count; x++)
            {
                if (InventoryList[x].ItemUID == itemuid)
                {
                    DataSet q0 = new DataSet();
                    GameServer.Sql.Exec(q0, "SELECT Price FROM GoodsInfoList WHERE GoodsID = '{0}'", InventoryList[x].ItemID);
                    if (q0.Tables[0].Rows.Count > 0)
                    {
                        return Convert.ToInt32(q0.Tables[0].Rows[0]["Price"].ToString());
                    }
                }
            }
            return 300;
        }
    }
}

