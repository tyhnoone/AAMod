using Microsoft.Xna.Framework.Graphics;
using Terraria.ModLoader;

namespace AAMod.Tiles.Trees
{
    public class Razetus : ModCactus
	{
        private Mod mod => ModLoader.GetMod("AAMod");

        public override Texture2D GetTexture()
		{
			return mod.GetTexture("Tiles/Trees/Razetus");
		}
    }
}