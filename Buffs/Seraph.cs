using Terraria;
using Terraria.ModLoader;

namespace AAMod.Buffs
{
    public class Seraph : ModBuff
	{
		public override void SetDefaults()
		{
			DisplayName.SetDefault("Seraph");
			Description.SetDefault("Small but feisty");
			Main.buffNoSave[Type] = true;
			Main.buffNoTimeDisplay[Type] = true;
		}

		public override void Update(Player player, ref int buffIndex)
		{
			AAPlayer modPlayer = player.GetModPlayer<AAPlayer>();
			if (player.ownedProjectileCounts[mod.ProjectileType("Seraph")] > 0 && BaseMod.BasePlayer.HasAccessory(player, ModContent.ItemType<Items.Boss.Athena.SeraphHarp>(), true, false))
			{
				modPlayer.Seraph = true;
			}
            else
            {
                modPlayer.Seraph = false;
            }
			if (!modPlayer.Seraph)
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