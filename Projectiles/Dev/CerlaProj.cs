using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ModLoader;

namespace AAMod.Projectiles.Dev
{
    public class CerlaProj : ModProjectile
	{
		public override void SetDefaults()
		{
			projectile.width = 16;
			projectile.height = 16;
			projectile.friendly = true;
			projectile.penetrate = 1;
			projectile.aiStyle = -1;
			projectile.penetrate = 5;
            projectile.tileCollide = false;
            projectile.timeLeft = 120;
			projectile.melee = true;
        }
		
		public override Color? GetAlpha(Color lightColor)
		{
			return Color.White;
		}
		
		public override bool? Colliding(Rectangle projHitbox, Rectangle targetHitbox)
		{
			if (targetHitbox.Width > 8 && targetHitbox.Height > 8)
			{
				targetHitbox.Inflate(-targetHitbox.Width / 8, -targetHitbox.Height / 8);
			}
			return projHitbox.Intersects(targetHitbox);
		}
		
		public override void AI()
		{
			if (projectile.ai[0] == 1)
			{
				projectile.extraUpdates = 1;
			}
			projectile.rotation = projectile.velocity.ToRotation() + MathHelper.ToRadians(90f);
            if (Main.rand.Next(1) == 0)
            {
                int dustnumber = Dust.NewDust(projectile.position, projectile.width, projectile.height, 6, 0f, 0f, ModContent.DustType<Dusts.StarDust>(), default, 0.8f);
                Main.dust[dustnumber].velocity *= 0.3f;
            }
        }

        public override void Kill(int timeLeft)
        {
            Main.PlaySound(new Terraria.Audio.LegacySoundStyle(2, 124, Terraria.Audio.SoundType.Sound));
            int dustnumber = Dust.NewDust(projectile.position, projectile.width, projectile.height, 6, 0f, 0f, 200, default, 0.8f);
            Main.dust[dustnumber].velocity *= 0.3f;
        }

		public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
		{
			if (projectile.ai[0] == 1 && Main.rand.Next(150) == 0)
			{
				int i = Item.NewItem(projectile.Hitbox, mod.ItemType("CMDOrb"), 1, false, 0, true);
				Main.item[i].velocity = new Vector2(Main.rand.Next(-5, 5), -3);
			}
		}

		public override bool PreDraw(SpriteBatch spriteBatch, Color lightColor)
        {
            Vector2 drawOrigin = new Vector2(Main.projectileTexture[projectile.type].Width * 0.5f, projectile.height * 0.5f);
            for (int k = 0; k < projectile.oldPos.Length; k++)
            {
                Vector2 drawPos = projectile.oldPos[k] - Main.screenPosition + drawOrigin + new Vector2(0f, projectile.gfxOffY);
                Color color = projectile.GetAlpha(lightColor) * ((projectile.oldPos.Length - k) / (float)projectile.oldPos.Length);
                spriteBatch.Draw(Main.projectileTexture[projectile.type], drawPos, null, color, projectile.rotation, drawOrigin, projectile.scale, SpriteEffects.None, 0f);
            }
            return true;
        }
	}
}