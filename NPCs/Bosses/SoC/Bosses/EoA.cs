using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using BaseMod;

namespace AAMod.NPCs.Bosses.SoC.Bosses
{
	public class EoA : DeityBrain
	{				
		public override void SetStaticDefaults()
		{
            DisplayName.SetDefault("Eye of Azathoth");
            Main.npcFrameCount[npc.type] = 4;
		}

        public override void SetDefaults()
        {
            npc.width = 78;
            npc.height = 120;
            npc.value = BaseUtility.CalcValue(0, 0, 0, 0);
            npc.npcSlots = 1;
            npc.aiStyle = -1;
            npc.lifeMax = 5000;
            npc.defense = 130;
            npc.damage = 60;
            npc.HitSound = SoundID.NPCHit31;
            npc.DeathSound = SoundID.NPCDeath35;
            npc.knockBackResist = 0f;	
			npc.noTileCollide = true;
			npc.defense = 130; //keep defense at 130
        }
        

		public override void NPCLoot()
		{
		}
		
		public int body = -1;
		public float rotValue = -1f;
		public bool spawnedDust = false;
        public bool fireAttack = false;

        public override void AI()
		{
			npc.noGravity = true;
			if(body == -1)
			{
				int npcID = BaseAI.GetNPC(npc.Center, mod.NPCType<DeityBrain>(), 500f, null);	
				if(npcID >= 0) body = npcID;
			}
			if(body == -1) return;				
			NPC brain = Main.npc[body];

            Player targetPlayer = Main.player[npc.target];

            if (brain == null || brain.life <= 0 || !brain.active || brain.type != mod.NPCType<DeityBrain>()) { BaseAI.KillNPCWithLoot(npc); return; }			
		
			if(Main.netMode != 2 && !spawnedDust)
			{
				spawnedDust = true;
				for (int m = 0; m < 20; m++)
				{
					int dustID = Dust.NewDust(npc.position, npc.width, npc.height, mod.DustType<Dusts.CthulhuDust>(), 0, -1f, 0, default(Color), 1f);
                    Main.dust[dustID].noGravity = true;
					Main.dust[dustID].velocity = new Vector2(MathHelper.Lerp(-1f, 1f, (float)Main.rand.NextDouble()), MathHelper.Lerp(-1f, 1f, (float)Main.rand.NextDouble()));
					Main.dust[dustID].velocity *= 2f;
				}
			}
			for (int m = npc.oldPos.Length - 1; m > 0; m--)
			{
				npc.oldPos[m] = npc.oldPos[m - 1];
			}
			npc.oldPos[0] = npc.position;

			int EoACount = ((DeityBrain)brain.modNPC).EyeCount;
			if(rotValue == -1f) rotValue = (npc.ai[0] % EoACount) * ((float)Math.PI * 2f / EoACount);
			rotValue += 0.05f;
			while(rotValue > (float)Math.PI * 2f) rotValue -= (float)Math.PI * 2f;
			npc.Center = BaseUtility.RotateVector(brain.Center, brain.Center + new Vector2(140f, 0f), rotValue);

			npc.spriteDirection = (npc.position.X - npc.oldPos[1].X) < 0 ? 1 : -1;
            npc.rotation = (float)Math.Atan2(npc.velocity.Y, npc.velocity.X) + 1.57f;
            
            npc.ai[1]++;
            int aiTimerFire = (npc.whoAmI % 3 == 0 ? 50 : npc.whoAmI % 2 == 0 ? 150 : 100); //aiTimerFire is different per head by using whoAmI (which is usually different) 
            
            if (targetPlayer != null && npc.ai[1] == aiTimerFire)
            {
                fireAttack = true;
                for (int i = 0; i < 5; ++i)
                {
                    Vector2 dir = Vector2.Normalize(targetPlayer.Center - npc.Center);
                    dir *= 5f;
                    Projectile.NewProjectile(npc.Center.X, npc.Center.Y, dir.X * 3, dir.Y * 3, mod.ProjectileType("DeityFlames"), (int)(npc.damage * .8f), 0f, Main.myPlayer);
                }
            }

            if (Main.netMode != 2 && Main.player[Main.myPlayer].miscTimer % 2 == 0)
			{
                Dust.NewDust(npc.position, npc.width, npc.height, mod.DustType<Dusts.CthulhuDust>(), 0, -1f, 0, default(Color), 1f);
            }			
		}

		public override bool PreDraw(SpriteBatch sb, Color dColor)
		{
			Color lightColor = BaseDrawing.GetNPCColor(npc, null);
            if (Main.player[npc.target] != null && Main.player[npc.target].active && !Main.player[npc.target].dead)
            {
                BaseDrawing.DrawAfterimage(sb, Main.npcTexture[npc.type], 0, npc, 2f, 0.9f, 2, true, 0f, 0f, lightColor);
            }
            Texture2D texture8 = Main.npcTexture[npc.type];
            Texture2D PupilTex = mod.GetTexture("NPCs/Bosses/SoC/Bosses/EoA_Pupil");
            Texture2D Glow = mod.GetTexture("NPCs/Bosses/SoC/Bosses/EoA_Glow");
            Vector2 origin15 = new Vector2(40f, 40f);
            Vector2 value33 = new Vector2(30f, 30f);
            Vector2 arg_A019_0 = npc.Center;
            Point point4 = npc.Center.ToTileCoordinates();
            Main.spriteBatch.Draw(texture8, npc.Center - Main.screenPosition, new Microsoft.Xna.Framework.Rectangle?(npc.frame), dColor, npc.rotation, origin15, 1f, SpriteEffects.None, 0f);
            Vector2 value34 = Utils.Vector2FromElipse(npc.localAI[0].ToRotationVector2(), value33 * npc.localAI[1]);
            Main.spriteBatch.Draw(PupilTex, npc.Center - Main.screenPosition + value34, null, AAColor.Cthulhu2, npc.rotation, PupilTex.Size() / 2f, npc.localAI[2], SpriteEffects.None, 0f);
            Main.spriteBatch.Draw(Glow, npc.Center - Main.screenPosition, new Microsoft.Xna.Framework.Rectangle?(npc.frame), AAColor.Cthulhu2, npc.rotation, origin15, 1f, SpriteEffects.None, 0f);
            return false;
		}		
	}
}