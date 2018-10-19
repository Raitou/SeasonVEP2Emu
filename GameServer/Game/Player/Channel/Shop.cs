using System;
using Common;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using libcomservice.Game;
using libcomservice.Data;
using libcomservice.Game.Player.Inventory;

namespace libcomservice.REQUEST
{
    public class shop
    {
        public void CashRatio(Session p, PacketRead r)
        {
            PacketWrite pw = new PacketWrite();
            pw.HexArray("00 00 00 00 FF FF FF FF 00 00 00 00");
            p.SendPacket(pw, 1557);
        }

        public void packageInfo(Session p, PacketRead r)
        {
            PacketWrite pw = new PacketWrite();
            pw.HexArray("00 00 00 02 00 00 00 01");
            p.SendPacket(pw, 1043);
        }
        public void packageInfoDetail(Session p, PacketRead r)
        {
            PacketWrite ks = new PacketWrite();
            int id1 = r.Int();
            int id2 = r.Int();
            ks.Int(id1);
            ks.Int(id2);
            ks.Int(0);
            p.SendPacket(ks, 1600);
        }

        public void SendMyVirtualCash(Session p)
        {
            p.PInfo.m_iVirtualPoint = Querys.Execute_SelectVirtualPoint(p.PInfo.m_dwUserUID);

            PacketWrite pw = new PacketWrite();
            pw.Int(p.PInfo.m_iVirtualPoint);
            p.SendPacket(pw, 394);
        }

        public void CheckItem(Session p, PacketRead r)
        {
            int ItemID = r.Int();
            PacketWrite pw = new PacketWrite();
            pw.Int(1);
            pw.Int(ItemID);
            p.SendPacket(pw, 677);
        }

        public void BuyGP(Session p, PacketRead r)
        {
            try
            {
                uint itemid = r.UInt();
                int quantity = r.Int();
                uint itemuid = 0;
                int valuePrice = SelectPrice(itemid);
                if (CheckItemExists(itemid, p) && quantity != -1)
                {
                    sItem currentItem = ItemExists(itemid, p);
                    itemuid = currentItem.ItemUID;
                    quantity += currentItem.Quantity;
                    p.PInventory.UpdateItem(p.PInfo.m_strLogin, itemid, quantity, itemuid);
                    p.PInventory.InventoryList.Remove(currentItem);
                }
                else
                {
                    itemuid = GetUID();
                    p.PInventory.AddItem(p.PInfo.m_strLogin, itemid, quantity, itemuid);
                }
                PacketWrite pw = new PacketWrite();
                pw.Int(BuyExceptionGP(valuePrice, p));
                pw.Int(p.PInfo.m_iGamePoint);
                pw.Int(1);
                pw.UInt(itemid);
                pw.Int(1);
                pw.UInt(itemuid);
                pw.Int(quantity);
                pw.Int(quantity);
                pw.Short(0);
                pw.HexArray("00 00 00 00 FF FF FF FF 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 0C 0C 53 6C A5 B9 73 A2 00 40 37 CC 00 00 0B 00 00 00 00 00 00 00 00 00");
                p.SendPacket(pw, 85);
            }
            catch (Exception ex)
            {
                Log.Write("\n{0}\n{1}\n", ex.Message, ex.StackTrace);
                PacketWrite pw = new PacketWrite();
                pw.Int(1);
                pw.Int(0);
                p.SendPacket(pw, 85);
            }
        }

        public void BuyVC(Session p, PacketRead r)
        {
            try
            {
                r.Jump(13);
                uint itemid = r.UInt();
                r.Jump(8);
                int quantity = r.Int();
                uint itemuid = 0;
                int valuePrice = SelectPrice(itemid);
                if (CheckItemExists(itemid, p) && quantity != -1)
                {
                    sItem currentItem = ItemExists(itemid, p);
                    itemuid = currentItem.ItemUID;
                    quantity += currentItem.Quantity;
                    p.PInventory.UpdateItem(p.PInfo.m_strLogin, itemid, quantity, itemuid);
                    p.PInventory.InventoryList.Remove(currentItem);
                }
                else
                {
                    itemuid = GetUID();
                    p.PInventory.AddItem(p.PInfo.m_strLogin, itemid, quantity, itemuid);                    
                }
                PacketWrite pw = new PacketWrite();
                pw.Int(BuyException(valuePrice, p));
                pw.Int(1);
                pw.UInt(itemid);
                pw.Int(1);
                pw.UInt(itemuid);
                pw.Int(quantity);
                pw.Int(quantity);
                pw.Short(0);//Epic
                pw.HexArray("00 00 00 00 FF FF FF FF 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00");
                pw.Byte(LoadItemType(itemid));
                pw.HexArray("00 00 00 00 00 00 00 00 00 00 00 00 00 04 FC F7 70 04 FC FF 78 00 00 0D 00 00 00 00 00 00 00 00 00 FF FF FF 9D");
                pw.Int(p.PInfo.m_iVirtualPoint);
                pw.HexArray("00 00 00 00 00 00 01 5E 1C 00 40 47 00 00 00 00");
                p.SendPacket(pw, 396);
            }
            catch
            {
                PacketWrite pw = new PacketWrite();
                pw.Int(1);
                pw.Int(0);
                p.SendPacket(pw, 396);
            }
        }

        private byte LoadItemType(uint goodsid)
        {
            for (int x = 0; x < libcomservice.Request.LookItens.Items.Length; x++)
            {
                if (libcomservice.Request.LookItens.Items[x] == goodsid)
                {
                    return 1;
                }
            }
            return 0;
        }

        private bool itemException(uint itemindex, Session p)
        {
            switch (itemindex)
            {
                case 1213960:
                    {
                        AddSlotNewChar(p, 0);
                        return true;
                    }
                default:
                    {
                        return false;
                    }
            }
        }

        public void AddSlotChar(Session p, PacketRead r)
        {
            uint _slotid = r.UInt();
            AddSlotNewChar(p, _slotid);
        }

        public void AddSlotNewChar(Session p, uint slotid)
        {
            int price = 500;
            int exce = BuyException(price, p);
            PacketWrite pw = new PacketWrite();
            pw.Int(exce);
            pw.Int(1);
            pw.Int(1);
            pw.UInt(slotid);
            pw.Int(1);
            pw.UInt(slotid);
            pw.HexArray("00 00 00 00 00 00 00 01 00 02 FF FF 00 00 FF FF FF FF 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 04 FA 6E 90 04 FA 72 98 00 00 0B 00 00 00 00 00 00 00 00 00");            
            p.SendPacket(pw, 1618);
            if (exce == 0)
            {
                p.PInfo.m_dwSlots += 1;
                Querys.Execute_UpdateSLOTS(p.PInfo.m_dwUserUID, p.PInfo.m_dwSlots);
            }
        }

        private bool CheckItemExists(uint itemindex, Session p)
        {
            for (int x = 0; x < p.PInventory.InventoryList.Count; x += 1)
            {
                if (p.PInventory.InventoryList[x].ItemID == itemindex && p.PInventory.InventoryList[x].Quantity != -1)
                {
                    return true;
                }
            }
            return false;
        }

        private int BuyExceptionGP(int price, Session p)
        {
            if (p.PInfo.m_iGamePoint < price)
            {
                return 1;
            }
            else
            {
                p.PInfo.m_iGamePoint -= price;
                Querys.Execute_UpdateGP(p.PInfo.m_dwUserUID, p.PInfo.m_iGamePoint);
                return 0;
            }
        }

        private int BuyException(int price, Session p)
        {
            if (p.PInfo.m_iVirtualPoint < price)
            {
                return 1;
            }
            else
            {
                p.PInfo.m_iVirtualPoint -= price;
                Querys.Execute_UpdateVP(p.PInfo.m_dwUserUID, p.PInfo.m_iVirtualPoint);                
                return 0;
            }
        }

        private sItem ItemExists(uint itemindex, Session p)
        {
            for (int x = 0; x < p.PInventory.InventoryList.Count; x += 1)
            {
                if (p.PInventory.InventoryList[x].ItemID == itemindex)
                {
                    return p.PInventory.InventoryList[x];
                }
            }
            return p.PInventory.InventoryList[0];
        }

        private uint GetUID()
        {
            DataSet q0 = new DataSet();
            GameServer.Sql.Exec(q0, "SELECT * FROM GoodsObjectList");
            return (uint)q0.Tables[0].Rows.Count;
        }

        private int SelectPrice(uint itemindex)
        {
            DataSet q0 = new DataSet();
            GameServer.Sql.Exec(q0, "SELECT Price FROM GoodsInfoList WHERE GoodsID = '{0}'", itemindex);
            if (q0.Tables[0].Rows.Count > 0)
            {
                return Convert.ToInt32(q0.Tables[0].Rows[0]["Price"].ToString());
            }
            return 300;
        }

    }
}