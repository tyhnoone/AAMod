namespace AAMod.Items.Boss.Akuma
{
    /*public class Daystorm : BaseAAItem //extend BaseAAItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Daystorm");
            Tooltip.SetDefault("Rapidly fires scorching hot lasers");					
        }

        public override void SetDefaults()
        {
            item.useStyle = 5;
            item.useAnimation = 20;
            item.useTime = 20;
            item.shootSpeed = 20f;
            item.knockBack = 2f;
            item.width = 20;
            item.height = 12;
            item.damage = 150;
            item.shoot = mod.ProjectileType("Daystorm");
            item.mana = 6;
            item.rare = 9;
            AARarity = 13;
            item.value = Item.sellPrice(0, 30, 0, 0);
            item.noMelee = true;
            item.noUseGraphic = true;
            item.magic = true;
            item.channel = true;

			glowmaskTexture = "Glowmasks/" + GetType().Name + "_Glow"; //the glowmask texture path.
			glowmaskDrawType = BaseAAItem.GLOWMASKTYPE_NONE; //what type it is when drawn in the hand, _NONE == no draw, _SWORD == like a sword, _GUN == like a gun
			glowmaskDrawColor = Color.White; //glowmask draw color
			customNameColor = AAColor.Akuma; //custom name color	
        }

        public override void ModifyTooltips(System.Collections.Generic.List<Terraria.ModLoader.TooltipLine> list)
        {
            foreach (Terraria.ModLoader.TooltipLine line2 in list)
            {
                if (line2.mod == "Terraria" && line2.Name == "ItemName")
                {
                    line2.overrideColor = AAColor.Rarity13;
                }
            }
        }

        public override void AddRecipes()  //How to craft this sword
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(null, "DaybreakIncinerite", 5);
            recipe.AddIngredient(null, "CrucibleScale", 5);
            recipe.AddIngredient(ItemID.LaserMachinegun);
            recipe.AddTile(null, "ACS");
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }*/
}
