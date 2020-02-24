using Terraria;
using Terraria.ID;

namespace AAMod.Items.Boss.Serpent
{
    public class Sickle : BaseAAItem
    {
        public override void SetDefaults()
        {
            item.damage = 23;    
            item.magic = true;
            item.width = 24;
            item.height = 28; 
            item.useTime = 17;  
            item.useAnimation = 17;
            item.useStyle = 5;
            item.noMelee = true; 
            item.knockBack = 1;
            item.value = Item.sellPrice(0, 5, 0, 0);
            item.rare = 3;
            item.mana = 9;
            item.UseSound = SoundID.Item8;
            item.autoReuse = true;
            item.shoot = mod.ProjectileType("IciclePro");
            item.shootSpeed = 9f;
        }

		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Icicle");
			Tooltip.SetDefault("Casts crystals that shatter in pieces.");
		}
    }
}
