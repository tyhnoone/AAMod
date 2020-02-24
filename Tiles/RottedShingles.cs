using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace AAMod.Tiles
{
    public class RottedShingles : ModTile
    {
        public override void SetDefaults()
        {
            Main.tileSolid[Type] = true;
			Main.tileMergeDirt[Type] = false;
            Main.tileBlockLight[Type] = true;
            drop = mod.ItemType("RottedShingles");   
            AddMapEntry(new Color(0, 0, 50));
			minPick = 0;
        }
    }
}