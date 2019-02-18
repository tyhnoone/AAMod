using Terraria;
using System;
using Terraria.ID;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria.ModLoader;
using Terraria.Audio;
using BaseMod;
using System.IO;

namespace AAMod.NPCs.Bosses.Akuma
{
    [AutoloadBossHead]
    public class Akuma : ModNPC
	{
        
        public override string Texture { get { return "AAMod/NPCs/Bosses/Akuma/Akuma"; } }

        public bool loludided;
        private bool weakness;

        public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Akuma; Draconian Demon");
			NPCID.Sets.TechnicallyABoss[npc.type] = true;
            Main.npcFrameCount[npc.type] = 3;

        }

        public override void ScaleExpertStats(int numPlayers, float bossLifeScale)
        {
            npc.lifeMax = (int)(npc.lifeMax * 0.8f * bossLifeScale);
            npc.defense = (int)(npc.defense * 1.2f);
        }

        public override void SetDefaults()
		{
			npc.noTileCollide = true;
			npc.height = 120;
			npc.width = 84;
			npc.aiStyle = -1;
			npc.netAlways = true;
			npc.knockBackResist = 0f;
            npc.damage = 100;
            npc.defense = 150;
            npc.lifeMax = 170000;
            if (Main.expertMode)
            {
                npc.value = Item.buyPrice(0, 0, 0, 0);
            }
            else
            {
                npc.value = Item.buyPrice(0, 55, 0, 0);
            }
            npc.knockBackResist = 0f;
            npc.boss = true;
            npc.aiStyle = -1;
            npc.lavaImmune = true;
            npc.noGravity = true;
            npc.noTileCollide = true;
            npc.behindTiles = true;
            music = mod.GetSoundSlot(Terraria.ModLoader.SoundType.Music, "Sounds/Music/Akuma");
            npc.DeathSound = new LegacySoundStyle(2, 124, Terraria.Audio.SoundType.Sound);
            for (int k = 0; k < npc.buffImmune.Length; k++)
            {
                npc.buffImmune[k] = true;
            }
            npc.buffImmune[103] = false;
            npc.alpha = 255;
            musicPriority = MusicPriority.BossHigh;
        }
        private bool fireAttack;
        private int attackFrame;
        private int attackCounter; 
        private int attackTimer;
        private int speed = 8;
        public static int MinionCount = 0;
        public int MaxMinons = Main.expertMode ? 3 : 4;

        public float[] internalAI = new float[4];
        public override void SendExtraAI(BinaryWriter writer)
        {
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
            base.ReceiveExtraAI(reader);
            if (Main.netMode == 1)
            {
                internalAI[0] = reader.ReadFloat();
                internalAI[1] = reader.ReadFloat();
                internalAI[2] = reader.ReadFloat();
                internalAI[3] = reader.ReadFloat();
            }
        }

        public override bool PreAI()
        {
            if (fireAttack == true || internalAI[0] >= 500)
            {
                attackCounter++;
                if (attackCounter > 10)
                {
                    attackFrame++;
                    attackCounter = 0;
                }
                if (attackFrame >= 3)
                {
                    attackFrame = 2;
                }
            }
            speed = 8;
            Main.dayTime = true;
            Main.time = 24000;
            Player player = Main.player[npc.target];
            float dist = npc.Distance(player.Center);
            
            if (Main.player[npc.target].dead || Math.Abs(npc.position.X - Main.player[npc.target].position.X) > 6000f || Math.Abs(npc.position.Y - Main.player[npc.target].position.Y) > 6000f)
            {
                if (loludided == false)
                {
                    Main.NewText("I thought you terrarians put up more of a fight. Guess not.", new Color(180, 41, 32));
                    loludided = true;
                }
                npc.velocity.Y = npc.velocity.Y + 1f;
                if ((double)npc.position.Y > Main.rockLayer * 16.0)
                {
                    npc.velocity.Y = npc.velocity.Y + 1f;
                    speed = 30;
                }
                if ((double)npc.position.Y > Main.rockLayer * 16.0)
                {
                    for (int num957 = 0; num957 < 200; num957++)
                    {
                        if (Main.npc[num957].aiStyle == npc.aiStyle)
                        {
                            Main.npc[num957].active = false;
                        }
                    }
                }
            }
            BaseAI.AIWorm(npc, new int[] { mod.NPCType<AncientLung>(), mod.NPCType<AkumaArms>(), mod.NPCType<AkumaBody>(), mod.NPCType<AkumaTail>() }, 6, 8f, speed, 0, true, false, true, false, false);

            internalAI[0]++;
            if (internalAI[0] == 500)
            {
                internalAI[1] += 1;
                Attack(npc, npc.velocity);
            }
            if (internalAI[0] >= 600)
            {
                internalAI[0] = 0;
            }

            if (dist > 300 & Main.rand.Next(20) == 1 && fireAttack == false && internalAI[0] < 500)
            {
                fireAttack = true;
            }
            if (fireAttack == true)
            {
                attackTimer++;
                if ((attackTimer == 20 || attackTimer == 50 || attackTimer == 79) && npc.HasBuff(BuffID.Wet))
                {
                    for (int spawnDust = 0; spawnDust < 2; spawnDust++)
                    {
                        int num935 = Dust.NewDust(new Vector2(npc.position.X, npc.position.Y), npc.width, npc.height, mod.DustType("MireBubbleDust"), 0f, 0f, 90, default(Color), 2f);
                        Main.dust[num935].noGravity = true;
                        Main.dust[num935].velocity.Y -= 1f;
                    }
                    if (weakness == false)
                    {
                        weakness = true;
                        Main.NewText("Water?! AGH...! I CAN'T BREATHE!", new Color(180, 41, 32));
                    }
                }
                else
                {
                    AAAI.BreatheFire(npc, true, mod.ProjectileType<AkumaBreath>(), 2, 2);
                    Main.PlaySound(4, (int)npc.Center.X, (int)npc.Center.Y, 60);
                }
                if (attackTimer >= 80)
                {
                    fireAttack = false;
                    attackTimer = 0;
                    attackFrame = 0;
                    attackCounter = 0;
                }
            }
            AAAI.DustOnNPCSpawn(npc, mod.DustType("AkumaDust"), 2, 12);
            npc.rotation = (float)Math.Atan2(npc.velocity.Y, npc.velocity.X) + 1.57f;
            if (npc.velocity.X < 0f)
            {
                npc.spriteDirection = 1;

            }
            else
            {
                npc.spriteDirection = -1;
            }
			return false;
		}

        public void Attack(NPC npc, Vector2 velocity)
        {
            Player player = Main.player[npc.target];
            if (npc.ai[0] == 1 || npc.ai[0] == 4 || npc.ai[0] == 6 || npc.ai[0] == 10 || npc.ai[0] == 14)
            {
                Roar(roarTimerMax, true);
                int Fireballs = Main.expertMode ? 7 : 10;
                for (int Loops = 0; Loops < Fireballs; Loops++)
                {
                    AkumaAttacks.Dragonfire(npc, mod, false);
                }
            }
            if ((npc.ai[0] == 2 || npc.ai[0] == 7 || npc.ai[0] == 9 || npc.ai[0] == 12 || npc.ai[0] == 15))
            {
                Roar(roarTimerMax, false);
                int Fireballs = Main.expertMode ? 3 : 5;
                for (int Loops = 0; Loops < Fireballs; Loops++)
                {
                    AAAI.BreatheFire(npc, false, mod.ProjectileType<AkumaBomb>(), 1, 2);
                }
            }
            if (npc.ai[0] == 3 || npc.ai[0] == 5 || npc.ai[0] == 8 || npc.ai[0] == 11 || npc.ai[0] == 13)
            {
                if (MinionCount < MaxMinons)
                {
                    Roar(roarTimerMax, false);
                    AkumaAttacks.SpawnLung(player, mod);
                    MinionCount += 1;
                }
            }

            if (npc.ai[0] > 25)
            {
                npc.ai[0] = 0;
            }
        }

        public override bool PreDraw(SpriteBatch spriteBatch, Color drawColor)
        {
            Texture2D texture = Main.npcTexture[npc.type];
            Texture2D attackAni = mod.GetTexture("NPCs/Bosses/Akuma/Akuma");
            var effects = npc.spriteDirection == -1 ? SpriteEffects.None : SpriteEffects.FlipHorizontally;
            if (fireAttack == false)
            {
                spriteBatch.Draw(texture, npc.Center - Main.screenPosition, npc.frame, drawColor, npc.rotation, npc.frame.Size() / 2, npc.scale, npc.spriteDirection == -1 ? SpriteEffects.None : SpriteEffects.FlipHorizontally, 0f);
            }
            if (fireAttack == true)
            {
                Vector2 drawCenter = new Vector2(npc.Center.X, npc.Center.Y);
                int num214 = attackAni.Height / 3;
                int y6 = num214 * attackFrame;
                Main.spriteBatch.Draw(attackAni, drawCenter - Main.screenPosition, new Microsoft.Xna.Framework.Rectangle?(new Microsoft.Xna.Framework.Rectangle(0, y6, attackAni.Width, num214)), drawColor, npc.rotation, new Vector2((float)attackAni.Width / 2f, (float)num214 / 2f), npc.scale, npc.spriteDirection == -1 ? SpriteEffects.None : SpriteEffects.FlipHorizontally, 0f);
            }
            return false;
        }

        public override void NPCLoot()
		{
            if (!Main.expertMode)
            {
                string[] lootTable = { "AkumaTerratool", "DayStorm", "LungStaff", "MorningGlory", "RadiantDawn", "Solar", "SunSpear", "ReignOfFire", "DaybreakArrow", "Daycrusher", "Dawnstrike", "SunStorm", "SunStaff", "DragonSlasher" };
                AAAI.DownedBoss(npc, mod, lootTable, AAWorld.downedAkuma, true, mod.ItemType("CrucibleScale"), 20, 30, false, false, true, 0, mod.ItemType("AkumaTrophy"), false);
                Main.NewText("Hmpf...you’re pretty good kid, but not good enough. Come back once you’ve gotten a bit better.", new Color(180, 41, 32));
                if (!AAWorld.downedAkuma)
                {
                    BaseUtility.Chat("The volcanoes of the inferno are finally quelled...", Color.DarkOrange.R, Color.DarkOrange.G, Color.DarkOrange.B, false);
                }
            }
            if (Main.expertMode)
            {
                Projectile.NewProjectile((new Vector2(npc.position.X, npc.position.Y)), (new Vector2(0f, 0f)), mod.ProjectileType("AkumaTransition"), 0, 0);
            }
            npc.value = 0f;
            npc.boss = false;
		}

        public override void BossLoot(ref string name, ref int potionType)
        {
            if (!Main.expertMode)
            {
                potionType = ItemID.SuperHealingPotion;   //boss drops
                AAWorld.downedAkuma = true;
            }
        }

        public override void HitEffect(int hitDirection, double damage)
        {
            if (npc.life <= 0)
            {
                npc.position.X = npc.position.X + (float)(npc.width / 2);
                npc.position.Y = npc.position.Y + (float)(npc.height / 2);
                npc.position.X = npc.position.X - (float)(npc.width / 2);
                npc.position.Y = npc.position.Y - (float)(npc.height / 2);
                int dust1 = mod.DustType<Dusts.AkumaDust>();
                int dust2 = mod.DustType<Dusts.AkumaDust>();
                Dust.NewDust(new Vector2(npc.position.X, npc.position.Y), npc.width, npc.height, dust1, 0f, 0f, 0, default(Color), 1f);
                Main.dust[dust1].velocity *= 0.5f;
                Main.dust[dust1].scale *= 1.3f;
                Main.dust[dust1].fadeIn = 1f;
                Main.dust[dust1].noGravity = false;
                Dust.NewDust(new Vector2(npc.position.X, npc.position.Y), npc.width, npc.height, dust2, 0f, 0f, 0, default(Color), 1f);
                Main.dust[dust2].velocity *= 0.5f;
                Main.dust[dust2].scale *= 1.3f;
                Main.dust[dust2].fadeIn = 1f;
                Main.dust[dust2].noGravity = true;
            }
        }

        public int roarTimer = 0; //if this is > 0, then use the roaring frame.
        public int roarTimerMax = 120; //default roar timer. only changed for fire breath as it's longer.
        public bool Roaring //wether or not he is roaring. only used clientside for frame visuals.
        {
            get
            {
                return roarTimer > 0;
            }
        }

        public void Roar(int timer, bool fireSound)
        {
            roarTimer = timer;
            if (fireSound)
            {
                Main.PlaySound(4, (int)npc.Center.X, (int)npc.Center.Y, 60);
            }
            else
            {
                int roarSound = mod.GetSoundSlot(Terraria.ModLoader.SoundType.Music, "Sounds/Sounds/AkumaRoar");
                Main.PlaySound(roarSound, (int)npc.Center.X, (int)npc.Center.Y, 92);
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
                projectile.damage *= (int).5;
            }
            else if (projectile.penetrate >= 1)
            {
                projectile.damage *= (int).5;
            }
        }

        public override void BossHeadSpriteEffects(ref SpriteEffects spriteEffects)
        {
            spriteEffects = (npc.spriteDirection == 1 ? SpriteEffects.FlipHorizontally : SpriteEffects.None);
        }

        public override void BossHeadRotation(ref float rotation)
        {
            rotation = npc.rotation;
        }
    }


    [AutoloadBossHead]
    public class AkumaArms : AncientLung
    {
        public override string Texture { get { return "AAMod/NPCs/Bosses/Akuma/AkumaArms"; } }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Akuma; Draconian Demon");
            Main.npcFrameCount[npc.type] = 1;
        }

        public override void SetDefaults()
        {
            base.SetDefaults();
            npc.dontCountMe = true;
        }

        public override bool PreNPCLoot()
        {
            return false;
        }

        public override void BossHeadSpriteEffects(ref SpriteEffects spriteEffects)
        {
            spriteEffects = (npc.spriteDirection == 1 ? SpriteEffects.FlipHorizontally : SpriteEffects.None);
        }

        public override void BossHeadRotation(ref float rotation)
        {
            rotation = npc.rotation;
        }
    }


    [AutoloadBossHead]
    public class AkumaBody : AncientLung
    {
        public override string Texture { get { return "AAMod/NPCs/Bosses/Akuma/AkumaBody"; } }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Akuma; Draconian Demon");
            Main.npcFrameCount[npc.type] = 1;
        }

        public override void SetDefaults()
        {
            base.SetDefaults();
            npc.dontCountMe = true;
        }

        public override bool PreNPCLoot()
        {
            return false;
        }

        public override void BossHeadSpriteEffects(ref SpriteEffects spriteEffects)
        {
            spriteEffects = (npc.spriteDirection == 1 ? SpriteEffects.FlipHorizontally : SpriteEffects.None);
        }

        public override void BossHeadRotation(ref float rotation)
        {
            rotation = npc.rotation;
        }
    }
    
    [AutoloadBossHead]
    public class AkumaTail : AncientLung
    {
        public override string Texture { get { return "AAMod/NPCs/Bosses/Akuma/AkumaTail"; } }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Akuma; Draconian Demon");
            Main.npcFrameCount[npc.type] = 1;
        }

        public override void SetDefaults()
        {
            base.SetDefaults();
            npc.dontCountMe = true;
        }

        public override bool PreNPCLoot()
        {
            return false;
        }

        public override bool? DrawHealthBar(byte hbPosition, ref float scale, ref Vector2 position)
        {
            return false;
        }

        public override void BossHeadSpriteEffects(ref SpriteEffects spriteEffects)
        {
            spriteEffects = (npc.spriteDirection == 1 ? SpriteEffects.FlipHorizontally : SpriteEffects.None);
        }

        public override void BossHeadRotation(ref float rotation)
        {
            rotation = npc.rotation;
        }
    }
    
}
