﻿using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Projectiles
{
    public class PonyShot : ModProjectile
    {
        public override string Texture => "AAMod/BlankTex";
        public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Pony Shot");
        }

        bool NoScythes = false;

        public override void SetDefaults()
        {
            projectile.width = 5;
            projectile.height = 5;
            projectile.friendly = true;
            projectile.hostile = false;
            projectile.magic = true;
            projectile.tileCollide = false;
            projectile.ignoreWater = true;
            projectile.penetrate = 1;
            projectile.timeLeft = 90;
            projectile.aiStyle = -1;
            projectile.alpha = 255;
            projectile.damage = 1;
        }

        public override void AI()
        {
            const int aislotHomingCooldown = 0;
            const int homingDelay = 30;
            const float desiredFlySpeedInPixelsPerFrame = 20;
            const float amountOfFramesToLerpBy = 10; // minimum of 1, please keep in full numbers even though it's a float!
            for (int num468 = 0; num468 < 20; num468++)
            {
                float Eggroll = Math.Abs(Main.GameUpdateCount) / 8f;
                float Pie = 1f * (float)Math.Sin(Eggroll);
                int num469 = Dust.NewDust(new Vector2(projectile.Center.X, projectile.Center.Y), 0, 0, ModContent.DustType<Dusts.AbyssDust>(), 0f, 0f, 0, Main.DiscoColor, 1f);
                Main.dust[num469].noGravity = true;
                Main.dust[num469].alpha = 20;
            }
                projectile.ai[aislotHomingCooldown]++;
            if (projectile.ai[aislotHomingCooldown] > homingDelay)
            {
                projectile.ai[aislotHomingCooldown] = homingDelay; 

                int foundTarget = HomeOnTarget();
                if (foundTarget != -1)
                {
                    NPC n = Main.npc[foundTarget];
                    Vector2 desiredVelocity = projectile.DirectionTo(n.Center) * desiredFlySpeedInPixelsPerFrame;
                    projectile.velocity = Vector2.Lerp(projectile.velocity, desiredVelocity, 1f / amountOfFramesToLerpBy);
                }
            }
            if (projectile.timeLeft < 10)
            {
                NoScythes = true;
            }
        }

        private int HomeOnTarget()
        {
            const bool homingCanAimAtWetEnemies = true;
            const float homingMaximumRangeInPixels = 1000;

            int selectedTarget = -1;
            for (int i = 0; i < Main.maxNPCs; i++)
            {
                NPC n = Main.npc[i];
                if (n.CanBeChasedBy(projectile) && (!n.wet || homingCanAimAtWetEnemies))
                {
                    float distance = projectile.Distance(n.Center);
                    if (distance <= homingMaximumRangeInPixels &&
                    (
                        selectedTarget == -1 || //there is no selected target
                        projectile.Distance(Main.npc[selectedTarget].Center) > distance) 
                    )
                    selectedTarget = i;
                }
            }

            return selectedTarget;
        }

        public override void Kill(int timeLeft)
        {
            if (!NoScythes)
            {
                Main.PlaySound(SoundID.Item14, projectile.position);
                Projectile.NewProjectile(projectile.Center.X, projectile.Center.Y, 0, 0, mod.ProjectileType("PonyBoom"), projectile.damage, 0, projectile.owner, 0f, 0f);
            }
        }
    }
}