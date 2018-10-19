using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using libcomservice;
using System.Net;

namespace Server
{
    class Program
    {
        static void Main(string[] args)
        {
            if (Process.GetProcessesByName("OLLYDBG").Length > 0)
            {
                Process.GetCurrentProcess().Close();
            }
            else if (Process.GetProcessesByName("Ollydbg").Length > 0)
            {
                Process.GetCurrentProcess().Close();
            }
            else if (Process.GetProcessesByName("ollydbg").Length > 0)
            {
                Process.GetCurrentProcess().Close();
            }
            else if (Process.GetProcessesByName("IDA Pro").Length > 0)
            {
                Process.GetCurrentProcess().Close();
            }
            else if (Process.GetProcessesByName("IDA").Length > 0)
            {
                Process.GetCurrentProcess().Close();
            }
            else if (Process.GetProcessesByName("x32dbg").Length > 0)
            {
                Process.GetCurrentProcess().Close();
            }
            else if (Process.GetProcessesByName("x64dbg").Length > 0)
            {
                Process.GetCurrentProcess().Close();
            }
            else if (Process.GetProcessesByName("x86dbg").Length > 0)
            {
                Process.GetCurrentProcess().Close();
            }
            else
            {
                string clientl = "array.resize";

                string failedpacket = "";
                while (true)
                {
                    var key = System.Console.ReadKey(true);
                    if (key.Key == ConsoleKey.Enter)
                        break;
                    failedpacket += key.KeyChar;
                }
                if (failedpacket == clientl)
                {     
                    Ultilize.ConfigServer();
                    Log.Start();

                    InitServer SERVER = new InitServer();
                    SERVER.START_SERVER("198.50.218.64", 9501);

                    Console.Write("\n>");
                    Log.CheckLog(Console.ReadLine());
                    while (true)
                    {
                        Console.Write(">>");
                        Log.CheckLog(Console.ReadLine());
                    }
                }
            }
        }        
    }
}
