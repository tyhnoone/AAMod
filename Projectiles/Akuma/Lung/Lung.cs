using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using BaseMod;
using AAMod;


namespace AAMod.Projectiles.Akuma.Lung
{
    public class LungHead : ModProjectile
    {
        public override void SetDefaults()
        {
            projectile.width = 24;
            projectile.height = 24;

            projectile.friendly = true;
            projectile.ignoreWater = true;
            projectile.netImportant = true;
            projectile.tileCollide = false;
            projectile.minion = true;

            projectile.penetrate = -1;
            projectile.timeLeft = 18000;
            ProjectileID.Sets.MinionSacrificable[projectile.type] = true;
            projectile.timeLeft *= 5;
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Ancient Lung");
        }
        public override void AI()
        {
            // vanilla code
            Player player10 = Main.player[projectile.owner];
            if ((int)Main.time % 120 == 0)
            {
                projectile.netUpdate = true;
            }
            if (!player10.active)
            {
                projectile.active = false;
                return;
            }
            int num1049 = 10;
            // head
            Vector2 center14 = player10.Center;
            float maxTargetDist = 700f;
            float num1052 = 1000f;
            int num1053 = -1;
            if (projectile.Distance(center14) > 2000f)
            {
                projectile.Center = center14;
                projectile.netUpdate = true;
            }
            bool flag67 = true;
            if (flag67)
            {
                NPC ownerMinionAttackTargetNPC5 = projectile.OwnerMinionAttackTargetNPC;
                if (ownerMinionAttackTargetNPC5 != null && ownerMinionAttackTargetNPC5.CanBeChasedBy(projectile, false))
                {
                    float num1054 = projectile.Distance(ownerMinionAttackTargetNPC5.Center);
                    if (num1054 < maxTargetDist * 2f)
                    {
                        num1053 = ownerMinionAttackTargetNPC5.whoAmI;
                        if (ownerMinionAttackTargetNPC5.boss)
                        {
                            int whoAmI = ownerMinionAttackTargetNPC5.whoAmI;
                        }
                        else
                        {
                            int whoAmI2 = ownerMinionAttackTargetNPC5.whoAmI;
                        }
                    }
                }
                if (num1053 < 0)
                {
                    for (int i = 0; i < Main.npc.Length; i++)
                    {
                        NPC target = Main.npc[i];
                        if (target.CanBeChasedBy(projectile, false) && player10.Distance(target.Center) < num1052)
                        {
                            float num1056 = projectile.Distance(target.Center);
                            if (num1056 < maxTargetDist)
                            {
                                num1053 = i;
                            }
                        }
                    }
                }
            }
            if (num1053 != -1)
            {
                NPC nPC15 = Main.npc[num1053];
                Vector2 vector148 = nPC15.Center - projectile.Center;
                float num1057 = (float)(vector148.X > 0f).ToDirectionInt();
                float num1058 = (float)(vector148.Y > 0f).ToDirectionInt();
                float scaleFactor15 = 0.4f;
                if (vector148.Length() < 600f)
                {
                    scaleFactor15 = 0.6f;
                }
                if (vector148.Length() < 300f)
                {
                    scaleFactor15 = 0.8f;
                }
                if (vector148.Length() > nPC15.Size.Length() * 0.75f)
                {
                    projectile.velocity += Vector2.Normalize(vector148) * scaleFactor15 * 1.5f;
                    if (Vector2.Dot(projectile.velocity, vector148) < 0.25f)
                    {
                        projectile.velocity *= 0.8f;
                    }
                }
                float num1059 = 30f;
                if (projectile.velocity.Length() > num1059)
                {
                    projectile.velocity = Vector2.Normalize(projectile.velocity) * num1059;
                }
            }
            else
            {
                float num1060 = 0.2f;
                Vector2 vector149 = center14 - projectile.Center;
                if (vector149.Length() < 200f)
                {
                    num1060 = 0.12f;
                }
                if (vector149.Length() < 140f)
                {
                    num1060 = 0.06f;
                }
                if (vector149.Length() > 100f)
                {
                    if (Math.Abs(center14.X - projectile.Center.X) > 20f)
                    {
                        projectile.velocity.X = projectile.velocity.X + num1060 * (float)Math.Sign(center14.X - projectile.Center.X);
                    }
                    if (Math.Abs(center14.Y - projectile.Center.Y) > 10f)
                    {
                        projectile.velocity.Y = projectile.velocity.Y + num1060 * (float)Math.Sign(center14.Y - projectile.Center.Y);
                    }
                }
                else if (projectile.velocity.Length() > 2f)
                {
                    projectile.velocity *= 0.96f;
                }
                if (Math.Abs(projectile.velocity.Y) < 1f)
                {
                    projectile.velocity.Y = projectile.velocity.Y - 0.1f;
                }
                float num1061 = 15f;
                if (projectile.velocity.Length() > num1061)
                {
                    projectile.velocity = Vector2.Normalize(projectile.velocity) * num1061;
                }
            }
            projectile.rotation = projectile.velocity.ToRotation() + 1.57079637f;
            int direction = projectile.direction;
            projectile.direction = (projectile.spriteDirection = ((projectile.velocity.X > 0f) ? 1 : -1));
            if (direction != projectile.direction)
            {
                projectile.netUpdate = true;
            }
            float num1062 = MathHelper.Clamp(projectile.localAI[0], 0f, 50f);
            projectile.position = projectile.Center;
            projectile.scale = 1f + num1062 * 0.01f;
            projectile.width = (projectile.height = (int)((float)num1049 * projectile.scale));
            projectile.Center = projectile.position;
            if (projectile.alpha > 0)
            {
                projectile.alpha -= 42;
                if (projectile.alpha < 0)
                {
                    projectile.alpha = 0;
                    return;
                }
            }
        }

        public class LungBody : ModProjectile
        {
            public override void SetDefaults()
            {
                projectile.width = 24;
                projectile.height = 24;

                projectile.friendly = true;
                projectile.ignoreWater = true;
                projectile.netImportant = true;
                projectile.tileCollide = false;
                projectile.minion = true;

                projectile.penetrate = -1;
                projectile.timeLeft = 18000;
                ProjectileID.Sets.MinionSacrificable[projectile.type] = true;
                projectile.timeLeft *= 5;
                projectile.minionSlots = 1f;
            }

            public override void SetStaticDefaults()
            {
                DisplayName.SetDefault("Ancient Lung");
            }
            public override void AI()
            {
                Player player10 = Main.player[projectile.owner];
                if (!player10.active)
                {
                    projectile.active = false;
                    return;
                }

                int num1049 = 10;
                Vector2 parCenter = Vector2.Zero;
                float parRot = 0f;
                float scaleFactor16 = 0f;
                float scaleFactor17 = 1f;
                if (projectile.ai[1] == 1f)
                {
                    projectile.ai[1] = 0f;
                    projectile.netUpdate = true;
                }
                Projectile parent = Main.projectile[(int)projectile.ai[0]];
                if ((int)projectile.ai[0] >= 0 && parent.active)
                {
                    parCenter = parent.Center;
                    parRot = parent.rotation;
                    scaleFactor17 = MathHelper.Clamp(parent.scale, 0f, 50f);
                    scaleFactor16 = 16f;
                    parent.localAI[0] = projectile.localAI[0] + 1f;
                }
                else
                {
                    return;
                }
                if (projectile.alpha > 0)
                {
                    int num3;
                    for (int num1068 = 0; num1068 < 2; num1068 = num3 + 1)
                    {
                        int num1069 = Dust.NewDust(projectile.position, projectile.width, projectile.height, 135, 0f, 0f, 100, default(Color), 2f);
                        Main.dust[num1069].noGravity = true;
                        Main.dust[num1069].noLight = true;
                        num3 = num1068;
                    }
                }
                projectile.alpha -= 42;
                if (projectile.alpha < 0)
                {
                    projectile.alpha = 0;
                }
                projectile.velocity = Vector2.Zero;
                Vector2 vector151 = parCenter - projectile.Center;
                if (parRot != projectile.rotation)
                {
                    float num1070 = MathHelper.WrapAngle(parRot - projectile.rotation);
                    vector151 = vector151.RotatedBy((double)(num1070 * 0.1f), default(Vector2));
                }
                projectile.rotation = vector151.ToRotation() + 1.57079637f;
                projectile.position = projectile.Center;
                projectile.scale = scaleFactor17;
                projectile.width = (projectile.height = (int)((float)num1049 * projectile.scale));
                projectile.Center = projectile.position;
                if (vector151 != Vector2.Zero)
                {
                    projectile.Center = parCenter - Vector2.Normalize(vector151) * scaleFactor16 * scaleFactor17;
                }
                projectile.spriteDirection = ((vector151.X > 0f) ? 1 : -1);
                return;
            }
        }
        public class LungTail : ModProjectile
        {
            public override void SetDefaults()
            {
                projectile.width = 24;
                projectile.height = 24;

                projectile.friendly = true;
                projectile.ignoreWater = true;
                projectile.netImportant = true;
                projectile.tileCollide = false;
                projectile.minion = true;

                projectile.penetrate = -1;
                projectile.timeLeft = 18000;
                ProjectileID.Sets.MinionSacrificable[projectile.type] = true;
                projectile.timeLeft *= 5;
            }

            public override void SetStaticDefaults()
            {
                DisplayName.SetDefault("Ancient Lung");
            }
            public override void AI()
            {
                Player player10 = Main.player[projectile.owner];
                if (!player10.active)
                {
                    projectile.active = false;
                    return;
                }

                int num1049 = 10;
                Vector2 parCenter = Vector2.Zero;
                float parRot = 0f;
                float scaleFactor16 = 0f;
                float scaleFactor17 = 1f;
                if (projectile.ai[1] == 1f)
                {
                    projectile.ai[1] = 0f;
                    projectile.netUpdate = true;
                }
                Projectile parent = Main.projectile[(int)projectile.ai[0]];
                if ((int)projectile.ai[0] >= 0 && parent.active)
                {
                    parCenter = parent.Center;
                    parRot = parent.rotation;
                    scaleFactor17 = MathHelper.Clamp(parent.scale, 0f, 50f);
                    scaleFactor16 = 16f;
                    parent.localAI[0] = projectile.localAI[0] + 1f;
                }
                else
                {
                    return;
                }
                if (projectile.alpha > 0)
                {
                    int num3;
                    for (int num1068 = 0; num1068 < 2; num1068 = num3 + 1)
                    {
                        int num1069 = Dust.NewDust(projectile.position, projectile.width, projectile.height, 135, 0f, 0f, 100, default(Color), 2f);
                        Main.dust[num1069].noGravity = true;
                        Main.dust[num1069].noLight = true;
                        num3 = num1068;
                    }
                }
                projectile.alpha -= 42;
                if (projectile.alpha < 0)
                {
                    projectile.alpha = 0;
                }
                projectile.velocity = Vector2.Zero;
                Vector2 vector151 = parCenter - projectile.Center;
                if (parRot != projectile.rotation)
                {
                    float num1070 = MathHelper.WrapAngle(parRot - projectile.rotation);
                    vector151 = vector151.RotatedBy((double)(num1070 * 0.1f), default(Vector2));
                }
                projectile.rotation = vector151.ToRotation() + 1.57079637f;
                projectile.position = projectile.Center;
                projectile.scale = scaleFactor17;
                projectile.width = (projectile.height = (int)((float)num1049 * projectile.scale));
                projectile.Center = projectile.position;
                if (vector151 != Vector2.Zero)
                {
                    projectile.Center = parCenter - Vector2.Normalize(vector151) * scaleFactor16 * scaleFactor17;
                }
                projectile.spriteDirection = ((vector151.X > 0f) ? 1 : -1);
                return;
            }
        }
	





/*
//idk how to use basemod sorry guys- orange
public class Lung : ModProjectile
    {
        public override void SetStaticDefaults()
		{
            DisplayName.SetDefault("Ancient Lung");
		}		

        public override void SetDefaults()
        {
            projectile.width = 24;
            projectile.height = 24;
            projectile.aiStyle = -1;
            projectile.timeLeft = 300;
            projectile.friendly = true;
            projectile.hostile = false;
            projectile.tileCollide = false;
			projectile.ignoreWater = true;
			projectile.minion = true;
            projectile.minionSlots = 1f;
            projectile.penetrate = -1;
            projectile.netImportant = true;	
			ProjectileID.Sets.MinionSacrificable[projectile.type] = true;				
        }

		public bool checkedMinPos = false;
		public static Texture2D[] textures
		{
			get
			{
				return AAMod.instance.texHandler.LungTex;
			}
			set
			{
                AAMod.instance.texHandler.LungTex = value;
			}
		}		
		public static int frameWidth = 40, frameHeight = 40;
		public Rectangle frame = new Rectangle(0, 0, frameWidth, frameHeight);
		
		public int pieceCount = 2, lastPieceCount = 2;
		public Vector2[] piecePositions = new Vector2[2];
		public float[] pieceRots = new float[2];

		public void SetMinionCount(int count)
		{
			pieceCount = count;
			if(lastPieceCount != pieceCount)
			{
				piecePositions = new Vector2[pieceCount];
				pieceRots = new float[pieceCount];	
				projectile.netUpdate2 = true;				
			}
			lastPieceCount = pieceCount;
		}

		public override void AI()
		{
			Player player = Main.player[projectile.owner];
			int[] projs = BaseAI.GetProjectiles(player.Center, projectile.type, player.whoAmI, -1);
			if(!checkedMinPos)
			{
				for (int m = 0; m < projs.Length; m++) { if (projs[m] == projectile.identity) { projectile.minionPos = m; } }
				if (Main.myPlayer == projectile.owner) { projectile.netUpdate = true; }
			}
			if (!Main.player[projectile.owner].active || Main.player[projectile.owner].dead || projs.Length > 1) { projectile.Kill(); return; }
			SetMinionCount(player.maxMinions);
			int npcTarget = -1;
			BaseAI.AIProjWorm(projectile, ref projectile.ai, ref npcTarget, new int[]{ mod.ProjType("Lung"), -1, -1 }, 1f, 1f, 14f, 9f);
			Vector2 projectileCenter = projectile.Center;
            
            projectile.ai[2]++;
            if (projectile.ai[2] >= 200 && npcTarget != 1)
            {
                projectile.frame = 1;
                Projectile.NewProjectile(projectile.position.X, projectile.position.Y, projectile.velocity.X * 3f, projectile.velocity.Y * 3f, mod.ProjectileType("AkumaBreath"), projectile.damage, 0, Main.myPlayer);
            }
            if (projectile.ai[2] >= 260)
            {
                projectile.ai[2] = 0;
                projectile.frame = 0;
            }
			for (int m = 0; m < piecePositions.Length; m++)
			{
				Vector2 lastPos = (m == 0 ? projectile.Center : piecePositions[m - 1]);
				float lastRot = (m == 0 ? projectile.rotation : pieceRots[m - 1]);
				Vector2 pieceCenter = piecePositions[m];
				float pieceRot = pieceRots[m];
				HandleWormPieceMovement(m, lastPos, lastRot, ref pieceCenter, ref pieceRot);
				piecePositions[m] = pieceCenter;
				pieceRots[m] = pieceRot;

				projectile.Center = piecePositions[m];
				projectile.Damage();
			}
			projectile.Center = projectileCenter;
		}
        
		public void HandleWormPieceMovement(int piece, Vector2 otherCenter, float otherRotation, ref Vector2 center, ref float rotation)
		{
			Vector2 centerDist = otherCenter - center;
			if (otherRotation != rotation)
			{
				float rotDist = MathHelper.WrapAngle(otherRotation - rotation);
				centerDist = centerDist.RotatedBy((double)(rotDist * 0.1f), default(Vector2));
			}
			rotation = centerDist.ToRotation() + 1.57079637f;
			if (centerDist != Vector2.Zero)
			{
				center = otherCenter - Vector2.Normalize(centerDist) * 32f;
			}
			if((piece == 0 || piece == 1 || piece == 2) && projectile.velocity.Length() > 6f)
			{
				float length = 6f - projectile.velocity.Length();
				Vector2 vel = projectile.velocity; vel.Normalize();
				vel *= length;
				center -= (piece == 0 ? vel * 1.5f : piece == 1 ? vel * 0.7f : vel * 0.4f);
			}
		}

		public override bool PreDraw(SpriteBatch sb, Color dColor)
		{
			if (textures == null)
			{
				textures = new Texture2D[8];
				textures[0] = Main.projectileTexture[projectile.type];
				textures[1] = mod.GetTexture("Glowmasks/Lung_Glow");		
				textures[2] = mod.GetTexture("Projectiles/Akuma/LungBody");	
				textures[3] = mod.GetTexture("Glowmasks/LungBody_Glow");	
				textures[4] = mod.GetTexture("Projectiles/Akuma/LungBody1");	
				textures[5] = mod.GetTexture("Glowmasks/LungBody2_Glow");					
				textures[6] = mod.GetTexture("Projectiles/Akuma/LungTail");	
				textures[7] = mod.GetTexture("Glowmasks/LungTail_Glow");
			}
			BaseDrawing.DrawTexture(sb, textures[0], 0, projectile.position, projectile.width, projectile.height, projectile.scale, projectile.rotation, projectile.spriteDirection, 2, frame, dColor);
			BaseDrawing.DrawTexture(sb, textures[1], 0, projectile.position, projectile.width, projectile.height, projectile.scale, projectile.rotation, projectile.spriteDirection, 1, frame, AAColor.AkumaA);	
			Vector2 projectileCenter = projectile.Center;
			float projectileRot = projectile.rotation;
			for(int m = 0; m < piecePositions.Length; m++)
			{
				if(piecePositions[m] == default(Vector2)) continue;
				projectile.Center = piecePositions[m];
				projectile.rotation = pieceRots[m];
				int texID = (m == piecePositions.Length - 1 ? 6 : (m % 2 == 0 ? 2 : 4));
				//BaseMod.BaseDrawing.DrawHitbox(sb, projectile.Hitbox, Color.Red);
				BaseDrawing.DrawTexture(sb, textures[texID], 0, projectile.position, projectile.width, projectile.height, projectile.scale, projectile.rotation, projectile.spriteDirection, 1, frame, dColor);
				BaseDrawing.DrawTexture(sb, textures[texID + 1], 0, projectile.position, projectile.width, projectile.height, projectile.scale, projectile.rotation, projectile.spriteDirection, 1, frame, AAColor.AkumaA);				
			}
			projectile.Center = projectileCenter;
			projectile.rotation = projectileRot;
			return false;
		}			
    }*/
}
