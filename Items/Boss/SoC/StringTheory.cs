using Terraria;
using Terraria.ID;
using Microsoft.Xna.Framework;
using Terraria.ModLoader;

namespace AAMod.Items.Boss.SoC
{
    public class StringTheory : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("String Theory");
			Tooltip.SetDefault("Shoots 2 waves of homing Cthulhu arrows"
			+"\n50% chance not to consume arrow");
        }

        public override void SetDefaults()
        {
            item.useStyle = 5;
            item.useAnimation = 30;
            item.useTime = 15;
            item.width = 12;
            item.height = 28;
            item.shoot = 1;
            item.useAmmo = AmmoID.Arrow;
            item.UseSound = SoundID.Item5;
            item.damage = 200;
            item.shootSpeed = 12f;
            item.knockBack = 4f;
            item.rare = 8;
            item.noMelee = true;
            item.value = 100000;
            item.ranged = true;
			item.autoReuse = true;
        }
		
		public override bool ConsumeAmmo(Player player)
		{
			return Main.rand.NextFloat() >= .50;
		}
		
		public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
		{
			type = mod.ProjectileType("CthulhuArrow");
			float numberProjectiles = 4;
			float rotation = MathHelper.ToRadians(10);
			position += Vector2.Normalize(new Vector2(speedX, speedY)) * 45f;
			for (int i = 0; i < numberProjectiles; i++)
			{
				Vector2 perturbedSpeed = new Vector2(speedX*5, speedY*5).RotatedBy(MathHelper.Lerp(-rotation, rotation, i / (numberProjectiles - 1))) * .2f;
				Projectile.NewProjectile(position.X, position.Y, perturbedSpeed.X, perturbedSpeed.Y, type, damage, knockBack, player.whoAmI);
			}
			return false;
		}
		
		public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(mod.ItemType("RealityBar"), 5);
            recipe.AddIngredient(ItemID.Tsunami);
            recipe.AddTile(mod.TileType("ACS"));
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}