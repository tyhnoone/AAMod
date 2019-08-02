using Terraria;
using Terraria.ID;
using Terraria.ModLoader;


namespace AAMod.Items.Armor.Chaos
{
    [AutoloadEquip(EquipType.Head)]
	public class ChaosKabuto : BaseAAItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Chaos Kabuto");
			Tooltip.SetDefault(@"10% increased melee damage");
        }

		public override void SetDefaults()
		{
			item.width = 24;
			item.height = 20;
			item.value = 100000;
			item.rare = 4;
			item.defense = 26;
		}

        public override void UpdateEquip(Player player)
        {
            player.meleeDamage += .1f;
        }

        public override bool IsArmorSet(Item head, Item body, Item legs)
        {
            return body.type == mod.ItemType("ChaosDou") && legs.type == mod.ItemType("ChaosGreaves");
        }

        public override void UpdateArmorSet(Player player)
		{
			player.setBonus = @"10% increased melee speed
Enemies are more likely to target you
Enemies that strike you are set ablaze
Your Swung weapons inflicts them with Bogtoxin and Dragonflame";
            player.meleeSpeed += .1f;
            player.aggro += 4;
            player.GetModPlayer<AAPlayer>(mod).kindledSet = true;
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(mod.ItemType("TrueBlazingKabuto"));
            recipe.AddIngredient(mod.ItemType("ChaosCrystal"));
            recipe.AddTile(null, "TruePaladinsSmeltery");
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
	}
}