using Terraria;
using Terraria.ModLoader;
using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using BaseMod;
using Terraria.ID;
using Terraria.Audio;
using System.IO;

namespace AAMod.NPCs.Bosses.Hydra
{
    [AutoloadBossHead]
    public class HydraHead1 : ModNPC
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Hydra");
            Main.npcFrameCount[npc.type] = 2;
            NPCID.Sets.TechnicallyABoss[npc.type] = true;
        }

        public override void SetDefaults()
        {
            base.SetDefaults();
            npc.lifeMax = 1300;
            npc.width = 46;
            npc.height = 46;
            npc.damage = 40;
            npc.npcSlots = 0;
            npc.dontCountMe = true;
            npc.noTileCollide = true;
            npc.boss = false;
            npc.HitSound = SoundID.NPCHit1;
            npc.DeathSound = new LegacySoundStyle(2, 88, Terraria.Audio.SoundType.Sound);
            npc.noGravity = true;
            for (int k = 0; k < npc.buffImmune.Length; k++)
            {
                npc.buffImmune[k] = true;
            }
			leftHead = false;
			middleHead = true;
        }

        public bool SetLife = false;
        public override void SendExtraAI(BinaryWriter writer)
        {
            base.SendExtraAI(writer);
            if (Main.netMode == 2 || Main.dedServ)
            {
                writer.Write(SetLife);
            }
        }

        public override void ReceiveExtraAI(BinaryReader reader)
        {
            base.ReceiveExtraAI(reader);
            if (Main.netMode == 1)
            {
                SetLife = reader.ReadBool();
            }
        }

        public override void ScaleExpertStats(int numPlayers, float bossLifeScale)
        {
            npc.lifeMax = (int)(npc.lifeMax * 0.6f * bossLifeScale);
            npc.damage = (int)(npc.damage * 0.6f);
        }

        public override float SpawnChance(NPCSpawnInfo spawnInfo)
        {
            return 0f;
        }

        public override void NPCLoot()
        {
            if (npc.type == mod.NPCType("HydraHead2"))
            {
                Gore.NewGore(npc.position, npc.velocity, mod.GetGoreSlot("Gores/HydraHeadGoreB"), 1f);
            }
            else
            {
                Gore.NewGore(npc.position, npc.velocity, mod.GetGoreSlot("Gores/HydraHeadGoreR"), 1f);
            }
            Gore.NewGore(npc.position, npc.velocity, mod.GetGoreSlot("Gores/HydraHeadGore1"), 1f);
            Gore.NewGore(npc.position, npc.velocity, mod.GetGoreSlot("Gores/HydraHeadGore2"), 1f);
            Gore.NewGore(npc.position, npc.velocity, mod.GetGoreSlot("Gores/HydraHeadGore3"), 1f);
            Gore.NewGore(npc.position, npc.velocity, mod.GetGoreSlot("Gores/HydraHeadGore4"), 1f);
        }

        public Hydra Body => (bodyNPC != null && bodyNPC.modNPC is Hydra) ? (Hydra)bodyNPC.modNPC : null;
        public NPC bodyNPC = null;	
        public bool middleHead = false;
        public bool leftHead = false;
        public int damage = 0;

        public int distFromBodyX = 60; //how far from the body to centeralize the movement points. (X coord)
        public int distFromBodyY = 90; //how far from the body to centeralize the movement points. (Y coord)
        public int movementVariance = 40; //how far from the center point to move.
        public bool fireAttack = false;

        public override void AI()
        {
	        if (bodyNPC == null)
            {
                NPC npcBody = Main.npc[(int)npc.ai[0]];
                if (npcBody.type == mod.NPCType<Hydra>())
                {
                    bodyNPC = npcBody;
                }
            }
			if(bodyNPC == null)
				return;
            if (!SetLife && Main.netMode != 1)
            {
                npc.lifeMax = bodyNPC.life / 3;
                npc.life = bodyNPC.life / 3;
                SetLife = true;
                npc.netUpdate = true;
            }
            if (!bodyNPC.active)
            {
                if (Main.netMode != 1) //force a kill to prevent 'ghosting'
                {
                    npc.life = 0;
                    npc.checkDead();
                    npc.netUpdate = true;
                }
                return;
            }			
			
            npc.timeLeft = 100;

            npc.TargetClosest();
            
            Player targetPlayer = Main.player[npc.target];

            if (targetPlayer == null || !targetPlayer.active || targetPlayer.dead) targetPlayer = null; //deliberately set to null


            if (!targetPlayer.GetModPlayer<AAPlayer>(mod).ZoneMire)
            {
                npc.damage = 80;
                npc.defense = 100;
            }
            else
            {
                npc.damage = 40;
                npc.defense = 0;
            }
            if (Main.expertMode)
            {
                damage = npc.damage / 4;
            }
            else
            {
                damage = npc.damage / 2;
            }

            if (Main.netMode != 1)
            {
                npc.ai[1]++;
                int aiTimerFire = npc.whoAmI % 3 == 0 ? 50 : npc.whoAmI % 2 == 0 ? 150 : 100; //aiTimerFire is different per head by using whoAmI (which is usually different) 
                if (leftHead) aiTimerFire += 30;
                if (middleHead)
                {
                    aiTimerFire = 100;
                }

                if (targetPlayer != null)
                {
                    Vector2 dir = Vector2.Normalize(targetPlayer.Center - npc.Center);
                    if (!middleHead && npc.ai[1] == aiTimerFire)
                    {
                        dir *= 9f;
                        for (int i = 0; i < 7; ++i)
                        {
                            int projID = Projectile.NewProjectile(npc.Center.X, npc.Center.Y, dir.X, dir.Y, mod.ProjectileType("AcidProj"), damage, 0f, Main.myPlayer);
                            Main.projectile[projID].netUpdate = true;
                        }
                    }
                    else
                    if (middleHead && npc.ai[1] >= 100 && npc.ai[1] % 10 == 0)
                    {
                        dir *= 6f;
                        dir.X += Main.rand.Next(-1, 1) * 0.5f;
                        dir.Y += Main.rand.Next(-1, 1) * 0.5f;
                        int projID = Projectile.NewProjectile(npc.Center.X, npc.Center.Y, dir.X, dir.Y, mod.ProjectileType("HydraBreath"), damage, 0f, Main.myPlayer);
                        Main.projectile[projID].netUpdate = true;
                    }
                }
                if (npc.ai[1] >= 200) //pick random spot to move head to
                {
                    npc.ai[1] = 0;
                    npc.ai[2] = Main.rand.Next(-movementVariance, movementVariance);
                    npc.ai[3] = Main.rand.Next(-movementVariance, movementVariance);
                    npc.netUpdate = true;
                }
            }
            Vector2 nextTarget = Body.npc.Center + new Vector2(middleHead ? 0 : leftHead ? -distFromBodyX : distFromBodyX, -distFromBodyY) + new Vector2(npc.ai[2], npc.ai[3]);
			float dist = Vector2.Distance(nextTarget, npc.Center);
            if (dist < 40f)
            {
                npc.velocity *= 0.9f;
                if (Math.Abs(npc.velocity.X) < 0.05f) npc.velocity.X = 0f;
                if (Math.Abs(npc.velocity.Y) < 0.05f) npc.velocity.Y = 0f;
            }else
            if (dist > 200f) //teleport to keep up with body
            {
                npc.Center = Body.npc.Center;
				npc.netUpdate = true;
            }	
            else
            {
                npc.velocity = Vector2.Normalize(nextTarget - npc.Center);
                npc.velocity *= 5f;
            }
            if (dist < 40f)
            {
                npc.velocity *= 0.9f;
                if (Math.Abs(npc.velocity.X) < 0.05f) npc.velocity.X = 0f;
                if (Math.Abs(npc.velocity.Y) < 0.05f) npc.velocity.Y = 0f;
            }
            else
            {
                npc.velocity = Vector2.Normalize(nextTarget - npc.Center);
                npc.velocity *= 5f;
            }
            npc.position += Body.npc.position - Body.npc.oldPosition;
            npc.position += bodyNPC.velocity;
            npc.rotation = 1.57f;
            npc.spriteDirection = -1;
            BaseDrawing.AddLight(npc.Center, leftHead ? new Color(255, 84, 84) : new Color(48, 232, 232));
        }

        public float moveSpeed = 16f; 
        public void MoveToPoint(Vector2 point)
        {
            float velMultiplier = 1f;
            Vector2 dist = point - npc.Center;
            float length = dist == Vector2.Zero ? 0f : dist.Length();
            if (length < moveSpeed)
            {
                velMultiplier = MathHelper.Lerp(0f, 1f, length / moveSpeed);
            }
            npc.velocity = length == 0f ? Vector2.Zero : Vector2.Normalize(dist);
            npc.velocity *= moveSpeed;
            npc.velocity *= velMultiplier;
        }

        public override bool PreDraw(SpriteBatch sb, Color lightColor)
        {
            return false;
        }

        public override void BossHeadRotation(ref float rotation)
        {
            rotation = npc.rotation;
        }

        public override bool PreNPCLoot()
        {
            return false;
        }

        public override void HitEffect(int hitDir, double damage)
        {
            if (bodyNPC == null)
            {
                bodyNPC.life -= (int)damage;
            }
        }
    }
}
