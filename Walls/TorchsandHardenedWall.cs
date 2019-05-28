using Microsoft.Xna.Framework;
using Terraria.ModLoader;

namespace AAMod.Walls
{
    public class TorchsandHardenedWall : ModWall
	{
		public override void SetDefaults()
		{
            dustType = mod.DustType("IncineriteDust");
			AddMapEntry(new Color(25, 12, 10));
            Terraria.ID.WallID.Sets.Conversion.HardenedSand[Type] = true;
        }

        public override void NumDust(int i, int j, bool fail, ref int num)
		{
			num = fail ? 1 : 3;
		}
        
    }
}