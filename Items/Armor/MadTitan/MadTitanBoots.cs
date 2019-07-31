using Terraria;
using Terraria.ModLoader;


namespace AAMod.Items.Armor.MadTitan
{
    [AutoloadEquip(EquipType.Legs)]
	public class MadTitanBoots : BaseAAItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Mad Titan's Greaves");
			Tooltip.SetDefault(@"40% increased movement speed
20% Increased Melee Speed");

		}

		public override void SetDefaults()
		{
			item.width = 22;
			item.height = 18;
			item.value = 3000000;
            item.rare = 9;
            AARarity = 14;
            item.defense = 40;
		}

        public override void ModifyTooltips(System.Collections.Generic.List<TooltipLine> list)
        {
            foreach (TooltipLine line2 in list)
            {
                if (line2.mod == "Terraria" && line2.Name == "ItemName")
                {
                    line2.overrideColor = AAColor.Rarity14;
                }
            }
        }

        public override void UpdateEquip(Player player)
		{
			player.moveSpeed += 0.4f;
            player.meleeSpeed += 0.2f;
		}

		public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(null, "DarkmatterGreaves", 1);
            recipe.AddIngredient(null, "RadiumCuisses", 1);
            recipe.AddIngredient(null, "UnstableSingularity", 20);
            recipe.AddTile(null, "AncientForge");
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
	}
}