using Terraria;
using Terraria.ModLoader;

namespace AAMod.Buffs
{
    public class HallowedPrism : ModBuff
	{
		public override void SetDefaults()
		{
			DisplayName.SetDefault("Hallow Prism");
			Description.SetDefault("Summons a prism to fight for you");
			Main.buffNoSave[Type] = true;
			Main.buffNoTimeDisplay[Type] = true;
		}

		public override void Update(Player player, ref int buffIndex)
		{
			AAPlayer modPlayer = player.GetModPlayer<AAPlayer>();
			if (player.ownedProjectileCounts[mod.ProjectileType("HallowedPrism")] > 0)
			{
				modPlayer.HallowedPrism = true;
			}
			if (!modPlayer.HallowedPrism)
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