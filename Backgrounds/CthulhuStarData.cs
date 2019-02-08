using Terraria;
using Terraria.Graphics.Shaders;
using BaseMod;
using Terraria.ModLoader;

namespace AAMod.Backgrounds
{
    public class CthulhuStarData : ScreenShaderData
    {
        private int CIndex;

        public CthulhuStarData(string passName) : base(passName)
        {
        }

        private void UpdateCthulhuStars()
        {

            AAPlayer modPlayer = Main.player[Main.myPlayer].GetModPlayer<AAPlayer>();
            int CType = ModLoader.GetMod("AAMod").NPCType("Cthulhu");


            if (CIndex >= 0 && Main.npc[CIndex].active && Main.npc[CIndex].type == CType)
            {
                return;
            }
            CIndex = -1;
            for (int i = 0; i < Main.npc.Length; i++)
            {
                if (Main.npc[i].active && Main.npc[i].type == CType)
                {
                    CIndex = i;
                    break;
                }
            }
        }

        public override void Apply()
        {
            UpdateCthulhuStars();
            if (CIndex != -1)
            {
                UseTargetPosition(Main.npc[CIndex].Center);
            }
            base.Apply();
        }
    }
}