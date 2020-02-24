using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace AAMod.Tiles
{
    public class ScorchedShinglesS : ModTile
    {
        public override void SetDefaults()
        {
            Main.tileSolid[Type] = true;
			Main.tileMergeDirt[Type] = false;
            Main.tileBlockLight[Type] = true;
            drop = mod.ItemType("ScorchedShingles");   
            AddMapEntry(new Color(153, 50, 0));
        }
        public override bool CanKillTile(int i, int j, ref bool blockDamaged)
        {
            if (AAWorld.downedShen)
            {
                return true;
            }
            return false;
        }

        public override bool CanExplode(int i, int j)
        {
            return false;
        }
    }
}