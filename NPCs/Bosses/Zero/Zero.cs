using System;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using BaseMod;

namespace AAMod.NPCs.Bosses.Zero
{
    [AutoloadBossHead]	
    public class Zero : ModNPC
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Zero");
            Main.npcFrameCount[npc.type] = 2; 
        }

        public override void SetDefaults()
        {
            npc.damage = 100;
            npc.defense = 200;
            npc.lifeMax = 600000;
            if (Main.expertMode)
            {
                npc.value = 0;
            }
            else
            {
                npc.value = 120000f;
            }
            npc.width = 206;
            npc.height = 208;
            npc.aiStyle = -1;
            npc.HitSound = SoundID.NPCHit4;
            npc.DeathSound = SoundID.NPCHit4;
            npc.noGravity = true;
            music = mod.GetSoundSlot(Terraria.ModLoader.SoundType.Music, "Sounds/Music/Zero");
            npc.noTileCollide = true;
            
            npc.knockBackResist = -1f;
            npc.boss = true;
            npc.friendly = false;
            npc.npcSlots = 0f;
            for (int k = 0; k < npc.buffImmune.Length; k++)
            {
                npc.buffImmune[k] = true;
            }
            npc.lavaImmune = true;
            npc.netAlways = true;
            musicPriority = MusicPriority.BossHigh;

            if (AAWorld.downedAllAncients)
            {
                npc.lifeMax = 700000;
                npc.damage = 140;
                npc.defense = 120;
            }
        }
        public override void SendExtraAI(BinaryWriter writer)
        {
            writer.Write((short)npc.localAI[0]);
            base.SendExtraAI(writer);
            if ((Main.netMode == 2 || Main.dedServ))
            {
                writer.Write((float)internalAI[0]);
                writer.Write((float)internalAI[1]);
                writer.Write((float)internalAI[2]);
                writer.Write((float)internalAI[3]);
            }
        }

        public override void ReceiveExtraAI(BinaryReader reader)
        {
            npc.localAI[0] = reader.ReadInt16();
            base.ReceiveExtraAI(reader);
            if (Main.netMode == 1)
            {
                internalAI[0] = reader.ReadFloat();
                internalAI[1] = reader.ReadFloat();
                internalAI[2] = reader.ReadFloat();
                internalAI[3] = reader.ReadFloat();
            }
        }



        public override bool? DrawHealthBar(byte hbPosition, ref float scale, ref Vector2 position)
        {
            scale = 1.5f;
            return null;
        }
        

        public override void HitEffect(int hitDirection, double damage)
        {
            if (npc.type == mod.NPCType<Zero>() && (NPC.AnyNPCs(mod.NPCType<VoidStar>()) || NPC.AnyNPCs(mod.NPCType<Taser>()) || NPC.AnyNPCs(mod.NPCType<RealityCannon>()) || NPC.AnyNPCs(mod.NPCType<RiftShredder>())))
            {
                npc.dontTakeDamage = true;
                npc.chaseable = false;
            }
            if (npc.life <= 0 && npc.type == mod.NPCType<Zero>())
            {
                Gore.NewGore(npc.position, npc.velocity, mod.GetGoreSlot("Gores/ZeroGore1"), 1f);
                Gore.NewGore(npc.position, npc.velocity, mod.GetGoreSlot("Gores/ZeroGore1"), 1f);
                Gore.NewGore(npc.position, npc.velocity, mod.GetGoreSlot("Gores/ZeroGore1"), 1f);
                Gore.NewGore(npc.position, npc.velocity, mod.GetGoreSlot("Gores/ZeroGore1"), 1f);
                Gore.NewGore(npc.position, npc.velocity, mod.GetGoreSlot("Gores/ZeroGore2"), 1f);
                Gore.NewGore(npc.position, npc.velocity, mod.GetGoreSlot("Gores/ZeroGore3"), 1f);
                Gore.NewGore(npc.position, npc.velocity, mod.GetGoreSlot("Gores/ZeroGore3"), 1f);
                Gore.NewGore(npc.position, npc.velocity, mod.GetGoreSlot("Gores/ZeroGore3"), 1f);
                Gore.NewGore(npc.position, npc.velocity, mod.GetGoreSlot("Gores/ZeroGore3"), 1f);
                npc.position.X = npc.position.X + (npc.width / 2);
                npc.position.Y = npc.position.Y + (npc.height / 2);
                npc.width = 100;
                npc.height = 100;
                npc.position.X = npc.position.X - (npc.width / 2);
                npc.position.Y = npc.position.Y - (npc.height / 2);
                Vector2 spawnAt = npc.Center + new Vector2(0f, npc.height / 2f);
                if (Main.expertMode)
                {
                    Main.NewText("PHYSICAL ZER0 UNIT IN CRITICAL C0NDITI0N. DISCARDING AND ENGAGING D00MSDAY PR0T0C0L.", Color.Red.R, Color.Red.G, Color.Red.B);
                    NPC.NewNPC((int)spawnAt.X, (int)spawnAt.Y, mod.NPCType("ZeroAwakened"));
                }
                if (!Main.expertMode)
                {
                    Main.NewText("D00MSDAY PR0T0CALL MALFUNCTI0N. MAIN.EXPERT M0DE = FALSE.", Color.Red.R, Color.Red.G, Color.Red.B);
                }
            }
        }

        public override void NPCLoot()
        {
            if (Main.expertMode)
            {
                npc.DropLoot(mod.ItemType("ApocalyptitePlate"), 2, 4);
            }
            else
            {
                if (!AAWorld.downedZero)
                {
                    Main.NewText("Doomstone stops glowing. You can now mine it.", Color.Silver);
                }
                AAWorld.downedZero = true;
                npc.DropLoot(mod.ItemType("ApocalyptitePlate"), 2, 4);
                npc.DropLoot(mod.ItemType("UnstableSingularity"), 25, 35);
                string[] lootTable =
                {
                    "Battery",
                    "ZeroArrow",
                    "Vortex",
                    "EventHorizon",
                    "RealityCannon",
                    "RiftShredder",
                    "VoidStar",
                    "TeslaHand",
                    "ZeroStar",
                    "Neutralizer",
                    "ZeroTerratool",
                    "DoomPortal"
                };
                int loot = Main.rand.Next(lootTable.Length);
                npc.DropLoot(mod.ItemType(lootTable[loot]));
                npc.DropLoot(Items.Vanity.Mask.ZeroMask.type, 1f / 7);
                npc.DropLoot(Items.Boss.Zero.ZeroTrophy.type, 1f / 10);
                npc.DropLoot(mod.ItemType<Items.Boss.EXSoul>(), 1f / 10);
                if (Main.rand.Next(50) == 0 && AAWorld.downedAllAncients)
                {
                    Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("RealityStone"));
                }
            }
        }
        
        public override void BossLoot(ref string name, ref int potionType)
        {
            if (!Main.expertMode)
            {
                potionType = ItemID.SuperHealingPotion;   //boss drops
            }
            else
            {
                potionType = 0;
            }
        }

        public Color GetGlowAlpha()
        {
            return AAColor.ZeroShield * (Main.mouseTextColor / 255f);
        }

        public bool saythelinezero = false;
        public bool ArmsGone = false;
        public float ShieldScale = 0.5f;
        public float RingRoatation = 0;

        public override bool PreDraw(SpriteBatch spritebatch, Color dColor)
        {
            Texture2D glowTex = mod.GetTexture("Glowmasks/Zero_Glow");

            if (npc.ai[0] != 0)
            {
                BaseDrawing.DrawAfterimage(spritebatch, Main.npcTexture[npc.type], 0, npc, 1.5f, 1f, 3, false, 0f, 0f, new Color(dColor.R, dColor.G, dColor.B, (byte)150));
            }
            
            Texture2D Shield = mod.GetTexture("NPCs/Bosses/Zero/ZeroShield");
            Texture2D Ring = mod.GetTexture("NPCs/Bosses/Zero/ZeroShieldRing");
            Texture2D RingGlow = mod.GetTexture("Glowmasks/ZeroShieldRing_Glow");
            Texture2D Vortex = mod.GetTexture("NPCs/Bosses/Zero/CycloneProtocol");

            if (VortexScale > 0)
            {
                BaseDrawing.DrawTexture(spritebatch, Vortex, 0, npc.position, npc.width, npc.height, VortexScale, 0, 0, 1, new Rectangle(0, 0, Vortex.Width, Vortex.Height), Color.White, true);
            }

            BaseDrawing.DrawTexture(spritebatch, Main.npcTexture[npc.type], 0, npc, dColor);
            BaseDrawing.DrawTexture(spritebatch, glowTex, 0, npc, GetGlowAlpha());

            if (ShieldScale > 0)
            {
                BaseDrawing.DrawTexture(spritebatch, Shield, 0, npc.position, npc.width, npc.height, ShieldScale, 0, 0, 1, new Rectangle(0, 0, Shield.Width, Shield.Height), GetGlowAlpha(), true);
                BaseDrawing.DrawTexture(spritebatch, Ring, 0, npc.position, npc.width, npc.height, ShieldScale * 2, RingRoatation, 0, 1, new Rectangle(0, 0, Ring.Width, Ring.Height), dColor, true);
                BaseDrawing.DrawTexture(spritebatch, RingGlow, 0, npc.position, npc.width, npc.height, ShieldScale * 2, RingRoatation, 0, 1, new Rectangle(0, 0, Ring.Width, Ring.Height), GetGlowAlpha(), true);
            }
            return false;
        }

        public float moveSpeed = 6f;
        public int MinionTimer = 0;
        public int LineStopper = 180;
        public Vector2 offsetBasePoint = new Vector2(-240f, 0f);
        public float[] internalAI = new float[4];
        public static int ChargeType = 0, XPos = 1, YPos = 2, PrepareCharge = 2;

        public override void AI()
        {
            if (Main.netMode != 1)
            {
                AAWorld.zeroUS = false;
            }
            LineStopper--;
            RingRoatation += 0.03f;
            
            npc.damage = npc.defDamage;
            npc.defense = npc.defDefense;
            if (npc.ai[0] == 0 && Main.netMode != 1)
            {
                npc.TargetClosest(true);
                npc.ai[0] = 1;
                int index1 = NPC.NewNPC((int)(npc.position.X + (double)(npc.width / 2)), (int)npc.position.Y + (npc.height / 2), mod.NPCType("VoidStar"), npc.whoAmI, 0.0f, 0.0f, 0.0f, 0.0f, byte.MaxValue);
                Main.npc[index1].ai[0] = -1f;
                Main.npc[index1].ai[1] = npc.whoAmI;
                Main.npc[index1].target = npc.target;
                Main.npc[index1].netUpdate = true;
                int index2 = NPC.NewNPC((int)(npc.position.X + (double)(npc.width / 2)), (int)npc.position.Y + (npc.height / 2), mod.NPCType("RiftShredder"), npc.whoAmI, 0.0f, 0.0f, 0.0f, 0.0f, byte.MaxValue);
                Main.npc[index2].ai[0] = 1f;
                Main.npc[index2].ai[1] = npc.whoAmI;
                Main.npc[index2].target = npc.target;
                Main.npc[index2].netUpdate = true;
                int index3 = NPC.NewNPC((int)(npc.position.X + (double)(npc.width / 2)), (int)npc.position.Y + (npc.height / 2), mod.NPCType("Taser"), npc.whoAmI, 0.0f, 0.0f, 0.0f, 0.0f, byte.MaxValue);
                Main.npc[index3].ai[0] = -1f;
                Main.npc[index3].ai[1] = npc.whoAmI;
                Main.npc[index3].target = npc.target;
                Main.npc[index3].ai[3] = 150f;
                Main.npc[index3].netUpdate = true;
                int index4 = NPC.NewNPC((int)(npc.position.X + (double)(npc.width / 2)), (int)npc.position.Y + (npc.height / 2), mod.NPCType("RealityCannon"), npc.whoAmI, 0.0f, 0.0f, 0.0f, 0.0f, byte.MaxValue);
                Main.npc[index4].ai[0] = 1f;
                Main.npc[index4].ai[1] = npc.whoAmI;
                Main.npc[index4].target = npc.target;
                Main.npc[index4].netUpdate = true;
                Main.npc[index4].ai[3] = 150f;
            }

            if (npc.life <= npc.lifeMax / 2 && npc.ai[0] == 1)
            {
                npc.TargetClosest(true);
                npc.ai[0] = 2;
                Main.NewText("INITIALIZING BACKUP WEAPON PR0T0C0L.", Color.Red.R, Color.Red.G, Color.Red.B);
                int index1 = NPC.NewNPC((int)(npc.position.X + (double)(npc.width / 2)), (int)npc.position.Y + (npc.height / 2), mod.NPCType("NovaFocus"), npc.whoAmI, 0.0f, 0.0f, 0.0f, 0.0f, byte.MaxValue);
                Main.npc[index1].ai[0] = -1f;
                Main.npc[index1].ai[1] = npc.whoAmI;
                Main.npc[index1].target = npc.target;
                Main.npc[index1].netUpdate = true;
                int index2 = NPC.NewNPC((int)(npc.position.X + (double)(npc.width / 2)), (int)npc.position.Y + (npc.height / 2), mod.NPCType("OmegaVolley"), npc.whoAmI, 0.0f, 0.0f, 0.0f, 0.0f, byte.MaxValue);
                Main.npc[index2].ai[0] = 1f;
                Main.npc[index2].ai[1] = npc.whoAmI;
                Main.npc[index2].target = npc.target;
                Main.npc[index2].netUpdate = true;
                int index3 = NPC.NewNPC((int)(npc.position.X + (double)(npc.width / 2)), (int)npc.position.Y + (npc.height / 2), mod.NPCType("Neutralizer"), npc.whoAmI, 0.0f, 0.0f, 0.0f, 0.0f, byte.MaxValue);
                Main.npc[index3].ai[0] = -1f;
                Main.npc[index3].ai[1] = npc.whoAmI;
                Main.npc[index3].target = npc.target;
                Main.npc[index3].ai[3] = 150f;
                Main.npc[index3].netUpdate = true;
                int index4 = NPC.NewNPC((int)(npc.position.X + (double)(npc.width / 2)), (int)npc.position.Y + (npc.height / 2), mod.NPCType("GenocideCannon"), npc.whoAmI, 0.0f, 0.0f, 0.0f, 0.0f, byte.MaxValue);
                Main.npc[index4].ai[0] = 1f;
                Main.npc[index4].ai[1] = npc.whoAmI;
                Main.npc[index4].target = npc.target;
                Main.npc[index4].netUpdate = true;
                Main.npc[index4].ai[3] = 150f;
            }

            if (npc.type == mod.NPCType<Zero>() && 
                (!NPC.AnyNPCs(mod.NPCType<VoidStar>()) &&
                !NPC.AnyNPCs(mod.NPCType<Taser>()) && 
                !NPC.AnyNPCs(mod.NPCType<RealityCannon>()) && 
                !NPC.AnyNPCs(mod.NPCType<RiftShredder>()) && 
                !NPC.AnyNPCs(mod.NPCType<Neutralizer>()) && 
                !NPC.AnyNPCs(mod.NPCType<OmegaVolley>()) && 
                !NPC.AnyNPCs(mod.NPCType<NovaFocus>()) && 
                !NPC.AnyNPCs(mod.NPCType<GenocideCannon>())))
            {
                ArmsGone = true;
                npc.dontTakeDamage = false;
                npc.chaseable = true;
                npc.damage = 160;
            }
            else
            {
                ArmsGone = false;
                npc.dontTakeDamage = true;
                npc.chaseable = false;
                npc.damage = 80;
                saythelinezero = false;
            }

            if (npc.type == mod.NPCType<Zero>() &&
                NPC.AnyNPCs(mod.NPCType<Neutralizer>()) &&
                NPC.AnyNPCs(mod.NPCType<OmegaVolley>()) &&
                NPC.AnyNPCs(mod.NPCType<NovaFocus>()) &&
                NPC.AnyNPCs(mod.NPCType<GenocideCannon>()))
            {
                npc.ai[3] = 1;
            }
            else
            {
                npc.ai[3] = 0;
            }


            if (ArmsGone && !saythelinezero)
            {
                saythelinezero = true;
                Main.NewText("CRITICAL ERR0R: ARM UNITS NOT FOUND. RER0UTING RES0URCES TO OFFENSIVE PR0T0C0LS. SHIELD L0WERED.", Color.Red.R, Color.Red.G, Color.Red.B);
            }

            if (!ArmsGone)
            {
                ShieldScale += .02f;
                if (ShieldScale > .5f)
                {
                    ShieldScale = .5f;
                }
            }
            else if (npc.ai[1] == 1f || npc.ai[1] == 6f)
            {
                ShieldScale += .2f;
                if (ShieldScale > .5f)
                {
                    ShieldScale = .5f;
                }
            }
            else
            {
                ShieldScale -= .05f;
                if (ShieldScale > .5f)
                {
                    ShieldScale = .5f;
                }
            }

            if (ShieldScale < 0)
            {
                ShieldScale = 0;
            }

            if (Main.player[npc.target].dead)
            {
                npc.TargetClosest(true);
                if (Main.player[npc.target].dead)
                {
					if(npc.ai[1] != 5f)
					{
						npc.ai[1] = 3f;
						npc.netUpdate = true;
					}
                }
            }

            if (Math.Abs(npc.position.X - Main.player[npc.target].position.X) > 6000f || Math.Abs(npc.position.Y - Main.player[npc.target].position.Y) > 6000f)
            {
                npc.TargetClosest(true);
                if (Math.Abs(npc.position.X - Main.player[npc.target].position.X) > 6000f || Math.Abs(npc.position.Y - Main.player[npc.target].position.Y) > 6000f)
                {
					if(npc.ai[1] != 5f)
					{
						npc.ai[1] = 4f;
						npc.netUpdate = true;
					}
                }
            }

            if (npc.ai[1] == 0f)
            {
                if (saythelinezero)
                {
                    npc.ai[2] += 1f;
                    npc.frame.Y = 0;
                }
                else
                {
                    npc.frame.Y = 202;
                }
                npc.damage = 100;
                npc.defense = 90;
                if (npc.ai[2] >= 600f)
                {
                    npc.ai[2] = 0f;
                    if (NPC.CountNPCS(mod.NPCType<SearcherZero>()) < 1)
                    {
                        npc.ai[1] = Main.rand.Next(2) == 0 ? 1f : 6f;
                    }
                    else
                    {
                        npc.ai[1] = 1f;
                    }
                    npc.TargetClosest(true);
                    npc.netUpdate = true;
                }
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
                else if (npc.position.Y < Main.player[npc.target].position.Y - 500f)
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
            }
            else if (npc.ai[1] == 1f)
            {
                npc.dontTakeDamage = true;
                npc.damage = 200;
                if (npc.velocity.X > 0)
                {
                    npc.rotation += .08f;
                }
                else
                {
                    npc.rotation -= .08f;
                }
                Vector2 vector45 = new Vector2(npc.position.X + ((float)npc.width * 0.5f), npc.position.Y + ((float)npc.height * 0.5f));
                float num444 = Main.player[npc.target].position.X + (float)(Main.player[npc.target].width / 2) - vector45.X;
                float num445 = Main.player[npc.target].position.Y + (float)(Main.player[npc.target].height / 2) - vector45.Y;
                float num446 = (float)Math.Sqrt((double)((num444 * num444) + (num445 * num445)));
                float num447 = 8f;
                num447 += num446 / 100f;
                if (num447 < 8f)
                {
                    num447 = 8f;
                }
                if (num447 > 32f)
                {
                    num447 = 32f;
                }
                num446 = num447 / num446;
                npc.velocity.X = num444 * num446;
                npc.velocity.Y = num445 * num446;

                npc.ai[2] += 1f;
                if (npc.ai[2] >= 240f)
                {
                    npc.dontTakeDamage = false;
                    npc.ai[2] = 0f;
                    npc.ai[1] = 0f;
                    npc.TargetClosest(true);
                    npc.netUpdate = true;
                }
                return;
            }
            else if (npc.ai[1] == 3f)
            {
                Main.NewText("TARGET NEUTRALIZED. RETURNING T0 0RBIT.", Color.Red);
                npc.ai[1] = 5f;
            }
            else if (npc.ai[1] == 4f)
            {				
                Main.NewText("TARGET L0ST. RETURNING T0 0RBIT.", Color.Red);
                npc.ai[1] = 5f;
            }
            else if (npc.ai[1] == 5f)
            {
                npc.velocity.Y -= 0.1f;
                if (npc.velocity.Y < -16f)
					npc.velocity.Y = -16f;
                npc.velocity.X = npc.velocity.X * 0.95f;
                if (npc.timeLeft > 500)
                {
                    npc.velocity.Y += 0.1f;
                    if (npc.velocity.Y > 15f) npc.velocity.Y = 15f;
                    npc.rotation = 0f;
                    if (npc.position.Y - npc.height - npc.velocity.Y >= Main.maxTilesY && Main.netMode != 1) { BaseAI.KillNPC(npc); npc.netUpdate2 = true; }
                    return;
                }
            }
            else if (npc.ai[1] == 6f)
            {
                npc.frame.Y = 202;
                npc.dontTakeDamage = true;
                npc.velocity *= 0.8f;
                internalAI[1]++;
                if (internalAI[1] == 1) Main.NewText("INITIATING PR0BE CREATI0N SYSTEM.", Color.Red);
                if ((internalAI[1] == 20 || internalAI[1] == 40 || internalAI[1] == 60 || internalAI[1] == 80 || internalAI[1] == 100 || internalAI[1] == 120 || internalAI[1] == 140 || internalAI[1] == 160) && NPC.CountNPCS(mod.NPCType<SearcherZero>()) < 12)
                {
                    NPC.NewNPC((int)npc.Center.X, (int)npc.Center.Y, mod.NPCType<SearcherZero>());
                }

                if (internalAI[1] >= 180)
                {
                    npc.dontTakeDamage = false;
                    internalAI[1] = 0;
                    npc.ai[2] = 0f;
                    npc.ai[1] = 0f;
                    npc.TargetClosest(true);
                    npc.netUpdate = true;
                }
            }
            else if (npc.ai[1] == 8f)
            {
                internalAI[1]++;
                if (internalAI[1] == 1) Main.NewText("CYCLONE PROTOCOL ENGAGED", Color.Red);
                npc.rotation += .2f;
                

                if (internalAI[1] > 120)
                {
                    if (SelectPoint)
                    {
                        int DirectionX = -1;
                        if (Main.player[npc.target].Center.X > npc.Center.X)
                        {
                            DirectionX = 1;
                        }
                        int DirectionY = -1;
                        if (Main.player[npc.target].Center.Y > npc.Center.Y)
                        {
                            DirectionY = 1;
                        }
                        float Pointx = 500 * DirectionX;
                        float PointY = 200 * DirectionY;
                        MovePoint = Main.player[npc.target].Center + new Vector2(Pointx, PointY);
                        SelectPoint = false;
                        npc.netUpdate = true;
                    }
                    VortexMovement(MovePoint);

                    npc.netUpdate = true;
                }
                else
                {
                    npc.frame.Y = 0;
                    npc.dontTakeDamage = true;
                    npc.velocity *= 0.8f;
                }

                if (internalAI[1] >= 260)
                {
                    if (VortexScale > 0)
                    {
                        VortexScale -= .05f;
                    }
                    else
                    {
                        VortexScale = 0f;
                    }

                    if (npc.rotation != 0)
                    {
                        npc.rotation += .1f;
                    }
                    npc.velocity *= 0.8f;
                    npc.netUpdate = true;
                    if (npc.rotation == 0)
                    {
                        npc.dontTakeDamage = false;
                        internalAI[1] = 0;
                        npc.ai[2] = 0f;
                        npc.ai[1] = 0f;
                        npc.TargetClosest(true);
                        npc.netUpdate = true;
                    }
                }
                else
                {
                    if (VortexScale < 1f)
                    {
                        VortexScale += .05f;
                    }
                    else
                    {
                        VortexScale = 1f;
                    }
                }

            }
        }

        public Vector2 MovePoint = new Vector2(0, 0);
        public bool SelectPoint = false;
        public float VortexScale = .1f;

        public static void DrawArm(Mod mod, NPC npc, SpriteBatch spriteBatch, Color drawColor)
        {
            Vector2 vector7 = new Vector2(npc.position.X + ((float)npc.width * 0.5f) - (5f * npc.ai[0]), npc.position.Y + 20f);
            for (int l = 0; l < 2; l++)
            {
                float num21 = Main.npc[(int)npc.ai[1]].position.X + (float)(Main.npc[(int)npc.ai[1]].width / 2) - vector7.X;
                float num22 = Main.npc[(int)npc.ai[1]].position.Y + (float)(Main.npc[(int)npc.ai[1]].height / 2) - vector7.Y;
                float num23;
                if (l == 0)
                {
                    num21 -= 200f * npc.ai[0];
                    num22 += 130f;
                    num23 = (float)Math.Sqrt(((num21 * num21) + (num22 * num22)));
                    num23 = 92f / num23;
                    vector7.X += num21 * num23;
                    vector7.Y += num22 * num23;
                }
                else
                {
                    num21 -= 50f * npc.ai[0];
                    num22 += 80f;
                    num23 = (float)Math.Sqrt((double)((num21 * num21) + (num22 * num22)));
                    num23 = 60f / num23;
                    vector7.X += num21 * num23;
                    vector7.Y += num22 * num23;
                }
                float rotation7 = (float)Math.Atan2(num22, (double)num21) - 1.57f;
                Texture2D Arm = mod.GetTexture("NPCs/Bosses/Zero/ZeroArm");
                Texture2D ArmGlow = mod.GetTexture("Glowmasks/ZeroArm_Glow");
                spriteBatch.Draw(Arm, new Vector2(vector7.X - Main.screenPosition.X, vector7.Y - Main.screenPosition.Y), new Rectangle?(new Rectangle(0, 0, Arm.Width, Arm.Height)), drawColor, rotation7, new Vector2(Arm.Width * 0.5f, Arm.Height * 0.5f), 1f, SpriteEffects.None, 0f);
                spriteBatch.Draw(ArmGlow, new Vector2(vector7.X - Main.screenPosition.X, vector7.Y - Main.screenPosition.Y), new Rectangle?(new Rectangle(0, 0, Arm.Width, Arm.Height)), ColorUtils.COLOR_GLOWPULSE, rotation7, new Vector2(Arm.Width * 0.5f, Arm.Height * 0.5f), 1f, SpriteEffects.None, 0f);
                if (l == 0)
                {
                    vector7.X += num21 * num23 / 2f;
                    vector7.Y += num22 * num23 / 2f;
                }
                else if (Main.rand.Next(2) == 0)
                {
                    vector7.X += (num21 * num23) - 16f;
                    vector7.Y += (num22 * num23) - 6f;
                }
            }
        }

        public void MoveToPoint(Vector2 point)
        {
            if (moveSpeed == 0f || npc.Center == point) return; //don't move if you have no move speed
            float velMultiplier = 1f;
            Vector2 dist = point - npc.Center;
            float length = (dist == Vector2.Zero ? 0f : dist.Length());
            if (length < moveSpeed)
            {
                velMultiplier = MathHelper.Lerp(0f, 1f, length / moveSpeed);
            }
            if (length < 200f)
            {
                moveSpeed *= 0.5f;
            }
            if (length < 100f)
            {
                moveSpeed *= 0.5f;
            }
            if (length < 50f)
            {
                moveSpeed *= 0.5f;
            }
            npc.velocity = (length == 0f ? Vector2.Zero : Vector2.Normalize(dist));
            npc.velocity *= moveSpeed;
            npc.velocity *= velMultiplier;
        }

        public void VortexMovement(Vector2 point)
        {
            if (moveSpeed == 0f || npc.Center == point) return; //don't move if you have no move speed
            float velMultiplier = 1f;
            Vector2 dist = point - npc.Center;
            float length = (dist == Vector2.Zero ? 0f : dist.Length());
            if (length < moveSpeed)
            {
                velMultiplier = MathHelper.Lerp(0f, 1f, length / moveSpeed);
            }
            if (length < 200f)
            {
                moveSpeed *= 0.5f;
            }
            if (length < 100f)
            {
                moveSpeed *= 0.5f;
            }
            if (length < 50f)
            {
                moveSpeed *= 0.5f;
            }
            npc.velocity = (length == 0f ? Vector2.Zero : Vector2.Normalize(dist));
            npc.velocity *= moveSpeed;
            npc.velocity *= velMultiplier;
            if (dist == new Vector2(0, 0))
            {
                SelectPoint = true;
            }
        }
    }
}







