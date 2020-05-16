using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Items.Blocks.Terra
{
    public class TerraTable : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Terra Table");
        }

        public override void SetDefaults()
        {
            item.width = 38;
            item.height = 26;
            item.maxStack = 99;
            item.useTurn = true;
            item.autoReuse = true;
            item.useAnimation = 15;
            item.useTime = 10;
            item.useStyle = ItemUseStyleID.SwingThrow;
            item.consumable = true;
            item.value = 250;
            item.createTile = mod.TileType("TerraTable");
        }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(mod.ItemType("TerraShard"), 8);
            recipe.AddIngredient(ItemID.HallowedBar, 4);
            recipe.AddTile(TileID.WorkBenches);
            recipe.SetResult(this);
            recipe.AddRecipe();

        }

    }
}