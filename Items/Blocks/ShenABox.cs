using Microsoft.Xna.Framework; using Microsoft.Xna.Framework.Graphics; using Terraria.ModLoader;
using Terraria;
using Terraria.ID;

namespace AAMod.Items.Blocks
{
	public class ShenABox : BaseAAItem
	{
        
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Shen Doragon Awakened Music Box");
            
            Tooltip.SetDefault(@"Plays 'Blaze of Glory' by Charlie Debnam");
        }

		public override void SetDefaults()
		{
			item.useStyle = 1;
			item.useTurn = true;
			item.useAnimation = 15;
			item.useTime = 10;
			item.autoReuse = true;
			item.consumable = true;
			item.createTile = mod.TileType("ShenABox");
            item.width = 72;
			item.height = 36;
			item.rare = 4;
			item.value = 10000;
			item.accessory = true;
        }


        public override void PostDrawInWorld(SpriteBatch spriteBatch, Color lightColor, Color alphaColor, float rotation, float scale, int whoAmI)
        {
            Texture2D texture = mod.GetTexture("Glowmasks/" + GetType().Name + "_Glow");
            spriteBatch.Draw
            (
                texture,
                new Vector2
                (
                    item.position.X - Main.screenPosition.X + item.width * 0.5f,
                    item.position.Y - Main.screenPosition.Y + item.height - texture.Height * 0.5f + 2f
                ),
                new Rectangle(0, 0, texture.Width, texture.Height),
                Color.White,
                rotation,
                texture.Size() * 0.5f,
                scale,
                SpriteEffects.None,
                0f
            );
        }


        public override void AddRecipes()
        {
            if (Main.expertMode == true)
            {
                ModRecipe recipe = new ModRecipe(mod);
                recipe.AddIngredient(ItemID.MusicBox);
                recipe.AddIngredient(null, "ShenBox");
                recipe.AddIngredient(null, "ChaosSoul");
                recipe.AddTile(TileID.Sawmill);
                recipe.SetResult(this);
                recipe.AddRecipe();
            }
        }
    }
}
