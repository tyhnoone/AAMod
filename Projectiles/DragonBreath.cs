﻿using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Projectiles
{
    public class DragonBreath : ModProjectile
    {
        public override string Texture => "AAMod/BlankTex";
        public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Dragon Breath");
		}
    	
        public override void SetDefaults()
        {
            projectile.width = 12;
            projectile.height = 12;
            projectile.friendly = true;
            projectile.ignoreWater = true;
            projectile.ranged = true;
            projectile.penetrate = -1;
            projectile.extraUpdates = 3;
            projectile.timeLeft = 45;
        }

        public override void AI()
        {
        	Lighting.AddLight(projectile.Center, 0f, (255 - projectile.alpha) * 0.2f / 255f, (255 - projectile.alpha) * 0f / 255f);
			if (projectile.timeLeft > 45)
			{
				projectile.timeLeft = 45;
			}
			if (projectile.ai[0] > 7f)
			{
				float num296 = 1f;
				if (projectile.ai[0] == 8f)
				{
					num296 = 0.25f;
				}
				else if (projectile.ai[0] == 9f)
				{
					num296 = 0.5f;
				}
				else if (projectile.ai[0] == 10f)
				{
					num296 = 0.75f;
				}
				projectile.ai[0] += 1f;
				int num297 = ModContent.DustType<Dusts.InfinityOverloadG>();
				if (Main.rand.Next(2) == 0)
				{
					for (int num298 = 0; num298 < 2; num298++)
					{
						int num299 = Dust.NewDust(new Vector2(projectile.position.X, projectile.position.Y), projectile.width, projectile.height, num297, projectile.velocity.X * 0.2f, projectile.velocity.Y * 0.2f, 100, new Color(Main.DiscoR, Main.DiscoG, Main.DiscoB), 1.25f);
						if (num297 == 66 && Main.rand.Next(3) == 0)
						{
							Main.dust[num299].noGravity = true;
							Main.dust[num299].scale *= 2f;
							Dust expr_DBEF_cp_0 = Main.dust[num299];
							expr_DBEF_cp_0.velocity.X *= 2f;
							Dust expr_DC0F_cp_0 = Main.dust[num299];
							expr_DC0F_cp_0.velocity.Y *= 2f;
						}
						else
						{
							Main.dust[num299].noGravity = true;
							Main.dust[num299].scale *= 0.8f;
						}
						Dust expr_DC74_cp_0 = Main.dust[num299];
						expr_DC74_cp_0.velocity.X *= 1.2f;
						Dust expr_DC94_cp_0 = Main.dust[num299];
						expr_DC94_cp_0.velocity.Y *= 1.2f;
						Main.dust[num299].scale *= num296;
                        Main.dust[num299].velocity += projectile.velocity;
                    }
				}
			}
			else
			{
				projectile.ai[0] += 1f;
			}
			projectile.rotation += 0.3f * projectile.direction;
			return;	
        }

        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
        	target.AddBuff(BuffID.OnFire, 240);
        }
    }
}