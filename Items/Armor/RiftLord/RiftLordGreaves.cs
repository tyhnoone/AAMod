using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using Terraria;
using Terraria.ModLoader;


namespace AAMod.Items.Armor.RiftLord
{
    [AutoloadEquip(EquipType.Legs)]
	public class RiftLordGreaves : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Rift Lord's Greaves");
			Tooltip.SetDefault(@"45% increased minion damage
9% increased damage resistance
+3 minion slots
70% increased movement speed
The energy of a broken reality infuses this armor");
		}

		public override void SetDefaults()
		{
			item.width = 22;
			item.height = 16;
            item.value = Item.sellPrice(3, 0, 0, 0);
            item.defense = 21;
		}

		public override void UpdateEquip(Player player)
		{
            player.minionDamage *= 1.45f;
            player.maxMinions += 3;
			player.endurance *= 1.09f;
            player.moveSpeed *= 1.6f;
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
            recipe.AddIngredient(null, "DoomsdayLeggings", 1);
            recipe.AddIngredient(null, "Infinitium", 14);
            recipe.AddTile(null, "AncientForge");
            recipe.SetResult(this);
            recipe.AddRecipe();
        }

        
    }
}