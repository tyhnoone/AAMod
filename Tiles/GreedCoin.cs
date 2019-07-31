﻿using System;
using Terraria.ModLoader;
using Terraria.ID;
using Terraria;
using Microsoft.Xna.Framework;

namespace AAMod.Tiles
{
    class GreedCoin : ModTile
    {
        public override void SetDefaults()
        {
            Main.tileSolid[Type] = false;
            Main.tileMergeDirt[Type] = false;
            Main.tileBlendAll[Type] = false;
            Main.tileFrameImportant[Type] = false;
            drop = mod.ItemType("CovetiteCoin");
            soundStyle = 18;
            AddMapEntry(Color.Goldenrod);
            dustType = DustID.GoldCoin;
        }

        public override bool TileFrame(int i, int j, ref bool resetFrame, ref bool noBreak)
        {
            Tile tile = Main.tile[i, j];
            Tile tile2 = Main.tile[i, j - 1];
            Tile tile3 = Main.tile[i, j + 1];
            int tileType = tile.type;
            if (!WorldGen.noTileActions && tile.active() && (tileType == Type))
            {
                if (Main.netMode == 0)
                {
                    if (tile3 != null && !tile3.active())
                    {
                        bool flag18 = !(tile2.active() && (TileID.Sets.BasicChest[tile2.type] || TileID.Sets.BasicChestFake[tile2.type] || tile2.type == 323 || TileLoader.IsDresser(tile2.type)));
                        if (flag18)
                        {
                            int damage = 10;
                            int projectileType = 0;
                            if (tileType == Type)
                            {
                                projectileType = mod.ProjectileType("GreedCoinProj");
                                damage = 0;
                            }
                            tile.ClearTile();
                            int num77 = Projectile.NewProjectile(i * 16 + 8, j * 16 + 8, 0f, 0.41f, projectileType, damage, 0f, Main.myPlayer, 0f, 0f);
                            Main.projectile[num77].ai[0] = 1f;
                            WorldGen.SquareTileFrame(i, j, true);
                        }
                    }
                }
                else if (Main.netMode == 2 && tile3 != null && !tile3.active())
                {
                    bool flag19 = !(tile2.active() && (TileID.Sets.BasicChest[tile2.type] || TileID.Sets.BasicChestFake[tile2.type] || tile2.type == 323 || TileLoader.IsDresser(tile2.type)));
                    if (flag19)
                    {
                        int damage2 = 10;
                        int projectileType = 0;
                        if (tileType == Type)
                        {
                            projectileType = mod.ProjectileType("GreedCoinProj");
                            damage2 = 0;
                        }

                        tile.active(false);
                        bool flag20 = false;
                        for (int m = 0; m < 1000; m++)
                        {
                            if (Main.projectile[m].active && Main.projectile[m].owner == Main.myPlayer && Main.projectile[m].type == projectileType && Math.Abs(Main.projectile[m].timeLeft - 3600) < 60 && Main.projectile[m].Distance(new Vector2(i * 16 + 8, j * 16 + 10)) < 4f)
                            {
                                flag20 = true;
                                break;
                            }
                        }
                        if (!flag20)
                        {
                            int num79 = Projectile.NewProjectile(i * 16 + 8, j * 16 + 8, 0f, 2.5f, projectileType, damage2, 0f, Main.myPlayer, 0f, 0f);
                            Main.projectile[num79].velocity.Y = 0.5f;
                            Projectile expr_7AAA_cp_0 = Main.projectile[num79];
                            expr_7AAA_cp_0.position.Y += 2f;
                            Main.projectile[num79].netUpdate = true;
                        }
                        NetMessage.SendTileSquare(-1, i, j, 1, TileChangeType.None);
                        WorldGen.SquareTileFrame(i, j, true);
                    }
                }
            }
            return true;
        }
    }
}
