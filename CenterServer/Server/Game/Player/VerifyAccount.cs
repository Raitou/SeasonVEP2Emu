using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Common;
using System.Data;
using libcomservice.Data;
using libcomservice.Request;

namespace libcomservice.REQUEST
{
    public class VerifyAccount
    {
        //1 Nada só fecha
        //2 Wtf nao faz nada
        //3 Incorrect Login or Password
        //4 Failed Autorized Gash
        //5 Your Game Client has not been updated to the last version.
        //6 Failed Autorized Gash
        //7 Incorrect Login or Password
        //8 Failed Autorized Gash
        //9 Incorrect Login or Password
        //10 Incorrect Login or Password
        //11 Incorrect Login or Password
        //12 Incorrect Login or Password
        //13 [Notice] Grand Chase is designed for players ages 7  and up. Cannot connect to the game
        //14 Acess Bloqued 
        //15 Your Game Client has not been updated to the last version.
        //16 "Message Erro In Korean"
        //17 the Connection has timed out. The server is not responding.
        //18 게임 클라이언트를 닫으시고, Bean Fun통해서 접속해주세요.
        //19 게임 클라이언트를 닫으시고, Bean Fun통해서 접속해주세요.        
        private string MarksURL = "http://www.gcreborn.com/GuildMarks/";

        public void Login(Session p, Read_Buffer r)
        {
            try
            {
                r.Int();
                p.PInfo.Username = r.String();
                p.PInfo.Passwd = r.String();

                PacketWrite WB = new PacketWrite();

                Log.Write("clog : KTRUser::KSkTRUser::CheckLogin::User {0} and Passwd {1}", p.PInfo.Username, p.PInfo.Passwd);
                int Login_Acess = Querys.Execute_VerifyAccount(p, p.PInfo.Username, p.PInfo.Passwd);
                if (Login_Acess == 0)
                {
                    SERVER_LIST Servers = new SERVER_LIST();
                    Servers.Send_List(p);
                    p.Req.HandlerNewsFromChannel(p, r);
                    p.Req.HandlerClientContents(p, r);
                    p.Req.HandlerSocketTablesInfo(p, r);
                    p.Req.HandlerCashBack(p, r);

                    Log.Write("clog : KTRUser::KSkTRUser::OnClientLoginOK(), {0}-{1}-{2}.", DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day);
                }
                WB.Int(Login_Acess);
                WB.UnicodeStr(p.PInfo.Username);
                WB.Str(p.PInfo.Passwd);
                WB.HexArray("00 00 00 00 14 0F 03 F7 4C 00 00 00 00 00 00 00 02 5A 5A 00 00 C9 8E 00 00 C9 8E");
                WB.UnicodeStr(MarksURL);
                WB.HexArray("00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 01 00 00 00 03 00 00 00 00 00 00 00 00 64 01 00 00 00 00 00 00 00 64 02 00 00 00 00 00 00 00 64 01 BF 80 00 00 FC 04 97 FF 00 00 00 00 00 00 00 00 00 00 00 00 00");

                WB.UnicodeStr(p.PInfo.Username);
                WB.UnicodeStr(p.PInfo.UserNick);

                WB.HexArray("00 00 00 04 01 00");

                WB.Int(p.PInfo.Guild_Contribution);
                WB.HexArray("00 00 00 00 00 00 00 00");

                WB.UnicodeStr(p.PInfo.Guild_Who);

                WB.HexArray("07 E1 06 12 00 00 00 07 E1 07 09 00 00 00 00 00 00 00 00 00 04");

                WB.UnicodeStr(p.PInfo.Guild_Name);                

                WB.HexArray("01 00 00 00 01");

                WB.UnicodeStr(p.PInfo.Guild_mark);

                WB.UnicodeStr(p.PInfo.Guild_desc);

                WB.HexArray("02 07 E1 05 15 00 00 00 00 02 E0 C2");

                WB.UnicodeStr(MarksURL);

                WB.Int(0);
                WB.Int(500000);

                WB.HexArray("00 00 00 C7 00 00 00 00 00 00 00 00 00 00 00 00 03 00 00 03 E8 00 00 00 03");

                WB.Int(1);
                WB.UnicodeStr(p.PInfo.Guild_desc);

                WB.Int(2);
                WB.UnicodeStr(p.PInfo.Guild_notice1);

                WB.Int(3);
                WB.UnicodeStr(p.PInfo.Guild_notice1);

                WB.Int(0);

                WB.UnicodeStr(MarksURL);

                WB.HexArray("00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 01 00 00 00 07 00 00 00 00 00 0C 1A C6 8E 02 00 00 00 00 0C 1A C6 8E 08 00 00 00 00 0C 1A C6 8E 09 00 00 00 00 0C 1A C6 8E 10 00 00 00 00 00 00 09 D3 11 00 00 00 00 0C 1A C6 8E 12 00 00 00 00 00 00 02 21 01 BF 80 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 FF FF 00 00 00 00 00 00 00 00 00 00 00 00 00 00");        

                p.SESSION_SEND(WB.Get_Packet(), 3);
            }
            catch (Exception Ex)
            {
                Log.Write("{0} \n\n {1}", Ex.Message, Ex.StackTrace);
            }
        }
    }
}
