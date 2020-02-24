﻿using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Projectiles.AH
{
    internal class FireMagic : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Fire Magic");
            Main.projFrames[projectile.type] = 4;
        }

        public override void SetDefaults()
        {

            projectile.width = 45;
            projectile.height = 40;
            projectile.friendly = true;
            projectile.hostile = false;
			projectile.magic = true;
            projectile.scale = 1.1f;
            projectile.ignoreWater = true;
            projectile.penetrate = 1;
            projectile.alpha = 0;
            projectile.timeLeft = 255;
            projectile.tileCollide = false;
        }

        public override Color? GetAlpha(Color lightColor)
        {
            return Color.White;
        }

        public override void AI()
        {
            projectile.velocity *= .98f;
            if (projectile.timeLeft > 0 && projectile.velocity == new Vector2(0, 0))
            {
                projectile.Kill();
            }
            projectile.frameCounter++;
            if (projectile.frameCounter > 6)
            {
                projectile.frame++;
                projectile.frameCounter = 0;
                if (projectile.frame > 3)
                {
                    projectile.frame = 0;
                }
            }
            for (int num189 = 0; num189 < 1; num189++)
            {
                int num190 = Dust.NewDust(new Vector2(projectile.position.X, projectile.position.Y), projectile.width, projectile.height, ModContent.DustType<Dusts.AkumaDust>(), 0f, 0f, 0);

                Main.dust[num190].scale *= 1.3f;
                Main.dust[num190].fadeIn = 1f;
                Main.dust[num190].noGravity = true;
            }
        }
		
		public override void Kill (int timeLeft)
		{
			Main.PlaySound(new Terraria.Audio.LegacySoundStyle(2, 124, Terraria.Audio.SoundType.Sound));
			float spread = 12f * 0.0174f;
			double startAngle = Math.Atan2(projectile.velocity.X, projectile.velocity.Y) - spread / 2;
			double deltaAngle = spread / 4;
			for (int i = 0; i < 4; i++)
			{
				double offsetAngle = startAngle + deltaAngle * (i + i * i) / 2f + 32f * i;
				Projectile.NewProjectile(projectile.Center.X, projectile.Center.Y, (float)(Math.Sin(offsetAngle) * 3f), (float)(Math.Cos(offsetAngle) * 3f), mod.ProjectileType("Ash"), projectile.damage / 6, projectile.knockBack, projectile.owner, 0f, 0f);
				Projectile.NewProjectile(projectile.Center.X, projectile.Center.Y, (float)(-Math.Sin(offsetAngle) * 3f), (float)(-Math.Cos(offsetAngle) * 3f), mod.ProjectileType("Ash"), projectile.damage / 6, projectile.knockBack, projectile.owner, 0f, 0f);
			}
		}
		
		public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            Projectile.NewProjectile(projectile.Center, Vector2.Zero, ModContent.ProjectileType<MagicBoom>(), projectile.damage, projectile.knockBack, projectile.owner, 0, 0);
            Main.PlaySound(SoundID.Item14, projectile.position);
            target.AddBuff(mod.BuffType("DragonFire"), 600);
            projectile.active = false;
		}
    }
}