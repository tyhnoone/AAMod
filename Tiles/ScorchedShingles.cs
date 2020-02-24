using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace AAMod.Tiles
{
    public class ScorchedShingles : ModTile
    {
        public override void SetDefaults()
        {
            Main.tileSolid[Type] = true;
			Main.tileMergeDirt[Type] = false;
            Main.tileBlockLight[Type] = true;
            drop = mod.ItemType("ScorchedShingles");   
            AddMapEntry(new Color(153, 50, 0));
			minPick = 0;
        }
    }
}