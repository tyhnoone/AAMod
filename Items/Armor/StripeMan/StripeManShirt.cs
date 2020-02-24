using Terraria;
using Terraria.ID;
using Terraria.ModLoader;


namespace AAMod.Items.Armor.StripeMan
{
    [AutoloadEquip(EquipType.Body)]
	public class StripeManShirt : BaseAAItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Stripeman's Lucky Shirt");
            Tooltip.SetDefault(@"Displays everything
You have chance to get gold coins in stoneblocks
You have more chance to meet with rare creatures.
You have more chance to get better things in pots
If you have enough money, you can resist an attack by losting all your money.
Have the effect of Arctic Diving Gear");
        }

		public override void SetDefaults()
		{
			item.width = 30;
			item.height = 20;
			item.value = 50;
			item.rare = -1;
			item.defense = 1;
            item.value = Item.sellPrice(0, 0, 0, 1);
        }

		public override void UpdateEquip(Player player)
        {
            player.GetModPlayer<AAPlayer>().StripeManSpawn = true;
			player.GetModPlayer<AAPlayer>().AncientGoldBody = true;
			player.GetModPlayer<AAPlayer>().AncientGoldSet = true;

			player.accWatch = 3;
			player.accDepthMeter = 1;
			player.accCompass = 1;
			player.accFishFinder = true;
			player.accWeatherRadio = true;
			player.accCalendar = true;
			player.accThirdEye = true;
			player.accJarOfSouls = true;
			player.accCritterGuide = true;
			player.accStopwatch = true;
			player.accOreFinder = true;
			player.accDreamCatcher = true;

			player.arcticDivingGear = true;
			player.accFlipper = true;
			player.accDivingHelm = true;
			player.iceSkate = true;
			if (player.wet)
			{
				Lighting.AddLight((int)player.Center.X / 16, (int)player.Center.Y / 16, 0.2f, 0.8f, 0.9f);
			}
        }

		public override void UpdateInventory(Player player)
		{
			player.accWatch = 3;
			player.accDepthMeter = 1;
			player.accCompass = 1;
			player.accFishFinder = true;
			player.accWeatherRadio = true;
			player.accCalendar = true;
			player.accThirdEye = true;
			player.accJarOfSouls = true;
			player.accCritterGuide = true;
			player.accStopwatch = true;
			player.accOreFinder = true;
			player.accDreamCatcher = true;
		}

		public override void UpdateVanity(Player player, EquipType type)
		{
			player.accWatch = 3;
			player.accDepthMeter = 1;
			player.accCompass = 1;
			player.accFishFinder = true;
			player.accWeatherRadio = true;
			player.accCalendar = true;
			player.accThirdEye = true;
			player.accJarOfSouls = true;
			player.accCritterGuide = true;
			player.accStopwatch = true;
			player.accOreFinder = true;
			player.accDreamCatcher = true;
		}

		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.AncientGoldHelmet, 1);
			recipe.AddIngredient(null, "AncientGoldBody", 1);
			recipe.AddIngredient(null, "AncientGoldLeg", 1);
			recipe.AddIngredient(ItemID.ArcticDivingGear, 1);
			recipe.AddIngredient(ItemID.PDA, 1);
			recipe.AddIngredient(null, "LuckyCracker", 1);
            recipe.AddTile(TileID.TinkerersWorkbench);
            recipe.SetResult(this);
			recipe.AddRecipe();
		}
	}
}