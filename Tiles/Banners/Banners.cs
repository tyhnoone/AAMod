
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using Terraria;
using Terraria.DataStructures;
using Terraria.ObjectData;
using Terraria.Enums;
using Terraria.ModLoader;

namespace AAMod.Tiles.Banners
{
    public class Banners : ModTile
	{
        public override void SetDefaults()
        {
			Main.tileFrameImportant[Type] = true;
			Main.tileNoAttach[Type] = true;
			Main.tileLavaDeath[Type] = false;
            TileObjectData.newTile.CopyFrom(TileObjectData.Style1x2Top);
            TileObjectData.newTile.Height = 3;
            TileObjectData.newTile.CoordinateHeights = new int[] { 16, 16, 16 };
			TileObjectData.newTile.CoordinatePadding = 0;		
			TileObjectData.newTile.AnchorTop = new AnchorData(AnchorType.SolidBottom, TileObjectData.newTile.Width, 0);			
            TileObjectData.newTile.StyleHorizontal = true;
            TileObjectData.addTile(Type);
			dustType = -1;
			disableSmartCursor = true;
			ModTranslation name = CreateMapEntryName();
			name.SetDefault("Banner");
			AddMapEntry(new Color(13, 88, 130), name);			
        }

		/*IF YOU ARE ADDING A BANNER:
		 1. add an entry for it's style in GetStyleForName, increasing the style count by one. 0 is used for 'broken' banners so you can easily tell if the banner didn't load right.
		 2. add an entry for it's name in GetBannerName, including spaces. The id used should match the one GetStyleForName gives.
		 */

		public static int GetStyleForName(string name)
		{
			switch(name)
			{
				case "Scout": return 1;
				case "FeralMonster": return 2;
				case "MiniDjinn": return 3;
				case "BlazePhoenix": return 4;
				case "ChaoticDawn": return 5;
				case "Dragron": return 6;
				case "Flamebrute": return 7;
				case "Flamespitter": return 8;
				case "InfernalSlime": return 9;
				case "InfernoSalamander": return 10;
				case "Wyrm": return 11;
				case "Wyrmling": return 12;
				case "Wyvern": return 13;
				case "Magmalgam": return 14;
				case "ChaoticTwilight": return 15;
				case "Kappa": return 16;
				case "Miregron": return 17;
				case "MireSlime": return 18;
				case "Mosster": return 19;
				case "Newt": return 20;
				case "AbyssClaw": return 21;
				case "BlazeClaw": return 22;
				case "ChaosDragon": return 23;
				case "CyberClaw": return 24;
				case "DragonClaw": return 25;
				case "ElderDragon": return 26;
				case "HydraClaw": return 27;
				case "MadnessBat": return 28;
				case "MadnessSlime": return 29;
				case "DimensionDiver": return 30;
				case "RiftShark": return 31;
				case "Squid": return 32;
				case "Snake": return 33; //snow serpent
				case "Bladon": return 34;
				case "TerraDeadshot": return 35;
				case "TerraWarlock": return 36;
				case "TerraWizard": return 37;
				case "Null": return 38;
				case "Searcher": return 39;
				case "Toxitoad": return 40;
                case "SagittariusMini": return 40;
            }
			return 0; //broken banner
		}

		public static string GetBannerName(int frameX, bool spaces = false)
        {
			string dropName = null;
			int style = frameX / 16;
			switch (style)
			{
				case 1: dropName = "Void Scout"; break;
				case 2: dropName = "Feral Monster"; break;		
				case 3: dropName = "Mini Djinn"; break;	
				case 4: dropName = "Blaze Phoenix"; break;		
				case 5: dropName = "Chaotic Dawn"; break;
				case 6: dropName = "Dragron"; break;		
				case 7: dropName = "Flame brute"; break;	
				case 8: dropName = "Flamespitter"; break;	
				case 9: dropName = "Infernal Slime"; break;
				case 10: dropName = "Inferno Salamander"; break;
				case 11: dropName = "Wyrm"; break;
				case 12: dropName = "Wyrmling"; break;		
				case 13: dropName = "Wyvern"; break;	
				case 14: dropName = "Magmalgam"; break;	
				case 15: dropName = "Chaotic Twilight"; break;
				case 16: dropName = "Kappa"; break;		
				case 17: dropName = "Miregron"; break;	
				case 18: dropName = "Mire Slime"; break;					
				case 19: dropName = "Mosster"; break;
				case 20: dropName = "Newt"; break;	
				case 21: dropName = "Abyss Claw"; break;
				case 22: dropName = "Blaze Claw"; break;		
				case 23: dropName = "Chaos Dragon"; break;	
				case 24: dropName = "Cyber Claw"; break;	
				case 25: dropName = "Dragon Claw"; break;
				case 26: dropName = "Elder Dragon"; break;		
				case 27: dropName = "Hydra Claw"; break;	
				case 28: dropName = "Madness Bat"; break;					
				case 29: dropName = "Madness Slime"; break;
				case 30: dropName = "Dimension Diver"; break;	
				case 31: dropName = "Rift Shark"; break;
				case 32: dropName = "Squid"; break;		
				case 33: dropName = "Snake"; break;	
				case 34: dropName = "Bladon"; break;	
				case 35: dropName = "Terra Deadshot"; break;
				case 36: dropName = "Terra Warlock"; break;		
				case 37: dropName = "Terra Wizard"; break;	
				case 38: dropName = "Null"; break;					
				case 39: dropName = "Searcher"; break;
				case 40: dropName = "Toxitoad"; break;
                case 41: dropName = "Shadow Scout"; break;
            }
			if(spaces) //fix for display names
			{
				switch (style)
				{
					case 3: dropName = "Djinn"; break;		
					case 6: dropName = "Pigron"; break;		
					case 7: dropName = "Flame Brute"; break;	
					case 8: dropName = "Flamespitter"; break;	
					case 9: dropName = "Infernal Slime"; break;
					case 10: dropName = "Singemander"; break;
					case 14: dropName = "Magmalgam"; break;					
					case 17: dropName = "Pigron"; break;				
					case 18: dropName = "Murky Slime"; break;			
					case 21: dropName = "Abyss Claw"; break;
					case 22: dropName = "Blaze Claw"; break;	
					case 28: dropName = "Madness Flier"; break;					
					case 30: dropName = "Trench Diver"; break;	
					case 31: dropName = "Trench Shark"; break;
					case 32: dropName = "Trench Squid"; break;						
					case 33: dropName = "Snow Serpent"; break;	
					case 34: dropName = "Terra Knight"; break;		
				}				
			}
			if(!string.IsNullOrEmpty(dropName) && !spaces) dropName = dropName.Replace(" ", null);
			return dropName;
        }

        public override void KillMultiTile(int x, int y, int frameX, int frameY)
        {
            string dropName = GetBannerName(frameX);
            if (!string.IsNullOrEmpty(dropName))
            {
                Item.NewItem(x * 16, y * 16, 16, 16, mod.ItemType(dropName + "Banner"), 1, false, -1, false);
            }
        }

        public override void NearbyEffects(int x, int y, bool closer)
        {
            if (closer)
            {
				string name = GetBannerName(Main.tile[x, y].frameX);
				if(name == null) return;

                Player player = Main.LocalPlayer;	
				if(name.Contains("Snake")) //worms need to be specific
				{
					player.NPCBannerBuff[mod.NPCType(name + "Head")] = true;
				}else				
					player.NPCBannerBuff[mod.NPCType(name)] = true;
                player.hasBanner = true;
            }
        }

        public override void SetSpriteEffects(int i, int j, ref SpriteEffects spriteEffects)
        {
            if (i % 2 == 1)
            {
                spriteEffects = SpriteEffects.FlipHorizontally;
            }
        }
	}
}

