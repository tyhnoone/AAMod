using System.IO;
using System;
using System.Linq;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Microsoft.Xna.Framework;
using Terraria.ModLoader;
using Terraria.World.Generation;
using Terraria.GameContent.Generation;
using Terraria.ModLoader.IO;
using AAMod.Tiles;
using BaseMod;
using AAMod.Worldgeneration;
using AAMod.Worldgen;
using Terraria.Utilities;
using Terraria.Localization;
using AAMod.Walls;

namespace AAMod
{
    public class AAWorld : ModWorld
    {
        public static int SmashDragonEgg = 2;
        public static int SmashHydraPod = 2;
        //tile ints
        public static int mireTiles = 0;
        public static int infernoTiles = 0;
        public static int voidTiles = 0;
        public static int mushTiles = 0;
        public static int terraTiles = 0;
        public static int stormTiles = 0;
        public static int pagodaTiles = 0;
        public static int lakeTiles = 0;
        public static int shipTiles = 0;
        public static int Radium = 0;
        public static int Darkmatter = 0;
        public static int DiscoBall = 0;
        public static int HoardTiles = 0;
        public static int CloudTiles = 0;
        //Worldgen
        public static bool TerrariumEnemies;
        public static bool Luminite;
        public static bool DarkMatter;
        public static bool HallowedOre;
        public static bool Dynaskull;
        public static bool ChaosOres;
        public static bool RadiumOre;
        public static bool AltarSmashed;
        public static int ChaosAltarsSmashed;
        public static int OreCount;
        public static bool DiscordOres;
        public static bool InfernoStripe;
        public static bool MireStripe;
        private int infernoSide = 0;
        public static bool Dissipate = true;
        public static int CloudAlpha = 255;
        //private int shipSide = 0;
        private Vector2 infernoPos = new Vector2(0, 0);
        private Vector2 mirePos = new Vector2(0, 0);
        private Vector2 InfernoCenter = -Vector2.One;
        private Vector2 MireCenter = -Vector2.One;
        public static Vector2 shipPos = new Vector2(0, 0);
        public string nums = "1234567890";
        public static bool ModContentGenerated;
        //Messages
        public static bool Evil;
        public static bool Empowered;
        //Boss Bools
        public static bool Ancients;
        public static bool downedMonarch;
        public static bool downedGrips;
        public static bool downedBrood;
        public static bool downedHydra;
        public static bool downedSerpent;
        public static bool downedDjinn;
        public static bool downedRajah;
        public static bool downedDB;
        public static bool downedNC;
        public static bool downedEquinox;
        public static bool downedAncient;
        public static bool downedSAncient;
        public static bool downedAkuma;
        public static bool downedYamata;
        public static bool zeroUS;
        public static bool downedZero;
        public static bool downedAllAncients;
        public static bool ShenSummoned;
        public static bool downedShen;
        public static bool downedToad;
        public static bool downedFungus;
        public static bool downedAshe;
        public static bool downedHaruka;
        public static bool downedSisters;
        public static bool downedSag;
        public static bool SistersSummoned;
        public static bool downedRajahsRevenge;
        public static bool downedAnubis;
        public static bool downedAthena;
        public static bool downedAthenaA;
        public static bool downedGreed;
        public static bool AthenaHerald;
        //Points
        public static Point WHERESDAVOIDAT;

        public static bool Anticheat = true;

        //Squid Lady
        public static int squid1 = 0;
        public static int squid2 = 0;
        public static int squid3 = 0;
        public static int squid4 = 0;
        public static int squid5 = 0;
        public static int squid6 = 0;
        public static int squid7 = 0;
        public static int squid8 = 0;
        public static int squid9 = 0;
        public static int squid10 = 0;
        public static int squid11 = 0;
        public static int squid12 = 0;
        public static int squid13 = 0;
        public static int squid14 = 0;
        public static int squid15 = 0;
        public static int squid16 = 0;

        //Other
        public static bool Suncaller = false;
        public static bool Mooncaller = false;
        public static int RabbitKills = 0;
        public static bool TimeStopped = false;
        public static double PausedTime = 0;

        public override void Initialize()
        {
            //Bosses
            downedAnubis = false;
            downedAthena = false;
            downedAthenaA = false;
            downedGreed = false;
            downedMonarch = false;
            downedGrips = false;
            downedEquinox = false;
            downedSAncient = false;
            downedAkuma = false;
            downedYamata = false;
            zeroUS = false;
            downedZero = false;
            downedShen = false;
            downedAllAncients = false;
            ShenSummoned = false;
            downedToad = false;
            downedFungus = false;
            downedDjinn = false;
            downedSerpent = false;
            downedBrood = false;
            downedHydra = false;
            downedAshe = false ;
            downedHaruka = false;
            downedSisters = false;
            downedSag = false;
            SistersSummoned = false;
            downedRajah = false;
            AthenaHerald = false;
            //World Changes
            TerrariumEnemies = NPC.downedBoss2;
            ChaosOres = downedGrips;
            Dynaskull = NPC.downedBoss3;
            HallowedOre = NPC.downedMechBossAny;
            Evil = NPC.downedPlantBoss;
            Luminite = NPC.downedMoonlord;
            DarkMatter = downedNC;
            RadiumOre = downedDB;
            DiscordOres = downedSisters;
            InfernoStripe = Main.hardMode;
            MireStripe = Main.hardMode;
            Anticheat = true;
            ModContentGenerated = false;
            Empowered = downedShen;
            mirePos = new Vector2(0, 0);
            infernoPos = new Vector2(0, 0);
            InfernoCenter = -Vector2.One;
            MireCenter = -Vector2.One;
            SmashDragonEgg = 2;
            SmashHydraPod = 2;
            Dissipate = true;
            CloudAlpha = 255;
            //Squid Lady
            squid1 = 0;
            squid2 = 0;
            squid3 = 0;
            squid4 = 0;
            squid5 = 0;
            squid6 = 0;
            squid7 = 0;
            squid8 = 0;
            squid9 = 0;
            squid10 = 0;
            squid11 = 0;
            squid12 = 0;
            squid13 = 0;
            squid14 = 0;
            squid15 = 0;
            squid16 = 0;
        }

        public static int Raycast(int x, int y)
        {
            while (!TileValid(x, y))
                y++;
            return y;
        }

        public static bool TileValid(int i, int j)
        {
            bool valid = false;
            try
            {
                valid = Main.tile[i, j].active() && Main.tileSolid[Main.tile[i, j].type];
            }
            catch (Exception e)
            {
                AAMod.instance.Logger.Error($"{e} \n{i}, {j}");
            }
            return valid;
        }

        public override TagCompound Save()
        {
            var downed = new List<string>();
            if (downedMonarch) downed.Add("MUSHMAN");
            if (downedGrips) downed.Add("GrabbyHands");
            if (downedHydra) downed.Add("Hydra");
            if (downedBrood) downed.Add("Nacho");
            if (NPC.downedBoss3) downed.Add("Dynaskull");
            if (NPC.downedMechBossAny) downed.Add("MechBoss");
            if (NPC.downedPlantBoss) downed.Add("Evil");
            if (NPC.downedMoonlord) downed.Add("MoonLord");
            if (downedEquinox) downed.Add("Equinox");
            if (Ancients) downed.Add("AA");
            if (downedAncient) downed.Add("A");
            if (downedSAncient) downed.Add("SA");
            if (downedAkuma) downed.Add("Akuma");
            if (downedYamata) downed.Add("Yamata");
            if (downedZero) downed.Add("0");
            if (downedShen) downed.Add("Shen");
            if (downedAllAncients) downed.Add("DAA");
            if (ShenSummoned) downed.Add("ShenS");
            if (downedSerpent) downed.Add("Serpent");
            if (downedDjinn) downed.Add("JojoReference");
            if (downedToad) downed.Add("Toad");
            if (downedFungus) downed.Add("Fungus");
            if (InfernoStripe) downed.Add("IStripe");
            if (MireStripe) downed.Add("MStripe");
            if (downedAshe) downed.Add("BetterDragonWaifu");
            if (downedHaruka) downed.Add("TrashDragonWaifu");
            if (downedSisters) downed.Add("Sisters");
            if (downedSag) downed.Add("Sag");
            if (ModContentGenerated) downed.Add("WorldGenned");
            if (SistersSummoned) downed.Add("Summoned");
            if (downedRajah) downed.Add("Rajah");
            if (downedRajahsRevenge) downed.Add("Rajah2");
            if (zeroUS) downed.Add("ZUS");
            if (downedAnubis) downed.Add("Doggo");
            if (downedAthena) downed.Add("BirdBitch");
            if (downedAthenaA) downed.Add("BirdBitchA");
            if (downedGreed) downed.Add("GimmeGimme");
            if (AthenaHerald) downed.Add("BitchBird");

            return new TagCompound {
                {"downed", downed},
				{"MCenter", MireCenter },
				{"ICenter", InfernoCenter },
                {"squid1", squid1},
                {"squid2", squid2},
                {"squid3", squid3},
                {"squid4", squid4},
                {"squid5", squid5},
                {"squid6", squid6},
                {"squid7", squid7},
                {"squid8", squid8},
                {"squid9", squid9},
                {"squid10", squid10},
                {"squid11", squid11},
                {"squid12", squid12},
                {"squid13", squid13},
                {"squid14", squid14},
                {"squid15", squid15},
                {"squid16", squid16},
                {"Bunny", RabbitKills},
                {"Egg", SmashDragonEgg},
                {"Pod", SmashHydraPod}
            };
        }
        public override void NetSend(BinaryWriter writer)
        {
            BitsByte flags = new BitsByte();
            flags[0] = downedMonarch;
            flags[1] = downedAncient;
            flags[2] = downedGrips;
            flags[3] = downedBrood;
            flags[4] = downedHydra;
            flags[5] = ModContentGenerated;
            flags[6] = downedRajah;
            flags[7] = downedRajahsRevenge;
            writer.Write(flags);

            BitsByte flags2 = new BitsByte();
            flags2[0] = zeroUS;
            flags2[1] = downedAshe;
            flags2[2] = downedHaruka;
            flags2[3] = SistersSummoned;
            flags2[4] = downedSisters;
            flags2[5] = downedSag;
            flags2[6] = downedEquinox;
            flags2[7] = downedAkuma;
            writer.Write(flags2);

            BitsByte flags3 = new BitsByte();
            flags3[0] = downedAllAncients;
            flags3[1] = downedYamata;
            flags3[2] = InfernoStripe;
            flags3[3] = MireStripe;
            flags3[4] = downedZero;
            flags3[5] = downedSAncient;
            flags3[6] = downedShen;
            flags3[7] = downedFungus;
            writer.Write(flags3);


            BitsByte flags4 = new BitsByte();
            flags4[0] = Ancients;
            flags4[1] = ShenSummoned;
            flags4[2] = downedSerpent;
            flags4[3] = downedDjinn;
            flags4[4] = downedToad;
            flags4[5] = downedAnubis;
            flags4[6] = downedAthena;
            flags4[7] = downedGreed;
            writer.Write(flags4);


            BitsByte flags5 = new BitsByte();
            flags5[0] = AthenaHerald;
            flags5[1] = downedAthenaA;
            writer.Write(flags5);

            writer.WriteVector2(MireCenter);
            writer.WriteVector2(InfernoCenter);

            writer.Write(squid1);
            writer.Write(squid2);
            writer.Write(squid3);
            writer.Write(squid4);
            writer.Write(squid5);
            writer.Write(squid6);
            writer.Write(squid7);
            writer.Write(squid8);
            writer.Write(squid9);
            writer.Write(squid10);
            writer.Write(squid11);
            writer.Write(squid12);
            writer.Write(squid13);
            writer.Write(squid14);
            writer.Write(squid15);
            writer.Write(squid16);
            writer.Write(RabbitKills);
            writer.Write(SmashDragonEgg);
            writer.Write(SmashHydraPod);
        }

        public override void NetReceive(BinaryReader reader)
        {
            BitsByte flags = reader.ReadByte();
            downedMonarch = flags[0];
            downedAncient = flags[1];
            downedGrips = flags[2];
            downedBrood = flags[3];
            downedHydra = flags[4];
            ModContentGenerated = flags[5];
            downedRajah = flags[6];
            downedRajahsRevenge = flags[7];

            BitsByte flags2 = reader.ReadByte();
            zeroUS = flags2[0];
            downedAshe = flags2[1];
            downedHaruka = flags2[2];
            SistersSummoned = flags2[3];
            downedSisters = flags2[4];
            downedSag = flags2[5];
            downedEquinox = flags2[6];
            downedAkuma = flags2[7];

            BitsByte flags3 = reader.ReadByte();
            downedAllAncients = flags3[0];
            downedYamata = flags3[1];
            InfernoStripe = flags3[2];
            MireStripe = flags3[3];
            downedZero = flags3[4];
            downedSAncient = flags3[5];
            downedShen = flags3[6];
            downedFungus = flags3[7];

            BitsByte flags4 = reader.ReadByte();
            Ancients = flags4[0];
            ShenSummoned = flags4[1];
            downedSerpent = flags4[2];
            downedDjinn = flags4[3];
            downedToad = flags4[4];
            downedAnubis = flags4[5];
            downedAthena = flags4[6];
            downedGreed = flags4[7];

            BitsByte flags5 = reader.ReadByte();
            AthenaHerald = flags5[0];
            downedAthenaA = flags5[1];

            MireCenter = reader.ReadVector2();
			InfernoCenter = reader.ReadVector2();		

            squid1 = reader.ReadInt32();
            squid2 = reader.ReadInt32();
            squid3 = reader.ReadInt32();
            squid4 = reader.ReadInt32();
            squid5 = reader.ReadInt32();
            squid6 = reader.ReadInt32();
            squid7 = reader.ReadInt32();
            squid8 = reader.ReadInt32();
            squid9 = reader.ReadInt32();
            squid10 = reader.ReadInt32();
            squid11 = reader.ReadInt32();
            squid12 = reader.ReadInt32();
            squid13 = reader.ReadInt32();
            squid14 = reader.ReadInt32();
            squid15 = reader.ReadInt32();
            squid16 = reader.ReadInt32();
            RabbitKills = reader.ReadInt32();
            SmashHydraPod = reader.ReadInt32();
            SmashDragonEgg = reader.ReadInt32();
        }

        public override void Load(TagCompound tag)
        {
            var downed = tag.GetList<string>("downed");
            //bosses
            downedMonarch = downed.Contains("MUSHMAN");
            downedGrips = downed.Contains("GrabbyHands");
            downedBrood = downed.Contains("Nacho");
            downedHydra = downed.Contains("Hydra");
            NPC.downedBoss3 = downed.Contains("Dynaskull");
            NPC.downedMechBossAny = downed.Contains("MechBoss");
            NPC.downedPlantBoss = downed.Contains("Evil");
            NPC.downedMoonlord = downed.Contains("MoonLord");
            downedEquinox = downed.Contains("Equinox");
            downedAncient = downed.Contains("A");
            downedSAncient = downed.Contains("SA");
            downedAkuma = downed.Contains("Akuma");
            downedYamata = downed.Contains("Yamata");
            downedZero = downed.Contains("0");
            downedShen = downed.Contains("Shen");
            downedAllAncients = downed.Contains("DAA");
            Ancients = downed.Contains("AA");
            ShenSummoned = downed.Contains("ShenS");
            downedSerpent = downed.Contains("Serpent");
            downedDjinn = downed.Contains("JojoReference");
            downedToad = downed.Contains("Toad");
            downedFungus = downed.Contains("Fungus");
            downedAshe = downed.Contains("BetterDragonWaifu");
            downedHaruka = downed.Contains("TrashDragonWaifu");
            downedSisters = downed.Contains("Sisters");
            downedSag = downed.Contains("Sag");
            SistersSummoned = downed.Contains("Summoned");
            downedRajah = downed.Contains("Rajah");
            downedRajahsRevenge = downed.Contains("Rajah2");
            zeroUS = downed.Contains("ZUS");
            downedAnubis = downed.Contains("Doggo");
            downedAthena = downed.Contains("BirdBitch");
            downedAthenaA = downed.Contains("BirdBitchA");
            downedGreed = downed.Contains("GimmeGimme");
            AthenaHerald = downed.Contains("BitchBird");
            //World Changes
            ChaosOres = downedGrips;
            Dynaskull = NPC.downedBoss3;
            HallowedOre = NPC.downedMechBossAny;
            Evil = NPC.downedPlantBoss;
            Luminite = NPC.downedMoonlord;
            RadiumOre = downedEquinox;
            DiscordOres = downedSisters;
            InfernoStripe = downed.Contains("IStripe");
            MireStripe = downed.Contains("MStripe");
            ModContentGenerated = downed.Contains("WorldGenned");

            if (tag.ContainsKey("MCenter")) // check if the altar coordinates exist in the save file
            {
                MireCenter = tag.Get<Vector2>("MCenter");
            }
            if (tag.ContainsKey("ICenter")) // check if the altar coordinates exist in the save file
            {
                InfernoCenter = tag.Get<Vector2>("ICenter");
            }
            //Squid Lady

            squid1 = tag.GetInt("squid1");
            squid2 = tag.GetInt("squid2");
            squid3 = tag.GetInt("squid3");
            squid4 = tag.GetInt("squid4");
            squid5 = tag.GetInt("squid5");
            squid6 = tag.GetInt("squid6");
            squid7 = tag.GetInt("squid7");
            squid8 = tag.GetInt("squid8");
            squid9 = tag.GetInt("squid9");
            squid10 = tag.GetInt("squid10");
            squid11 = tag.GetInt("squid11");
            squid12 = tag.GetInt("squid12");
            squid13 = tag.GetInt("squid13");
            squid14 = tag.GetInt("squid14");
            squid15 = tag.GetInt("squid15");
            squid16 = tag.GetInt("squid16");
            RabbitKills = tag.GetInt("Bunny");
            SmashDragonEgg = tag.GetInt("Egg");
            SmashHydraPod = tag.GetInt("Pod");
        }

        private string NumberRand(int size)
        {
            char[] chars = new char[size];
            for (int i = 0; i < size; i++)
            {
                chars[i] = nums[Main.rand.Next(nums.Length)];
            }
            return new string(chars);
        }

        public override void ModifyWorldGenTasks(List<GenPass> tasks, ref float totalWeight)
        {
            int shiniesIndex = tasks.FindIndex(genpass => genpass.Name.Equals("Shinies"));
            int ChaosIndex = tasks.FindIndex(genpass => genpass.Name.Equals("Larva"));
            int shiniesIndex1 = tasks.FindIndex(genpass => genpass.Name.Equals("Micro Biomes"));
            int shiniesIndex2 = tasks.FindIndex(genpass => genpass.Name.Equals("Final Cleanup"));


            tasks.Insert(shiniesIndex + 1, new PassLegacy("Prisms", delegate (GenerationProgress progress)
            {
                GenPrisms(progress);
            }));
            tasks.Insert(shiniesIndex + 2, new PassLegacy("Abyssium", delegate (GenerationProgress progress)
            {
                GenAbyssium();
            }));
            tasks.Insert(shiniesIndex + 3, new PassLegacy("Incinerite", delegate (GenerationProgress progress)
            {
                GenIncinerite();
            }));
            tasks.Insert(shiniesIndex + 4, new PassLegacy("Everleaf", delegate (GenerationProgress progress)
            {
                GenEverleaf();
            }));
            tasks.Insert(shiniesIndex + 5, new PassLegacy("Relic", delegate (GenerationProgress progress)
            {
                GenRelicOre();
            }));
            tasks.Insert(ChaosIndex + 5, new PassLegacy("Mire and Inferno", delegate (GenerationProgress progress)
            {
				MireAndInferno(progress);
            }));

            tasks.Insert(shiniesIndex2 + 7, new PassLegacy("Terrarium", delegate (GenerationProgress progress)
            {
                Terrarium(progress);
            }));

            tasks.Insert(shiniesIndex2 + 7, new PassLegacy("Hoard", delegate (GenerationProgress progress)
            {
                Hoard(progress);
            }));

            tasks.Insert(shiniesIndex2 + 7, new PassLegacy("Acropolis", delegate (GenerationProgress progress)
            {
                Acropolis(progress);
            }));

            tasks.Insert(shiniesIndex2 + 5, new PassLegacy("Void Islands", delegate (GenerationProgress progress)
            {
                VoidIslands(progress);
            }));


            tasks.Insert(shiniesIndex2 + 8, new PassLegacy("Altars", delegate (GenerationProgress progress)
            {
                Altars(progress);
            }));

            int DungeonChests = tasks.FindIndex((GenPass genpass) => genpass.Name.Equals("Dungeon"));
            if (DungeonChests >= 0)
            {
                tasks.Insert(DungeonChests + 1, new PassLegacy("InfernoChest", delegate (GenerationProgress progress)
                {
                    bool placed = false;
                    int Minimum = 50;
                    int Maximum = Main.maxTilesX / 2;
                    if (Main.dungeonX > Maximum)
                    {
                        Minimum = Maximum;
                        Maximum = Main.maxTilesX - 50;
                    }
                    while (!placed)
                    {
                        int PlaceHere = WorldGen.genRand.Next(Minimum, Maximum);
                        int PlacementHeight = WorldGen.genRand.Next((int)Main.worldSurface, Main.maxTilesY - 200);
                        if (Main.wallDungeon[Main.tile[PlaceHere, PlacementHeight].wall] && !Main.tile[PlaceHere, PlacementHeight].active())
                        {
                            while (PlacementHeight < Main.maxTilesY - 200)
                            {
                                PlacementHeight++;
                                if (WorldGen.SolidTile(PlaceHere, PlacementHeight))
                                {
                                    int PlacementSuccess = WorldGen.PlaceChest(PlaceHere, PlacementHeight - 1, (ushort)mod.TileType("InfernoChest"), false, 1);
                                    if (PlacementSuccess >= 0)
                                    {
                                        Chest chest = Main.chest[PlacementSuccess];
                                        chest.item[0].SetDefaults(mod.ItemType("DragonriderStaff"), false);
                                        chest.item[1].SetDefaults(Utils.Next(WorldGen.genRand, new int[]
                                        { mod.ItemType("RadiantIncinerite") }), false);
                                        chest.item[1].stack = WorldGen.genRand.Next(11, 20);
                                        Item item = chest.item[2];
                                        UnifiedRandom genRand = WorldGen.genRand;
                                        int[] array = new int[]
                                        { mod.ItemType("DragonfireFlask") };
                                        item.SetDefaults(Utils.Next(genRand, array), false);
                                        chest.item[2].stack = WorldGen.genRand.Next(1, 4);
                                        Item item2 = chest.item[3];
                                        UnifiedRandom genRand2 = WorldGen.genRand;
                                        int[] array2 = new int[]
                                        { 302, 2327, 2351, 304, 2329 };
                                        item2.SetDefaults(Utils.Next(genRand2, array2), false);
                                        chest.item[3].stack = WorldGen.genRand.Next(1, 3);
                                        chest.item[4].SetDefaults(Utils.Next(WorldGen.genRand, new int[]
                                        { 282, 286 }), false);
                                        chest.item[4].stack = WorldGen.genRand.Next(15, 31);
                                        chest.item[5].SetDefaults(73, false);
                                        chest.item[5].stack = WorldGen.genRand.Next(1, 3);
                                        placed = true ;
                                        break;
                                    }
                                    break;
                                }
                            }
                        }
                    }
                }));

                tasks.Insert(DungeonChests + 2, new PassLegacy("MireChest", delegate (GenerationProgress progress)
                {
                    bool placed = false;
                    int Minimum = 50;
                    int Maximum = Main.maxTilesX / 2;
                    if (Main.dungeonX > Maximum)
                    {
                        Minimum = Maximum;
                        Maximum = Main.maxTilesX - 50;
                    }
                    while (!placed)
                    {
                        int PlaceHere = WorldGen.genRand.Next(Minimum, Maximum);
                        int PlacementHeight = WorldGen.genRand.Next((int)Main.worldSurface, Main.maxTilesY - 200);
                        if (Main.wallDungeon[Main.tile[PlaceHere, PlacementHeight].wall] && !Main.tile[PlaceHere, PlacementHeight].active())
                        {
                            while (PlacementHeight < Main.maxTilesY - 200)
                            {
                                PlacementHeight++;
                                if (WorldGen.SolidTile(PlaceHere, PlacementHeight))
                                {
                                    int PlacementSuccess = WorldGen.PlaceChest(PlaceHere, PlacementHeight - 1, (ushort)mod.TileType("MireChest"), false, 1);
                                    if (PlacementSuccess >= 0)
                                    {
                                        Chest chest = Main.chest[PlacementSuccess];
                                        chest.item[0].SetDefaults(mod.ItemType("BogBomb"), false);
                                        chest.item[1].SetDefaults(Utils.Next(WorldGen.genRand, new int[]
                                        { mod.ItemType("DeepAbyssium") }), false);
                                        chest.item[1].stack = WorldGen.genRand.Next(11, 20);
                                        Item item = chest.item[2];
                                        UnifiedRandom genRand = WorldGen.genRand;
                                        int[] array = new int[]
                                        { mod.ItemType("HydratoxinFlask") };
                                        item.SetDefaults(Utils.Next(genRand, array), false);
                                        chest.item[2].stack = WorldGen.genRand.Next(1, 4);
                                        Item item2 = chest.item[3];
                                        UnifiedRandom genRand2 = WorldGen.genRand;
                                        int[] array2 = new int[]
                                        { 302, 2327, 2351, 304, 2329 };
                                        item2.SetDefaults(Utils.Next(genRand2, array2), false);
                                        chest.item[3].stack = WorldGen.genRand.Next(1, 3);
                                        chest.item[4].SetDefaults(Utils.Next(WorldGen.genRand, new int[]
                                        { 282, 286 }), false);
                                        chest.item[4].stack = WorldGen.genRand.Next(15, 31);
                                        chest.item[5].SetDefaults(73, false);
                                        chest.item[5].stack = WorldGen.genRand.Next(1, 3);
                                        placed = true;
                                        break;
                                    }
                                    break;
                                }
                            }
                        }
                    }
                }));


                tasks.Insert(DungeonChests + 3, new PassLegacy("VoidChest", delegate (GenerationProgress progress)
                {
                    bool placed = false;
                    int Minimum = 50;
                    int Maximum = Main.maxTilesX / 2;
                    if (Main.dungeonX > Maximum)
                    {
                        Minimum = Maximum;
                        Maximum = Main.maxTilesX - 50;
                    }
                    while (!placed)
                    {
                        int PlaceHere = WorldGen.genRand.Next(Minimum, Maximum);
                        int PlacementHeight = WorldGen.genRand.Next((int)Main.worldSurface, Main.maxTilesY - 200);
                        if (Main.wallDungeon[Main.tile[PlaceHere, PlacementHeight].wall] && !Main.tile[PlaceHere, PlacementHeight].active())
                        {
                            while (PlacementHeight < Main.maxTilesY - 200)
                            {
                                PlacementHeight++;
                                if (WorldGen.SolidTile(PlaceHere, PlacementHeight))
                                {
                                    int PlacementSuccess = WorldGen.PlaceChest(PlaceHere, PlacementHeight - 1, (ushort)mod.TileType("DoomsdayChest"), false, 1);
                                    if (PlacementSuccess >= 0)
                                    {
                                        Chest chest = Main.chest[PlacementSuccess];
                                        chest.item[0].SetDefaults(mod.ItemType("SingularityCannon"), false);
                                        chest.item[1].SetDefaults(Utils.Next(WorldGen.genRand, new int[]
                                        { mod.ItemType("VoidEnergy") }), false);
                                        chest.item[1].stack = WorldGen.genRand.Next(11, 20);
                                        Item item = chest.item[2];
                                        UnifiedRandom genRand = WorldGen.genRand;
                                        int[] array = new int[]
                                        { mod.ItemType("Doomite") };
                                        item.SetDefaults(Utils.Next(genRand, array), false);
                                        chest.item[2].stack = WorldGen.genRand.Next(1, 4);
                                        Item item2 = chest.item[3];
                                        UnifiedRandom genRand2 = WorldGen.genRand;
                                        int[] array2 = new int[]
                                        { 302, 2327, 2351, 304, 2329 };
                                        item2.SetDefaults(Utils.Next(genRand2, array2), false);
                                        chest.item[3].stack = WorldGen.genRand.Next(1, 3);
                                        chest.item[4].SetDefaults(Utils.Next(WorldGen.genRand, new int[]
                                        { 282, 286 }), false);
                                        chest.item[4].stack = WorldGen.genRand.Next(15, 31);
                                        chest.item[5].SetDefaults(73, false);
                                        chest.item[5].stack = WorldGen.genRand.Next(1, 3);
                                        placed = true;
                                        break;
                                    }
                                    break;
                                }
                            }
                        }
                    }
                }));
            }
            
            ModContentGenerated = true;
        }

        private void GenIncinerite()
        {
            int x = Main.maxTilesX;
            int y = Main.maxTilesY;
            for (int k = 0; k < (int)(x * y * 15E-05); k++)
            {
                int tilesX = WorldGen.genRand.Next(0, Main.maxTilesX);
                int tilesY = WorldGen.genRand.Next((int)WorldGen.rockLayerLow, Main.maxTilesY);
                if (Main.tile[tilesX, tilesY].type == 1)
                {
                    WorldGen.OreRunner(tilesX, tilesY, WorldGen.genRand.Next(2, 4), WorldGen.genRand.Next(3, 6), (ushort)mod.TileType("IncineriteOre"));
                }
            }
        }

        private void GenEverleaf()
        {
            int x = Main.maxTilesX;
            int y = Main.maxTilesY;
            for (int k = 0; k < (int)(x * y * 15E-05); k++)
            {
                int tilesX = WorldGen.genRand.Next(0, x);
                int tilesY = WorldGen.genRand.Next(0, y);
                if (Main.tile[tilesX, tilesY].type == 59)
                {
                    WorldGen.OreRunner(tilesX, tilesY, WorldGen.genRand.Next(3, 8), WorldGen.genRand.Next(3, 8), (ushort)mod.TileType("EverleafRoot"));
                }
            }
        }

        private void GenAbyssium()
        {
            int x = Main.maxTilesX;
            int y = Main.maxTilesY;
            for (int k = 0; k < (int)(x * y * 15E-05); k++)
            {
                int tilesX = WorldGen.genRand.Next(0, Main.maxTilesX);
                int tilesY = WorldGen.genRand.Next((int)WorldGen.rockLayerLow, Main.maxTilesY);
                if (Main.tile[tilesX, tilesY].type == 59)
                {
                    WorldGen.OreRunner(tilesX, tilesY, WorldGen.genRand.Next(2, 4), WorldGen.genRand.Next(3, 6), (ushort)mod.TileType("AbyssiumOre"));
                }
            }
        }

        private void GenRelicOre()
        {
            int x = Main.maxTilesX;
            int y = Main.maxTilesY;
            for (int k = 0; k < (int)(x * y * 15E-05); k++)
            {
                int tilesX = WorldGen.genRand.Next(0, Main.maxTilesX);
                int tilesY = WorldGen.genRand.Next(0, Main.maxTilesY);
                if (Main.tile[tilesX, tilesY].type == TileID.IceBlock)
                {
                    WorldGen.OreRunner(tilesX, tilesY, WorldGen.genRand.Next(2, 4), WorldGen.genRand.Next(3, 6), (ushort)mod.TileType("RelicOre"));
                }
            }
        }

        private void GenPrisms(GenerationProgress progress)
        {
            progress.Message = Language.GetTextValue("LegacyWorldGen.23");
            int amount = (int)(Main.maxTilesX * 0.4f * 0.2f);
            for (int k = 0; k < amount; k++)
            {
                int x = WorldGen.genRand.Next(0, Main.maxTilesX);
                int y = WorldGen.genRand.Next((int)Main.worldSurface, Main.maxTilesY);
                while (Main.tile[x, y].type != 1)
                {
                    x = WorldGen.genRand.Next(0, Main.maxTilesX);
                    y = WorldGen.genRand.Next((int)Main.worldSurface, Main.maxTilesY);
                }
                WorldGen.TileRunner(x, y, WorldGen.genRand.Next(2, 6), WorldGen.genRand.Next(3, 7), mod.TileType<PrismOre>());
            }
        }

        public void VoidIslands(GenerationProgress progress)
        {
            progress.Message = "0" + NumberRand(1) + "0" + NumberRand(1) + "0" + NumberRand(1) + "0" + NumberRand(1) + "0" + NumberRand(1) + "0" + NumberRand(1) + "0" + NumberRand(1) + "0" + NumberRand(1) + "0" + NumberRand(1) + "0";

            progress.Set(0f);
            int VoidHeight = 0;
            progress.Set(0.1f);
            VoidHeight = 120;
            progress.Set(0.4f);
            Point center = new Point((Main.maxTilesX / 15 * 14) + (Main.maxTilesX / 15 / 2) - 100, center.Y = VoidHeight);
            WHERESDAVOIDAT = center;
            progress.Set(0.5f);
            Point oldposition = new Point(1, 1);
            progress.Set(0.6f);
            List<Point> posIslands = new List<Point>();
            progress.Set(0.7f);
            int IslandNumber = 2;
            if (GetWorldSize() != 1)
            {
                IslandNumber = 4;
            }

            for (int i = 0; i < IslandNumber; i++)
            {
                Point position = new Point(
                    center.X + (WorldGen.genRand.Next(35, 55) * (WorldGen.genRand.NextBool() ? -1 : 1)),
                    center.Y + (WorldGen.genRand.Next(35, 55) * (WorldGen.genRand.NextBool() ? -1 : 1)));

                while (posIslands.Any(x => Vector2.Distance(x.ToVector2(), position.ToVector2()) < 35))
                {
                    for (int k = 0; k < posIslands.Count; ++k)
                    {
                        while ((int)Vector2.Distance(posIslands[k].ToVector2(), position.ToVector2()) < 35)
                        {
                            position = new Point(center.X + (WorldGen.genRand.Next(35, 45) * (WorldGen.genRand.NextBool() ? -1 : 1)),
                              center.Y + (WorldGen.genRand.Next(35, 45) * (WorldGen.genRand.NextBool() ? -1 : 1)));
                        }
                    }
                }
                MiniIsland(position, 60);
                posIslands.Add(position);
                oldposition = position;
                for (int k = 0; k < posIslands.Count; ++k)
                {
                    for (int FuckWorldGen = 0; FuckWorldGen < 6; ++FuckWorldGen)
                    {
                        Point randompoint = new Point(
                            posIslands[k].X + WorldGen.genRand.Next(-30, 31),
                            posIslands[k].Y + WorldGen.genRand.Next(7, 42));
                        WorldGen.TileRunner(randompoint.X, randompoint.Y, WorldGen.genRand.Next(5, 8), WorldGen.genRand.Next(6, 13), mod.TileType("Apocalyptite"), false, 0f, 0f, false, true);
                    }
                }
            }
            progress.Set(0.85f);
            for (int j = 0; j < posIslands.Count; ++j)
            {
                Point position = posIslands[j];
                position.X -= 4;
                position.Y -= 11;
                VoidHouses(position.X, position.Y, (ushort)mod.TileType("DoomitePlate"), 10, 7);
            }
            progress.Set(1f);
        }

        public int BlockLining(double x, double y, int repeats, int tileType, bool random, int max, int min = 3)
        {
            for (double i = x; i < x + repeats; i++)
            {
                if (random)
                {
                    for (double k = y; k < y + Main.rand.Next(min, max); k++)
                    {
                        WorldGen.PlaceTile((int)i, (int)k, tileType);
                    }
                }
                else
                {
                    for (double k = y; k < y + max; k++)
                    {
                        WorldGen.PlaceTile((int)i, (int)k, tileType);
                    }
                }
            }
            return repeats;
        }

        private void MiniIsland(Point position, int size)
        {
            for (int i = -size / 2; i < size / 2; ++i)
            {
                int repY = (size / 2) - Math.Abs(i);
                int offset = repY / 5;
                repY += WorldGen.genRand.Next(4);
                for (int j = -offset; j < repY; ++j)
                {
                    WorldGen.PlaceTile(position.X + i, position.Y + j, mod.TileType<Doomstone>());
                }
                int y = Raycast(position.X + i, position.Y - 5);
                WorldGen.PlaceObject(position.X + i, y, mod.TileType("OroborosTree"));
                WorldGen.GrowTree(position.X + i, y);
            }
        }

        private void Altars(GenerationProgress progress)
        {
            progress.Message = "Placing Chaos Altars";
            for (int num = 0; num < Main.maxTilesX / 390; num++)
            {
                int xAxis = WorldGen.genRand.Next(200, Main.maxTilesX - 200);
                int yAxis = WorldGen.genRand.Next((int)WorldGen.rockLayer + 150, Main.maxTilesY - 250);
                for (int AltarX = xAxis - 45; AltarX < xAxis + 45; AltarX++)
                {
                    for (int AltarY = yAxis - 45; AltarY < yAxis + 45; AltarY++)
                    {
                        Tile tile = Main.tile[AltarX, AltarY];
                        int Altar = Main.rand.Next(2);

                        switch (Altar)
                        {
                            case 0:
                                Altar = mod.TileType<ChaosAltar1>();
                                break;
                            default:
                                Altar = mod.TileType<ChaosAltar2>();
                                break;
                        }
                        if (Main.rand.Next(15) == 0)
                        {
                            if ((tile.type == mod.TileType<Torchstone>() ||
                                tile.type == mod.TileType<Torchsand>() ||
                                tile.type == mod.TileType<Torchice>() ||
                                tile.type == mod.TileType<Torchsandstone>() ||
                                tile.type == mod.TileType<Torchsand>() ||
                                tile.type == mod.TileType<InfernoGrass>())  
                                && Altar == mod.TileType<ChaosAltar1>())
                            {
                                Altar = mod.TileType<ChaosAltar2>();
                            }
                            if ((tile.type == mod.TileType<Depthstone>() || 
                                tile.type == mod.TileType<Depthsand>() || 
                                tile.type == mod.TileType<IndigoIce>() ||
                                tile.type == mod.TileType<Depthsandstone>() ||
                                tile.type == mod.TileType<Depthsand>() ||
                                tile.type == mod.TileType<MireGrass>()) 
                                && Altar == mod.TileType<ChaosAltar2>())
                            {
                                Altar = mod.TileType<ChaosAltar1>();
                            }
                            WorldGen.PlaceObject(AltarX, AltarY - 1, Altar);
                        }
                    }
                }
            }
        }
        
        public int ChestNumber = 0;

        public void VoidHouses(int X, int Y, int type = 30, int sizeX = 10, int sizeY = 7)
        {
            int wallID = (ushort)mod.WallType("DoomiteWall");
            //Clear area
            for (int i = X; i < X + sizeX - 1; ++i)
            {
                for (int j = Y - 1; j < Y + sizeY; ++j)
                {
                    WorldGen.KillTile(i, j);
                }
            }
            //Wall Placement
            for (int i = X + 1; i < X + sizeX - 2; ++i)
            {
                for (int j = Y + 1; j < Y + sizeY - 1; ++j)
                {
                    if (WorldGen.genRand.Next(5) >= 1)
                    {
                        WorldGen.KillWall(i, j);
                        WorldGen.PlaceWall(i, j, wallID);
                    }
                }
            };
            //Side placements
            for (int i = Y; i < Y + sizeY - 1; ++i)
            {
                WorldGen.PlaceTile(X, i, type);
                WorldGen.PlaceTile(X + (sizeX - 2), i, (ushort)mod.TileType("DoomitePlate"));
            }
            //Roof-floor placements
            for (int i = X; i < X + sizeX - 2; ++i)
            {
                WorldGen.PlaceTile(i, Y, type);
                WorldGen.PlaceTile(i, Y + (sizeY - 1), (ushort)mod.TileType("DoomitePlate"));
            }
            WorldGen.PlaceTile(X + sizeX - 2, Y + sizeY - 1, (ushort)mod.TileType("DoomitePlate"));

            int PlacementSuccess = WorldGen.PlaceChest(X + ((sizeX - 1) / 2), Y + sizeY - 2, (ushort)mod.TileType("OroborosChest"), true);
            if (PlacementSuccess >= 0)
            {
                Chest chest = Main.chest[PlacementSuccess];
                if (ChestNumber == 0)
                {
                    VoidLoot(mod.ItemType("Voidsaber"), chest);
                }
                else if (ChestNumber == 1)
                {
                    VoidLoot(mod.ItemType("DoomGun"), chest);
                }
                else if (ChestNumber == 2)
                {
                    VoidLoot(mod.ItemType("DoomStaff"), chest);

                }
                else if (ChestNumber == 3)
                {
                    VoidLoot(mod.ItemType("ProbeControlUnit"), chest);
                }
                ChestNumber += 1;
            }
            //Side holes
            for (int i = Y + sizeY - 4; i > Y + sizeY; --i)
            {
                WorldGen.KillTile(X, i);
            }
        }

        public void VoidLoot(int Item, Chest chest)
        {
            chest.item[0].SetDefaults(Item, false);
            chest.item[1].SetDefaults(mod.ItemType("DoomiteScrap"), false);
            chest.item[1].stack = WorldGen.genRand.Next(4, 6);
            Item item = chest.item[2];
            UnifiedRandom genRand = WorldGen.genRand;
            int[] array2 = new int[]
            { 302, 2327, 2351, 304, 2329 };
            item.SetDefaults(Utils.Next(genRand, array2), false);
            chest.item[2].stack = WorldGen.genRand.Next(1, 3);
            chest.item[3].SetDefaults(Utils.Next(WorldGen.genRand, new int[]
            { 282, 286 }), false);
            chest.item[3].stack = WorldGen.genRand.Next(15, 31);
            chest.item[4].SetDefaults(73, false);
            chest.item[4].stack = WorldGen.genRand.Next(1, 3);
        }

        public override void PostWorldGen()
        {
            int[] itemsToPlaceInDungeonChests = new int[] { mod.ItemType("SkullStaff") };
            int itemsToPlaceInDungeonChestsChoice = 0;
            for (int chestIndex = 0; chestIndex < 1000; chestIndex++)
            {
                Chest chest = Main.chest[chestIndex];
                if (chest != null && Main.tile[chest.x, chest.y].type == TileID.Containers && Main.tile[chest.x, chest.y].frameX == 2 * 36)
                {
                    if (Main.rand.Next(3) == 0)
                    {
                        for (int inventoryIndex = 0; inventoryIndex < 40; inventoryIndex++)
                        {
                            if (chest.item[inventoryIndex].type == 0)
                            {
                                chest.item[inventoryIndex].SetDefaults(itemsToPlaceInDungeonChests[itemsToPlaceInDungeonChestsChoice]);
                                itemsToPlaceInDungeonChestsChoice = (itemsToPlaceInDungeonChestsChoice + 1) % itemsToPlaceInDungeonChests.Length;
                                break;
                            }
                        }
                    }
                }
            }
        }

        public static Point CloudPoint = new Point((int)(Main.maxTilesX * 0.65f), 100);
        public Vector2 Origin = new Vector2((int)(Main.maxTilesX * 0.65f), 100) * 16;

        public override void PostUpdate()
        {
            if (NPC.AnyNPCs(mod.NPCType<NPCs.Bosses.Athena.Athena>()))
            {
                Dissipate = false;
                CloudAlpha -= 4;
                if (CloudAlpha <= 0)
                {
                    CloudAlpha = 0;
                }
            }
            else
            {
                if (!Dissipate)
                {
                    CloudAlpha += 5;

                    if (CloudAlpha >= 255)
                    {
                        Dissipate = true;
                        CloudAlpha = 255;
                        CloudKill Clouds = new CloudKill();
                        Clouds.Place(CloudPoint, WorldGen.structures);
                    }
                }
            }

            if (TimeStopped)
            {
                Main.fastForwardTime = false;
                Main.time = PausedTime;
            }
            if (downedEquinox)
            {
                if (RadiumOre == false)
                {
                    RadiumOre = true;
                    if (Main.netMode != 1) BaseUtility.Chat("The gift of the celestials sparkle in the atmosphere...", Color.Violet);
                    for (int i = 0; i < Main.maxTilesX / 25; ++i)
                    {
                        int X = WorldGen.genRand.Next(50, Main.maxTilesX / 10 * 9); //X position, centre.
                        int Y = WorldGen.genRand.Next(10, 100); //Y position, centre.
                        int radius = WorldGen.genRand.Next(2, 5); //Radius.
                        for (int x = X - radius; x <= X + radius; x++)
                        {
                            for (int y = Y - radius; y <= Y + radius; y++)
                            {
                                if (Vector2.Distance(new Vector2(X, Y), new Vector2(x, y)) <= radius) //Checks if coords are within a circle position
                                {
                                    WorldGen.PlaceTile(x, y, mod.TileType<RadiumOre>(), true); //Places tile of type InsertTypeHere at the specified coords
                                }
                            }
                        }
                    }
                }
            }

            if (NPC.downedMoonlord)
            {
                if (Ancients == false)
                {
                    Ancients = true;
                    if (Main.netMode != 1) BaseUtility.Chat("The Ancients have Awakened!", Color.ForestGreen);
                }
                if (Luminite == false)
                {
                    Luminite = true;
                    if (Main.netMode != 1) BaseUtility.Chat("The Essence of the Moon Lord sparkles in the caves below...", Color.DarkSeaGreen);
                    for (int k = 0; k < (int)(Main.maxTilesX * Main.maxTilesY * 6E-05); k++)
                    {
                        WorldGen.OreRunner(WorldGen.genRand.Next(0, Main.maxTilesX), WorldGen.genRand.Next((int)Main.rockLayer, Main.maxTilesY - 200), WorldGen.genRand.Next(5, 9), WorldGen.genRand.Next(6, 10), (ushort)mod.TileType("LuminiteOre"));
                    }
                    return;
                }
            }
            if (NPC.downedMechBossAny)
            {
                if (HallowedOre == false)
                {
                    HallowedOre = true;
                    if (Main.netMode != 1) BaseUtility.Chat("The caves shine with light for a brief moment...", Color.Goldenrod);
                    int x = Main.maxTilesX;
                    int y = Main.maxTilesY;
                    for (int k = 0; k < (int)(x * y * 15E-05); k++)
                    {
                        int tilesX = WorldGen.genRand.Next(0, x);
                        int tilesY = WorldGen.genRand.Next((int)(y * .3f), (int)(y * .75f));
                        WorldGen.OreRunner(tilesX, tilesY, WorldGen.genRand.Next(3, 8), WorldGen.genRand.Next(4, 9), (ushort)mod.TileType<HallowedOre>());
                    }
                }
            }

            if (downedSisters)
            {
                if (!DiscordOres)
                {
                    DiscordOres = true;
                    if (Main.netMode != 1) BaseUtility.Chat("Chaotic energy grows in the deepest parts of the world.", Color.Magenta);
                    int x = Main.maxTilesX;
                    int y = Main.maxTilesY;
                    for (int k = 0; k < (int)(x * y * 15E-05); k++)
                    {
                        int tilesX = WorldGen.genRand.Next(0, x);
                        int tilesY = WorldGen.genRand.Next((int)(y * .3f), (int)(y * .75f));
                        if (Main.tile[tilesX, tilesY].type == 59)
                        {
                            WorldGen.OreRunner(tilesX, tilesY, WorldGen.genRand.Next(5, 6), WorldGen.genRand.Next(10, 11), (ushort)mod.TileType("EventideAbyssiumOre"));
                        }
                    }
                    for (int k = 0; k < (int)(x * y * 15E-05); k++)
                    {
                        int tilesX = WorldGen.genRand.Next(0, x);
                        int tilesY = WorldGen.genRand.Next((int)(y * .3f), (int)(y * .75f));
                        if (Main.tile[tilesX, tilesY].type == 1)
                        {
                            WorldGen.OreRunner(tilesX, tilesY, WorldGen.genRand.Next(5, 6), WorldGen.genRand.Next(10, 11), (ushort)mod.TileType("DaybreakIncineriteOre"));
                        }
                    }
                }
            }
            if (NPC.downedBoss2)
            {
                if (!TerrariumEnemies)
                {
                    TerrariumEnemies = true;
                    if (Main.netMode != 1) BaseUtility.Chat("You hear a hum of harmony from the Terrarium after the defeat of a great evil...", Color.LimeGreen);
                }
            }
            if (NPC.downedBoss3)
            {
                if (!Dynaskull)
                {
                    Dynaskull = true;
                    if (Main.netMode != 1) BaseUtility.Chat("Bones of the ancient past burst with energy!", Color.DarkOrange.R, Color.DarkOrange.G, Color.DarkOrange.B);
                    if (Main.netMode != 1) BaseUtility.Chat("The desert winds stir...", Color.Orange);
                    if (Main.netMode != 1) BaseUtility.Chat("The winter hills rumble...", Color.Cyan.R, Color.Cyan.G, Color.Cyan.B);
                    int x = Main.maxTilesX;
                    int y = Main.maxTilesY;
                    for (int k = 0; k < (int)(x * y * 15E-05); k++)
                    {
                        int tilesX = WorldGen.genRand.Next(0, x);
                        int tilesY = WorldGen.genRand.Next(0, y);
                        if (Main.tile[tilesX, tilesY].type == 397)
                        {
                            WorldGen.OreRunner(tilesX, tilesY, WorldGen.genRand.Next(5, 6), WorldGen.genRand.Next(10, 11), (ushort)mod.TileType("DynaskullOre"));
                        }
                    }
                }
            }
            if (NPC.downedPlantBoss)
            {
                if (!Evil)
                {
                    Evil = true;
                    if (Main.netMode != 1) BaseUtility.Chat("Hey kid, it's me, Anubis. Do me a favor and meet me back in town, I wanna talk to ya about somethin'.", Color.Gold.R, Color.Gold.G, Color.Gold.B);
                }
            }
            
            if (downedAkuma || downedYamata || downedZero)
            {
                downedAncient = true;
            }

            if (downedShen)
            {
                downedSAncient = true;
            }

            if (downedAkuma && downedYamata && downedZero)
            {
                if (downedAllAncients == false)
                {
                    if (Main.netMode != 1) BaseUtility.Chat("Chaos begins to stir in the atmosphere...", Color.DarkMagenta.R, Color.DarkMagenta.G, Color.DarkMagenta.B);
                    downedAllAncients = true;
                }
            }
            if (Main.hardMode)
            {
                if (InfernoStripe == false)
                {
                    InfernoStripe = true;

                    if (Main.netMode != 1) BaseUtility.Chat("The Souls of Fury and Wrath are unleashed upon the world!", Color.Magenta.R, Color.Magenta.G, Color.Magenta.B);
                    ConversionHandler.ConvertDown((int)InfernoCenter.X, 0, 120, ConversionType.INFERNO);
                }
                if (MireStripe == false)
                {
                    MireStripe = true;

                    ConversionHandler.ConvertDown((int)MireCenter.X, 0, 120, ConversionType.MIRE);
                }
            }
        }

        public override void TileCountsAvailable(int[] tileCounts)
        {
            Main.sandTiles += tileCounts[mod.TileType<Torchsand>()] + tileCounts[mod.TileType<Torchsandstone>()] + tileCounts[mod.TileType<TorchsandHardened>()] + tileCounts[mod.TileType<Depthsand>()] + tileCounts[mod.TileType<Depthsandstone>()] + tileCounts[mod.TileType<DepthsandHardened>()];
            Main.snowTiles += tileCounts[mod.TileType<Torchice>()] + tileCounts[mod.TileType<IndigoIce>()] + tileCounts[mod.TileType<TorchAsh>()];
            mireTiles = tileCounts[mod.TileType<MireGrass>()]+ tileCounts[mod.TileType<Depthstone>()] + tileCounts[mod.TileType<Depthsand>()] + tileCounts[mod.TileType<Depthsandstone>()] + tileCounts[mod.TileType<DepthsandHardened>()] + tileCounts[mod.TileType<IndigoIce>()];
            infernoTiles = tileCounts[mod.TileType<InfernoGrass>()]+ tileCounts[mod.TileType<Torchstone>()] + tileCounts[mod.TileType<Torchsand>()] + tileCounts[mod.TileType<Torchsandstone>()] + tileCounts[mod.TileType<TorchsandHardened>()] + tileCounts[mod.TileType<Torchice>()] + tileCounts[mod.TileType<TorchAsh>()];
            voidTiles = tileCounts[mod.TileType<Doomstone>()] + tileCounts[mod.TileType<Apocalyptite>()];
            mushTiles = tileCounts[mod.TileType<Mycelium>() ];
            Main.jungleTiles += mireTiles;
            pagodaTiles = tileCounts[mod.TileType<ScorchedDynastyWoodS>()] + tileCounts[mod.TileType<ScorchedShinglesS>()];
            lakeTiles = tileCounts[mod.TileType<Darkmud>()] + tileCounts[mod.TileType<AbyssGrass>()] + tileCounts[mod.TileType<AbyssWood>()] + tileCounts[mod.TileType<AbyssWoodSolid>()];
            terraTiles = tileCounts[mod.TileType<TerraCrystal>()] + tileCounts[mod.TileType<TerraWood>()] + tileCounts[mod.TileType<TerraLeaves>()];
            Radium = tileCounts[mod.TileType<RadiumOre>()];
            HoardTiles = tileCounts[mod.TileType<GreedBrick>()] + tileCounts[mod.TileType<GreedStone>()] + tileCounts[mod.TileType<GreedDoorClosed>()] + tileCounts[mod.TileType<GreedDoorOpen>()];
            CloudTiles = tileCounts[mod.TileType<AcropolisBlock>()] + tileCounts[mod.TileType<AcropolisAltarBlock>()] + tileCounts[mod.TileType<AcropolisClouds>()];
        }

        private void MireAndInferno(GenerationProgress progress)
        {
            infernoSide = (Main.dungeonX > Main.maxTilesX / 2) ? (-1) : 1;
            infernoPos.X = (Main.maxTilesX >= 8000) ? (infernoSide == 1 ? WorldGen.genRand.Next(2000, 2300) : (Main.maxTilesX - WorldGen.genRand.Next(2000, 2300))) : (infernoSide == 1 ? WorldGen.genRand.Next(1500, 1700) : (Main.maxTilesX - WorldGen.genRand.Next(1500, 1700)));
            mirePos.X = (Main.maxTilesX >= 8000) ? (infernoSide != 1 ? WorldGen.genRand.Next(2000, 2300) : (Main.maxTilesX - WorldGen.genRand.Next(2000, 2300))) : (infernoSide != 1 ? WorldGen.genRand.Next(1500, 1700) : (Main.maxTilesX - WorldGen.genRand.Next(1500, 1700)));
            int j = (int)WorldGen.worldSurfaceLow - 30;
            while (Main.tile[(int)infernoPos.X, j] != null && !Main.tile[(int)infernoPos.X, j].active())
            {
                j++;
            }
            for (int l = (int)infernoPos.X - 25; l < (int)infernoPos.X + 25; l++)
            {
                for (int m = j - 6; m < j + 90; m++)
                {
                    if (Main.tile[l, m] != null && Main.tile[l, m].active())
                    {
                        int type = Main.tile[l, m].type;
                        if (type == TileID.Cloud || type == TileID.RainCloud || type == TileID.Sunplate)
                        {
                            j++;
                            if (!Main.tile[l, m].active())
                            {
                                j++;
                            }
                        }
                    }
                }
            }
            infernoPos.Y = j;
            int q = (int)WorldGen.worldSurfaceLow - 30;
            while (Main.tile[(int)mirePos.X, q] != null && !Main.tile[(int)mirePos.X, q].active())
            {
                q++;
            }
            for (int l = (int)mirePos.X - 25; l < (int)mirePos.X + 25; l++)
            {
                for (int m = q - 6; m < q + 90; m++)
                {
                    if (Main.tile[l, m] != null && Main.tile[l, m].active())
                    {
                        int type = Main.tile[l, m].type;
                        if (type == TileID.Cloud || type == TileID.RainCloud || type == TileID.Sunplate)
                        {
                            q++;
                        }
                    }
                }
            }
            mirePos.Y = q;

            InfernoCenter = infernoPos;

            MireCenter = mirePos;

            progress.Message = "Spreading Chaos";
            progress.Message = "Scorching the Inferno";
            InfernoVolcano();
            progress.Message = "Flooding the Mire";
            MireAbyss();
        }

        private void Terrarium(GenerationProgress progress)
        {
            progress.Message = "Constructing the Terrarium";
            TerraSphere();
        }

        private void Acropolis(GenerationProgress progress)
        {
            progress.Message = "Amassing Treasure";
            SkyAcropolis();
        }

        public void SkyAcropolis()
        {
            Point origin = new Point((int)(Main.maxTilesX * 0.65f), 100);
            Acropolis biome = new Acropolis();
            biome.Place(origin, WorldGen.structures);
        }
        private void Hoard(GenerationProgress progress)
        {
            progress.Message = "Amassing Treasure";
            HoardCave();
        }

        public void HoardCave()
        {
            Point origin = new Point((int)(Main.maxTilesX * 0.3f), (int)(Main.maxTilesY * 0.65f));
            origin.Y = BaseWorldGen.GetFirstTileFloor(origin.X, origin.Y, true);
            Hoard biome = new Hoard();
            HoardClear delete = new HoardClear();
            delete.Place(origin, WorldGen.structures);
            biome.Place(origin, WorldGen.structures);
        }

        public static int GetWorldSize()
        {
            if (Main.maxTilesX <= 4200) { return 1; }
            else if (Main.maxTilesX <= 6400) { return 2; }
            else if (Main.maxTilesX <= 8400) { return 3; }
            return 1;
        }

        public void InfernoVolcano()
        {
            Point origin = new Point((int)infernoPos.X, (int)infernoPos.Y);
            origin.Y = BaseWorldGen.GetFirstTileFloor(origin.X, origin.Y, true);
            InfernoBiome biome = new InfernoBiome();
            InfernoDelete delete = new InfernoDelete();
            delete.Place(origin, WorldGen.structures);
            biome.Place(origin, WorldGen.structures);
        }

        public void MireAbyss()
        {
            Point origin = new Point ((int)mirePos.X, (int)mirePos.Y);
            origin.Y = BaseWorldGen.GetFirstTileFloor(origin.X, origin.Y, true);
            MireDelete delete = new MireDelete();
            MireBiome biome = new MireBiome();
            delete.Place(origin, WorldGen.structures);
            biome.Place(origin, WorldGen.structures);
        }

        public void SunkenShip()
        {
            Point origin = new Point((int)shipPos.X, (int)shipPos.Y);
            origin.Y = BaseWorldGen.GetFirstTileFloor(origin.X, origin.Y, true);
            BOTE biome = new BOTE();
            biome.Place(origin, WorldGen.structures);
        }

        public void TerraSphere()
        {
            Point origin = new Point((int)(Main.maxTilesX * 0.5f), (int)(Main.maxTilesY * 0.4f));
            origin.Y = BaseWorldGen.GetFirstTileFloor(origin.X, origin.Y, true);
            TerrariumDelete delete = new TerrariumDelete();
            TerrariumSphere biome = new TerrariumSphere();
            delete.Place(origin, WorldGen.structures);
            biome.Place(origin, WorldGen.structures);
        }
        
        public override void ResetNearbyTileEffects()
        {
            AAPlayer modPlayer = Main.player[Main.myPlayer].GetModPlayer<AAPlayer>(mod);
            modPlayer.VoidUnit = false;
            modPlayer.SunAltar = false;
            modPlayer.MoonAltar = false;
            modPlayer.AkumaAltar = false;
            modPlayer.YamataAltar = false;
        }

        public static void SmashAltar()
        {
            Mod mod = AAMod.instance;
            if (Main.netMode == 1 || !Main.hardMode || WorldGen.noTileActions || WorldGen.gen)
            {
                return;
            }
            int Ore1 = mod.TileType<YtriumOre>();
            int Ore2 = mod.TileType<UraniumOre>();
            int Ore3 = mod.TileType<TechneciumOre>();
            Player player = Main.player[Main.myPlayer];
            int num = 0;
            int num2 = ChaosAltarsSmashed / 3 + 1;
            float num3 = Main.maxTilesX / 4200;
            num3 = (num3 * 310f - 85 * num) * 0.85f / num2;
            if (OreCount >= 3)
            {
                OreCount = 0;
            }

            int num4;
            if (OreCount == 0)
            {
                if (Main.netMode == 0)
                {
                    if (Main.netMode != 1) BaseUtility.Chat("Your world bursts with Yttrium!", Color.Goldenrod.R, Color.Goldenrod.G, Color.Goldenrod.B, false);
                }
                num = Ore1;
                num3 *= 1.05f;
                num4 = 4;
            }
            else if (OreCount == 1)
            {
                if (Main.netMode == 0)
                {
                    if (Main.netMode != 1) BaseUtility.Chat("Your world bursts with Uranium!", Color.DarkSeaGreen.R, Color.DarkSeaGreen.G, Color.DarkSeaGreen.B, false);
                }
                num = Ore2;
                num3 *= 1.05f;
                num4 = 3;
            }
            else
            {
                if (Main.netMode == 0)
                {
                    if (Main.netMode != 1) BaseUtility.Chat("Your world bursts with Technecium!", Color.DarkCyan.R, Color.DarkCyan.G, Color.DarkCyan.B, false);
                }
                num = Ore3;
                num4 = 2;
            }
            int num8 = 0;
            while (num8 < num3)
            {
                int i2 = WorldGen.genRand.Next(100, Main.maxTilesX - 100);
                double num9 = Main.worldSurface;
                if (num == Ore2)
                {
                    num9 = Main.rockLayer;
                }
                if (num == Ore3)
                {
                    num9 = (Main.rockLayer + Main.rockLayer + Main.maxTilesY) / 3.0;
                }
                int j2 = WorldGen.genRand.Next((int)num9, Main.maxTilesY - 150);
                WorldGen.OreRunner(i2, j2, WorldGen.genRand.Next(5, 9 + num4), WorldGen.genRand.Next(5, 9 + num4), (ushort)num);
                num8++;
            }
            if (Main.netMode != 1)
            {
                int num14 = Main.rand.Next(2) + 1;
                for (int k = 0; k < num14; k++)
                {
                    Spawn(player, mod, "ChaosDragon");
                }
            }

            OreCount += 1;
            ChaosAltarsSmashed++;
        }

        public static void Spawn(Player player, Mod mod, string name)
        {
            if (Main.netMode != 1)
            {
                int bossType = mod.NPCType(name);
                if (NPC.AnyNPCs(bossType)) { return; } //don't spawn if there's already a boss!
                int npcID = NPC.NewNPC((int)player.Center.X, (int)player.Center.Y, bossType, 0);
                Main.npc[npcID].Center = player.Center - new Vector2(MathHelper.Lerp(-200f, 200f, (float)Main.rand.NextDouble()), 100f);
                Main.npc[npcID].netUpdate2 = true; Main.npc[npcID].netUpdate = true;
            }
        }

        /* 1 = Inferno
         * 2 = Mire
         * 3 = Void
         * 4 = Mushroom
         */
        public static void AAConvert(int i, int j, int conversionType, int size = 4)
        {
            Mod mod = AAMod.instance;
            for (int k = i - size; k <= i + size; k++)
            {
                for (int l = j - size; l <= j + size; l++)
                {
                    if (WorldGen.InWorld(k, l, 1) && Math.Abs(k - i) + Math.Abs(l - j) < 6)
                    {
                        int type = Main.tile[k, l].type;
                        int wall = Main.tile[k, l].wall;
                        bool sendNet = false;
                        if (conversionType == 1)
                        {
                            if (WallID.Sets.Conversion.Stone[wall])
                            {
                                Main.tile[k, l].wall = (ushort)mod.WallType<TorchstoneWall>();
                                WorldGen.SquareWallFrame(k, l, true);
                                sendNet = true;
                            }
                            else if (WallID.Sets.Conversion.Sandstone[wall])
                            {
                                Main.tile[k, l].wall = (ushort)mod.WallType<TorchsandstoneWall>();
                                WorldGen.SquareWallFrame(k, l, true);
                                sendNet = true;
                            }
                            else if (WallID.Sets.Conversion.HardenedSand[wall])
                            {
                                Main.tile[k, l].wall = (ushort)mod.WallType<TorchsandHardenedWall>();
                                WorldGen.SquareWallFrame(k, l, true);
                                sendNet = true;
                            }
                            if (TileID.Sets.Conversion.Stone[type])
                            {
                                Main.tile[k, l].type = (ushort)mod.TileType<Torchstone>();
                                WorldGen.SquareTileFrame(k, l, true);
                                sendNet = true;
                            }
                            else if (TileID.Sets.Conversion.Grass[type] && type != TileID.JungleGrass)
                            {
                                Main.tile[k, l].type = (ushort)mod.TileType<InfernoGrass>();
                                WorldGen.SquareTileFrame(k, l, true);
                                sendNet = true;
                            }
                            else if (TileID.Sets.Conversion.Ice[type])
                            {
                                Main.tile[k, l].type = (ushort)mod.TileType<Torchice>();
                                WorldGen.SquareTileFrame(k, l, true);
                                sendNet = true;
                            }
                            else if (TileID.Sets.Conversion.Sand[type])
                            {
                                Main.tile[k, l].type = (ushort)mod.TileType<Torchsand>();
                                WorldGen.SquareTileFrame(k, l);
                                sendNet = true;
                            }
                            else if (TileID.Sets.Conversion.HardenedSand[type])
                            {
                                Main.tile[k, l].type = (ushort)mod.TileType<TorchsandHardened>();
                                WorldGen.SquareTileFrame(k, l);
                                sendNet = true;
                            }
                            else if (TileID.Sets.Conversion.Sandstone[type])
                            {
                                Main.tile[k, l].type = (ushort)mod.TileType<Torchsandstone>();
                                WorldGen.SquareTileFrame(k, l);
                                sendNet = true;
                            }

                            if (sendNet)
                                NetMessage.SendTileSquare(-1, k, l, 1);
                        }
                        else if (conversionType == 2)
                        {
                            if (WallID.Sets.Conversion.Stone[wall])
                            {
                                Main.tile[k, l].wall = (ushort)mod.WallType<DepthstoneWall>();
                                WorldGen.SquareWallFrame(k, l, true);
                                sendNet = true;
                            }
                            else if (WallID.Sets.Conversion.Sandstone[wall])
                            {
                                Main.tile[k, l].wall = (ushort)mod.WallType<DepthsandstoneWall>();
                                WorldGen.SquareWallFrame(k, l, true);
                                sendNet = true;
                            }
                            else if (WallID.Sets.Conversion.HardenedSand[wall])
                            {
                                Main.tile[k, l].wall = (ushort)mod.WallType<DepthsandHardenedWall>();
                                WorldGen.SquareWallFrame(k, l, true);
                                sendNet = true;
                            }
                            if (TileID.Sets.Conversion.Stone[type])
                            {
                                Main.tile[k, l].type = (ushort)mod.TileType<Depthstone>();
                                WorldGen.SquareTileFrame(k, l, true);
                                sendNet = true;
                            }
                            else if (type == TileID.JungleGrass)
                            {
                                Main.tile[k, l].type = (ushort)mod.TileType<MireGrass>();
                                WorldGen.SquareTileFrame(k, l, true);
                                sendNet = true;
                            }
                            else if (TileID.Sets.Conversion.Ice[type])
                            {
                                Main.tile[k, l].type = (ushort)mod.TileType<IndigoIce>();
                                WorldGen.SquareTileFrame(k, l, true);
                                sendNet = true;
                            }
                            else if (TileID.Sets.Conversion.Sand[type])
                            {
                                Main.tile[k, l].type = (ushort)mod.TileType<Depthsand>();
                                WorldGen.SquareTileFrame(k, l);
                                sendNet = true;
                            }
                            else if (TileID.Sets.Conversion.HardenedSand[type])
                            {
                                Main.tile[k, l].type = (ushort)mod.TileType<DepthsandHardened>();
                                WorldGen.SquareTileFrame(k, l);
                                sendNet = true;
                            }
                            else if (TileID.Sets.Conversion.Sandstone[type])
                            {
                                Main.tile[k, l].type = (ushort)mod.TileType<Depthsandstone>();
                                WorldGen.SquareTileFrame(k, l);
                                sendNet = true;
                            }
                            if (sendNet)
                                NetMessage.SendTileSquare(-1, k, l, 1);
                        }
                        else if (conversionType == 3)
                        {
                            if (TileID.Sets.Conversion.Stone[type] || TileID.Sets.Conversion.Moss[type])
                            {
                                Main.tile[k, l].type = (ushort)mod.TileType<DoomstoneB>();
                                WorldGen.SquareTileFrame(k, l, true);
                                NetMessage.SendTileSquare(-1, k, l, 1);
                            }
                            else if (TileID.Sets.Conversion.Grass[type])
                            {
                                Main.tile[k, l].type = (ushort)mod.TileType<Doomgrass>();
                                WorldGen.SquareTileFrame(k, l, true);
                                NetMessage.SendTileSquare(-1, k, l, 1);
                            }
                        }
                        else if (conversionType == 4)
                        {
                            if (WallID.Sets.Conversion.Grass[type])
                            {
                                Main.tile[k, l].type = (ushort)mod.WallType<Mushwall>();
                                WorldGen.SquareTileFrame(k, l, true);
                                NetMessage.SendTileSquare(-1, k, l, 1);
                            }
                            else if (TileID.Sets.Conversion.Grass[type])
                            {
                                Main.tile[k, l].type = (ushort)mod.TileType<Mycelium>();
                                WorldGen.SquareTileFrame(k, l, true);
                                NetMessage.SendTileSquare(-1, k, l, 1);
                            }
                        }
                        else if (conversionType == 5)
                        {
                            if (wall == WallID.Mushroom)
                            {
                                Main.tile[k, l].type = WallID.Jungle;
                                WorldGen.SquareTileFrame(k, l, true);
                                NetMessage.SendTileSquare(-1, k, l, 1);
                            }
                            else if (wall == WallID.MushroomUnsafe)
                            {
                                Main.tile[k, l].type = WallID.JungleUnsafe;
                                WorldGen.SquareTileFrame(k, l, true);
                                NetMessage.SendTileSquare(-1, k, l, 1);
                            }
                            else if (wall == mod.WallType<Mushwall>())
                            {
                                Main.tile[k, l].type = WallID.Grass;
                                WorldGen.SquareTileFrame(k, l, true);
                                NetMessage.SendTileSquare(-1, k, l, 1);
                            }


                            if (type == TileID.MushroomGrass)
                            {
                                Main.tile[k, l].type = TileID.JungleGrass;
                                WorldGen.SquareTileFrame(k, l, true);
                                NetMessage.SendTileSquare(-1, k, l, 1);
                            }
                            else if (type == (ushort)mod.TileType<Mycelium>())
                            {
                                Main.tile[k, l].type = TileID.Grass;
                                WorldGen.SquareTileFrame(k, l, true);
                                NetMessage.SendTileSquare(-1, k, l, 1);
                            }
                        }
                        else if (conversionType == 6) //Jungle
                        {
                            if (wall == 2)
                            {
                                Main.tile[k, l].wall = 15;
                                WorldGen.SquareWallFrame(k, l, true);
                                NetMessage.SendTileSquare(-1, k, l, 1);
                            }
                            else if (wall == 63)
                            {
                                Main.tile[k, l].wall = 64;
                                WorldGen.SquareWallFrame(k, l, true);
                                NetMessage.SendTileSquare(-1, k, l, 1);
                            }
                            else if (WallID.Sets.Conversion.Stone[wall] && wall != WallID.Stone)
                            {
                                Main.tile[k, l].wall = WallID.Stone;
                                WorldGen.SquareWallFrame(k, l, true);
                                NetMessage.SendTileSquare(-1, k, l, 1);
                            }
                            else if (WallID.Sets.Conversion.HardenedSand[wall] && wall != WallID.HardenedSand)
                            {
                                Main.tile[k, l].wall = WallID.HardenedSand;
                                WorldGen.SquareWallFrame(k, l, true);
                                NetMessage.SendTileSquare(-1, k, l, 1);
                            }
                            else if (WallID.Sets.Conversion.Sandstone[wall] && wall != WallID.Sandstone)
                            {
                                Main.tile[k, l].wall = WallID.Sandstone;
                                WorldGen.SquareWallFrame(k, l, true);
                                NetMessage.SendTileSquare(-1, k, l, 1);
                            }

                            if (type == 0 && Main.tile[k, l].active())
                            {
                                Main.tile[k, l].type = TileID.Mud;
                                WorldGen.SquareTileFrame(k, l, true);
                                NetMessage.SendTileSquare(-1, k, l, 1);
                            }
                            else if (TileID.Sets.Grass[type] || type == TileID.MushroomGrass)
                            {
                                Main.tile[k, l].type = TileID.JungleGrass;
                                WorldGen.SquareTileFrame(k, l, true);
                                NetMessage.SendTileSquare(-1, k, l, 1);
                            }
                            else if (TileID.Sets.Stone[type] && type != TileID.Stone)
                            {
                                Main.tile[k, l].type = TileID.Stone;
                                WorldGen.SquareTileFrame(k, l, true);
                                NetMessage.SendTileSquare(-1, k, l, 1);
                            }
                            else if (type == 3)
                            {
                                Main.tile[k, l].type = 61;
                                WorldGen.SquareTileFrame(k, l, true);
                                NetMessage.SendTileSquare(-1, k, l, 1);
                            }
                            else if (type == 52)
                            {
                                Main.tile[k, l].type = 62;
                                WorldGen.SquareTileFrame(k, l, true);
                                NetMessage.SendTileSquare(-1, k, l, 1);
                            }
                            else if (type == 73)
                            {
                                Main.tile[k, l].type = 74;
                                WorldGen.SquareTileFrame(k, l, true);
                                NetMessage.SendTileSquare(-1, k, l, 1);
                            }
                        }
                        else if (conversionType == 7) //Jungle Removal
                        {
                            if (wall == 15)
                            {
                                Main.tile[k, l].wall = 2;
                                WorldGen.SquareWallFrame(k, l, true);
                                NetMessage.SendTileSquare(-1, k, l, 1);
                            }
                            if (wall == 64)
                            {
                                Main.tile[k, l].wall = 63;
                                WorldGen.SquareWallFrame(k, l, true);
                                NetMessage.SendTileSquare(-1, k, l, 1);
                            }

                            if (type == TileID.Mud && Main.tile[k, l].active())
                            {
                                Main.tile[k, l].type = 0;
                                WorldGen.SquareTileFrame(k, l, true);
                                NetMessage.SendTileSquare(-1, k, l, 1);
                            }

                            else if (type == 60)
                            {
                                Main.tile[k, l].type = 2;
                                WorldGen.SquareTileFrame(k, l, true);
                                NetMessage.SendTileSquare(-1, k, l, 1);
                            }
                            else if (type == 61)
                            {
                                Main.tile[k, l].type = 3;
                                WorldGen.SquareTileFrame(k, l, true);
                                NetMessage.SendTileSquare(-1, k, l, 1);
                            }
                            else if (type == 62)
                            {
                                Main.tile[k, l].type = 52;
                                WorldGen.SquareTileFrame(k, l, true);
                                NetMessage.SendTileSquare(-1, k, l, 1);
                            }
                            else if (type == 74)
                            {
                                Main.tile[k, l].type = 73;
                                WorldGen.SquareTileFrame(k, l, true);
                                NetMessage.SendTileSquare(-1, k, l, 1);
                            }
                        }
                        else if (conversionType == 8) //Snow
                        {
                            if (wall == 2 || wall == 63 || wall == 65)
                            {
                                Main.tile[k, l].wall = 40;
                                WorldGen.SquareWallFrame(k, l, true);
                                NetMessage.SendTileSquare(-1, k, l, 1);
                            }
                            if (type == 0 && Main.tile[k, l].active() || type == 2 || type == 23 || type == 109 || type == 199)
                            {
                                Main.tile[k, l].type = 147;
                                WorldGen.SquareTileFrame(k, l, true);
                                NetMessage.SendTileSquare(-1, k, l, 1);
                            }
                            else if (type == 1)
                            {
                                Main.tile[k, l].type = 161;
                                WorldGen.SquareTileFrame(k, l, true);
                                NetMessage.SendTileSquare(-1, k, l, 1);
                            }
                            else if (type == 25)
                            {
                                Main.tile[k, l].type = 163;
                                WorldGen.SquareTileFrame(k, l, true);
                                NetMessage.SendTileSquare(-1, k, l, 1);
                            }
                            else if (type == 117)
                            {
                                Main.tile[k, l].type = 164;
                                WorldGen.SquareTileFrame(k, l, true);
                                NetMessage.SendTileSquare(-1, k, l, 1);
                            }

                            else if (type == 203)
                            {
                                Main.tile[k, l].type = 200;
                                WorldGen.SquareTileFrame(k, l, true);
                                NetMessage.SendTileSquare(-1, k, l, 1);
                            }
                            else if (type == mod.TileType<Torchstone>())
                            {
                                Main.tile[k, l].type = (ushort)mod.TileType<Torchice>();
                                WorldGen.SquareTileFrame(k, l, true);
                                NetMessage.SendTileSquare(-1, k, l, 1);
                            }
                            else if (type == mod.TileType<Depthstone>())
                            {
                                Main.tile[k, l].type = (ushort)mod.TileType<IndigoIce>();
                                WorldGen.SquareTileFrame(k, l, true);
                                NetMessage.SendTileSquare(-1, k, l, 1);
                            }
                        }
                        else if (conversionType == 9) //Snowmelt
                        {
                            if (wall == WallID.SnowWallUnsafe)
                            {
                                Main.tile[k, l].wall = WallID.GrassUnsafe;
                                WorldGen.SquareWallFrame(k, l, true);
                                NetMessage.SendTileSquare(-1, k, l, 1);
                            }
                            if (wall == WallID.IceUnsafe)
                            {
                                Main.tile[k, l].wall = WallID.Stone;
                                WorldGen.SquareWallFrame(k, l, true);
                                NetMessage.SendTileSquare(-1, k, l, 1);
                            }
                            if (type == TileID.SnowBlock)
                            {
                                if ((WorldGen.InWorld(k, l - 1, 1) && Main.tile[k, l - 1].type == TileID.Trees) || (WorldGen.InWorld(k, l + 1, 1) && Main.tile[k, l + 1].type == TileID.Trees) ||
                                    (WorldGen.InWorld(k, l - 1, 1) && Main.tile[k, l - 1] == null) ||
                                    (WorldGen.InWorld(k, l + 1, 1) && Main.tile[k, l + 1] == null) ||
                                    (WorldGen.InWorld(k - 1, l, 1) && Main.tile[k - 1, l] == null) ||
                                    (WorldGen.InWorld(k - 1, l, 1) && Main.tile[k - 1, l] == null))
                                {
                                    Main.tile[k, l].type = 2;
                                }
                                else
                                {
                                    Main.tile[k, l].type = 0;
                                }
                                WorldGen.SquareTileFrame(k, l, true);
                                NetMessage.SendTileSquare(-1, k, l, 1);
                            }
                            else if (type == TileID.IceBlock)
                            {
                                Main.tile[k, l].type = 1;
                                WorldGen.SquareTileFrame(k, l, true);
                                NetMessage.SendTileSquare(-1, k, l, 1);
                            }
                            else if (type == TileID.CorruptIce)
                            {
                                Main.tile[k, l].type = TileID.Ebonstone;
                                WorldGen.SquareTileFrame(k, l, true);
                                NetMessage.SendTileSquare(-1, k, l, 1);
                            }
                            else if (type == TileID.HallowedIce)
                            {
                                Main.tile[k, l].type = TileID.Pearlstone;
                                WorldGen.SquareTileFrame(k, l, true);
                                NetMessage.SendTileSquare(-1, k, l, 1);
                            }
                            else if (type == TileID.FleshIce)
                            {
                                Main.tile[k, l].type = TileID.Crimstone;
                                WorldGen.SquareTileFrame(k, l, true);
                                NetMessage.SendTileSquare(-1, k, l, 1);
                            }
                            else if (type == mod.TileType<Torchice>())
                            {
                                Main.tile[k, l].type = (ushort)mod.TileType<Torchstone>();
                                WorldGen.SquareTileFrame(k, l, true);
                                NetMessage.SendTileSquare(-1, k, l, 1);
                            }
                            else if (type == mod.TileType<IndigoIce>())
                            {
                                Main.tile[k, l].type = (ushort)mod.TileType<Depthstone>();
                                WorldGen.SquareTileFrame(k, l, true);
                                NetMessage.SendTileSquare(-1, k, l, 1);
                            }
                        }
                        else if (conversionType == 11) //Order
                        {
                            if (wall == mod.WallType<TorchstoneWall>() || wall == mod.WallType<DepthstoneWall>())
                            {
                                Main.tile[k, l].wall = WallID.Stone;
                                WorldGen.SquareWallFrame(k, l, true);
                                NetMessage.SendTileSquare(-1, k, l, 1);
                            }
                            else if (wall == mod.WallType<InfernoGrassWall>())
                            {
                                Main.tile[k, l].wall = WallID.GrassUnsafe;
                                WorldGen.SquareWallFrame(k, l, true);
                                NetMessage.SendTileSquare(-1, k, l, 1);
                            }
                            else if (wall == mod.WallType<MireJungleWall>())
                            {
                                Main.tile[k, l].wall = WallID.JungleUnsafe;
                                WorldGen.SquareWallFrame(k, l, true);
                                NetMessage.SendTileSquare(-1, k, l, 1);
                            }
                            else if (wall == mod.WallType<TorchsandHardenedWall>() || wall == mod.WallType<DepthsandHardenedWall>())
                            {
                                Main.tile[k, l].wall = WallID.HardenedSand;
                                WorldGen.SquareWallFrame(k, l, true);
                                NetMessage.SendTileSquare(-1, k, l, 1);
                            }
                            else if (wall == mod.WallType<TorchsandstoneWall>() || wall == mod.WallType<DepthsandstoneWall>())
                            {
                                Main.tile[k, l].wall = WallID.Sandstone;
                                WorldGen.SquareWallFrame(k, l, true);
                                NetMessage.SendTileSquare(-1, k, l, 1);
                            }

                            if (type == mod.TileType<InfernoGrass>() || type == mod.TileType<Doomgrass>())
                            {
                                Main.tile[k, l].type = TileID.Grass;
                                WorldGen.SquareTileFrame(k, l, true);
                                NetMessage.SendTileSquare(-1, k, l, 1);
                            }
                            else if (type == mod.TileType<Torchstone>() || type == mod.TileType<Depthstone>())
                            {
                                Main.tile[k, l].type = TileID.Stone;
                                WorldGen.SquareTileFrame(k, l, true);
                                NetMessage.SendTileSquare(-1, k, l, 1);
                            }
                            else if (type == mod.TileType<MireGrass>())
                            {
                                Main.tile[k, l].type = TileID.JungleGrass;
                                WorldGen.SquareTileFrame(k, l, true);
                                NetMessage.SendTileSquare(-1, k, l, 1);
                            }
                            else if (type == mod.TileType<TorchAsh>())
                            {
                                Main.tile[k, l].type = TileID.SnowBlock;
                                WorldGen.SquareTileFrame(k, l, true);
                                NetMessage.SendTileSquare(-1, k, l, 1);
                            }
                            else if (type == mod.TileType<Torchsand>() || type == mod.TileType<Depthsand>())
                            {
                                Main.tile[k, l].type = TileID.Sand;
                                WorldGen.SquareTileFrame(k, l, true);
                                NetMessage.SendTileSquare(-1, k, l, 1);
                            }
                            else if (type == mod.TileType<TorchsandHardened>() || type == mod.TileType<DepthsandHardened>())
                            {
                                Main.tile[k, l].type = TileID.Sand;
                                WorldGen.SquareTileFrame(k, l, true);
                                NetMessage.SendTileSquare(-1, k, l, 1);
                            }
                            else if (type == mod.TileType<Torchsandstone>() || type == mod.TileType<Depthsandstone>())
                            {
                                Main.tile[k, l].type = TileID.Sandstone;
                                WorldGen.SquareTileFrame(k, l, true);
                                NetMessage.SendTileSquare(-1, k, l, 1);
                            }
                            else if (type == mod.TileType<Torchice>() || type == mod.TileType<IndigoIce>())
                            {
                                Main.tile[k, l].type = TileID.IceBlock;
                                WorldGen.SquareTileFrame(k, l, true);
                                NetMessage.SendTileSquare(-1, k, l, 1);
                            }
                        }
                    }
                }
            }
        }
    }

    public class CloudKill : MicroBiome
    {
        public override bool Place(Point origin, StructureMap structures)
        {
            Mod mod = AAMod.instance;

            Dictionary<Color, int> colorToTile = new Dictionary<Color, int>
            {
                [new Color(255, 255, 0)] = -2,
                [Color.Black] = -1 //don't touch when genning		
            };

            TexGen gen = BaseWorldGenTex.GetTexGenerator(mod.GetTexture("Worldgeneration/AcropolisArena"), colorToTile);

            gen.Generate(origin.X, origin.Y, true, true);

            return true;
        }
    }
}
