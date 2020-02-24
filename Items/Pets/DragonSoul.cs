using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Items.Pets
{
    /// <summary>
    /// ALPHA THIS IS NOT AN ITEMS, ALSO WHY THE ITEM HAVE JUST AN EXTRA S, IT WOULDN'T BE CASE IF IT WAS IN THE PROPER PLACE. ALSO WOULD BE BETTER IN POETHIC FRENCH
    /// </summary>
    public class DragonSoul : ModProjectile
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Dragon Soul");
			Main.projFrames[projectile.type] = 4;
			Main.projPet[projectile.type] = true;
			ProjectileID.Sets.LightPet[projectile.type] = true;
		}

		public override void SetDefaults()
		{
			projectile.width = 30;
			projectile.height = 30;
			projectile.penetrate = -1;
			projectile.netImportant = true;
			projectile.timeLeft *= 5;
			projectile.friendly = true;
			projectile.ignoreWater = true;
			projectile.tileCollide = false;

		}

		public override void AI()
        {
            Lighting.AddLight((int)(projectile.Center.X + projectile.width / 2) / 16, (int)(projectile.position.Y + projectile.height / 2) / 16, .5f, 0.3f, 0f);
            if (projectile.velocity.X > 0f)
            {
                projectile.spriteDirection = -1;
            }
            else if (projectile.velocity.X < 0f)
            {
                projectile.spriteDirection = 1;
            }
            projectile.rotation = projectile.velocity.X * 0.1f;
            projectile.frameCounter++;
            if (projectile.frameCounter >= 4)
            {
                projectile.frame++;
                projectile.frameCounter = 0;
            }
            if (projectile.frame >= 4)
            {
                projectile.frame = 0;
            }
            Player player = Main.player[projectile.owner];
            if (Main.myPlayer == projectile.owner)
            {
                if (player.GetModPlayer<AAPlayer>().DragonSoul)
                {
                    projectile.timeLeft = 2;
                }
            }
            if (Main.player[projectile.owner].dead)
            {
                projectile.Kill();
                return;
            }
            float num146 = 3.5f;
            Vector2 vector13 = new Vector2(projectile.position.X + projectile.width * 0.5f, projectile.position.Y + projectile.height * 0.5f);
            float num147 = Main.player[projectile.owner].position.X + Main.player[projectile.owner].width / 2 - vector13.X;
            float num148 = Main.player[projectile.owner].position.Y + Main.player[projectile.owner].height / 2 - vector13.Y;
            int num149 = 40;
            float num150 = (float)Math.Sqrt(num147 * num147 + num148 * num148);
            num150 = (float)Math.Sqrt(num147 * num147 + num148 * num148);
            if (num150 > 800f)
            {
                projectile.position.X = Main.player[projectile.owner].position.X + Main.player[projectile.owner].width / 2 - projectile.width / 2;
                projectile.position.Y = Main.player[projectile.owner].position.Y + Main.player[projectile.owner].height / 2 - projectile.height / 2;
                return;
            }
            if (num150 > num149)
            {
                num150 = num146 / num150;
                num147 *= num150;
                num148 *= num150;
                projectile.velocity.X = num147;
                projectile.velocity.Y = num148;
                return;
            }
            projectile.velocity.X = 0f;
            projectile.velocity.Y = 0f;
            return;
        }
    }
}