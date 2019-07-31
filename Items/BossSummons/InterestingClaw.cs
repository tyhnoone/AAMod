using BaseMod;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using AAMod.NPCs.Bosses.Grips;

namespace AAMod.Items.BossSummons
{
    //imported from my tAPI mod because I'm lazy
    public class InterestingClaw : BaseAAItem
	{
		public override void SetStaticDefaults()
		{
            DisplayName.SetDefault("Interesting Looking Claw");
            Tooltip.SetDefault(@"It's oddly Clammy
Can only be used at night");
		}

		public override void SetDefaults()
		{
			item.width = 32;
			item.height = 24;
			item.maxStack = 20;
			item.rare = 2;
            item.value = Item.sellPrice(0, 0, 0, 0);
            item.useAnimation = 45;
			item.useTime = 45;
			item.useStyle = 4;
			item.UseSound = SoundID.Item44;
			item.consumable = true;
		}

		public override void AddRecipes()
		{
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(null, "HydraClaw", 6);
            recipe.AddTile(TileID.DemonAltar);
            recipe.SetResult(this, 1);
            recipe.AddRecipe();
        }

        public override bool CanUseItem(Player player)
        {
            if (NPC.AnyNPCs(mod.NPCType<GripOfChaosBlue>()) || NPC.AnyNPCs(mod.NPCType<GripOfChaosRed>()))
            {
                if (player.whoAmI == Main.myPlayer) if (Main.netMode != 1) BaseUtility.Chat("The Grips of Chaos are already here!", Color.Indigo, false);
                return false;
            }
            if (Main.dayTime)
            {
                if (player.whoAmI == Main.myPlayer) if (Main.netMode != 1) BaseUtility.Chat("The claw lays limp in your hand. Nasty.", Color.Indigo, false);
                return false;
            }
            return true;
        }

        public override bool UseItem(Player player)
        {
            AAModGlobalNPC.SpawnBoss(player, mod.NPCType("GripOfChaosRed"), true, 0, 0, "The Grips of Chaos", true);
            AAModGlobalNPC.SpawnBoss(player, mod.NPCType("GripOfChaosBlue"), false, 0, 0, "The Grips of Chaos", false);
            Main.PlaySound(15, (int)player.position.X, (int)player.position.Y, 0);
            return true;
        }
    }
}