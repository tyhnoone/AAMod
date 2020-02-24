using Terraria;
using Terraria.ModLoader;

namespace AAMod.Buffs
{
    public class SkullMinion : ModBuff
	{
		public override void SetDefaults()
		{
			DisplayName.SetDefault("Skull Minion");
			Description.SetDefault("Summons a dungeon skull to fight for you");
			Main.buffNoSave[Type] = true;
			Main.buffNoTimeDisplay[Type] = true;
		}

		public override void Update(Player player, ref int buffIndex)
		{
			AAPlayer modPlayer = player.GetModPlayer<AAPlayer>();
			if (player.ownedProjectileCounts[mod.ProjectileType("SkullMinion")] > 0)
			{
				modPlayer.SkullMinion = true;
			}
			if (!modPlayer.SkullMinion)
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