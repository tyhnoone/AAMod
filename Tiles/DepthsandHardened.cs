using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace AAMod.Tiles
{
    public class DepthsandHardened : ModTile
    {
        public override void SetDefaults()
        {
            Main.tileSolid[Type] = true;
            Main.tileBlendAll[Type] = true;
            Main.tileBlockLight[Type] = true;
            Terraria.ID.TileID.Sets.Conversion.HardenedSand[Type] = true;
            Main.tileLighted[Type] = false;
            dustType = mod.DustType("DeepAbyssiumDust");
            drop = mod.ItemType("DepthsandHardened");   
            AddMapEntry(new Color(0, 0, 127));
			minPick = 65;
        }
    }
}