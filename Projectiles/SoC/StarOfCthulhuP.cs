using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Projectiles.SoC
{
    public class StarOfCthulhuP : ModProjectile
    {
		public int counter = 0;
        public override void SetDefaults()
        {
            projectile.CloneDefaults(106);
			projectile.melee = false;
			projectile.thrown = true;
            projectile.penetrate = -1;  
            projectile.width = 68;
            projectile.height = 70;
			projectile.aiStyle = 3;
			projectile.scale = 0.75f;
			projectile.tileCollide = false;
			aiType = 106;
        }

		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Star of Cthulhu");
		}
		
		public override void AI()
		{
			counter++;
			if (Main.rand.Next(2) == 0)
			{
				Dust dust = Dust.NewDustDirect(projectile.position, projectile.height, projectile.width, 18,
				projectile.velocity.X * .5f, projectile.velocity.Y * .5f, 200, Scale: 1.1f);
				dust.velocity += projectile.velocity * 0.4f;
				dust.velocity *= 0.3f;
			}
			for (int i = 0; i < 200; i++)
            {
                NPC target = Main.npc[i];

                float shootToX = target.position.X + target.width * 0.5f - projectile.Center.X;
                float shootToY = target.position.Y + target.height * 0.5f - projectile.Center.Y;
                float distance = (float)Math.Sqrt(shootToX * shootToX + shootToY * shootToY);

                if (distance < 350f && target.catchItem == 0 && !target.friendly && target.active)
                {
                    if (counter >= 5)
                    {
                        distance = 1.6f / distance;
                        shootToX *= distance * 3;
                        shootToY *= distance * 3;
						Main.PlaySound(SoundID.Item17, projectile.position);
                        int P = Projectile.NewProjectile(projectile.Center.X, projectile.Center.Y, shootToX*2, shootToY*2, 55, projectile.damage, 0, Main.myPlayer, 0f, 0f);
						Main.projectile[P].hostile = false;
						Main.projectile[P].friendly = true;
						Main.projectile[P].thrown = true;
						Main.projectile[P].penetrate = 1;
						Main.projectile[P].localNPCHitCooldown = 1;
                        counter = 0;
                    }
                }
            }
		}
		
		public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
		{
			target.immune[projectile.owner] = 1;
		}
    }
}
