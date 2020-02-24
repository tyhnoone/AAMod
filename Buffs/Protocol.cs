using Terraria;
using Terraria.ModLoader;

namespace AAMod.Buffs
{
    public class Protocol : ModBuff
	{
		public override void SetDefaults()
		{
			DisplayName.SetDefault("D00M PR0T0C0L");
			Description.SetDefault("Summons a baby ZER0 PR0T0C0L to fight for you");
			Main.buffNoSave[Type] = true;
			Main.buffNoTimeDisplay[Type] = true;
		}

		public override void Update(Player player, ref int buffIndex)
		{
			AAPlayer modPlayer = player.GetModPlayer<AAPlayer>();
			if (player.ownedProjectileCounts[mod.ProjectileType("Protocol")] > 0)
			{
				modPlayer.Protocol = true;
			}
			if (!modPlayer.Protocol)
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