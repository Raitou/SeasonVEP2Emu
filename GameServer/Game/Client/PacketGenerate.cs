using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Common;
using System.Security.Cryptography;

namespace libcomservice.REQUEST
{
    class PacketGenerate
    {
        public byte[] GENERATE_()
        {
            byte[] GENERATE_KEY = new byte[8];

            RNGCryptoServiceProvider RANGE_GRYPTO_GENERATE = new RNGCryptoServiceProvider();            
            RANGE_GRYPTO_GENERATE.GetBytes(GENERATE_KEY);            
            return GENERATE_KEY;
        }

        public void SEND_CRYPTOBUFFER(Session PLAYER)
        {
            try
            {
                byte[] NEW_CRYPTO_KEY = GENERATE_();
                byte[] NEW_CRYPTO_AUTH = GENERATE_();
                PacketWrite WB = new PacketWrite();
                WB.Short(24787);
                WB.Int(8);
                WB.ArrayBytes(NEW_CRYPTO_AUTH);
                WB.Int(8);
                WB.ArrayBytes(NEW_CRYPTO_KEY);
                WB.Int(1);
                WB.Int(0);
                WB.Int(0);
                PLAYER.SendPacket(WB, 1);

                PLAYER.Cryptography.AUTHENTIC_KEY = NEW_CRYPTO_AUTH;
                PLAYER.Cryptography.CRYPTOGRAPHY_KEY = NEW_CRYPTO_KEY;

                PacketWrite WAIT_TIME = new PacketWrite();
                WAIT_TIME.Int(100);
                PLAYER.SendPacket(WAIT_TIME, 5);
                System.Threading.Thread.Sleep(100);
            }
            catch (Exception ex)
            {
                Log.Write("\n===========:Error:===========\n{0}\n{1}\n=============================n", ex.Message, ex.StackTrace);
            }
        }
    }
}
