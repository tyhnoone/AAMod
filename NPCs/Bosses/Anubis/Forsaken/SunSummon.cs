using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;
using System;

namespace AAMod.NPCs.Bosses.Anubis.Forsaken
{
    public class SunSummon : ModProjectile
    {
        public override string Texture => "AAMod/BlankTex";
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Sun Summon");
        }

        public override void SetDefaults()
        {
            projectile.width = 98;
            projectile.height = 98;
            projectile.penetrate = -1;
            projectile.friendly = false;
            projectile.hostile = true;
            projectile.tileCollide = false;
            projectile.ignoreWater = true;
            projectile.alpha = 255;
        }

        public override void AI()
        {
            for (int num468 = 0; num468 < 5; num468++)
            {
                int num469 = Dust.NewDust(new Vector2(projectile.Center.X, projectile.Center.Y), 0, 0, ModContent.DustType<Dusts.ForsakenDust>(), 0f, 0f, 0, default, 2f);
                Main.dust[num469].noGravity = true;
            }
            projectile.damage = 0;
            projectile.knockBack = 0;


            projectile.ai[1] = projectile.velocity.Length();

            projectile.velocity = projectile.velocity.RotatedBy(projectile.ai[1] / (2 * Math.PI * projectile.ai[0] * ++projectile.localAI[0]));

            projectile.ai[0]++;

            if (projectile.ai[0] > 60)
            {
                projectile.Kill();
            }
        }

        public override void Kill(int timeLeft)
        {
            int MinionType = ModContent.NPCType<ForsakenSun>();

            int Minion = NPC.NewNPC((int)projectile.Center.X, (int)projectile.Center.Y, MinionType, 0);
            Main.npc[Minion].netUpdate2 = true;
        }
    }
}
