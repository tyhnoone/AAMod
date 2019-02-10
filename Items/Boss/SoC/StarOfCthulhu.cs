using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Items.Boss.SoC
{
    public class StarOfCthulhu : ModItem
    {
        public override void SetDefaults()
        {
            item.CloneDefaults(ItemID.LightDisc);
			item.melee = false;
			item.thrown = true;
            item.damage = 150;                            
            item.value = 5000000;
            item.rare = 8;
            item.knockBack = 5;
            item.useStyle = 1;
            item.useAnimation = 20;
            item.useTime = 20;
            item.shoot = mod.ProjectileType("StarOfCthulhuP");
			item.shootSpeed = 18f;
			item.width = 90;
            item.height = 94;
        }

		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Star of Cthulhu");
			Tooltip.SetDefault("Shoots stings while close to enemies");
		}
		
		public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
		{
			position.X -= 10;
			return true;
		}

        public override void AddRecipes()
        {                                                   //How to craft this item
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(mod.ItemType("RealityBar"), 5);              //exeample of how to craft with a modded item
			recipe.AddIngredient(ItemID.LightDisc, 5);
			recipe.AddTile(mod.TileType("ACS"));
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
