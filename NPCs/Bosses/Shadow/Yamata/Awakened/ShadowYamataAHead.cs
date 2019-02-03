using Terraria.ModLoader;

namespace AAMod.NPCs.Bosses.Shadow.Yamata.Awakened
{
    [AutoloadBossHead]
    public class ShadowYamataAHead : ShadowYamataHead
    {
        public override void SetStaticDefaults()
        {
			base.SetStaticDefaults();
            DisplayName.SetDefault("Shadow of Yamata Awakened");
        }

        public override void SetDefaults()
        {
            base.SetDefaults();
			isAwakened = true;
            for (int k = 0; k < npc.buffImmune.Length; k++)
            {
                npc.buffImmune[k] = true;
            }
        }
    }
}
