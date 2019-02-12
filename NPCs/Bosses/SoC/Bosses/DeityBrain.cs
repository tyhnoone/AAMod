using System;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.Audio;
using Terraria.ModLoader;
using BaseMod;

namespace AAMod.NPCs.Bosses.SoC.Bosses
{
    [AutoloadBossHead]
    public class DeityBrain : ModNPC
    {
        public override string Texture { get { return "AAMod/NPCs/Bosses/SoC/Bosses/DeityEater"; } }

        public int fireTimer = 0;

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Lu'Kthu");
            NPCID.Sets.TechnicallyABoss[npc.type] = true;
            Main.npcFrameCount[npc.type] = 8;
        }

        public override void SetDefaults()
        {
            npc.width = 100;
            npc.height = 100;
            npc.aiStyle = -1;
            npc.netAlways = true;
            npc.damage = 90;
            npc.defense = 100;
            npc.lifeMax = 150000;
            npc.HitSound = SoundID.NPCHit9;
            npc.DeathSound = SoundID.NPCDeath11;
            npc.noGravity = true;
            npc.noTileCollide = true;
            npc.knockBackResist = 0f;
            npc.behindTiles = true;
            npc.scale = 1f;
            npc.buffImmune[20] = true;
            npc.buffImmune[24] = true;
            npc.buffImmune[39] = true;
            for (int m = 0; m < npc.buffImmune.Length; m++) npc.buffImmune[m] = true;
            npc.dontTakeDamage = true;
            npc.alpha = 255;
            npc.timeLeft = NPC.activeTime * 30;
        }

        public override void ScaleExpertStats(int numPlayers, float bossLifeScale)
        {
            npc.lifeMax = npc.lifeMax;
        }

        public override void FindFrame(int frameHeight)
        {
            int num = 180;
            npc.frameCounter += 1.0;
            if (npc.frameCounter > 6.0)
            {
                npc.frameCounter = 0.0;
                npc.frame.Y = npc.frame.Y + num;
            }
            if (npc.ai[0] >= 0f)
            {
                if (npc.frame.Y > num * 3)
                {
                    npc.frame.Y = 0;
                }
            }
            else
            {
                if (npc.frame.Y < num * 4)
                {
                    npc.frame.Y = num * 4;
                }
                if (npc.frame.Y > num * 7)
                {
                    npc.frame.Y = num * 4;
                }
            }
        }


        public int EyeCount = 15;
        public int[] totalEyes = null;
        public bool spawnAlpha = false;

        public override void AI()
        {

            if (npc.alpha != 0)
            {
                for (int spawnDust = 0; spawnDust < 2; spawnDust++)
                {
                    int num935 = Dust.NewDust(new Vector2(npc.position.X, npc.position.Y), npc.width, npc.height, mod.DustType("CthulhuDust"), 0f, 0f, 100, default(Color), 2f);
                    Main.dust[num935].noGravity = true;
                    Main.dust[num935].noLight = true;
                }
            }
            if (spawnAlpha == false)
            {
                npc.alpha -= 12;
            }
            if (npc.alpha < 0 && spawnAlpha == false)
            {
                npc.alpha = 0;
                spawnAlpha = true;
            }

            AAModGlobalNPC.Brain = npc.whoAmI;

            bool Cthulhu = NPC.AnyNPCs(mod.NPCType<Cthulhu>());

            bool SoC = NPC.AnyNPCs(mod.NPCType<SoC>());

            if (SoC)
            {
                music = mod.GetSoundSlot(Terraria.ModLoader.SoundType.Music, "Sounds/Music/SoC");
            }
            else if (Cthulhu)
            {
                music = mod.GetSoundSlot(Terraria.ModLoader.SoundType.Music, "Sounds/Music/Cthulhu");
            }
            EyeCount = (Main.expertMode ? 20 : 15);
            totalEyes = BaseAI.GetNPCs(npc.Center, mod.NPCType("EoA"), 1500f);
            if (Main.netMode != 1 && npc.localAI[0] == 0f)
            {
                npc.localAI[0] = 1f;
                for (int num761 = 0; EyeCount < 20; num761++)
                {
                    float num762 = npc.Center.X;
                    float num763 = npc.Center.Y;
                    num762 += (float)Main.rand.Next(-npc.width, npc.width);
                    num763 += (float)Main.rand.Next(-npc.height, npc.height);
                    int num764 = NPC.NewNPC((int)num762, (int)num763, mod.NPCType<EoA>(), 0, 0f, 0f, 0f, 0f, 255);
                    Main.npc[num764].velocity = new Vector2((float)Main.rand.Next(-30, 31) * 0.1f, (float)Main.rand.Next(-30, 31) * 0.1f);
                    Main.npc[num764].netUpdate = true;
                }
            }
            if (Main.netMode != 1)
            {
                npc.TargetClosest(true);
                int num765 = 6000;
                if (Math.Abs(npc.Center.X - Main.player[npc.target].Center.X) + Math.Abs(npc.Center.Y - Main.player[npc.target].Center.Y) > (float)num765)
                {
                    npc.active = false;
                    npc.life = 0;
                    if (Main.netMode == 2)
                    {
                        NetMessage.SendData(23, -1, -1, null, npc.whoAmI, 0f, 0f, 0f, 0, 0, 0);
                    }
                }
            }
            if (npc.ai[0] < 0f)
            {
                if (npc.localAI[2] == 0f)
                {
                    Main.PlaySound(3, (int)npc.position.X, (int)npc.position.Y, 1, 1f, 0f);
                    npc.localAI[2] = 1f;
                    Gore.NewGore(npc.position, npc.velocity, mod.GetGoreSlot("Gores/DeityBrain1"), 1f);
                    Gore.NewGore(npc.position, npc.velocity, mod.GetGoreSlot("Gores/DeityBrain2"), 1f);
                    for (int num766 = 0; num766 < 20; num766++)
                    {
                        Dust.NewDust(npc.position, npc.width, npc.height, mod.DustType<Dusts.CthulhuDust>(), (float)Main.rand.Next(-30, 31) * 0.2f, (float)Main.rand.Next(-30, 31) * 0.2f, 0, default(Color), 1f);
                    }
                    Main.PlaySound(15, (int)npc.position.X, (int)npc.position.Y, 0, 1f, 0f);
                }
                npc.dontTakeDamage = false;
                npc.knockBackResist = 0.5f;
                if (Main.expertMode)
                {
                    npc.knockBackResist *= Main.expertKnockBack;
                }
                npc.TargetClosest(true);
                Vector2 vector94 = new Vector2(npc.Center.X, npc.Center.Y);
                float num767 = Main.player[npc.target].Center.X - vector94.X;
                float num768 = Main.player[npc.target].Center.Y - vector94.Y;
                float num769 = (float)Math.Sqrt((double)(num767 * num767 + num768 * num768));
                float num770 = 8f;
                num769 = num770 / num769;
                num767 *= num769;
                num768 *= num769;
                npc.velocity.X = (npc.velocity.X * 50f + num767) / 51f;
                npc.velocity.Y = (npc.velocity.Y * 50f + num768) / 51f;
                if (npc.ai[0] == -1f)
                {
                    if (Main.netMode != 1)
                    {
                        npc.localAI[1] += 1f;
                        if (npc.justHit)
                        {
                            npc.localAI[1] -= (float)Main.rand.Next(5);
                        }
                        int num771 = 60 + Main.rand.Next(120);
                        if (Main.netMode != 0)
                        {
                            num771 += Main.rand.Next(30, 90);
                        }
                        if (npc.localAI[1] >= (float)num771)
                        {
                            npc.localAI[1] = 0f;
                            npc.TargetClosest(true);
                            int num772 = 0;
                            int num773;
                            int num774;
                            while (true)
                            {
                                num772++;
                                num773 = (int)Main.player[npc.target].Center.X / 16;
                                num774 = (int)Main.player[npc.target].Center.Y / 16;
                                if (Main.rand.Next(2) == 0)
                                {
                                    num773 += Main.rand.Next(7, 13);
                                }
                                else
                                {
                                    num773 -= Main.rand.Next(7, 13);
                                }
                                if (Main.rand.Next(2) == 0)
                                {
                                    num774 += Main.rand.Next(7, 13);
                                }
                                else
                                {
                                    num774 -= Main.rand.Next(7, 13);
                                }
                                if (!WorldGen.SolidTile(num773, num774))
                                {
                                    break;
                                }
                                if (num772 > 100)
                                {
                                    goto Block_2789;
                                }
                            }
                            npc.ai[3] = 0f;
                            npc.ai[0] = -2f;
                            npc.ai[1] = (float)num773;
                            npc.ai[2] = (float)num774;
                            npc.netUpdate = true;
                            npc.netSpam = 0;
                            Block_2789:;
                        }
                    }
                }
                else if (npc.ai[0] == -2f)
                {
                    npc.velocity *= 0.9f;
                    if (Main.netMode != 0)
                    {
                        npc.ai[3] += 15f;
                    }
                    else
                    {
                        npc.ai[3] += 25f;
                    }
                    if (npc.ai[3] >= 255f)
                    {
                        npc.ai[3] = 255f;
                        npc.position.X = npc.ai[1] * 16f - (float)(npc.width / 2);
                        npc.position.Y = npc.ai[2] * 16f - (float)(npc.height / 2);
                        Main.PlaySound(SoundID.Item8, npc.Center);
                        npc.ai[0] = -3f;
                        npc.netUpdate = true;
                        npc.netSpam = 0;
                    }
                    npc.alpha = (int)npc.ai[3];
                }
                else if (npc.ai[0] == -3f)
                {
                    if (Main.netMode != 0)
                    {
                        npc.ai[3] -= 15f;
                    }
                    else
                    {
                        npc.ai[3] -= 25f;
                    }
                    if (npc.ai[3] <= 0f)
                    {
                        npc.ai[3] = 0f;
                        npc.ai[0] = -1f;
                        npc.netUpdate = true;
                        npc.netSpam = 0;
                    }
                    npc.alpha = (int)npc.ai[3];
                }
            }
            else
            {
                npc.TargetClosest(true);
                Vector2 vector95 = new Vector2(npc.Center.X, npc.Center.Y);
                float num775 = Main.player[npc.target].Center.X - vector95.X;
                float num776 = Main.player[npc.target].Center.Y - vector95.Y;
                float num777 = (float)Math.Sqrt((double)(num775 * num775 + num776 * num776));
                float num778 = 1f;
                if (num777 < num778)
                {
                    npc.velocity.X = num775;
                    npc.velocity.Y = num776;
                }
                else
                {
                    num777 = num778 / num777;
                    npc.velocity.X = num775 * num777;
                    npc.velocity.Y = num776 * num777;
                }
                if (npc.ai[0] == 0f)
                {
                    if (Main.netMode != 1)
                    {
                        int num779 = 0;
                        for (int num780 = 0; num780 < 200; num780++)
                        {
                            if (Main.npc[num780].active && Main.npc[num780].type == mod.NPCType<EoA>())
                            {
                                num779++;
                            }
                        }
                        if (num779 == 0)
                        {
                            npc.ai[0] = -1f;
                            npc.localAI[1] = 0f;
                            npc.alpha = 0;
                            npc.netUpdate = true;
                        }
                        npc.localAI[1] += 1f;
                        if (npc.localAI[1] >= (float)(120 + Main.rand.Next(300)))
                        {
                            npc.localAI[1] = 0f;
                            npc.TargetClosest(true);
                            int num781 = 0;
                            int num782;
                            int num783;
                            while (true)
                            {
                                num781++;
                                num782 = (int)Main.player[npc.target].Center.X / 16;
                                num783 = (int)Main.player[npc.target].Center.Y / 16;
                                num782 += Main.rand.Next(-50, 51);
                                num783 += Main.rand.Next(-50, 51);
                                if (!WorldGen.SolidTile(num782, num783) && Collision.CanHit(new Vector2((float)(num782 * 16), (float)(num783 * 16)), 1, 1, Main.player[npc.target].position, Main.player[npc.target].width, Main.player[npc.target].height))
                                {
                                    break;
                                }
                                if (num781 > 100)
                                {
                                    goto Block_2806;
                                }
                            }
                            npc.ai[0] = 1f;
                            npc.ai[1] = (float)num782;
                            npc.ai[2] = (float)num783;
                            npc.netUpdate = true;
                            Block_2806:;
                        }
                    }
                }
                else if (npc.ai[0] == 1f)
                {
                    npc.alpha += 5;
                    if (npc.alpha >= 255)
                    {
                        Main.PlaySound(SoundID.Item8, npc.Center);
                        npc.alpha = 255;
                        npc.position.X = npc.ai[1] * 16f - (float)(npc.width / 2);
                        npc.position.Y = npc.ai[2] * 16f - (float)(npc.height / 2);
                        npc.ai[0] = 2f;
                    }
                }
                else if (npc.ai[0] == 2f)
                {
                    npc.alpha -= 5;
                    if (npc.alpha <= 0)
                    {
                        npc.alpha = 0;
                        npc.ai[0] = 0f;
                    }
                }
            }
            if (Main.player[npc.target].dead)
            {
                if (npc.localAI[3] < 120f)
                {
                    npc.localAI[3] += 1f;
                }
                if (npc.localAI[3] > 60f)
                {
                    npc.velocity.Y = npc.velocity.Y + (npc.localAI[3] - 60f) * 0.25f;
                }
                npc.ai[0] = 2f;
                npc.alpha = 10;
                return;
            }
            if (npc.localAI[3] > 0f)
            {
                npc.localAI[3] -= 1f;
                return;
            }
        }


        public override void HitEffect(int hitDirection, double dmg)
        {
            if (npc.life > 0)
            {
                SoC.ComeBack = true;
                int num121 = 0;
                while ((double)num121 < dmg / (double)npc.lifeMax * 3.0)
                {
                    if (Main.rand.Next(3) == 0)
                    {
                        Dust.NewDust(npc.position, npc.width, npc.height, mod.DustType<Dusts.CthulhuDust>(), (float)hitDirection, -1f, 0, Color.Transparent, 0.75f);
                    }
                    if (Main.rand.Next(2) == 0)
                    {
                        Dust dust39 = Main.dust[Dust.NewDust(npc.position, npc.width, npc.height, mod.DustType<Dusts.CthulhuDust>(), 0f, 0f, 0, default(Color), 1f)];
                        dust39.noGravity = true;
                    }
                    for (int num122 = 0; num122 < npc.oldPos.Length; num122++)
                    {
                        if (Main.rand.Next(4) == 0)
                        {
                            if (npc.oldPos[num122] == Vector2.Zero)
                            {
                                break;
                            }
                            if (Main.rand.Next(3) == 0)
                            {
                                Dust.NewDust(npc.oldPos[num122], npc.width, npc.height, mod.DustType<Dusts.CthulhuDust>(), (float)hitDirection, -1f, 0, Color.Transparent, 0.75f);
                            }
                            if (Main.rand.Next(2) == 0)
                            {
                                Dust dust40 = Main.dust[Dust.NewDust(npc.oldPos[num122], npc.width, npc.height, mod.DustType<Dusts.CthulhuDust>(), 0f, 0f, 0, default(Color), 1f)];
                                dust40.noGravity = true;
                            }
                        }
                    }
                    num121++;
                }
            }
        }

        

        public override void ModifyHitByProjectile(Projectile projectile, ref int damage, ref float knockback, ref bool crit, ref int hitDirection)
        {
            Player player = Main.player[npc.target];
            if (player.vortexStealthActive && projectile.ranged)
            {
                damage /= 2;
                crit = false;
            }
            if (projectile.penetrate == -1 && !projectile.minion)
            {
                projectile.damage *= (int).2;
            }
            else if (projectile.penetrate >= 1)
            {
                projectile.damage *= (int).2;
            }
        }

        public override bool PreDraw(SpriteBatch sb, Color drawColor)
        {
            Texture2D currentTex = Main.npcTexture[npc.type];
            Texture2D GlowTex = mod.GetTexture("Glowmasks/DeityBrain_Glow");

            BaseDrawing.DrawTexture(sb, currentTex, 0, npc, drawColor);

            //draw glow/glow afterimage
            BaseDrawing.DrawTexture(sb, GlowTex, 0, npc, AAColor.Cthulhu);
            BaseDrawing.DrawAfterimage(sb, GlowTex, 0, npc, 0.8f, 1f, 6, false, 0f, 0f, AAColor.Cthulhu);

            return false;
        }
    }
}