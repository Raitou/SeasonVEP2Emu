using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Security.Cryptography;

namespace libcomservice
{
    public class Cryptography
    {
        DESCryptoServiceProvider DES = new DESCryptoServiceProvider();
        public byte[] CRYPTOGRAPHY_KEY { get; set; }
        public byte[] AUTHENTIC_KEY  { get; set; }
        public Cryptography()
        {
            DES.Mode = CipherMode.CBC;
            DES.Padding = PaddingMode.None;
        }

        public byte[] DECRYPT(byte[] buffer, byte[] IV)
        {
            byte[] resultBuffer;

            using (ICryptoTransform decryptor = DES.CreateDecryptor(CRYPTOGRAPHY_KEY, IV))
            {
                resultBuffer = decryptor.TransformFinalBlock(buffer, 0, buffer.Length);
            }
            return resultBuffer;
        }

        public byte[] ENCRYPT(byte[] data, byte[] IV)
        {
            byte[] dataToEncrypt = PadData(data);
            byte[] encryptedData;

            using (ICryptoTransform encryptor = DES.CreateEncryptor(CRYPTOGRAPHY_KEY, IV))
            {
                encryptedData = encryptor.TransformFinalBlock(dataToEncrypt, 0, dataToEncrypt.Length);
            }

            return encryptedData;
        }

        //GET_PADDING
        private static byte[] PadData(byte[] data)
        {
            int distance = 8 - (data.Length % 8);
            int paddingLength = (distance >= 3) ? (distance) : (8 + distance);

            byte[] padding = new byte[paddingLength];

            for (int i = 0; i < (paddingLength-1); i++)
            {                
                padding[i] = (byte)(i+1);
            }            
            padding[paddingLength-1] = padding[paddingLength -2];

            return Sequence.Concat(data, padding);
        }
    }
}
