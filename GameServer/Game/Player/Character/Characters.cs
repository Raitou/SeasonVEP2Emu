using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Common;
using libcomservice.Data;
using libcomservice.Game;
using libcomservice.Core;

namespace libcomservice.REQUEST
{
    public struct attribute
    {
        public byte attId { get; set; }
        public byte percent{ get; set; }
    }

    public struct Look
    {
        public uint ItemID;
        public uint ItemUID;        
    }

    public struct Equip
    {
        public int ItemID;
        public int ItemUID;
    }

    public struct Mission
    {
        public int MissionID;
        public int MissionUID;
    }

    public struct Skill
    {
        public int SkillGroup;
        public int SkillID;
        public byte Promotion;
    }

    public struct PetAttacks
    {
        public int AtkID;
        public uint AtkUID;
    }

    public struct Pet
    {
        public int m_dwUID;
        public int m_dwID;
        public string m_strName;
        public int m_mapInitExp;
        public uint m_dwEXP;
        public int m_iLevel;
        public byte m_cPromotion;
        public int m_nHatchingID;
        public int m_iInitSatiation;
        public int m_iSatiation;
        public List<PetAttacks> Slot1;
        public List<PetAttacks> Slot2;
    }

    public struct Chars
    {
        public byte CharType;
        public int Win;
        public int Lose;
        public byte Promotion;
        public long Exp;
        public int Level;
        public List<attribute> Attributes;
        public List<Equip> Equipements;
        public List<Look> LookItens;
        public List<Skill> Skills;        
        public Pet[] Pets;
    }

    public class Character
    {
        public Chars[] CharInfo;

        public int PositionCharacter(byte CharacterID,Session right)
        {
            for (int i = 0; i < right.PCharacters.CharInfo.Length; i++)
                if (right.PCharacters.CharInfo[i].CharType == CharacterID)
                    return i;
            return -1;
        }

        public void LoadCharacters(string Login, int LoginUID)
        {
            DataSet DBAcess = new DataSet();
            GameServer.Sql.Exec(DBAcess, "SELECT * FROM  characters WHERE Login = '{0}'", Login);

            CharInfo = new Chars[DBAcess.Tables[0].Rows.Count];

            for (int i = 0; i < DBAcess.Tables[0].Rows.Count; i++)
            {
                CharInfo[i].CharType = Convert.ToByte(DBAcess.Tables[0].Rows[i]["CharType"].ToString());
                CharInfo[i].Promotion = Convert.ToByte(DBAcess.Tables[0].Rows[i]["Promotion"].ToString());
                CharInfo[i].Exp = Convert.ToInt64(DBAcess.Tables[0].Rows[i]["Exp"].ToString());
                CharInfo[i].Level = Convert.ToInt32(DBAcess.Tables[0].Rows[i]["Level"].ToString());
                CharInfo[i].Win = Convert.ToInt32(DBAcess.Tables[0].Rows[i]["Win"].ToString());
                CharInfo[i].Lose = Convert.ToInt32(DBAcess.Tables[0].Rows[i]["Lose"].ToString());
              

                DataSet NewDBAcess = new DataSet();
                GameServer.Sql.Exec(NewDBAcess, "SELECT * FROM  GEIEquipItem WHERE LoginUID = '{0}' AND CharType = '{1}'", LoginUID, CharInfo[i].CharType);
                CharInfo[i].Equipements = new List<Equip>();
                for (int j = 0; j < NewDBAcess.Tables[0].Rows.Count; j++)
                {
                    CharInfo[i].Equipements.Add
                    (
                        new Equip()
                        {
                            ItemID = Convert.ToInt32(NewDBAcess.Tables[0].Rows[j]["ItemID"].ToString()),
                            ItemUID = Convert.ToInt32(NewDBAcess.Tables[0].Rows[j]["ItemUID"].ToString())
                        }
                    );
                }

                DataSet q1 = new DataSet();
                GameServer.Sql.Exec(q1, "SELECT * FROM  GEIEEquipLook WHERE LoginUID = '{0}' AND CharType = '{1}'", LoginUID, CharInfo[i].CharType);
                CharInfo[i].LookItens = new List<Look>();
                for (int j = 0; j < q1.Tables[0].Rows.Count; j++)
                {
                    CharInfo[i].LookItens.Add
                    (
                        new Look()
                        {
                            ItemID = Convert.ToUInt32(q1.Tables[0].Rows[j]["ItemID"].ToString()),
                            ItemUID = Convert.ToUInt32(q1.Tables[0].Rows[j]["ItemUID"].ToString())                            
                        }
                    );
                }

                DataSet q0 = new DataSet();
                GameServer.Sql.Exec(q0, "SELECT * FROM  STGSkillTreeSet WHERE LoginUID = '{0}' AND CharType = '{1}'", LoginUID, CharInfo[i].CharType);
                CharInfo[i].Skills = new List<Skill>();
                for (int j = 0; j < q0.Tables[0].Rows.Count; j++)
                {
                    CharInfo[i].Skills.Add
                    (
                        new Skill()
                        {
                            SkillID = Convert.ToInt32(q0.Tables[0].Rows[j]["SkillID"].ToString()),
                            SkillGroup = Convert.ToInt32(q0.Tables[0].Rows[j]["SetID"].ToString()),
                            Promotion = Convert.ToByte(q0.Tables[0].Rows[j]["Promotion"].ToString())
                        }
                    );
                }
                CharInfo[i].Pets = new Pet[1];
                CharInfo[i].Pets[0].m_dwUID = 0;
                CharInfo[i].Pets[0].m_dwID = 0;        
                CharInfo[i].Pets[0].m_strName = "";
                CharInfo[i].Pets[0].m_mapInitExp = 0;
                CharInfo[i].Pets[0].m_dwEXP = 0;
                CharInfo[i].Pets[0].m_iLevel = 0;
                CharInfo[i].Pets[0].m_cPromotion = 0;
                CharInfo[i].Pets[0].m_nHatchingID = 0;
                CharInfo[i].Pets[0].m_iInitSatiation = 0;
                CharInfo[i].Pets[0].m_iSatiation = 0;
                CharInfo[i].Pets[0].Slot1 = new List<PetAttacks>();
                CharInfo[i].Pets[0].Slot2 = new List<PetAttacks>();
                //CharInfo[i].Pets[0].Slot1.Add(new PetAttacks() { AtkID = 663040 , AtkUID = 14262317 });
                CharInfo[i].LookItens = new List<Look>();                
            }
        }

        public void ChangeCharacterInRoom(Session p, PacketRead r)
        {
            byte oldChar = r.Byte();
            byte newChar = r.Byte();
            p.PInfo.m_ucCharType = newChar;
            PacketWrite pw = new PacketWrite();
            pw.Int(0);
            pw.Int(0);
            pw.Byte(newChar);
            pw.HexArray("00 00 00 00 00 00 00 00");
            pw.HexArray("00 00 00 8C 00 00 78 6E 00 00 00 01 00 00 00 01 00 00 00 14 00 00 00 00 00 00 00 01 00 00 00 14 00 00 00 00 00 00 78 78 00 00 00 01 00 00 00 01 00 00 00 15 00 00 00 00 00 00 00 01 00 00 00 15 00 00 00 00 00 00 78 82 00 00 00 01 00 00 00 01 00 00 00 16 00 00 00 00 00 00 00 01 00 00 00 16 00 00 00 00 00 00 78 8C 00 00 00 01 00 00 00 01 00 00 00 17 00 00 00 00 00 00 00 01 00 00 00 17 00 00 00 00 00 00 78 96 00 00 00 01 00 00 00 01 00 00 00 18 00 00 00 00 00 00 00 01 00 00 00 18 00 00 00 00 00 00 78 A0 00 00 00 01 00 00 00 01 00 00 00 19 00 00 00 00 00 00 00 01 00 00 00 19 00 00 00 00 00 00 78 AA 00 00 00 01 00 00 00 01 00 00 00 1A 00 00 00 00 00 00 00 01 00 00 00 1A 00 00 00 00 00 00 78 B4 00 00 00 01 00 00 00 01 00 00 00 1B 00 00 00 00 00 00 00 01 00 00 00 1B 00 00 00 00 00 00 78 BE 00 00 00 01 00 00 00 01 00 00 00 1C 00 00 00 00 00 00 00 01 00 00 00 1C 00 00 00 00 00 00 78 C8 00 00 00 01 00 00 00 01 00 00 00 1D 00 00 00 00 00 00 00 01 00 00 00 1D 00 00 00 00 00 00 78 D2 00 00 00 01 00 00 00 01 00 00 00 1E 00 00 00 00 00 00 00 01 00 00 00 1E 00 00 00 00 00 00 78 DC 00 00 00 01 00 00 00 01 00 00 00 1F 00 00 00 00 00 00 00 01 00 00 00 1F 00 00 00 00 00 00 78 E6 00 00 00 01 00 00 00 01 00 00 00 20 00 00 00 00 00 00 00 01 00 00 00 20 00 00 00 00 00 00 78 F0 00 00 00 01 00 00 00 01 00 00 00 21 00 00 00 00 00 00 00 01 00 00 00 21 00 00 00 00 00 00 78 FA 00 00 00 01 00 00 00 01 00 00 00 22 00 00 00 00 00 00 00 01 00 00 00 22 00 00 00 00 00 00 79 04 00 00 00 01 00 00 00 01 00 00 00 23 00 00 00 00 00 00 00 01 00 00 00 23 00 00 00 00 00 00 79 0E 00 00 00 01 00 00 00 01 00 00 00 24 00 00 00 00 00 00 00 01 00 00 00 24 00 00 00 00 00 00 79 18 00 00 00 01 00 00 00 01 00 00 00 25 00 00 00 00 00 00 00 01 00 00 00 25 00 00 00 00 00 00 79 22 00 00 00 01 00 00 00 01 00 00 00 26 00 00 00 00 00 00 00 01 00 00 00 26 00 00 00 00 00 00 79 2C 00 00 00 01 00 00 00 01 00 00 00 28 00 00 00 00 00 00 00 01 00 00 00 28 00 00 00 00 00 00 79 36 00 00 00 01 00 00 00 01 00 00 00 2A 00 00 00 00 00 00 00 01 00 00 00 2A 00 00 00 00 00 00 79 40 00 00 00 01 00 00 00 01 00 00 00 2C 00 00 00 00 00 00 00 01 00 00 00 2C 00 00 00 00 00 00 79 4A 00 00 00 01 00 00 00 01 00 00 00 2E 00 00 00 00 00 00 00 01 00 00 00 2E 00 00 00 00 00 00 85 C0 00 00 00 01 00 00 00 01 00 00 00 30 00 00 00 00 00 00 00 01 00 00 00 30 00 00 00 00 00 00 85 CA 00 00 00 01 00 00 00 01 00 00 00 32 00 00 00 00 00 00 00 01 00 00 00 32 00 00 00 00 00 00 85 D4 00 00 00 01 00 00 00 01 00 00 00 42 00 00 00 00 00 00 00 01 00 00 00 42 00 00 00 00 00 00 85 DE 00 00 00 01 00 00 00 01 00 00 00 44 00 00 00 00 00 00 00 01 00 00 00 44 00 00 00 00 00 00 85 E8 00 00 00 01 00 00 00 01 00 00 00 46 00 00 00 00 00 00 00 01 00 00 00 46 00 00 00 00 00 00 85 F2 00 00 00 01 00 00 00 01 00 00 00 48 00 00 00 00 00 00 00 01 00 00 00 48 00 00 00 00 00 00 85 FC 00 00 00 01 00 00 00 01 00 00 00 4A 00 00 00 00 00 00 00 01 00 00 00 4A 00 00 00 00 00 00 86 06 00 00 00 01 00 00 00 01 00 00 00 4C 00 00 00 00 00 00 00 01 00 00 00 4C 00 00 00 00 00 00 86 10 00 00 00 01 00 00 00 01 00 00 00 50 00 00 00 00 00 00 00 01 00 00 00 50 00 00 00 00 00 00 86 1A 00 00 00 01 00 00 00 01 00 00 00 52 00 00 00 00 00 00 00 01 00 00 00 52 00 00 00 00 00 00 86 24 00 00 00 01 00 00 00 00 00 00 00 00 00 00 86 2E 00 00 00 01 00 00 00 00 00 00 00 00 00 00 86 38 00 00 00 01 00 00 00 00 00 00 00 00 00 00 86 42 00 00 00 01 00 00 00 00 00 00 00 00 00 00 86 4C 00 00 00 01 00 00 00 00 00 00 00 00 00 01 45 8C 00 00 00 01 00 00 00 01 00 00 00 00 00 00 00 00 00 00 00 01 00 00 00 00 00 00 00 00 00 01 45 96 00 00 00 01 00 00 00 01 00 00 00 01 00 00 00 00 00 00 00 01 00 00 00 01 00 00 00 00 00 01 45 A0 00 00 00 01 00 00 00 01 00 00 00 02 00 00 00 00 00 00 00 01 00 00 00 02 00 00 00 00 00 01 45 AA 00 00 00 01 00 00 00 01 00 00 00 03 00 00 00 00 00 00 00 01 00 00 00 03 00 00 00 00 00 01 45 B4 00 00 00 01 00 00 00 01 00 00 00 04 00 00 00 00 00 00 00 01 00 00 00 04 00 00 00 00 00 01 45 BE 00 00 00 01 00 00 00 01 00 00 00 05 00 00 00 00 00 00 00 01 00 00 00 05 00 00 00 00 00 01 45 C8 00 00 00 01 00 00 00 01 00 00 00 06 00 00 00 00 00 00 00 01 00 00 00 06 00 00 00 00 00 01 45 D2 00 00 00 01 00 00 00 01 00 00 00 07 00 00 00 00 00 00 00 01 00 00 00 07 00 00 00 00 00 01 45 DC 00 00 00 01 00 00 00 01 00 00 00 08 00 00 00 00 00 00 00 01 00 00 00 08 00 00 00 00 00 01 45 E6 00 00 00 01 00 00 00 01 00 00 00 09 00 00 00 00 00 00 00 01 00 00 00 09 00 00 00 00 00 01 45 F0 00 00 00 01 00 00 00 01 00 00 00 0A 00 00 00 00 00 00 00 01 00 00 00 0A 00 00 00 00 00 01 45 FA 00 00 00 01 00 00 00 01 00 00 00 0B 00 00 00 00 00 00 00 01 00 00 00 0B 00 00 00 00 00 01 46 04 00 00 00 01 00 00 00 01 00 00 00 0C 00 00 00 00 00 00 00 01 00 00 00 0C 00 00 00 00 00 01 46 0E 00 00 00 01 00 00 00 01 00 00 00 0D 00 00 00 00 00 00 00 01 00 00 00 0D 00 00 00 00 00 01 46 18 00 00 00 01 00 00 00 01 00 00 00 0E 00 00 00 00 00 00 00 01 00 00 00 0E 00 00 00 00 00 01 46 22 00 00 00 01 00 00 00 01 00 00 00 0F 00 00 00 00 00 00 00 01 00 00 00 0F 00 00 00 00 00 01 46 2C 00 00 00 01 00 00 00 01 00 00 00 10 00 00 00 00 00 00 00 01 00 00 00 10 00 00 00 00 00 01 46 36 00 00 00 01 00 00 00 01 00 00 00 11 00 00 00 00 00 00 00 01 00 00 00 11 00 00 00 00 00 01 46 40 00 00 00 01 00 00 00 01 00 00 00 12 00 00 00 00 00 00 00 01 00 00 00 12 00 00 00 00 00 01 46 4A 00 00 00 01 00 00 00 01 00 00 00 13 00 00 00 00 00 00 00 01 00 00 00 13 00 00 00 00 00 01 9B 18 00 00 00 01 00 00 00 01 00 00 00 27 00 00 00 00 00 00 00 01 00 00 00 27 00 00 00 00 00 01 FF 40 00 00 00 01 00 00 00 01 00 00 00 29 00 00 00 00 00 00 00 01 00 00 00 29 00 00 00 00 00 01 FF 4A 00 00 00 01 00 00 00 01 00 00 00 2B 00 00 00 00 00 00 00 01 00 00 00 2B 00 00 00 00 00 02 1E 9E 00 00 00 01 00 00 00 01 00 00 00 2D 00 00 00 00 00 00 00 01 00 00 00 2D 00 00 00 00 00 02 29 A2 00 00 00 01 00 00 00 01 00 00 00 2F 00 00 00 00 00 00 00 01 00 00 00 2F 00 00 00 00 00 02 41 08 00 00 00 01 00 00 00 01 00 00 00 31 00 00 00 00 00 00 00 01 00 00 00 31 00 00 00 00 00 02 CA 56 00 00 00 01 00 00 00 01 00 00 00 33 00 00 00 00 00 00 00 01 00 00 00 33 00 00 00 00 00 02 CA 60 00 00 00 01 00 00 00 01 00 00 00 34 00 00 00 00 00 00 00 01 00 00 00 34 00 00 00 00 00 02 CA 6A 00 00 00 01 00 00 00 01 00 00 00 35 00 00 00 00 00 00 00 01 00 00 00 35 00 00 00 00 00 02 CA 74 00 00 00 01 00 00 00 01 00 00 00 36 00 00 00 00 00 00 00 01 00 00 00 36 00 00 00 00 00 02 D1 2C 00 00 00 01 00 00 00 01 00 00 00 37 00 00 00 00 00 00 00 01 00 00 00 37 00 00 00 00 00 02 D1 36 00 00 00 01 00 00 00 01 00 00 00 38 00 00 00 00 00 00 00 01 00 00 00 38 00 00 00 00 00 02 D1 40 00 00 00 01 00 00 00 01 00 00 00 39 00 00 00 00 00 00 00 01 00 00 00 39 00 00 00 00 00 02 D1 4A 00 00 00 01 00 00 00 01 00 00 00 3A 00 00 00 00 00 00 00 01 00 00 00 3A 00 00 00 00 00 02 E0 54 00 00 00 01 00 00 00 01 00 00 00 3B 00 00 00 00 00 00 00 01 00 00 00 3B 00 00 00 00 00 02 E0 5E 00 00 00 01 00 00 00 01 00 00 00 3C 00 00 00 00 00 00 00 01 00 00 00 3C 00 00 00 00 00 02 E0 68 00 00 00 01 00 00 00 01 00 00 00 3D 00 00 00 00 00 00 00 01 00 00 00 3D 00 00 00 00 00 02 E0 72 00 00 00 01 00 00 00 01 00 00 00 3E 00 00 00 00 00 00 00 01 00 00 00 3E 00 00 00 00 00 02 E0 7C 00 00 00 01 00 00 00 01 00 00 00 3F 00 00 00 00 00 00 00 01 00 00 00 3F 00 00 00 00 00 02 E0 86 00 00 00 01 00 00 00 01 00 00 00 40 00 00 00 00 00 00 00 01 00 00 00 40 00 00 00 00 00 03 4A 76 00 00 00 01 00 00 00 01 00 00 00 41 00 00 00 00 00 00 00 01 00 00 00 41 00 00 00 00 00 03 4A 80 00 00 00 01 00 00 00 01 00 00 00 43 00 00 00 00 00 00 00 01 00 00 00 43 00 00 00 00 00 03 4A 8A 00 00 00 01 00 00 00 01 00 00 00 45 00 00 00 00 00 00 00 01 00 00 00 45 00 00 00 00 00 03 4A 94 00 00 00 01 00 00 00 01 00 00 00 47 00 00 00 00 00 00 00 01 00 00 00 47 00 00 00 00 00 04 89 86 00 00 00 01 00 00 00 01 00 00 00 49 00 00 00 00 00 00 00 01 00 00 00 49 00 00 00 00 00 04 89 90 00 00 00 01 00 00 00 01 00 00 00 4B 00 00 00 00 00 00 00 01 00 00 00 4B 00 00 00 00 00 05 0F 6E 00 00 00 01 00 00 00 01 00 00 00 4D 00 00 00 00 00 00 00 01 00 00 00 4D 00 00 00 00 00 05 0F 78 00 00 00 01 00 00 00 01 00 00 00 4E 00 00 00 00 00 00 00 01 00 00 00 4E 00 00 00 00 00 05 9A 42 00 00 00 01 00 00 00 01 00 00 00 4F 00 00 00 00 00 00 00 01 00 00 00 4F 00 00 00 00 00 06 E2 3A 00 00 00 01 00 00 00 01 00 00 00 51 00 00 00 00 00 00 00 01 00 00 00 51 00 00 00 00 00 08 33 1A 00 00 00 01 00 00 00 01 00 00 00 53 00 00 00 00 00 00 00 01 00 00 00 53 00 00 00 00 00 08 33 24 00 00 00 01 00 00 00 01 00 00 00 54 00 00 00 00 00 00 00 01 00 00 00 54 00 00 00 00 00 09 54 66 00 00 00 01 00 00 00 00 00 00 00 00 00 09 54 70 00 00 00 01 00 00 00 00 00 00 00 00 00 09 54 7A 00 00 00 01 00 00 00 00 00 00 00 00 00 09 54 84 00 00 00 01 00 00 00 00 00 00 00 00 00 09 54 8E 00 00 00 01 00 00 00 00 00 00 00 00 00 0A 1E 28 00 00 00 01 00 00 00 01 00 00 00 5F 00 00 00 00 00 00 00 01 00 00 00 5F 00 00 00 00 00 0A 1E 32 00 00 00 01 00 00 00 01 00 00 00 60 00 00 00 00 00 00 00 01 00 00 00 60 00 00 00 00 00 0C 55 08 00 00 00 01 00 00 00 01 00 00 00 61 00 00 00 00 00 00 00 01 00 00 00 61 00 00 00 00 00 0C 55 12 00 00 00 01 00 00 00 01 00 00 00 62 00 00 00 00 00 00 00 01 00 00 00 62 00 00 00 00 00 0D 72 94 00 00 00 01 00 00 00 01 00 00 00 63 00 00 00 00 00 00 00 01 00 00 00 63 00 00 00 00 00 0D 72 9E 00 00 00 01 00 00 00 01 00 00 00 64 00 00 00 00 00 00 00 01 00 00 00 64 00 00 00 00 00 0E E9 E4 00 00 00 01 00 00 00 01 00 00 00 65 00 00 00 00 00 00 00 01 00 00 00 65 00 00 00 00 00 0E E9 EE 00 00 00 01 00 00 00 01 00 00 00 66 00 00 00 00 00 00 00 01 00 00 00 66 00 00 00 00 00 0E E9 F8 00 00 00 01 00 00 00 01 00 00 00 67 00 00 00 00 00 00 00 01 00 00 00 67 00 00 00 00 00 0E EA 02 00 00 00 01 00 00 00 01 00 00 00 68 00 00 00 00 00 00 00 01 00 00 00 68 00 00 00 00 00 0E EA 0C 00 00 00 01 00 00 00 01 00 00 00 6B 00 00 00 00 00 00 00 01 00 00 00 6B 00 00 00 00 00 0E EA 16 00 00 00 01 00 00 00 01 00 00 00 6B 00 00 00 00 00 00 00 01 00 00 00 6B 00 00 00 00 00 0F 85 98 00 00 00 01 00 00 00 01 00 00 00 69 00 00 00 00 00 00 00 01 00 00 00 69 00 00 00 00 00 0F 85 A2 00 00 00 01 00 00 00 01 00 00 00 6A 00 00 00 00 00 00 00 01 00 00 00 6A 00 00 00 00 00 10 49 60 00 00 00 01 00 00 00 01 00 00 00 6C 00 00 00 00 00 00 00 01 00 00 00 6C 00 00 00 00 00 10 49 6A 00 00 00 01 00 00 00 01 00 00 00 6D 00 00 00 00 00 00 00 01 00 00 00 6D 00 00 00 00 00 10 6A 3A 00 00 00 01 00 00 00 01 00 00 00 6E 00 00 00 00 00 00 00 01 00 00 00 6E 00 00 00 00 00 10 6A 44 00 00 00 01 00 00 00 01 00 00 00 6F 00 00 00 00 00 00 00 01 00 00 00 6F 00 00 00 00 00 10 A5 18 00 00 00 01 00 00 00 01 00 00 00 70 00 00 00 00 00 00 00 01 00 00 00 70 00 00 00 00 00 10 A5 22 00 00 00 01 00 00 00 01 00 00 00 71 00 00 00 00 00 00 00 01 00 00 00 71 00 00 00 00 00 10 E6 E0 00 00 00 01 00 00 00 01 00 00 00 72 00 00 00 00 00 00 00 01 00 00 00 72 00 00 00 00 00 10 E6 EA 00 00 00 01 00 00 00 01 00 00 00 73 00 00 00 00 00 00 00 01 00 00 00 73 00 00 00 00 00 12 6A A6 00 00 00 01 00 00 00 01 00 00 00 74 00 00 00 00 00 00 00 01 00 00 00 74 00 00 00 00 00 12 6A B0 00 00 00 01 00 00 00 01 00 00 00 75 00 00 00 00 00 00 00 01 00 00 00 75 00 00 00 00 00 12 6A BA 00 00 00 01 00 00 00 01 00 00 00 76 00 00 00 00 00 00 00 01 00 00 00 76 00 00 00 00 00 12 6A C4 00 00 00 01 00 00 00 01 00 00 00 77 00 00 00 00 00 00 00 01 00 00 00 77 00 00 00 00 00 12 6A CE 00 00 00 01 00 00 00 01 00 00 00 78 00 00 00 00 00 00 00 01 00 00 00 78 00 00 00 00 00 12 6A D8 00 00 00 01 00 00 00 01 00 00 00 79 00 00 00 00 00 00 00 01 00 00 00 79 00 00 00 00 00 12 9F 26 00 00 00 01 00 00 00 01 00 00 00 7A 00 00 00 00 00 00 00 01 00 00 00 7A 00 00 00 00 00 12 9F 30 00 00 00 01 00 00 00 01 00 00 00 7B 00 00 00 00 00 00 00 01 00 00 00 7B 00 00 00 00 00 12 9F 3A 00 00 00 01 00 00 00 01 00 00 00 7C 00 00 00 00 00 00 00 01 00 00 00 7C 00 00 00 00 00 12 9F 44 00 00 00 01 00 00 00 01 00 00 00 7D 00 00 00 00 00 00 00 01 00 00 00 7D 00 00 00 00 00 12 9F 4E 00 00 00 01 00 00 00 01 00 00 00 7E 00 00 00 00 00 00 00 01 00 00 00 7E 00 00 00 00 00 13 8C 24 00 00 00 01 00 00 00 01 00 00 00 7F 00 00 00 00 00 00 00 01 00 00 00 7F 00 00 00 00 00 13 8C 2E 00 00 00 01 00 00 00 00 00 00 00 00 00 13 8C 38 00 00 00 01 00 00 00 01 00 00 00 80 00 00 00 00 00 00 00 01 00 00 00 80 00 00 00 00 00 13 8C 42 00 00 00 01 00 00 00 01 00 00 00 85 00 00 00 00 00 00 00 01 00 00 00 85 00 00 00 00 00 13 8C 4C 00 00 00 01 00 00 00 01 00 00 00 81 00 00 00 00 00 00 00 01 00 00 00 81 00 00 00 00 00 13 8C 56 00 00 00 01 00 00 00 01 00 00 00 86 00 00 00 00 00 00 00 01 00 00 00 86 00 00 00 00 00 13 8C 60 00 00 00 01 00 00 00 01 00 00 00 82 00 00 00 00 00 00 00 01 00 00 00 82 00 00 00 00 00 13 8C 6A 00 00 00 01 00 00 00 01 00 00 00 87 00 00 00 00 00 00 00 01 00 00 00 87 00 00 00 00 00 13 8C 74 00 00 00 01 00 00 00 01 00 00 00 83 00 00 00 00 00 00 00 01 00 00 00 83 00 00 00 00 00 13 8C 7E 00 00 00 01 00 00 00 01 00 00 00 88 00 00 00 00 00 00 00 01 00 00 00 88 00 00 00 00 00 13 8C 88 00 00 00 01 00 00 00 01 00 00 00 84 00 00 00 00 00 00 00 01 00 00 00 84 00 00 00 00 00 13 8C 92 00 00 00 01 00 00 00 01 00 00 00 89 00 00 00 00 00 00 00 01 00 00 00 89 00 00 00 00");
            Serializables.m_vecMissionSlot(p,pw);
            Serializables.SerializeStages(p,pw);
            pw.HexArray("00 00 00 00 00 01 00 00 00 00 00 00 00 00 00 00 00 00 01 00 00 00 00 00 00 00 00");
            pw.Int(p.PInfo.m_dwInvetoryInfo);
            pw.HexArray("00 00 00 64 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 FF FF 00 00 00 00 00 00 00 00 00 00 40 47 1C");
            p.SendPacket(pw, 1722);
        }

        public void ChangeIndoor(Session p, PacketRead r)
        {
            byte[] data = new byte[r.Get_Payload().Length];
            Array.Copy(r.Get_Payload(), 0, data, 0, data.Length);
            PacketWrite pw = new PacketWrite();
            pw.ArrayBytes(data);
            p.PInfo.CurRoom.SendForAllPlayersInRoom(pw, 1724);
        }

        public void ChageSlot(Session p, PacketRead r)
        {
            PacketWrite pw = new PacketWrite();
            pw.HexArray("00 00 00 00 00 00 00 00 00 00 00 00");
            p.SendPacket(pw, 319);
        }

        internal void AddCoordiItem(uint _itemID, uint _ItemUID, int CharacterPosition, Session p)
        {
            p.PCharacters.CharInfo[CharacterPosition].LookItens.Add
                (
                    new Look() { ItemID = _itemID, ItemUID = _ItemUID }
                );
            Querys.Execute_InsertLook(p.PInfo.m_dwUserUID, p.PCharacters.CharInfo[CharacterPosition].CharType, _itemID, _ItemUID);
        }

        internal void AddEquipment(int _itemID, int _ItemUID, int CharacterPosition,Session p)
        {
            p.PCharacters.CharInfo[CharacterPosition].Equipements.Add
                (
                    new Equip() { ItemID = _itemID, ItemUID = _ItemUID}
                );
            Querys.Execute_InsertEquip(p.PInfo.m_dwUserUID, p.PCharacters.CharInfo[CharacterPosition].CharType, _itemID, _ItemUID);
        }

        internal void AddAtkPetSlot1(int _petAtkId, uint _petAtkUID,int charPosition,Session right)
        {
            right.PCharacters.CharInfo[charPosition].Pets[0].Slot1.Add(new PetAttacks() { AtkID = _petAtkId, AtkUID = _petAtkUID });
        }

        internal void AddAtkPetSlot2(int _petAtkId, uint _petAtkUID, int charPosition, Session right)
        {
            right.PCharacters.CharInfo[charPosition].Pets[0].Slot2.Add(new PetAttacks() { AtkID = _petAtkId, AtkUID = _petAtkUID });
        }

        internal void RemoveEquipment(int _itemID, int CharacterPosition,Session p)
        {
            for (int i = 0; i < p.PCharacters.CharInfo[CharacterPosition].Equipements.Count; i++)
            {
                if (p.PCharacters.CharInfo[CharacterPosition].Equipements[i].ItemID == _itemID)
                {
                    p.PCharacters.CharInfo[CharacterPosition].Equipements.Remove(p.PCharacters.CharInfo[CharacterPosition].Equipements[i]);
                    Querys.Execute_RemoveEquip(p.PInfo.m_dwUserUID, p.PCharacters.CharInfo[CharacterPosition].CharType, _itemID, p.PCharacters.CharInfo[CharacterPosition].Equipements[i].ItemUID);
                }
            }
        }

        internal void RemoveLook(uint _itemID, int CharacterPosition, Session p)
        {
            for (int i = 0; i < p.PCharacters.CharInfo[CharacterPosition].LookItens.Count; i++)
            {
                if (p.PCharacters.CharInfo[CharacterPosition].LookItens[i].ItemID == _itemID)
                {
                    p.PCharacters.CharInfo[CharacterPosition].LookItens.Remove(p.PCharacters.CharInfo[CharacterPosition].LookItens[i]);
                    Querys.Execute_RemoveLook(p.PInfo.m_dwUserUID, p.PCharacters.CharInfo[CharacterPosition].CharType, _itemID, p.PCharacters.CharInfo[CharacterPosition].LookItens[i].ItemUID);
                }
            }
        }

        public void CreateCharacter(Session p, PacketRead r)
        {
            try
            {
                byte chartype = r.Byte();
                int unk = r.Int();

                byte promotion = 3;
                long exp = 100;
                int level = 1;
                if (chartype == 17)
                {
                    promotion = 1;
                    exp = 28982;
                    level = 20;
                }
                else if (chartype == 16)
                {
                    promotion = 0;
                }
                else if (chartype == 18)
                {
                    promotion = 0;
                    exp = 28982;
                    level = 20;
                }
                else if (chartype == 15)
                {
                    promotion = 1;
                }
                else if (chartype == 10)
                {
                    exp = 114074;
                    level = 30;
                }
                else if (chartype == 19)
                {
                    exp = 114074;
                    level = 30;
                    promotion = 0;
                }
                int win = 0;
                int lose = 0;

                int totalequips = 0;
                int helm = 0;
                int upper = 0;
                int down = 0;
                int weapon = 0;
                int gloves = 0;
                int shoes = 0;
                int itemuid = 0;

                PacketWrite pw = new PacketWrite();
                pw.Int(0);
                pw.Byte(chartype);
                pw.Int(0);
                pw.Byte(promotion);
                pw.Byte(promotion);
                pw.Long(exp);
                pw.Int(win);
                pw.Int(lose);
                pw.Int(win);
                pw.Int(lose);
                pw.Long(exp);
                pw.Int(level);
                pw.Int(totalequips);
                if (totalequips > 0)
                {
                    itemuid += 1;
                    pw.Int(helm);
                    pw.Int(1);
                    pw.Int(itemuid);
                    pw.Int(-1);
                    pw.Int(-1);
                    pw.HexArray("00 02 00 00 00 00 00 00 00 0E 00 00 00 00 00 00 00 00 59 DF 1E A8 00 00 00 00 00 00 00 00 01 00 00 00 00 00 00 00 00 00 00 00 11 00 00 00 00 00 00 00 00");

                    itemuid += 1;
                    pw.Int(upper);
                    pw.Int(1);
                    pw.Int(itemuid);
                    pw.Int(-1);
                    pw.Int(-1);
                    pw.HexArray("00 02 00 00 00 00 00 00 00 0E 00 00 00 00 00 00 00 00 59 DF 1E A8 00 00 00 00 00 00 00 00 01 00 00 00 00 00 00 00 00 00 00 00 11 00 00 00 00 00 00 00 00");

                    itemuid += 1;
                    pw.Int(down);
                    pw.Int(1);
                    pw.Int(itemuid);
                    pw.Int(-1);
                    pw.Int(-1);
                    pw.HexArray("00 02 00 00 00 00 00 00 00 0E 00 00 00 00 00 00 00 00 59 DF 1E A8 00 00 00 00 00 00 00 00 01 00 00 00 00 00 00 00 00 00 00 00 11 00 00 00 00 00 00 00 00");

                    itemuid += 1;
                    pw.Int(weapon);
                    pw.Int(1);
                    pw.Int(itemuid);
                    pw.Int(-1);
                    pw.Int(-1);
                    pw.HexArray("00 02 00 00 00 00 00 00 00 0E 00 00 00 00 00 00 00 00 59 DF 1E A8 00 00 00 00 00 00 00 00 01 00 00 00 00 00 00 00 00 00 00 00 11 00 00 00 00 00 00 00 00");

                    itemuid += 1;
                    pw.Int(gloves);
                    pw.Int(1);
                    pw.Int(itemuid);
                    pw.Int(-1);
                    pw.Int(-1);
                    pw.HexArray("00 02 00 00 00 00 00 00 00 0E 00 00 00 00 00 00 00 00 59 DF 1E A8 00 00 00 00 00 00 00 00 01 00 00 00 00 00 00 00 00 00 00 00 11 00 00 00 00 00 00 00 00");

                    itemuid += 1;
                    pw.Int(shoes);
                    pw.Int(1);
                    pw.Int(itemuid);
                    pw.Int(-1);
                    pw.Int(-1);
                    pw.HexArray("00 02 00 00 00 00 00 00 00 0E 00 00 00 00 00 00 00 00 59 DF 1E A8 00 00 00 00 00 00 00 00 01 00 00 00 00 00 00 00 00 00 00 00 11 00 00 00 00 00 00 00 00");
                }
                pw.HexArray("00 00 00 02 00 00 00 A0 00 00 00 01 00 00 00 00 00 00 00 00 00 00 00 00 64 00 00 00 00 00 00 00 64 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 01 2C 00 00 01 2C 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 07");
                pw.Int(p.PCharacters.CharInfo.Length);
                pw.HexArray("00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 FF FF 00 00 00 00 00 00 00 07 D0 00 00 07 D0 00 00 00 0A 00 00 00 00 00 00 00 5A 00 00 00 64 00 00 00 00 00 00 00 00 FF F9 FD 78");
                Serializables.SerializeStages(p, pw);
                pw.Int(0);//Visuais or Equips - 690530

                pw.Int(promotion + 2);
                pw.Byte(chartype);
                pw.Byte((byte)255);
                pw.Int(0);
                for (byte y = 0; y < promotion + 1; y++)
                {
                    pw.Byte(chartype);
                    pw.Byte(y);
                    pw.Int(0);
                    /*for (int x = 0; x < TotalSkill; x++)
                    {
                        pw.Int(TotalSkills[x].SkillId);
                    }*/
                }
                pw.HexArray("00 00 00 02 00 00 00 14 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 01 00 00 00 01 00 00 00 00 00 00 00 00 00 00 00 00 00 02 00 00 00 02 00 00 00 00 00 00 00 00 00 00 00 00 00 03 00 00 00 03 00 00 00 00 00 00 00 00 00 00 00 00 00 04 00 00 00 04 00 00 00 00 00 00 00 00 00 00 00 00 00 05 00 00 00 05 00 00 00 00 00 00 00 00 00 00 00 00 00 06 00 00 00 06 00 00 00 00 00 00 00 00 00 00 00 00 00 07 00 00 00 07 00 00 00 00 00 00 00 00 00 00 00 00 00 08 00 00 00 08 00 00 00 00 00 00 00 00 00 00 00 00 00 09 00 00 00 09 00 00 00 00 00 00 00 00 00 00 00 00 00 0A 00 00 00 0A 00 00 00 00 00 00 00 00 00 00 00 00 00 0B 00 00 00 0B 00 00 00 00 00 00 00 00 00 00 00 00 00 0C 00 00 00 0C 00 00 00 00 00 00 00 00 00 00 00 00 00 0D 00 00 00 0D 00 00 00 00 00 00 00 00 00 00 00 00 00 0E 00 00 00 0E 00 00 00 00 00 00 00 00 00 00 00 00 00 0F 00 00 00 0F 00 00 00 00 00 00 00 00 00 00 00 00 00 10 00 00 00 10 00 00 00 00 00 00 00 00 00 00 00 00 00 11 00 00 00 11 00 00 00 00 00 00 00 00 00 00 00 00 00 12 00 00 00 12 00 00 00 00 00 00 00 00 00 00 00 00 00 13 00 00 00 13 00 00 00 00 00 00 00 00 00 00");
                pw.Int(p.PInfo.m_dwSlots - 1);
                pw.HexArray("00 00 00 01 00 00 E5 6A 00 00 00 00 00 D9 92 03 00 00 00 14 00 00 00 14 00 00 00 00 00 00 FF FF FF FF 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 FF FF FF FF 01 58 73 0D 0A F6 50 D8 00 00 0D 00 00 00 00 00 00 00 00 00 00 00 00 01 00 00 00 00 00 00 E5 6A 00 00 00 00 00 D9 92 03 0D 00 00 00 00");
                p.SendPacket(pw, 1410);

                Array.Resize(ref p.PCharacters.CharInfo, p.PCharacters.CharInfo.Length + 1);
                p.PCharacters.CharInfo[p.PCharacters.CharInfo.Length - 1].CharType = chartype;
                p.PCharacters.CharInfo[p.PCharacters.CharInfo.Length - 1].Promotion = promotion;
                p.PCharacters.CharInfo[p.PCharacters.CharInfo.Length - 1].Exp = exp;
                p.PCharacters.CharInfo[p.PCharacters.CharInfo.Length - 1].Level = level;
                p.PCharacters.CharInfo[p.PCharacters.CharInfo.Length - 1].Win = 0;
                p.PCharacters.CharInfo[p.PCharacters.CharInfo.Length - 1].Lose = 0;
                p.PCharacters.CharInfo[p.PCharacters.CharInfo.Length - 1].Equipements = new List<Equip>();
                p.PCharacters.CharInfo[p.PCharacters.CharInfo.Length - 1].Skills = new List<Skill>();
                p.PCharacters.CharInfo[p.PCharacters.CharInfo.Length - 1].LookItens = new List<Look>();
                p.PCharacters.CharInfo[p.PCharacters.CharInfo.Length - 1].Pets = new Pet[1];
                p.PCharacters.CharInfo[p.PCharacters.CharInfo.Length - 1].Pets[0].m_dwUID = 0;
                p.PCharacters.CharInfo[p.PCharacters.CharInfo.Length - 1].Pets[0].m_dwID = 0;
                p.PCharacters.CharInfo[p.PCharacters.CharInfo.Length - 1].Pets[0].m_strName = "";
                p.PCharacters.CharInfo[p.PCharacters.CharInfo.Length - 1].Pets[0].m_mapInitExp = 0;
                p.PCharacters.CharInfo[p.PCharacters.CharInfo.Length - 1].Pets[0].m_dwEXP = 0;
                p.PCharacters.CharInfo[p.PCharacters.CharInfo.Length - 1].Pets[0].m_iLevel = 0;
                p.PCharacters.CharInfo[p.PCharacters.CharInfo.Length - 1].Pets[0].m_cPromotion = 0;
                p.PCharacters.CharInfo[p.PCharacters.CharInfo.Length - 1].Pets[0].m_nHatchingID = 0;
                p.PCharacters.CharInfo[p.PCharacters.CharInfo.Length - 1].Pets[0].m_iInitSatiation = 0;
                p.PCharacters.CharInfo[p.PCharacters.CharInfo.Length - 1].Pets[0].m_iSatiation = 0;
                p.PCharacters.CharInfo[p.PCharacters.CharInfo.Length - 1].Pets[0].Slot1 = new List<PetAttacks>();
                p.PCharacters.CharInfo[p.PCharacters.CharInfo.Length - 1].Pets[0].Slot2 = new List<PetAttacks>();

                p.PInfo.m_dwSlots -= 1;
                Querys.Execute_InsertCharacter(p.PInfo.m_strLogin, chartype, promotion, exp, level);
                Querys.Execute_UpdateSLOTS(p.PInfo.m_dwUserUID, p.PInfo.m_dwSlots);
                Querys.Execute_InsertSP(p.PInfo.m_dwUserUID, chartype, exp);
                p.STInfo.SendGetFullSpInfo(p);

                PacketWrite pw2 = new PacketWrite();
                pw2.HexArray("00 00 00 00 00 00 00 02 00 14 DC DC 00 00 00 01 3B 9C 50 A1 00 00 00 00 5A 35 8E F2 5A 34 3D 72 00 00 00 00 00 14 DC E6 00 00 00 01 3B 9C 50 A2 00 00 00 00 5A 35 8E F2 5A 34 3D 72 00 00 00 00 00");
                p.SendPacket(pw2, 202);
            }
            catch(Exception ex)
            {
                Log.Write("{0}\n{1}", ex.Message, ex.StackTrace);
                PacketWrite pw = new PacketWrite();
                pw.HexArray("00 00 00 01 0D 00 00 00 00 00 00 00 00 00 00 00 00 00 64 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 64 00 00 00 01 00 00 00 06 00 0A 89 62 00 00 00 00 00 D9 91 FD 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 0A 89 6C 00 00 00 00 00 D9 91 FE 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 0A 89 76 00 00 00 00 00 D9 91 FF 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 0A 89 80 00 00 00 00 00 D9 92 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 0A 89 8A 00 00 00 00 00 D9 92 01 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 0A A0 BE 00 00 00 00 00 D9 92 02 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 02 00 00 00 A0 00 00 00 01 00 00 00 00 00 00 00 00 00 00 00 00 64 00 00 00 00 00 00 00 64 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 01 2C 00 00 01 2C 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 07 00 00 00 01 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 FF FF 00 00 00 00 00 00 00 07 D0 00 00 07 D0 00 00 00 0A 00 00 00 00 00 00 00 5A 00 00 00 64 00 00 00 00 00 00 00 00 FF F9 FD 78 00 00 00 55 00 00 00 07 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 08 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 09 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 0A 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 0B 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 0C 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 0D 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 0E 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 0F 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 10 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 11 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 12 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 13 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 14 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 15 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 16 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 17 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 18 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 19 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 1A 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 1B 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 1D 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 1E 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 24 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 27 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 28 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 29 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 2A 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 2B 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 2C 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 2D 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 2E 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 2F 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 30 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 31 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 32 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 33 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 34 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 35 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 36 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 37 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 38 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 39 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 3A 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 3B 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 3C 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 3D 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 3E 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 3F 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 40 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 43 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 44 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 45 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 46 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 47 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 48 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 49 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 4A 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 4B 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 4C 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 4E 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 4F 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 50 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 51 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 52 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 53 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 54 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 55 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 56 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 57 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 58 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 59 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 5A 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 5B 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 5C 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 5D 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 5E 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 5F 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 62 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 63 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 64 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 65 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 66 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 67 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 6A 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 06 00 0A 89 62 00 00 00 00 00 D9 91 FD FF FF FF FF FF FF FF FF 00 01 00 00 00 00 FF FF FF FF 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 01 00 00 00 00 00 02 00 00 00 02 00 03 01 41 10 00 00 01 02 01 41 00 00 00 00 00 00 00 00 00 00 00 00 00 FF FF FF FF 01 58 73 0D 0A F6 50 D8 00 00 0D 00 00 00 00 00 00 00 00 00 00 0A 89 6C 00 00 00 00 00 D9 91 FE FF FF FF FF FF FF FF FF 00 01 00 00 00 00 FF FF FF FF 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 01 00 00 00 00 00 02 00 00 00 02 00 07 01 3F 00 00 00 01 0C 01 3D 23 D7 0A 00 00 00 00 00 00 00 00 00 00 FF FF FF FF 01 58 73 0D 0A F6 50 D8 00 00 0D 00 00 00 00 00 00 00 00 00 00 0A 89 76 00 00 00 00 00 D9 91 FF FF FF FF FF FF FF FF FF 00 02 00 00 00 00 FF FF FF FF 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 02 00 00 00 00 00 02 00 00 00 00 01 02 00 00 00 03 00 01 01 41 00 00 00 01 08 01 3D A3 D7 0A 02 00 01 40 A0 00 00 00 00 00 00 00 00 00 00 00 00 FF FF FF FF 01 58 73 0D 0A F6 50 D8 00 00 0D 00 00 00 00 00 00 00 00 00 00 0A 89 80 00 00 00 00 00 D9 92 00 FF FF FF FF FF FF FF FF 00 01 00 00 00 00 FF FF FF FF 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 01 00 00 00 00 00 02 00 00 00 02 00 0A 01 3E 0F 5C 29 01 04 01 41 D8 00 00 00 00 00 00 00 00 00 00 00 00 FF FF FF FF 01 58 73 0D 0A F6 50 D8 00 00 0D 00 00 00 00 00 00 00 00 00 00 0A 89 8A 00 00 00 00 00 D9 92 01 FF FF FF FF FF FF FF FF 00 01 00 00 00 00 FF FF FF FF 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 01 00 00 00 00 00 02 00 00 00 02 00 07 01 3F 00 00 00 01 00 01 40 A0 00 00 00 00 00 00 00 00 00 00 00 00 FF FF FF FF 01 58 73 0D 0A F6 50 D8 00 00 0D 00 00 00 00 00 00 00 00 00 00 0A A0 BE 00 00 00 00 00 D9 92 02 FF FF FF FF FF FF FF FF 00 01 00 00 00 00 FF FF FF FF 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 01 00 00 00 00 00 02 00 00 00 02 00 03 01 41 10 00 00 01 09 01 00 00 00 00 00 00 00 00 00 00 00 00 00 00 FF FF FF FF 01 58 73 0D 0A F6 50 D8 00 00 0D 00 00 00 00 00 00 00 00 00 00 00 00 01 0D FF 00 00 00 03 00 00 03 4A 00 00 03 4B 00 00 03 4C 00 00 00 02 00 00 00 14 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 01 00 00 00 01 00 00 00 00 00 00 00 00 00 00 00 00 00 02 00 00 00 02 00 00 00 00 00 00 00 00 00 00 00 00 00 03 00 00 00 03 00 00 00 00 00 00 00 00 00 00 00 00 00 04 00 00 00 04 00 00 00 00 00 00 00 00 00 00 00 00 00 05 00 00 00 05 00 00 00 00 00 00 00 00 00 00 00 00 00 06 00 00 00 06 00 00 00 00 00 00 00 00 00 00 00 00 00 07 00 00 00 07 00 00 00 00 00 00 00 00 00 00 00 00 00 08 00 00 00 08 00 00 00 00 00 00 00 00 00 00 00 00 00 09 00 00 00 09 00 00 00 00 00 00 00 00 00 00 00 00 00 0A 00 00 00 0A 00 00 00 00 00 00 00 00 00 00 00 00 00 0B 00 00 00 0B 00 00 00 00 00 00 00 00 00 00 00 00 00 0C 00 00 00 0C 00 00 00 00 00 00 00 00 00 00 00 00 00 0D 00 00 00 0D 00 00 00 00 00 00 00 00 00 00 00 00 00 0E 00 00 00 0E 00 00 00 00 00 00 00 00 00 00 00 00 00 0F 00 00 00 0F 00 00 00 00 00 00 00 00 00 00 00 00 00 10 00 00 00 10 00 00 00 00 00 00 00 00 00 00 00 00 00 11 00 00 00 11 00 00 00 00 00 00 00 00 00 00 00 00 00 12 00 00 00 12 00 00 00 00 00 00 00 00 00 00 00 00 00 13 00 00 00 13 00 00 00 00 00 00 00 00 00 00 00 00 00 02 00 00 00 01 00 00 E5 6A 00 00 00 00 00 D9 92 03 00 00 00 14 00 00 00 14 00 00 00 00 00 00 FF FF FF FF 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 FF FF FF FF 01 58 73 0D 0A F6 50 D8 00 00 0D 00 00 00 00 00 00 00 00 00 00 00 00 01 00 00 00 00 00 00 E5 6A 00 00 00 00 00 D9 92 03 0D 00 00 00 00");
                p.SendPacket(pw, 1410);
            }
        }

        public void CreatePet(Session right, PacketRead rs)
        {
            byte cmOK = rs.Byte();
            int petID = rs.Int();
            int petNew = rs.Int();
            int petUID = rs.Int();
            string petName = rs.String();
            //Log.Write("OK:{0} , PetID:{1}, PetUID:{2}, PetName:{3}\n\n", cmOK, petID, petUID, petName);

            PacketWrite pw = new PacketWrite();
            pw.Int(0);//dwOK
            pw.Byte(right.PInfo.m_ucCharType);
            pw.Int(petNew);
            pw.Int(petUID);
            pw.Int(petID);
            pw.Str(petName);
            pw.HexArray("00 00 00 03 00 00 00 00 64 01 00 00 00 64 02 00 00 00 64 00 00 00 64 00 00 00 00 00 FF FF FF FF 00 00 03 E8 00 00 03 E8 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 FF 0B 00 00 00 00 00 00 00 00 01 00 0A 1E 00 00 00 00 00 00 D9 A0 2D 00 00 00 64 00 00 00 64 00 00 FF FF 00 00 FF FF FF FF 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 04 00 00 0B 00 00 00 00 00 00 00 00 00");

            right.SendPacket(pw, 214);
        }

        public void SetCurrentCharacter(Session right, PacketRead r)
        {
            int characterID = r.Int();
            PacketWrite pw = new PacketWrite();
            pw.Int(characterID);
            right.SendPacket(pw, 212);
        }

        public void EquipItem(Session right, PacketRead rs)
        {
            try
            {
                string m_strLogin = rs.UnicodeString();
                byte m_cCharIndex = rs.Byte();
                right.PInfo.m_ucCharType = m_cCharIndex;
                int m_vecCharInfo = rs.Int();
                if (m_vecCharInfo >= 2)
                {
                    Serializables.KEquipItemInfo(right, rs, m_vecCharInfo);
                }

                PacketWrite pw = new PacketWrite();

                if (m_vecCharInfo >= 2)
                {
                    byte inc = rs.Get_Payload()[rs.Get_Payload().Length - 1];
                    byte[] data = new byte[rs.Get_Payload().Length - 7 - 5 - inc - 6];
                    Array.Copy(rs.Get_Payload(), 7, data, 0, data.Length);
                    pw.ArrayBytes(data);
                }
                else
                {
                    pw.UnicodeStr(m_strLogin);
                    pw.Byte(m_cCharIndex);
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
                }
                pw.Int(right.PInfo.m_dwUserUID);
                pw.Short(0);

                right.SendPacket(pw, 96);
                if (right.PInfo.CurRoom != null)
                    right.PInfo.CurRoom.SendForAllPlayersInRoom(pw, 96);
            }
            catch
            {
                PacketWrite pw = new PacketWrite();

                byte inc = rs.Get_Payload()[rs.Get_Payload().Length - 1];
                byte[] data = new byte[rs.Get_Payload().Length - 7 - 5 - inc - 6];
                Array.Copy(rs.Get_Payload(), 7, data, 0, data.Length);
                pw.ArrayBytes(data);
                pw.Int(right.PInfo.m_dwUserUID);
                pw.Short(0);

                right.SendPacket(pw, 96);
                if (right.PInfo.CurRoom != null)
                    right.PInfo.CurRoom.SendForAllPlayersInRoom(pw, 96);
            }
        }

        public void LookEquip(Session right, PacketRead r)
        {
            try
            {
                int CharactersCount = r.Int();

                for (int i = 0; i < CharactersCount; i++)
                {
                    byte m_ucCharType = r.Byte();
                    int CharacterPosition = right.PCharacters.PositionCharacter(m_ucCharType, right);
                    r.Jump(8);
                    int ItemsCount = r.Int();
                    for (int j = 0; j < ItemsCount; j++)
                    {
                        r.Jump(4);
                        int ItemPosition = 0;
                        uint ItemUID = r.UInt();

                        for (int i2 = 0; i2 < right.PInventory.InventoryList.Count; i2++)
                        {
                            if (right.PInventory.InventoryList[i2].ItemUID == ItemUID)
                            {
                                ItemPosition = i2;
                                break;
                            }
                        }
                        right.PCharacters.AddCoordiItem(right.PInventory.InventoryList[ItemPosition].ItemID, ItemUID, CharacterPosition, right);
                    }
                    int DeleteCount = r.Int();
                    for (int j = 0; j < DeleteCount; j++)
                    {
                        r.Jump(4);
                        int ItemPosition = 0;
                        uint ItemUID = r.UInt();

                        for (int i2 = 0; i2 < right.PInventory.InventoryList.Count; i2++)
                        {
                            if (right.PInventory.InventoryList[i2].ItemUID == ItemUID)
                            {
                                ItemPosition = i2;
                                break;
                            }
                        }
                        right.PCharacters.RemoveLook(right.PInventory.InventoryList[ItemPosition].ItemID, CharacterPosition, right);
                    }
                }

                PacketWrite ks = new PacketWrite();

                ks.Int(right.PInfo.m_dwUserUID);
                ks.Int(right.PCharacters.CharInfo.Length);
                for (int i = 0; i < right.PCharacters.CharInfo.Length; i++)
                {
                    ks.Byte((byte)right.PCharacters.CharInfo[i].CharType);
                    ks.Int(right.PCharacters.CharInfo[i].LookItens.Count);
                    for (int j = 0; j < right.PCharacters.CharInfo[i].LookItens.Count; j++)
                    {
                        ks.UInt(right.PCharacters.CharInfo[i].LookItens[j].ItemID);
                        ks.Int(1);
                        ks.UInt(right.PCharacters.CharInfo[i].LookItens[j].ItemUID);
                        ks.HexArray("00 02 FF FF FF FF 00 00 00 00 00 00 00 00 00 00 00 00 00");
                    }
                }

                right.SendPacket(ks, 861);
                right.SendPacket(ks, 862);
                if(right.PInfo.CurRoom != null)
                    right.PInfo.CurRoom.SendForAllPlayersInRoom(ks, 862);
            }
            catch
            {
                PacketWrite pw = new PacketWrite();
                pw.Int(1);
                right.SendPacket(pw, 861);
            }
        } 
    }
}
