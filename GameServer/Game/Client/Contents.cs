﻿using Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace libcomservice.Request
{
    public enum MatchModes : int
    {
        GC_GMC_MATCH = 0,
        GC_GMC_GUILD_BATTLE = 1,
        GC_GMC_DUNGEON = 2,
        GC_GMC_INDIGO = 3,
        GC_GMC_TUTORIAL = 4,
        GC_GMC_TAG_MATCH = 5,
        GC_GMC_MONSTER_CRUSADER = 6,
        GC_GMC_MONSTER_HUNT = 7,
        GC_GMC_DEATHMATCH = 8,
        GC_GMC_MINIGAME = 9,
        GC_GMC_ANGELS_EGG = 10,
        GC_GMC_CAPTAIN = 11,
        GC_GMC_DOTA = 12,
        GC_GMC_AGIT = 13,
        GC_GMC_AUTOMATCH = 14,
        GC_GMC_FATAL_DEATHMATCH = 15,
        GC_GMC_MONSTERMATCH = 16,
    }

    public struct Events
    {
        public string TextureFile;
        public int[] RewardItems;
        public int MBoxID;
        public int EventCoin;
        public int EventUID;
    }

    public struct Characters
    {
        public int CharId;
        public int MaxPromotion;
    }

    public class ClientContents
    {
        private int MaxLevel = 95;

        private List<Characters> lCharacters = new List<Characters>();
        private List<Events> lEvents = new List<Events>();

        public ClientContents()
        {
            LoadCharacters();
            LoadEvents();
        }

        private void LoadEvents()
        {
            lEvents.Add(new Events() { EventUID = 0, EventCoin = 413430, TextureFile = "tex_gc_mbox_gawibawibo_dlg.dds", RewardItems = new int[] { 413430 }, MBoxID = 149 });
            lEvents.Add(new Events() { EventUID = 1, EventCoin = 9854, TextureFile = "tex_gc_eclipse_plot_dlg.dds", RewardItems = new int[] { 791120 }, MBoxID = 183 });            
        }

        private void LoadCharacters()
        {
            lCharacters.Add(new Characters() { CharId = 0, MaxPromotion = 4 });
            lCharacters.Add(new Characters() { CharId = 1, MaxPromotion = 4 });
            lCharacters.Add(new Characters() { CharId = 2, MaxPromotion = 4 });
            lCharacters.Add(new Characters() { CharId = 3, MaxPromotion = 4 });
            lCharacters.Add(new Characters() { CharId = 4, MaxPromotion = 4 });
            lCharacters.Add(new Characters() { CharId = 5, MaxPromotion = 4 });
            lCharacters.Add(new Characters() { CharId = 6, MaxPromotion = 4 });
            lCharacters.Add(new Characters() { CharId = 7, MaxPromotion = 4 });
            lCharacters.Add(new Characters() { CharId = 8, MaxPromotion = 4 });
            lCharacters.Add(new Characters() { CharId = 9, MaxPromotion = 4 });
            lCharacters.Add(new Characters() { CharId = 10, MaxPromotion = 4 });
            lCharacters.Add(new Characters() { CharId = 11, MaxPromotion = 4 });
            lCharacters.Add(new Characters() { CharId = 12, MaxPromotion = 4 });
            lCharacters.Add(new Characters() { CharId = 13, MaxPromotion = 4 });
            lCharacters.Add(new Characters() { CharId = 14, MaxPromotion = 4 });
            lCharacters.Add(new Characters() { CharId = 15, MaxPromotion = 2 });
            lCharacters.Add(new Characters() { CharId = 16, MaxPromotion = 2 });
            lCharacters.Add(new Characters() { CharId = 17, MaxPromotion = 2 });
            lCharacters.Add(new Characters() { CharId = 18, MaxPromotion = 2 });
            lCharacters.Add(new Characters() { CharId = 19, MaxPromotion = 1 });
        }

        public void Contents(Session player)
        {
            PacketWrite Write = new PacketWrite();
            Write.HexArray("00 00 00 00 00 00 00 03 00 00 00 00 00 00 00 01 00 00 00 02 00 00 00 0A 00 00 00 00 00 00 00 01 00 00 00 02 00 00 00 09 00 00 00 01 00 00 00 01 00 00 00 0A 00 00 00 01 00 00 00 01 00 00 00 0E 00 00 00 01 00 00 00 01 00 00 00 12 00 00 00 01 00 00 00 01 00 00 00 14 00 00 00 01 00 00 00 01 00 00 00 02 00 00 00 01 00 00 00 03 00 00 00 03");

            //MatchModes
            Write.Int(4);
            Write.Int((int)MatchModes.GC_GMC_MATCH);
            Write.Int((int)MatchModes.GC_GMC_INDIGO);
            Write.Int((int)MatchModes.GC_GMC_DEATHMATCH);
            Write.Int((int)MatchModes.GC_GMC_ANGELS_EGG);

            Write.HexArray("00 00 00 08 00 00 00 01 00 00 00 09 00 00 00 15 00 00 00 01 00 00 00 0D 00 00 00 0E 00 00 00 00 00 00 00 02 00 00 00 01 00 00 00 02 00 00 00 01 00 00 00 01 00 00 00 05 00 00 00 03 00 00 00 01 00 00 00 06 00 00 00 04 00 00 00 01 00 00 00 00 00 00 00 05 00 00 00 02 00 00 00 03 00 00 00 04 00 00 00 06 00 00 00 01 00 00 00 1C 00 00 00 07 00 00 00 01 00 00 00 1D 00 00 00 08 00 00 00 02 00 00 00 1F 00 00 00 20 00 00 00 02 00 00 00 42");
            //Stages
            Write.HexArray("00 00 00 07 00 00 00 08 00 00 00 09 00 00 00 0A 00 00 00 0B 00 00 00 0C 00 00 00 0D 00 00 00 0E 00 00 00 0F 00 00 00 10 00 00 00 11 00 00 00 12 00 00 00 13 00 00 00 14 00 00 00 15 00 00 00 16 00 00 00 17 00 00 00 18 00 00 00 19 00 00 00 1A 00 00 00 1B 00 00 00 1E 00 00 00 24 00 00 00 27 00 00 00 28 00 00 00 29 00 00 00 2A 00 00 00 2B 00 00 00 2C 00 00 00 2D 00 00 00 2E 00 00 00 2F 00 00 00 30 00 00 00 31 00 00 00 32 00 00 00 37 00 00 00 38 00 00 00 39 00 00 00 3A 00 00 00 3B 00 00 00 3C 00 00 00 3D 00 00 00 3E 00 00 00 3F 00 00 00 40 00 00 00 46 00 00 00 49 00 00 00 4A 00 00 00 4B 00 00 00 4C 00 00 00 4E 00 00 00 4F 00 00 00 50 00 00 00 51 00 00 00 54 00 00 00 55 00 00 00 56 00 00 00 57 00 00 00 58 00 00 00 59 00 00 00 5A 00 00 00 5B 00 00 00 5C 00 00 00 5D 00 00 00 5E 00 00 00 5F");

            Write.HexArray("00 00 00 09 00 00 00 02 00 00 00 21 00 00 00 22 00 00 00 0A 00 00 00 01 00 00 00 25 00 00 00 0B 00 00 00 01 00 00 00 26 00 00 00 0D 00 00 00 01 00 00 00 42 00 00 00 0C 00 00 00 01 00 00 00 41 00 00 00 0B 00 00 00 04 00 00 00 00 01 00 00 00 63 00 00 00 00 01 00 00 00 0C 00 00 00 00 00 00 00 01 00 00 00 02 00 00 00 04 00 00 00 07 00 00 00 08 00 00 00 09 00 00 00 0A 00 00 00 0B 00 00 00 2B 00 00 00 2F 00 00 00 5F 00 00 00 01 01 00 00 00 0D 00 00 00 0A 00 00 00 64 00 00 00 00 00 00 00 01 00 00 00 02 00 00 00 03 00 00 00 09 00 00 00 04 00 00 00 06 00 00 00 05 00 00 00 08 00 00 00 07 00 00 00 0B 00 00 00 05 01 00 00 00 0F 00 00 00 64 00 00 00 00 00 00 00 01 00 00 00 02 00 00 00 03 00 00 00 04 00 00 00 05 00 00 00 06 00 00 00 07 00 00 00 08 00 00 00 09 00 00 00 0A 00 00 00 0B 00 00 00 2B 00 00 00 2F 00 00 00 03 00 00 00 00 08 00 00 00 64 00 00 00 00 00 00 00 01 00 00 00 09 00 00 00 0B 00 00 00 02 00 00 00 0A 00 00 00 04 00 00 00 0C 01 00 00 00 01 00 00 00 00 00 00 00 08 00 00 00 00 0D 00 00 00 00 00 00 00 01 00 00 00 02 00 00 00 03 00 00 00 04 00 00 00 06 00 00 00 07 00 00 00 08 00 00 00 09 00 00 00 0A 00 00 00 0B 00 00 00 2B 00 00 00 2F 00 00 00 0B 01 00 00 00 0F 00 00 00 64 00 00 00 00 00 00 00 01 00 00 00 02 00 00 00 03 00 00 00 04 00 00 00 05 00 00 00 06 00 00 00 07 00 00 00 08 00 00 00 09 00 00 00 0A 00 00 00 0B 00 00 00 2B 00 00 00 2F 00 00 00 0A 00 00 00 00 02 00 00 00 00 00 00 00 0A 00 00 00 06 00 00 00 00 01 00 00 00 00 00 00 00 07 00 00 00 00 01 00 00 00 00 00 00 00 02 00 00 00 00 00 00 00 01");
            
            Write.Int(MaxLevel);
            Write.Int(lCharacters.Count);
            for (int x = 0; x < lCharacters.Count; x++)
            {
                Write.Int(lCharacters[x].CharId);
                Write.Int(lCharacters[x].MaxPromotion);
                for (byte y = 0; y < lCharacters[x].MaxPromotion; y++)
                {
                    Write.Byte(y);
                }
            }

            Write.HexArray("00 00 00 13 00 00 00 00 00 00 00 01 00 00 00 02 00 00 00 03 00 00 00 04 00 00 00 05 00 00 00 06 00 00 00 07 00 00 00 08 00 00 00 09 00 00 00 0A 00 00 00 0B 00 00 00 0C 00 00 00 0D 00 00 00 0E 00 00 00 0F 00 00 00 10 00 00 00 11 00 00 00 12 00 00 00 13 00 00 00 00 00 00 00 02 00 00 00 01 00 00 00 02 00 00 00 02 00 00 00 02 00 00 00 03 00 00 00 01 00 00 00 04 00 00 00 01 00 00 00 05 00 00 00 01 00 00 00 06 00 00 00 01 00 00 00 07 00 00 00 01 00 00 00 08 00 00 00 01 00 00 00 09 00 00 00 01 00 00 00 0A 00 00 00 00 00 00 00 0B 00 00 00 01 00 00 00 0C 00 00 00 01 00 00 00 0D 00 00 00 01 00 00 00 0E 00 00 00 00 00 00 00 0F 00 00 00 01 00 00 00 10 00 00 00 01 00 00 00 11 00 00 00 00 00 00 00 12 00 00 00 00 00 00 00 12 00 00 00 00 00 01 83 3F 00 00 00 01 00 01 83 40 00 00 00 02 00 01 83 41 00 00 00 03 00 01 83 42 00 00 00 04 00 01 83 43 00 00 00 05 00 01 83 44 00 00 00 06 00 01 83 45 00 00 00 07 00 01 83 46 00 00 00 08 00 01 83 47 00 00 00 09 00 01 83 48 00 00 00 0A 00 01 83 49 00 00 00 0B 00 01 83 4A 00 00 00 0C 00 01 83 4B 00 00 00 0D 00 01 83 4C 00 00 00 0E 00 01 83 4D 00 00 00 0F 00 01 83 4E 00 00 00 10 00 01 83 4F 00 00 00 11 00 01 E3 32");
            Write.HexArray("00 00 00 43 00 00 00 00 00 00 00 01 00 00 00 02 00 00 00 03 00 00 00 04 00 00 00 05 00 00 00 06 00 00 00 07 00 00 00 08 00 00 00 09 00 00 00 0A 00 00 00 0B 00 00 00 0C 00 00 00 0D 00 00 00 0E 00 00 00 0F 00 00 00 10 00 00 00 11 00 00 00 12 00 00 00 13 00 00 00 14 00 00 00 15 00 00 00 16 00 00 00 17 00 00 00 18 00 00 00 19 00 00 00 1A 00 00 00 1B 00 00 00 1C 00 00 00 1D 00 00 00 1E 00 00 00 1F 00 00 00 20 00 00 00 21 00 00 00 22 00 00 00 23 00 00 00 24 00 00 00 26 00 00 00 27 00 00 00 28 00 00 00 29 00 00 00 2A 00 00 00 2B 00 00 00 2C 00 00 00 2D 00 00 00 2E 00 00 00 2F 00 00 00 30 00 00 00 31 00 00 00 32 00 00 00 33 00 00 00 34 00 00 00 39 00 00 00 3A 00 00 00 3B 00 00 00 3C 00 00 00 3D 00 00 00 3E 00 00 00 3F 00 00 00 40 00 00 00 41 00 00 00 42 00 00 00 43 00 00 00 44 00 00 00 45 00 00 00 46 00 00 00 47");
            Write.HexArray("00 00 00 02 00 00 00 00 00 00 00 01 00 00 00 00 00 00 00 17 00 00 01 B9 00 00 01 B9 00 00 00 64 00 00 00 CF 00 00 03 E8 00 00 01 BA 00 00 01 BA 00 00 00 64 00 00 00 CF 00 00 03 E8 00 00 01 BB 00 00 01 BB 00 00 00 64 00 00 00 CF 00 00 03 E8 00 00 01 BC 00 00 01 BC 00 00 00 64 00 00 00 CF 00 00 03 E8 00 00 01 BD 00 00 01 BD 00 00 00 64 00 00 00 CF 00 00 03 E8 00 00 01 BF 00 00 01 BF 00 00 00 64 00 00 00 CF 00 00 03 E8 00 00 01 C0 00 00 01 C0 00 00 00 64 00 00 00 CF 00 00 03 E8 00 00 01 C1 00 00 01 C1 00 00 00 64 00 00 00 CF 00 00 03 E8 00 00 01 C2 00 00 01 C2 00 00 00 64 00 00 00 CF 00 00 03 E8 00 00 02 1A 00 00 02 1A 00 00 00 79 00 00 05 DC 00 00 0D AC 00 00 02 5C 00 00 02 5C 00 00 00 8C 00 00 04 4C 00 00 24 86 00 00 02 5D 00 00 02 5D 00 00 00 8C 00 00 04 4C 00 00 24 86 00 00 02 5E 00 00 02 5E 00 00 00 8C 00 00 04 4C 00 00 24 86 00 00 02 5F 00 00 02 5F 00 00 00 8C 00 00 04 4C 00 00 24 86 00 00 02 60 00 00 02 60 00 00 00 8C 00 00 04 4C 00 00 24 86 00 00 02 61 00 00 02 61 00 00 00 8C 00 00 04 4C 00 00 24 86 00 00 02 62 00 00 02 62 00 00 00 8C 00 00 04 4C 00 00 24 86 00 00 02 63 00 00 02 63 00 00 00 8C 00 00 04 4C 00 00 24 86 00 00 02 65 00 00 02 65 00 00 00 8C 00 00 04 4C 00 00 24 86 00 00 02 67 00 00 02 67 00 00 00 C8 00 00 08 98 00 00 1B 58 00 00 02 68 00 00 02 68 00 00 00 F0 00 00 0C E4 00 00 1D 4C 00 00 02 69 00 00 02 69 00 00 00 B4 00 00 01 F4 00 00 24 86 00 00 02 6A 00 00 02 6A 00 00 00 B4 00 00 01 F4 00 00 24 86");
            Write.HexArray("00 00 00 00 00 00 00 02");
            Write.HexArray("00 00 00 18 00 00 00 1E");
            Write.Int(lEvents.Count);
            for (int x = 0; x < lEvents.Count; x++)
            {
                Write.Int(lEvents[x].EventUID);
                Write.Int(lEvents[x].MBoxID);
                Write.UnicodeStr(lEvents[x].TextureFile);
                Write.Int(1);
                Write.Int(lEvents[x].EventCoin);
            }                                    
            player.SendPacket(Write, 12);
        }
    }
}
