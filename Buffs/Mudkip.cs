using Terraria;
using Terraria.ModLoader;

namespace AAMod.Buffs
{
    public class Mudkip : ModBuff
	{
		public override void SetDefaults()
		{
			// DisplayName and Description are automatically set from the .lang files, but below is how it is done normally.
			DisplayName.SetDefault("Mudkip");
			Description.SetDefault("So I heard you like mudkips");
			Main.buffNoTimeDisplay[Type] = true;
			Main.vanityPet[Type] = true;
		}

		public override void Update(Player player, ref int buffIndex)
		{
			player.buffTime[buffIndex] = 18000;
            player.GetModPlayer<AAPlayer>().Mudkip = true;
			bool petProjectileNotSpawned = player.ownedProjectileCounts[mod.ProjectileType("Mudkip")] <= 0;
			if (petProjectileNotSpawned && player.whoAmI == Main.myPlayer)
			{
				Projectile.NewProjectile(player.position.X + (player.width / 2), player.position.Y + player.height / 2, 0f, 0f, mod.ProjectileType("Mudkip"), 0, 0f, player.whoAmI, 0f, 0f);
			}
            if (!player.GetModPlayer<AAPlayer>().Alpha || !player.GetModPlayer<AAPlayer>().Mudkip)
            {
                player.DelBuff(buffIndex);
                buffIndex--;
            }
        }
	}
}