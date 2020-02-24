using Terraria;
using Terraria.ModLoader;

namespace AAMod.Buffs
{
    public class Forsaken : ModBuff
	{
		public override void SetDefaults()
		{
			DisplayName.SetDefault("Forsaken");
			Description.SetDefault("You are forsaken");
			Main.debuff[Type] = true;
		}
    }
}
