using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Graphics;

namespace AAMod.Items.Armor.RiftLord
{
    [AutoloadEquip(EquipType.Head)]
	public class RiftLordMask : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Rift Lord's Mask");
			Tooltip.SetDefault(@"45% increased minion damage
9% increased damage resistance
+3 minion slots
The energy of a broken reality infuses this armor");

		}

		public override void SetDefaults()
		{
			item.width = 24;
			item.height = 22;
            item.value = Item.sellPrice(3, 0, 0, 0);
            item.defense = 30;
		}
		
		public override void UpdateEquip(Player player)
		{
            player.minionDamage *= 1.35f;
            player.endurance *= 1.08f;
            player.ammoCost75 = true;
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

        public override bool IsArmorSet(Item head, Item body, Item legs)
		{
			return body.type == mod.ItemType("RiftLordPlate") && legs.type == mod.ItemType("RiftLordGreaves");
		}

		public override void UpdateArmorSet(Player player)
		{
			
			player.setBonus = @"'The power to split reality in two courses through your veins'
A lovecrafian Kraken fights at your side
You can see all potential threats around you
Your attacks leave the spacial stability of your enemies broken and shredded";
            
            //player.AddBuff(BuffID.Hunter, 2);
            //player.AddBuff(BuffID.Dangersense, 2);
            player.GetModPlayer<AAPlayer>(mod).infinitySet = true;
		}

		public override void AddRecipes()
		{
            return;
			ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(null, "DoomsdayHelmet", 1);
            recipe.AddIngredient(null, "Infinitium", 12);
            recipe.AddTile(null, "ACS");
            recipe.SetResult(this);
			recipe.AddRecipe();
		}

        /*public override void PostDrawInWorld(SpriteBatch spriteBatch, Color lightColor, Color alphaColor, float rotation, float scale, int whoAmI)
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
        }*/
    }
}