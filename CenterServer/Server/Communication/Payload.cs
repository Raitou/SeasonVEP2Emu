namespace Common
{
    public class Payload
    {
        private const int ID_OFFSET = 0;
        private const int COMPRESSION_FLAG_OFFSET = 6;
        private const int CONTENT_OFFSET = 7;
        private const int PAYLOAD_HEADER_LENGTH = 7;
                
        public byte[] Data { get; private set; }

        public Payload(byte[] content, short packetId, bool isCompressed)
        {
            var writer = new PacketWrite();//Data

            writer.Short(packetId);
            writer.Int(content.Length);
            writer.Bool(isCompressed);
            writer.ArrayBytes(content);

            Data = new byte[writer.Get_Packet().Length];
            Data = writer.Get_Packet();
        }

        public Payload(byte[] payload)
        {
            Data = payload;
        }
    }
}