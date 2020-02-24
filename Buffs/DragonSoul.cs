using Terraria;
using Terraria.ModLoader;

namespace AAMod.Buffs
{
    public class DragonSoul : ModBuff
	{
		public override void SetDefaults()
		{
			DisplayName.SetDefault("Dragon Soul");
			Description.SetDefault("Burns with the rage of a dragon");
			Main.buffNoTimeDisplay[Type] = true;
			Main.lightPet[Type] = true;
		}

		public override void Update(Player player, ref int buffIndex)
		{
			player.GetModPlayer<AAPlayer>().DragonSoul = true;
			player.buffTime[buffIndex] = 18000;
			bool petProjectileNotSpawned = player.ownedProjectileCounts[mod.ProjectileType("DragonSoul")] <= 0;
			if (petProjectileNotSpawned && player.whoAmI == Main.myPlayer)
			{
				Projectile.NewProjectile(player.position.X + player.width / 2, player.position.Y + player.height / 2, 0f, 0f, mod.ProjectileType("DragonSoul"), 0, 0f, player.whoAmI, 0f, 0f);
			}
		}
	}
}