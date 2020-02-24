using Terraria;
using Terraria.ModLoader;

namespace AAMod.Items.DevTools.Cinematic
{
	public class CinematicBuff : ModBuff
	{
		public override void SetDefaults()
		{
			DisplayName.SetDefault("Purity Shield");
			Description.SetDefault("The Spirit of Purity lends you power");
			Main.buffNoTimeDisplay[Type] = true;
			Main.buffNoSave[Type] = true;
		}

		public override void Update(Player player, ref int buffIndex)
		{
			player.mount.SetMount(mod.MountType("CinematicThing"), player);
			player.buffTime[buffIndex] = 10;
			CinematicPlayer modPlayer = player.GetModPlayer<CinematicPlayer>();
			modPlayer.CinematicM = true;
		}
	}

	public class CinematicPlayer : ModPlayer
	{
		public bool CinematicM = false;

		public override void ResetEffects()
		{
			CinematicM = false;
		}
	}
}
