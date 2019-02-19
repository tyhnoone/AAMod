using Terraria;
using System;
using Terraria.ID;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria.ModLoader;
using Terraria.Audio;
using BaseMod;
using System.IO;
using Terraria.Graphics.Shaders;

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

            Player player = Main.player[npc.target];

            if (fireAttack == true || internalAI[0] >= 500)
            {
                npc.frameCounter++;

                if (npc.frameCounter >= 10)
                {
                    npc.frameCounter = 0;
                    npc.frame.Y += 146;
                    if (npc.frame.Y > (146 * 3))
                    {
                        npc.frame.Y = npc.frame.Y * 2;
                    }
                }
            }
            else
            {
                npc.frameCounter = 0;
                npc.frame.Y = 0;
            }
            Main.dayTime = true;
            Main.time = 24000;
            float dist = npc.Distance(player.Center);
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

            npc.spriteDirection = npc.velocity.X > 0 ? 1 : -1;
            npc.ai[1]++;
            if (npc.ai[1] >= 1200)
                npc.ai[1] = 0;
            npc.TargetClosest(true);
            if (!Main.player[npc.target].active || Main.player[npc.target].dead)
            {
                npc.TargetClosest(true);
                if (!Main.player[npc.target].active || Main.player[npc.target].dead)
                {
                    npc.ai[3]++;
                    npc.velocity.Y = npc.velocity.Y + 0.11f;
                    if (npc.ai[3] >= 300)
                    {
                        npc.active = false;
                    }
                }
                else
                    npc.ai[3] = 0;
            }

            for (int k = 0; k < 2; k++)
            {
                int dust = Dust.NewDust(npc.position - new Vector2(8f, 8f), npc.width + 16, npc.height + 16, 6, 0f, 0f, 0, Color.Black, 0.2f);
                Main.dust[dust].velocity *= 0.0f;
                Main.dust[dust].noGravity = true;
            }
            if (Main.netMode != 1)
            {
                if (npc.ai[0] == 0)
                {
                    npc.realLife = npc.whoAmI;
                    int latestNPC = npc.whoAmI;

                    for (int i = 0; i < 3; ++i)
                    {
                        latestNPC = NPC.NewNPC((int)npc.Center.X, (int)npc.Center.Y, mod.NPCType("AkumaABody"), npc.whoAmI, 0, latestNPC);
                        Main.npc[(int)latestNPC].realLife = npc.whoAmI;
                        Main.npc[(int)latestNPC].ai[3] = npc.whoAmI;

                        latestNPC = NPC.NewNPC((int)npc.Center.X, (int)npc.Center.Y, mod.NPCType("AkumaAArms"), npc.whoAmI, 0, latestNPC);
                        Main.npc[(int)latestNPC].realLife = npc.whoAmI;
                        Main.npc[(int)latestNPC].ai[3] = npc.whoAmI;
                    }
                    latestNPC = NPC.NewNPC((int)npc.Center.X, (int)npc.Center.Y, mod.NPCType("AkumaABody"), npc.whoAmI, 0, latestNPC);
                    Main.npc[(int)latestNPC].realLife = npc.whoAmI;
                    Main.npc[(int)latestNPC].ai[3] = npc.whoAmI;
                    
                    latestNPC = NPC.NewNPC((int)npc.Center.X, (int)npc.Center.Y, mod.NPCType("AkumaABody1"), npc.whoAmI, 0, latestNPC);
                    Main.npc[latestNPC].realLife = npc.whoAmI;
                    Main.npc[latestNPC].ai[3] = npc.whoAmI;

                    latestNPC = NPC.NewNPC((int)npc.Center.X, (int)npc.Center.Y, mod.NPCType("AkumaATail"), npc.whoAmI, 0, latestNPC);
                    Main.npc[(int)latestNPC].realLife = npc.whoAmI;
                    Main.npc[(int)latestNPC].ai[3] = npc.whoAmI;

                    npc.ai[0] = 1;
                    npc.netUpdate = true;
                }
            }

            int minTilePosX = (int)(npc.position.X / 16.0) - 1;
            int maxTilePosX = (int)((npc.position.X + npc.width) / 16.0) + 2;
            int minTilePosY = (int)(npc.position.Y / 16.0) - 1;
            int maxTilePosY = (int)((npc.position.Y + npc.height) / 16.0) + 2;
            if (minTilePosX < 0)
                minTilePosX = 0;
            if (maxTilePosX > Main.maxTilesX)
                maxTilePosX = Main.maxTilesX;
            if (minTilePosY < 0)
                minTilePosY = 0;
            if (maxTilePosY > Main.maxTilesY)
                maxTilePosY = Main.maxTilesY;

            bool collision = true;

            float speed = 18f;
            float acceleration = 0.09f;

            Vector2 npcCenter = new Vector2(npc.position.X + npc.width * 0.5f, npc.position.Y + npc.height * 0.5f);
            float targetXPos = Main.player[npc.target].position.X + (Main.player[npc.target].width / 2);
            float targetYPos = Main.player[npc.target].position.Y + (Main.player[npc.target].height / 2);

            float targetRoundedPosX = (float)((int)(targetXPos / 16.0) * 16);
            float targetRoundedPosY = (float)((int)(targetYPos / 16.0) * 16);
            npcCenter.X = (float)((int)(npcCenter.X / 16.0) * 16);
            npcCenter.Y = (float)((int)(npcCenter.Y / 16.0) * 16);
            float dirX = targetRoundedPosX - npcCenter.X;
            float dirY = targetRoundedPosY - npcCenter.Y;

            float length = (float)Math.Sqrt(dirX * dirX + dirY * dirY);
            if (!collision)
            {
                npc.TargetClosest(true);
                npc.velocity.Y = npc.velocity.Y + 0.11f;
                if (npc.velocity.Y > speed)
                    npc.velocity.Y = speed;
                if (Math.Abs(npc.velocity.X) + Math.Abs(npc.velocity.Y) < speed * 0.4)
                {
                    if (npc.velocity.X < 0.0)
                        npc.velocity.X = npc.velocity.X - acceleration * 1.1f;
                    else
                        npc.velocity.X = npc.velocity.X + acceleration * 1.1f;
                }
                else if (npc.velocity.Y == speed)
                {
                    if (npc.velocity.X < dirX)
                        npc.velocity.X = npc.velocity.X + acceleration;
                    else if (npc.velocity.X > dirX)
                        npc.velocity.X = npc.velocity.X - acceleration;
                }
                else if (npc.velocity.Y > 4.0)
                {
                    if (npc.velocity.X < 0.0)
                        npc.velocity.X = npc.velocity.X + acceleration * 0.9f;
                    else
                        npc.velocity.X = npc.velocity.X - acceleration * 0.9f;
                }
            }
            else
            {
                if (npc.soundDelay == 0)
                {
                    float num1 = length / 40f;
                    if (num1 < 10.0)
                        num1 = 10f;
                    if (num1 > 20.0)
                        num1 = 20f;
                    npc.soundDelay = (int)num1;
                }
                float absDirX = Math.Abs(dirX);
                float absDirY = Math.Abs(dirY);
                float newSpeed = speed / length;
                dirX = dirX * newSpeed;
                dirY = dirY * newSpeed;
                if (npc.velocity.X > 0.0 && dirX > 0.0 || npc.velocity.X < 0.0 && dirX < 0.0 || (npc.velocity.Y > 0.0 && dirY > 0.0 || npc.velocity.Y < 0.0 && dirY < 0.0))
                {
                    if (npc.velocity.X < dirX)
                        npc.velocity.X = npc.velocity.X + acceleration;
                    else if (npc.velocity.X > dirX)
                        npc.velocity.X = npc.velocity.X - acceleration;
                    if (npc.velocity.Y < dirY)
                        npc.velocity.Y = npc.velocity.Y + acceleration;
                    else if (npc.velocity.Y > dirY)
                        npc.velocity.Y = npc.velocity.Y - acceleration;
                    if (Math.Abs(dirY) < speed * 0.2 && (npc.velocity.X > 0.0 && dirX < 0.0 || npc.velocity.X < 0.0 && dirX > 0.0))
                    {
                        if (npc.velocity.Y > 0.0)
                            npc.velocity.Y = npc.velocity.Y + acceleration * 2f;
                        else
                            npc.velocity.Y = npc.velocity.Y - acceleration * 2f;
                    }
                    if (Math.Abs(dirX) < speed * 0.2 && (npc.velocity.Y > 0.0 && dirY < 0.0 || npc.velocity.Y < 0.0 && dirY > 0.0))
                    {
                        if (npc.velocity.X > 0.0)
                            npc.velocity.X = npc.velocity.X + acceleration * 2f;
                        else
                            npc.velocity.X = npc.velocity.X - acceleration * 2f;
                    }
                }
                else if (absDirX > absDirY)
                {
                    if (npc.velocity.X < dirX)
                        npc.velocity.X = npc.velocity.X + acceleration * 1.1f;
                    else if (npc.velocity.X > dirX)
                        npc.velocity.X = npc.velocity.X - acceleration * 1.1f;
                    if (Math.Abs(npc.velocity.X) + Math.Abs(npc.velocity.Y) < speed * 0.5)
                    {
                        if (npc.velocity.Y > 0.0)
                            npc.velocity.Y = npc.velocity.Y + acceleration;
                        else
                            npc.velocity.Y = npc.velocity.Y - acceleration;
                    }
                }
                else
                {
                    if (npc.velocity.Y < dirY)
                        npc.velocity.Y = npc.velocity.Y + acceleration * 1.1f;
                    else if (npc.velocity.Y > dirY)
                        npc.velocity.Y = npc.velocity.Y - acceleration * 1.1f;
                    if (Math.Abs(npc.velocity.X) + Math.Abs(npc.velocity.Y) < speed * 0.5)
                    {
                        if (npc.velocity.X > 0.0)
                            npc.velocity.X = npc.velocity.X + acceleration;
                        else
                            npc.velocity.X = npc.velocity.X - acceleration;
                    }
                }
            }
            npc.rotation = (float)Math.Atan2(npc.velocity.Y, npc.velocity.X) + 1.57f;

            if (collision)
            {
                if (npc.localAI[0] != 1)
                    npc.netUpdate = true;
                npc.localAI[0] = 1f;
            }
            else
            {
                if (npc.localAI[0] != 0.0)
                    npc.netUpdate = true;
                npc.localAI[0] = 0.0f;
            }
            if ((npc.velocity.X > 0.0 && npc.oldVelocity.X < 0.0 || npc.velocity.X < 0.0 && npc.oldVelocity.X > 0.0 || (npc.velocity.Y > 0.0 && npc.oldVelocity.Y < 0.0 || npc.velocity.Y < 0.0 && npc.oldVelocity.Y > 0.0)) && !npc.justHit)
                npc.netUpdate = true;


            if (npc.life <= npc.lifeMax / 3)
            {
                music = mod.GetSoundSlot(Terraria.ModLoader.SoundType.Music, "Sounds/Music/RayOfHope");
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

            return false;
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


        public static Texture2D glowTex = null, glowTex2 = null, glowTex3 = null, glowTex4 = null, glowTex5 = null;

        public Vector2 position, oldPosition;
        public Rectangle Hitbox;
        public Vector2 Center
        {
            get { return new Vector2(position.X + ((float)Hitbox.Width * 0.5f), position.Y + ((float)Hitbox.Height * 0.5f)); }
            set { position = new Vector2(value.X - ((float)Hitbox.Width * 0.5f), value.Y - ((float)Hitbox.Height * 0.5f)); }
        }

        public override bool PreDraw(SpriteBatch spriteBatch, Color drawColor)
        {
            Texture2D texture = Main.npcTexture[npc.type];
            if (glowTex == null)
            {
                glowTex = mod.GetTexture("Glowmasks/AkumaA_Glow");
                glowTex2 = mod.GetTexture("Glowmasks/AkumaAArms_Glow");
                glowTex3 = mod.GetTexture("Glowmasks/AkumaABody_Glow");
                glowTex4 = mod.GetTexture("Glowmasks/AkumaABody1_Glow");
                glowTex5 = mod.GetTexture("Glowmasks/AkumaATail_Glow");
            }
            npc.position.Y += npc.height * 0.5f;


            int shader = GameShaders.Armor.GetShaderIdFromItemId(ItemID.LivingOceanDye);
            if (fireAttack == true || internalAI[0] >= 500)
            {
                shader = GameShaders.Armor.GetShaderIdFromItemId(ItemID.LivingFlameDye);
            }
            else
            {
                shader = GameShaders.Armor.GetShaderIdFromItemId(ItemID.LivingOceanDye);
            }

            Color lightColor = npc.GetAlpha(BaseDrawing.GetLightColor(Center));

            Texture2D myGlowTex = (npc.type == mod.NPCType<AkumaA>() ? glowTex : npc.type == mod.NPCType<AkumaAArms>() ? glowTex2 : npc.type == mod.NPCType<AkumaABody>() ? glowTex3 : npc.type == mod.NPCType<AkumaABody1>() ? glowTex4 : glowTex5);
            spriteBatch.Draw(texture, npc.Center - Main.screenPosition, npc.frame, drawColor, npc.rotation, npc.frame.Size() / 2, npc.scale, npc.spriteDirection == -1 ? SpriteEffects.None : SpriteEffects.FlipHorizontally, 0f);
            BaseDrawing.DrawTexture(spriteBatch, myGlowTex, shader, npc, Color.White);
            return false;
        }

        public override void HitEffect(int hitDirection, double damage)
        {
            int dust1 = mod.DustType<Dusts.AkumaADust>();
            int dust2 = mod.DustType<Dusts.AkumaDust>();
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

        public override void ModifyHitByItem(Player player, Item item, ref int damage, ref float knockback, ref bool crit)
        {
            ModifyCritArea(npc, ref crit);
        }

        public override void ModifyHitByProjectile(Projectile projectile, ref int damage, ref float knockback, ref bool crit, ref int hitDirection)
        {
            ModifyCritArea(npc, ref crit);
        }

        private void ModifyCritArea(NPC npc, ref bool crit)
        {
            if (npc.realLife >= 0)
            {
                if (npc.whoAmI == npc.realLife)
                {
                    crit = true;
                }
                if (npc.ai[0] == 0)
                {
                    crit = false;
                }
            }
        }

        public override void UpdateLifeRegen(ref int damage)
        {
            if (npc.realLife >= 0 && npc.whoAmI != npc.realLife)
            {
                damage = 0;
                npc.lifeRegen = 0;
            }
        }

        public override void OnHitByProjectile(Projectile projectile, int damage, float knockback, bool crit)
        {
            MakeSegmentsImmune(npc, projectile.owner);
        }

        public override void OnHitByItem(Player player, Item item, int damage, float knockback, bool crit)
        {
            MakeSegmentsImmune(npc, player.whoAmI);
        }
        
        public void MakeSegmentsImmune(NPC npc, int id)
        {
            if (npc.realLife >= 0)
            {
                bool last = false;
                NPC parent = Main.npc[npc.realLife];
                parent.lifeRegen = npc.lifeRegen;
                int i = 0;
                while (parent.ai[0] > 0 || last)
                {
                    parent.immune[id] = npc.immune[id];
                    for (int j = 0; j < npc.buffType.Length; j++)
                    {
                        if (npc.buffType[j] > 0 && npc.buffTime[j] > 0)
                        {
                            parent.buffType[j] = npc.buffType[j];
                            parent.buffTime[j] = npc.buffTime[j];
                        }
                    }
                    if (last) { break; }
                    parent = Main.npc[(int)parent.ai[0]];
                    if (parent.ai[0] == 0) { last = true; }
                    if (i++ > 200) { throw new InvalidOperationException("Recursion detected"); } // Just in case
                }
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
    public class AkumaAArms : AkumaA
    {
        public override string Texture { get { return "AAMod/NPCs/Bosses/Akuma/AkumaAArms"; } }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Akuma Awakened; Blazing Fury Incarnate");
            Main.npcFrameCount[npc.type] = 1;
        }

        public override void SetDefaults()
        {
            base.SetDefaults();
            npc.dontCountMe = true;
        }

        public override bool? DrawHealthBar(byte hbPosition, ref float scale, ref Vector2 position)
        {
            return false;
        }

        public override bool PreNPCLoot()
        {
            return false;
        }

        public override bool PreAI()
        {
            Vector2 chasePosition = Main.npc[(int)npc.ai[1]].Center;
            Vector2 directionVector = chasePosition - npc.Center;
            npc.spriteDirection = ((directionVector.X > 0f) ? 1 : -1);
            if (npc.ai[3] > 0)
                npc.realLife = (int)npc.ai[3];
            if (npc.target < 0 || npc.target == byte.MaxValue || Main.player[npc.target].dead)
                npc.TargetClosest(true);
            if (Main.player[npc.target].dead && npc.timeLeft > 300)
                npc.timeLeft = 300;

            if (Main.netMode != 1)
            {
                if (!Main.npc[(int)npc.ai[1]].active || Main.npc[(int)npc.ai[3]].type != mod.NPCType("AkumaA"))
                {
                    npc.life = 0;
                    npc.HitEffect(0, 10.0);
                    npc.active = false;
                    NetMessage.SendData(28, -1, -1, null, npc.whoAmI, -1f, 0.0f, 0.0f, 0, 0, 0);
                }
            }

            if (npc.ai[1] < (double)Main.npc.Length)
            {
                Vector2 npcCenter = new Vector2(npc.position.X + (float)npc.width * 0.5f, npc.position.Y + (float)npc.height * 0.5f);
                float dirX = Main.npc[(int)npc.ai[1]].position.X + (float)(Main.npc[(int)npc.ai[1]].width / 2) - npcCenter.X;
                float dirY = Main.npc[(int)npc.ai[1]].position.Y + (float)(Main.npc[(int)npc.ai[1]].height / 2) - npcCenter.Y;
                npc.rotation = (float)Math.Atan2(dirY, dirX) + 1.57f;
                float length = (float)Math.Sqrt(dirX * dirX + dirY * dirY);
                float dist = (length - (float)npc.width) / length;
                float posX = dirX * dist;
                float posY = dirY * dist;

                if (dirX < 0f)
                {
                    npc.spriteDirection = 1;

                }
                else
                {
                    npc.spriteDirection = -1;
                }

                npc.velocity = Vector2.Zero;
                npc.position.X = npc.position.X + posX;
                npc.position.Y = npc.position.Y + posY;
            }

            Player player = Main.player[npc.target];
            if (npc.target < 0 || npc.target == 255 || Main.player[npc.target].dead || !Main.player[npc.target].active)
            {
                npc.TargetClosest(true);
            }
            npc.netUpdate = true;
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

        public override void ModifyHitByItem(Player player, Item item, ref int damage, ref float knockback, ref bool crit)
        {
            ModifyCritArea(npc, ref crit);
        }

        public override void ModifyHitByProjectile(Projectile projectile, ref int damage, ref float knockback, ref bool crit, ref int hitDirection)
        {
            ModifyCritArea(npc, ref crit);
        }

        private void ModifyCritArea(NPC npc, ref bool crit)
        {
            if (npc.realLife >= 0)
            {
                if (npc.whoAmI == npc.realLife)
                {
                    crit = true;
                }
                if (npc.ai[0] == 0)
                {
                    crit = false;
                }
            }
        }

        public override void UpdateLifeRegen(ref int damage)
        {
            if (npc.realLife >= 0 && npc.whoAmI != npc.realLife)
            {
                damage = 0;
                npc.lifeRegen = 0;
            }
        }

        public override void OnHitByProjectile(Projectile projectile, int damage, float knockback, bool crit)
        {
            MakeSegmentsImmune(npc, projectile.owner);
        }

        public override void OnHitByItem(Player player, Item item, int damage, float knockback, bool crit)
        {
            MakeSegmentsImmune(npc, player.whoAmI);
        }
    }

    [AutoloadBossHead]
    public class AkumaABody : AkumaA
    {
        public override string Texture { get { return "AAMod/NPCs/Bosses/Akuma/AkumaABody"; } }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Akuma Awakened; Blazing Fury Incarnate");
            Main.npcFrameCount[npc.type] = 1;
        }

        public override void SetDefaults()
        {
            base.SetDefaults();
            npc.dontCountMe = true;
        }

        public override bool? DrawHealthBar(byte hbPosition, ref float scale, ref Vector2 position)
        {
            return false;
        }

        public override bool PreNPCLoot()
        {
            return false;
        }

        public override bool PreAI()
        {
            Vector2 chasePosition = Main.npc[(int)npc.ai[1]].Center;
            Vector2 directionVector = chasePosition - npc.Center;
            npc.spriteDirection = ((directionVector.X > 0f) ? 1 : -1);
            if (npc.ai[3] > 0)
                npc.realLife = (int)npc.ai[3];
            if (npc.target < 0 || npc.target == byte.MaxValue || Main.player[npc.target].dead)
                npc.TargetClosest(true);
            if (Main.player[npc.target].dead && npc.timeLeft > 300)
                npc.timeLeft = 300;

            if (Main.netMode != 1)
            {
                if (!Main.npc[(int)npc.ai[1]].active || Main.npc[(int)npc.ai[3]].type != mod.NPCType("AkumaA"))
                {
                    npc.life = 0;
                    npc.HitEffect(0, 10.0);
                    npc.active = false;
                    NetMessage.SendData(28, -1, -1, null, npc.whoAmI, -1f, 0.0f, 0.0f, 0, 0, 0);
                }
            }

            if (npc.ai[1] < (double)Main.npc.Length)
            {
                Vector2 npcCenter = new Vector2(npc.position.X + (float)npc.width * 0.5f, npc.position.Y + (float)npc.height * 0.5f);
                float dirX = Main.npc[(int)npc.ai[1]].position.X + (float)(Main.npc[(int)npc.ai[1]].width / 2) - npcCenter.X;
                float dirY = Main.npc[(int)npc.ai[1]].position.Y + (float)(Main.npc[(int)npc.ai[1]].height / 2) - npcCenter.Y;
                npc.rotation = (float)Math.Atan2(dirY, dirX) + 1.57f;
                float length = (float)Math.Sqrt(dirX * dirX + dirY * dirY);
                float dist = (length - (float)npc.width) / length;
                float posX = dirX * dist;
                float posY = dirY * dist;

                if (dirX < 0f)
                {
                    npc.spriteDirection = 1;

                }
                else
                {
                    npc.spriteDirection = -1;
                }

                npc.velocity = Vector2.Zero;
                npc.position.X = npc.position.X + posX;
                npc.position.Y = npc.position.Y + posY;
            }

            Player player = Main.player[npc.target];
            if (npc.target < 0 || npc.target == 255 || Main.player[npc.target].dead || !Main.player[npc.target].active)
            {
                npc.TargetClosest(true);
            }
            npc.netUpdate = true;
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

        public override void ModifyHitByItem(Player player, Item item, ref int damage, ref float knockback, ref bool crit)
        {
            ModifyCritArea(npc, ref crit);
        }

        public override void ModifyHitByProjectile(Projectile projectile, ref int damage, ref float knockback, ref bool crit, ref int hitDirection)
        {
            ModifyCritArea(npc, ref crit);
        }

        private void ModifyCritArea(NPC npc, ref bool crit)
        {
            if (npc.realLife >= 0)
            {
                if (npc.whoAmI == npc.realLife)
                {
                    crit = true;
                }
                if (npc.ai[0] == 0)
                {
                    crit = false;
                }
            }
        }

        public override void UpdateLifeRegen(ref int damage)
        {
            if (npc.realLife >= 0 && npc.whoAmI != npc.realLife)
            {
                damage = 0;
                npc.lifeRegen = 0;
            }
        }

        public override void OnHitByProjectile(Projectile projectile, int damage, float knockback, bool crit)
        {
            MakeSegmentsImmune(npc, projectile.owner);
        }

        public override void OnHitByItem(Player player, Item item, int damage, float knockback, bool crit)
        {
            MakeSegmentsImmune(npc, player.whoAmI);
        }
    }
    
    [AutoloadBossHead]
    public class AkumaABody1 : AkumaA
    {
        public override string Texture { get { return "AAMod/NPCs/Bosses/Akuma/AkumaABody1"; } }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Akuma Awakened; Blazing Fury Incarnate");
            Main.npcFrameCount[npc.type] = 1;
        }

        public override void SetDefaults()
        {
            base.SetDefaults();
            npc.dontCountMe = true;
        }

        public override bool? DrawHealthBar(byte hbPosition, ref float scale, ref Vector2 position)
        {
            return false;
        }

        public override bool PreNPCLoot()
        {
            return false;
        }

        public override bool PreAI()
        {
            Vector2 chasePosition = Main.npc[(int)npc.ai[1]].Center;
            Vector2 directionVector = chasePosition - npc.Center;
            npc.spriteDirection = ((directionVector.X > 0f) ? 1 : -1);
            if (npc.ai[3] > 0)
                npc.realLife = (int)npc.ai[3];
            if (npc.target < 0 || npc.target == byte.MaxValue || Main.player[npc.target].dead)
                npc.TargetClosest(true);
            if (Main.player[npc.target].dead && npc.timeLeft > 300)
                npc.timeLeft = 300;

            if (Main.netMode != 1)
            {
                if (!Main.npc[(int)npc.ai[1]].active || Main.npc[(int)npc.ai[3]].type != mod.NPCType("AkumaA"))
                {
                    npc.life = 0;
                    npc.HitEffect(0, 10.0);
                    npc.active = false;
                    NetMessage.SendData(28, -1, -1, null, npc.whoAmI, -1f, 0.0f, 0.0f, 0, 0, 0);
                }
            }

            if (npc.ai[1] < (double)Main.npc.Length)
            {
                Vector2 npcCenter = new Vector2(npc.position.X + (float)npc.width * 0.5f, npc.position.Y + (float)npc.height * 0.5f);
                float dirX = Main.npc[(int)npc.ai[1]].position.X + (float)(Main.npc[(int)npc.ai[1]].width / 2) - npcCenter.X;
                float dirY = Main.npc[(int)npc.ai[1]].position.Y + (float)(Main.npc[(int)npc.ai[1]].height / 2) - npcCenter.Y;
                npc.rotation = (float)Math.Atan2(dirY, dirX) + 1.57f;
                float length = (float)Math.Sqrt(dirX * dirX + dirY * dirY);
                float dist = (length - (float)npc.width) / length;
                float posX = dirX * dist;
                float posY = dirY * dist;

                if (dirX < 0f)
                {
                    npc.spriteDirection = 1;

                }
                else
                {
                    npc.spriteDirection = -1;
                }

                npc.velocity = Vector2.Zero;
                npc.position.X = npc.position.X + posX;
                npc.position.Y = npc.position.Y + posY;
            }

            Player player = Main.player[npc.target];
            if (npc.target < 0 || npc.target == 255 || Main.player[npc.target].dead || !Main.player[npc.target].active)
            {
                npc.TargetClosest(true);
            }
            npc.netUpdate = true;
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

        public override void ModifyHitByItem(Player player, Item item, ref int damage, ref float knockback, ref bool crit)
        {
            ModifyCritArea(npc, ref crit);
        }

        public override void ModifyHitByProjectile(Projectile projectile, ref int damage, ref float knockback, ref bool crit, ref int hitDirection)
        {
            ModifyCritArea(npc, ref crit);
        }

        private void ModifyCritArea(NPC npc, ref bool crit)
        {
            if (npc.realLife >= 0)
            {
                if (npc.whoAmI == npc.realLife)
                {
                    crit = true;
                }
                if (npc.ai[0] == 0)
                {
                    crit = false;
                }
            }
        }

        public override void UpdateLifeRegen(ref int damage)
        {
            if (npc.realLife >= 0 && npc.whoAmI != npc.realLife)
            {
                damage = 0;
                npc.lifeRegen = 0;
            }
        }
        
        public override void OnHitByProjectile(Projectile projectile, int damage, float knockback, bool crit)
        {
            MakeSegmentsImmune(npc, projectile.owner);
        }

        public override void OnHitByItem(Player player, Item item, int damage, float knockback, bool crit)
        {
            MakeSegmentsImmune(npc, player.whoAmI);
        }
    }
    
    [AutoloadBossHead]
    public class AkumaATail : AkumaA
    {
        public override string Texture { get { return "AAMod/NPCs/Bosses/Akuma/AkumaATail"; } }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Akuma Awakened; Blazing Fury Incarnate");
            Main.npcFrameCount[npc.type] = 1;
        }

        public override void SetDefaults()
        {
            base.SetDefaults();
            npc.dontCountMe = true;
        }

        public override bool? DrawHealthBar(byte hbPosition, ref float scale, ref Vector2 position)
        {
            return false;
        }

        public override bool PreNPCLoot()
        {
            return false;
        }

        public override bool PreAI()
        {
            Vector2 chasePosition = Main.npc[(int)npc.ai[1]].Center;
            Vector2 directionVector = chasePosition - npc.Center;
            npc.spriteDirection = ((directionVector.X > 0f) ? 1 : -1);
            if (npc.ai[3] > 0)
                npc.realLife = (int)npc.ai[3];
            if (npc.target < 0 || npc.target == byte.MaxValue || Main.player[npc.target].dead)
                npc.TargetClosest(true);
            if (Main.player[npc.target].dead && npc.timeLeft > 300)
                npc.timeLeft = 300;

            if (Main.netMode != 1)
            {
                if (!Main.npc[(int)npc.ai[1]].active || Main.npc[(int)npc.ai[3]].type != mod.NPCType("AkumaA"))
                {
                    npc.life = 0;
                    npc.HitEffect(0, 10.0);
                    npc.active = false;
                    NetMessage.SendData(28, -1, -1, null, npc.whoAmI, -1f, 0.0f, 0.0f, 0, 0, 0);
                }
            }

            if (npc.ai[1] < (double)Main.npc.Length)
            {
                Vector2 npcCenter = new Vector2(npc.position.X + (float)npc.width * 0.5f, npc.position.Y + (float)npc.height * 0.5f);
                float dirX = Main.npc[(int)npc.ai[1]].position.X + (float)(Main.npc[(int)npc.ai[1]].width / 2) - npcCenter.X;
                float dirY = Main.npc[(int)npc.ai[1]].position.Y + (float)(Main.npc[(int)npc.ai[1]].height / 2) - npcCenter.Y;
                npc.rotation = (float)Math.Atan2(dirY, dirX) + 1.57f;
                float length = (float)Math.Sqrt(dirX * dirX + dirY * dirY);
                float dist = (length - (float)npc.width) / length;
                float posX = dirX * dist;
                float posY = dirY * dist;

                if (dirX < 0f)
                {
                    npc.spriteDirection = 1;

                }
                else
                {
                    npc.spriteDirection = -1;
                }

                npc.velocity = Vector2.Zero;
                npc.position.X = npc.position.X + posX;
                npc.position.Y = npc.position.Y + posY;
            }

            Player player = Main.player[npc.target];
            if (npc.target < 0 || npc.target == 255 || Main.player[npc.target].dead || !Main.player[npc.target].active)
            {
                npc.TargetClosest(true);
            }
            npc.netUpdate = true;
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

        public override void ModifyHitByItem(Player player, Item item, ref int damage, ref float knockback, ref bool crit)
        {
            ModifyCritArea(npc, ref crit);
        }

        public override void ModifyHitByProjectile(Projectile projectile, ref int damage, ref float knockback, ref bool crit, ref int hitDirection)
        {
            ModifyCritArea(npc, ref crit);
        }

        private void ModifyCritArea(NPC npc, ref bool crit)
        {
            if (npc.realLife >= 0)
            {
                if (npc.whoAmI == npc.realLife)
                {
                    crit = true;
                }
                if (npc.ai[0] == 0)
                {
                    crit = false;
                }
            }
        }

        public override void UpdateLifeRegen(ref int damage)
        {
            if (npc.realLife >= 0 && npc.whoAmI != npc.realLife)
            {
                damage = 0;
                npc.lifeRegen = 0;
            }
        }

        public override void OnHitByProjectile(Projectile projectile, int damage, float knockback, bool crit)
        {
            MakeSegmentsImmune(npc, projectile.owner);
        }

        public override void OnHitByItem(Player player, Item item, int damage, float knockback, bool crit)
        {
            MakeSegmentsImmune(npc, player.whoAmI);
        }
    }
}
