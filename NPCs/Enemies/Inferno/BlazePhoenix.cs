using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.Localization;
using Terraria.Utilities;
using Terraria.ModLoader;
using BaseMod;

namespace AAMod.NPCs.Enemies.Inferno
{
    public class BlazePhoenix : ModNPC
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Blaze Phoenix");
            Main.npcFrameCount[npc.type] = 8;
        }

        public override void SetDefaults()
        {
			npc.width = 30;
			npc.height = 30;
            npc.aiStyle = -1;
            npc.npcSlots = 1;
            npc.value = BaseUtility.CalcValue(0, 1, 25, 0);
            npc.lifeMax = 300;
            npc.defense = 30;
            npc.noGravity = true;
			npc.noTileCollide = true;
			npc.knockBackResist = 0f;
			npc.buffImmune[BuffID.OnFire] = true;
			npc.buffImmune[BuffID.Daybreak] = true;
            npc.damage = 70;
        }

        public override void AI()
        {
			AIShadowflameGhost(npc, ref npc.ai, false, 660f, 0.3f, 10f, 0.2f, 6f, 5f, 12f, 0.4f, 0.4f, 0.95f, 5f);
			npc.spriteDirection = (npc.velocity.X > 0 ? -1 : 1);
			BaseMod.BaseAI.LookAt(npc.Center + npc.velocity, npc, 0);
            npc.frameCounter++;
            if (npc.frameCounter > 3)
            {
                npc.frameCounter = 0;
                npc.frame.Y += 76;
                if (npc.frame.Y > 76 * 7)
                {
                    npc.frame.Y = 0;
                }
            }
        }
		
		public static void AIShadowflameGhost(NPC npc, ref float[] ai, bool speedupOverTime = false, float distanceBeforeTakeoff = 660f, float velIntervalX = 0.3f, float velMaxX = 7f, float velIntervalY = 0.2f, float velMaxY = 4f, float velScalarY = 4f, float velScalarYMax = 15f, float velIntervalXTurn = 0.4f, float velIntervalYTurn = 0.4f, float velIntervalScalar = 0.95f, float velIntervalMaxTurn = 5f)
		{
			int npcAvoidCollision;
			for (int m = 0; m < 200; m = npcAvoidCollision + 1)
			{
				if (m != npc.whoAmI && Main.npc[m].active && Main.npc[m].type == npc.type)
				{
					Vector2 dist = Main.npc[m].Center - npc.Center;
					if (dist.Length() < 50f)
					{
						dist.Normalize();
						if (dist.X == 0f && dist.Y == 0f)
						{
							if (m > npc.whoAmI)
								dist.X = 1f;
							else
								dist.X = -1f;
						}
						dist *= 0.4f;
						npc.velocity -= dist;
						Main.npc[m].velocity += dist;
					}
				}
				npcAvoidCollision = m;
			}
			if (speedupOverTime)
			{
				float timerMax = 120f;
				if (npc.localAI[0] < timerMax)
				{
					if (npc.localAI[0] == 0f)
					{
						Main.PlaySound(SoundID.Item8, npc.Center);
						npc.TargetClosest(true);
						if (npc.direction > 0)
						{
							npc.velocity.X = npc.velocity.X + 2f;
						}
						else
						{
							npc.velocity.X = npc.velocity.X - 2f;
						}
						for (int m = 0; m < 20; m = npcAvoidCollision + 1)
						{
							npcAvoidCollision = m;
						}
					}
					npc.localAI[0] += 1f;
					float timerPartial = 1f - npc.localAI[0] / timerMax;
					float timerPartialTimes20 = timerPartial * 20f;
					int nextNPC = 0;
					while ((float)nextNPC < timerPartialTimes20)
					{
						npcAvoidCollision = nextNPC;
						nextNPC = npcAvoidCollision + 1;
					}
				}
			}
			if (npc.ai[0] == 0f)
			{
				npc.TargetClosest(true);
				npc.ai[0] = 1f;
				npc.ai[1] = (float)npc.direction;
			}
			else if (npc.ai[0] == 1f)
			{
				npc.TargetClosest(true);
				npc.velocity.X = npc.velocity.X + npc.ai[1] * velIntervalX;
				
				if (npc.velocity.X > velMaxX)
					npc.velocity.X = velMaxX;
				else if (npc.velocity.X < -velMaxX)
					npc.velocity.X = -velMaxX;

				float playerDistY = Main.player[npc.target].Center.Y - npc.Center.Y;
				if (Math.Abs(playerDistY) > velMaxY)
					velScalarY = velScalarYMax;

				if (playerDistY > velMaxY) 
					playerDistY = velMaxY;
				else if (playerDistY < -velMaxY) 
					playerDistY = -velMaxY;
				
				npc.velocity.Y = (npc.velocity.Y * (velScalarY - 1f) + playerDistY) / velScalarY;
				if ((npc.ai[1] > 0f && Main.player[npc.target].Center.X - npc.Center.X < -distanceBeforeTakeoff) || (npc.ai[1] < 0f && Main.player[npc.target].Center.X - npc.Center.X > distanceBeforeTakeoff))
				{
					npc.ai[0] = 2f;
					npc.ai[1] = 0f;
					if (npc.Center.Y + 20f > Main.player[npc.target].Center.Y)
						npc.ai[1] = -1f;
					else
						npc.ai[1] = 1f;
				}
			}
			else if (npc.ai[0] == 2f)
			{
				npc.velocity.Y = npc.velocity.Y + npc.ai[1] * velIntervalYTurn;
				
				if (npc.velocity.Length() > velIntervalMaxTurn)
					npc.velocity *= velIntervalScalar;
				
				if (npc.velocity.X > -1f && npc.velocity.X < 1f)
				{
					npc.TargetClosest(true);
					npc.ai[0] = 3f;
					npc.ai[1] = (float)npc.direction;
				}
			}
			else if (npc.ai[0] == 3f)
			{
				npc.velocity.X = npc.velocity.X + npc.ai[1] * velIntervalXTurn;
				
				if (npc.Center.Y > Main.player[npc.target].Center.Y)
					npc.velocity.Y = npc.velocity.Y - velIntervalY;
				else
					npc.velocity.Y = npc.velocity.Y + velIntervalY;
				
				if (npc.velocity.Length() > velIntervalMaxTurn)
					npc.velocity *= velIntervalScalar;
				
				if (npc.velocity.Y > -1f && npc.velocity.Y < 1f)
				{
					npc.TargetClosest(true);
					npc.ai[0] = 0f;
					npc.ai[1] = (float)npc.direction;
				}
			}																
		}		

        public Color GetGlowAlpha()
        {
            return new Color(220, 150, 150) * ((float)Main.mouseTextColor / 255f);
        }

        public override float SpawnChance(NPCSpawnInfo spawnInfo)
        {
            return spawnInfo.player.GetModPlayer<AAPlayer>(mod).ZoneInferno && Main.dayTime && Main.hardMode ? .8f : 0f;
        }

        public override void NPCLoot()
        {
			if(Main.netMode != 1)
			{
				Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("Dragonfire"), 1 + Main.rand.Next(2));
			}
        }

        public float auraPercent = 0f;
        public bool auraDirection = true;

        public override bool PreDraw(SpriteBatch spritebatch, Color dColor)
        {
            if (auraDirection) { auraPercent += 0.1f; auraDirection = auraPercent < 1f; }
            else { auraPercent -= 0.1f; auraDirection = auraPercent <= 0f; }
            BaseDrawing.DrawAfterimage(spritebatch, Main.npcTexture[npc.type], 0, npc, 0.8f, 1f, 4, false, 0f, 0f, GetGlowAlpha());
            BaseDrawing.DrawTexture(spritebatch, Main.npcTexture[npc.type], 0, npc, Color.White);			
            return false;
        }

        public override void OnHitPlayer(Player target, int damage, bool crit)
        {
            target.AddBuff(mod.BuffType("DragonFire"), 600);
        }

        public override void HitEffect(int hitDirection, double damage)
        {
			bool isDead = npc.life <= 0;
            if (isDead)
            {
				for (int m = 0; m < 30; m++)
				{
					int dustID = Dust.NewDust(new Vector2(npc.Center.X, npc.Center.Y), npc.width, 1, DustID.Fire, -npc.velocity.X * 0.2f,
						-npc.velocity.Y * 0.2f, 100, default(Color), 2f);
					Main.dust[dustID].velocity *= 2f;
					dustID = Dust.NewDust(new Vector2(npc.Center.X, npc.Center.Y), npc.width, npc.height, mod.DustType<Dusts.BroodmotherDust>(), -npc.velocity.X * 0.2f,
						-npc.velocity.Y * 0.2f, 100, default(Color));
					Main.dust[dustID].velocity *= 2f;
				}
            }
			for (int m = 0; m < 5; m++)
			{
				Dust.NewDust(npc.position, npc.width, npc.height, DustID.Fire, npc.velocity.X * 0.2f, npc.velocity.Y * 0.2f, 100, Color.White, 1.3f);
			}
        }	
    }
}
