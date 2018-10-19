using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Common;

namespace libcomservice.REQUEST
{
    public class SendLoadList
    {
        private string[] TtLoad = new string[4];
        private string[] Match = new string[3];
        private string[] Square = new string[3];

        public SendLoadList()
        {
            TtLoad[0] = "Load1_1.dds";
            TtLoad[1] = "Load1_2.dds";
            TtLoad[2] = "Load1_3.dds";
            TtLoad[3] = "LoadGauge.dds";
            Square[0] = "Square.lua";
            Square[1] = "SquareObject.lua";
            Square[2] = "Square3DObject.lua";
            Match[0] = "ui_match_load1.dds";
            Match[1] = "ui_match_load2.dds";
            Match[2] = "ui_match_load3.dds";
        }

        public void Loading(Session p, PacketRead r)
        {
            PacketWrite WB = new PacketWrite();
            WB.HexArray("00 00 00 00 00 00 00 05 00 00 00 00 00 00 00 01 00 00 00");
            WB.HexArray("02 00 00 00 03 00 00 00 04 00 00 00 01 00 00 00 00");

            WB.Int(TtLoad.Length);
            WB.UnicodeStr(TtLoad[0]);
            WB.UnicodeStr(TtLoad[1]);
            WB.UnicodeStr(TtLoad[2]);
            WB.UnicodeStr(TtLoad[3]);
            WB.HexArray("00 00 00 02 00 00 00 00 00 00 00 01 00 00 00 01 00 00 00 00");

            WB.Int(Match.Length);
            WB.UnicodeStr(Match[0]);
            WB.UnicodeStr(Match[1]);
            WB.UnicodeStr(Match[2]);
            WB.Int(0);

            WB.Int(Square.Length);
            WB.Int(0);
            WB.UnicodeStr(Square[0]);
            WB.Int(1);
            WB.UnicodeStr(Square[1]);
            WB.Int(2);
            WB.UnicodeStr(Square[2]);
            WB.HexArray("00 00 00 03 00 00 00 00 00 00 00 01 00 00 00 02 00 00 00 00");
            WB.Int(0);
            p.SendPacket(WB, 22);
            WaitTime(p, r);
        }

        public void WaitTime(Session PLAYER, PacketRead RB)
        {
            PacketWrite wb = new PacketWrite();
            wb.Int(100);
            PLAYER.SendPacket(wb,5);
        }
    }
}
