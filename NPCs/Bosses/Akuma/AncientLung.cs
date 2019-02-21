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
    public class AncientLung : ModNPC
	{
        
        public override string Texture { get { return "AAMod/NPCs/Bosses/Akuma/AncientLung"; } }

        public bool loludided;
        private bool weakness;

        public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Ancient Lung");
            Main.npcFrameCount[npc.type] = 3;

        }

        public override void SetDefaults()
		{
			npc.noTileCollide = true;
			npc.height = 24;
			npc.width = 24;
			npc.aiStyle = -1;
			npc.netAlways = true;
			npc.knockBackResist = 0f;
            npc.damage = 60;
            npc.defense = 10;
            npc.lifeMax = 5000;
            npc.value = Item.buyPrice(0, 0, 0, 0);
            npc.knockBackResist = 0f;
            npc.aiStyle = -1;
            npc.lavaImmune = true;
            npc.noGravity = true;
            npc.noTileCollide = true;
            npc.DeathSound = new LegacySoundStyle(2, 124, Terraria.Audio.SoundType.Sound);
            for (int k = 0; k < npc.buffImmune.Length; k++)
            {
                npc.buffImmune[k] = true;
            }
            npc.buffImmune[103] = false;
            npc.alpha = 255;
        }
        private bool fireAttack;
        private int attackFrame;
        private int attackCounter; 
        private int attackTimer;
        private int speed = 8;
        
        public override bool PreAI()
        {
            if (fireAttack == true && npc.type == mod.NPCType<AncientLung>())
            {
                attackCounter++;
                if (attackCounter > 10)
                {
                    attackFrame++;
                    attackCounter = 0;
                }
                if (attackFrame >= 2)
                {
                    attackFrame = 1;
                }
            }
            speed = 6;
            Main.dayTime = true;
            Main.time = 24000;
            Player player = Main.player[npc.target];
            float dist = npc.Distance(player.Center);

            BaseAI.AIWorm(npc, new int[] { mod.NPCType<AncientLung>(),mod.NPCType<AncientLungBody1>(), mod.NPCType<AncientLungTail>() }, 6, 8f, speed, 0, true, false, true, false, false);
            
            if (dist > 300 & Main.rand.Next(20) == 1 && fireAttack == false)
            {
                fireAttack = true;
            }
            if (fireAttack == true)
            {
                attackTimer++;
                if (attackTimer == 20 || attackTimer == 40 || attackTimer == 60)
                {
                    AAAI.BreatheFire(npc, true, mod.ProjectileType<Flameburst>(), 2, 2);
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

        public override bool PreDraw(SpriteBatch spriteBatch, Color drawColor)
        {
            Texture2D texture = Main.npcTexture[npc.type];
            Texture2D attackAni = mod.GetTexture("NPCs/Bosses/Akuma/AncientLung");
            var effects = npc.spriteDirection == -1 ? SpriteEffects.None : SpriteEffects.FlipHorizontally;
            if (fireAttack == false)
            {
                spriteBatch.Draw(texture, npc.Center - Main.screenPosition, npc.frame, drawColor, npc.rotation, npc.frame.Size() / 2, npc.scale, npc.spriteDirection == -1 ? SpriteEffects.None : SpriteEffects.FlipHorizontally, 0f);
            }
            if (fireAttack == true)
            {
                Vector2 drawCenter = new Vector2(npc.Center.X, npc.Center.Y);
                int num214 = attackAni.Height / 2;
                int y6 = num214 * attackFrame;
                Main.spriteBatch.Draw(attackAni, drawCenter - Main.screenPosition, new Microsoft.Xna.Framework.Rectangle?(new Microsoft.Xna.Framework.Rectangle(0, y6, attackAni.Width, num214)), drawColor, npc.rotation, new Vector2((float)attackAni.Width / 2f, (float)num214 / 2f), npc.scale, npc.spriteDirection == -1 ? SpriteEffects.None : SpriteEffects.FlipHorizontally, 0f);
            }
            return false;
        }

        public override void NPCLoot()
        {
            Akuma.MinionCount -= 1;
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
    
    public class AncientLungBody1 : AncientLung
    {
        public override string Texture { get { return "AAMod/NPCs/Bosses/Akuma/AncientLungBody1"; } }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Ancient Lung");
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
    
    public class AncientLungBody : AncientLung
    {
        public override string Texture { get { return "AAMod/NPCs/Bosses/Akuma/AncientLungBody"; } }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Ancient Lung");
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
    
    public class AncientLungTail : AncientLung
    {
        public override string Texture { get { return "AAMod/NPCs/Bosses/Akuma/AncientLungTail"; } }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Ancient Lung");
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
