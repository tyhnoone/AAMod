using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace AAMod.Tiles
{
    public class TorchsandHardened : ModTile
    {
        public override void SetDefaults()
        {
            Main.tileSolid[Type] = true;
            Terraria.ID.TileID.Sets.Conversion.HardenedSand[Type] = true;
            Main.tileBlendAll[Type] = true;
            Main.tileBlockLight[Type] = true;
            dustType = mod.DustType("RazewoodDust");
            drop = mod.ItemType("TorchsandHardened");   
            AddMapEntry(new Color(50, 30, 17));
            minPick = 65;
        }
    }
}