using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace libcomservice.REQUEST
{
    public class PlayerInfo
    {
        public int m_dwAuthType = 0;
        public int m_dwUserUID;
        public int m_dwSlots = 0;
        public int m_dwPlayTime = 0;
        public string m_strLogin;
        public string m_strPasswd;
        public bool m_bMale = false;
        public int m_iVersion = 0;        
        public string m_strNickName = "";
        internal int m_dwChannelUID = 0;
        internal string m_cStatus = "...";
        public int m_iGamePoint = 0;
        internal int m_iVirtualPoint = 0;
        public byte m_ucCharType = 0;
        internal int m_iDiaryPoint = 700;
        internal int m_iLifePoint = 3;
        internal int m_dwInvetoryInfo = 500;        
        public string m_strGetIP = "0.0.0.0";
        public uint m_dwChecksum = 0;
        public int WorldBossPoint = 0;
        public int Guild_Contribution = 0;
        public string m_strGuildName = "Ernas Players";
        public string Guild_Who = "Hiro Gato.";
        public string m_aiGuildMark = "4_1.png";
        public string Guild_desc = "Guilda Apenas Para Moderadores!\nwww.ernas.com\n";
        public string Guild_notice1 = "What";
        public string Guild_notice2 = "Null";
        public List<Mission> Missions = new List<Mission>();
        public Room CurRoom;

        internal int SquareID = -1;
    }
}
