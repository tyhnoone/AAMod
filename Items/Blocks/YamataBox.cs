using Terraria.ModLoader;
using Terraria.ID;
using System.Collections.Generic;

namespace AAMod.Items.Blocks
{
    public class YamataBox : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Yamata Music Box");
            Tooltip.SetDefault("Plays 'Crescent of Madness' by Saucecoie");
        }


        public override void ModifyTooltips(List<TooltipLine> list)
        {
            foreach (TooltipLine line2 in list)
            {
                if (line2.mod == "Terraria" && line2.Name == "ItemName")
                {
                    line2.overrideColor = AAColor.Yamata;
                }
            }
        }

        public override void SetDefaults()
        {
            item.useStyle = 1;
            item.useTurn = true;
            item.useAnimation = 15;
            item.useTime = 10;
            item.autoReuse = true;
            item.consumable = true;
            item.createTile = mod.TileType("YamataBox");
            item.width = 24;
            item.height = 24;
            item.rare = 4;
            item.value = 10000;
            item.accessory = true;
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.MusicBox);
            recipe.AddIngredient(null, "EventideAbyssium", 5);
            recipe.AddIngredient(null, "DreadScale", 5);
            recipe.AddTile(TileID.Sawmill);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}