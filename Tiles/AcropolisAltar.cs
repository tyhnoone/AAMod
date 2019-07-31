﻿using BaseMod;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ObjectData;

namespace AAMod.Tiles
{
    public class AcropolisAltar : ModTile
    {
        public override void SetDefaults()
        {
            Main.tileSolidTop[Type] = false;
            Main.tileFrameImportant[Type] = true;
            Main.tileNoAttach[Type] = true;
            dustType = DustID.BlueCrystalShard;
            Main.tileLavaDeath[Type] = false;
            TileObjectData.newTile.CopyFrom(TileObjectData.Style3x3);
            TileObjectData.newTile.CoordinateHeights = new int[] { 16, 16, 16 };
            TileObjectData.newTile.CoordinateWidth = 16;
            TileObjectData.newTile.CoordinatePadding = 2;
            TileObjectData.addTile(Type);
            ModTranslation name = CreateMapEntryName();
            name.SetDefault("Owl Altar");
            AddMapEntry(new Color(0, 50, 150), name);
            disableSmartCursor = true;
            animationFrameHeight = 54;
        }

        public override void AnimateTile(ref int frame, ref int frameCounter)
        {
            if (NPC.AnyNPCs(mod.NPCType("Athena")))
            {
                frame = 1;
            }
            else
            {
                frame = 0;
            }
        }

        public Color White(Color color)
        {
            return AAColor.Sky;
        }

        public override void PostDraw(int x, int y, SpriteBatch sb)
        {
            Tile tile = Main.tile[x, y];
            Texture2D glowTex = mod.GetTexture("Glowmasks/AcropolisAltar_Glow");

            int frameY = tile != null && tile.active() ? tile.frameY + (Main.tileFrame[Type] * 54) : 0;
            BaseDrawing.DrawTileTexture(sb, glowTex, x, y, 16, 16, tile.frameX, frameY, false, false, false, null, White);
        }

        public override void RightClick(int i, int j)
        {
            Vector2 Origin = new Vector2((int)(Main.maxTilesX * 0.65f), 100);
            Player player = Main.player[Main.myPlayer];
            int type = mod.ItemType<Items.BossSummons.Owl>();
            if (BasePlayer.HasItem(player, type, 1))
            {
                for (int m = 0; m < 50; m++)
                {
                    Item item = player.inventory[m];
                    if (item != null && item.type == type && item.stack >= 1)
                    {
                        item.stack--;
                        if (item.stack <= 0)
                        {
                            item = new Item();
                        }
                        AAModGlobalNPC.SpawnBoss(player, mod.NPCType<NPCs.Bosses.Athena.Athena>(), true, Origin, "Athena");
                        for (int a = 0; a < 8; a++)
                        {
                            Dust.NewDust(Origin, 152, 114, mod.DustType<NPCs.Bosses.Athena.Feather>(), Main.rand.Next(-1, 2), 1, 0);
                        }
                    }
                }
            }
        }

        public override bool CanKillTile(int i, int j, ref bool blockDamaged)
        {
            return false;
        }

        public override bool CanExplode(int i, int j)
        {
            return false;
        }
    }
}