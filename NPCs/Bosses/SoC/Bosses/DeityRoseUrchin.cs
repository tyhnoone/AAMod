using System;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.NPCs.Bosses.SoC.Bosses
{
    public class DeityRoseUrchin : ModNPC
    {

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Shadow Urchin");
        }
        public override void SetDefaults()
        {
            npc.width = 22;
            npc.height = 22;
            npc.aiStyle = -1;
            npc.damage = 130;
            npc.defense = 80;
            npc.lifeMax = 4000;
            npc.HitSound = SoundID.NPCHit34;
            npc.DeathSound = SoundID.NPCDeath37;
            npc.value = 0;
            npc.buffImmune[20] = true;
            npc.buffImmune[24] = true;
            npc.buffImmune[39] = true;
            npc.knockBackResist = 0.1f;
            npc.noGravity = true;
        }

        
        public override void AI()
        {
            npc.noTileCollide = false;
            if (npc.ai[0] == 0f)
            {
                npc.TargetClosest(true);
                if (Collision.CanHit(npc.Center, 1, 1, Main.player[npc.target].Center, 1, 1))
                {
                    npc.ai[0] = 1f;
                }
                else
                {
                    Vector2 value41 = Main.player[npc.target].Center - npc.Center;
                    value41.Y -= (float)(Main.player[npc.target].height / 4);
                    float num1262 = value41.Length();
                    if (num1262 > 800f)
                    {
                        npc.ai[0] = 2f;
                    }
                    else
                    {
                        Vector2 center26 = npc.Center;
                        center26.X = Main.player[npc.target].Center.X;
                        Vector2 vector200 = center26 - npc.Center;
                        if (vector200.Length() > 8f && Collision.CanHit(npc.Center, 1, 1, center26, 1, 1))
                        {
                            npc.ai[0] = 3f;
                            npc.ai[1] = center26.X;
                            npc.ai[2] = center26.Y;
                            Vector2 center27 = npc.Center;
                            center27.Y = Main.player[npc.target].Center.Y;
                            if (vector200.Length() > 8f && Collision.CanHit(npc.Center, 1, 1, center27, 1, 1) && Collision.CanHit(center27, 1, 1, Main.player[npc.target].position, 1, 1))
                            {
                                npc.ai[0] = 3f;
                                npc.ai[1] = center27.X;
                                npc.ai[2] = center27.Y;
                            }
                        }
                        else
                        {
                            center26 = npc.Center;
                            center26.Y = Main.player[npc.target].Center.Y;
                            if ((center26 - npc.Center).Length() > 8f && Collision.CanHit(npc.Center, 1, 1, center26, 1, 1))
                            {
                                npc.ai[0] = 3f;
                                npc.ai[1] = center26.X;
                                npc.ai[2] = center26.Y;
                            }
                        }
                        if (npc.ai[0] == 0f)
                        {
                            npc.localAI[0] = 0f;
                            value41.Normalize();
                            value41 *= 0.5f;
                            npc.velocity += value41;
                            npc.ai[0] = 4f;
                            npc.ai[1] = 0f;
                        }
                    }
                }
            }
            else if (npc.ai[0] == 1f)
            {
                npc.rotation += (float)npc.direction * 0.3f;
                Vector2 value42 = Main.player[npc.target].Center - npc.Center;
                float num1263 = value42.Length();
                float num1264 = 5.5f;
                num1264 += num1263 / 100f;
                int num1265 = 50;
                value42.Normalize();
                value42 *= num1264;
                npc.velocity = (npc.velocity * (float)(num1265 - 1) + value42) / (float)num1265;
                if (!Collision.CanHit(npc.Center, 1, 1, Main.player[npc.target].Center, 1, 1))
                {
                    npc.ai[0] = 0f;
                    npc.ai[1] = 0f;
                }
            }
            else if (npc.ai[0] == 2f)
            {
                npc.rotation = npc.velocity.X * 0.1f;
                npc.noTileCollide = true;
                Vector2 value43 = Main.player[npc.target].Center - npc.Center;
                float num1267 = value43.Length();
                float scaleFactor11 = 3f;
                int num1268 = 3;
                value43.Normalize();
                value43 *= scaleFactor11;
                npc.velocity = (npc.velocity * (float)(num1268 - 1) + value43) / (float)num1268;
                if (num1267 < 600f && !Collision.SolidCollision(npc.position, npc.width, npc.height))
                {
                    npc.ai[0] = 0f;
                }
            }
            else if (npc.ai[0] == 3f)
            {
                npc.rotation = npc.velocity.X * 0.1f;
                Vector2 value44 = new Vector2(npc.ai[1], npc.ai[2]);
                Vector2 value45 = value44 - npc.Center;
                float num1269 = value45.Length();
                float num1270 = 2f;
                float num1271 = 3f;
                value45.Normalize();
                value45 *= num1270;
                npc.velocity = (npc.velocity * (num1271 - 1f) + value45) / num1271;
                if (npc.collideX || npc.collideY)
                {
                    npc.ai[0] = 4f;
                    npc.ai[1] = 0f;
                }
                if (num1269 < num1270 || num1269 > 800f || Collision.CanHit(npc.Center, 1, 1, Main.player[npc.target].Center, 1, 1))
                {
                    npc.ai[0] = 0f;
                }
            }
            else if (npc.ai[0] == 4f)
            {
                npc.rotation = npc.velocity.X * 0.1f;
                if (npc.collideX)
                {
                    npc.velocity.X = npc.velocity.X * -0.8f;
                }
                if (npc.collideY)
                {
                    npc.velocity.Y = npc.velocity.Y * -0.8f;
                }
                Vector2 value46;
                if (npc.velocity.X == 0f && npc.velocity.Y == 0f)
                {
                    value46 = Main.player[npc.target].Center - npc.Center;
                    value46.Y -= (float)(Main.player[npc.target].height / 4);
                    value46.Normalize();
                    npc.velocity = value46 * 0.1f;
                }
                float scaleFactor12 = 2f;
                float num1272 = 20f;
                value46 = npc.velocity;
                value46.Normalize();
                value46 *= scaleFactor12;
                npc.velocity = (npc.velocity * (num1272 - 1f) + value46) / num1272;
                npc.ai[1] += 1f;
                if (npc.ai[1] > 180f)
                {
                    npc.ai[0] = 0f;
                    npc.ai[1] = 0f;
                }
                if (Collision.CanHit(npc.Center, 1, 1, Main.player[npc.target].Center, 1, 1))
                {
                    npc.ai[0] = 0f;
                }
                npc.localAI[0] += 1f;
                if (npc.localAI[0] >= 5f && !Collision.SolidCollision(npc.position - new Vector2(10f, 10f), npc.width + 20, npc.height + 20))
                {
                    npc.localAI[0] = 0f;
                    Vector2 center28 = npc.Center;
                    center28.X = Main.player[npc.target].Center.X;
                    if (Collision.CanHit(npc.Center, 1, 1, center28, 1, 1) && Collision.CanHit(npc.Center, 1, 1, center28, 1, 1) && Collision.CanHit(Main.player[npc.target].Center, 1, 1, center28, 1, 1))
                    {
                        npc.ai[0] = 3f;
                        npc.ai[1] = center28.X;
                        npc.ai[2] = center28.Y;
                    }
                    else
                    {
                        center28 = npc.Center;
                        center28.Y = Main.player[npc.target].Center.Y;
                        if (Collision.CanHit(npc.Center, 1, 1, center28, 1, 1) && Collision.CanHit(Main.player[npc.target].Center, 1, 1, center28, 1, 1))
                        {
                            npc.ai[0] = 3f;
                            npc.ai[1] = center28.X;
                            npc.ai[2] = center28.Y;
                        }
                    }
                }
            }
            else if (npc.ai[0] == 5f)
            {
                Player player7 = Main.player[npc.target];
                if (!player7.active || player7.dead)
                {
                    npc.ai[0] = 0f;
                    npc.ai[1] = 0f;
                    npc.netUpdate = true;
                }
                else
                {
                    npc.Center = ((player7.gravDir == 1f) ? player7.Top : player7.Bottom) + new Vector2((float)(player7.direction * 4), 0f);
                    npc.gfxOffY = player7.gfxOffY;
                    npc.velocity = Vector2.Zero;
                    player7.AddBuff(163, 59, true);
                }
            }
            
        }
    }
}