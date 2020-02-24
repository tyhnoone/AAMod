using Microsoft.Xna.Framework;
using Terraria.ID;

namespace AAMod.Items.Boss.Greed
{
    public class GildedGlock : BaseAAItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Gilded Glock");
            Tooltip.SetDefault("Uses Coins as Ammo");
        }
        public override void SetDefaults()
        {
            item.width = 44;
            item.height = 30;
            item.rare = 8;
            item.useStyle = 5;
            item.useAnimation = 28;
            item.useTime = 28;
            item.UseSound = SoundID.Item41;
            item.damage = 70;
            item.knockBack = 7;
            item.ranged = true;
            item.autoReuse = false;
            item.noMelee = true;
            item.shoot = 158;
            item.shootSpeed = 12;
            item.useAmmo = AmmoID.Coin;
        }

        public override Vector2? HoldoutOffset()
        {
            return new Vector2(-2, 0);
        }
    }
}