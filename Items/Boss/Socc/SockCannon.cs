using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Items.Boss.Socc
{
    public class SockCannon : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Sock Cannon");
			Tooltip.SetDefault("Shoots exploding Sock Rockets");
		}

		public override void SetDefaults()
		{
			item.useStyle = 5;
			item.autoReuse = true;
			item.useAnimation = 10;
			item.useTime = 10;
			item.useAmmo = AmmoID.Rocket;
			item.width = 50;
			item.height = 20;
			item.shoot = mod.ProjectileType("SockRocket");
			item.UseSound = SoundID.Item11;
			item.damage = 100;
			item.shootSpeed = 15f;
			item.noMelee = true;
			item.value = Item.sellPrice(0, 25, 0, 0);
			item.knockBack = 4f;
			item.rare = 8;
			item.ranged = true;
		}
	}
}
