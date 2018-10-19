using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace libcomservice.Request.Server
{
    public class EXPReward
    {
        public bool isLevelUP = false;
        public void getExp(int exp, Session userid, int characterid, int level)
        {
            if ((exp >= 100) && (exp < 240) && (level < 1))
            {
                updateNivel(1, userid, characterid);
                isLevelUP = true; return;
            }
            else if ((exp >= 240) && (exp < 548) && (level < 2))
            {
                updateNivel(2, userid, characterid);
                isLevelUP = true; return;
            }
            else if ((exp >= 548) && (exp < 884) && (level < 3))
            {
                updateNivel(3, userid, characterid);
                isLevelUP = true; return;
            }
            else if ((exp >= 884) && (exp < 1430) && (level < 4))
            {
                updateNivel(4, userid, characterid);
                isLevelUP = true; return;
            }
            else if ((exp >= 1430) && (exp < 2060) && (level < 5))
            {
                updateNivel(5, userid, characterid);
                isLevelUP = true; return;
            }
            else if ((exp >= 2060) && (exp < 2732) && (level < 6))
            {
                updateNivel(6, userid, characterid);
                isLevelUP = true; return;
            }
            else if ((exp >= 2732) && (exp < 3488) && (level < 7))
            {
                updateNivel(7, userid, characterid);
                isLevelUP = true; return;
            }
            else if ((exp >= 3488) && (exp < 4286) && (level < 8))
            {
                updateNivel(8, userid, characterid);
                isLevelUP = true; return;
            }
            else if ((exp >= 4286) && (exp < 5168) && (level < 9))
            {
                updateNivel(9, userid, characterid);
                isLevelUP = true; return;
            }
            else if ((exp >= 5168) && (exp < 6680) && (level < 10))
            {
                updateNivel(10, userid, characterid);
                isLevelUP = true; return;
            }
            else if ((exp >= 6680) && (exp < 8318) && (level < 11))
            {
                updateNivel(11, userid, characterid);
                isLevelUP = true; return;
            }
            else if ((exp >= 8318) && (exp < 10145) && (level < 12))
            {
                updateNivel(12, userid, characterid);
                isLevelUP = true; return;
            }
            else if ((exp >= 10145) && (exp < 12098) && (level < 13))
            {
                updateNivel(13, userid, characterid);
                isLevelUP = true; return;
            }
            else if ((exp >= 12098) && (exp < 14303) && (level < 14))
            {
                updateNivel(14, userid, characterid);
                isLevelUP = true; return;
            }
            else if ((exp >= 14303) && (exp < 16697) && (level < 15))
            {
                updateNivel(15, userid, characterid);
                isLevelUP = true; return;
            }
            else if ((exp >= 16697) && (exp < 19343) && (level < 16))
            {
                updateNivel(16, userid, characterid);
                isLevelUP = true; return;
            }
            else if ((exp >= 19343) && (exp < 22241) && (level < 17))
            {
                updateNivel(17, userid, characterid);
                isLevelUP = true; return;
            }
            else if ((exp >= 22241) && (exp < 25454) && (level < 18))
            {
                updateNivel(18, userid, characterid);
                isLevelUP = true; return;
            }
            else if ((exp >= 25454) && (exp < 28982) && (level < 19))
            {
                updateNivel(19, userid, characterid);
                isLevelUP = true; return;
            }
            else if ((exp >= 28982) && (exp < 32825) && (level < 20))
            {
                updateNivel(20, userid, characterid);
                isLevelUP = true; return;
            }
            else if ((exp >= 32825) && (exp < 37046) && (level < 21))
            {
                updateNivel(21, userid, characterid);
                isLevelUP = true; return;
            }
            else if ((exp >= 37046) && (exp < 124376) && (level < 22))
            {
                updateNivel(22, userid, characterid);
                isLevelUP = true; return;
            }
            else if ((exp >= 124376) && (exp < 50066) && (level < 23))
            {
                updateNivel(23, userid, characterid);
                isLevelUP = true; return;
            }
            else if ((exp >= 50066) && (exp < 57626) && (level < 24))
            {
                updateNivel(24, userid, characterid);
                isLevelUP = true; return;
            }
            else if ((exp >= 57626) && (exp < 65858) && (level < 25))
            {
                updateNivel(25, userid, characterid);
                isLevelUP = true; return;
            }
            else if ((exp >= 65858) && (exp < 74930) && (level < 26))
            {
                updateNivel(26, userid, characterid);
                isLevelUP = true; return;
            }
            else if ((exp >= 74930) && (exp < 84926) && (level < 27))
            {
                updateNivel(27, userid, characterid);
                isLevelUP = true; return;
            }
            else if ((exp >= 84926) && (exp < 95930) && (level < 28))
            {
                updateNivel(28, userid, characterid);
                isLevelUP = true; return;
            }
            else if ((exp >= 95930) && (exp < 114074) && (level < 29))
            {
                updateNivel(29, userid, characterid);
                isLevelUP = true; return;
            }
            else if ((exp >= 114074) && (exp < 134108) && (level < 30))
            {
                updateNivel(30, userid, characterid);
                isLevelUP = true; return;
            }
            else if ((exp >= 134108) && (exp < 156032) && (level < 31))
            {
                updateNivel(31, userid, characterid);
                isLevelUP = true; return;
            }
            else if ((exp >= 156032) && (exp < 180224) && (level < 32))
            {
                updateNivel(32, userid, characterid);
                isLevelUP = true; return;
            }
            else if ((exp >= 180224) && (exp < 206810) && (level < 33))
            {
                updateNivel(33, userid, characterid);
                isLevelUP = true; return;
            }
            else if ((exp >= 206810) && (exp < 236042) && (level < 34))
            {
                updateNivel(34, userid, characterid);
                isLevelUP = true; return;
            }
            else if ((exp >= 236042) && (exp < 268172) && (level < 35))
            {
                updateNivel(35, userid, characterid);
                isLevelUP = true; return;
            }
            else if ((exp >= 268172) && (exp < 315380) && (level < 36))
            {
                updateNivel(36, userid, characterid);
                isLevelUP = true; return;
            }
            else if ((exp >= 315380) && (exp < 367292) && (level < 37))
            {
                updateNivel(37, userid, characterid);
                isLevelUP = true; return;
            }
            else if ((exp >= 367292) && (exp < 424412) && (level < 38))
            {
                updateNivel(38, userid, characterid);
                isLevelUP = true; return;
            }
            else if ((exp >= 424412) && (exp < 487244) && (level < 39))
            {
                updateNivel(39, userid, characterid);
                isLevelUP = true; return;
            }
            else if ((exp >= 487244) && (exp < 556292) && (level < 40))
            {
                updateNivel(40, userid, characterid);
                isLevelUP = true; return;
            }
            else if ((exp >= 556292) && (exp < 632396) && (level < 41))
            {
                updateNivel(41, userid, characterid);
                isLevelUP = true; return;
            }
            else if ((exp >= 632396) && (exp < 716060) && (level < 42))
            {
                updateNivel(42, userid, characterid);
                isLevelUP = true; return;
            }
            else if ((exp >= 716060) && (exp < 808124) && (level < 43))
            {
                updateNivel(43, userid, characterid);
                isLevelUP = true; return;
            }
            else if ((exp >= 808124) && (exp < 909260) && (level < 44))
            {
                updateNivel(44, userid, characterid);
                isLevelUP = true; return;
            }
            else if ((exp >= 909260) && (exp < 1020644) && (level < 45))
            {
                updateNivel(45, userid, characterid);
                isLevelUP = true; return;
            }
            else if ((exp >= 1020644) && (exp < 1173734) && (level < 46))
            {
                updateNivel(46, userid, characterid);
                isLevelUP = true; return;
            }
            else if ((exp >= 1173734) && (exp < 1342154) && (level < 47))
            {
                updateNivel(47, userid, characterid);
                isLevelUP = true; return;
            }
            else if ((exp >= 1342154) && (exp < 1527374) && (level < 48))
            {
                updateNivel(48, userid, characterid);
                isLevelUP = true; return;
            }
            else if ((exp >= 1527374) && (exp < 1771814) && (level < 49))
            {
                updateNivel(49, userid, characterid);
                isLevelUP = true; return;
            }
            else if ((exp >= 1771814) && (exp < 2040698) && (level < 50))
            {
                updateNivel(50, userid, characterid);
                isLevelUP = true; return;
            }
            else if ((exp >= 2040698) && (exp < 2336546) && (level < 51))
            {
                updateNivel(51, userid, characterid);
                isLevelUP = true; return;
            }
            else if ((exp >= 2336546) && (exp < 2716100) && (level < 52))
            {
                updateNivel(52, userid, characterid);
                isLevelUP = true; return;
            }
            else if ((exp >= 2716100) && (exp < 3133580) && (level < 53))
            {
                updateNivel(53, userid, characterid);
                isLevelUP = true; return;
            }
            else if ((exp >= 3133580) && (exp < 3766190) && (level < 54))
            {
                updateNivel(54, userid, characterid);
                isLevelUP = true; return;
            }
            else if ((exp >= 3766190) && (exp < 4462385) && (level < 55))
            {
                updateNivel(55, userid, characterid);
                isLevelUP = true; return;
            }
            else if ((exp >= 4462385) && (exp < 5228240) && (level < 56))
            {
                updateNivel(56, userid, characterid);
                isLevelUP = true; return;
            }
            else if ((exp >= 5228240) && (exp < 6070640) && (level < 57))
            {
                updateNivel(57, userid, characterid);
                isLevelUP = true; return;
            }
            else if ((exp >= 6070640) && (exp < 6997280) && (level < 58))
            {
                updateNivel(58, userid, characterid);
                isLevelUP = true; return;
            }
            else if ((exp >= 6997280) && (exp < 8118158) && (level < 59))
            {
                updateNivel(59, userid, characterid);
                isLevelUP = true; return;
            }
            else if ((exp >= 8118158) && (exp < 9351302) && (level < 60))
            {
                updateNivel(60, userid, characterid);
                isLevelUP = true; return;
            }
            else if ((exp >= 9351302) && (exp < 10707849) && (level < 61))
            {
                updateNivel(61, userid, characterid);
                isLevelUP = true; return;
            }
            else if ((exp >= 10707849) && (exp < 12199828) && (level < 62))
            {
                updateNivel(62, userid, characterid);
                isLevelUP = true; return;
            }
            else if ((exp >= 12199828) && (exp < 13990252) && (level < 63))
            {
                updateNivel(63, userid, characterid);
                isLevelUP = true; return;
            }
            else if ((exp >= 13990252) && (exp < 15960010) && (level < 64))
            {
                updateNivel(64, userid, characterid);
                isLevelUP = true; return;
            }
            else if ((exp >= 15960010) && (exp < 18307147) && (level < 65))
            {
                updateNivel(65, userid, characterid);
                isLevelUP = true; return;
            }
            else if ((exp >= 18307147) && (exp < 20889103) && (level < 66))
            {
                updateNivel(66, userid, characterid);
                isLevelUP = true; return;
            }
            else if ((exp >= 20889103) && (exp < 23947501) && (level < 67))
            {
                updateNivel(67, userid, characterid);
                isLevelUP = true; return;
            }
            else if ((exp >= 23947501) && (exp < 27311512) && (level < 68))
            {
                updateNivel(68, userid, characterid);
                isLevelUP = true; return;
            }
            else if ((exp >= 27311512) && (exp < 32402572) && (level < 69))
            {
                updateNivel(69, userid, characterid);
                isLevelUP = true; return;
            }
            else if ((exp >= 32402572) && (exp < 40803172) && (level < 70))
            {
                updateNivel(70, userid, characterid);
                isLevelUP = true; return;
            }
            else if ((exp >= 40803172) && (exp < 51582577) && (level < 71))
            {
                updateNivel(71, userid, characterid);
                isLevelUP = true; return;
            }
            else if ((exp >= 51582577) && (exp < 65134297) && (level < 72))
            {
                updateNivel(72, userid, characterid);
                isLevelUP = true; return;
            }
            else if ((exp >= 65134297) && (exp < 81905077) && (level < 73))
            {
                updateNivel(73, userid, characterid);
                isLevelUP = true; return;
            }
            else if ((exp >= 81905077) && (exp < 102403477) && (level < 74))
            {
                updateNivel(74, userid, characterid);
                isLevelUP = true; return;
            }
            else if ((exp >= 102403477) && (exp < 145331114) && (level < 75))
            {
                updateNivel(75, userid, characterid);
                isLevelUP = true; return;
            }
            else if ((exp >= 145331114) && (exp < 216875489) && (level < 76))
            {
                updateNivel(76, userid, characterid);
                isLevelUP = true; return;
            }
            else if ((exp >= 216875489) && (exp < 321807989) && (level < 77))
            {
                updateNivel(77, userid, characterid);
                isLevelUP = true; return;
            }
            else if ((exp >= 321807989) && (exp < 466089239) && (level < 78))
            {
                updateNivel(78, userid, characterid);
                isLevelUP = true; return;
            }
            else if ((exp >= 466089239) && (exp < 679394639) && (level < 79))
            {
                updateNivel(79, userid, characterid);
                isLevelUP = true; return;
            }
            else if ((exp >= 679394639) && (exp < 849426871) && (level < 80))
            {
                updateNivel(80, userid, characterid);
                isLevelUP = true; return;
            }
            else if ((exp >= 849426871) && (exp < 1062013104) && (level < 81))
            {
                updateNivel(81, userid, characterid);
                isLevelUP = true; return;
            }
            else if ((exp >= 1062013104) && (exp < 1327803336) && (level < 82))
            {
                updateNivel(82, userid, characterid);
                isLevelUP = true; return;
            }
            else if ((exp >= 1327803336) && (exp < 1660112942) && (level < 83))
            {
                updateNivel(83, userid, characterid);
                isLevelUP = true; return;
            }
            else if ((exp >= 1660112942) && (exp < 2075589740) && (level < 84))
            {
                updateNivel(84, userid, characterid);
                isLevelUP = true; return;
            }
            else if ((exp >= 2075589740) && (level < 85))
            {
                updateNivel(85, userid, characterid);
                isLevelUP = true; return;
            }
        }

        private void updateNivel(int lv, Session player, int characterid)
        {
            DataSet Banco = new DataSet();
            GameServer.Sql.Exec(Banco, "UPDATE  Characters SET  Level = '{0}' WHERE Login = '{1}'   AND CharType = '{2}'", lv, player.PInfo.m_strLogin, characterid);
            for (int i = 0; i < player.PCharacters.CharInfo.Length; i++)
            {
                if (player.PCharacters.CharInfo[i].CharType == characterid)
                {
                    player.PCharacters.CharInfo[i].Level = lv;
                }
            }
        }

        public void updateExp(int exp, string login, int characterid)
        {
            DataSet Banco = new DataSet();
            GameServer.Sql.Exec(Banco, "UPDATE   Characters SET  Exp = '{0}' WHERE Login = '{1}'   AND CharType = '{2}'", exp, login, characterid);
        }
    }
}
