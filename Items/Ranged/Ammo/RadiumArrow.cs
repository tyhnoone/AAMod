using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Items.Ranged.Ammo
{
    public class RadiumArrow : BaseAAItem
	{
		public override void SetStaticDefaults()
		{
            DisplayName.SetDefault("Radium Arrow");
		}

		public override void SetDefaults()
		{
			item.damage = 14;
			item.ranged = true;
			item.width = 14;
			item.height = 32;
			item.maxStack = 999;
			item.consumable = true;
			item.knockBack = 4f;
			item.value = 30;
			item.rare = 11;
			item.shoot = mod.ProjectileType("RadiumArrow");
			item.shootSpeed = 6f; 
			item.ammo = AmmoID.Arrow;
		}

        public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(null, "Stardust", 1);
            recipe.AddIngredient(null, "RadiumBar", 3);
            recipe.AddTile(null, "QuantumFusionAccelerator");
            recipe.SetResult(this, 400);
			recipe.AddRecipe();
		}
	}
}
