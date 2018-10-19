using Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace libcomservice.Game.Client
{
    public class Gacha
    {
        public void DepotInfo(Session p, PacketRead r)
        {
            r.Jump(5);
            p.PInfo.m_ucCharType = r.Byte();
            //Log.Write("Depot, CharType:{0}", p.PInfo.m_ucCharType);
            PacketWrite pw = new PacketWrite();
            pw.Int(5);
            pw.Byte(0);
            pw.Byte(p.PInfo.m_ucCharType);
            pw.HexArray("01 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00");
            p.SendPacket(pw, 1342);
        }

        public void GachaRewardList(Session p, PacketRead r)
        {
            int unkvalue = r.Int();
            int account = r.Int();
            //Log.Write("GachaRewardList, value:{0}", unkvalue);
            //Log.Write("GachaRewardList, value:{0}", account);
            PacketWrite pw = new PacketWrite();
            pw.Int(unkvalue);
            pw.Int(account);
            pw.HexArray("01 00 00 00 02 00 00 00 00 00 00 00 06 00 0B F3 C4 00 0B F3 CE 00 0B F3 D8 00 0B F3 E2 00 0B F3 EC 00 0B F3 F6 00 00 00 01 00 00 00 05 00 0B F8 10 00 0B F8 1A 00 0B F8 24 00 0B F8 2E 00 0B F8 E2 00 00 00 06 00 00 00 1E 00 00 00 02 00 00 00 28 00 00 00 02 00 00 00 32 00 00 00 03 00 00 00 3C 00 00 00 03 00 00 00 46 00 00 00 04 00 00 00 50 00 00 00 05 00 00 00 00");
            p.SendPacket(pw, 454);
        }

        public void GachaSetReward(Session p, PacketRead r)
        {
            int unkvalue = r.Int();
            int account = r.Int();
            //Log.Write("GachaSetReward, value:{0}", unkvalue);
            //Log.Write("GachaSetReward, value:{0}", account);
            PacketWrite pw = new PacketWrite();
            pw.Int(unkvalue);
            pw.Int(account);
            pw.HexArray("00 00 00 00 00 00 00 00 00 00 00 02 00 00 00 00 00 00 00 01 00 0B F5 F4 00 00 00 01 00 00 00 01 00 0C 22 CC");
            p.SendPacket(pw, 456);
        }

        public void GachaSelectReward(Session p, PacketRead r)
        {
            PacketWrite pw = new PacketWrite();
            pw.HexArray("00 00 00 04 00 00 00 C6 00 00 00 00 00 00 00 C7 00 00 00 00 00 00 00 C8 00 00 00 00 00 00 00 C9 00 00 00 00");
            p.SendPacket(pw, 464);
        }
    }
}
