using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Common;

namespace libcomservice
{
    public class CheckConnection
    {
        public void HearBeat(Session PLAYER,Read_Buffer RB)
        {
            PacketWrite WB = new PacketWrite();
            WB.Int(0);
            PLAYER.SESSION_SEND(WB.Get_Packet(),0);

        }
    }
}
