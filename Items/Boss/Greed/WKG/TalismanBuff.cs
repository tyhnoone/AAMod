using Terraria;
using Terraria.ModLoader;

namespace AAMod.Items.Boss.Greed.WKG
{
    public class TalismanBuff : ModBuff
	{
		public override void SetDefaults()
		{
			DisplayName.SetDefault("Ruthless Desire");
			Description.SetDefault("MONEY MONEY MONEY!!!");
		}

		public override void Update(Player player, ref int buffIndex)
        {
            base.Update(player, ref buffIndex);
            player.GetModPlayer<AAPlayer>().Greed2 = true;
            player.allDamage += player.GetModPlayer<AAPlayer>().GreedyDamage / 100f;
        }
	}
}