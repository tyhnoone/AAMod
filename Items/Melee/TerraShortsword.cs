using Terraria;
using Terraria.ID;
using Microsoft.Xna.Framework; using Microsoft.Xna.Framework.Graphics; using Terraria.ModLoader;

namespace AAMod.Items.Melee
{
    public class TerraShortsword : ModItem
    {
        public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Terra Shortsword");
			Tooltip.SetDefault("Who may thought that shortswords may be any good?");
        }
		public override void SetDefaults()
		{
            
			item.damage = 36;
			item.melee = true;
			item.width = 40;
			item.height = 46;
			item.useTime = 15;
			item.useAnimation = 15;
			item.useStyle = 3;
			item.knockBack = 4;
			item.value = 100000;
			item.rare = 4;
			item.UseSound = SoundID.Item1;
			item.autoReuse = true;
        }

        public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(ItemID.GoldBar, 10);
			recipe.AddIngredient(ItemID.Stinger, 8);
			recipe.AddIngredient(ItemID.JungleSpores, 6);
			recipe.AddIngredient(ItemID.CrimtaneBar, 10);
			recipe.AddIngredient(ItemID.HellstoneBar, 10);
			recipe.AddTile(TileID.DemonAltar);
			recipe.SetResult(this);
			recipe.AddRecipe();
		}
	}
}
