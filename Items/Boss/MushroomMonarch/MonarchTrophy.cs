namespace AAMod.Items.Boss.MushroomMonarch
{
    public class MonarchTrophy : BaseAAItem
	{
        public static int type;
        
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Mushroom Monarch Trophy");
		}

        public override void SetDefaults()
		{
			item.width = 32;
			item.height = 32;
			item.maxStack = 99;
			item.useTurn = true;
			item.autoReuse = true;
			item.useAnimation = 15;
			item.useTime = 10;
            item.rare = 1;
            item.useStyle = 1;
			item.consumable = true;
			item.value = 2000;
            item.rare = 2;
            item.expert = true; item.expertOnly = true;
			item.createTile = mod.TileType("MonarchTrophy");
		}
	}
}