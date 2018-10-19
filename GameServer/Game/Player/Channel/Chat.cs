using System;
using Common;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using libcomservice.Game;
using libcomservice.Data;
using libcomservice.Core;

namespace libcomservice.REQUEST
{
    public class cChat
    {
        public static string StringDivide(string text, int Position, int Size = -1)
        {
            if (Size != -1)
            {
                return text.Substring(Position, Size);
            }
            return text.Substring(Position);
        }

        private void SendText(Session p, int ID, String Text, string Nick = null)
        {

            PacketWrite pw = new PacketWrite();
            pw.Byte(1);
            pw.Int(p.PInfo.m_dwUserUID);
            pw.UnicodeStr(p.PInfo.m_strNickName);
            pw.Int(0);
            pw.Int(0);
            pw.Int(-1);
            pw.UnicodeStr(Text);
            pw.Int(0);
            pw.Int(0);

            if (ID == 0)
            {
                for (int i = 0; i < GameServer.UsersList.Count; i++)
                {
                    if (GameServer.UsersList[i].PInfo.m_dwChannelUID != p.PInfo.m_dwChannelUID)
                    {
                        continue;
                    }
                    GameServer.UsersList[i].SendPacket(pw, 38);
                }
            }

            if (ID == 1)
            {
                p.PInfo.CurRoom.SendForAllPlayersInRoom(pw,39);
            }
        }

        public void OnChat(Session p, PacketRead r)
        {
            r.Jump(1);
            r.Jump(4);
            string ServerName = r.UnicodeString();
            r.Jump(4);
            r.Jump(4);
            r.Jump(4);
            string text = r.UnicodeString();

            if (p.PInfo.m_cStatus == "LOBBY")
                SendText(p, 0, text);
            if (p.PInfo.m_cStatus == "ROOM")
                SendText(p, 1, text);
        }

        private void GameMasterCommands(string Text, Session p)
        {
            if (p.PInfo.m_dwAuthType == 3)
            {
                if (Text == string.Format("$!{0}", StringDivide(Text, 2)))
                {
                    Command_SignBoard(p, string.Format("{0}:{1}", p.PInfo.m_strNickName, StringDivide(Text, 2)));
                }
            }
        }

        public static void Command_SignBoard(Session player, string Msg)
        {
            PacketWrite pw = new PacketWrite();
            pw.HexArray("00 00 00 03 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00");
            pw.UnicodeStr(Msg);

            Serializables.SendForAllPlayers(pw, 362);
        }

    }
}