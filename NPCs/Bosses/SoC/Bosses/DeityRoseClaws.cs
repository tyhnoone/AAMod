using System;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.Audio;
using Terraria.ModLoader;

namespace AAMod.NPCs.Bosses.SoC.Bosses
{
    public class DeityRoseClaws: ModNPC
	{

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Ei'Lor's Tentacle");
            Main.npcFrameCount[npc.type] = 4;
        }

        public override void SetDefaults()
        {
            npc.width = 24;
            npc.height = 24;
            npc.aiStyle = -1;
            npc.damage = 60;
            npc.defense = 20;
            npc.lifeMax = 1000;
            npc.HitSound = SoundID.NPCHit1;
            npc.DeathSound = SoundID.NPCDeath1;
            npc.noGravity = true;
            npc.noTileCollide = true;
        }

        public override void AI()
        {
            if (AAModGlobalNPC.Rose < 0)
            {
                npc.StrikeNPCNoInteraction(9999, 0f, 0, false, false, false);
                npc.netUpdate = true;
                return;
            }
            int num750 = AAModGlobalNPC.Rose;
            if (npc.ai[3] > 0f)
            {
                num750 = (int)npc.ai[3] - 1;
            }
            if (Main.netMode != 1)
            {
                npc.localAI[0] -= 1f;
                if (npc.localAI[0] <= 0f)
                {
                    npc.localAI[0] = (float)Main.rand.Next(120, 480);
                    npc.ai[0] = (float)Main.rand.Next(-100, 101);
                    npc.ai[1] = (float)Main.rand.Next(-100, 101);
                    npc.netUpdate = true;
                }
            }
            npc.TargetClosest(true);
            float num751 = 0.2f;
            float num752 = 200f;
            if ((double)Main.npc[AAModGlobalNPC.Rose].life < (double)Main.npc[AAModGlobalNPC.Rose].lifeMax * 0.25)
            {
                num752 += 100f;
            }
            if ((double)Main.npc[AAModGlobalNPC.Rose].life < (double)Main.npc[AAModGlobalNPC.Rose].lifeMax * 0.1)
            {
                num752 += 100f;
            }
            if (Main.expertMode)
            {
                float num753 = 1f - (float)npc.life / (float)npc.lifeMax;
                num752 += num753 * 300f;
                num751 += 0.3f;
            }
            if (!Main.npc[num750].active || AAModGlobalNPC.Rose < 0)
            {
                npc.active = false;
                return;
            }
            float num754 = Main.npc[num750].position.X + (float)(Main.npc[num750].width / 2);
            float num755 = Main.npc[num750].position.Y + (float)(Main.npc[num750].height / 2);
            Vector2 vector93 = new Vector2(num754, num755);
            float num756 = num754 + npc.ai[0];
            float num757 = num755 + npc.ai[1];
            float num758 = num756 - vector93.X;
            float num759 = num757 - vector93.Y;
            float num760 = (float)Math.Sqrt((double)(num758 * num758 + num759 * num759));
            num760 = num752 / num760;
            num758 *= num760;
            num759 *= num760;
            if (npc.position.X < num754 + num758)
            {
                npc.velocity.X = npc.velocity.X + num751;
                if (npc.velocity.X < 0f && num758 > 0f)
                {
                    npc.velocity.X = npc.velocity.X * 0.9f;
                }
            }
            else if (npc.position.X > num754 + num758)
            {
                npc.velocity.X = npc.velocity.X - num751;
                if (npc.velocity.X > 0f && num758 < 0f)
                {
                    npc.velocity.X = npc.velocity.X * 0.9f;
                }
            }
            if (npc.position.Y < num755 + num759)
            {
                npc.velocity.Y = npc.velocity.Y + num751;
                if (npc.velocity.Y < 0f && num759 > 0f)
                {
                    npc.velocity.Y = npc.velocity.Y * 0.9f;
                }
            }
            else if (npc.position.Y > num755 + num759)
            {
                npc.velocity.Y = npc.velocity.Y - num751;
                if (npc.velocity.Y > 0f && num759 < 0f)
                {
                    npc.velocity.Y = npc.velocity.Y * 0.9f;
                }
            }
            if (npc.velocity.X > 8f)
            {
                npc.velocity.X = 8f;
            }
            if (npc.velocity.X < -8f)
            {
                npc.velocity.X = -8f;
            }
            if (npc.velocity.Y > 8f)
            {
                npc.velocity.Y = 8f;
            }
            if (npc.velocity.Y < -8f)
            {
                npc.velocity.Y = -8f;
            }
            if (num758 > 0f)
            {
                npc.spriteDirection = 1;
                npc.rotation = (float)Math.Atan2((double)num759, (double)num758);
            }
            if (num758 < 0f)
            {
                npc.spriteDirection = -1;
                npc.rotation = (float)Math.Atan2((double)num759, (double)num758) + 3.14f;
                return;
            }
        }

        public override void HitEffect(int hitDirection, double dmg)
        {
            if (npc.life > 0)
            {
                int num440 = 0;
                while ((double)num440 < dmg / (double)npc.lifeMax * 100.0)
                {
                    Dust.NewDust(npc.position, npc.width, npc.height, mod.DustType<Dusts.CthulhuDust>(), (float)hitDirection, -1f, 0, default(Color), 1f);
                    num440++;
                }
                return;
            }
            for (int num441 = 0; num441 < 150; num441++)
            {
                Dust.NewDust(npc.position, npc.width, npc.height, mod.DustType<Dusts.CthulhuDust>(), (float)(2 * hitDirection), -2f, 0, default(Color), 1f);
                
            }
        }

        public override bool PreDraw(SpriteBatch spriteBatch, Color drawColor)
        {
            Rectangle rectangle = new Rectangle((int)Main.screenPosition.X - 800, (int)Main.screenPosition.Y - 800, Main.screenWidth + 1600, Main.screenHeight + 1600);
            for (int i = 199; i >= 0; i--)
            {
                try
                {
                    if (Main.npc[i].active && Main.npc[i].type > 0 && Main.npc[i].type < 580 && !Main.npc[i].hide)
                    {
                        Main.npc[i].visualOffset *= 0.95f;
                        Main.npc[i].position += Main.npc[i].visualOffset;
                        if (Main.npc[i].behindTiles == npc.behindTiles)
                        {
                            if (AAModGlobalNPC.Rose >= 0)
                            {
                                int num11 = AAModGlobalNPC.Rose;
                                if (Main.npc[i].ai[3] > 0f)
                                {
                                    num11 = (int)Main.npc[i].ai[3] - 1;
                                }
                                Vector2 vector3 = new Vector2(Main.npc[i].position.X + (float)(Main.npc[i].width / 2), Main.npc[i].position.Y + (float)(Main.npc[i].height / 2));
                                float num12 = Main.npc[num11].Center.X - vector3.X;
                                float num13 = Main.npc[num11].Center.Y - vector3.Y;
                                float rotation3 = (float)Math.Atan2((double)num13, (double)num12) - 1.57f;
                                bool flag4 = true;
                                while (flag4)
                                {
                                    int num14 = 16;
                                    int num15 = 32;
                                    float num16 = (float)Math.Sqrt((double)(num12 * num12 + num13 * num13));
                                    if (num16 < (float)num15)
                                    {
                                        num14 = (int)num16 - num15 + num14;
                                        flag4 = false;
                                    }
                                    num16 = (float)num14 / num16;
                                    num12 *= num16;
                                    num13 *= num16;
                                    vector3.X += num12;
                                    vector3.Y += num13;
                                    num12 = Main.npc[num11].Center.X - vector3.X;
                                    num13 = Main.npc[num11].Center.Y - vector3.Y;
                                    Microsoft.Xna.Framework.Color color3 = Lighting.GetColor((int)vector3.X / 16, (int)(vector3.Y / 16f));
                                    Main.spriteBatch.Draw(mod.GetTexture("NPCs/Bosses/SoC/Bosses/DeityRoseClaws_Chain"), new Vector2(vector3.X - Main.screenPosition.X, vector3.Y - Main.screenPosition.Y), new Microsoft.Xna.Framework.Rectangle?(new Microsoft.Xna.Framework.Rectangle(0, 0, Main.chain27Texture.Width, num14)), color3, rotation3, new Vector2((float)Main.chain27Texture.Width * 0.5f, (float)Main.chain27Texture.Height * 0.5f), 1f, SpriteEffects.None, 0f);
                                }
                            }
                        } 
                        Main.npc[i].position -= Main.npc[i].visualOffset;
                    }
                }
                catch
                {
                    Main.npc[i].active = false;
                }
            }
            return true;
        }
    }
}