using Terraria;
using Terraria.ModLoader;

namespace AAMod.Buffs
{
    public class SockPuppet : ModBuff
	{
		public override void SetDefaults()
		{
			DisplayName.SetDefault("Sock Puppet");
			Description.SetDefault("Shoots your enemies with Holy Laser");
			Main.buffNoSave[Type] = true;
			Main.buffNoTimeDisplay[Type] = true;
		}

		public override void Update(Player player, ref int buffIndex)
		{
			AAPlayer modPlayer = player.GetModPlayer<AAPlayer>(mod);
			if (player.ownedProjectileCounts[mod.ProjectileType("SockPuppet")] > 0)
			{
				modPlayer.SockPuppet = true;
			}
			if (!modPlayer.SockPuppet)
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