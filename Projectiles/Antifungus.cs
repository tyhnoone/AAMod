using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using AAMod.Walls;
using AAMod.Dusts;
using AAMod.Tiles;

namespace AAMod.Projectiles
{
    internal class Antifungus : ModProjectile
    {
        public override string Texture => "AAMod/BlankTex";
        public override void SetDefaults()
        {
            projectile.width = 6;
            projectile.height = 6;
            projectile.friendly = true;
            projectile.alpha = 255;
            projectile.penetrate = -1;
            projectile.extraUpdates = 2;
            projectile.tileCollide = false;
            projectile.ignoreWater = true;
        }

        public override void AI()
        {
            int dustType = ModContent.DustType<SwarmDust>();
            if (projectile.owner == Main.myPlayer)
            {
                Convert((int)(projectile.position.X + projectile.width / 2) / 16, (int)(projectile.position.Y + projectile.height / 2) / 16);
            }
            if (projectile.timeLeft > 133)
            {
                projectile.timeLeft = 133;
            }
            if (projectile.ai[0] > 7f)
            {
                float dustScale = 1f;
                if (projectile.ai[0] == 8f)
                {
                    dustScale = 0.2f;
                }
                else if (projectile.ai[0] == 9f)
                {
                    dustScale = 0.4f;
                }
                else if (projectile.ai[0] == 10f)
                {
                    dustScale = 0.6f;
                }
                else if (projectile.ai[0] == 11f)
                {
                    dustScale = 0.8f;
                }
                projectile.ai[0] += 1f;
                for (int i = 0; i < 1; i++)
                {
                    int dustIndex = Dust.NewDust(new Vector2(projectile.position.X, projectile.position.Y), projectile.width, projectile.height, dustType, projectile.velocity.X * 0.2f, projectile.velocity.Y * 0.2f, 100);
                    Dust dust = Main.dust[dustIndex];
                    dust.noGravity = true;
                    dust.scale *= 1.75f;
                    dust.velocity.X *= 2f;
                    dust.velocity.Y *= 2f;
                    dust.scale *= dustScale;
                }
            }
            else
            {
                projectile.ai[0] += 1f;
            }
            projectile.rotation += 0.3f * projectile.direction;
        }

        public void Convert(int i, int j, int size = 4)
        {
            for (int k = i - size; k <= i + size; k++)
            {
                for (int l = j - size; l <= j + size; l++)
                {
                    if (WorldGen.InWorld(k, l, 1) && Math.Abs(k - i) + Math.Abs(l - j) < Math.Sqrt(size * size + size * size))
                    {
                        int type = Main.tile[k, l].type;
                        int wall = Main.tile[k, l].wall;
                        if (wall == WallID.Mushroom)
                        {
                            Main.tile[k, l].wall = WallID.Jungle;
                            WorldGen.SquareWallFrame(k, l, true);
                            NetMessage.SendTileSquare(-1, k, l, 1);
                        }
                        else if (wall == WallID.MushroomUnsafe)
                        {
                            Main.tile[k, l].wall = WallID.JungleUnsafe;
                            WorldGen.SquareWallFrame(k, l, true);
                            NetMessage.SendTileSquare(-1, k, l, 1);
                        }
                        else if (wall == (ushort)ModContent.WallType<Mushwall>())
                        {
                            Main.tile[k, l].wall = WallID.Grass;
                            WorldGen.SquareWallFrame(k, l, true);
                            NetMessage.SendTileSquare(-1, k, l, 1);
                        }
                        else if (type == ModContent.TileType<Mycelium>())
                        {
                            Main.tile[k, l].type = TileID.Grass;
                            WorldGen.SquareTileFrame(k, l, true);
                            NetMessage.SendTileSquare(-1, k, l, 1);
                        }
                        else if (type == TileID.MushroomGrass)
                        {
                            Main.tile[k, l].type = TileID.JungleGrass;
                            WorldGen.SquareTileFrame(k, l, true);
                            NetMessage.SendTileSquare(-1, k, l, 1);
                        }
                    }
                }
            }
        }
    }
}
