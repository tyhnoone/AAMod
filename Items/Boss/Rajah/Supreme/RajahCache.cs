using Terraria;

namespace AAMod.Items.Boss.Rajah
{
    public class RajahBag : BaseAAItem
    {
        
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Treasure Bag");
            Tooltip.SetDefault("{$CommonItemTooltip.RightClickToOpen}");
        }

        public override void SetDefaults()
        {
            item.maxStack = 999;
            item.consumable = true;
            item.width = 32;
            item.height = 32;
            item.expert = true;
        }

        public override int BossBagNPC => mod.NPCType("Rajah");

        public override bool CanRightClick()
        {
            return true;
        }

        public override void OpenBossBag(Player player)
        {
            if (Main.rand.Next(7) == 0)
            {
                player.QuickSpawnItem(mod.ItemType("RajahMask"));
            }
            if (Main.rand.Next(20) == 0)
            {
                AAPlayer modPlayer = player.GetModPlayer<AAPlayer>(mod);
                modPlayer.SADevArmor();
            }
            player.QuickSpawnItem(mod.ItemType<RajahPelt>(), Main.rand.Next(15, 31));
            player.QuickSpawnItem(mod.ItemType("RajahSash"));
            string[] lootTable = { "Excalihare", "FluffyFury", "RabbitsWrath" };
            int loot = Main.rand.Next(lootTable.Length);
            player.QuickSpawnItem(mod.ItemType(lootTable[loot]));
        }
    }
}