using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.NPCs.Bosses.Sock
{
	public class Sock : ModNPC
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("S O C C");
            Main.npcFrameCount[npc.type] = 11;			
		}

		public override void SetDefaults()
		{
            npc.aiStyle = -1;
            npc.width = 60;
            npc.height = 140;
            npc.noGravity = true;
            npc.noTileCollide = true;
            npc.chaseable = true;
            npc.lavaImmune = true;
            npc.boss = true;
            npc.damage = 120;
            npc.defense = 50;
            npc.lifeMax = 60000;
            npc.knockBackResist = 0f;
            npc.npcSlots = 10f;
            npc.HitSound = SoundID.NPCHit14;
            npc.DeathSound = SoundID.NPCDeath20;
            npc.value = 10000f;
            npc.netAlways = true;
            npc.timeLeft = NPC.activeTime * 30;
        }

        public override void AI()
        {
            float maxDistanceAmt = 4f;
            float maxDistance = 350f;
            float increment = 0.011f;
            float closeIncrement = 0.019f;
            float distanceAmt = 1f;
            npc.TargetClosest(true);
            float distX = Main.player[npc.target].Center.X - npc.Center.X;
            float distY = Main.player[npc.target].Center.Y - npc.Center.Y;
            float dist = (float)Math.Sqrt((double)(distX * distX + distY * distY));
            npc.ai[1] += 1f;
            if (npc.ai[1] > 600f)
            {
                increment *= 14f;
                distanceAmt = 6f;
                if (npc.ai[1] > 650f) { npc.ai[1] = 0f; }
            }
            else
            if (dist < 250f)
            {
                npc.ai[0] += 0.9f;
                if (npc.ai[0] > 0f) { npc.velocity.Y = npc.velocity.Y + closeIncrement; } else { npc.velocity.Y = npc.velocity.Y - closeIncrement; }
                if (npc.ai[0] < -100f || npc.ai[0] > 100f) { npc.velocity.X = npc.velocity.X + closeIncrement; } else { npc.velocity.X = npc.velocity.X - closeIncrement; }
                if (npc.ai[0] > 200f) { npc.ai[0] = -200f; }
            }
            if (dist > maxDistance)
            {
                distanceAmt = maxDistanceAmt + (maxDistanceAmt / 4f);
                increment = 0.3f;
            }
            else if (dist > maxDistance - (maxDistance / 7f))
            {
                distanceAmt = maxDistanceAmt - (maxDistanceAmt / 4f);
                increment = 0.2f;
            }
            else if (dist > maxDistance - (2 * (maxDistance / 7f)))
            {
                distanceAmt = (maxDistanceAmt / 2.66f);
                increment = 0.1f;
            }
            dist = distanceAmt / dist;
            distX *= dist; distY *= dist;
            if (Main.player[npc.target].dead)
            {
                distX = (float)npc.direction * distanceAmt / 2f;
                distY = -distanceAmt / 2f;
            }
            if (npc.velocity.X < distX) { npc.velocity.X = npc.velocity.X + increment; }
            else if (npc.velocity.X > distX) { npc.velocity.X = npc.velocity.X - increment; }
            if (npc.velocity.Y < distY) { npc.velocity.Y = npc.velocity.Y + increment; }
            else if (npc.velocity.Y > distY) { npc.velocity.Y = npc.velocity.Y - increment; }
        }

        public override bool PreNPCLoot()
		{
			return false;
		}

		public override bool? DrawHealthBar(byte hbPosition, ref float scale, ref Vector2 position)
		{
			return false;
		}	
    }
}