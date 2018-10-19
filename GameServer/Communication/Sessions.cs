using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Net.Sockets;
using System.Reflection;
using System.Runtime.Serialization;
using System.Security.Cryptography;
using libcomservice.Game;
using Common;
using libcomservice.REQUEST;
using libcomservice.Game.Player.Channel;
using libcomservice.Game.Player;
using libcomservice.Game.Global;


namespace libcomservice
{
    public class Session
    {
        private TcpClient C;
        private TcpListener S;        
        private int B_Size = 0;        
        private Handlers HRecv = new Handlers();
        private PacketGenerate Key_Crypt = new PacketGenerate();
        public Cryptography Cryptography = new Cryptography();
        private bool C_Running = false;
        public SendLoadList Load = new SendLoadList();
        public CheckConnection Check_Connection = new CheckConnection();
        public PlayerInfo PInfo = new PlayerInfo();
        public VerifyAccount Login = new VerifyAccount();        
        public CheckRequests Req = new CheckRequests();
        public libcomservice.Game.Player.Inventory.Inventory PInventory = new libcomservice.Game.Player.Inventory.Inventory();
        public libcomservice.Game.Player.Channel.ChannelInfo PChannel = new libcomservice.Game.Player.Channel.ChannelInfo();
        public Nick_Manager Nick = new Nick_Manager();
        public shop Shop = new shop();
        public cChat Chat = new cChat();
        public Skill_Tree STInfo = new Skill_Tree();
        public Character PCharacters = new Character();
        public libcomservice.Game.RelayServer.RelayServer PRoom = new libcomservice.Game.RelayServer.RelayServer();
        public libcomservice.Request.Player.StagesInfo PStages = new libcomservice.Request.Player.StagesInfo();
        public libcomservice.Game.Hero.HeroDungeon HeroDugeons = new libcomservice.Game.Hero.HeroDungeon();
        public ListeningServers ServersList = new ListeningServers();
        public LetterBox PLetter = new LetterBox();
        public Quests userQuests = new Quests();
        public libcomservice.Game.Client.Gacha SystemREC = new Game.Client.Gacha();
        
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
            try
            {
                HRecv.RegisterHandler(0, Check_Connection.GetType());
                HRecv.RegisterHandler(0, Check_Connection.GetType().GetMethod("HearBeat"));

                HRecv.RegisterHandler(37, Chat.GetType());
                HRecv.RegisterHandler(37, Chat.GetType().GetMethod("OnChat"));

                HRecv.RegisterHandler(33, Login.GetType());
                HRecv.RegisterHandler(33, Login.GetType().GetMethod("Login"));

                HRecv.RegisterHandler(46, PChannel.GetType());
                HRecv.RegisterHandler(46, PChannel.GetType().GetMethod("ChannelList"));

                HRecv.RegisterHandler(44, PChannel.GetType());
                HRecv.RegisterHandler(44, PChannel.GetType().GetMethod("EnterChannel"));

                HRecv.RegisterHandler(48, PChannel.GetType());
                HRecv.RegisterHandler(48, PChannel.GetType().GetMethod("ListRooms"));

                HRecv.RegisterHandler(50, PChannel.GetType());
                HRecv.RegisterHandler(50, PChannel.GetType().GetMethod("UsersList"));

                HRecv.RegisterHandler(52, PChannel.GetType());
                HRecv.RegisterHandler(52, PChannel.GetType().GetMethod("EnterRoom"));

                HRecv.RegisterHandler(56, PChannel.GetType());
                HRecv.RegisterHandler(56, PChannel.GetType().GetMethod("CreateRoom"));

                HRecv.RegisterHandler(58, PChannel.GetType());
                HRecv.RegisterHandler(58, PChannel.GetType().GetMethod("LeaveChannel"));

                HRecv.RegisterHandler(60, PRoom.GetType());
                HRecv.RegisterHandler(60, PRoom.GetType().GetMethod("KChangeRoomInfo"));

                HRecv.RegisterHandler(65, PChannel.GetType());
                HRecv.RegisterHandler(65, PChannel.GetType().GetMethod("LeaveRoom"));

                HRecv.RegisterHandler(68, PRoom.GetType());
                HRecv.RegisterHandler(68, PRoom.GetType().GetMethod("StartGame"));

                HRecv.RegisterHandler(71, PRoom.GetType());
                HRecv.RegisterHandler(71, PRoom.GetType().GetMethod("LoadComplete"));

                HRecv.RegisterHandler(72, PRoom.GetType());
                HRecv.RegisterHandler(72, PRoom.GetType().GetMethod("KChangeRoomUserInfo"));

                HRecv.RegisterHandler(76, PRoom.GetType());
                HRecv.RegisterHandler(76, PRoom.GetType().GetMethod("LeaveGame"));

                HRecv.RegisterHandler(78, PRoom.GetType());
                HRecv.RegisterHandler(78, PRoom.GetType().GetMethod("EndGame"));

                HRecv.RegisterHandler(84, Shop.GetType());
                HRecv.RegisterHandler(84, Shop.GetType().GetMethod("BuyGP"));

                HRecv.RegisterHandler(94, PCharacters.GetType());
                HRecv.RegisterHandler(94, PCharacters.GetType().GetMethod("EquipItem"));

                HRecv.RegisterHandler(135, Nick.GetType());
                HRecv.RegisterHandler(135, Nick.GetType().GetMethod("Register"));

                HRecv.RegisterHandler(160, Req.GetType());
                HRecv.RegisterHandler(160, Req.GetType().GetMethod("MigrateServer"));

                HRecv.RegisterHandler(173, userQuests.GetType());
                HRecv.RegisterHandler(173, userQuests.GetType().GetMethod("RegisterMission"));

                HRecv.RegisterHandler(176, userQuests.GetType());
                HRecv.RegisterHandler(176, userQuests.GetType().GetMethod("CompleteMission"));

                HRecv.RegisterHandler(177, userQuests.GetType());
                HRecv.RegisterHandler(177, userQuests.GetType().GetMethod("RemoveMission"));

                HRecv.RegisterHandler(155, ServersList.GetType());
                HRecv.RegisterHandler(155, ServersList.GetType().GetMethod("SendList"));

                HRecv.RegisterHandler(211, PCharacters.GetType());
                HRecv.RegisterHandler(211, PCharacters.GetType().GetMethod("SetCurrentCharacter"));

                HRecv.RegisterHandler(213, PCharacters.GetType());
                HRecv.RegisterHandler(213, PCharacters.GetType().GetMethod("CreatePet"));

                HRecv.RegisterHandler(395, Shop.GetType());
                HRecv.RegisterHandler(395, Shop.GetType().GetMethod("BuyVC"));

                HRecv.RegisterHandler(433, STInfo.GetType());
                HRecv.RegisterHandler(433, STInfo.GetType().GetMethod("GetFullSpInfo"));

                HRecv.RegisterHandler(435, STInfo.GetType());
                HRecv.RegisterHandler(435, STInfo.GetType().GetMethod("SkillTraining"));

                HRecv.RegisterHandler(437, STInfo.GetType());
                HRecv.RegisterHandler(437, STInfo.GetType().GetMethod("SetSkill"));

                HRecv.RegisterHandler(453, SystemREC.GetType());
                HRecv.RegisterHandler(453, SystemREC.GetType().GetMethod("GachaRewardList"));

                HRecv.RegisterHandler(455, SystemREC.GetType());
                HRecv.RegisterHandler(455, SystemREC.GetType().GetMethod("GachaSetReward"));

                HRecv.RegisterHandler(463, SystemREC.GetType());
                HRecv.RegisterHandler(463, SystemREC.GetType().GetMethod("GachaSelectReward"));

                HRecv.RegisterHandler(676, Shop.GetType());
                HRecv.RegisterHandler(676, Shop.GetType().GetMethod("CheckItem"));

                HRecv.RegisterHandler(846, Req.GetType());
                HRecv.RegisterHandler(846, Req.GetType().GetMethod("IDLEState"));

                HRecv.RegisterHandler(853, PRoom.GetType());
                HRecv.RegisterHandler(853, PRoom.GetType().GetMethod("LoadState"));

                HRecv.RegisterHandler(860, PCharacters.GetType());
                HRecv.RegisterHandler(860, PCharacters.GetType().GetMethod("LookEquip"));

                HRecv.RegisterHandler(865, PInventory.GetType());
                HRecv.RegisterHandler(865, PInventory.GetType().GetMethod("BundleSellItens"));

                HRecv.RegisterHandler(886, HeroDugeons.GetType());
                HRecv.RegisterHandler(886, HeroDugeons.GetType().GetMethod("Catalog"));

                HRecv.RegisterHandler(888, HeroDugeons.GetType());
                HRecv.RegisterHandler(888, HeroDugeons.GetType().GetMethod("Material"));

                HRecv.RegisterHandler(913, PRoom.GetType());
                HRecv.RegisterHandler(913, PRoom.GetType().GetMethod("SpecialReward"));

                HRecv.RegisterHandler(920, Req.GetType());
                HRecv.RegisterHandler(920, Req.GetType().GetMethod("ReceiveExp"));

                HRecv.RegisterHandler(927, PRoom.GetType());
                HRecv.RegisterHandler(927, PRoom.GetType().GetMethod("LoadStage"));

                HRecv.RegisterHandler(1012, Req.GetType());
                HRecv.RegisterHandler(1012, Req.GetType().GetMethod("Choicebox"));

                HRecv.RegisterHandler(1042, Shop.GetType());
                HRecv.RegisterHandler(1042, Shop.GetType().GetMethod("packageInfo"));

                HRecv.RegisterHandler(1084, HeroDugeons.GetType());
                HRecv.RegisterHandler(1084, HeroDugeons.GetType().GetMethod("MaterialInfo"));

                HRecv.RegisterHandler(1105, Req.GetType());
                HRecv.RegisterHandler(1105, Req.GetType().GetMethod("AgitMapCatalogue"));

                HRecv.RegisterHandler(1113, Req.GetType());
                HRecv.RegisterHandler(1113, Req.GetType().GetMethod("AgitStoreCatalog"));

                HRecv.RegisterHandler(1183, Req.GetType());
                HRecv.RegisterHandler(1183, Req.GetType().GetMethod("FaityTreeLvTable"));

                HRecv.RegisterHandler(1225, Req.GetType());
                HRecv.RegisterHandler(1225, Req.GetType().GetMethod("InvenBuffItemList"));

                HRecv.RegisterHandler(111, Req.GetType());
                HRecv.RegisterHandler(111, Req.GetType().GetMethod("Kairos"));

                HRecv.RegisterHandler(1280, PLetter.GetType());
                HRecv.RegisterHandler(1280, PLetter.GetType().GetMethod("GetPostLetterList"));

                HRecv.RegisterHandler(1287, PLetter.GetType());
                HRecv.RegisterHandler(1287, PLetter.GetType().GetMethod("ReadLetter"));

                HRecv.RegisterHandler(1290, PLetter.GetType());
                HRecv.RegisterHandler(1290, PLetter.GetType().GetMethod("GetItemfromLetter"));

                HRecv.RegisterHandler(1337, Req.GetType());
                HRecv.RegisterHandler(1337, Req.GetType().GetMethod("ExpPotionList"));

                HRecv.RegisterHandler(1339, Req.GetType());
                HRecv.RegisterHandler(1339, Req.GetType().GetMethod("DepotInfoReq"));

                HRecv.RegisterHandler(1341, SystemREC.GetType());
                HRecv.RegisterHandler(1341, SystemREC.GetType().GetMethod("DepotInfo"));

                HRecv.RegisterHandler(1409, PCharacters.GetType());
                HRecv.RegisterHandler(1409, PCharacters.GetType().GetMethod("CreateCharacter"));

                HRecv.RegisterHandler(1556, Shop.GetType());
                HRecv.RegisterHandler(1556, Shop.GetType().GetMethod("CashRatio"));

                HRecv.RegisterHandler(1599, Shop.GetType());
                HRecv.RegisterHandler(1599, Shop.GetType().GetMethod("packageInfoDetail"));

                HRecv.RegisterHandler(1502, HeroDugeons.GetType());
                HRecv.RegisterHandler(1502, HeroDugeons.GetType().GetMethod("UpgradeInfo"));

                HRecv.RegisterHandler(1616, Shop.GetType());
                HRecv.RegisterHandler(1616, Shop.GetType().GetMethod("AddSlotChar"));

                HRecv.RegisterHandler(1620, PChannel.GetType());
                HRecv.RegisterHandler(1620, PChannel.GetType().GetMethod("ChangeCharInChannel"));

                HRecv.RegisterHandler(1647, PChannel.GetType());
                HRecv.RegisterHandler(1647, PChannel.GetType().GetMethod("RoomMyInfoDivide"));

                HRecv.RegisterHandler(1721, PCharacters.GetType());
                HRecv.RegisterHandler(1721, PCharacters.GetType().GetMethod("ChangeCharacterInRoom"));

                HRecv.RegisterHandler(1723, PCharacters.GetType());
                HRecv.RegisterHandler(1723, PCharacters.GetType().GetMethod("ChangeIndoor"));

                HRecv.RegisterHandler(1743, Req.GetType());
                HRecv.RegisterHandler(1743, Req.GetType().GetMethod("HeathPoint"));
            }
            catch (Exception ex)
            {
                Log.Write("\n===========:Error:===========\n{0}\n{1}\n=============================n", ex.Message, ex.StackTrace);
            }
        }

        public void Create_Session(Object stateInfo)
        {
            C_Running = true;
            Init_Session();
            Start_Handler();
            Key_Crypt.SEND_CRYPTOBUFFER(this);
            Log.Write("clog : KTRUser::KSkTRUser::OnClientConnected(), {0}-{1}-{2}.", DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day);
            byte[] buffer = new byte[65536];
            while (C_Running)
            {
                try
                {
                    B_Size = 0;
                    if (C_Running)
                    {
                        if ((B_Size = C.Client.Receive(buffer, 0, buffer.Length, 0)) > 32)
                            SESSION_RECEIVE(buffer, B_Size);
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
                finally
                {
                    Log.Write("packet finished.");
                }
            }
        }

        public void SendPacket(PacketWrite Pw, short PACKETID, bool FLAG = false, short PREFIX = -1)
        {
            try
            {
                Payload PAYLOAD = new Payload(Pw.Get_Packet(), PACKETID, FLAG);
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

                byte[] ENCRYPT_BUFFER = Cryptography.ENCRYPT(PAYLOAD.Data, GENERATE_IV);

                ushort NEW_BUFFER_SIZE = (ushort)(16 + ENCRYPT_BUFFER.Length + 10);

                byte[] GENERATE_HMAC = new byte[10];

                HMACMD5 NEW_HMAC = new HMACMD5(Cryptography.AUTHENTIC_KEY);

                byte[] CONCAT_BUFFER = Sequence.Concat(BitConverter.GetBytes(PREFIX), BitConverter.GetBytes(COUNT), GENERATE_IV, ENCRYPT_BUFFER);

                byte[] AUTH_CODE = Sequence.ReadBlock(NEW_HMAC.ComputeHash(Sequence.Concat(BitConverter.GetBytes(PREFIX), BitConverter.GetBytes(COUNT), GENERATE_IV, ENCRYPT_BUFFER)), 0, 10);

                byte[] BUFFER_RESULT = Sequence.Concat(BitConverter.GetBytes(NEW_BUFFER_SIZE), CONCAT_BUFFER, AUTH_CODE);

                C.Client.Send(BUFFER_RESULT, 0, BUFFER_RESULT.Length, 0);
                Log.Write("clog : KTRUser::KSkTRUser::OnSendCompleted({0}), {0}-{1}-{2}. {3}:{4}:{5}", PACKETID, DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, DateTime.Now.Second, DateTime.Now.Minute, DateTime.Now.Hour);
            }
            catch (Exception ex)
            {
                Log.Write("\n===========:Error:===========\n{0}\n{1}\n=============================n", ex.Message, ex.StackTrace);
            }
        }


        private void SESSION_RECEIVE(byte[] _buffer, int _size)
        {
            int pos = 0;
            PacketRead RB_BUFFER = new PacketRead(_buffer, 0);
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

                    PacketRead r0 = new PacketRead(n_buffer, 0);

                    pos += (ushort)((_buffer[pos + 1] << 8) | (_buffer[pos]));
                    ushort new_size = r0.UShort();
                    r0.Short();
                    r0.Int();

                    byte[] iv = r0.Buffer_Array_Bytes(8);
                    byte[] content = r0.Buffer_Array_Bytes(new_size - 16 - 10);
                    byte[] get_payload = Cryptography.DECRYPT(content, iv);

                    PacketRead rb = new PacketRead(get_payload, 0, true);
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
                        PacketWrite p = new PacketWrite();
                        p.Int(0);
                        SendPacket(p, (short)(packetId + 1));
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
            try
            {
                libcomservice.Data.Querys.Execute_UpdatePlayerTimer(PInfo.m_dwPlayTime, PInfo.m_dwUserUID);
                if (PInfo.CurRoom != null)
                    ChannelInfo.ProcessExit(this);
                GameServer.m_usUsers -= 1;
                GameServer.UsersList.Remove(this);
                Log.Write("clog : KTRUserManager::Tick(), {0}-{1}-{2}. {3}:{4}:{5}, Delete User : {6}", DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, DateTime.Now.Second, DateTime.Now.Minute, DateTime.Now.Hour, Thread.CurrentThread.ManagedThreadId);
                C_Running = false;
                C.Close();
            }
            catch (Exception ex)
            {
                Log.Write("\n===========:Error:===========\n{0}\n{1}\n=============================n", ex.Message, ex.StackTrace);
            }
        }
    }
}
