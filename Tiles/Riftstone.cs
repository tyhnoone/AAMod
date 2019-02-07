using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ModLoader;

namespace AAMod.Tiles
{
    public class Riftstone : ModTile
    {
        public Texture2D glowTex;
        public bool glow = true;
        public override void SetDefaults()
        {
            Main.tileSolid[Type] = true;
            Main.tileMergeDirt[Type] = false;
            soundType = 21;
            drop = mod.ItemType("Riftstone");
            dustType = mod.DustType("CthulhuDust");
            AddMapEntry(new Color(20, 20, 70));
			minPick = 260;
        }

        public override void ModifyLight(int x, int y, ref float r, ref float g, ref float b)
        {
            if (!glow) return;
            Color color = BaseMod.BaseUtility.ColorMult(AAColor.Cthulhu, 0.7f);
            r = (color.R / 255f); g = (color.G / 255f); b = (color.B / 255f);
        }

        public override void PostDraw(int x, int y, SpriteBatch sb)
        {
            Tile tile = Main.tile[x, y];
            if (glow && (tile != null && tile.active() && tile.type == this.Type))
            {
                if (glowTex == null) glowTex = mod.GetTexture("Glowmasks/Riftstone_Glow2");
                BaseMod.BaseDrawing.DrawTileTexture(sb, glowTex, x, y, true, false, false, null, AAGlobalTile.GetCthulhuColorDim);
            }
        }

        public override bool CanExplode(int i, int j)
        {
            return false;
        }
    }
}