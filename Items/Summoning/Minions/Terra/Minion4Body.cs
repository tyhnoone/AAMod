﻿using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ModLoader;

namespace AAMod.Items.Summoning.Minions.Terra
{
    public class Minion4Body : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Terra Weaver");
        }

        public override void SetDefaults()
        {
            projectile.width = 24;
            projectile.height = 24;
            projectile.penetrate = -1;
            projectile.timeLeft *= 5;
            projectile.minion = true;
            projectile.friendly = true;
            projectile.ignoreWater = true;
            projectile.tileCollide = false;
            projectile.alpha = 255;
            projectile.netImportant = true;
            projectile.minionSlots = 0;
            projectile.hide = true;
        }

        public override Color? GetAlpha(Color lightColor)
        {
            return Color.White;
        }

        public override void DrawBehind(int index, List<int> drawCacheProjsBehindNPCsAndTiles, List<int> drawCacheProjsBehindNPCs, List<int> drawCacheProjsBehindProjectiles,
            List<int> drawCacheProjsOverWiresUI)
        {
            drawCacheProjsBehindProjectiles.Add(index);
        }

        public override bool PreDraw(SpriteBatch spriteBatch, Color lightColor)
        {
            Texture2D texture2D13 = Main.projectileTexture[projectile.type];
            int num214 = Main.projectileTexture[projectile.type].Height / Main.projFrames[projectile.type];
            int y6 = num214 * projectile.frame;
            Main.spriteBatch.Draw(texture2D13, projectile.Center - Main.screenPosition + new Vector2(0f, projectile.gfxOffY), new Rectangle(0, y6, texture2D13.Width, num214),
                projectile.GetAlpha(Color.White), projectile.rotation, new Vector2(texture2D13.Width / 2f, num214 / 2f), projectile.scale,
                projectile.spriteDirection == 1 ? SpriteEffects.None : SpriteEffects.FlipHorizontally, 0f);
            return false;
        }

		public override bool TileCollideStyle(ref int width, ref int height, ref bool fallThrough)
		{
			fallThrough = true;
			return false;
		}
		
		public override bool OnTileCollide(Vector2 oldVelocity)
		{
			return false;
		}
		
        public override void AI()
        {
			projectile.tileCollide = false;
            Player player = Main.player[projectile.owner];
            AAPlayer modPlayer = player.GetModPlayer<AAPlayer>(mod);

            if ((int) Main.time % 120 == 0) projectile.netUpdate = true;
            if (!player.active)
            {
                projectile.active = false;
                return;
            }

            int num1038 = 10;
			if (modPlayer.TerraSummon)
			{
				projectile.timeLeft = 2;
			}
            if (player.dead) modPlayer.TerraSummon = false;
            if (modPlayer.TerraSummon) projectile.timeLeft = 2;
            num1038 = 30;

            bool flag67 = false;
            Vector2 value67 = Vector2.Zero;
            Vector2 arg_2D865_0 = Vector2.Zero;
            float num1052 = 0f;
            float scaleFactor16 = 0f;
            float scaleFactor17 = 1f;
            if (projectile.ai[1] == 1f)
            {
                projectile.ai[1] = 0f;
                projectile.netUpdate = true;
            }

            int byUUID = Projectile.GetByUUID(projectile.owner, (int) projectile.ai[0]);
            if (byUUID >= 0 && Main.projectile[byUUID].active)
            {
                flag67 = true;
                value67 = Main.projectile[byUUID].Center;
                num1052 = Main.projectile[byUUID].rotation;
                float num1053 = MathHelper.Clamp(Main.projectile[byUUID].scale, 0f, 50f);
                scaleFactor17 = num1053;
                scaleFactor16 = 16f;
                Main.projectile[byUUID].localAI[0] = projectile.localAI[0] + 1f;
                if (Main.projectile[byUUID].type != mod.ProjectileType("Minion4Head")) Main.projectile[byUUID].localAI[1] = projectile.whoAmI;
            }

            if (!flag67) return;
            if (projectile.alpha > 0)
                for (int num1054 = 0; num1054 < 2; num1054++)
                {
                    int num1055 = Dust.NewDust(projectile.position, projectile.width, projectile.height, mod.DustType<Dusts.SummonDust>(), 0f, 0f, 100, default, 2f);
                    Main.dust[num1055].noGravity = true;
                    Main.dust[num1055].noLight = true;
                }

            projectile.alpha -= 42;
            if (projectile.alpha < 0) projectile.alpha = 0;
            projectile.velocity = Vector2.Zero;
            Vector2 vector134 = value67 - projectile.Center;
            if (num1052 != projectile.rotation)
            {
                float num1056 = MathHelper.WrapAngle(num1052 - projectile.rotation);
                vector134 = vector134.RotatedBy(num1056 * 0.1f, default);
            }

            projectile.rotation = vector134.ToRotation() + 1.57079637f;
            projectile.position = projectile.Center;
            projectile.scale = scaleFactor17;
            projectile.width = projectile.height = (int) (num1038 * projectile.scale);
            projectile.Center = projectile.position;
            if (vector134 != Vector2.Zero) projectile.Center = value67 - Vector2.Normalize(vector134) * scaleFactor16 * scaleFactor17;
            projectile.spriteDirection = vector134.X > 0f ? 1 : -1;
        }

        public override void Kill(int timeLeft)
        {
            Player player = Main.player[projectile.owner];
            if (player.slotsMinions + projectile.minionSlots > player.maxMinions && projectile.owner == Main.myPlayer)
            {
                int byUUID = Projectile.GetByUUID(projectile.owner, projectile.ai[0]);
                if (byUUID != -1)
                {
                    Projectile projectile1 = Main.projectile[byUUID];
                    if (projectile1.type != mod.ProjectileType("Minion4Head")) projectile1.localAI[1] = projectile.localAI[1];
                    projectile1 = Main.projectile[(int) projectile.localAI[1]];
                    projectile1.ai[0] = projectile.ai[0];
                    projectile1.ai[1] = 1f;
                    projectile1.netUpdate = true;
                }
            }
        }
    }
}