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
    public class AkumaA : ModNPC
	{
        public override string Texture { get { return "AAMod/NPCs/Bosses/Akuma/AkumaA"; } }

        public bool Panic;
        public bool Loludided;
        private bool weakness = false;
        public int fireTimer = 0;

        public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Akuma Awakened; Blazing Fury Incarnate");
			NPCID.Sets.TechnicallyABoss[npc.type] = true;
            Main.npcFrameCount[npc.type] = 3;
        }

		public override void SetDefaults()
		{
			npc.noTileCollide = true;
            npc.width = 84;
            npc.height = 120;
			npc.aiStyle = -1;
			npc.netAlways = true;
            npc.lifeMax = 150000;
            if (npc.life > npc.lifeMax / 3)
            {
                npc.damage = 125;
                npc.defense = 125;
            }
            if (npc.life <= npc.lifeMax / 3)
            {
                npc.damage = 150;
                npc.defense = 100;
            }
            npc.value = Item.buyPrice(20, 0, 0, 0);
            npc.knockBackResist = 0f;
            npc.boss = true;
            npc.aiStyle = -1;
            npc.lavaImmune = true;
            npc.noGravity = true;
            npc.noTileCollide = true;
            npc.behindTiles = true;
            npc.DeathSound = new LegacySoundStyle(2, 88, Terraria.Audio.SoundType.Sound);
            music = mod.GetSoundSlot(Terraria.ModLoader.SoundType.Music, "Sounds/Music/Akuma2");
            musicPriority = MusicPriority.BossHigh;
            bossBag = mod.ItemType("AkumaBag");
            for (int k = 0; k < npc.buffImmune.Length; k++)
            {
                npc.buffImmune[k] = true;
            }
            npc.buffImmune[103] = false;
            npc.alpha = 255;
        }

        public override void ScaleExpertStats(int numPlayers, float bossLifeScale)
        {
            npc.lifeMax = (int)(npc.lifeMax * 0.8f * bossLifeScale);
            npc.defense = (int)(npc.defense * 1.2f);
        }

        private bool fireAttack;
        private int attackFrame;
        private int attackCounter;
        private int attackTimer;
        private int speed = 8;
        public static int MinionCount = 0;

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
            if (fireAttack == true)
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

            npc.frameCounter = 0;
            Main.dayTime = true;
            Main.time = 24000;
            Player player = Main.player[npc.target];
            float dist = npc.Distance(player.Center);
            int speedval = 9;
            if (npc.life <= npc.lifeMax / 3 && npc.type == mod.NPCType<AkumaA>())
            {
                speedval = 11;
            }

            speed = speedval;

            if (Main.player[npc.target].dead || Math.Abs(npc.position.X - Main.player[npc.target].position.X) > 6000f || Math.Abs(npc.position.Y - Main.player[npc.target].position.Y) > 6000f)
            {
                if (Loludided == false)
                {
                    Main.NewText("You just got burned, kid.", Color.DeepSkyBlue.R, Color.DeepSkyBlue.G, Color.DeepSkyBlue.B);
                    Loludided = true;
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

            if (dist > 400 & Main.rand.Next(20) == 1 && fireAttack == false && internalAI[0] < 500)
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
                        Main.NewText("ACK..! WATER! I LOATHE WATER!!!", Color.DeepSkyBlue);
                    }
                }
                else
                {
                    AAAI.BreatheFire(npc, true, mod.ProjectileType<AkumaABreath>(), 2, 2);
                }
                if (attackTimer >= 80)
                {
                    fireAttack = false;
                    attackTimer = 0;
                    attackFrame = 0;
                    attackCounter = 0;
                }
            }
            AAAI.DustOnNPCSpawn(npc, mod.DustType("AkumaADust"), 2, 12);
            npc.rotation = (float)Math.Atan2(npc.velocity.Y, npc.velocity.X) + 1.57f;
            if (npc.velocity.X < 0f)
            {
                npc.spriteDirection = 1;

            }
            else
            {
                npc.spriteDirection = -1;
            }
            if (npc.life <= npc.lifeMax / 3)
            {
                music = mod.GetSoundSlot(Terraria.ModLoader.SoundType.Music, "Sounds/Music/RayOfHope");
            }
            return false;
        }

        public void Attack(NPC npc, Vector2 velocity)
        {
            Player player = Main.player[npc.target];
            if (npc.ai[0] == 1 || npc.ai[0] == 5 || npc.ai[0] == 6 || npc.ai[0] == 11 || npc.ai[0] == 17)
            {
                int Fireballs = Main.expertMode ? 7 : 10;
                for (int Loops = 0; Loops < Fireballs; Loops++)
                {
                    AkumaAttacks.Dragonfire(npc, mod, true);
                }
            }
            if ((npc.ai[0] == 2 || npc.ai[0] == 6 || npc.ai[0] == 9 || npc.ai[0] == 13 || npc.ai[0] == 16))
            {
                int Fireballs = Main.expertMode ? 3 : 5;
                for (int Loops = 0; Loops < Fireballs; Loops++)
                {
                    AAAI.BreatheFire(npc, false, mod.ProjectileType<AkumaABomb>(), 1, 2);
                }
            }
            if (npc.ai[0] == 3 || npc.ai[0] == 4 || npc.ai[0] == 7 || npc.ai[0] == 12 || npc.ai[0] == 15)
            {
                int Fireballs = Main.expertMode ? 12 : 14;
                for (int Loops = 0; Loops < Fireballs; Loops++)
                {
                    AkumaAttacks.Eruption(npc, mod);
                }
            }
            if (npc.ai[0] == 4 || npc.ai[0] == 8 || npc.ai[0] == 10 || npc.ai[0] == 14 || npc.ai[0] == 18)
            {
                int MaxMinons = Main.expertMode ? 3 : 4;
                if (MinionCount < MaxMinons)
                {
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
            Texture2D attackAni = mod.GetTexture("NPCs/Bosses/Akuma/AkumaA");
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
        public override void HitEffect(int hitDirection, double damage)
        {
            int dust1 = mod.DustType<Dusts.AkumaADust>();
            int dust2 = mod.DustType<Dusts.AkumaADust>();
            if (npc.life <= 0)
            {
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
            if (npc.life > npc.lifeMax / 3)
            {
                Panic = false;
            }
            if (npc.life <= npc.lifeMax / 3 && Panic == false && !AAWorld.downedAkuma && npc.type == mod.NPCType<AkumaA>())
            {
                Panic = true;
                Main.NewText("What?! How have you lasted this long?! Why you little... I refuse to be bested by a terrarian again! Have at it!", Color.DeepSkyBlue.R, Color.DeepSkyBlue.G, Color.DeepSkyBlue.B);
            }
            if (npc.life <= npc.lifeMax / 3 && Panic == false && AAWorld.downedAkuma && npc.type == mod.NPCType<AkumaA>())
            {
                Panic = true;
                Main.NewText("Still got it, do you? Ya got fire in your spirit! I like that about you, kid!", Color.DeepSkyBlue.R, Color.DeepSkyBlue.G, Color.DeepSkyBlue.B);
            }


        }

        public const string HeadTex = "AAMod/NPCs/Boss/Akuma/AkumaAHead_Head_Boss";

        public override void BossHeadSlot(ref int index)
        {

            index = NPCHeadLoader.GetBossHeadSlot(HeadTex);

        }
        public override void BossHeadRotation(ref float rotation)
        {

            rotation = npc.rotation;

        }

        public override void NPCLoot()
		{
            if (Main.expertMode)
            {
                //npc.DropLoot(Items.Vanity.Mask.AkumaMask.type, 1f / 7);
                npc.DropLoot(Items.Boss.Akuma.AkumaTrophy.type, 1f / 10);
                if (Main.rand.NextFloat() < 0.1f)
                {
                    Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("EXSoul"));
                }
                npc.DropBossBags();
                
            }
            return;
        }

        public override void BossLoot(ref string name, ref int potionType)
        {
            

            if (!AAWorld.downedAkuma && Main.expertMode)
            {
                Main.NewText("Gah! How could this happen?! Even in my full form?! Fine, take your reward. You earned it.", Color.DeepSkyBlue.R, Color.DeepSkyBlue.G, Color.DeepSkyBlue.B);
                BaseUtility.Chat("The volcanoes of the inferno are finally quelled...", Color.DarkOrange.R, Color.DarkOrange.G, Color.DarkOrange.B, false);

            }
            if (AAWorld.downedAkuma && Main.expertMode)
            {
                Main.NewText("Snuffed out again. You have my respect, kid. Here.", Color.DeepSkyBlue.R, Color.DeepSkyBlue.G, Color.DeepSkyBlue.B);

            }
            if (!Main.expertMode)
            {
                Main.NewText("Nice hacks, kid. Now come back and fight me like a real man in expert mode. Then I’ll give you your prize.", Color.DeepSkyBlue.R, Color.DeepSkyBlue.G, Color.DeepSkyBlue.B);

            }

            if (Main.expertMode)
            {
                potionType = ItemID.SuperHealingPotion;   //boss drops
                AAWorld.downedAkuma = true;
            }
        }

        public override bool StrikeNPC(ref double damage, int defense, ref float knockback, int hitDirection, ref bool crit)
        {
            if (damage > npc.lifeMax / 2)
            {
                Main.NewText("Wuss.", Color.DeepSkyBlue.R, Color.DeepSkyBlue.G, Color.DeepSkyBlue.B);
            }
            return false;
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
    }


    [AutoloadBossHead]
    public class AkumaAArms : AkumaA
    {
        public override string Texture { get { return "AAMod/NPCs/Bosses/Akuma/AkumaAArms"; } }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Akuma Awakened; Blazing Fury Incarnate");
        }

        public override void SetDefaults()
        {
            base.SetDefaults();
             npc.width = 84;
            npc.height = 84;
            npc.dontCountMe = true;
            npc.alpha = 255;
        }

        public override bool? DrawHealthBar(byte hbPosition, ref float scale, ref Vector2 position)
        {
            return false;
        }

        public override bool PreAI()
        {
            if (npc.ai[3] > 0)
                npc.realLife = (int)npc.ai[3];
            if (npc.target < 0 || npc.target == byte.MaxValue || Main.player[npc.target].dead)
                npc.TargetClosest(true);
            if (Main.player[npc.target].dead && npc.timeLeft > 300)
                npc.timeLeft = 300;

            if (Main.npc[(int)npc.ai[1]].alpha < 128)
            {
                if (npc.alpha != 0)
                {
                    for (int num934 = 0; num934 < 2; num934++)
                    {
                        int num935 = Dust.NewDust(new Vector2(npc.position.X, npc.position.Y), npc.width, npc.height, mod.DustType("AkumaADust"), 0f, 0f, 100, default(Color), 2f);
                        Main.dust[num935].noGravity = false;
                        Main.dust[num935].noLight = false;
                    }
                }
                npc.alpha -= 10;
                if (npc.alpha < 0)
                {
                    npc.alpha = 0;
                }
            }

            if (Main.netMode != 1)
            {
                if (!Main.npc[(int)npc.ai[1]].active)
                {
                    npc.life = 0;
                    npc.HitEffect(0, 10.0);
                    npc.active = false;
                    // NetMessage.SendData(28, -1, -1, "", npc.whoAmI, -1f, 0.0f, 0.0f, 0, 0, 0);
                }
            }
            if (npc.life <= npc.lifeMax / 3)
            {
                music = mod.GetSoundSlot(Terraria.ModLoader.SoundType.Music, "Sounds/Music/RayOfHope");
            }

            if (npc.ai[1] < (double)Main.npc.Length)
            {
                // We're getting the center of this NPC.
                Vector2 npcCenter = new Vector2(npc.position.X + (float)npc.width * 0.5f, npc.position.Y + (float)npc.height * 0.5f);
                // Then using that center, we calculate the direction towards the 'parent NPC' of this NPC.
                float dirX = Main.npc[(int)npc.ai[1]].position.X + (float)(Main.npc[(int)npc.ai[1]].width / 2) - npcCenter.X;
                float dirY = Main.npc[(int)npc.ai[1]].position.Y + (float)(Main.npc[(int)npc.ai[1]].height / 2) - npcCenter.Y;
                // We then use Atan2 to get a correct rotation towards that parent NPC.
                npc.rotation = (float)Math.Atan2(dirY, dirX) + 1.57f;
                // We also get the length of the direction vector.
                float length = (float)Math.Sqrt(dirX * dirX + dirY * dirY);
                // We calculate a new, correct distance.
                float dist = (length - (float)npc.width) / length;
                float posX = dirX * dist;
                float posY = dirY * dist;

                // Reset the velocity of this NPC, because we don't want it to move on its own

                if (dirX < 0f)
                {
                    npc.spriteDirection = 1;

                }
                else
                {
                    npc.spriteDirection = -1;
                }
                // And set this NPCs position accordingly to that of this NPCs parent NPC.
                npc.position.X = npc.position.X + posX;
                npc.position.Y = npc.position.Y + posY;
            }
            return false;
        }

        public const string BodyTex = "AAMod/NPCs/Boss/Akuma/AkumaABody_Head_Boss";

        public override void BossHeadSlot(ref int index)
        {

            index = NPCHeadLoader.GetBossHeadSlot(BodyTex);

        }
        public override void BossHeadRotation(ref float rotation)
        {

            rotation = npc.rotation;

        }

        public override void HitEffect(int hitDirection, double damage)
        {
            
            if (npc.life <= 0)
            {
                int dust1 = mod.DustType<Dusts.AkumaADust>();
                int dust2 = mod.DustType<Dusts.AkumaADust>();
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
            if (npc.life > npc.lifeMax / 3)
            {
                Panic = false;
            }
            if (npc.life <= npc.lifeMax / 3 && Panic == false && !AAWorld.downedAkuma && npc.type == mod.NPCType<AkumaA>())
            {
                Panic = true;
                Main.NewText("What?! How have you lasted this long?! Why you little...I refuse to be bested by a terrarian again! Have at it!", Color.DeepSkyBlue.R, Color.DeepSkyBlue.G, Color.DeepSkyBlue.B);
            }
            if (npc.life <= npc.lifeMax / 3 && Panic == false && AAWorld.downedAkuma && npc.type == mod.NPCType<AkumaA>())
            {
                Panic = true;
                Main.NewText("Still got it, do you? Ya got fire in your spirit; I like that about you, kid!", Color.DeepSkyBlue.R, Color.DeepSkyBlue.G, Color.DeepSkyBlue.B);
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
        public override void ScaleExpertStats(int numPlayers, float bossLifeScale)
        {
            npc.lifeMax = (int)(npc.lifeMax * 0.8f * bossLifeScale);
            npc.damage = (int)(npc.damage * 0.8f);
        }
    }
    
    [AutoloadBossHead]
    public class AkumaABody : AkumaA
    {
        public override string Texture { get { return "AAMod/NPCs/Bosses/Akuma/AkumaABody"; } }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Akuma Awakened; Blazing Fury Incarnate");
        }

        public override void SetDefaults()
        {
            base.SetDefaults();
             npc.width = 84;
            npc.height = 84;
            npc.dontCountMe = true;
            npc.alpha = 255;
        }



        public override bool? DrawHealthBar(byte hbPosition, ref float scale, ref Vector2 position)
        {
            return false;
        }

        public override bool PreAI()
        {
            if (npc.ai[3] > 0)
                npc.realLife = (int)npc.ai[3];
            if (npc.target < 0 || npc.target == byte.MaxValue || Main.player[npc.target].dead)
                npc.TargetClosest(true);
            if (Main.player[npc.target].dead && npc.timeLeft > 300)
                npc.timeLeft = 300;

            if (Main.npc[(int)npc.ai[1]].alpha < 128)
            {
                if (npc.alpha != 0)
                {
                    for (int num934 = 0; num934 < 2; num934++)
                    {
                        int num935 = Dust.NewDust(new Vector2(npc.position.X, npc.position.Y), npc.width, npc.height, mod.DustType("AkumaADust"), 0f, 0f, 100, default(Color), 2f);
                        Main.dust[num935].noGravity = false;
                        Main.dust[num935].noLight = false;
                    }
                }
                npc.alpha -= 10;
                if (npc.alpha < 0)
                {
                    npc.alpha = 0;
                }
            }

            if (Main.netMode != 1)
            {
                if (!Main.npc[(int)npc.ai[1]].active)
                {
                    npc.life = 0;
                    npc.HitEffect(0, 10.0);
                    npc.active = false;
                    // NetMessage.SendData(28, -1, -1, "", npc.whoAmI, -1f, 0.0f, 0.0f, 0, 0, 0);
                }
            }
            if (npc.life <= npc.lifeMax  / 3)
            {
                music = mod.GetSoundSlot(Terraria.ModLoader.SoundType.Music, "Sounds/Music/RayOfHope");
            }

            if (npc.ai[1] < (double)Main.npc.Length)
            {
                // We're getting the center of this NPC.
                Vector2 npcCenter = new Vector2(npc.position.X + (float)npc.width * 0.5f, npc.position.Y + (float)npc.height * 0.5f);
                // Then using that center, we calculate the direction towards the 'parent NPC' of this NPC.
                float dirX = Main.npc[(int)npc.ai[1]].position.X + (float)(Main.npc[(int)npc.ai[1]].width / 2) - npcCenter.X;
                float dirY = Main.npc[(int)npc.ai[1]].position.Y + (float)(Main.npc[(int)npc.ai[1]].height / 2) - npcCenter.Y;
                // We then use Atan2 to get a correct rotation towards that parent NPC.

                npc.rotation = (float)Math.Atan2(dirY, dirX) + 1.57f;
                // We also get the length of the direction vector.
                float length = (float)Math.Sqrt(dirX * dirX + dirY * dirY);
                // We calculate a new, correct distance.
                float dist = (length - (float)npc.width) / length;
                float posX = dirX * dist;
                float posY = dirY * dist;

                // Reset the velocity of this NPC, because we don't want it to move on its own
                if (dirX < 0f)
                {
                    npc.spriteDirection = 1;

                }
                else
                {
                    npc.spriteDirection = -1;
                }
                // And set this NPCs position accordingly to that of this NPCs parent NPC.
                npc.position.X = npc.position.X + posX;
                npc.position.Y = npc.position.Y + posY;
            }
            return false;
        }

        public const string BodyTex = "AAMod/NPCs/Boss/Akuma/AkumaABody_Head_Boss";

        public override void BossHeadSlot(ref int index)
        {

            index = NPCHeadLoader.GetBossHeadSlot(BodyTex);

        }
        public override void BossHeadRotation(ref float rotation)
        {

            rotation = npc.rotation;

        }

        public override void HitEffect(int hitDirection, double damage)
        {
            int dust1 = mod.DustType<Dusts.AkumaADust>();
            int dust2 = mod.DustType<Dusts.AkumaADust>();
            if (npc.life <= 0)
            {
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
            if (npc.life > npc.lifeMax  / 3)
            {
                Panic = false;
            }
            if (npc.life <= npc.lifeMax / 3 && Panic == false && !AAWorld.downedAkuma && npc.type == mod.NPCType<AkumaA>())
            {
                Panic = true;
                Main.NewText("What?! How have you lasted this long?! Why you little...I refuse to be bested by a terrarian again! Have at it!", Color.DeepSkyBlue.R, Color.DeepSkyBlue.G, Color.DeepSkyBlue.B);
            }
            if (npc.life <= npc.lifeMax / 3 && Panic == false && AAWorld.downedAkuma && npc.type == mod.NPCType<AkumaA>())
            {
                Panic = true;
                Main.NewText("Still got it, do you? Ya got fire in your spirit; I like that about you, kid!", Color.DeepSkyBlue.R, Color.DeepSkyBlue.G, Color.DeepSkyBlue.B);
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
        public override void ScaleExpertStats(int numPlayers, float bossLifeScale)
        {
            npc.lifeMax = (int)(npc.lifeMax * 0.8f * bossLifeScale);
            npc.damage = (int)(npc.damage * 0.8f);
        }
    }


    [AutoloadBossHead]
    public class AkumaATail : AkumaA
    {
        public override string Texture { get { return "AAMod/NPCs/Bosses/Akuma/AkumaATail"; } }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Akuma Awakened; Blazing Fury Incarnate");
        }

        public override void SetDefaults()
        {
            base.SetDefaults();
            npc.width = 78;
            npc.height = 78;
            npc.dontCountMe = true;
            npc.alpha = 255;
        }



        public override bool? DrawHealthBar(byte hbPosition, ref float scale, ref Vector2 position)
        {
            return false;
        }

        public override bool PreAI()
        {
            if (npc.ai[3] > 0)
                npc.realLife = (int)npc.ai[3];
            if (npc.target < 0 || npc.target == byte.MaxValue || Main.player[npc.target].dead)
                npc.TargetClosest(true);
            if (Main.player[npc.target].dead && npc.timeLeft > 300)
                npc.timeLeft = 300;

            if (Main.netMode != 1)
            {
                if (!Main.npc[(int)npc.ai[1]].active)
                {
                    npc.life = 0;
                    npc.HitEffect(0, 10.0);
                    npc.active = false;
                    // NetMessage.SendData(28, -1, -1, "", npc.whoAmI, -1f, 0.0f, 0.0f, 0, 0, 0);
                }
            }
            if (npc.life <= npc.lifeMax  / 3)
            {
                music = mod.GetSoundSlot(Terraria.ModLoader.SoundType.Music, "Sounds/Music/RayOfHope");
            }

            if (Main.npc[(int)npc.ai[1]].alpha < 128)
            {
                if (npc.alpha != 0)
                {
                    for (int num934 = 0; num934 < 2; num934++)
                    {
                        int num935 = Dust.NewDust(new Vector2(npc.position.X, npc.position.Y), npc.width, npc.height, mod.DustType("AkumaADust"), 0f, 0f, 100, default(Color), 2f);
                        Main.dust[num935].noGravity = false;
                        Main.dust[num935].noLight = false;
                    }
                }
                npc.alpha -= 10;
                if (npc.alpha < 0)
                {
                    npc.alpha = 0;
                }
            }


            if (npc.ai[1] < (double)Main.npc.Length)
            {
                // We're getting the center of this NPC.
                Vector2 npcCenter = new Vector2(npc.position.X + (float)npc.width * 0.5f, npc.position.Y + (float)npc.height * 0.5f);
                // Then using that center, we calculate the direction towards the 'parent NPC' of this NPC.
                float dirX = Main.npc[(int)npc.ai[1]].position.X + (float)(Main.npc[(int)npc.ai[1]].width / 2) - npcCenter.X;
                float dirY = Main.npc[(int)npc.ai[1]].position.Y + (float)(Main.npc[(int)npc.ai[1]].height / 2) - npcCenter.Y;
                // We then use Atan2 to get a correct rotation towards that parent NPC.
                npc.rotation = (float)Math.Atan2(dirY, dirX) + 1.57f;
                // We also get the length of the direction vector.
                float length = (float)Math.Sqrt(dirX * dirX + dirY * dirY);
                // We calculate a new, correct distance.
                float dist = (length - (float)npc.width) / length;
                float posX = dirX * dist;
                float posY = dirY * dist;

                // Reset the velocity of this NPC, because we don't want it to move on its own
                if (dirX < 0f)
                {
                    npc.spriteDirection = 1;

                }
                else
                {
                    npc.spriteDirection = -1;
                }
                // And set this NPCs position accordingly to that of this NPCs parent NPC.
                npc.position.X = npc.position.X + posX;
                npc.position.Y = npc.position.Y + posY;
            }
            return false;
        }

        public const string BodyTex = "AAMod/NPCs/Boss/Akuma/AkumaATail_Head_Boss";

        public override void BossHeadSlot(ref int index)
        {

            index = NPCHeadLoader.GetBossHeadSlot(BodyTex);

        }
        public override void BossHeadRotation(ref float rotation)
        {

            rotation = npc.rotation;

        }

        public override void HitEffect(int hitDirection, double damage)
        {
            int dust1 = mod.DustType<Dusts.AkumaADust>();
            int dust2 = mod.DustType<Dusts.AkumaADust>();
            if (npc.life <= 0)
            {
                Dust.NewDust(new Vector2(npc.position.X, npc.position.Y), npc.width, npc.height, dust1, 0f, 0f, 0, default(Color), 1f);
                Main.dust[dust1].scale *= 1.3f;
                Main.dust[dust1].fadeIn = 1f;
                Main.dust[dust1].noGravity = false;
                Dust.NewDust(new Vector2(npc.position.X, npc.position.Y), npc.width, npc.height, dust2, 0f, 0f, 0, default(Color), 1f);
                Main.dust[dust2].scale *= 1.3f;
                Main.dust[dust2].fadeIn = 1f;
                Main.dust[dust2].noGravity = true;

            }
            if (npc.life > npc.lifeMax  / 3)
            {
                Panic = false;
            }
            if (npc.life <= npc.lifeMax / 3 && Panic == false && !AAWorld.downedAkuma && npc.type == mod.NPCType<AkumaA>())
            {
                Panic = true;
                Main.NewText("What?! How have you lasted this long?! Why you little...I refuse to be bested by a terrarian again! Have at it!", Color.DeepSkyBlue.R, Color.DeepSkyBlue.G, Color.DeepSkyBlue.B);
            }
            if (npc.life <= npc.lifeMax / 3 && Panic == false && AAWorld.downedAkuma && npc.type == mod.NPCType<AkumaA>())
            {
                Panic = true;
                Main.NewText("Still got it, do you? Ya got fire in your spirit; I like that about you, kid!", Color.DeepSkyBlue.R, Color.DeepSkyBlue.G, Color.DeepSkyBlue.B);
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
        public override void ScaleExpertStats(int numPlayers, float bossLifeScale)
        {
            npc.lifeMax = (int)(npc.lifeMax * 0.8f * bossLifeScale);
            npc.damage = (int)(npc.damage * 0.8f);
        }
    }

}
