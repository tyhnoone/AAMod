﻿using Terraria;
using Terraria.ModLoader;

namespace AAMod.Buffs
{
    public class Hunted : ModBuff
	{
		public override void SetDefaults()
		{
			DisplayName.SetDefault("Hunted");
			Description.SetDefault(@"You are being hunted by an abyssal creature
Flight time and run speed reduced");
			Main.debuff[Type] = true;
			Main.pvpBuff[Type] = true;
			Main.buffNoSave[Type] = true;
            Main.buffNoTimeDisplay[Type] = true;
        }
		
		public override void Update(Player player, ref int buffIndex)
		{
			player.GetModPlayer<AAPlayer>().Hunted = true;
		}
	}
}