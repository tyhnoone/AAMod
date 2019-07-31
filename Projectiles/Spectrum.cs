﻿using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Projectiles
{
    public class Spectrum : ModProjectile
    {
    	public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Crystal");
			Main.projFrames[projectile.type] = 5;
		}
    	
        public override void SetDefaults()
        {
            projectile.width = 22;
            projectile.height = 26;
            projectile.friendly = true;
			projectile.penetrate = -1;
			projectile.tileCollide = false;
			projectile.magic = true;
			projectile.ignoreWater = true;
        }

        public override void AI()
        {
			projectile.frameCounter++;
			int num28 = (projectile.ai[0] < 180f) ? 6 : 1;
			if (projectile.frameCounter >= num28)
			{
				projectile.frameCounter = 0;
				if (++projectile.frame >= 5)
				{
					projectile.frame = 0;
				}
			}
        	Player player = Main.player[projectile.owner];
        	float num = 1.57079637f;
        	Vector2 vector = player.RotatedRelativePoint(player.MountedCenter, true);
        	float num26 = 30f;
			if (projectile.ai[0] > 90f)
			{
				num26 = 15f;
			}
			if (projectile.ai[0] > 120f)
			{
				num26 = 5f;
			}
			projectile.damage = (int)(player.inventory[player.selectedItem].damage * player.magicDamage);
			projectile.ai[0] += 1f;
			projectile.ai[1] += 1f;
			bool flag9 = false;
			if (projectile.ai[0] % num26 == 0f)
			{
				flag9 = true;
			}
			int num27 = 10;
			bool flag10 = false;
			if (projectile.ai[0] % num26 == 0f)
			{
				flag10 = true;
			}
			if (projectile.ai[1] >= 1f)
			{
				projectile.ai[1] = 0f;
				flag10 = true;
				if (Main.myPlayer == projectile.owner)
				{
					float scaleFactor5 = player.inventory[player.selectedItem].shootSpeed * projectile.scale;
					Vector2 value12 = vector;
					Vector2 value13 = Main.screenPosition + new Vector2(Main.mouseX, Main.mouseY) - value12;
					if (player.gravDir == -1f)
					{
						value13.Y = Main.screenHeight - Main.mouseY + Main.screenPosition.Y - value12.Y;
					}
					Vector2 vector11 = Vector2.Normalize(value13);
					if (float.IsNaN(vector11.X) || float.IsNaN(vector11.Y))
					{
						vector11 = -Vector2.UnitY;
					}
					vector11 = Vector2.Normalize(Vector2.Lerp(vector11, Vector2.Normalize(projectile.velocity), 0.92f));
					vector11 *= scaleFactor5;
					if (vector11.X != projectile.velocity.X || vector11.Y != projectile.velocity.Y)
					{
						projectile.netUpdate = true;
					}
					projectile.velocity = vector11;
				}
			}
			if (projectile.soundDelay <= 0)
			{
				projectile.soundDelay = num27;
				projectile.soundDelay *= 2;
				if (projectile.ai[0] != 1f)
				{
					Main.PlaySound(SoundID.Item15, projectile.position);
				}
			}
			if (flag10 && Main.myPlayer == projectile.owner)
			{
				bool flag11 = !flag9 || player.CheckMana(player.inventory[player.selectedItem].mana, true, false);
				bool flag12 = player.channel && flag11 && !player.noItems && !player.CCed;
				if (flag12)
				{
					if (projectile.ai[0] == 1f)
					{
						Vector2 center3 = projectile.Center;
						Vector2 vector12 = Vector2.Normalize(projectile.velocity);
						if (float.IsNaN(vector12.X) || float.IsNaN(vector12.Y))
						{
							vector12 = -Vector2.UnitY;
						}
						int num29 = projectile.damage;
						for (int l = 0; l < 7; l++)
						{
							Projectile.NewProjectile(center3.X, center3.Y, vector12.X, vector12.Y, mod.ProjectileType("SpectrumBeam"), num29, projectile.knockBack, projectile.owner, l, projectile.whoAmI);
						}
						projectile.netUpdate = true;
					}
				}
				else
				{
					projectile.Kill();
				}
			}
			projectile.position = player.RotatedRelativePoint(player.MountedCenter, true) - projectile.Size / 2f;
			projectile.rotation = projectile.velocity.ToRotation() + num;
			projectile.spriteDirection = projectile.direction;
			projectile.timeLeft = 2;
			player.ChangeDir(projectile.direction);
			player.heldProj = projectile.whoAmI;
			player.itemTime = 2;
			player.itemAnimation = 2;
			player.itemRotation = (float)Math.Atan2(projectile.velocity.Y * projectile.direction, projectile.velocity.X * projectile.direction);
        }
        
        public override bool PreDraw(SpriteBatch spriteBatch, Color lightColor)
        {
        	Vector2 mountedCenter = Main.player[projectile.owner].MountedCenter;
            Color color25 = Lighting.GetColor((int)(projectile.position.X + projectile.width * 0.5) / 16, (int)((projectile.position.Y + projectile.height * 0.5) / 16.0));
			if (projectile.hide && !ProjectileID.Sets.DontAttachHideToAlpha[projectile.type])
			{
				color25 = Lighting.GetColor((int)mountedCenter.X / 16, (int)(mountedCenter.Y / 16f));
			}
			SpriteEffects spriteEffects = SpriteEffects.None;
			if (projectile.spriteDirection == -1)
			{
				spriteEffects = SpriteEffects.FlipHorizontally;
			}
        	Texture2D texture2D14 = Main.projectileTexture[projectile.type];
			int num215 = Main.projectileTexture[projectile.type].Height / Main.projFrames[projectile.type];
			int y7 = num215 * projectile.frame;
			Vector2 vector27 = (projectile.position + new Vector2(projectile.width, projectile.height) / 2f + Vector2.UnitY * projectile.gfxOffY - Main.screenPosition).Floor();
			float scale5 = 1f;
			if (Main.player[projectile.owner].shroomiteStealth && Main.player[projectile.owner].inventory[Main.player[projectile.owner].selectedItem].ranged)
			{
				float num216 = Main.player[projectile.owner].stealth;
				if (num216 < 0.03)
				{
					num216 = 0.03f;
				}
				float arg_97B3_0 = (1f + num216 * 10f) / 11f;
				color25 *= num216;
				scale5 = num216;
			}
			if (Main.player[projectile.owner].setVortex && Main.player[projectile.owner].inventory[Main.player[projectile.owner].selectedItem].ranged)
			{
				float num217 = Main.player[projectile.owner].stealth;
				if (num217 < 0.03)
				{
					num217 = 0.03f;
				}
				float arg_9854_0 = (1f + num217 * 10f) / 11f;
				color25 = color25.MultiplyRGBA(new Color(Vector4.Lerp(Vector4.One, new Vector4(0.16f, 0.12f, 0f, 0f), 1f - num217)));
				scale5 = num217;
			}
			Main.spriteBatch.Draw(texture2D14, vector27, new Microsoft.Xna.Framework.Rectangle?(new Rectangle(0, y7, texture2D14.Width, num215)), projectile.GetAlpha(color25), projectile.rotation, new Vector2(texture2D14.Width / 2f, num215 / 2f), projectile.scale, spriteEffects, 0f);
        	float scaleFactor2 = (float)Math.Cos(6.28318548f * (projectile.ai[0] / 30f)) * 2f + 2f;
			if (projectile.ai[0] > 120f)
			{
				scaleFactor2 = 4f;
			}
			for (float num218 = 0f; num218 < 4f; num218 += 1f)
			{
				Main.spriteBatch.Draw(texture2D14, vector27 + Vector2.UnitY.RotatedBy(num218 * 6.28318548f / 4f, default) * scaleFactor2, new Microsoft.Xna.Framework.Rectangle?(new Rectangle(0, y7, texture2D14.Width, num215)), projectile.GetAlpha(color25).MultiplyRGBA(new Color(255, 255, 255, 0)) * 0.03f, projectile.rotation, new Vector2(texture2D14.Width / 2f, num215 / 2f), projectile.scale, spriteEffects, 0f);
			}
			return false;
        }

        public override Color? GetAlpha(Color lightColor)
        {
        	return Color.White;
        }
    }
}