using Common;
using libcomservice.Core;
using libcomservice.Game;
using System;

namespace libcomservice.REQUEST
{
    internal struct Slot
    {
        internal bool Active;
        internal bool Open;
        internal bool Leader;
        internal bool AFK;
        internal byte Status;
        internal int Kills;
        internal int Deaths;
        internal byte Win;
        internal Session usr;
        internal int LoadPlayer;
        internal int isKilled;
        internal int Team;
        internal int PositionSlot;
    }

    public class Room
    {
        internal ushort m_usRoomID;
        internal string m_strRoomPasswd;
        internal string m_strRoomName;
        internal bool m_bPublic;
        internal bool m_bGuild;
        internal byte m_cGrade;
        internal byte m_cGameCategory;
        internal int m_iGameMode;
        internal int m_iSubGameMode;
        internal bool m_bRandomableMap = false;
        internal int m_iMapID;
        internal int m_iP2PVersion = 3;
        internal int m_cDifficulty = 0;
        internal int m_vecMonsterSlot = 0;
        internal int m_vecBossId = 0;
        internal byte RoutingMethod = 1;
        internal int m_vecBansSlot = 3;
        internal bool m_bPlaying = false;
        internal short m_usMaxUsers = 6;
        internal Slot[] m_dwSlots = new Slot[6];

        internal int m_abTotalSlotsOpen()
        {
            Int32 islot = 0;
            for (Int32 a = 0; a < m_usMaxUsers; a++)
                if (m_dwSlots[a].Open == true)
                    islot++;
            return islot;
        }

        internal int m_abSlotOpen()
        {
            int m_dwcurSlot = 0;
            for (int x = 0; x < m_usMaxUsers; x++)
                if (m_dwcurSlot == 0 && m_dwSlots[x].Open == true)
                    return x;
            return -1;
        }

        internal short m_usUsers()
        {
            short players = 0;
            for (short i = 0; i < m_usMaxUsers; i++)
                if (m_dwSlots[i].Active == true)
                    players++;
            return players;
        }

        internal int m_dwUserSlot(Session user)
        {
            for (Int32 i = 0; i < m_usMaxUsers; i++)
            {
                if (m_dwSlots[i].usr == user && m_dwSlots[i].Active == true)
                    return i;
            }
            return 0;
        }

        internal Session m_sSearchLeader()
        {
            for (int a = 0; a < m_usMaxUsers; a++)
            {
                if (m_dwSlots[a].Leader == true)
                    return m_dwSlots[a].usr;
            }
            return null;
        }

        internal void ChangeModes(byte matchmode, bool randommap, int gamemode, int itemmode, int map, int difficulty)
        {
            m_cGameCategory = matchmode;
            m_bRandomableMap = randommap;
            m_iGameMode = gamemode;
            m_iSubGameMode = itemmode;
            m_iMapID = map;
            m_cDifficulty = difficulty;
        }

        internal void ChangeSlot(int position, bool active, Session player, bool leader, bool open, byte state, bool afk, int deaths = 0, int kills = 0, byte win = 0)
        {
            m_dwSlots[position].Open = open;
            m_dwSlots[position].AFK = afk;
            m_dwSlots[position].Win = win;
            m_dwSlots[position].Kills = kills;
            m_dwSlots[position].Status = state;
            m_dwSlots[position].Deaths = deaths;
            m_dwSlots[position].PositionSlot = position;
            m_dwSlots[position].Active = active;
            m_dwSlots[position].usr = player;
            m_dwSlots[position].Leader = leader;
        }

        internal int GetPositionPlayer(Session user)
        {
            for (int i = 0; i < m_usMaxUsers; i++)
            {
                if (m_dwSlots[i].Active == true && m_dwSlots[i].usr == user)
                    return i;
            }
            return 0;
        }

        internal int GetTeam(Session user)
        {
            for (int i = 0; i < m_usMaxUsers; i++)
            {
                if (m_dwSlots[i].Active == true && m_dwSlots[i].usr == user)
                    return i / (m_usMaxUsers / 2);
            }
            return 0;
        }

        internal void SendForAllPlayersInRoom(PacketWrite pw, short opcode)
        {
            for (int i = 0; i < m_usMaxUsers; i++)
            {
                if (m_dwSlots[i].Active == true)
                {
                    m_dwSlots[i].usr.SendPacket(pw, opcode);
                }
            }
        }

        internal int PVPChangeSlot(Room room, int Team)
        {
            int new_position = 0;

            if (Team == 0)

                for (int i = 0; i < 3; i++)
                {

                    if (room.m_dwSlots[i].Active == false && room.m_dwSlots[i].Open == true)
                    {
                        new_position = i;
                        return new_position;
                    }

                }

            else

                for (int i = 3; i < 6; i++)
                {

                    if (room.m_dwSlots[i].Active == false && room.m_dwSlots[i].Open == true)
                    {
                        new_position = i;
                        return new_position;
                    }
                }

            return 0;
        }
    }
}
