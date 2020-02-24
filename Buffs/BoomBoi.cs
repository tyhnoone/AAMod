using Terraria;
using Terraria.ModLoader;

namespace AAMod.Buffs
{
    public class BoomBoi : ModBuff
	{
		public override void SetDefaults()
		{
			// DisplayName and Description are automatically set from the .lang files, but below is how it is done normally.
			DisplayName.SetDefault("Boomer");
			Description.SetDefault("Doesn't actually explode...Yet.");
			Main.buffNoTimeDisplay[Type] = true;
			Main.vanityPet[Type] = true;
		}

		public override void Update(Player player, ref int buffIndex)
		{
			player.buffTime[buffIndex] = 1800000;
            player.GetModPlayer<AAPlayer>().BoomBoi = true;
			bool petProjectileNotSpawned = player.ownedProjectileCounts[mod.ProjectileType("BoomBoi")] <= 0;
			if (petProjectileNotSpawned && player.whoAmI == Main.myPlayer)
			{
				Projectile.NewProjectile(player.position.X + player.width / 2, player.position.Y + player.height / 2, 0f, 0f, mod.ProjectileType("BoomBoi"), 0, 0f, player.whoAmI, 0f, 0f);
			}
		}
	}
}