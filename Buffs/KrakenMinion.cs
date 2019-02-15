using Terraria;
using Terraria.ModLoader;

namespace AAMod.Buffs
{
    public class KrakenMinion: ModBuff
	{
		public override void SetDefaults()
		{
			DisplayName.SetDefault("Lovecraftian Kraken");
			Description.SetDefault("He's been released");
			Main.buffNoSave[Type] = true;
			Main.buffNoTimeDisplay[Type] = true;
		}

		public override void Update(Player player, ref int buffIndex)
		{
			AAPlayer modPlayer = player.GetModPlayer<AAPlayer>(mod);
			if (player.ownedProjectileCounts[mod.ProjectileType("KrakenMinion")] > 0)
			{
				modPlayer.KrakenMinion = true;
			}
			if (!modPlayer.KrakenMinion)
			{
				player.DelBuff(buffIndex);
				buffIndex--;
			}
			else
			{
				player.buffTime[buffIndex] = 18000;
			}
		}
	}
}