using System; using System.Collections.Generic;
using Microsoft.Xna.Framework;
using AAMod;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using BaseMod;
using Terraria.Audio;

namespace AAMod.Items.Boss.SoC
{
	public class SquidSlammer : ModItem
	{
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Squid Slammer");
			Tooltip.SetDefault("Charges power as it is swung to smash enemies"
			+"\nDrops Cthulhu Bombs after swing");
        }
        public override void SetDefaults()
        {
			item.useStyle = 5;
			item.useAnimation = 30;
			item.useTime = 30;
			item.shootSpeed = 24f;
			item.knockBack = 7f;
			item.width = 64;
			item.height = 64;
			item.UseSound = SoundID.DD2_MonkStaffSwing;
			item.shoot = mod.ProjectileType("SquidSlammer");
			item.rare = 8;
			item.value = Item.sellPrice(0, 1, 0, 0);
			item.noMelee = true;
			item.noUseGraphic = true;
			item.channel = true;
			item.autoReuse = true;
			item.melee = true;
			item.damage = 120;
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(null, "RealityBar", 5);
            recipe.AddIngredient(3835);
            recipe.AddTile(null, "ACS");
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}