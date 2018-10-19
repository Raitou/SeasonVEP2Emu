using Common;
using libcomservice.Core;
using libcomservice.REQUEST;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace libcomservice.Game.Player.Channel
{
    public class ChannelInfo
    {
        private Room RoomExists(ushort roomid)
        {
            if (GameServer.List_Rooms.ContainsKey(roomid))
                return GameServer.List_Rooms[roomid];
            return null;
        }

        public void ChannelList(Session s, PacketRead r)
        {
            PacketWrite p = new PacketWrite();
            p.HexArray("00 00 00 06 00 00 00 01 00 00 00 14 46 00 72 00 65 00 65 00 20 00 43 00 6C 00 61 00 73 00 73 00 01 2C 00 00 03 00 00 00 00 02 00 00 00 1C 42 00 65 00 67 00 69 00 6E 00 6E 00 65 00 72 00 20 00 43 00 6C 00 61 00 73 00 73 00 01 2C 00 00 04 00 00 00 00 03 00 00 00 18 4D 00 69 00 64 00 64 00 6C 00 65 00 20 00 43 00 6C 00 61 00 73 00 73 00 01 2C 00 00 05 00 00 00 00 04 00 00 00 1C 53 00 75 00 70 00 65 00 72 00 69 00 6F 00 72 00 20 00 43 00 6C 00 61 00 73 00 73 00 01 2C 00 00 06 00 00 00 00 05 00 00 00 18 4D 00 61 00 73 00 74 00 65 00 72 00 20 00 43 00 6C 00 61 00 73 00 73 00 01 2C 00 00 07 00 00 00 00 06 00 00 00 0E 44 00 75 00 6E 00 67 00 65 00 6F 00 6E 00 01 2C 01 24 00 00");
            s.SendPacket(PacketCompress.EnterChannelCompress(p.Get_Packet()), 47);
        }

        public void EnterChannel(Session s, PacketRead r)
        {
            s.PInfo.m_dwChannelUID = r.Int();
            PacketWrite p = new PacketWrite();
            p.HexArray("00 00 00 00 03");
            p.Int(Serializables.m_timeStamp());
            p.Int(Serializables.m_timeStamp());
            s.SendPacket(p, 45);
            GameServer.UsersList.Add(s);
            s.PInfo.m_cStatus = "LOBBY";
        }

        public void LeaveChannel(Session s, PacketRead r)
        {
            PacketWrite pw = new PacketWrite();
            pw.Int(0);
            s.SendPacket(pw, 58);
            GameServer.UsersList.Remove(s);
            s.PInfo.m_cStatus = "...";
        }

        public void ListRooms(Session p, PacketRead r)
        {
            try
            {
                byte RoomType = r.Byte();
                byte RoomMode = r.Byte();

                int RoomsCount = 0;
                foreach (ushort i in GameServer.List_Rooms.Keys)
                {
                    if (p.PInfo.m_dwChannelUID == 6)
                    {
                        if (RoomMode != GameServer.List_Rooms[i].m_iGameMode)
                            continue;
                    }
                    else
                    {
                        if (GameServer.List_Rooms[i].m_cGameCategory != 0)
                            continue;
                    }
                    if (RoomType == 1)
                        if (GameServer.List_Rooms[i].m_bPlaying == true || GameServer.List_Rooms[i].m_abTotalSlotsOpen() == 0)
                            continue;
                    RoomsCount += 1;
                }

                PacketWrite pw = new PacketWrite();
                pw.Int(RoomsCount);

                foreach (ushort RoomPosition in GameServer.List_Rooms.Keys)
                {
                    if (p.PInfo.m_dwChannelUID == 6)
                    {
                        if (RoomMode != GameServer.List_Rooms[RoomPosition].m_iGameMode)
                            continue;
                    }
                    else
                    {
                        if (GameServer.List_Rooms[RoomPosition].m_cGameCategory != 0)
                            continue;
                    }

                    if (RoomType == 1)
                        if (GameServer.List_Rooms[RoomPosition].m_bPlaying == true || GameServer.List_Rooms[RoomPosition].m_abTotalSlotsOpen() == 0)
                            continue;

                    pw.UShort(GameServer.List_Rooms[RoomPosition].m_usRoomID);
                    pw.UnicodeStr(GameServer.List_Rooms[RoomPosition].m_strRoomName);

                    if (GameServer.List_Rooms[RoomPosition].m_strRoomPasswd.Length > 0)
                        pw.Bool(false);
                    else
                        pw.Bool(true);

                    pw.Byte(0);
                    pw.UnicodeStr(GameServer.List_Rooms[RoomPosition].m_strRoomPasswd);
                    pw.Short((short)(GameServer.List_Rooms[RoomPosition].m_abTotalSlotsOpen() + 1));
                    pw.Short(GameServer.List_Rooms[RoomPosition].m_usUsers());
                    pw.Bool(GameServer.List_Rooms[RoomPosition].m_bPlaying);
                    pw.HexArray("6F 0C CC FA 0B 08 00 00 00 20 63 00 00 F5 00 00 00 03 00 00 00 00");
                    pw.Short(0);
                    pw.Short(GameServer.List_Rooms[RoomPosition].m_usUsers());
                    for (int x = 0; x < GameServer.List_Rooms[RoomPosition].m_usMaxUsers; x++)
                    {
                        if (GameServer.List_Rooms[RoomPosition].m_dwSlots[x].Active)
                        {
                            pw.UnicodeStr(GameServer.List_Rooms[RoomPosition].m_dwSlots[x].usr.PInfo.m_strNickName);
                            pw.Byte(11);
                        }
                    }
                    pw.HexArray("00 00 00 00 01 01 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 01 00 00 00 00 00 00 00 00 00 00 00 04 42 00 52 00 00 00 00 00 07 00 00 00 00 FF FF FF FF");
                }
                p.SendPacket(PacketCompress.HalfCompress(pw.Get_Packet()), 49);
            }
            catch
            {
                PacketWrite pw = new PacketWrite();
                pw.Int(0);
                p.SendPacket(PacketCompress.HalfCompress(pw.Get_Packet()), 49);
            }
        }

        public void UsersList(Session s, PacketRead r)
        {
            PacketWrite pw = new PacketWrite();
            pw.Int(GameServer.UsersList.Count);
            for (int i = 0; i < GameServer.UsersList.Count; i++)
            {
                pw.Int(GameServer.UsersList[i].PInfo.m_dwUserUID);//id
                pw.UnicodeStr(GameServer.UsersList[i].PInfo.m_strLogin);
                pw.Byte((byte)11);
                pw.HexArray("00 13 00 00 00 00 00");
            }
            s.SendPacket(PacketCompress.HalfCompress(pw.Get_Packet()), 51);
        }

        public void LeaveRoom(Session s, PacketRead r)
        {
            try
            {
                ProcessExit(s);
                PacketWrite pw = new PacketWrite();
                pw.Int(0);
                s.SendPacket(pw, 66);
            }
            catch (Exception ex)
            {
                Log.Write("\n===========:Error:===========\n{0}\n{1}\n=============================n", ex.Message, ex.StackTrace);
                PacketWrite pw = new PacketWrite();
                pw.Int(0);
                s.SendPacket(pw, 66);
            }
        }

        public void ChangeCharInChannel(Session right, PacketRead r)
        {
            try
            {
                ChangeJob(right);
                WorldBossContrib(right);
                CharacterUserInfo(right);
                CharsEquipsInfo(right);
                r.Jump(1);
                byte newCharacter = r.Byte();
                int m_dwCharPosition = right.PCharacters.PositionCharacter(newCharacter, right);
                right.PInfo.m_ucCharType = newCharacter;

                PacketWrite ks = new PacketWrite();                
                ks.Int(0);
                ks.Int(0);
                ks.Byte(newCharacter);
                ks.HexArray("00 00 00 00 00 00 00 00 00 00 00 8C 00 00 78 6E 00 00 00 01 00 00 00 00 00 00 00 00 00 00 78 78 00 00 00 01 00 00 00 00 00 00 00 00 00 00 78 82 00 00 00 01 00 00 00 00 00 00 00 00 00 00 78 8C 00 00 00 01 00 00 00 00 00 00 00 00 00 00 78 96 00 00 00 01 00 00 00 00 00 00 00 00 00 00 78 A0 00 00 00 01 00 00 00 00 00 00 00 00 00 00 78 AA 00 00 00 01 00 00 00 00 00 00 00 00 00 00 78 B4 00 00 00 01 00 00 00 00 00 00 00 00 00 00 78 BE 00 00 00 01 00 00 00 00 00 00 00 00 00 00 78 C8 00 00 00 01 00 00 00 00 00 00 00 00 00 00 78 D2 00 00 00 01 00 00 00 00 00 00 00 00 00 00 78 DC 00 00 00 01 00 00 00 00 00 00 00 00 00 00 78 E6 00 00 00 01 00 00 00 00 00 00 00 00 00 00 78 F0 00 00 00 01 00 00 00 00 00 00 00 00 00 00 78 FA 00 00 00 01 00 00 00 00 00 00 00 00 00 00 79 04 00 00 00 01 00 00 00 00 00 00 00 00 00 00 79 0E 00 00 00 01 00 00 00 00 00 00 00 00 00 00 79 18 00 00 00 01 00 00 00 00 00 00 00 00 00 00 79 22 00 00 00 01 00 00 00 00 00 00 00 00 00 00 79 2C 00 00 00 01 00 00 00 00 00 00 00 00 00 00 79 36 00 00 00 01 00 00 00 00 00 00 00 00 00 00 79 40 00 00 00 01 00 00 00 00 00 00 00 00 00 00 79 4A 00 00 00 01 00 00 00 00 00 00 00 00 00 00 85 C0 00 00 00 01 00 00 00 00 00 00 00 00 00 00 85 CA 00 00 00 01 00 00 00 00 00 00 00 00 00 00 85 D4 00 00 00 01 00 00 00 00 00 00 00 00 00 00 85 DE 00 00 00 01 00 00 00 00 00 00 00 00 00 00 85 E8 00 00 00 01 00 00 00 00 00 00 00 00 00 00 85 F2 00 00 00 01 00 00 00 00 00 00 00 00 00 00 85 FC 00 00 00 01 00 00 00 00 00 00 00 00 00 00 86 06 00 00 00 01 00 00 00 00 00 00 00 00 00 00 86 10 00 00 00 01 00 00 00 00 00 00 00 00 00 00 86 1A 00 00 00 01 00 00 00 00 00 00 00 00 00 00 86 24 00 00 00 01 00 00 00 00 00 00 00 00 00 00 86 2E 00 00 00 01 00 00 00 00 00 00 00 00 00 00 86 38 00 00 00 01 00 00 00 00 00 00 00 00 00 00 86 42 00 00 00 01 00 00 00 00 00 00 00 00 00 00 86 4C 00 00 00 01 00 00 00 00 00 00 00 00 00 01 45 8C 00 00 00 01 00 00 00 00 00 00 00 00 00 01 45 96 00 00 00 01 00 00 00 00 00 00 00 00 00 01 45 A0 00 00 00 01 00 00 00 00 00 00 00 00 00 01 45 AA 00 00 00 01 00 00 00 00 00 00 00 00 00 01 45 B4 00 00 00 01 00 00 00 00 00 00 00 00 00 01 45 BE 00 00 00 01 00 00 00 00 00 00 00 00 00 01 45 C8 00 00 00 01 00 00 00 00 00 00 00 00 00 01 45 D2 00 00 00 01 00 00 00 00 00 00 00 00 00 01 45 DC 00 00 00 01 00 00 00 00 00 00 00 00 00 01 45 E6 00 00 00 01 00 00 00 00 00 00 00 00 00 01 45 F0 00 00 00 01 00 00 00 00 00 00 00 00 00 01 45 FA 00 00 00 01 00 00 00 00 00 00 00 00 00 01 46 04 00 00 00 01 00 00 00 00 00 00 00 00 00 01 46 0E 00 00 00 01 00 00 00 00 00 00 00 00 00 01 46 18 00 00 00 01 00 00 00 00 00 00 00 00 00 01 46 22 00 00 00 01 00 00 00 00 00 00 00 00 00 01 46 2C 00 00 00 01 00 00 00 00 00 00 00 00 00 01 46 36 00 00 00 01 00 00 00 00 00 00 00 00 00 01 46 40 00 00 00 01 00 00 00 00 00 00 00 00 00 01 46 4A 00 00 00 01 00 00 00 00 00 00 00 00 00 01 9B 18 00 00 00 01 00 00 00 00 00 00 00 00 00 01 FF 40 00 00 00 01 00 00 00 00 00 00 00 00 00 01 FF 4A 00 00 00 01 00 00 00 00 00 00 00 00 00 02 1E 9E 00 00 00 01 00 00 00 00 00 00 00 00 00 02 29 A2 00 00 00 01 00 00 00 00 00 00 00 00 00 02 41 08 00 00 00 01 00 00 00 00 00 00 00 00 00 02 CA 56 00 00 00 01 00 00 00 00 00 00 00 00 00 02 CA 60 00 00 00 01 00 00 00 00 00 00 00 00 00 02 CA 6A 00 00 00 01 00 00 00 00 00 00 00 00 00 02 CA 74 00 00 00 01 00 00 00 00 00 00 00 00 00 02 D1 2C 00 00 00 01 00 00 00 00 00 00 00 00 00 02 D1 36 00 00 00 01 00 00 00 00 00 00 00 00 00 02 D1 40 00 00 00 01 00 00 00 00 00 00 00 00 00 02 D1 4A 00 00 00 01 00 00 00 00 00 00 00 00 00 02 E0 54 00 00 00 01 00 00 00 00 00 00 00 00 00 02 E0 5E 00 00 00 01 00 00 00 00 00 00 00 00 00 02 E0 68 00 00 00 01 00 00 00 00 00 00 00 00 00 02 E0 72 00 00 00 01 00 00 00 00 00 00 00 00 00 02 E0 7C 00 00 00 01 00 00 00 00 00 00 00 00 00 02 E0 86 00 00 00 01 00 00 00 00 00 00 00 00 00 03 4A 76 00 00 00 01 00 00 00 00 00 00 00 00 00 03 4A 80 00 00 00 01 00 00 00 00 00 00 00 00 00 03 4A 8A 00 00 00 01 00 00 00 00 00 00 00 00 00 03 4A 94 00 00 00 01 00 00 00 00 00 00 00 00 00 04 89 86 00 00 00 01 00 00 00 00 00 00 00 00 00 04 89 90 00 00 00 01 00 00 00 00 00 00 00 00 00 05 0F 6E 00 00 00 01 00 00 00 00 00 00 00 00 00 05 0F 78 00 00 00 01 00 00 00 00 00 00 00 00 00 05 9A 42 00 00 00 01 00 00 00 00 00 00 00 00 00 06 E2 3A 00 00 00 01 00 00 00 00 00 00 00 00 00 08 33 1A 00 00 00 01 00 00 00 00 00 00 00 00 00 08 33 24 00 00 00 01 00 00 00 00 00 00 00 00 00 09 54 66 00 00 00 01 00 00 00 00 00 00 00 00 00 09 54 70 00 00 00 01 00 00 00 00 00 00 00 00 00 09 54 7A 00 00 00 01 00 00 00 00 00 00 00 00 00 09 54 84 00 00 00 01 00 00 00 00 00 00 00 00 00 09 54 8E 00 00 00 01 00 00 00 00 00 00 00 00 00 0A 1E 28 00 00 00 01 00 00 00 00 00 00 00 00 00 0A 1E 32 00 00 00 01 00 00 00 00 00 00 00 00 00 0C 55 08 00 00 00 01 00 00 00 00 00 00 00 00 00 0C 55 12 00 00 00 01 00 00 00 00 00 00 00 00 00 0D 72 94 00 00 00 01 00 00 00 00 00 00 00 00 00 0D 72 9E 00 00 00 01 00 00 00 00 00 00 00 00 00 0E E9 E4 00 00 00 01 00 00 00 00 00 00 00 00 00 0E E9 EE 00 00 00 01 00 00 00 00 00 00 00 00 00 0E E9 F8 00 00 00 01 00 00 00 00 00 00 00 00 00 0E EA 02 00 00 00 01 00 00 00 00 00 00 00 00 00 0E EA 0C 00 00 00 01 00 00 00 00 00 00 00 00 00 0E EA 16 00 00 00 01 00 00 00 00 00 00 00 00 00 0F 85 98 00 00 00 01 00 00 00 00 00 00 00 00 00 0F 85 A2 00 00 00 01 00 00 00 00 00 00 00 00 00 10 49 60 00 00 00 01 00 00 00 00 00 00 00 00 00 10 49 6A 00 00 00 01 00 00 00 00 00 00 00 00 00 10 6A 3A 00 00 00 01 00 00 00 00 00 00 00 00 00 10 6A 44 00 00 00 01 00 00 00 00 00 00 00 00 00 10 A5 18 00 00 00 01 00 00 00 00 00 00 00 00 00 10 A5 22 00 00 00 01 00 00 00 00 00 00 00 00 00 10 E6 E0 00 00 00 01 00 00 00 00 00 00 00 00 00 10 E6 EA 00 00 00 01 00 00 00 00 00 00 00 00 00 12 6A A6 00 00 00 01 00 00 00 00 00 00 00 00 00 12 6A B0 00 00 00 01 00 00 00 00 00 00 00 00 00 12 6A BA 00 00 00 01 00 00 00 00 00 00 00 00 00 12 6A C4 00 00 00 01 00 00 00 00 00 00 00 00 00 12 6A CE 00 00 00 01 00 00 00 00 00 00 00 00 00 12 6A D8 00 00 00 01 00 00 00 00 00 00 00 00 00 12 9F 26 00 00 00 01 00 00 00 00 00 00 00 00 00 12 9F 30 00 00 00 01 00 00 00 00 00 00 00 00 00 12 9F 3A 00 00 00 01 00 00 00 00 00 00 00 00 00 12 9F 44 00 00 00 01 00 00 00 00 00 00 00 00 00 12 9F 4E 00 00 00 01 00 00 00 00 00 00 00 00 00 13 8C 24 00 00 00 01 00 00 00 00 00 00 00 00 00 13 8C 2E 00 00 00 01 00 00 00 00 00 00 00 00 00 13 8C 38 00 00 00 01 00 00 00 00 00 00 00 00 00 13 8C 42 00 00 00 01 00 00 00 00 00 00 00 00 00 13 8C 4C 00 00 00 01 00 00 00 00 00 00 00 00 00 13 8C 56 00 00 00 01 00 00 00 00 00 00 00 00 00 13 8C 60 00 00 00 01 00 00 00 00 00 00 00 00 00 13 8C 6A 00 00 00 01 00 00 00 00 00 00 00 00 00 13 8C 74 00 00 00 01 00 00 00 00 00 00 00 00 00 13 8C 7E 00 00 00 01 00 00 00 00 00 00 00 00 00 13 8C 88 00 00 00 01 00 00 00 00 00 00 00 00 00 13 8C 92 00 00 00 01 00 00 00 00 00 00 00 00 00 00 00 02 00 14 DC DC 00 00 00 01 3B 9C 50 A1 00");
                ks.Int(500);//talvez rank
                ks.HexArray("35 8E F2 5A 34 3D 72 00 00 00 00 00 14 DC E6 00 00 00 01 3B 9C 50 A2 00 00 00 00 5A 35 8E F2 5A 34 3D 72 00 00 00 00");
                Serializables.SerializeStages(right, ks);
                ks.Byte(0);
                ks.Int(right.PCharacters.CharInfo[m_dwCharPosition].Equipements.Count);
                for (int x = 0; x < right.PCharacters.CharInfo[m_dwCharPosition].Equipements.Count; x += 1)
                {
                    ks.Int(right.PCharacters.CharInfo[m_dwCharPosition].Equipements[x].ItemID);
                    ks.Int(0);
                    ks.Int(right.PCharacters.CharInfo[m_dwCharPosition].Equipements[x].ItemUID);
                    ks.HexArray("00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00");
                }
                ks.HexArray("00 00 00 00 00 00 00 00 00 00 00 00 00 01 00 00 00 00 00 00 00 00");
                ks.Int(right.PInfo.m_dwInvetoryInfo);
                ks.HexArray("00 00 00 64 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 FF FF 00 00 00 00 00 00 00 00 00 00 40 47 1C");                
                right.SendPacket(ks, 1621);
                CharsEquipsInfo(right);
            }
            catch (Exception ex)
            {
                Log.Write("\n===========:Error:===========\n{0}\n{1}\n=============================n", ex.Message, ex.StackTrace);
            }
        }

        private void ChangeJob(Session right)
        {
            PacketWrite ks = new PacketWrite();
            ks.Int(0);
            right.SendPacket(ks, 185);
        }

        private void CharacterUserInfo(Session right)
        {
            PacketWrite ks = new PacketWrite();
            ks.HexArray("00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 02 00 00 00 11 00 00 00 12 00 00 00 00");
            right.SendPacket(ks, 1584);
        }

        public void CharsEquipsInfo(Session right)
        {
            PacketWrite pw = new PacketWrite();
            pw.UnicodeStr(right.PInfo.m_strLogin);
            pw.Byte(right.PInfo.m_ucCharType);
            pw.Int(right.PCharacters.CharInfo.Length);
            for (int x = 0; x < right.PCharacters.CharInfo.Length; x++)
            {
                pw.Byte(right.PCharacters.CharInfo[x].CharType);
                pw.Int(right.PCharacters.CharInfo[x].Equipements.Count);
                for (int x2 = 0; x2 < right.PCharacters.CharInfo[x].Equipements.Count; x2++)
                {
                    pw.Int(right.PCharacters.CharInfo[x].Equipements[x2].ItemID);
                    pw.Int(1);
                    pw.Int(right.PCharacters.CharInfo[x].Equipements[x2].ItemUID);
                    pw.HexArray("00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00");
                }
                pw.Int(0);
                Serializables.KPetInfo(right, pw, x);
                pw.HexArray("00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00");
            }            
            pw.Int(right.PInfo.m_dwUserUID);
            pw.Short(0);
            right.SendPacket(pw, 96);
            if (right.PInfo.CurRoom != null)
                right.PInfo.CurRoom.SendForAllPlayersInRoom(pw, 96);
        }

        public void WorldBossContrib(Session right)
        {
            PacketWrite ks = new PacketWrite();
            ks.HexArray("FF FF FF 9D");
            ks.Int(right.PInfo.WorldBossPoint);
            right.SendPacket(ks, 1736);            
        }

        public void CreateRoom(Session p, PacketRead r)
        {
            //===================
            // 0; OK..
            // 1; 현재 채널에 더이상의 방을 만들 수 없음.
            // 2; 존재하지 않는 캐릭터가 선택되었습니다.
            // 3; 입장할수 있는 올바른 레벨이 아닙니다.
            try
            {
                int m_dwOK = 0;
                Serializables.KRoomInfo(p, r);
                byte[] restant = r.Buffer_Array_Bytes(78);
                Serializables.KUserInfo(p, r);

                PacketWrite pw = new PacketWrite();
                pw.Int(m_dwOK);
                Serializables.KUserInfo(p, pw);
                Serializables.SerializeStages(p, pw);
                pw.HexArray("01 01 00 00 00 00 00 00 00 00 00 00 00 00");
                Serializables.m_roomCharacterInfo(p, pw);
                pw.HexArray("00 00 00 02 6B 00 A8 C0 E4 02 2D B1 00 00 00 01 7F 1A 00 00 00 00 00 00 00 00 00 00 00 01 00 00 00 00 00 00 E5 6A 00 00 00 00 00 D9 92 03 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 04 5A 00 5A 00 0B 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 01 00 00 00 01 00 00 00 01 00 00 00 00 00 00 00 D2 F0 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 D2 F0 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 01 00 00 00 00 01 00 00 00 00 85 89 82 21 00 00 00 00 00 00 00 00 01 00 00 00 01 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00");
                Serializables.KRoomInfo(p, pw);
                pw.ArrayBytes(restant);

                p.SendPacket(pw, 57);
                GameServer.List_Rooms.Add(p.PInfo.CurRoom.m_usRoomID, p.PInfo.CurRoom);
                GameServer.UsersList.Remove(p);
                p.PInfo.m_cStatus = "ROOM";
            }
            catch (Exception ex)
            {
                Log.Write("\n===========:Error:===========\n{0}\n{1}\n=============================n", ex.Message, ex.StackTrace);
                PacketWrite pw = new PacketWrite();
                pw.Int(1);
                pw.UnicodeStr(p.PInfo.m_strLogin);
                pw.Int(p.PInfo.m_dwUserUID);
                pw.UnicodeStr(p.PInfo.m_strNickName);
                p.SendPacket(pw, 57);
            }
        }

        public static void ProcessExit(Session p)
        {
            try
            {
                if (p.PInfo.CurRoom.m_usUsers() >= 2)
                {
                    int MySlot = p.PInfo.CurRoom.GetPositionPlayer(p);
                    InformLeaveRoom(p);

                    if (p.PInfo.CurRoom.m_dwSlots[MySlot].Leader == true)
                    {
                        for (int i = 0; i < p.PInfo.CurRoom.m_usMaxUsers; i++)
                        {
                            if (p.PInfo.CurRoom.m_dwSlots[i].Active == true && p.PInfo.CurRoom.m_dwSlots[i].usr != p)
                            {
                                p.PInfo.CurRoom.m_dwSlots[i].Leader = true;
                                ChangeNewLeader(p, i);
                                break;
                            }
                        }
                    }

                    p.PInfo.CurRoom.m_dwSlots[MySlot].Active = false;
                    p.PInfo.CurRoom.m_dwSlots[MySlot].Open = true;
                    p.PInfo.CurRoom.m_dwSlots[MySlot].usr = null;
                    p.PInfo.CurRoom.m_dwSlots[MySlot].Status = 0;
                    p.PInfo.CurRoom.m_dwSlots[MySlot].AFK = false;
                    p.PInfo.CurRoom.m_dwSlots[MySlot].Leader = false;
                }
                else
                {
                    GameServer.List_Rooms.Remove(p.PInfo.CurRoom.m_usRoomID);
                }

                GameServer.UsersList.Add(p);
                p.PInfo.CurRoom = null;
                p.PInfo.m_cStatus = "LOBBY";
            }

            catch (Exception ex)
            {
                Log.Write("\n===========:Error:===========\n{0}\n{1}\n=============================n", ex.Message, ex.StackTrace);
            }
        }

        private static void ChangeNewLeader(Session player, int NewLeaderPosition)
        {
            try
            {
                PacketWrite WB = new PacketWrite();
                WB.Int(player.PInfo.CurRoom.m_dwSlots[NewLeaderPosition].usr.PInfo.m_dwUserUID);
                WB.Byte(1);
                for (int i = 0; i < player.PInfo.CurRoom.m_usMaxUsers; i++)
                {
                    if (player.PInfo.CurRoom.m_dwSlots[i].Active == true)
                    {
                        player.PInfo.CurRoom.m_dwSlots[i].usr.SendPacket(WB, 129);
                    }
                }
            }
            catch (Exception ex)
            {
                Log.Write("\n===========:Error:===========\n{0}\n{1}\n=============================n", ex.Message, ex.StackTrace);
            }
        }

        private static void InformLeaveRoom(Session player)
        {
            try
            {
                PacketWrite WB = new PacketWrite();
                WB.UnicodeStr(player.PInfo.m_strLogin);
                WB.Int(0);
                WB.Int(player.PInfo.m_dwUserUID);
                WB.Int(3);
                for (int i = 0; i < player.PInfo.CurRoom.m_usMaxUsers; i++)
                {
                    if (player.PInfo.CurRoom.m_dwSlots[i].Active == true)
                    {
                        player.PInfo.CurRoom.m_dwSlots[i].usr.SendPacket(WB, 130);
                    }
                }
            }
            catch (Exception ex)
            {
                Log.Write("\n===========:Error:===========\n{0}\n{1}\n=============================n", ex.Message, ex.StackTrace);
            }
        }

        private void RoomInfo(Session p, Room room)
        {
            PacketWrite pw = new PacketWrite();
            pw.UShort(room.m_usRoomID);
            pw.UnicodeStr(room.m_strRoomName);
            pw.Byte(1);
            if (room.m_strRoomPasswd.Length > 0)
                pw.Bool(true);
            else
                pw.Bool(false);

            pw.UnicodeStr(room.m_strRoomPasswd);
            pw.Short(room.m_usUsers());
            pw.Short(room.m_usMaxUsers);
            pw.Bool(room.m_bPlaying);
            pw.Byte(11);
            pw.Byte(room.m_cGameCategory);
            pw.Int(room.m_iGameMode);
            pw.Int(room.m_iSubGameMode);
            pw.Bool(room.m_bRandomableMap);
            pw.Int(room.m_iMapID);
            pw.Int(room.m_iP2PVersion);
            for (short j = 0; j < room.m_usMaxUsers; j++)

                pw.Bool(room.m_dwSlots[j].Open);

            if (room.m_usMaxUsers == 4)
            {
                pw.Bool(false);
                pw.Bool(false);
            }
            pw.Int(-1);
            pw.Int(room.m_cDifficulty);
            pw.HexArray("00 00 00 00 01");
            pw.WriteIP(GameServer.m_dwIP);
            pw.UShort(GameServer.m_usURelayServerPort);
            pw.WriteIP(GameServer.m_dwIP);
            pw.UShort(GameServer.m_usTRelayServerPort);
            pw.HexArray("01 00 01 00 00 01 2C 00 00 00 14 00 02 26 24 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 04 42 00 52 00 00 06 01 00 00 00 00 00 00 00 06 00 00 00 00 FF FF FF FF");

            p.SendPacket(pw, 1558);
        }

        public void RoomMyInfoDivide(Session p, PacketRead r)
        {
            r.Jump(6);
            ushort roomid = (ushort)r.Get_Short();
            Room room = RoomExists(roomid);
            int TempCount = -1;
            for (int i = 0; i < room.m_usMaxUsers; i++)
            {
                if (room.m_dwSlots[i].Active == false)
                    continue;
                TempCount++;
                if (room.m_dwSlots[i].usr == p)
                {
                    PacketWrite pw = new PacketWrite();
                    pw.Int(0);
                    pw.Int(room.m_usUsers());
                    pw.Int(TempCount);
                    pw.UnicodeStr(room.m_dwSlots[i].usr.PInfo.m_strLogin);
                    pw.Int(room.m_dwSlots[i].usr.PInfo.m_dwUserUID);
                    pw.UnicodeStr(room.m_dwSlots[i].usr.PInfo.m_strNickName);
                    pw.Int(i);
                    pw.Byte(room.m_dwSlots[i].usr.PInfo.m_ucCharType);
                    pw.Int(0);
                    pw.HexArray("00 FF 00 FF 00 FF 00 00 00 00");
                    pw.Byte((byte)room.GetTeam(room.m_dwSlots[i].usr));
                    pw.HexArray("00 00 00 00 64 00 00");
                    pw.Int(room.m_dwSlots[i].usr.PInfo.m_iGamePoint);
                    pw.Short(0);
                    Serializables.SerializeStages(room.m_dwSlots[i].usr, pw);
                    if (room.m_sSearchLeader() == room.m_dwSlots[i].usr)
                    {
                        pw.Bool(true);
                    }
                    else
                    {
                        pw.Bool(false);
                    }
                    pw.HexArray("01 00 00 00 00 00 00 00 00 00 00 00 00");
                    Serializables.m_roomCharacterInfo(room.m_dwSlots[i].usr, pw);
                    pw.HexArray("00 00 00 02 76 19 A8 C0 10 0C D6 BA 00 00 00 01 7F 39 00 00 00 00 00 00 00 00 00 00 00");
                    pw.Byte(room.m_dwSlots[i].Status);
                    pw.HexArray("00 00 00 00 00 12 C8 F2 00 00 00 00 00 D6 D9 BE 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 04 42 00 52 00 0B 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 01 00 00 00 01 00 00 00 01 00 00 00 00 00 00 00 D2 F0 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 D2 F0 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 01 00 00 00 00 01 00 00 00 00 00 06 11 16 00 00 00 00 00 00 00 00 46 00 00 00 01 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 97 D8 00 00 01 A0 04 7B 00 00 00 00 00 00 00 00");

                    p.SendPacket(pw, 1472);
                    break;
                }
            }
        }

        private void RoomInfoDivide(Session p, Room room)
        {
            int TempCount = -1;
            for (int i = 0; i < room.m_usMaxUsers; i++)
            {
                if (room.m_dwSlots[i].Active == false)
                    continue;

                TempCount++;

                PacketWrite pw = new PacketWrite();
                pw.Int(0);
                pw.Int(room.m_usUsers());
                pw.Int(TempCount);
                pw.UnicodeStr(room.m_dwSlots[i].usr.PInfo.m_strLogin);
                pw.Int(room.m_dwSlots[i].usr.PInfo.m_dwUserUID);
                pw.UnicodeStr(room.m_dwSlots[i].usr.PInfo.m_strNickName);
                pw.Int(i);
                pw.Byte(room.m_dwSlots[i].usr.PInfo.m_ucCharType);
                pw.Int(0);
                pw.HexArray("00 FF 00 FF 00 FF 00 00 00 00");
                pw.Byte((byte)room.GetTeam(room.m_dwSlots[i].usr));
                pw.HexArray("00 00 00 00 64 00 00");
                pw.Int(room.m_dwSlots[i].usr.PInfo.m_iGamePoint);
                pw.Short(0);
                Serializables.SerializeStages(room.m_dwSlots[i].usr, pw);
                if (room.m_sSearchLeader() == room.m_dwSlots[i].usr)
                {
                    pw.Bool(true);
                }
                else
                {
                    pw.Bool(false);
                }
                pw.HexArray("01 00 00 00 00 00 00 00 00 00 00 00 00");
                Serializables.m_roomCharacterInfo(room.m_dwSlots[i].usr, pw);
                pw.HexArray("00 00 00 02 76 19 A8 C0 10 0C D6 BA 00 00 00 01 7F 39 00 00 00 00 00 00 00 00 00 00 00");
                pw.Byte(room.m_dwSlots[i].Status);
                pw.HexArray("00 00 00 00 00 12 C8 F2 00 00 00 00 00 D6 D9 BE 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 04 42 00 52 00 0B 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 01 00 00 00 01 00 00 00 01 00 00 00 00 00 00 00 D2 F0 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 D2 F0 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 01 00 00 00 00 01 00 00 00 00 00 06 11 16 00 00 00 00 00 00 00 00 46 00 00 00 01 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 97 D8 00 00 01 A0 04 7B 00 00 00 00 00 00 00 00");
                p.SendPacket(pw, 1472);
            }
        }

        public void EnterRoom(Session p, PacketRead r)
        {
            try
            {                
                r.Jump(4);
                ushort roomID = (ushort)r.Get_Short();
                string Password = r.UnicodeString();
                r.Jump(5);
                string userLogin = r.UnicodeString();
                r.Jump(4);
                string userNick = r.UnicodeString();
                r.Jump(4);
                p.PInfo.m_ucCharType = r.Byte();

                Room room = RoomExists(roomID);

                if (room == null)
                {
                    PacketWrite pw2 = new PacketWrite();
                    pw2.Int(1);
                    p.SendPacket(pw2, 54);
                    return;
                }
                else if (room.m_abTotalSlotsOpen() == 0)
                {
                    PacketWrite pw2 = new PacketWrite();
                    pw2.Int(1);
                    p.SendPacket(pw2, 54);
                    return;
                }
                else if (room.m_bPlaying == true)
                {
                    PacketWrite pw2 = new PacketWrite();
                    pw2.Int(1);
                    p.SendPacket(pw2, 54);
                    return;
                }
                else if (room.m_strRoomPasswd != Password)
                {
                    PacketWrite pw2 = new PacketWrite();
                    pw2.Int(1);
                    p.SendPacket(pw2, 54);
                    return;
                }
                int pos = 0;
                if (room.m_cGameCategory == 2)
                {
                    pos = room.m_abSlotOpen();
                }
                else
                {
                    byte Team1 = 0, Team2 = 0;
                    int EmptyPos1 = -1, EmptyPos2 = -1;
                    for (int i = 0; i < 3; i++)
                    {
                        if (room.m_dwSlots[i].Active == true)
                            Team1++;
                        if (EmptyPos1 == -1 && room.m_dwSlots[i].Open == true)
                            EmptyPos1 = i;
                    }
                    for (int i = 3; i < 6; i++)
                    {
                        if (room.m_dwSlots[i].Active == true)
                            Team2++;
                        if (EmptyPos2 == -1 && room.m_dwSlots[i].Open == true)
                            EmptyPos2 = i;
                    }

                    pos = EmptyPos1;
                    if (Team1 >= Team2)
                        pos = EmptyPos2;
                }
                room.m_dwSlots[pos].Active = true;
                room.m_dwSlots[pos].usr = p;
                room.m_dwSlots[pos].Open = false;
                room.m_dwSlots[pos].AFK = false;
                room.m_dwSlots[pos].Deaths = 0;
                room.m_dwSlots[pos].Kills = 0;
                room.m_dwSlots[pos].Win = 0;
                room.m_dwSlots[pos].PositionSlot = pos;
                room.m_dwSlots[pos].Leader = false;

                p.PInfo.CurRoom = room;
                p.PInfo.m_cStatus = "ROOM";
                GameServer.UsersList.Remove(p);

                PacketWrite pw = new PacketWrite();
                pw.UnicodeStr(p.PInfo.m_strLogin);
                pw.Int(p.PInfo.m_dwUserUID);
                pw.UnicodeStr(p.PInfo.m_strNickName);
                pw.Int(pos);
                pw.Byte(p.PInfo.m_ucCharType);
                pw.Int(0);
                pw.HexArray("00 FF 00 FF 00 FF 00 00 00 00");
                pw.Byte((byte)(pos /3));
                pw.HexArray("01 00 00 00 64 00 00");
                pw.Int(p.PInfo.m_iGamePoint);
                pw.Short(0);
                Serializables.SerializeStages(p, pw);
                if (room.m_sSearchLeader() == p)
                {
                    pw.Bool(true);
                }
                else
                {
                    pw.Bool(false);
                }
                pw.HexArray("01 00 00 00 00 00 00 00 00 00 00 00 00");
                Serializables.m_roomCharacterInfo(p, pw);
                pw.HexArray("00 00 00 02 76 19 A8 C0 10 0C D6 BA 00 00 00 01 7F 39 00 00 00 00 00 00 00 00 00 00 00");
                pw.Byte(room.m_dwSlots[pos].Status);
                pw.HexArray("00 00 00 00 00 12 C8 F2 00 00 00 00 00 D6 D9 BE 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 04 42 00 52 00 0B 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 01 00 00 00 01 00 00 00 01 00 00 00 00 00 00 00 D2 F0 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 D2 F0 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 01 00 00 00 00 01 00 00 00 00 00 06 11 16 00 00 00 00 00 00 00 00 46 00 00 00 01 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 97 D8 00 00 01 A0 04 7B 00 00 00 00 00 00 00 00");

                room.SendForAllPlayersInRoom(pw, 54);
                RoomInfo(p, room);
                RoomInfoDivide(p, room); 
            }
            catch (Exception ex)
            {
                Log.Write("{0}", ex.StackTrace);
                PacketWrite pw = new PacketWrite();
                pw.Int(1);
                p.SendPacket(pw, 54);
                p.PInfo.CurRoom = null;
                p.PInfo.m_cStatus = "LOBBY";
                GameServer.UsersList.Add(p);
            }
        }

    }
}