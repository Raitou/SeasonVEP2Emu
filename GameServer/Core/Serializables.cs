using Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace libcomservice.Core
{
    public static class Serializables
    {
        public static int m_timeStamp()
        {
            DateTime d1;
            DateTime d2;
            TimeSpan d3;
            int timeStamp = 0;

            d1 = DateTime.Parse("1970-01-01 00:00:00");
            d2 = DateTime.Now;
            d3 = d2 - d1;
            timeStamp = Convert.ToInt32(d3.TotalSeconds);
            return timeStamp;
        }

        public static void m_mapCharacterInfo(Session right, PacketWrite ks)
        {
            ks.Int(right.PCharacters.CharInfo.Length);
            for (int x = 0; x < right.PCharacters.CharInfo.Length; x++)
            {
                ks.Byte(right.PCharacters.CharInfo[x].CharType);
                ks.Byte(right.PCharacters.CharInfo[x].CharType);
                ks.Int(0);
                ks.Byte(right.PCharacters.CharInfo[x].Promotion);
                ks.Byte(right.PCharacters.CharInfo[x].Promotion);
                ks.Long(right.PCharacters.CharInfo[x].Exp);
                ks.Int(right.PCharacters.CharInfo[x].Win);
                ks.Int(right.PCharacters.CharInfo[x].Lose);
                ks.Int(right.PCharacters.CharInfo[x].Win);
                ks.Int(right.PCharacters.CharInfo[x].Lose);
                ks.Long(right.PCharacters.CharInfo[x].Exp);
                ks.Int(right.PCharacters.CharInfo[x].Level);
                ks.Int(right.PCharacters.CharInfo[x].Equipements.Count);
                for (int y = 0; y < right.PCharacters.CharInfo[x].Equipements.Count; y++)
                {
                    ks.Int(right.PCharacters.CharInfo[x].Equipements[y].ItemID);
                    ks.Int(1);
                    ks.Int(right.PCharacters.CharInfo[x].Equipements[y].ItemUID);
                    ks.HexArray("00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00");
                }
                ks.Int(255);
                ks.HexArray("00 00 00 A0 00 00 00 01 00 00 00 00 00 00 00 00 00 00 00 0B 18 00 00 00 00 00 00 0B 18 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00");
                ks.Int(7);
                ks.Int(7);
                ks.HexArray("00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00");
                ks.Int(500);
                ks.Int(x);
                ks.HexArray("00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 FF FF");
                ks.Int(right.PInfo.m_iGamePoint);
                ks.Int(right.PInfo.m_iGamePoint);
                ks.Int(right.PInfo.m_iLifePoint);
                ks.Int(0);
                ks.Int(260);
                ks.Int(260);
                ks.Int(right.PCharacters.CharInfo[x].LookItens.Count);
                for (int y = 0; y < right.PCharacters.CharInfo[x].LookItens.Count; y++)
                {
                    ks.UInt(right.PCharacters.CharInfo[x].LookItens[y].ItemID);
                    ks.Int(1);
                    ks.UInt(right.PCharacters.CharInfo[x].LookItens[y].ItemUID);
                    ks.HexArray("00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00");
                }
                ks.Int(0);
                ks.Int(0);
            }
        }

        public static void m_roomCharacterInfo(Session right, PacketWrite ks)
        {
            ks.Int(right.PCharacters.CharInfo.Length);
            for (int x = 0; x < right.PCharacters.CharInfo.Length; x++)
            {
                ks.Byte(right.PCharacters.CharInfo[x].CharType);
                ks.Int(0);
                ks.Byte(right.PCharacters.CharInfo[x].Promotion);
                ks.Byte(right.PCharacters.CharInfo[x].Promotion);
                ks.Long(right.PCharacters.CharInfo[x].Exp);
                ks.Int(right.PCharacters.CharInfo[x].Level);
                ks.Int(right.PCharacters.CharInfo[x].Win);
                ks.Int(right.PCharacters.CharInfo[x].Lose);
                ks.Int(right.PCharacters.CharInfo[x].Equipements.Count);
                for (int y = 0; y < right.PCharacters.CharInfo[x].Equipements.Count; y++)
                {
                    ks.Int(right.PCharacters.CharInfo[x].Equipements[y].ItemID);
                    ks.Int(1);
                    ks.Int(right.PCharacters.CharInfo[x].Equipements[y].ItemUID);
                    ks.HexArray("00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00");
                }

                ks.Int(0);
                ks.Int(right.PCharacters.CharInfo[x].LookItens.Count);
                for (int y = 0; y < right.PCharacters.CharInfo[x].LookItens.Count; y++)
                {
                    ks.UInt(right.PCharacters.CharInfo[x].LookItens[y].ItemID);
                    ks.Int(1);
                    ks.UInt(right.PCharacters.CharInfo[x].LookItens[y].ItemUID);
                    ks.HexArray("00 02 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00");
                }
                KPetInfo(right, ks, x);
                ks.HexArray("00 00 00 00 00 00 00 00 04 00 00 00 00 00 01 00 00 00 00 02 00 00 00 00 03");
                ks.Int(0);
                ks.HexArray("00 00 00 8C 00 00 00 A0 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00");
                ks.Int(right.PCharacters.CharInfo[x].Promotion + 2);
                ks.Byte(right.PCharacters.CharInfo[x].CharType);
                ks.Byte(255);
                ks.Int(0);
                for (byte y = 0; y < right.PCharacters.CharInfo[x].Promotion + 1; y++)
                {
                    ks.Byte(right.PCharacters.CharInfo[x].CharType);
                    ks.Byte(y);
                    ks.Int(0);
                }
                ks.HexArray("00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00");
                ks.HexArray("00 00 04 E2 00 00 04 E2 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 07");
                ks.Int(x);
                ks.Int(right.PInfo.m_iGamePoint);
                ks.Int(right.PInfo.m_iLifePoint);
                ks.HexArray("00 00 00 00 00 00 00 01 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00");
            }
        }

        public static void KPetInfo(Session right, PacketWrite ks, int obj)
        {
            ks.Int(0);//Pet is New
                        
            ks.Int(right.PCharacters.CharInfo[obj].Pets[0].m_dwUID);
            ks.Int(right.PCharacters.CharInfo[obj].Pets[0].m_dwID);
            ks.Str(right.PCharacters.CharInfo[obj].Pets[0].m_strName);
            ks.Int(right.PCharacters.CharInfo[obj].Pets[0].m_mapInitExp);
            for (byte y = 0; y < right.PCharacters.CharInfo[obj].Pets[0].m_mapInitExp; y += 1)
            {
                ks.Byte(y);
                ks.Int(100);
            }
            ks.UInt(right.PCharacters.CharInfo[obj].Pets[0].m_dwEXP);
            ks.Int(right.PCharacters.CharInfo[obj].Pets[0].m_iLevel);
            ks.Byte(right.PCharacters.CharInfo[obj].Pets[0].m_cPromotion);
            ks.Int(right.PCharacters.CharInfo[obj].Pets[0].m_nHatchingID);
            ks.Int(right.PCharacters.CharInfo[obj].Pets[0].m_iInitSatiation);
            ks.Int(right.PCharacters.CharInfo[obj].Pets[0].m_iSatiation);
            ks.Int(right.PCharacters.CharInfo[obj].Pets[0].Slot1.Count);//00 00 00 01
            for (int y = 0; y < right.PCharacters.CharInfo[obj].Pets[0].Slot1.Count; y += 1)
            {
                ks.Int(right.PCharacters.CharInfo[obj].Pets[0].Slot1[y].AtkID);
                ks.Int(1);
                ks.UInt(right.PCharacters.CharInfo[obj].Pets[0].Slot1[y].AtkUID);
                ks.Byte(0);
            }
            ks.Int(right.PCharacters.CharInfo[obj].Pets[0].Slot2.Count);//00 00 00 01
            for (int y = 0; y < right.PCharacters.CharInfo[obj].Pets[0].Slot2.Count; y += 1)
            {
                ks.Int(right.PCharacters.CharInfo[obj].Pets[0].Slot2[y].AtkID);
                ks.Int(1);
                ks.UInt(right.PCharacters.CharInfo[obj].Pets[0].Slot2[y].AtkUID);
                ks.Byte(0);
            }
            ks.HexArray("00 00 00 00 00 00 00 00 00 00 00 00 FF");
            ks.Byte(right.PCharacters.CharInfo[obj].CharType);
        }

        public static void SerializeStages(Session right, PacketWrite ks)
        {
            int Position = 0;
            Position = ks.Get_Position();
            int StageNum = 0;
            ks.Int(85);
            ks.Int(7);
            ks.HexArray("00 00 00");
            ks.Byte(1);
            ks.Byte(1);
            ks.Byte(1);
            ks.HexArray("00 00 00 00 00 00 00 00 00 00");
            for (int i = 8; i <= 106; i++)
            {
                StageNum = 0;
                for (int j = 0; j < right.PStages.StageList.Count; j++)
                {
                    if (right.PStages.StageList[j].StageID == i)
                    {
                        if (right.PStages.StageList[j].StageID == 28) continue;
                        if ((right.PStages.StageList[j].StageID > 30) && (right.PStages.StageList[j].StageID < 36)) continue;
                        if ((right.PStages.StageList[j].StageID == 37) || (right.PStages.StageList[j].StageID == 38)) continue;
                        if ((right.PStages.StageList[j].StageID == 65) || (right.PStages.StageList[j].StageID == 66)) continue;
                        if (right.PStages.StageList[j].StageID == 77) continue;
                        if ((right.PStages.StageList[j].StageID == 96) || (right.PStages.StageList[j].StageID == 97)) continue;
                        if ((right.PStages.StageList[j].StageID == 104) || (right.PStages.StageList[j].StageID == 105)) continue;

                        ks.Int(right.PStages.StageList[j].StageID);
                        ks.HexArray("00 00 00");
                        ks.Byte(right.PStages.StageList[j].DificutyLv1);
                        ks.Byte(right.PStages.StageList[j].DificutyLv2);
                        ks.Byte(right.PStages.StageList[j].DificutyLv3);
                        ks.HexArray("00 00 00 00 00 00 00 00 00 00");
                        StageNum = 1;
                        break;
                    }
                }
                if (StageNum == 0)
                {
                    if (i == 28) continue;
                    if ((i > 30) && (i < 36)) continue;
                    if ((i == 37) || (i == 38)) continue;
                    if ((i == 65) || (i == 66)) continue;
                    if (i == 77) continue;
                    if ((i == 96) || (i == 97)) continue;
                    if ((i == 104) || (i == 105)) continue;

                    ks.Int(i);
                    ks.HexArray("00 00 00 00 00 00 00 00 00 00 00 00 00 00 00");
                }
            }
        }

        internal static void SendForAllPlayers(PacketWrite ks, short Opcode)
        {
            for (int i = 0; i < GameServer.UsersList.Count; i++)
            {
                GameServer.UsersList[i].SendPacket(ks, Opcode);
            }
            foreach (ushort RoomPosition in GameServer.List_Rooms.Keys)
            {
                for (int i = 0; i < GameServer.List_Rooms[RoomPosition].m_usMaxUsers; i++)
                {
                    if (GameServer.List_Rooms[RoomPosition].m_dwSlots[i].Active == true)
                    {
                        GameServer.List_Rooms[RoomPosition].m_dwSlots[i].usr.SendPacket(ks, Opcode);
                    }
                }
            }
        }

        public static void KInDoorItemInfo(int m_dwID, int m_dwUID,PacketWrite ks)
        {
            ks.Int(m_dwID);
            ks.Int(1);
            ks.Int(m_dwID);
            ks.HexArray("00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00");
        }

        public static void KEquipItemInfo(Session right, PacketRead rs, int m_vecCharInfo)
        {
            for (int i = 0; i < m_vecCharInfo; i += 1)
            {
                byte m_cCharIndex = rs.Byte();
                int m_vecEquipInfo = rs.Int();
                int m_dwCharPosition = right.PCharacters.PositionCharacter(m_cCharIndex, right);
                for (int x = 0; x < m_vecEquipInfo; x += 1)
                {
                    int m_dwID = rs.Int();
                    int m_dwUnk = rs.Int();
                    int m_dwUID = rs.Int();
                    byte m_cUpgrade = rs.Byte();
                    byte m_cGrade = rs.Byte();
                    int m_dwLevel = rs.Int();
                    byte[] test = rs.Buffer_Array_Bytes(9);
                    int m_dwAtrribs = rs.Int();
                    for (int x2 = 0; x2 < m_dwAtrribs; x2++)
                    {
                        rs.Jump(7);
                    }
                    right.PCharacters.AddEquipment(m_dwID, m_dwUID, m_dwCharPosition, right);
                    //Log.Write("Character:{0} Equips: {1} Current Item:{2} Atribbs:{3}", m_cCharIndex, m_vecEquipInfo, m_dwID, m_dwAtrribs);
                }
                int m_vecDelInfo = rs.Int();
                for (int x = 0; x < m_vecDelInfo; x += 1)
                {
                    int m_dwID = rs.Int();
                    int m_dwUnk = rs.Int();
                    int m_dwUID = rs.Int();
                    byte m_cUpgrade = rs.Byte();
                    byte m_cGrade = rs.Byte();
                    int m_dwLevel = rs.Int();
                    byte[] test = rs.Buffer_Array_Bytes(9);
                    int m_dwAtrribs = rs.Int();
                    for (int x2 = 0; x2 < m_dwAtrribs; x2++)
                    {
                        rs.Jump(7);
                    }
                    right.PCharacters.RemoveEquipment(m_dwID, m_dwCharPosition, right);                    
                }
                int m_dwPetIsNew = rs.Int();
                int m_dwPetUID = rs.Int();                
                int m_dwPetID = rs.Int();
                string m_strPetName = rs.String();
                int m_mapInitExp = rs.Int();
                for (int x = 0; x < m_mapInitExp; x++)
                {
                    rs.Byte();
                    rs.Int();
                }
                uint m_dwExp = rs.UInt();
                int m_iLevel = rs.Int();
                byte m_cPromotion = rs.Byte();
                int m_nHatchingID = rs.Int();
                int m_iInitSatiation = rs.Int();
                int m_iSatiation = rs.Int();
                uint m_vecEquipItem = rs.UInt();
                right.PCharacters.CharInfo[m_dwCharPosition].Pets[0].Slot1.Clear();                
                for (uint x = 0; x < m_vecEquipItem; x++)
                {                    
                    uint m_dwPetAtkId = rs.UInt();
                    rs.Jump(4);
                    uint m_dwPetAtkUID = rs.UInt();
                    rs.Byte();
                    right.PCharacters.AddAtkPetSlot1((int)m_dwPetAtkId, m_dwPetAtkUID, m_dwCharPosition, right);                    
                }
                uint m_vecEquipItem2 = rs.UInt();
                right.PCharacters.CharInfo[m_dwCharPosition].Pets[0].Slot2.Clear();
                for (uint x = 0; x < m_vecEquipItem2; x++)
                {
                    uint m_dwPetAtkId = rs.UInt();
                    rs.Jump(4);
                    uint m_dwPetAtkUID = rs.UInt();
                    rs.Byte();
                    right.PCharacters.AddAtkPetSlot2((int)m_dwPetAtkId, m_dwPetAtkUID, m_dwCharPosition, right);
                }
                byte[] restantPet = rs.Buffer_Array_Bytes(14);
                right.PCharacters.CharInfo[m_dwCharPosition].Pets[0].m_dwUID = m_dwPetUID;
                right.PCharacters.CharInfo[m_dwCharPosition].Pets[0].m_dwID = m_dwPetID;
                right.PCharacters.CharInfo[m_dwCharPosition].Pets[0].m_strName = m_strPetName;
                right.PCharacters.CharInfo[m_dwCharPosition].Pets[0].m_cPromotion = m_cPromotion;
                right.PCharacters.CharInfo[m_dwCharPosition].Pets[0].m_dwEXP = m_dwExp;
                right.PCharacters.CharInfo[m_dwCharPosition].Pets[0].m_iInitSatiation = m_iInitSatiation;
                right.PCharacters.CharInfo[m_dwCharPosition].Pets[0].m_iLevel = m_iLevel;
                right.PCharacters.CharInfo[m_dwCharPosition].Pets[0].m_iSatiation = m_iSatiation;
                right.PCharacters.CharInfo[m_dwCharPosition].Pets[0].m_mapInitExp = m_mapInitExp;
                right.PCharacters.CharInfo[m_dwCharPosition].Pets[0].m_nHatchingID = m_nHatchingID;
                //Log.Write("Character:{0} PetID: {1} PetName:{2} Equips:{3}", m_cCharIndex, m_dwPetID, m_strPetName, m_vecEquipItem);

                rs.Jump(41);
            }
        }

        public static void KUserInfo(Session right, PacketRead rs)
        {
            string m_strLogin = rs.String();
            int m_dwID = rs.Int();
            string m_strNickName = rs.String();
            rs.Int();
            byte m_cStatus = rs.Byte();
            rs.Jump(22);
            int m_iGamePoint = rs.Int();
            rs.Short();

            right.PInfo.m_ucCharType =  m_cStatus;
        }

        public static void KUserInfo(Session right, PacketWrite ks)
        {
            //#c22CBAA
            //#c769EFF
            ks.UnicodeStr(right.PInfo.m_strLogin);
            ks.Int(right.PInfo.m_dwUserUID);
            ks.UnicodeStr(right.PInfo.m_strNickName);
            ks.Int(0);
            ks.Byte(right.PInfo.m_ucCharType);
            ks.HexArray("00 00 00 00 00 FF 00 FF 00 FF 00 00 00 00 00 00 00 00 00 64 00 00");
            ks.Int(right.PInfo.m_iGamePoint);
            ks.Short(0);
        }

        public static void KRoomInfo(Session right, PacketRead rs)
        {
            libcomservice.REQUEST.Room room = new libcomservice.REQUEST.Room();

            ushort m_usRoomID = rs.UShort();
            room.m_strRoomName = rs.UnicodeString();
            room.m_bPublic = rs.Bool();
            room.m_bGuild = rs.Bool();
            room.m_strRoomPasswd = rs.UnicodeString();
            short m_usUsers = rs.Short();
            room.m_usMaxUsers = rs.Get_Short();
            room.m_bPlaying = rs.Bool();
            room.m_cGrade = rs.Byte();
            room.m_cGameCategory = rs.Byte();
            room.m_iGameMode = rs.Int();
            room.m_iSubGameMode = rs.Int();
            room.m_bRandomableMap = rs.Bool();
            room.m_iMapID = rs.Int();
            room.m_iP2PVersion = rs.Int();
            byte[] m_abSlotOpen = rs.Buffer_Array_Bytes(6);
            room.m_vecMonsterSlot = rs.Int();
            room.m_cDifficulty = rs.Int();
            rs.Jump(17);

            ushort index_room = 0;
            for (ushort i = 1; i < GameServer.m_usMaxUsers; i++)
            {
                if (GameServer.List_Rooms.ContainsKey(i))
                    continue;
                index_room = i;
                break;
            }
            room.m_usRoomID = index_room;

            room.m_dwSlots[0].Active = true;
            room.m_dwSlots[0].usr = right;
            room.m_dwSlots[0].Leader = true;
            room.m_dwSlots[0].Open = false;
            room.m_dwSlots[0].Status = 0;
            room.m_dwSlots[0].AFK = false;
            room.m_dwSlots[0].Team = 0;

            for (int i = 1; i < room.m_usMaxUsers; i++)
            {
                room.m_dwSlots[i].Active = false;
                room.m_dwSlots[i].usr = null;
                room.m_dwSlots[i].Leader = false;
                room.m_dwSlots[i].Open = true;
                room.m_dwSlots[i].Status = 0;
                room.m_dwSlots[i].AFK = false;
                room.m_dwSlots[i].Team = 0;
            }
            right.PInfo.CurRoom = room;
        }

        public static void KRoomInfo(Session right, PacketWrite ks)
        {
            libcomservice.REQUEST.Room room = right.PInfo.CurRoom;
            
            ks.UShort(room.m_usRoomID);
            ks.UnicodeStr(room.m_strRoomName);
            ks.Bool(room.m_bPublic);
            ks.Bool(room.m_bGuild);
            ks.UnicodeStr(room.m_strRoomPasswd);
            ks.Short(room.m_usUsers());
            ks.Short(7);
            ks.Bool(room.m_bPlaying);
            ks.Byte(room.m_cGrade);
            ks.Byte(room.m_cGameCategory);
            ks.Int(room.m_iGameMode);
            ks.Int(room.m_iSubGameMode);
            ks.Bool(room.m_bRandomableMap);
            ks.Int(room.m_iMapID);
            ks.Int(room.m_iP2PVersion);
            for (short j = 0; j < room.m_usMaxUsers; j++)
            {
                ks.Bool(room.m_dwSlots[j].Open);
            }
            if (room.m_usMaxUsers == 4)
            {
                ks.Bool(false);
                ks.Bool(false);
            }
            ks.Int(room.m_vecMonsterSlot);
            ks.Int(room.m_cDifficulty);
            ks.HexArray("00 00 00 00 01");
            ks.WriteIP(GameServer.m_dwIP);
            ks.UShort(GameServer.m_usURelayServerPort);
            ks.WriteIP(GameServer.m_dwIP);
            ks.UShort(GameServer.m_usTRelayServerPort);
        }

        public static void m_vecMissionSlot(Session right, PacketWrite ks)
        {
            ks.Int(right.PInfo.Missions.Count);
            for (int x = 0; x < right.PInfo.Missions.Count; x++)
            {
                ks.Int(right.PInfo.Missions[x].MissionID);
                ks.Int(1);
                ks.Int(right.PInfo.Missions[x].MissionUID);
                ks.Int(1);
                ks.HexArray("59 80 21 7C 59 7E CF FC");
                ks.Int(0);
            }
        }

        internal static void KItemInfo(PacketWrite ks, uint itemid, uint itemuid,byte level)
        {
            ks.UInt(itemid);
            ks.Int(1);
            ks.UInt(itemuid);
            ks.Int(-1);
            ks.Int(-1);
            ks.Byte(0);
            ks.Byte(0);
            ks.Byte(0);
            ks.Byte(level);
            ks.HexArray("00 00");
            ks.Int(-1);
            ks.HexArray("00 00 00 00 59 6E 2C DB");
            ks.HexArray("00 00 00 00 00 00 00 00 00 00 00 00");//Atribs
            ks.Byte(0);
            ks.Int(0);
            ks.Int(0);
            ks.HexArray("00 07 D8 47 DC 07 D8 47 EC 00 4E 6D A5 00 00 06 00 00 00 00 00 00 00 00 00");
        }
    }
}
