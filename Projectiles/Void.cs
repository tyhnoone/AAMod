﻿using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;

namespace AAMod.Projectiles
{
	public class Void : ModProjectile
	{
		public override void SetStaticDefaults()
		{
			ProjectileID.Sets.YoyosLifeTimeMultiplier[projectile.type] = 3.5f;
			ProjectileID.Sets.YoyosMaximumRange[projectile.type] = 300f;
			ProjectileID.Sets.YoyosTopSpeed[projectile.type] = 13f;
		}

		public override void SetDefaults()
		{
			projectile.extraUpdates = 0;
			projectile.width = 16;
			projectile.height = 16;
			projectile.aiStyle = 99;
			projectile.friendly = true;
			projectile.penetrate = -1;
			projectile.melee = true;
			projectile.scale = 1f;
		}

        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            int p = Projectile.NewProjectile(projectile.Center, new Vector2(0, 0), ProjectileID.Electrosphere, projectile.damage, projectile.knockBack, projectile.owner, projectile.whoAmI, 0);
            Main.projectile[p].ranged = false;
            Main.projectile[p].melee = false;
        }

        public override void PostAI()
		{
			if (Main.rand.Next(2) == 0)
			{
				Dust dust = Dust.NewDustDirect(projectile.position, projectile.width, projectile.height, DustID.Electric);
				dust.noGravity = true;
            }
        }
	}
}
