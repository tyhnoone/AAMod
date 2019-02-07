using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.NPCs.Bosses.SoC

{
    public class Watcher : ModProjectile
	{
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Watcher");
		}

		public override void SetDefaults()
		{
            projectile.width = 14;
            projectile.height = 14;
            projectile.aiStyle = -1;
            projectile.hostile = true;
            projectile.penetrate = -1;
            projectile.alpha = 255;
            projectile.timeLeft = 600;
        }

        public override void AI()
        {
            projectile.alpha -= 40;
            if (projectile.alpha < 0)
            {
                projectile.alpha = 0;
            }
            if (projectile.ai[0] == 0f)
            {
                projectile.localAI[0] += 1f;
                if (projectile.localAI[0] >= 45f)
                {
                    projectile.localAI[0] = 0f;
                    projectile.ai[0] = 1f;
                    projectile.ai[1] = -projectile.ai[1];
                    projectile.netUpdate = true;
                }
                projectile.velocity.X = projectile.velocity.RotatedBy((double)projectile.ai[1], default(Vector2)).X;
                projectile.velocity.X = MathHelper.Clamp(projectile.velocity.X, -6f, 6f);
                projectile.velocity.Y = projectile.velocity.Y - 0.08f;
                if (projectile.velocity.Y > 0f)
                {
                    projectile.velocity.Y = projectile.velocity.Y - 0.2f;
                }
                if (projectile.velocity.Y < -7f)
                {
                    projectile.velocity.Y = -7f;
                }
            }
            else if (projectile.ai[0] == 1f)
            {
                projectile.localAI[0] += 1f;
                if (projectile.localAI[0] >= 90f)
                {
                    projectile.localAI[0] = 0f;
                    projectile.ai[0] = 2f;
                    projectile.ai[1] = (float)Player.FindClosest(projectile.position, projectile.width, projectile.height);
                    projectile.netUpdate = true;
                }
                projectile.velocity.X = projectile.velocity.RotatedBy((double)projectile.ai[1], default(Vector2)).X;
                projectile.velocity.X = MathHelper.Clamp(projectile.velocity.X, -6f, 6f);
                projectile.velocity.Y = projectile.velocity.Y - 0.08f;
                if (projectile.velocity.Y > 0f)
                {
                    projectile.velocity.Y = projectile.velocity.Y - 0.2f;
                }
                if (projectile.velocity.Y < -7f)
                {
                    projectile.velocity.Y = -7f;
                }
            }
            else if (projectile.ai[0] == 2f)
            {
                Vector2 vector68 = Main.player[(int)projectile.ai[1]].Center - projectile.Center;
                if (vector68.Length() < 30f)
                {
                    projectile.Kill();
                    return;
                }
                vector68.Normalize();
                vector68 *= 14f;
                vector68 = Vector2.Lerp(projectile.velocity, vector68, 0.6f);
                if (vector68.Y < 6f)
                {
                    vector68.Y = 6f;
                }
                float num784 = 0.4f;
                if (projectile.velocity.X < vector68.X)
                {
                    projectile.velocity.X = projectile.velocity.X + num784;
                    if (projectile.velocity.X < 0f && vector68.X > 0f)
                    {
                        projectile.velocity.X = projectile.velocity.X + num784;
                    }
                }
                else if (projectile.velocity.X > vector68.X)
                {
                    projectile.velocity.X = projectile.velocity.X - num784;
                    if (projectile.velocity.X > 0f && vector68.X < 0f)
                    {
                        projectile.velocity.X = projectile.velocity.X - num784;
                    }
                }
                if (projectile.velocity.Y < vector68.Y)
                {
                    projectile.velocity.Y = projectile.velocity.Y + num784;
                    if (projectile.velocity.Y < 0f && vector68.Y > 0f)
                    {
                        projectile.velocity.Y = projectile.velocity.Y + num784;
                    }
                }
                else if (projectile.velocity.Y > vector68.Y)
                {
                    projectile.velocity.Y = projectile.velocity.Y - num784;
                    if (projectile.velocity.Y > 0f && vector68.Y < 0f)
                    {
                        projectile.velocity.Y = projectile.velocity.Y - num784;
                    }
                }
            }
            if (projectile.alpha < 40)
            {
                int num785 = Dust.NewDust(projectile.Center - Vector2.One * 5f, 10, 10, mod.DustType<Dusts.CthulhuDust>(), -projectile.velocity.X / 3f, -projectile.velocity.Y / 3f, 150, Color.Transparent, 1.2f);
                Main.dust[num785].noGravity = true;
            }
            projectile.rotation = projectile.velocity.ToRotation() + 1.57079637f;
        }

        public override void Kill(int timeLeft)
        {
            Main.PlaySound(29, (int)projectile.position.X, (int)projectile.position.Y, 103, 1f, 0f);
            projectile.position = projectile.Center;
            projectile.width = (projectile.height = 144);
            projectile.position.X = projectile.position.X - (float)(projectile.width / 2);
            projectile.position.Y = projectile.position.Y - (float)(projectile.height / 2);
            for (int num193 = 0; num193 < 4; num193++)
            {
                Dust.NewDust(new Vector2(projectile.position.X, projectile.position.Y), projectile.width, projectile.height, mod.DustType<Dusts.CthulhuDust>(), 0f, 0f, 100, default(Color), 1.5f);
            }
            for (int num194 = 0; num194 < 40; num194++)
            {
                int num195 = Dust.NewDust(new Vector2(projectile.position.X, projectile.position.Y), projectile.width, projectile.height, mod.DustType<Dusts.CthulhuDust>(), 0f, 0f, 0, default(Color), 2.5f);
                Main.dust[num195].noGravity = true;
                Main.dust[num195].velocity *= 3f;
                num195 = Dust.NewDust(new Vector2(projectile.position.X, projectile.position.Y), projectile.width, projectile.height, mod.DustType<Dusts.CthulhuDust>(), 0f, 0f, 100, default(Color), 1.5f);
                Main.dust[num195].velocity *= 2f;
                Main.dust[num195].noGravity = true;
            }
            for (int num196 = 0; num196 < 1; num196++)
            {
                int num197 = Gore.NewGore(projectile.position + new Vector2((float)(projectile.width * Main.rand.Next(100)) / 100f, (float)(projectile.height * Main.rand.Next(100)) / 100f) - Vector2.One * 10f, default(Vector2), Main.rand.Next(61, 64), 1f);
                Main.gore[num197].velocity *= 0.3f;
                Gore expr_6EC5_cp_0 = Main.gore[num197];
                expr_6EC5_cp_0.velocity.X = expr_6EC5_cp_0.velocity.X + (float)Main.rand.Next(-10, 11) * 0.05f;
                Gore expr_6EF5_cp_0 = Main.gore[num197];
                expr_6EF5_cp_0.velocity.Y = expr_6EF5_cp_0.velocity.Y + (float)Main.rand.Next(-10, 11) * 0.05f;
            }
            projectile.Damage();
        }
    }
}
