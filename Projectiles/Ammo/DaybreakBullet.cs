using Terraria.ID;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace AAMod.Projectiles.Ammo
{
    public class DaybreakBullet : ModProjectile
    {
        
        public override void SetDefaults()
        {
            projectile.width = 4;
            projectile.height = 4;
            projectile.aiStyle = 1;
            projectile.friendly = true;
            projectile.penetrate = 1;
            projectile.light = 0.5f;
            projectile.alpha = 30;
            projectile.extraUpdates = 2;
            projectile.scale = 1.3f;
            projectile.timeLeft = 600;
            projectile.ranged = true;
            aiType = ProjectileID.Bullet;
        }

		public override void SetStaticDefaults()
		{
		    DisplayName.SetDefault("Antimatter");
		}

        public override void AI()
        {
            Lighting.AddLight(projectile.Center, .1f, .5f, 1f);
        }

        public override void Kill(int timeLeft)
        {
            Main.PlaySound(SoundID.Item14, projectile.position);
            for (int num565 = 0; num565 < 7; num565++)
            {
                Dust.NewDust(new Vector2(projectile.position.X, projectile.position.Y), projectile.width, projectile.height, ModContent.DustType<Dusts.AkumaDust>(), 0f, 0f, 100, default, 1.5f);
            }
            for (int num566 = 0; num566 < 3; num566++)
            {
                int num567 = Dust.NewDust(new Vector2(projectile.position.X, projectile.position.Y), projectile.width, projectile.height, ModContent.DustType<Dusts.AkumaADust>(), 0f, 0f, 100);
                Main.dust[num567].noGravity = true;
                Main.dust[num567].velocity *= 3f;
                num567 = Dust.NewDust(new Vector2(projectile.position.X, projectile.position.Y), projectile.width, projectile.height, ModContent.DustType<Dusts.AkumaADust>(), 0f, 0f, 100);
                Main.dust[num567].velocity *= 2f;
            }
            int num568 = Gore.NewGore(new Vector2(projectile.position.X - 10f, projectile.position.Y - 10f), default, Main.rand.Next(61, 64), 1f);
            Main.gore[num568].velocity *= 0.3f;
            Gore expr_12836_cp_0 = Main.gore[num568];
            expr_12836_cp_0.velocity.X += Main.rand.Next(-10, 11) * 0.05f;
            Gore expr_12866_cp_0 = Main.gore[num568];
            expr_12866_cp_0.velocity.Y += Main.rand.Next(-10, 11) * 0.05f;
            if (projectile.owner == Main.myPlayer)
            {
                projectile.localAI[1] = -1f;
                projectile.maxPenetrate = 0;
                projectile.position.X = projectile.position.X + (projectile.width / 2);
                projectile.position.Y = projectile.position.Y + (projectile.height / 2);
                projectile.width = 120;
                projectile.height = 120;
                projectile.position.X = projectile.position.X - (projectile.width / 2);
                projectile.position.Y = projectile.position.Y - (projectile.height / 2);
                projectile.Damage();
            }
        }




        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            target.immune[projectile.owner] = 5;
            { }
            target.AddBuff(BuffID.Daybreak, 200);
            int proj = Projectile.NewProjectile(projectile.Center.X, projectile.Center.Y, projectile.velocity.X, projectile.velocity.Y, mod.ProjectileType("FireProjBoom"), projectile.damage / 6, projectile.knockBack, projectile.owner, 0f, 0f);
            Main.projectile[proj].melee = false;
            Main.projectile[proj].ranged = true;

        }
    }
}
