using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.ModLoader;

namespace AAMod.NPCs.Bosses.SoC
{
    internal class CthulhuNuke : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            Main.projFrames[projectile.type] = 4;
            DisplayName.SetDefault("Reality Bomb");
        }

        public override void SetDefaults()
        {
            projectile.width = 10;
            projectile.height = 10;
            projectile.friendly = false;
            projectile.hostile = true;
            projectile.scale = 1.1f;
            projectile.ignoreWater = true;
            projectile.tileCollide = true;
            projectile.penetrate = 1;
            projectile.alpha = 60;
            projectile.timeLeft = 300;
        }

        public override void AI()
        {
            if (projectile.timeLeft > 0)
            {
                projectile.timeLeft--;
            }
            if (projectile.timeLeft == 0)
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
            projectile.rotation = (float)Math.Atan2(projectile.velocity.Y, projectile.velocity.X) + 1.57f;
            
            for (int num189 = 0; num189 < 1; num189++)
            {
                int num190 = Dust.NewDust(new Vector2(projectile.position.X, projectile.position.Y), projectile.width, projectile.height, mod.DustType<Dusts.CthulhuDust>(), 0f, 0f, 0, default(Color), 1f);

                Main.dust[num190].scale *= 1.3f;
                Main.dust[num190].fadeIn = 1f;
                Main.dust[num190].noGravity = true;
            }
        }

        private bool TileHit = false;

        private bool PlayerHit = false;

        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            TileHit = true;
            return true;
        }

        public override void OnHitPlayer(Player target, int damage, bool crit)
        {
            PlayerHit = true;
        }


        public override void Kill(int timeleft)
        {
            for (int num468 = 0; num468 < 20; num468++)
            {
                int num469 = Dust.NewDust(new Vector2(projectile.Center.X, projectile.Center.Y), projectile.width, projectile.height, mod.DustType<Dusts.CthulhuDust>(), -projectile.velocity.X * 0.2f,
                    -projectile.velocity.Y * 0.2f, 0, default(Color), 1f);
                Main.dust[num469].noGravity = true;
                Main.dust[num469].velocity *= 2f;
                num469 = Dust.NewDust(new Vector2(projectile.Center.X, projectile.Center.Y), projectile.width, projectile.height, mod.DustType<Dusts.CthulhuDust>(), -projectile.velocity.X * 0.2f,
                    -projectile.velocity.Y * 0.2f, 0, default(Color), 1f);
                Main.dust[num469].velocity *= 2f;
            }
            if (TileHit)
            {
                Projectile.NewProjectile(projectile.Center.X, projectile.Center.Y + 20, projectile.velocity.X, projectile.velocity.Y, mod.ProjectileType("CthulhuStrike"), projectile.damage, 1, 255);
            }
            if (PlayerHit)
            {
                Projectile.NewProjectile(projectile.Center.X, projectile.Center.Y + 20, projectile.velocity.X, projectile.velocity.Y, mod.ProjectileType("Cthulhusplosion"), projectile.damage, 1, 255);
            }
            
        }
    }
}