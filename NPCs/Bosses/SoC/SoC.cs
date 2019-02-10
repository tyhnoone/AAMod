using System;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.Audio;
using Terraria.ModLoader;
using BaseMod;
using AAMod.NPCs.Bosses.SoC.Bosses;
using Terraria.Graphics.Shaders;

namespace AAMod.NPCs.Bosses.SoC
{
    [AutoloadBossHead]
    public class SoC : ModNPC
    {

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Soul of Cthulhu");
        }

        public override void SetDefaults()
        {
            npc.npcSlots = 100;
            npc.width = 54;
            npc.height = 54;
            npc.aiStyle = -1;
            npc.damage = 100;
            npc.defense = 150;
            npc.lifeMax = 1000000;
            npc.value = Item.buyPrice(35, 0, 0, 0);
            npc.DeathSound = new LegacySoundStyle(2, 88, Terraria.Audio.SoundType.Sound);
            npc.knockBackResist = 0f;
            npc.boss = true;
            music = mod.GetSoundSlot(Terraria.ModLoader.SoundType.Music, "Sounds/Music/SoC");
            npc.noGravity = true;
            npc.netAlways = true;
            for (int m = 0; m < npc.buffImmune.Length; m++) npc.buffImmune[m] = true;
        }

        public bool LeaveLine = false;
        public bool Leviathan = false;
        public bool Summon = false;

        public float Rotation = 0;
        public float AlphaTimer = 0;
        public float alpha = 255;
        public float scale = 0;
        public float RingRotation = 0;
        public float morphTimer = 0;
        public float RiftSpin = 0;
        public bool Morphed = false;
        public static bool ComeBack = false;
        public int ReturnTimer = 100;

        public float[] customAI = new float[4];
        public override void SendExtraAI(BinaryWriter writer)
        {
            base.SendExtraAI(writer);
            if ((Main.netMode == 2 || Main.dedServ))
            {
                writer.Write((short)customAI[0]);
                writer.Write((short)customAI[1]);
                writer.Write((short)customAI[2]);
                writer.Write((short)customAI[3]);
            }
        }

        public override void ReceiveExtraAI(BinaryReader reader)
        {
            base.ReceiveExtraAI(reader);
            if (Main.netMode == 1)
            {
                customAI[0] = reader.ReadFloat();
                customAI[1] = reader.ReadFloat();
                customAI[2] = reader.ReadFloat();
                customAI[3] = reader.ReadFloat();
            }
        }

        public override bool? DrawHealthBar(byte hbPosition, ref float scale, ref Vector2 position)
        {
            scale = 1.5f;
            return null;
        }

        public override void NPCLoot()
        {
            if (Main.expertMode)
            {
                NPC.NewNPC((int)npc.Center.X, (int)npc.Center.Y, mod.NPCType<CthulhuPortal>(), 0, 0);
            }
            else
            {
                npc.DropLoot(mod.ItemType("RealityBar"), 25, 35);
                string[] lootTable =
                {
                    "RealityAnchor",
                    "SquidStorm",
                    "CthulhuCannon",
                    "GalacticStormspike",
                };
                AAWorld.downedSoC = true;
                int loot = Main.rand.Next(lootTable.Length);
                npc.DropLoot(mod.ItemType(lootTable[loot]));
            }
        }

        int oneTime = 0;
        public float moveSpeed = 6f;
        public int EnemyTimer = 0;

        public override void AI()
        {
            Player player = Main.player[npc.target];
            AAPlayer modPlayer = Main.player[npc.target].GetModPlayer<AAPlayer>();
            modPlayer.Leave = false;
            npc.rotation = npc.velocity.X / 15f;
            Vector2 spawnAt = npc.Center + new Vector2(0f, npc.height / 2f);
            float EyeSummon = npc.lifeMax * .85f;
            float EaterSummon = npc.lifeMax * .70f;
            float BrainSummon = npc.lifeMax * .55f;
            float SkullSummon = npc.lifeMax * .40f;
            float RoseSummon = npc.lifeMax * .25f;
            float LeviathanSummon = npc.lifeMax * .10f;
            bool BossAlive = NPC.AnyNPCs(mod.NPCType<DeityEye>()) || NPC.AnyNPCs(mod.NPCType<DeityEater>()) || NPC.AnyNPCs(mod.NPCType<DeityBrain>()) || NPC.AnyNPCs(mod.NPCType<DeitySkull>()) || NPC.AnyNPCs(mod.NPCType<DeityLeviathan>()) || NPC.AnyNPCs(mod.NPCType<DeityRose>());
            npc.ai[3]++;
            customAI[3]++;
            if (npc.ai[3] >= 600 && !BossAlive)
            {
                NPC.NewNPC((int)spawnAt.X, (int)spawnAt.Y, mod.NPCType("Portal"), 0, -npc.velocity.X, -npc.velocity.Y);
                npc.ai[3] = 0;
            }


            if (oneTime == 0)
            {
                RainStart();
                oneTime++;
            }

            if (BossAlive || npc.ai[1] == 2)
            {
                npc.alpha += 12;
                if (npc.alpha >= 140)
                {
                    npc.alpha = 140;
                }
                npc.dontTakeDamage = true;
                npc.damage = 0;
                if (BossAlive)
                {
                    MoveToPoint(new Vector2(player.Center.X, player.Center.Y - 60));
                    return;
                }
            }
            else
            {
                npc.alpha -= 30;
                if (npc.alpha <= 0)
                {
                    npc.alpha = 0;
                }
                npc.damage = 100;
                npc.dontTakeDamage = false;
            }

            if (npc.life < EyeSummon && customAI[2] == 0) //Spawn Eye boi
            {
                customAI[2] = 1;
                npc.ai[1] = 2f;
                npc.dontTakeDamage = true;
                customAI[3] = 0;
            }
            else if (npc.life < EaterSummon && customAI[2] == 1)
            {
                customAI[2] = 2;
                npc.ai[1] = 2f;
                npc.dontTakeDamage = true;
                customAI[3] = 0;
            }
            else if (npc.life < BrainSummon && customAI[2] == 2)
            {
                customAI[2] = 3;
                npc.ai[1] = 2f;
                npc.dontTakeDamage = true;
                customAI[3] = 0;
            }
            else if (npc.life < SkullSummon && customAI[2] == 3)
            {
                customAI[2] = 4;
                npc.ai[1] = 2f;
                npc.dontTakeDamage = true;
                customAI[3] = 0;
            }
            else if (npc.life < RoseSummon && customAI[2] == 4)
            {
                customAI[2] = 5;
                npc.ai[1] = 2f;
                npc.dontTakeDamage = true;
                customAI[3] = 0;
            }
            else if (npc.life < LeviathanSummon && customAI[2] == 5)
            {
                customAI[2] = 6;
                npc.ai[1] = 2f;
                npc.dontTakeDamage = true;
                customAI[3] = 0;
            }
            
            if (Main.player[npc.target].dead)
            {
                npc.TargetClosest(true);
                if (Main.player[npc.target].dead)
                {
                    npc.ai[1] = 3f;
                }
            }
            if (Math.Abs(npc.position.X - Main.player[npc.target].position.X) > 6000f || Math.Abs(npc.position.Y - Main.player[npc.target].position.Y) > 6000f)
            {
                npc.TargetClosest(true);
                if (Main.player[npc.target].dead)
                {
                    npc.ai[1] = 4f;
                }
            }
            if (npc.ai[1] == 0f)
            {
                npc.damage = 100;

                Rotation += npc.velocity.X * .01f;
                RiftSpin -= npc.velocity.X * .01f;

                
                npc.rotation = npc.velocity.X / 15f;
                if (npc.position.Y > Main.player[npc.target].position.Y - 200f)
                {
                    if (npc.velocity.Y > 0f)
                    {
                        npc.velocity.Y = npc.velocity.Y * 0.98f;
                    }
                    npc.velocity.Y = npc.velocity.Y - 0.1f;
                    if (npc.velocity.Y > 2f)
                    {
                        npc.velocity.Y = 2f;
                    }
                }
                else if (npc.position.Y < Main.player[npc.target].position.Y - 300f)
                {
                    if (npc.velocity.Y < 0f)
                    {
                        npc.velocity.Y = npc.velocity.Y * 0.98f;
                    }
                    npc.velocity.Y = npc.velocity.Y + 0.1f;
                    if (npc.velocity.Y < -2f)
                    {
                        npc.velocity.Y = -2f;
                    }
                }
                if (npc.position.X + (float)(npc.width / 2) > Main.player[npc.target].position.X + (float)(Main.player[npc.target].width / 2) + 100f)
                {
                    if (npc.velocity.X > 0f)
                    {
                        npc.velocity.X = npc.velocity.X * 0.98f;
                    }
                    npc.velocity.X = npc.velocity.X - 0.1f;
                    if (npc.velocity.X > 8f)
                    {
                        npc.velocity.X = 8f;
                    }
                }
                if (npc.position.X + (float)(npc.width / 2) < Main.player[npc.target].position.X + (float)(Main.player[npc.target].width / 2) - 100f)
                {
                    if (npc.velocity.X < 0f)
                    {
                        npc.velocity.X = npc.velocity.X * 0.98f;
                    }
                    npc.velocity.X = npc.velocity.X + 0.1f;
                    if (npc.velocity.X < -8f)
                    {
                        npc.velocity.X = -8f;
                        return;
                    }
                }
                npc.ai[2] += 1f;
                if (npc.ai[2] >= 600f)
                {
                    npc.ai[2] = 0f;
                    npc.ai[1] = 1f;
                    npc.TargetClosest(true);
                    npc.netUpdate = true;
                }
            }
            else
            {
                if (npc.ai[1] == 1f)
                {
                    npc.defense = 180;
                    npc.damage = 200;
                    npc.ai[2] += 1f;
                    if (npc.ai[2] == 2f)
                    {
                        Main.PlaySound(29, (int)npc.Center.X, (int)npc.Center.Y, 92);
                    }
                    if (npc.ai[2] >= 400f)
                    {
                        npc.ai[2] = 0f;
                        npc.ai[1] = 0f;
                    }
                    npc.rotation += npc.direction * 0.7f;
                    Vector2 vector44 = new Vector2(npc.position.X + ((float)npc.width * 0.5f), npc.position.Y + ((float)npc.height * 0.5f));
                    float num441 = Main.player[npc.target].position.X + (float)(Main.player[npc.target].width / 2) - vector44.X;
                    float num442 = Main.player[npc.target].position.Y + (float)(Main.player[npc.target].height / 2) - vector44.Y;
                    float num443 = (float)Math.Sqrt((double)((num441 * num441) + (num442 * num442)));
                    float num4 = 5f + num443 / 100f;
                    if (num4 < 8.0)
                        num4 = 8f;
                    if (num4 > 32.0)
                        num4 = 32f;
                    float num5 = num4 / num443;
                    npc.velocity.X = num441 * num5;
                    npc.velocity.Y = num442 * num5;
                    Rotation += npc.velocity.X * .08f;
                    RiftSpin -= npc.velocity.X * .08f;
                    return;

                }
                if (npc.ai[1] == 2f)
                {
                    Summon = true;
                    npc.velocity *= .8f;
                    if (npc.velocity.X < .5f || npc.velocity.X > -.5f)
                    {
                        npc.velocity.X = 0;
                    }
                    if (npc.velocity.Y < .5f || npc.velocity.Y > -.5f)
                    {
                        npc.velocity.Y = 0;
                    }

                    if (npc.velocity.X == 0 && npc.velocity.Y == 0)
                    {

                        Rotation += .2f;
                        RiftSpin -= .2f;
                        customAI[1]++;

                        if (customAI[1] > 300)
                        {
                            if (customAI[0] == 0)
                            {
                                Summon = true;
                                customAI[0] = 1;
                                NPC.NewNPC((int)spawnAt.X, (int)spawnAt.Y, mod.NPCType("DeityEye"));
                                npc.ai[2] = 0f;
                                npc.ai[1] = 0f;
                                return;
                            }
                            if (customAI[0] == 1)
                            {
                                Summon = true;
                                customAI[0] = 2;
                                NPC.NewNPC((int)spawnAt.X, (int)spawnAt.Y, mod.NPCType("DeityEater"));
                                npc.ai[2] = 0f;
                                npc.ai[1] = 0f;
                                return;
                            }
                            if (customAI[0] == 2)
                            {
                                Summon = true;
                                customAI[0] = 3;
                                NPC.NewNPC((int)spawnAt.X, (int)spawnAt.Y, mod.NPCType("DeityBrain"));
                                npc.ai[2] = 0f;
                                npc.ai[1] = 0f;
                                return;
                            }
                            if (customAI[0] == 3)
                            {
                                Summon = true;
                                customAI[0] = 4;
                                NPC.NewNPC((int)spawnAt.X, (int)spawnAt.Y, mod.NPCType("DeitySkull"), 0, 0, 1);
                                npc.ai[2] = 0f;
                                npc.ai[1] = 0f;
                                return;
                            }
                            if (customAI[0] == 4)
                            {
                                Summon = true;
                                customAI[0] = 5;
                                NPC.NewNPC((int)spawnAt.X, (int)spawnAt.Y, mod.NPCType("DeityRose"));
                                npc.ai[2] = 0f;
                                npc.ai[1] = 0f;
                                return;
                            }
                            if (customAI[0] == 5)
                            {
                                Summon = true;
                                customAI[0] = 6;
                                NPC.NewNPC((int)spawnAt.X, (int)spawnAt.Y, mod.NPCType("DeityLeviathan"));
                                npc.ai[2] = 0f;
                                npc.ai[1] = 0f;
                                return;
                            }
                        }
                    }
                    return;
                }
                if (npc.ai[1] == 3f)
                {
                    Main.NewText("...good riddance...", Color.DarkCyan);
                    npc.ai[1] = 5f;
                }
                if (npc.ai[1] == 4f)
                {
                    Main.NewText("...do not return...", Color.DarkCyan);
                    npc.ai[1] = 5f;
                }
                if (npc.ai[1] == 5f)
                {
                    npc.alpha += 5;
                    {
                        if (npc.alpha >= 255)
                        {
                            npc.active = false;
                        }
                    }
                }
            }
        }

        public override bool StrikeNPC(ref double damage, int defense, ref float knockback, int hitDirection, ref bool crit)
        {
            if (AAWorld.Anticheat == true)
            {
                if (damage > npc.lifeMax / 8)
                {
                    Main.NewText("YOU CANNOT CHEAT DEATH", Color.DarkCyan);
                    damage = 0;
                }

                return false;
            }

            return true;
        }


        public override bool PreDraw(SpriteBatch spriteBatch, Color drawColor)
        {
            Texture2D texture2D13 = Main.npcTexture[npc.type];
            Texture2D WheelTex = mod.GetTexture("NPCs/Bosses/SoC/SoC_Wheel");
            Texture2D RingTex = mod.GetTexture("NPCs/Bosses/SoC/DeityCircle");
            Texture2D RitualTex = mod.GetTexture("NPCs/Bosses/SoC/DeityRitual");
            Texture2D Rift = mod.GetTexture("NPCs/Bosses/SoC/Rift");
            Texture2D GlowTex = mod.GetTexture("Glowmasks/SoC_Glow");
            Vector2 vector38 = npc.position + new Vector2(npc.width, npc.height) / 2f + Vector2.UnitY * npc.gfxOffY - Main.screenPosition;
            Vector2 origin8 = new Vector2((float)RitualTex.Width, (float)RitualTex.Height) / 2f;
            int num214 = Main.npcTexture[npc.type].Height;
            Color color25 = Lighting.GetColor((int)(npc.position.X + npc.width * 0.5) / 16, (int)((npc.position.Y + npc.height * 0.5) / 16.0));
            Color? alpha4 = GetAlpha(color25);
            Vector2 drawCenter = new Vector2(npc.Center.X, npc.Center.Y);
            bool BossAlive = NPC.AnyNPCs(mod.NPCType<DeityEye>()) || NPC.AnyNPCs(mod.NPCType<DeityEater>()) || NPC.AnyNPCs(mod.NPCType<DeityBrain>()) || NPC.AnyNPCs(mod.NPCType<DeitySkull>()) || NPC.AnyNPCs(mod.NPCType<DeityLeviathan>()) || NPC.AnyNPCs(mod.NPCType<DeityRose>());
            if (Summon)
            {
                Rotation += .2f;
                RiftSpin -= .2f;
                if (customAI[3] < 300f)
                {
                    alpha -= 5;
                }
                else
                {
                    alpha += 12;
                }
                if (alpha < 0)
                {
                    alpha = 0;
                }
                if (alpha > 255)
                {
                    alpha = 255;
                }
                scale = 1f - alpha / 255f;
                RingRotation += 0.0149599658f;
                Main.spriteBatch.Draw(RingTex, vector38, null, AAColor.Cthulhu, -RingRotation, RingTex.Size() / 2f, scale, SpriteEffects.None, 0f);
                Main.spriteBatch.Draw(RitualTex, vector38, null, AAColor.Cthulhu, RingRotation, origin8, scale, SpriteEffects.None, 0f);
                Main.spriteBatch.Draw(RingTex, vector38, null, AAColor.Cthulhu, -RingRotation, RingTex.Size() / 2f, scale * 0.42f, SpriteEffects.None, 0f);
            }

            int shader = 0;

            if (BossAlive)
            {
                shader = GameShaders.Armor.GetShaderIdFromItemId(ItemID.LivingOceanDye);
            }
            else
            {
                shader = 0;
            }
            BaseDrawing.DrawTexture(spriteBatch, Rift, 0, npc.position, npc.width, npc.height, 1.5f, RiftSpin, 0, 1, new Rectangle(0, 0, Rift.Width, Rift.Height), AAColor.Cthulhu, true);

            BaseDrawing.DrawTexture(spriteBatch, WheelTex, shader, npc.position, npc.width, npc.height, npc.scale, Rotation, 0, 1, new Rectangle(0, 0, WheelTex.Width, WheelTex.Height), drawColor, true);

            BaseDrawing.DrawTexture(spriteBatch, texture2D13, shader, npc.position, npc.width, npc.height, npc.scale, npc.rotation, 0, 1, new Rectangle(0, 0, texture2D13.Width, texture2D13.Height), drawColor, true);

            if (BossAlive || Summon)
            {
                BaseDrawing.DrawTexture(spriteBatch, GlowTex, 0, npc.position, npc.width, npc.height, npc.scale, npc.rotation, 0, 1, new Rectangle(0, 0, GlowTex.Width, GlowTex.Height), AAColor.Cthulhu, true);

                BaseDrawing.DrawAfterimage(spriteBatch, GlowTex, 0, npc, 0.8f, 1f, 6, false, 0f, 0f, AAColor.Cthulhu);
            }

            return false;
        }

        public void MoveToPoint(Vector2 point)
        {
            float velMultiplier = 1f;
            Vector2 dist = point - npc.Center;
            float length = dist.Length();
            if (length < moveSpeed)
            {
                velMultiplier = MathHelper.Lerp(0f, 1f, dist.Length() / moveSpeed);
            }
            npc.velocity = Vector2.Normalize(point - npc.Center);
            npc.velocity *= moveSpeed;
            npc.velocity *= velMultiplier;
            if (length < 200f)
            {
                npc.velocity *= 0.9f;
            }
            if (length < 150f)
            {
                npc.velocity *= 0.9f;
            }
            if (length < 100f)
            {
                npc.velocity *= 0.8f;
            }
            if (length < 50f)
            {
                npc.velocity *= 0.8f;
            }
        }

        private void RainStart()
        {
            if (!Main.raining)
            {
                int num = 86400;
                int num2 = num / 24;
                Main.rainTime = Main.rand.Next(num2 * 8, num);
                if (Main.rand.Next(3) == 0)
                {
                    Main.rainTime += Main.rand.Next(0, num2);
                }
                if (Main.rand.Next(4) == 0)
                {
                    Main.rainTime += Main.rand.Next(0, num2 * 2);
                }
                if (Main.rand.Next(5) == 0)
                {
                    Main.rainTime += Main.rand.Next(0, num2 * 2);
                }
                if (Main.rand.Next(6) == 0)
                {
                    Main.rainTime += Main.rand.Next(0, num2 * 3);
                }
                if (Main.rand.Next(7) == 0)
                {
                    Main.rainTime += Main.rand.Next(0, num2 * 4);
                }
                if (Main.rand.Next(8) == 0)
                {
                    Main.rainTime += Main.rand.Next(0, num2 * 5);
                }
                float num3 = 1f;
                if (Main.rand.Next(2) == 0)
                {
                    num3 += 0.05f;
                }
                if (Main.rand.Next(3) == 0)
                {
                    num3 += 0.1f;
                }
                if (Main.rand.Next(4) == 0)
                {
                    num3 += 0.15f;
                }
                if (Main.rand.Next(5) == 0)
                {
                    num3 += 0.2f;
                }
                Main.rainTime = (int)((float)Main.rainTime * num3);
                Main.raining = true;
                if (Main.netMode == 2)
                {
                    NetMessage.SendData(7, -1, -1, null, 0, 0f, 0f, 0f, 0, 0, 0);
                }
            }
        }
    }
}