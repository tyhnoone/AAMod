using Terraria;
using Terraria.ModLoader;

namespace AAMod.NPCs.Bosses.Rajah.Supreme
{
    public class SupremeRajahLeave : ModNPC
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Rajah Rabbit; Champion of the innocent");
            Main.npcFrameCount[npc.type] = 4;
        }
        public override void SetDefaults()
        {
            npc.width = 130;
            npc.height = 220;
            npc.aiStyle = -1;
            npc.damage = 0;
            npc.defense = 90;
            npc.lifeMax = 50000;
            npc.knockBackResist = 0f;
            npc.npcSlots = 1000f;
            npc.dontTakeDamage = true;
            npc.boss = false;
            npc.netAlways = true;
            npc.noTileCollide = true;
        }

        public override void AI()
        {
            npc.velocity.X *= 0.00f;
            npc.velocity.Y -= .1f;
            if (npc.position.Y + npc.velocity.Y <= 0f && Main.netMode != 1) { BaseMod.BaseAI.KillNPC(npc); npc.netUpdate = true; }
        }

        public override void FindFrame(int frameHeight)
        {
            if (++npc.frameCounter > 3)
            {
                npc.frameCounter = 0;
                npc.frame.Y += frameHeight;
            }
            if (npc.frame.Y > frameHeight * 3)
            {
                npc.frame.Y = 0;
            }
        }
    }
}