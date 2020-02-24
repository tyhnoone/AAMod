using Terraria;
using Terraria.ID;
using Microsoft.Xna.Framework;

namespace AAMod.Items.Boss.Anubis.Forsaken
{
    public class Soulsplitter : BaseAAItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Soulsplitter");
            Tooltip.SetDefault("Shoots out a trio of wall-piercing returning phantom blades on swing");
        }

		public override void SetDefaults()
		{
			item.autoReuse = true;
			item.useStyle = 1;
			item.useAnimation = 20;
			item.useTime = 20;
			item.knockBack = 5f;
			item.width = 24;
			item.height = 28;
			item.damage = 250;
			item.UseSound = SoundID.Item71;
			item.shoot = mod.ProjectileType("Soulsplitter");
			item.shootSpeed = 14f;
			item.value = 10000;
			item.melee = true;
            item.noMelee = true;
            item.noUseGraphic = true;
            item.rare = 9;
            AARarity = 12;
        }

        public override void ModifyTooltips(System.Collections.Generic.List<Terraria.ModLoader.TooltipLine> list)
        {
            foreach (Terraria.ModLoader.TooltipLine line2 in list)
            {
                if (line2.mod == "Terraria" && line2.Name == "ItemName")
                {
                    line2.overrideColor = AAColor.Rarity12;
                }
            }
        }

        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
			float numberProjectiles = 3;
			float rotation = MathHelper.ToRadians(6);
			position += Vector2.Normalize(new Vector2(speedX, speedY)) * 45f;
			for (int i = 0; i < numberProjectiles; i++)
			{
				Vector2 perturbedSpeed = new Vector2(speedX, speedY).RotatedBy(MathHelper.Lerp(-rotation, rotation, i / (numberProjectiles - 1))) * 1f;
				Projectile.NewProjectile(position.X, position.Y, perturbedSpeed.X, perturbedSpeed.Y, type, damage, knockBack, player.whoAmI);
			}
            return false;
        }
	}
}
