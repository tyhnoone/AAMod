using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Utilities;

namespace AAMod.NPCs.TownNPCs
{
    [AutoloadHead]
	public class Lovecraftian : ModNPC
	{
        public override string Texture => "AAMod/NPCs/TownNPCs/Lovecraftian";

        public override bool Autoload(ref string name)
		{
			name = "Lovecraftian";
			return mod.Properties.Autoload;
		}

        public override void SetStaticDefaults()
        {
            Main.npcFrameCount[npc.type] = 26;
            NPCID.Sets.ExtraFramesCount[npc.type] = 10;
            NPCID.Sets.AttackFrameCount[npc.type] = 5;
            NPCID.Sets.DangerDetectRange[npc.type] = 700;
            NPCID.Sets.AttackType[npc.type] = 0;
            NPCID.Sets.AttackTime[npc.type] = 40;
            NPCID.Sets.AttackAverageChance[npc.type] = 20;
            NPCID.Sets.HatOffsetY[npc.type] = 3;
        }

        public override void SetDefaults()
        {
            npc.townNPC = true;
            npc.friendly = true;
            npc.width = 18;
            npc.height = 40;
            npc.aiStyle = 7;
            npc.damage = 40;
            npc.defense = 38;
            npc.lifeMax = 600;
            npc.HitSound = SoundID.NPCHit1;
            npc.DeathSound = SoundID.NPCDeath1;
            npc.knockBackResist = 0.5f;
            animationType = NPCID.Guide;
        }

        public override void HitEffect(int hitDirection, double damage)
        {
        }

        public override bool CanTownNPCSpawn(int numTownNPCs, int money)
        {
            for (int k = 0; k < 255; k++)
            {
                Player player = Main.player[k];
                if (player.active)
                {
                    if (NPC.downedBoss1 == true)
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        public override string TownNPCName()
		{
			switch (WorldGen.genRand.Next(4))
			{
				case 0:
					return "Aletheia";
				case 1:
					return "C'thalpa";
				case 2:
					return "D’endrrah";
                case 3:
                    return "Ycnàgnnisssz";
                default:
                    return "Yidhra";				
			}
		}
        
        public override string GetChat()
        {   
			Mod Fargos = ModLoader.GetMod("FargoMod");
			Mod GRealm = ModLoader.GetMod("Grealm");

            WeightedRandom<string> chat = new WeightedRandom<string>();


            int Pirate = NPC.FindFirstNPC(NPCID.Pirate);
            int Mutant = Fargos == null ? -1 : NPC.FindFirstNPC(Fargos.NPCType("Mutant"));
            int HordeZombie = GRealm == null ? -1 : NPC.FindFirstNPC(GRealm.NPCType("HordeZombie"));

            chat.Add("You know, where I’m from, I’m what your world would call ‘hot stuff.’");

            chat.Add("I wasn’t the only thing that came here. A whole bunch of other stuff came through with me when a spacial rift opened up in my world. Stuff like the Eye of Cthulhu and the Brain of Cthulhu were already here though. No clue where those two came from.");

            chat.Add("...What are you looking at? You act like you've never seen a squid-person before.");

            chat.Add("Yes I’m a woman. What about it? Is it the tentacle beard that threw you off?");

            chat.Add("If you have any sense of self preservation, I’d avoid that sunken ship in the ocean just off the coast. Scary things from my neck of the woods hang out there, especially... nevermind.");

            chat.Add("Ever just find things in your tentacles that you don’t know how they got there? No? Just me?");

            chat.Add("Hey, your world is pretty interesting. Could you bring me some samples from different biomes for me to study ? If you do, I can make some neat stuff to trade with you.");
            

            //If Pirate is present
            if (Pirate >= 0)
            {
                chat.Add("Oh. This is awkward. Poor " + Main.npc[Pirate].GivenName + ". His ship was the one that got destroyed when I fell out of that rift.");
            }

            //If mutant is present

            if (Mutant >= 0)
            {
                chat.Add("That " + Main.npc[Mutant].GivenName + " is talking out of his ass. Cthulhu would most likely squash him before he could even say *ech*.");
            }

            //If Horde Zombie is present
            if (HordeZombie >= 0)
            {
                chat.Add("That dead guy shambling around freaks me out, and that’s saying something considering I’m a walking horror story. I don’t know, I just feel like he knows too much...");
            }


            //Post - Moon Lord
            if (NPC.downedMoonlord)
            {
                chat.Add("Fun fact; The Moon Lord and Cthulhu are brothers. At least that’s what some pink pixie lady I met one time told me.");
            }

            //Providing materials

            return chat; // chat is implicitly cast to a string. You can also do "return chat.Get();" if that makes you feel better
        }

        public override void SetChatButtons(ref string button, ref string button2)
        {
            button = "Shop";
            button2 = "Supply Ingredients";
        }

        public override void OnChatButtonClicked(bool firstButton, ref bool shop)
        {
            Player player = Main.LocalPlayer;
            AAPlayer p = player.GetModPlayer<AAPlayer>(mod);

            if (firstButton)
            {
                shop = true;
            }

            if (!firstButton)
            {
                Main.PlaySound(12, -1, -1, 1);

                int Mushman = NPC.FindFirstNPC(mod.NPCType("Mushman"));

                int Item1 = player.FindItem(mod.ItemType<Items.Materials.TerraShard>());
                int Item2 = player.FindItem(mod.ItemType<Items.Materials.DragonScale>());
                int Item3 = player.FindItem(mod.ItemType<Items.Materials.MirePod>());
                int Item4 = player.FindItem(ItemID.RottenChunk);
                int Item5 = player.FindItem(ItemID.Vertebrae);
                int Item6 = player.FindItem(ItemID.PixieDust);
                int Item7 = player.FindItem(mod.ItemType<Items.Materials.DoomiteScrap>());
                int Item8 = player.FindItem(ItemID.JungleSpores);
                int Item9 = player.FindItem(mod.ItemType<Items.Boss.MushroomMonarch.Mushium>());
                int Item10 = player.FindItem(mod.ItemType<Items.Boss.MushroomMonarch.GlowingMushium>());
                int Item11 = player.FindItem(ItemID.Stinger);
                int Item12 = player.FindItem(ItemID.IceMachine);
                int Item13 = player.FindItem(ItemID.Bunny);

                if (Item1 >= 0 && AAWorld.squid1 < 5) //Item 1: 3 Blueberries
                {
                    Main.npcChatCornerItem = mod.ItemType<Items.Materials.TerraShard>();
                    player.inventory[Item1].stack--;
                    if (player.inventory[Item1].stack <= 0)
                    {
                        player.inventory[Item1] = new Item();
                    }
                    if (AAWorld.squid1 == 4)
                    {
                        Main.npcChatText = "Oh! Are those purity shards? Perfect! Here, take this. You can purify most biomes with this special flask I made.";
                        player.QuickSpawnItem(mod.ItemType("PurityFlask"), 5);
                        Main.npcChatCornerItem = mod.ItemType("PurityFlask");
                    }

                    if (Main.netMode == 1)
                    {
						AANet.SendNetMessage(AANet.UpdateLovecraftianCount, (byte)1);
                    }
                    AAWorld.squid1++;
                    Main.PlaySound(24, -1, -1, 1);
                }
                else if (Item2 >= 0 && AAWorld.squid2 < 5) //Item 2: 3 Teal Mushrooms
                {
                    Main.npcChatCornerItem = mod.ItemType<Items.Materials.DragonScale>();
                    player.inventory[Item2].stack--;
                    if (player.inventory[Item2].stack <= 0)
                    {
                        player.inventory[Item2] = new Item();
                    }
                    if (AAWorld.squid2 == 4)
                    {
                        Main.npcChatText = "Dragons, eh? I've seen scarier. Anyways, here's a new flask. Careful, it's hot.";
                        player.QuickSpawnItem(mod.ItemType("AshJar"), 5);
                        Main.npcChatCornerItem = mod.ItemType("AshJar");
                    }

                    if (Main.netMode == 1)
                    {
						AANet.SendNetMessage(AANet.UpdateLovecraftianCount, (byte)2);
                    }
                    AAWorld.squid2++;
                    Main.PlaySound(24, -1, -1, 1);
                }
                else if (Item3 >= 0 && AAWorld.squid3 < 5)
                {
                    Main.npcChatCornerItem = mod.ItemType<Items.Materials.MirePod>();
                    player.inventory[Item3].stack--;
                    if (player.inventory[Item3].stack <= 0)
                    {
                        player.inventory[Item3] = new Item();
                    }
                    if (AAWorld.squid3 == 4)
                    {
                        Main.npcChatText = "What is this..? It's literally a ball of...something. I'm gonna look into it. Here, new flask. Go crazy.";
                        player.QuickSpawnItem(mod.ItemType("DarkwaterFlask"), 5);
                        Main.npcChatCornerItem = mod.ItemType("DarkwaterFlask");
                    }

					if(Main.netMode == 1)
					{
						AANet.SendNetMessage(AANet.UpdateLovecraftianCount, (byte)3);
					}
                    AAWorld.squid3++;
                    Main.PlaySound(24, -1, -1, 1);
                }
                else if (Item4 >= 0 && AAWorld.squid4 < 5)
                {
                    Main.npcChatCornerItem = ItemID.RottenChunk;
                    player.inventory[Item4].stack--;
                    if (player.inventory[Item4].stack <= 0)
                    {
                        player.inventory[Item4] = new Item();
                    }
                    if (AAWorld.squid4 == 4)
                    {
                        Main.npcChatText = "Why are you gagging? There are things that smell way worse than this. It's only decomposing flesh.";
                        player.QuickSpawnItem(mod.ItemType("CorruptionFlask"), 5);
                        Main.npcChatCornerItem = mod.ItemType("CorruptionFlask");
                    }

					if(Main.netMode == 1)
					{
						AANet.SendNetMessage(AANet.UpdateLovecraftianCount, (byte)4);
					}
                    AAWorld.squid4++;
                    Main.PlaySound(24, -1, -1, 1);
                }
                else if (Item5 >= 0 && AAWorld.squid5 < 5)
                {
                    Main.npcChatCornerItem = ItemID.Vertebrae;
                    player.inventory[Item5].stack--;
                    if (player.inventory[Item5].stack <= 0)
                    {
                        player.inventory[Item5] = new Item();
                    }
                    if (AAWorld.squid5 == 4)
                    {
                        Main.npcChatText = "Hm...bones? I've seen ones like these before. Similar to ones from where I came.";
                        player.QuickSpawnItem(mod.ItemType("CrimsonFlask"), 5);
                        Main.npcChatCornerItem = mod.ItemType("CrimsonFlask");
                    }
					if(Main.netMode == 1)
					{
						AANet.SendNetMessage(AANet.UpdateLovecraftianCount, (byte)5);
					}
                    AAWorld.squid5++;
                    Main.PlaySound(24, -1, -1, 1);
                }
                else if (Item6 >= 0 && AAWorld.squid6 < 5)
                {
                    Main.npcChatCornerItem = ItemID.PixieDust;
                    player.inventory[Item6].stack--;
                    if (player.inventory[Item6].stack <= 0)
                    {
                        player.inventory[Item6] = new Item();
                    }
                    if (AAWorld.squid6 == 4)
                    {
                        Main.npcChatText = "What is this stuff? It's...sparkly..? Whatever, I'll research it a bit more. Here's a new flask.";
                        player.QuickSpawnItem(mod.ItemType("MeanGreenStew"), 5);
                        Main.npcChatCornerItem = mod.ItemType("MeanGreenStew");
                    }
					if(Main.netMode == 1)
					{
						AANet.SendNetMessage(AANet.UpdateLovecraftianCount, (byte)6);
					}
                    AAWorld.squid6++;
                    Main.PlaySound(24, -1, -1, 1);
                }
                else if (Item7 >= 0 && AAWorld.squid7 < 5)
                {
                    Main.npcChatCornerItem = mod.ItemType<Items.Materials.DoomiteScrap>();
                    player.inventory[Item7].stack--;
                    if (player.inventory[Item7].stack <= 0)
                    {
                        player.inventory[Item7] = new Item();
                    }
                    if (AAWorld.squid7 == 4)
                    {
                        Main.npcChatText = "Wow this is heavy! What is this? I've never seen this kind of metal before. Oh right. New flask. Here.";
                        player.QuickSpawnItem(mod.ItemType("VoidFlask"), 5);
                        Main.npcChatCornerItem = mod.ItemType("Z");
                    }
					if(Main.netMode == 1)
					{
						AANet.SendNetMessage(AANet.UpdateLovecraftianCount, (byte)7);
					}
                    AAWorld.squid7++;
                    Main.PlaySound(24, -1, -1, 1);
                }
                else if (Item8 >= 0 && AAWorld.squid8 < 5)
                {
                    Main.npcChatCornerItem = ItemID.JungleSpores;
                    player.inventory[Item8].stack--;
                    if (player.inventory[Item8].stack <= 0)
                    {
                        player.inventory[Item8] = new Item();
                    }
                    if (AAWorld.squid8 == 4)
                    {
                        Main.npcChatText = "Hmm...glowing spores? I've never seen something like this aside from glowing mushrooms. Speaking of mushrooms, here, I found a recipe for some really good fungicide. I made it into a flask.";
                        player.QuickSpawnItem(mod.ItemType("Fungicide"), 5);
                        Main.npcChatCornerItem = mod.ItemType("Fungicide");
                    }
					if(Main.netMode == 1)
					{
						AANet.SendNetMessage(AANet.UpdateLovecraftianCount, (byte)8);
					}
                    AAWorld.squid8++;
                    Main.PlaySound(24, -1, -1, 1);
                }
                else if (Item9 >= 0 && AAWorld.squid9 < 5 && Mushman >= 0)
                {
                    Main.npcChatCornerItem = mod.ItemType<Items.Boss.MushroomMonarch.Mushium>();
                    player.inventory[Item9].stack--;
                    if (player.inventory[Item9].stack <= 0)
                    {
                        player.inventory[Item9] = new Item();
                    }
                    if (AAWorld.squid9 == 4)
                    {
                        Main.npcChatText = "What is this stuff? It's so squishy...I'll make it work I guess. Oh by the way, " + Main.npc[Mushman].GivenName + " showed me how to make mushroom spores. Feel free to use it how you see fit.";
                        player.QuickSpawnItem(mod.ItemType("SporeSac"), 5);
                        Main.npcChatCornerItem = mod.ItemType("SporeSac");
                    }
					if(Main.netMode == 1)
					{
						AANet.SendNetMessage(AANet.UpdateLovecraftianCount, (byte)9);
					}
                    AAWorld.squid9++;
                    Main.PlaySound(24, -1, -1, 1);
                }
                else if (Item10 >= 0 && AAWorld.squid10 < 5 && Mushman >= 0)
                {
                    Main.npcChatCornerItem = mod.ItemType<Items.Boss.MushroomMonarch.GlowingMushium>();
                    player.inventory[Item10].stack--;
                    if (player.inventory[Item10].stack <= 0)
                    {
                        player.inventory[Item10] = new Item();
                    }
                    if (AAWorld.squid10 == 4)
                    {
                        Main.npcChatText = "Hm, this stuff is pretty glowy. *Sniff* Yowza that burns my nostrils. Anyways, here, I made a glowing version of  " + Main.npc[Mushman].GivenName + "'s spores.";
                        player.QuickSpawnItem(mod.ItemType("GlowingSporeSac"), 5);
                        Main.npcChatCornerItem = mod.ItemType("GlowingSporeSac");
                    }
					if(Main.netMode == 1)
					{
						AANet.SendNetMessage(AANet.UpdateLovecraftianCount, (byte)10);
					}
                    AAWorld.squid10++;
                    Main.PlaySound(24, -1, -1, 1);
                }
                else if (Item11 >= 0 && AAWorld.squid11 < 5)
                {
                    Main.npcChatCornerItem = ItemID.Stinger;
                    player.inventory[Item11].stack--;
                    if (player.inventory[Item11].stack <= 0)
                    {
                        player.inventory[Item11] = new Item();
                    }
                    if (AAWorld.squid11 == 4)
                    {
                        Main.npcChatText = "Oh thank you so much! These stingers will work nicely for some makeshift syringes. Here, I've developed a brand new solution that changes forest into jungle. Nifty, huh?";
                        player.QuickSpawnItem(mod.ItemType("JungleFlask"), 5);
                        Main.npcChatCornerItem = mod.ItemType("JungleFlask");
                    }
                    if (Main.netMode == 1)
                    {
                        AANet.SendNetMessage(AANet.UpdateLovecraftianCount, (byte)11);
                    }
                    AAWorld.squid11++;
                    Main.PlaySound(24, -1, -1, 1);
                }
                else if (Item12 >= 0 && AAWorld.squid12 < 1)
                {
                    Main.npcChatCornerItem = ItemID.IceMachine;
                    player.inventory[Item12].stack--;
                    if (player.inventory[Item12].stack <= 0)
                    {
                        player.inventory[Item12] = new Item();
                    }
                    if (AAWorld.squid12 == 0)
                    {
                        Main.npcChatText = "Now THIS will come in handy. Thank you. Hey, speaking of ice, check this out. Snow creation AND removal flasks? You like it? Two for the price of one!";
                        player.QuickSpawnItem(mod.ItemType("IceFlask"), 3);
                        player.QuickSpawnItem(mod.ItemType("IcemeltFlask"), 3);
                        Main.npcChatCornerItem = mod.ItemType("IceFlask");
                    }
                    if (Main.netMode == 1)
                    {
                        AANet.SendNetMessage(AANet.UpdateLovecraftianCount, (byte)12);
                    }
                    AAWorld.squid12++;
                    Main.PlaySound(24, -1, -1, 1);
                }
                else if (Item13 >= 0 && AAWorld.squid13 < 5)
                {
                    Main.npcChatCornerItem = ItemID.Bunny;
                    player.inventory[Item13].stack--;
                    if (player.inventory[Item13].stack <= 0)
                    {
                        player.inventory[Item13] = new Item();
                    }
                    if (AAWorld.squid13 == 4)
                    {
                        Main.npcChatText = "I wish we had these back where I came from. They're adorable~! Oh, but I'd be careful with these little guys if I were you. The dryad told me there's some giant monster that protects them...anyways, while you were out, I made a new flask. It turns Jungle into forest. Careful with it.";
                        player.QuickSpawnItem(mod.ItemType("ForestFlask"), 5);
                        Main.npcChatCornerItem = mod.ItemType("ForestFlask");
                    }
                    if (Main.netMode == 1)
                    {
                        AANet.SendNetMessage(AANet.UpdateLovecraftianCount, (byte)13);
                    }
                    AAWorld.squid13++;
                    Main.PlaySound(24, -1, -1, 1);
                }
                else
                {
                    if (!BaseMod.BasePlayer.HasItem(player, mod.ItemType<Items.Flasks.SquidList>()))
                    {
                        Main.npcChatText = "Here's a list of some things I need for my research. If you lose it, I'll happily write up a new one for you";
                        int itemID = Item.NewItem((int)player.position.X, (int)player.position.Y, player.width, player.height, mod.ItemType("SquidList"), 1, false, 0, false, false);
                        if (Main.netMode == 1)
                        {
                            NetMessage.SendData(21, -1, -1, null, itemID, 1f, 0f, 0f, 0, 0, 0);
                        }
                    }
                    else
                    {
                        Main.npcChatText = "Hmm...nothing? I need stuff to study. I'd like some important materials from biomes. Monster pieces, plants, etc.";
                    }
                    Main.npcChatCornerItem = 0;
                    Main.PlaySound(12, -1, -1, 1);
                }
            }
        }


        public override void SetupShop(Chest shop, ref int nextSlot)
        {
            if (AAWorld.squid1 >= 5)
            {
                shop.item[nextSlot].SetDefaults(mod.ItemType<Items.Flasks.PurityFlask>());
                nextSlot++;
            }
            if (AAWorld.squid2 >= 5)
            {
                shop.item[nextSlot].SetDefaults(mod.ItemType<Items.Flasks.AshJar>());
                nextSlot++;
            }
            if (AAWorld.squid3 >= 5)
            {
                shop.item[nextSlot].SetDefaults(mod.ItemType<Items.Flasks.DarkwaterFlask>());
                nextSlot++;
            }
            if (AAWorld.squid4 >= 5)
            {
                shop.item[nextSlot].SetDefaults(mod.ItemType<Items.Flasks.CorruptionFlask>());
                nextSlot++;
            }
            if (AAWorld.squid5 >= 5)
            {
                shop.item[nextSlot].SetDefaults(mod.ItemType<Items.Flasks.CrimsonFlask>());
                nextSlot++;
            }
            if (AAWorld.squid6 >= 5)
            {
                shop.item[nextSlot].SetDefaults(mod.ItemType<Items.Flasks.HallowFlask>());
                nextSlot++;
            }
            if (AAWorld.squid7 >= 5)
            {
                shop.item[nextSlot].SetDefaults(mod.ItemType<Items.Flasks.VoidFlask>());
                nextSlot++;
            }
            if (AAWorld.squid8 >= 5)
            {
                shop.item[nextSlot].SetDefaults(mod.ItemType<Items.Flasks.Fungicide>());
                nextSlot++;
            }
            if (AAWorld.squid9 >= 5)
            {
                shop.item[nextSlot].SetDefaults(mod.ItemType<Items.Usable.SporeSac>());
                nextSlot++;
            }
            if (AAWorld.squid10 >= 5)
            {
                shop.item[nextSlot].SetDefaults(mod.ItemType<Items.Flasks.GlowingSporeSac>());
                nextSlot++;
            }
            if (AAWorld.squid11 >= 5)
            {
                shop.item[nextSlot].SetDefaults(mod.ItemType<Items.Flasks.JungleFlask>());
                nextSlot++;
            }
            if (AAWorld.squid12 >= 1)
            {
                shop.item[nextSlot].SetDefaults(mod.ItemType<Items.Flasks.IceFlask>());
                nextSlot++;
                shop.item[nextSlot].SetDefaults(mod.ItemType<Items.Flasks.IcemeltFlask>());
                nextSlot++;
            }
            if (AAWorld.squid13 >= 5)
            {
                shop.item[nextSlot].SetDefaults(mod.ItemType<Items.Flasks.ForestFlask>());
                nextSlot++;
            }
        }

        public override void TownNPCAttackProj(ref int projType, ref int attackDelay)
        {
            projType = mod.ProjectileType<EyeShot>();
            attackDelay = 1;
        }
        public override void TownNPCAttackStrength(ref int damage, ref float knockback)
        {
            damage = 30;
            knockback = 4f;
        }

        public override void TownNPCAttackCooldown(ref int cooldown, ref int randExtraCooldown)
        {
            cooldown = 20;
            randExtraCooldown = 20;
        }

        public override void TownNPCAttackProjSpeed(ref float multiplier, ref float gravityCorrection, ref float randomOffset)
        {

            multiplier = 4f;

            randomOffset = 2f;

        }
    }
}
