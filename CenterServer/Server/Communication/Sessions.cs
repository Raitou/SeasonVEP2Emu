using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Net.Sockets;
using System.Reflection;
using System.Runtime.Serialization;
using Common;
using System.Security.Cryptography;
using libcomservice.REQUEST;
using libcomservice.Request;


namespace libcomservice
{
    public class Session
    {
        private TcpClient C;
        private TcpListener S;        
        private int B_Size = 0;
        private byte[] Buffer = new byte[4000];

        private Handlers HRecv = new Handlers();
        private PacketGenerate Key_Crypt = new PacketGenerate();
        public Cryptography Cryptography = new Cryptography();
        private bool C_Running = false;
                        
        public SendLoadList Load = new SendLoadList();
        public CheckConnection Check_Connection = new CheckConnection();
        public PlayerInfo PInfo = new PlayerInfo();
        public VerifyAccount Login = new VerifyAccount();        
        public CheckRequests Req = new CheckRequests();
        
        public void Config_Session(TcpListener GET_SERVER,TcpClient GET_CLIENT)
        {            
            S = GET_SERVER;
            C = GET_CLIENT;
        }

        public void Init_Session()
        {
            Cryptography.CRYPTOGRAPHY_KEY = new byte[8] { 0xC7, 0xD8, 0xC4, 0xBF, 0xB5, 0xE9, 0xC0, 0xFD };
            Cryptography.AUTHENTIC_KEY = new byte[8] { 0xC0, 0xD3, 0xBD, 0xC3, 0xB7, 0xCE, 0xB8, 0xB8 };
        }
       
        public void Start_Handler()
        {
            HRecv.RegisterHandler(0, Check_Connection.GetType());
            HRecv.RegisterHandler(0, Check_Connection.GetType().GetMethod("HearBeat"));

            HRecv.RegisterHandler(2, Login.GetType());
            HRecv.RegisterHandler(2, Login.GetType().GetMethod("Login"));

            HRecv.RegisterHandler(21, Load.GetType());
            HRecv.RegisterHandler(21, Load.GetType().GetMethod("Loading"));

            HRecv.RegisterHandler(15, Req.GetType());
            HRecv.RegisterHandler(15, Req.GetType().GetMethod("HandlerGuideBook"));

            HRecv.RegisterHandler(23, Req.GetType());
            HRecv.RegisterHandler(23, Req.GetType().GetMethod("HandlerConfigPing"));
            
            HRecv.RegisterHandler(25, Req.GetType());
            HRecv.RegisterHandler(25, Req.GetType().GetMethod("HandlerSHAFileList"));
        }

        public void Create_Session(Object stateInfo)
        {
            C_Running = true;
            Init_Session();
            Start_Handler();
            Key_Crypt.SEND_CRYPTOBUFFER(this);
            Log.Write("clog : KTRUser::KSkTRUser::OnClientConnected(), {0}-{1}-{2}.", DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day);
            while (C_Running)
            {
                try
                {
                    B_Size = 0;
                    if (C_Running)
                    {
                        if ((B_Size = C.Client.Receive(Buffer, 0, 4000, 0)) > 32)
                            SESSION_RECEIVE(Buffer, B_Size);
                        else
                            Destroy_Session();

                    }
                    else
                        Destroy_Session();
                }
                catch
                {
                    Destroy_Session();
                }
            }            
        }

        public void SESSION_SEND(byte[] CONTENT, short PACKETID, bool FLAG = false, short PREFIX = -1)
        {
            Payload PAYLOAD = new Payload(CONTENT, PACKETID, FLAG);
            var PREFIX_GENERATOR = new Random();
            short NEW_PREFIX;
            if (PREFIX == -1)
            {
                NEW_PREFIX = (short)PREFIX_GENERATOR.Next();
            }
            else
            {
                NEW_PREFIX = 0;
            }

            int COUNT = 1;
            byte[] GENERATE_IV = new byte[8];            
            byte BYTE_TEMP;

            Random random = new Random();
            BYTE_TEMP = (byte)random.Next(0x00, 0xFF);

            for (int i = 0; i < GENERATE_IV.Length; i++)
            {
                GENERATE_IV[i] = BYTE_TEMP;
            }            

            byte[] ENCRYPT_BUFFER = Cryptography.ENCRYPT(PAYLOAD.Data,GENERATE_IV);

            ushort NEW_BUFFER_SIZE = (ushort)(16 + ENCRYPT_BUFFER.Length + 10);                        

            byte[] GENERATE_HMAC = new byte[10];

            HMACMD5 NEW_HMAC = new HMACMD5(Cryptography.AUTHENTIC_KEY);

            byte[] CONCAT_BUFFER = Sequence.Concat(BitConverter.GetBytes(PREFIX), BitConverter.GetBytes(COUNT), GENERATE_IV, ENCRYPT_BUFFER);
            
            byte[] AUTH_CODE = Sequence.ReadBlock(NEW_HMAC.ComputeHash(Sequence.Concat(BitConverter.GetBytes(PREFIX), BitConverter.GetBytes(COUNT), GENERATE_IV, ENCRYPT_BUFFER)), 0, 10);

            byte[] BUFFER_RESULT = Sequence.Concat(BitConverter.GetBytes(NEW_BUFFER_SIZE), CONCAT_BUFFER, AUTH_CODE);
            
            C.Client.Send(BUFFER_RESULT,0,BUFFER_RESULT.Length,0);
            Log.Write("clog : KTRUser::KSkTRUser::OnSendCompleted({0}), {0}-{1}-{2}. {3}:{4}:{5}",PACKETID, DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, DateTime.Now.Second, DateTime.Now.Minute, DateTime.Now.Hour);
        }


        private void SESSION_RECEIVE(byte[] _buffer, int _size)
        {
            int pos = 0;
            Read_Buffer RB_BUFFER = new Read_Buffer(_buffer, 0);
            ushort size = RB_BUFFER.UShort();
            while (pos < _size)
            {
                if (C_Running == false)
                    return;
                if (pos >= _size)
                    return;
                try
                {
                    byte[] n_buffer = new byte[size];
                    Array.Copy(_buffer, pos, n_buffer, 0, size);

                    Read_Buffer r0 = new Read_Buffer(n_buffer, 0);

                    pos += (ushort)((_buffer[pos + 1] << 8) | (_buffer[pos]));
                    ushort new_size = r0.UShort();
                    r0.Short();
                    r0.Int();

                    byte[] iv = r0.Buffer_Array_Bytes(8);
                    byte[] content = r0.Buffer_Array_Bytes(new_size - 16 - 10);
                    byte[] get_payload = Cryptography.DECRYPT(content, iv);

                    Read_Buffer rb = new Read_Buffer(get_payload, 0, true);
                    ushort packetId = (ushort)((get_payload[0] << 8) | (get_payload[1]));

                    if (HRecv.HANDLER.ContainsKey(packetId))
                    {
                        Log.Write("clog : KTRUser::KSkTRUser::OnRecvCompleted({6}), {0}-{1}-{2}. {3}:{4}:{5}", DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, DateTime.Now.Second, DateTime.Now.Minute, DateTime.Now.Hour, packetId);

                        Type type = Type.GetType(HRecv.TYPE_HANDLER[packetId].ToString());

                        object classInstance = Activator.CreateInstance(type, null);

                        HRecv.HANDLER[packetId].Invoke(classInstance, new object[] { this, rb });
                    }
                    else
                    {
                        Log.Write("clog : KTRUser::KSkTRUser::OnRecvFailed,Packet unknown {6}! {0}-{1}-{2}. {3}:{4}:{5}", DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, DateTime.Now.Second, DateTime.Now.Minute, DateTime.Now.Hour, packetId);
                        Log.Write("\nclog : KTRUser::KSkTRUser::OnRecvFailed, Payload: \n{0}", BitConverter.ToString(get_payload).Replace("-", " "));
                    }
                }
                catch (Exception ex)
                {
                    Log.Write("{0} \n {1}", ex.Message, ex.StackTrace);
                }
            }
        }


        public void Destroy_Session()
        {            
            Log.Write("clog : KTRUserManager::Tick(), {0}-{1}-{2}. {3}:{4}:{5}, Delete User : {6}", DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, DateTime.Now.Second, DateTime.Now.Minute, DateTime.Now.Hour, Thread.CurrentThread.ManagedThreadId);
            C_Running = false;
            C.Close();            
        }
    }
}
