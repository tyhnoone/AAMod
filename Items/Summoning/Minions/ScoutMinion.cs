﻿namespace AAMod.Items.Summoning.Minions
{
    /*public class ScoutMinion : ModProjectile
    {
    	public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Scout");
			ProjectileID.Sets.MinionSacrificable[projectile.type] = true;
            ProjectileID.Sets.MinionTargettingFeature[projectile.type] = true;
		}
    	
        public override void SetDefaults()
        {
            projectile.width = 20;
            projectile.height = 20;
            projectile.netImportant = true;
            projectile.friendly = true;
            projectile.ignoreWater = true;
            projectile.minionSlots = 0;
            projectile.timeLeft = 18000;
            projectile.penetrate = -1;
            projectile.tileCollide = false;
            projectile.timeLeft *= 5;
            projectile.minion = true;
            projectile.minionSlots = 0;
        }

        public override void AI()
        {
        	if (projectile.localAI[0] == 0f)
        	{
        		int num226 = 36;
				for (int num227 = 0; num227 < num226; num227++)
				{
					Vector2 vector6 = Vector2.Normalize(projectile.velocity) * new Vector2(projectile.width / 2f, projectile.height) * 0.75f;
					vector6 = vector6.RotatedBy((num227 - (num226 / 2 - 1)) * 6.28318548f / num226, default) + projectile.Center;
					Vector2 vector7 = vector6 - projectile.Center;
					int num228 = Dust.NewDust(vector6 + vector7, 0, 0, 235, vector7.X * 1.75f, vector7.Y * 1.75f, 100, default, 1.1f);
					Main.dust[num228].noGravity = true;
					Main.dust[num228].velocity = vector7;
				}
				projectile.localAI[0] += 1f;
        	}
        	float num633 = 700f;
			float num634 = 800f;
			float num635 = 1200f;
			float num636 = 150f;
			Player player = Main.player[projectile.owner];
            AAPlayer modPlayer = Main.player[projectile.owner].GetModPlayer<AAPlayer>();
            if (player.dead)
            {
                modPlayer.ScoutMinion = false;
            }
            if (modPlayer.ScoutMinion)
            {
                projectile.timeLeft = 2;
            }
            float num637 = 0.05f;
			for (int num638 = 0; num638 < 1000; num638++)
			{
				bool flag23 = Main.projectile[num638].type == mod.ProjectileType("ScoutMinion");
				if (num638 != projectile.whoAmI && Main.projectile[num638].active && Main.projectile[num638].owner == projectile.owner && flag23 && Math.Abs(projectile.position.X - Main.projectile[num638].position.X) + Math.Abs(projectile.position.Y - Main.projectile[num638].position.Y) < projectile.width)
				{
					if (projectile.position.X < Main.projectile[num638].position.X)
					{
						projectile.velocity.X = projectile.velocity.X - num637;
					}
					else
					{
						projectile.velocity.X = projectile.velocity.X + num637;
					}
					if (projectile.position.Y < Main.projectile[num638].position.Y)
					{
						projectile.velocity.Y = projectile.velocity.Y - num637;
					}
					else
					{
						projectile.velocity.Y = projectile.velocity.Y + num637;
					}
				}
			}
			bool flag24 = false;
			if (flag24)
			{
				return;
			}
			Vector2 vector46 = projectile.position;
			bool flag25 = false;
			if (projectile.ai[0] != 1f)
			{
				projectile.tileCollide = false;
			}
			if (projectile.tileCollide && WorldGen.SolidTile(Framing.GetTileSafely((int)projectile.Center.X / 16, (int)projectile.Center.Y / 16)))
			{
				projectile.tileCollide = false;
			}
			for (int num645 = 0; num645 < 200; num645++)
			{
				NPC nPC2 = Main.npc[num645];
				if (nPC2.CanBeChasedBy(projectile, false))
				{
					float num646 = Vector2.Distance(nPC2.Center, projectile.Center);
					if (((Vector2.Distance(projectile.Center, vector46) > num646 && num646 < num633) || !flag25) && Collision.CanHitLine(projectile.position, projectile.width, projectile.height, nPC2.position, nPC2.width, nPC2.height))
					{
						num633 = num646;
						vector46 = nPC2.Center;
						flag25 = true;
					}
				}
			}
			float num647 = num634;
			if (flag25)
			{
				num647 = num635;
			}
			if (Vector2.Distance(player.Center, projectile.Center) > num647)
			{
				projectile.ai[0] = 1f;
				projectile.tileCollide = false;
				projectile.netUpdate = true;
			}
			if (flag25 && projectile.ai[0] == 0f)
			{
				Vector2 vector47 = vector46 - projectile.Center;
				float num648 = vector47.Length();
				vector47.Normalize();
				if (num648 > 200f)
				{
					float scaleFactor2 = 6f;
					vector47 *= scaleFactor2;
					projectile.velocity = (projectile.velocity * 40f + vector47) / 41f;
				}
				else
				{
					float num649 = 4f;
					vector47 *= -num649;
					projectile.velocity = (projectile.velocity * 40f + vector47) / 41f;
				}
			}
			else
			{
				bool flag26 = false;
				if (!flag26)
				{
					flag26 = projectile.ai[0] == 1f;
				}
				float num650 = 6f;
				if (flag26)
				{
					num650 = 15f;
				}
				Vector2 center2 = projectile.Center;
				Vector2 vector48 = player.Center - center2 + new Vector2(0f, -60f);
				float num651 = vector48.Length();
				if (num651 > 200f && num650 < 8f)
				{
					num650 = 8f;
				}
				if (num651 < num636 && flag26 && !Collision.SolidCollision(projectile.position, projectile.width, projectile.height))
				{
					projectile.ai[0] = 0f;
					projectile.netUpdate = true;
				}
				if (num651 > 2000f)
				{
					projectile.position.X = Main.player[projectile.owner].Center.X - projectile.width / 2;
					projectile.position.Y = Main.player[projectile.owner].Center.Y - projectile.height / 2;
					projectile.netUpdate = true;
				}
				if (num651 > 70f)
				{
					vector48.Normalize();
					vector48 *= num650;
					projectile.velocity = (projectile.velocity * 40f + vector48) / 41f;
				}
				else if (projectile.velocity.X == 0f && projectile.velocity.Y == 0f)
				{
					projectile.velocity.X = -0.15f;
					projectile.velocity.Y = -0.05f;
				}
			}
			if (flag25)
			{
				projectile.rotation = (vector46 - projectile.Center).ToRotation() + 3.14159274f;
			}
			else
			{
				projectile.rotation = projectile.velocity.ToRotation() + 3.14159274f;
			}
			projectile.frameCounter++;
			if (projectile.frameCounter > 3)
			{
				projectile.frame++;
				projectile.frameCounter = 0;
			}
			if (projectile.frame > 2)
			{
				projectile.frame = 0;
			}
			if (projectile.ai[1] > 0f)
			{
				projectile.ai[1] += Main.rand.Next(1, 4);
			}
			if (projectile.ai[1] > 90f)
			{
				projectile.ai[1] = 0f;
				projectile.netUpdate = true;
			}
			if (projectile.ai[0] == 0f)
			{
				float scaleFactor3 = 8f;
				int num658 = mod.ProjectileType("Neutralizer");
				if (flag25 && projectile.ai[1] == 0f)
				{
					projectile.ai[1] += 1f;
					if (Main.myPlayer == projectile.owner && Collision.CanHitLine(projectile.position, projectile.width, projectile.height, vector46, 0, 0))
					{
						Vector2 value19 = vector46 - projectile.Center;
						value19.Normalize();
						value19 *= scaleFactor3;
						int num659 = Projectile.NewProjectile(projectile.Center.X, projectile.Center.Y, value19.X, value19.Y, num658, (int)(projectile.damage * 0.8f), 0f, Main.myPlayer, 0f, 0f);
                        Main.projectile[num659].ranged = false;
                        Main.projectile[num659].minion = true;
                        Main.projectile[num659].timeLeft = 300;
						projectile.netUpdate = true;
					}
				}
			}
        }
    }*/
}