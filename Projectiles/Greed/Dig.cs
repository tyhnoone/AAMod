﻿using Terraria;
using Terraria.ModLoader;

namespace AAMod.Projectiles.Greed
{
    public class Dig : ModProjectile
    {

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Gold Digger");
        }

        public override void SetDefaults()
        {
            projectile.width = 24;
            projectile.height = 24;
            projectile.friendly = true;
            projectile.aiStyle = -1;
            projectile.magic = true;
            projectile.penetrate = -1;
            projectile.ignoreWater = true;
            projectile.tileCollide = true;
        }

        public override void AI()
        {
            if (projectile.alpha > 0)
            {
                projectile.alpha -= 12;
            }
            if (projectile.alpha < 0)
            {
                projectile.alpha = 0;
            }
            if (projectile.ai[0] == 0f)
            {
                projectile.ai[1] += 1f;
                if (projectile.ai[1] >= 45f)
                {
                    float num975 = 0.98f;
                    float num976 = 0.35f;
                    projectile.ai[1] = 45f;
                    projectile.velocity.X = projectile.velocity.X * num975;
                    projectile.velocity.Y = projectile.velocity.Y + num976;
                }
                projectile.rotation = projectile.velocity.ToRotation() + 0.785f; //1.57079637f;
            }
        }

        public override void Kill(int timeLeft)
        {
            Projectile.NewProjectile(projectile.position.X, projectile.position.Y, 0, 0, mod.ProjectileType("CoinFountain"), projectile.damage, 1);
        }
    }
}