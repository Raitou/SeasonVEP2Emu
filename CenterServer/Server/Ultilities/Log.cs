using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Diagnostics;

namespace libcomservice
{
    public static class Log
    {
        public static bool Islog = false;
        public static void Start()
        {
            if (Ultilize.DATA.Connected)
            {
                Console.WriteLine(@"DriverConnect() success. m_strDSN : FILEDSN={0}\odbc_main.dsn",Directory.GetCurrentDirectory());
                Console.WriteLine(@"DriverConnect() success. m_strDSN : FILEDSN={0}\odbc_main.dsn", Directory.GetCurrentDirectory());
                Console.WriteLine(@"DriverConnect() success. m_strDSN : FILEDSN={0}\odbc_main.dsn", Directory.GetCurrentDirectory());
                Console.WriteLine(@"DriverConnect() success. m_strDSN : FILEDSN={0}\odbc_main.dsn", Directory.GetCurrentDirectory());
                Console.WriteLine(@"DriverConnect() success. m_strDSN : FILEDSN={0}\odbc_stat.dsn", Directory.GetCurrentDirectory());
                Console.WriteLine("\n-----------------------------------------------");
                Console.WriteLine("Change LogLevel (from 2 to 0 ).");
                Console.WriteLine("-----------------------------------------------");
                Console.WriteLine("Change LogLevel (from 2 to 0 ).");
                Console.WriteLine("KSubThread::Run(), thread start.");
                Console.WriteLine("cout : KBlockIPList::LoadScript(), KSubThread::Run(), thread start.");
                Console.WriteLine("2017-08-17 12:30:31, Blocked IP List Size : 0, ElapsedTime : 0");
                Console.WriteLine("cout : KCnDonationManager::LoadScript(), 2017-08-17 12:30:31, **** Server Initionize Finished.. ****");
                Console.WriteLine("");
                Console.WriteLine("Starting Lua Interpreter : ( Ctrl + z, Return ) To Terminate.");
                Console.WriteLine("");
                Console.WriteLine("Lua 5.0.2  Copyright (C) 1994-2004 Tecgraf, PUC-Rio");

            }
            else
            {
                Console.WriteLine(@"DriverConnect() Failed. m_strDSN : FILEDSN={0}\odbc_main.dsn", Directory.GetCurrentDirectory());
                Process.GetCurrentProcess().CloseMainWindow();
            }
        }

        public static void CheckLog(string LogStates)
        {
            if (LogStates == ">log(2)" | LogStates == "log(2)" | LogStates == ">>log(2)")
            {
                Console.WriteLine("Change LogLevel (from 0 to 2 ).");
                Log.Islog = true;
                Console.Write(">");
                CheckLog(Console.ReadLine());
            }
            else if (LogStates == ">^Z" | LogStates == "^Z" | LogStates == ">>^Z")
            {
                Process.GetCurrentProcess().CloseMainWindow();
            }
        }

        public static void Write(string text,params object[] args)
        {
            if (Log.Islog)
            {
                Console.WriteLine(text,args);
            }
        }

    }
}
