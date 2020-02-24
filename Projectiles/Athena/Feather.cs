using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;
using BaseMod;
using System;
using Microsoft.Xna.Framework.Graphics;

namespace AAMod.Projectiles.Athena
{
	public class Feather : ModProjectile
    {
        public override void SetDefaults()
        {
            projectile.width = 16;
            projectile.height = 16;
            projectile.aiStyle = -1;
            projectile.minion = true;
            projectile.penetrate = 3;
            projectile.friendly = true;
        }

        public override void AI()
        {
            projectile.rotation = (float)Math.Atan2(projectile.velocity.Y, projectile.velocity.X) + 2.355f;
        }

        public override bool PreDraw(SpriteBatch spriteBatch, Color lightColor)
        {
            BaseDrawing.DrawAfterimage(spriteBatch, Main.projectileTexture[projectile.type], 0, projectile, 1f, 1f, 5, false, 0f, 0f, lightColor);
            BaseDrawing.DrawTexture(spriteBatch, Main.projectileTexture[projectile.type], 0, projectile, lightColor, false);
            return false;
        }
    }
}