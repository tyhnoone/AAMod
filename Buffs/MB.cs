using Terraria;
using Terraria.ModLoader;

namespace AAMod.Buffs
{
    public class MB : ModBuff
    {
        public override void SetDefaults()
        {
            DisplayName.SetDefault("Lunar Wasp");
            Description.SetDefault("plz don't sue Erilipah");
            Main.buffNoSave[Type] = true;
            Main.buffNoTimeDisplay[Type] = true;
        }

        public override void Update(Player player, ref int buffIndex)
        {
            AAPlayer modPlayer = player.GetModPlayer<AAPlayer>();
            if (player.ownedProjectileCounts[mod.ProjectileType("MoonBeeMinion")] > 0)
            {
                modPlayer.MoonBee = true;
            }
            if (!modPlayer.MoonBee)
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