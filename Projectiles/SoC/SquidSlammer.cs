using Microsoft.Xna.Framework;
using ReLogic.Utilities;
using System;
using System.Collections.Generic;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.Enums;
using Terraria.GameContent;
using Terraria.GameContent.Achievements;
using Terraria.GameContent.Events;
using Terraria.GameContent.Shaders;
using Terraria.GameContent.UI;
using Terraria.Graphics.Effects;
using Terraria.Graphics.Shaders;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.Utilities;
using Terraria.World.Generation;
using Terraria.ModLoader;

namespace AAMod.Projectiles.SoC
{
    public class SquidSlammer : ModProjectile
    {
        public override void SetDefaults()
        {
			projectile.width = 64;
			projectile.height = 64;
			projectile.aiStyle = 140;
			projectile.friendly = true;
			projectile.melee = true;
			projectile.penetrate = -1;
			projectile.alpha = 255;
			projectile.hide = true;
			projectile.tileCollide = false;
			projectile.ignoreWater = true;
			projectile.usesLocalNPCImmunity = true;
			projectile.localNPCHitCooldown = 3;
			projectile.ownerHitCheck = true;
        }

		public override void AI()
		{
			float num = 50f;
			float num2 = 2f;
			float scaleFactor = 20f;
			Player player = Main.player[projectile.owner];
			float num3 = -0.7853982f;
			Vector2 value = player.RotatedRelativePoint(player.MountedCenter, true);
			Vector2 value2 = Vector2.Zero;
			if (player.dead)
			{
				projectile.Kill();
				return;
			}
			
			int num4 = projectile.damage * 2;
			int num5 = Math.Sign(projectile.velocity.X);
			projectile.velocity = new Vector2((float)num5, 0f);
			if (projectile.ai[0] == 0f)
			{
				projectile.rotation = new Vector2((float)num5, -player.gravDir).ToRotation() + num3 + 3.14159274f;
				if (projectile.velocity.X < 0f)
				{
					projectile.rotation -= 1.57079637f;
				}
			}
			projectile.alpha -= 128;
			if (projectile.alpha < 0)
			{
				projectile.alpha = 0;
			}
			float arg_2E4_0 = projectile.ai[0] / num;
			float num6 = 1f;
			projectile.ai[0] += num6;
			projectile.rotation += 6.28318548f * num2 / num * (float)num5;
			bool flag = projectile.ai[0] == (float)((int)(num / 2f));
			if (projectile.ai[0] >= num || (flag && !player.controlUseItem))
			{
				projectile.Kill();
				player.reuseDelay = 10;
			}
			else if (flag)
			{
				Vector2 mouseWorld = Main.MouseWorld;
				int num7 = (player.DirectionTo(mouseWorld).X > 0f) ? 1 : -1;
				if ((float)num7 != projectile.velocity.X)
				{
					player.ChangeDir(num7);
					projectile.velocity = new Vector2((float)num7, 0f);
					projectile.netUpdate = true;
					projectile.rotation -= 3.14159274f;
				}
			}
			float num8 = projectile.rotation - 0.7853982f * (float)num5;
			value2 = (num8 + ((num5 == -1) ? 3.14159274f : 0f)).ToRotationVector2() * (projectile.ai[0] / num) * scaleFactor;
			Vector2 vector = projectile.Center + (num8 + ((num5 == -1) ? 3.14159274f : 0f)).ToRotationVector2() * 30f;
			if (Main.rand.Next(2) == 0)
			{
				Dust dust = Dust.NewDustDirect(vector - new Vector2(5f), 10, 10, 31, player.velocity.X, player.velocity.Y, 150, default(Color), 1f);
				dust.velocity = projectile.DirectionTo(dust.position) * 0.1f + dust.velocity * 0.1f;
			}
			if (arg_2E4_0 >= 0.75f)
			{
				Dust dust2 = Dust.NewDustDirect(vector - new Vector2(5f), 10, 10, 55, player.velocity.X, player.velocity.Y, 50, default(Color), 1f);
				dust2.velocity = projectile.DirectionTo(dust2.position) * 0.1f + dust2.velocity * 0.1f;
				dust2.noGravity = true;
				dust2.color = new Color(20, 255, 100, 160);
			}
			if (projectile.ai[0] >= num - 8f && projectile.ai[0] < num - 2f)
			{
				for (int i = 0; i < 5; i++)
				{
					Dust expr_3F7 = Dust.NewDustDirect(vector - new Vector2(5f), 10, 10, 55, player.velocity.X, player.velocity.Y, 50, default(Color), 1f);
					expr_3F7.velocity *= 1.2f;
					expr_3F7.noGravity = true;
					expr_3F7.scale += 0.1f;
					expr_3F7.color = new Color(20, 255, 100, 160);
				}
			}
			if (projectile.ai[0] == num - 3f && projectile.owner == Main.myPlayer)
			{
				Point point;
				Vector2 vector12 = Main.screenPosition + new Vector2((float)Main.mouseX, (float)Main.mouseY);
				Vector2 vector2 = player.RotatedRelativePoint(player.MountedCenter, true);
				float num75 = 10f;
				float num119 = vector12.Y;
				if (num119 > player.Center.Y - 200f)
				{
					num119 = player.Center.Y - 200f;
				}
				for (int num120 = 0; num120 < 5; num120++)
				{
					vector2 = player.Center + new Vector2((float)(-(float)Main.rand.Next(0, 401) * player.direction), -600f);
					vector2.Y -= (float)(100 * num120);
					Vector2 vector13 = vector12 - vector2;
					if (vector13.Y < 0f)
					{
						vector13.Y *= -1f;
					}
					if (vector13.Y < 20f)
					{
						vector13.Y = 20f;
					}
				vector13.Normalize();
				vector13 *= num75;
				float num82 = vector13.X;
				float num83 = vector13.Y;
				float speedX5 = num82;
				float speedY6 = num83 + (float)Main.rand.Next(-40, 41) * 0.02f;
				Projectile.NewProjectile(vector2.X, vector2.Y, speedX5, speedY6, mod.ProjectileType("CthulhuBomb"), projectile.damage*4, projectile.knockBack, projectile.owner);
				}
				if (projectile.localAI[1] == 1f || WorldUtils.Find(vector.ToTileCoordinates(), Searches.Chain(new Searches.Down(4), new GenCondition[]
				{
					new Conditions.IsSolid()
				}), out point))
				{
					Projectile.NewProjectile(vector + new Vector2((float)(num5 * 20), -60f), Vector2.Zero, 698, num4, 0f, projectile.owner, 0f, 0f);
                    Projectile.NewProjectile(vector + new Vector2((float)(num5 * 20), -60f), Vector2.Zero, mod.ProjectileType<SquidSlam>(), num4, 0f, projectile.owner, 0f, 0f);
                    Main.PlayTrackedSound(SoundID.DD2_MonkStaffGroundImpact, projectile.Center);
				}
				else
				{
					Main.PlayTrackedSound(SoundID.DD2_MonkStaffGroundMiss, projectile.Center);
				}
			}
		}

		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Squid Slammer");
		}
    }
}
