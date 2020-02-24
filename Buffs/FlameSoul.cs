using Terraria;
using Terraria.ModLoader;

namespace AAMod.Buffs
{
    public class FlameSoul : ModBuff
	{
		public override void SetDefaults()
		{
			DisplayName.SetDefault("Flame Soul");
			Description.SetDefault("The weaker you are, the harder it fights");
			Main.buffNoSave[Type] = true;
			Main.buffNoTimeDisplay[Type] = true;
		}

		public override void Update(Player player, ref int buffIndex)
		{
			AAPlayer modPlayer = player.GetModPlayer<AAPlayer>();
			if (player.ownedProjectileCounts[mod.ProjectileType("FlameSoul")] > 0)
			{
				modPlayer.FlameSoul = true;
			}
			if (!modPlayer.FlameSoul)
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