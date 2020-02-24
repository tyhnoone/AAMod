﻿using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using Microsoft.Xna.Framework;

namespace AAMod.Projectiles
{
    public class FreedomShotCharged : ModProjectile
    {
        private bool firstHit = true;

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Freedom Charged Shot");
            Main.projFrames[projectile.type] = 3;
        }

        public override void SetDefaults()
        {
            projectile.width = 28;
            projectile.height = 28;
            projectile.friendly = true;
            projectile.ranged = true;
            projectile.ignoreWater = true;
            projectile.extraUpdates = 2;
        }

        public override void AI()
        {
			if (Main.rand.Next(2) == 0)
			{
				Dust dust = Dust.NewDustDirect(projectile.position, projectile.height, projectile.width, 74,
					projectile.velocity.X, projectile.velocity.Y, 200, Scale: 1f);
				dust.velocity += projectile.velocity * 0.3f;
				dust.velocity *= 0.2f;
			}
			if (++projectile.frameCounter >= 5)
            {
                projectile.frameCounter = 0;
                if (++projectile.frame >= 2)
                {
                    projectile.frame = 0;
                }
            }
            projectile.rotation = projectile.velocity.ToRotation(); // projectile faces sprite right
        }

        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            if (firstHit && projectile.owner == Main.myPlayer)
            {
                Projectile.NewProjectile(projectile.Center, new Vector2(0, 0), mod.ProjectileType("FreedomBall"), projectile.damage, 0f, projectile.owner);
                firstHit = false;
            }
        }

        public override void Kill(int timeLeft)
		{
			Main.PlaySound(SoundID.DD2_ExplosiveTrapExplode, projectile.position);
			int p = Projectile.NewProjectile(projectile.Center.X, projectile.Center.Y, 0f, 0f, mod.ProjectileType("DummyExplosionTerra"), projectile.damage, 0, Main.myPlayer);
			Main.projectile[p].magic = false;
			Main.projectile[p].ranged = true;
			for (int index1 = 0; index1 < 30; ++index1)
			{
				int index2 = Dust.NewDust(new Vector2(projectile.position.X, projectile.position.Y), projectile.width, projectile.height, 74, 0.0f, 0.0f, 100, new Color(), 1f);
				Main.dust[index2].velocity *= 1.1f;
				Main.dust[index2].scale *= 0.99f;
			}
		}
    }
}
