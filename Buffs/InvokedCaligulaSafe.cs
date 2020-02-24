using Terraria.ModLoader;
using Terraria;
using AAMod.Items.Dev.Invoker;

namespace AAMod.Buffs
{
	public class InvokedCaligulaSafe : ModBuff
	{
		public override void SetDefaults()
		{
			DisplayName.SetDefault("Invoked Caligula");
			Description.SetDefault("Your claw hit the enemy crasily and heal you.");
			Main.pvpBuff[Type] = true;
			Main.debuff[Type] = false;
			Main.buffNoTimeDisplay[Type] = true;
		}

        public override void Update(Player player, ref int buffIndex)
        {
            player.GetModPlayer<InvokerPlayer>().InvokedCaligula = true;
			
			player.buffTime[buffIndex] = 18000;
        }
	}
}