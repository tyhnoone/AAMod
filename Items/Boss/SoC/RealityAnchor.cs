using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Microsoft.Xna.Framework.Graphics;
using Terraria.ModLoader;

namespace AAMod.Items.Boss.SoC
{
    public class RealityAnchor : ModItem
    {
        
        public override void SetStaticDefaults()
        {
            
            DisplayName.SetDefault("Reality Anchor");
            Tooltip.SetDefault(@"The further the anchor falls, the larger the explosion when it hits a tile");
        }

        public override void ModifyTooltips(List<TooltipLine> list)
        {
            foreach (TooltipLine line2 in list)
            {
                if (line2.mod == "Terraria" && line2.Name == "ItemName")
                {
                    line2.overrideColor = AAColor.Cthulhu;
                }
            }
        }

        public override void SetDefaults()
        {
            item.width = 46;
            item.height = 48;
            item.value = Item.buyPrice(1, 0, 0, 0); ;
            item.rare = 1;

            item.noMelee = true;
            item.useStyle = 5;
            item.useAnimation = 40;
            item.useTime = 40;
            item.knockBack = 7.5F;
            item.damage = 300;
            item.noUseGraphic = true;
            item.shoot = mod.ProjectileType("Anchor");
            item.shootSpeed = 14f;
            item.UseSound = SoundID.Item1;
            item.melee = true;
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(null, "RealityBar", 5);
            recipe.AddIngredient(ItemID.Anchor, 1);
            recipe.AddTile(null, "ACS");
            recipe.SetResult(this, 1);
            recipe.AddRecipe();
        }
    }
}