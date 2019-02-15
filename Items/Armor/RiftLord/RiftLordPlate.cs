using Terraria;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Graphics;


namespace AAMod.Items.Armor.RiftLord
{
    [AutoloadEquip(EquipType.Body)]
	public class RiftLordPlate : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Rift Lord's Garb");
            Tooltip.SetDefault(@"45% increased minion damage
9% increased damage resistance
+3 minion slots
The energy of a broken reality infuses this armor");
		}

		public override void SetDefaults()
		{
			item.width = 30;
			item.height = 20;
            item.value = Item.sellPrice(3, 0, 0, 0);
            item.defense = 40;
		}

        public override void UpdateEquip(Player player)
        {

            player.minionDamage *= 1.45f;
            player.maxMinions += 3;
            player.endurance *= 1.09f;
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



        public override void AddRecipes()
        {
            return;
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(null, "DoomsdayChestplate", 1);
            recipe.AddIngredient(null, "Infinitium", 15);
            recipe.AddTile(null, "ACS");
            recipe.SetResult(this);
            recipe.AddRecipe();
        }

        
    }
}