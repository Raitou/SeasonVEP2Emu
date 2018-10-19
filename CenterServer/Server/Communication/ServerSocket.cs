using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Sockets;
using System.Net;
using System.Threading;

namespace libcomservice
{
    public class InitServer
    {
        private TcpListener SERVER; 
        private bool SERVER_RUNNING = false;
        private int THREADS_AVAILABLES = 2499;
        private int THREADS_RUNNING = 0;
        private int MAX_THREADS = 2500;
        public void START_SERVER(string Address,int Port)
        {
            SERVER = new TcpListener(IPAddress.Parse(Address), Port);
            SERVER.Start();
            SERVER.Server.Listen(MAX_THREADS);
            SERVER_RUNNING = true;            
            try
            {                
                ThreadPool.SetMaxThreads(MAX_THREADS, MAX_THREADS);                
                SERVER.BeginAcceptTcpClient(ACCEPT_CONNECTION, null);
            }
            catch(Exception ex)
            {
                Log.Write("{0} \n {1}",ex.Message,ex.StackTrace);
            }
        }

        public void ACCEPT_CONNECTION(IAsyncResult RESULT)
        {
            TcpClient CLIENT = SERVER.EndAcceptTcpClient(RESULT);
            try
            {
                THREADS_RUNNING += 1;
                THREADS_AVAILABLES -= 1;
                Session SESSIONS = new Session();
                SESSIONS.Config_Session(SERVER,CLIENT);
                ThreadPool.QueueUserWorkItem(SESSIONS.Create_Session);
                SERVER.BeginAcceptTcpClient(ACCEPT_CONNECTION, null);
            }
            catch
            {
                Log.Write("clog : KTRUser::KSkTRUser::OnClientFailedConnection(),{0}-{1}-{2}.", DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day);                
            }                            
        }

    }
}
