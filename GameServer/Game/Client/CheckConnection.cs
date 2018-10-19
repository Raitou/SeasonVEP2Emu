using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Common;

namespace libcomservice
{
    public class CheckConnection
    {
        public void HearBeat(Session p,PacketRead r)
        {
            PacketWrite pw = new PacketWrite();
            pw.Int(0);
            p.SendPacket(pw,0);
        }
    }
}
