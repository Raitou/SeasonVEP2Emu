using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Sockets;
using System.Net;
using System.Threading;
using libcomservice.Core;

namespace libcomservice
{
    public class InitServer
    {
        private TcpListener SERVER;
        private bool Running = false;
        public void START_SERVER(string Address,int Port)
        {
            SERVER = new TcpListener(IPAddress.Parse(Address), Port);
            SERVER.Start();
            SERVER.Server.Listen(GameServer.m_usMaxUsers);
            Running = true;

            ThreadPool.QueueUserWorkItem(timeServer);
            try
            {
                ThreadPool.SetMaxThreads(GameServer.m_usMaxUsers, GameServer.m_usMaxUsers);                
                SERVER.BeginAcceptTcpClient(ACCEPT_CONNECTION, null);
            }
            catch(Exception ex)
            {
                SERVER.Stop();
                Running = false;
                Log.Write("{0} \n {1}",ex.Message,ex.StackTrace);
                GameServer.m_usUsers = 0;
                START_SERVER(Address, Port);
            }
        }

        public void ACCEPT_CONNECTION(IAsyncResult RESULT)
        {            
            try
            {
                TcpClient CLIENT = SERVER.EndAcceptTcpClient(RESULT);                
                if (GameServer.m_usMaxUsers != GameServer.m_usUsers)
                {
                    GameServer.m_usUsers += 1;
                    Session SESSIONS = new Session();
                    SESSIONS.Config_Session(SERVER, CLIENT);
                    ThreadPool.QueueUserWorkItem(SESSIONS.Create_Session);
                    SERVER.BeginAcceptTcpClient(ACCEPT_CONNECTION, null);
                }
            }
            catch
            {
                Log.Write("clog : KTRUser::KSkTRUser::OnClientFailedConnection(),{0}-{1}-{2}.", DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day);                
            }                            
        }

        private void timeServer(Object stateInfo)
        {
            while (Running)
            {
                try
                {
                    Thread.Sleep(900);
                    ServerTime();
                    if (DateTime.Now == DateTime.Parse(string.Format("{0}-{1}-{2} 00:00:00", DateTime.Now.Day, DateTime.Now.Month, DateTime.Now.Year)))
                    {
                        libcomservice.Data.Querys.Execute_ResetCalendar();
                        libcomservice.Data.Querys.Execute_UpdatePeriod();
                    }
                    exception();
                }
                catch (Exception ex)
                {
                    Log.Write("Error in Time server:\n{0}\n{1}",ex.Message,ex.StackTrace);
                }
            }
        }

        private void Alarm(int minutes,int dungeonid,int open,int close,bool isopen = false)
        {
            //Log.Write("Sending Alarm Dungeon...");
            Common.PacketWrite ks = new Common.PacketWrite();
            ks.Byte(0);
            ks.Bool(isopen);
            ks.Int(dungeonid);//00 00 00 3E 
            ks.Int(minutes);//00 00 00 03 
            ks.Int(open);//B9 AB 5E 5A
            ks.Int(close);//59 CB B5 E8
            GameServer.SendForAllPlayersInServer(ks, 893);
        }

        private void exception()
        {
            int timeStamp = Serializables.m_timeStamp();
            for (int x = 0; x < GameServer.HeroEpic.Count; x++ )
            {
                for (int x2 = 0; x2 < GameServer.HeroEpic[x].TimeStamps.Count; x2++)
                {
                    //Log.Write("Current:{0} ,Hero:{1}", timeStamp, GameServer.HeroEpic[x].TimeStamps[x2].StampOpen);
                    if ((GameServer.HeroEpic[x].TimeStamps[x2].StampOpen - 300) == timeStamp)
                    {
                        Alarm(5, GameServer.HeroEpic[x].DungeonID, GameServer.HeroEpic[x].TimeStamps[x2].StampOpen, GameServer.HeroEpic[x].TimeStamps[x2].StampClose);
                    }
                    else if ((GameServer.HeroEpic[x].TimeStamps[x2].StampOpen - 180) == timeStamp)
                    {
                        Alarm(3, GameServer.HeroEpic[x].DungeonID, GameServer.HeroEpic[x].TimeStamps[x2].StampOpen, GameServer.HeroEpic[x].TimeStamps[x2].StampClose);
                    }
                    else if ((GameServer.HeroEpic[x].TimeStamps[x2].StampOpen - 120) == timeStamp)
                    {
                        Alarm(2, GameServer.HeroEpic[x].DungeonID, GameServer.HeroEpic[x].TimeStamps[x2].StampOpen, GameServer.HeroEpic[x].TimeStamps[x2].StampClose);
                    }
                    else if ((GameServer.HeroEpic[x].TimeStamps[x2].StampClose - 300) == timeStamp)
                    {
                        Alarm(5, GameServer.HeroEpic[x].DungeonID, GameServer.HeroEpic[x].TimeStamps[x2].StampOpen, GameServer.HeroEpic[x].TimeStamps[x2].StampClose, true);
                    }
                    else if ((GameServer.HeroEpic[x].TimeStamps[x2].StampClose - 180) == timeStamp)
                    {
                        Alarm(3, GameServer.HeroEpic[x].DungeonID, GameServer.HeroEpic[x].TimeStamps[x2].StampOpen, GameServer.HeroEpic[x].TimeStamps[x2].StampClose, true);
                    }
                    else if ((GameServer.HeroEpic[x].TimeStamps[x2].StampClose - 120) == timeStamp)
                    {
                        Alarm(2, GameServer.HeroEpic[x].DungeonID, GameServer.HeroEpic[x].TimeStamps[x2].StampOpen, GameServer.HeroEpic[x].TimeStamps[x2].StampClose, true);
                    }
                }
            }
        }

        public void ServerTime()
        {
            DateTime d2 = DateTime.Now;

            Common.PacketWrite ks = new Common.PacketWrite();
            ks.Int(Serializables.m_timeStamp());
            ks.Int(d2.Year);
            ks.Int(d2.Month);
            ks.Int(d2.Day);
            ks.Int(d2.Hour);
            ks.Int(d2.Minute);
            ks.Int(d2.Second);

            GameServer.SendForAllPlayersInServer(ks, 416);
        }

    }
}
