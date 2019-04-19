using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace AAMod.Water
{
    public class InfernoWaterStyle : ModWaterStyle
    {
        public override bool ChooseWaterStyle()
        {
            return Main.bgStyle == mod.GetSurfaceBgStyleSlot("InfernoSurfaceBgStyle");
        }

        public override int ChooseWaterfallStyle()
        {
            return mod.GetWaterfallStyleSlot("InfernoWaterfallStyle");
        }

        public override int GetSplashDust()
        {
            return mod.DustType("InfernoWaterSplash");
        }

        public override int GetDropletGore()
        {
            return mod.GetGoreSlot("Water/InfernoDroplet");
        }

        public override void LightColorMultiplier(ref float r, ref float g, ref float b)
        {
            r = 1f;
            g = 1f;
            b = 1f;
        }

        public override Color BiomeHairColor()
        {
            return Color.OrangeRed;
        }
    }
}