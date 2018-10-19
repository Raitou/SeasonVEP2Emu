using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using libcomservice.REQUEST;
using System.IO;
using libcomservice.Game.Hero;

namespace libcomservice
{
    public static class GameServer
    {        
        public static Database Sql;
        public const ushort m_usUdpPort = 9401;
        public static int m_usUsers = 0;
        public static short m_cType = 0;
        public const int m_usMaxUsers = 100;
        public const string m_dwIP = "158.69.101.201";//192.99.179.154
        public const ushort m_usTRelayServerPort = 9700;
        public const ushort m_usURelayServerPort = 9600;
        public const ushort m_usMsgPort = 9300;
        public const string m_vecMsg = "Welcome Grand Chase Ernas!\n\nwww.gcernas.com";
        public const string m_strServerName = "Grand Chase Ernas!";
        public const int m_iSessionInfo = 0;
        internal static List<Session> UsersList;
        internal static Dictionary<ushort, Room> List_Rooms;
        internal static List<DungeonsInfo> HeroEpic = new List<DungeonsInfo>();

        public static void ConfigServer()
        {
            List_Rooms = new Dictionary<ushort, Room>();
            UsersList = new List<Session>();
            LoadConfigurations();
            LoadHero();
        }

        internal static void SendForAllPlayersInServer(Common.PacketWrite pw, short opcode)
        {
            for (int x = 0; x < UsersList.Count; x++)
            {
                UsersList[x].SendPacket(pw,opcode);
            }
        }

        internal static void IncTime(Common.PacketWrite pw, short opcode)
        {
            for (int x = 0; x < UsersList.Count; x++)
            {
                UsersList[x].PInfo.m_dwPlayTime += 1;
            }
        }

        private static void LoadConfigurations()
        {
            var Ini_DBConfigurator = new TIniFile(string.Format(@"{0}\odbc.dsn", Directory.GetCurrentDirectory()));
            Sql = new Database
                (
                    Ini_DBConfigurator.Read("ODBC", "SERVER"),
                    Ini_DBConfigurator.Read("ODBC", "DATABASE"),
                    Ini_DBConfigurator.Read("ODBC", "UID"),
                    Ini_DBConfigurator.Read("ODBC", "PWD")
                );
            DataSet Query = new DataSet();
        }

        private static void LoadHero()
        {
            var HeroI = new DungeonsInfo() { DungeonID = 40, RequiredLevel = 40, TimeStamps = new List<TimeStamp>() };
            HeroI.TimeStamps.Add(new TimeStamp() { StampOpen = 1516406400, StampClose = 1516407600 });
            HeroI.TimeStamps.Add(new TimeStamp() { StampOpen = 1516408800, StampClose = 1516410000 });
            HeroI.TimeStamps.Add(new TimeStamp() { StampOpen = 1516411200, StampClose = 1516412400 });
            HeroI.TimeStamps.Add(new TimeStamp() { StampOpen = 1516413600, StampClose = 1516414800 });
            HeroI.TimeStamps.Add(new TimeStamp() { StampOpen = 1516416000, StampClose = 1516417200 });
            HeroI.TimeStamps.Add(new TimeStamp() { StampOpen = 1516418400, StampClose = 1516419600 });
            HeroI.TimeStamps.Add(new TimeStamp() { StampOpen = 1516420800, StampClose = 1516423200 });
            HeroI.TimeStamps.Add(new TimeStamp() { StampOpen = 1516424400, StampClose = 1516424400 });
            HeroI.TimeStamps.Add(new TimeStamp() { StampOpen = 1516429200, StampClose = 1516430400 });
            HeroI.TimeStamps.Add(new TimeStamp() { StampOpen = 1516432800, StampClose = 1516434000 });
            HeroI.TimeStamps.Add(new TimeStamp() { StampOpen = 1516435200, StampClose = 1516436400 });
            HeroI.TimeStamps.Add(new TimeStamp() { StampOpen = 1516437600, StampClose = 1516438800 });
            HeroI.TimeStamps.Add(new TimeStamp() { StampOpen = 1516440000, StampClose = 1516441200 });
            HeroI.TimeStamps.Add(new TimeStamp() { StampOpen = 1516442400, StampClose = 1516443600 });
            HeroI.TimeStamps.Add(new TimeStamp() { StampOpen = 1516444800, StampClose = 1516446000 });
            HeroI.TimeStamps.Add(new TimeStamp() { StampOpen = 1516447200, StampClose = 1516448400 });
            HeroI.TimeStamps.Add(new TimeStamp() { StampOpen = 1516449600, StampClose = 1516450800 });
            GameServer.HeroEpic.Add(HeroI);

            var HeroII = new DungeonsInfo() { DungeonID = 63, RequiredLevel = 30, TimeStamps = new List<TimeStamp>() };
            HeroII.TimeStamps.Add(new TimeStamp() { StampOpen = 1516406400, StampClose = 1516407600 });
            HeroII.TimeStamps.Add(new TimeStamp() { StampOpen = 1516408800, StampClose = 1516410000 });
            HeroII.TimeStamps.Add(new TimeStamp() { StampOpen = 1516411200, StampClose = 1516412400 });
            HeroII.TimeStamps.Add(new TimeStamp() { StampOpen = 1516413600, StampClose = 1516414800 });
            HeroII.TimeStamps.Add(new TimeStamp() { StampOpen = 1516416000, StampClose = 1516417200 });
            HeroII.TimeStamps.Add(new TimeStamp() { StampOpen = 1516418400, StampClose = 1516419600 });
            HeroII.TimeStamps.Add(new TimeStamp() { StampOpen = 1516420800, StampClose = 1516423200 });
            HeroII.TimeStamps.Add(new TimeStamp() { StampOpen = 1516424400, StampClose = 1516424400 });
            HeroII.TimeStamps.Add(new TimeStamp() { StampOpen = 1516429200, StampClose = 1516430400 });
            HeroII.TimeStamps.Add(new TimeStamp() { StampOpen = 1516432800, StampClose = 1516434000 });
            HeroII.TimeStamps.Add(new TimeStamp() { StampOpen = 1516435200, StampClose = 1516436400 });
            HeroII.TimeStamps.Add(new TimeStamp() { StampOpen = 1516437600, StampClose = 1516438800 });
            HeroII.TimeStamps.Add(new TimeStamp() { StampOpen = 1516440000, StampClose = 1516441200 });
            HeroII.TimeStamps.Add(new TimeStamp() { StampOpen = 1516442400, StampClose = 1516443600 });
            HeroII.TimeStamps.Add(new TimeStamp() { StampOpen = 1516444800, StampClose = 1516446000 });
            HeroII.TimeStamps.Add(new TimeStamp() { StampOpen = 1516447200, StampClose = 1516448400 });
            HeroII.TimeStamps.Add(new TimeStamp() { StampOpen = 1516449600, StampClose = 1516450800 });
            GameServer.HeroEpic.Add(HeroII);
        }
    }
}
