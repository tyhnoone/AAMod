using Terraria;
using Terraria.ModLoader;

namespace AAMod.Buffs
{
    public class Fishnado : ModBuff
	{
		public override void SetDefaults()
		{
			DisplayName.SetDefault("Fishnado");
			Description.SetDefault("Summons a fishnado to fight for you");
			Main.buffNoSave[Type] = true;
			Main.buffNoTimeDisplay[Type] = true;
		}

		public override void Update(Player player, ref int buffIndex)
		{
			AAPlayer modPlayer = player.GetModPlayer<AAPlayer>();
			if (player.ownedProjectileCounts[mod.ProjectileType("Fishnado")] > 0)
			{
				modPlayer.Fishnado = true;
			}
			if (!modPlayer.Fishnado)
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