using Terraria.ID;

namespace AAMod.Items.Mushrooms
{
    public class Purple : BaseAAItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Purple Alchemical Mushroom");
            Tooltip.SetDefault(@"It smells weird");
        }
        public override void SetDefaults()
        {
            item.width = 16;
            item.height = 16;
            item.maxStack = 99;
            item.rare = ItemRarityID.Green;
        }
    }
}