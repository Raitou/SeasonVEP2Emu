using Common;
using libcomservice.Game;
using libcomservice.REQUEST;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using libcomservice.Core;

namespace libcomservice.Request.GameMaster
{
    public static class Commands
    {
        public static string StringDivide(string text, int Position, int Size = -1)
        {
            if (Size != -1)
            {
                return text.Substring(Position, Size);
            }
            return text.Substring(Position);
        }

       /* public static void Command_Exit(Session player)
        {
            WriteBuffer Write = new WriteBuffer();
            Write.Int(0);
            Write.Int(0);
            player.Session_Send(Write, 1240);
        }

        public static void Command_NextMode(Session player)
        {
            WriteBuffer WB = new WriteBuffer();
            WB.Int(0);
            WB.Byte(0);
            WB.Short(0);

            WB.Byte((byte)player.Account.CurrentRoom.MatchMode);
            WB.Int(player.Account.CurrentRoom.GameMode + 1);
            WB.Int(player.Account.CurrentRoom.ItemMode);
            WB.Boolean(player.Account.CurrentRoom.RandomMap);
            WB.Int(player.Account.CurrentRoom.Map);

            WB.Int(0);
            WB.HexArray("FF FF FF FF 00 00 00 00 00 00 00");

            WB.Short((short)player.Account.CurrentRoom.PlayersInRoom());
            WB.Short((short)player.Account.CurrentRoom.OpenSlots());

            for (int i = 0; i < player.Account.CurrentRoom.MaxPlayers; i++)
            {
                WB.Boolean(player.Account.CurrentRoom.Slots[i].Open);
            }

            WB.Int(0);
            WB.HexArray("00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 01 00");
            for (int i = 0; i < player.Account.CurrentRoom.MaxPlayers; i++)
            {
                if (player.Account.CurrentRoom.Slots[i].Active == true)
                {
                    player.Account.CurrentRoom.Slots[i].Player.Session_Send(WB, 29);
                }
            }
        }

        public static void Command_PrevioustMode(Session player)
        {
            WriteBuffer WB = new WriteBuffer();
            WB.Int(0);
            WB.Byte(0);
            WB.Short(0);

            WB.Byte((byte)player.Account.CurrentRoom.MatchMode);
            WB.Int(player.Account.CurrentRoom.GameMode - 1);
            WB.Int(player.Account.CurrentRoom.ItemMode);
            WB.Boolean(player.Account.CurrentRoom.RandomMap);
            WB.Int(player.Account.CurrentRoom.Map);

            WB.Int(0);
            WB.HexArray("FF FF FF FF 00 00 00 00 00 00 00");

            WB.Short((short)player.Account.CurrentRoom.PlayersInRoom());
            WB.Short((short)player.Account.CurrentRoom.OpenSlots());

            for (int i = 0; i < player.Account.CurrentRoom.MaxPlayers; i++)
            {
                WB.Boolean(player.Account.CurrentRoom.Slots[i].Open);
            }

            WB.Int(0);
            WB.HexArray("00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 01 00");
            for (int i = 0; i < player.Account.CurrentRoom.MaxPlayers; i++)
            {
                if (player.Account.CurrentRoom.Slots[i].Active == true)
                {
                    player.Account.CurrentRoom.Slots[i].Player.Session_Send(WB, 29);
                }
            }
        }*/

        public static void Command_SignBoard(Session player, string Msg)
        {
            PacketWrite pw = new PacketWrite();
            pw.HexArray("00 00 00 03 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00");
            pw.UnicodeStr(Msg);
           
            Serializables.SendForAllPlayers(pw, 362);
        }

       /* public static void Command_AddItem(Session player, string Msg)
        {
            int itemValue = Convert.ToInt32(Msg);
            DataSet query = new DataSet();
            player.Inventory.AddItem(player.Account.AccountID, itemValue, player.Inventory.ItemsList.Count, -1, 0, 0);
            ServerInfo.DATA.Exec(query, "INSERT INTO inventory (  LoginUID,  ItemID,  ItemUID,  Quantity,  Status,  Type,  Upgrade,  ItemLevel) VALUES  (    '{0}',    '{1}',    '{2}',    '-1',    '0',    '0',    '0',    '0'  ) ;", player.Account.AccountID, itemValue);
            player.Inventory.SendInventory(player);
        }

        public static void Command_KillingSpree(Session player, string Msg)
        {
            WriteBuffer Write = new WriteBuffer();
            Write.HexArray("00 00 00 03 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00");
            Write.UNIString(Msg);
            for (int i = 0; i < player.Account.CurrentRoom.MaxPlayers; i++)
                if (player.Account.CurrentRoom.Slots[i].Active == true)
                    player.Account.CurrentRoom.Slots[i].Player.Session_Send(Write, 353);
        }

        public static void Command_ChangeMode(Session player, int match,int game,int map)
        {
            player.Account.CurrentRoom.ChangeModes(match, false, game, 0, map, 0);

            WriteBuffer WB = new WriteBuffer();
            WB.Int(0);
            WB.Byte(0);
            WB.Short(0);

            WB.Byte((byte)player.Account.CurrentRoom.MatchMode);
            WB.Int(player.Account.CurrentRoom.GameMode);
            WB.Int(0);
            WB.Boolean(false);
            WB.Int(player.Account.CurrentRoom.Map);

            WB.Int(12);
            WB.HexArray("FF FF FF FF 00 00 00 00 00 00 00");

            WB.Short((short)player.Account.CurrentRoom.PlayersInRoom());
            WB.Short((short)player.Account.CurrentRoom.OpenSlots());

            for (int i = 0; i < player.Account.CurrentRoom.MaxPlayers; i++)
            {
                WB.Boolean(player.Account.CurrentRoom.Slots[i].Open);
            }
            if (player.Account.CurrentRoom.MaxPlayers == 4)
            {
                WB.Boolean(false);
                WB.Boolean(false);
            }

            WB.Int(0);
            WB.HexArray("00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00");

            WB.Short(0);
            WB.Short(0);
            WB.Short(1);            

            for (int i = 0; i < player.Account.CurrentRoom.MaxPlayers; i++)
            {
                if (player.Account.CurrentRoom.Slots[i].Active == true)
                {
                    player.Account.CurrentRoom.Slots[i].Player.Session_Send(WB, 29);
                }
            }
        }*/

    }
}
