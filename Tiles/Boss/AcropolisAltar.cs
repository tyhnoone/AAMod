﻿using BaseMod;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ObjectData;
using Terraria.Localization;
using AAMod.NPCs.Bosses.Athena;
using AAMod.NPCs.Bosses.Athena.Olympian;

namespace AAMod.Tiles.Boss
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

        public Vector2 Origin = new Vector2((int)(Main.maxTilesX * 0.65f), 100) * 16;

        public override bool NewRightClick(int i, int j)
        {
            Player player = Main.LocalPlayer;
            int type = ModContent.ItemType<Items.BossSummons.Owl>();
            bool Athena = NPC.AnyNPCs(ModContent.NPCType<Athena>()) || NPC.AnyNPCs(ModContent.NPCType<AthenaFlee>()) || NPC.AnyNPCs(ModContent.NPCType<AthenaDefeat>()) || NPC.AnyNPCs(ModContent.NPCType<AthenaA>());
            if (BasePlayer.HasItem(player, type, 1) && !Athena)
            {
                for (int m = 0; m < 50; m++)
                {
                    Item item = player.inventory[m];
                    if (item != null && item.type == type && item.stack >= 1)
                    {
                        item.stack--;
                        SpawnBoss(player, ModContent.NPCType<Athena>(), true, player.Center, 0, -1, Language.GetTextValue("Mods.AAMod.Common.Athena"), false);
                    }
                }
            }
            return true;
        }

        // SpawnBoss(player, mod.NPCType("MyBoss"), true, 0, 0, "DerpyBoi 2", false);
        public static void SpawnBoss(Player player, int bossType, bool spawnMessage = true, Vector2 Pos = default, int overrideDirection = 0, int overrideDirectionY = 0, string overrideDisplayName = "", bool namePlural = false)
        {
            if (overrideDirection == 0)
                overrideDirection = Main.rand.Next(2) == 0 ? -1 : 1;
            if (overrideDirectionY == 0)
                overrideDirectionY = -1;
            Vector2 npcCenter = Pos + new Vector2(MathHelper.Lerp(500f, 800f, (float)Main.rand.NextDouble()) * overrideDirection, 800f * overrideDirectionY);
            for (int a = 0; a < 8; a++)
            {
                Dust.NewDust(npcCenter, 152, 114, ModContent.DustType<Feather>(), Main.rand.Next(-1, 2), 1, 0);
            }
            AAModGlobalNPC.SpawnBoss(player, bossType, spawnMessage, npcCenter, overrideDisplayName, namePlural);
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