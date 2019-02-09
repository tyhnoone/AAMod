using BaseMod;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.IO;
using Terraria;
using Terraria.ModLoader;
using AAMod.NPCs.Bosses.SoC.Bosses;
using Terraria.Graphics.Shaders;
using Terraria.ID;

namespace AAMod.NPCs.Bosses.SoC
{
    public class Cthulhu : ModNPC
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Cthulhu, Cosmic Calamity");

        }
        public override void SetDefaults()
        {
            npc.width = 222;
            npc.height = 228;
            npc.alpha = 255;
            npc.damage = 0;
            music = mod.GetSoundSlot(SoundType.Music, "Sounds/Music/Cthulhu");
            npc.lifeMax = 1500000;
            npc.dontTakeDamage = false;
            npc.noGravity = true;
            npc.aiStyle = -1;
            npc.boss = true;
            npc.chaseable = false;
            npc.scale *= 1.2f;
            bossBag = mod.ItemType("SoCCache");
            for (int k = 0; k < npc.buffImmune.Length; k++)
            {
                npc.buffImmune[k] = true;
            }
            npc.knockBackResist = 0f;
        }


        public float[] shootAI = new float[4];

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
        
        public int BoomTimer = 0;
        public int Speechtimer = 0;

        public float ShieldScale = 0;
        public float ShieldRotation = 0;

        public override void AI()
        {
            Player player = Main.player[npc.target];
            float EyeSummon = npc.lifeMax * .85f;
            float BrainSummon = npc.lifeMax * .70f;
            float EaterSummon = npc.lifeMax * .55f;
            float SkullSummon = npc.lifeMax * .40f;
            float RoseSummon = npc.lifeMax * .25f;
            float LeviathanSummon = npc.lifeMax * .15f;

            bool BossAlive = NPC.AnyNPCs(mod.NPCType<DeityEye>()) || NPC.AnyNPCs(mod.NPCType<DeityEater>()) || NPC.AnyNPCs(mod.NPCType<DeityBrain>()) || NPC.AnyNPCs(mod.NPCType<DeitySkull>()) || NPC.AnyNPCs(mod.NPCType<DeityLeviathan>()) || NPC.AnyNPCs(mod.NPCType<DeityRose>());

            Vector2 Explosion = new Vector2(Main.rand.Next((int)npc.position.X + npc.width), Main.rand.Next((int)npc.position.Y - npc.height));

            ShieldRotation += .05f;

            if (BossAlive)
            {
                npc.dontTakeDamage = true;
                if (ShieldScale < .75f)
                {
                    ShieldScale += .05f;
                }
                if (ShieldScale >= .75f)
                {
                    ShieldScale = .75f;
                }
            }
            else
            {
                if (ShieldScale > 0)
                {
                    ShieldScale -= .05f;
                }
                if (ShieldScale <= 0)
                {
                    npc.dontTakeDamage = false;
                    ShieldScale = 0;
                }
            }

            if (npc.ai[1] == 1f)
            {
                BoomTimer++;
                npc.ai[3]++;
                if (BoomTimer == 60)
                {
                    Projectile.NewProjectile(Explosion, new Vector2(0, 0), mod.ProjectileType<CthulhuDeathBoom>(), 0, 0, Main.myPlayer);
                    BoomTimer = 0;
                }
                if (npc.ai[3] == 40)
                {
                    Main.NewText("AAAAAAAAAGGHHH….!!", Color.DarkCyan);
                }
                
                if (npc.ai[3] == 100)
                {
                    Main.NewText("NO!", Color.DarkCyan);
                }

                if (npc.ai[3] == 160)
                {
                    Main.NewText("NO!!!", Color.DarkCyan);
                }

                if (npc.ai[3] == 220)
                {
                    Main.NewText("IMPOSSIBLE", Color.DarkCyan);
                }

                if (npc.ai[3] == 280)
                {
                    Main.NewText("BY THE NAME OF AZATHOTH!", Color.DarkCyan);
                }

                if (npc.ai[3] == 340)
                {
                    Main.NewText("I CURSE YOU AND YOUR WORLD!", Color.DarkCyan);
                }

                if (npc.ai[3] == 400)
                {
                    Main.NewText("MAY YOU AND YOUR KIND ROT IN THE DEPTHS OF THE OCEAN!", Color.DarkCyan);
                }

                if (npc.ai[3] == 460)
                {
                    Main.NewText("GRAAAAAAAAAAAHHH…!", Color.DarkCyan);
                }

                if (npc.ai[3] == 520)
                {
                    Projectile.NewProjectile(npc.Center, new Vector2(0, 0), mod.ProjectileType<CthulhuDeath>(), 0, 0, Main.myPlayer);
                    Gore.NewGore(npc.Center, npc.velocity, mod.GetGoreSlot("Gores/CthulhuGore"), 1.2f);
                    npc.life = 0;
                }
                    return;
            }

            BaseAI.AISpaceOctopus(npc, ref customAI, .1f, 1, 0f, 120f, FireMagic);

            if (npc.life < EyeSummon && npc.ai[2] == 0)
            {
                npc.ai[2] = 1;
                Main.NewText("Cyaegha! Scorch this insignificant mortal with your flames of agony!", Color.DarkCyan);
                if (Main.netMode != 1)
                {
                    NPC.SpawnOnPlayer(player.whoAmI, mod.NPCType("DeityEye"));
                }
            }

            if (npc.life < EaterSummon && npc.ai[2] == 1)
            {
                npc.ai[2] = 2;
                Main.NewText("Cyaegha! Scorch this insignificant mortal with your flames of agony!", Color.DarkCyan);
                if (Main.netMode != 1)
                {
                    NPC.SpawnOnPlayer(player.whoAmI, mod.NPCType("DeityEater"));
                }
            }

            if (npc.life < BrainSummon && npc.ai[2] == 2)
            {
                npc.ai[2] = 3;
                Main.NewText("Lu'Kthu! Send your minions upon this pest to distract them!", Color.DarkCyan);
                if (Main.netMode != 1)
                {
                    NPC.SpawnOnPlayer(player.whoAmI, mod.NPCType("DeityBrain"));
                }
            }

            if (npc.life < SkullSummon && npc.ai[2] == 3)
            {
                npc.ai[2] = 4;
                Main.NewText("Rhan-Tegoth! Crush this annoyance with your claws of pain!", Color.DarkCyan);
                if (Main.netMode != 1)
                {
                    NPC.SpawnOnPlayer(player.whoAmI, mod.NPCType("DeitySkull"));
                }
            }

            if (npc.life < RoseSummon && npc.ai[2] == 4)
            {
                npc.ai[2] = 5;
                Main.NewText("Ei'Lor! Devour my foe for me so I may destroy this world!", Color.DarkCyan);
                if (Main.netMode != 1)
                {
                    NPC.SpawnOnPlayer(player.whoAmI, mod.NPCType("DeityRose"));
                }
            }

            if (npc.life < LeviathanSummon && npc.ai[2] == 5)
            {
                npc.ai[2] = 6;
                Main.NewText("THAT TEARS IT! VILE-OCT! DESTROY THIS INSIGNIFICANT PEST!", Color.DarkCyan);
                if (Main.netMode != 1)
                {
                    NPC.SpawnOnPlayer(player.whoAmI, mod.NPCType("DeityLeviathan"));
                }
            }

            if (npc.life <= npc.lifeMax / 10 )
            {
                
                music = mod.GetSoundSlot(SoundType.Music, "Sounds/Music/LastStand");
                if (npc.ai[2] == 6)
                {
                    npc.ai[2] = 7;
                    Main.NewText("I SHALL NOT LOSE TO A MORTAL AGAIN. YOU WILL FEEL MY WRATH UPON YOUR WORLD!", Color.DarkCyan);
                }
            }
        }

        public override void NPCLoot()
        {
            if (Main.expertMode)
            {
                npc.DropBossBags();
                AAWorld.downedSoC = true;
            }
            else
            {
                Main.NewText("Cheaters do not deserve the spoils of battle", Color.DarkCyan);
            }
        }

        int ShootThis;
        int Loop;
        
        public void FireMagic(NPC npc, Vector2 velocity)
        {
            Player player = Main.player[npc.target];
            int num429 = 1;
            if (npc.position.X + (npc.width / 2) < Main.player[npc.target].position.X + Main.player[npc.target].width)
            {
                num429 = -1;
            }
            Vector2 PlayerDistance = new Vector2(npc.position.X + npc.width * 0.5f, npc.position.Y + npc.height * 0.5f);
            float PlayerPosX = Main.player[npc.target].position.X + (Main.player[npc.target].width / 2) + (num429 * 180) - PlayerDistance.X;
            float PlayerPosY = Main.player[npc.target].position.Y + (Main.player[npc.target].height / 2) - PlayerDistance.Y;
            float PlayerPos = (float)Math.Sqrt((PlayerPosX * PlayerPosX) + (PlayerPosY * PlayerPosY));
            float num433 = 6f;
            PlayerPosX = Main.player[npc.target].position.X + (Main.player[npc.target].width / 2) - PlayerDistance.X;
            PlayerPosY = Main.player[npc.target].position.Y + (Main.player[npc.target].height / 2) - PlayerDistance.Y;
            PlayerPos = (float)Math.Sqrt((PlayerPosX * PlayerPosX + PlayerPosY * PlayerPosY));
            PlayerPos = num433 / PlayerPos;
            PlayerPosX *= PlayerPos;
            PlayerPosY *= PlayerPos;
            PlayerPosY += Main.rand.Next(-40, 41) * 0.01f;
            PlayerPosX += Main.rand.Next(-40, 41) * 0.01f;
            PlayerPosY += npc.velocity.Y * 0.5f;
            PlayerPosX += npc.velocity.X * 0.5f;
            PlayerDistance.X -= PlayerPosX * 1f;
            PlayerDistance.Y -= PlayerPosY * 1f;
            Vector2 spawnAt = npc.Center + new Vector2(0f, npc.height / 2f);
            npc.ai[0] += 1;
            if (npc.ai[0] == 1 || npc.ai[0] == 4 || npc.ai[0] == 6 || npc.ai[0] == 15 || npc.ai[0] == 20)
            {
                ShootThis = mod.ProjectileType<CthulhuNuke>();
            }
            if (npc.ai[0] == 2 || npc.ai[0] == 3 || npc.ai[0] == 9 || npc.ai[0] == 17 || npc.ai[0] == 22)
            {
                NPC.NewNPC((int)spawnAt.X, (int)spawnAt.Y, mod.NPCType("Portal"), 0, -npc.velocity.X * 1.2f, -npc.velocity.Y * 1.2f);
                return;
            }
            if (npc.ai[0] == 5 || npc.ai[0] == 12 || npc.ai[0] == 16 || npc.ai[0] == 21 || npc.ai[0] == 25)
            {
                ShootThis = mod.ProjectileType<CthulhuShot>();
            }
            if (npc.ai[0] == 7 || npc.ai[0] == 11 || npc.ai[0] == 14 || npc.ai[0] == 18 || npc.ai[0] == 24)
            {
                ShootThis = mod.ProjectileType<Watcher>();
                Loop = 5;
            }
            if (npc.ai[0] == 8 || npc.ai[0] == 10 || npc.ai[0] == 13 || npc.ai[0] == 19 || npc.ai[0] == 23)
            {
                ShootThis = mod.ProjectileType<Watcher>();
                Loop = 9;
            }
            if (ShootThis == mod.ProjectileType<Watcher>())
            {
                float spread = 45f * 0.0174f;
                float baseSpeed = (float)Math.Sqrt((PlayerPosX * PlayerPosX) + (PlayerPosY * PlayerPosY));
                double startAngle = Math.Atan2(PlayerPosX, PlayerPosY) - .1d;
                double deltaAngle = spread / 6f;
                double offsetAngle;
                for (int i = 0; i < Loop; i++)
                {
                    offsetAngle = startAngle + (deltaAngle * i);
                    Projectile.NewProjectile(npc.Center.X, npc.Center.Y, baseSpeed * (float)Math.Sin(offsetAngle), baseSpeed * (float)Math.Cos(offsetAngle), ShootThis, (int)(npc.damage * .5f), 0f, Main.myPlayer);
                }
            }
            else
            {
                Projectile.NewProjectile(PlayerDistance.X, PlayerDistance.Y, PlayerPosX * 2, PlayerPosY * 2, ShootThis, (int)(npc.damage * .8f), 0f, Main.myPlayer);
            }
            if (npc.ai[0] > 25)
            {
                npc.ai[0] = 0;
            }
        }

        public override void HitEffect(int hitDirection, double damage)
        {
            if (npc.life <= 0 && npc.ai[1] != 1 && Main.expertMode)
            {
                npc.ai[1] = 1f;
                npc.life = npc.lifeMax;
                npc.netUpdate = true;
                npc.dontTakeDamage = true;
            }
        }

        public override bool PreDraw(SpriteBatch sb, Color drawColor)
        {
            bool BossAlive = NPC.AnyNPCs(mod.NPCType<DeityEye>()) || NPC.AnyNPCs(mod.NPCType<DeityEater>()) || NPC.AnyNPCs(mod.NPCType<DeityBrain>()) || NPC.AnyNPCs(mod.NPCType<DeitySkull>()) || NPC.AnyNPCs(mod.NPCType<DeityLeviathan>()) || NPC.AnyNPCs(mod.NPCType<DeityRose>());
            Texture2D currentTex = Main.npcTexture[npc.type];
            Texture2D GlowTex = mod.GetTexture("Glowmasks/Cthulhu_Glow");
            Texture2D Barrier = mod.GetTexture("NPCs/Bosses/SoC/CthulhuBarrier");
            Texture2D Shield = mod.GetTexture("NPCs/Bosses/SoC/CthulhuShield");
            Vector2 drawCenter = new Vector2(npc.Center.X, npc.Center.Y);

            Main.spriteBatch.Draw(currentTex, (drawCenter - Main.screenPosition), new Rectangle?(new Rectangle(0, 0, currentTex.Width, currentTex.Height)), drawColor, npc.rotation, new Vector2(currentTex.Width / 2f, currentTex.Height / 2f), npc.scale, SpriteEffects.None, 0f);

            //draw glow/glow afterimage
            BaseDrawing.DrawTexture(sb, GlowTex, 0, npc, AAColor.Cthulhu2);
            BaseDrawing.DrawAfterimage(sb, GlowTex, 0, npc, 0.8f, 1f, 6, false, 0f, 0f, AAColor.Cthulhu2);

            //Draw Shield
            int shader = GameShaders.Armor.GetShaderIdFromItemId(ItemID.AcidDye);

            if (BossAlive)
            {
                BaseDrawing.DrawTexture(sb, Shield, shader, npc.position, Shield.Width, Shield.Height, ShieldScale, ShieldRotation, 0, 1, new Rectangle(0, 0, Shield.Width, Shield.Height), AAColor.Cthulhu);
                BaseDrawing.DrawTexture(sb, Barrier, 0, npc.position, Barrier.Width, Barrier.Height, ShieldScale, ShieldRotation, 0, 1, new Rectangle(0, 0, Barrier.Width, Barrier.Height), Color.White);
            }
            return false;
        }
    }
}