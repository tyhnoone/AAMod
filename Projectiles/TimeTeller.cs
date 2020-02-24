using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Projectiles   //The directory for your .cs and .png; Example: TutorialMOD/Projectiles
{
    public class TimeTeller : ModProjectile   //make sure the sprite file is named like the class name (CustomYoyoProjectile)
    {
 
        public override void SetDefaults()
        {
            projectile.extraUpdates = 0;
            projectile.width = 14;
            projectile.height = 14;
            projectile.aiStyle = 99;
            projectile.friendly = true;
            projectile.penetrate = -1;
            projectile.melee = true;
            ProjectileID.Sets.YoyosLifeTimeMultiplier[projectile.type] = -1f;
            ProjectileID.Sets.YoyosMaximumRange[projectile.type] = 400f;
            ProjectileID.Sets.YoyosTopSpeed[projectile.type] = 17.5f;
        }

        public override void PostAI()
        {
            projectile.localAI[1] += 1f;
            if (projectile.localAI[1] >= 25f)
            {
                float num3 = 400f;
                Vector2 vector = projectile.velocity;
                Vector2 vector2 = new Vector2(Main.rand.Next(-100, 101), Main.rand.Next(-100, 101));
                vector2.Normalize();
                vector2 *= Main.rand.Next(10, 41) * 0.1f;
                if (Main.rand.Next(3) == 0)
                {
                    vector2 *= 2f;
                }
                vector *= 0.25f;
                vector += vector2;
                for (int j = 0; j < 200; j++)
                {
                    if (Main.npc[j].CanBeChasedBy(this, false))
                    {
                        float num4 = Main.npc[j].position.X + Main.npc[j].width / 2;
                        float num5 = Main.npc[j].position.Y + Main.npc[j].height / 2;
                        float num6 = Math.Abs(projectile.position.X + projectile.width / 2 - num4) + Math.Abs(projectile.position.Y + projectile.height / 2 - num5);
                        if (num6 < num3 && Collision.CanHit(projectile.position, projectile.width, projectile.height, Main.npc[j].position, Main.npc[j].width, Main.npc[j].height))
                        {
                            num3 = num6;
                            vector.X = num4;
                            vector.Y = num5;
                            vector -= projectile.Center;
                            vector.Normalize();
                            vector *= 8f;
                        }
                    }
                }
                vector *= 0.8f;
                Projectile.NewProjectile(projectile.Center.X - vector.X, projectile.Center.Y - vector.Y, vector.X, vector.Y, ModContent.ProjectileType<Time>(), projectile.damage, projectile.knockBack, projectile.owner, 0f, 0f);
                projectile.localAI[1] = 0f;
            }
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Time Teller");
        }
    }
}
