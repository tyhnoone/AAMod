using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ModLoader;

namespace AAMod.Projectiles.Rajah.Supreme
{
    public class BaneTEX : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Bane of the Bunny");
        }

        public override void SetDefaults()
        {
            projectile.width = 16;
            projectile.height = 16;
            projectile.friendly = true;
            projectile.aiStyle = -1;
            projectile.melee = true;
            projectile.penetrate = -1;
            projectile.extraUpdates = 1;
            projectile.usesLocalNPCImmunity = true;
            projectile.localNPCHitCooldown = 0;
        }

        public bool StuckInEnemy = false;
        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            Rectangle myRect = new Rectangle((int)projectile.position.X, (int)projectile.position.Y, projectile.width, projectile.height);
            bool flag3 = projectile.Colliding(myRect, target.getRect());
            if (!target.boss)
            {
                Main.npc[(int)projectile.ai[1]].AddBuff(mod.BuffType<Buffs.SpearStuck>(), 100000);
                if (flag3 && !StuckInEnemy)
                {
                    StuckInEnemy = true;
                    projectile.ai[0] = 1f;
                    projectile.ai[1] = target.whoAmI;
                    projectile.velocity = (target.Center - projectile.Center) * 0.75f;
                    projectile.netUpdate = true;
                }
            }
        }

        public override void AI()
        {
            if (projectile.alpha > 0)
            {
                projectile.alpha -= 25;
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
                projectile.rotation = projectile.velocity.ToRotation() + 1.57079637f;
            }
            if (projectile.ai[0] == 1f)
            {
                projectile.usesLocalNPCImmunity = false;
                projectile.ignoreWater = true;
                projectile.tileCollide = false;
                int num977 = 15;
                bool flag53 = false;
                projectile.localAI[0] += 1f;
                int num978 = (int)projectile.ai[1];
                if (projectile.localAI[0] >= 60 * num977)
                {
                    flag53 = true;
                }
                else if (num978 < 0 || num978 >= 200)
                {
                    flag53 = true;
                }
                else if (Main.npc[num978].active)
                {
                    projectile.Center = Main.npc[num978].Center - projectile.velocity * 2f;
                    projectile.gfxOffY = Main.npc[num978].gfxOffY;
                }
                else
                {
                    flag53 = true;
                }
                if (flag53)
                {
                    projectile.Kill();
                }
            }
        }

        public override bool PreDraw(SpriteBatch spriteBatch, Color lightColor)
        {
            Rectangle frame = BaseMod.BaseDrawing.GetFrame(projectile.frame, Main.projectileTexture[projectile.type].Width, Main.projectileTexture[projectile.type].Height, 0, 2);
            BaseMod.BaseDrawing.DrawTexture(spriteBatch, Main.projectileTexture[projectile.type], 0, projectile.position, projectile.width, projectile.height, projectile.scale, projectile.rotation, 0, 1, frame, lightColor, true);
            return false;
        }
    }
}
