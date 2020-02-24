﻿using Microsoft.Xna.Framework.Graphics;
using Terraria.ModLoader;

namespace AAMod.Tiles.Trees
{
    class OroborosTree : ModTree
    {
        private Mod mod => ModLoader.GetMod("AAMod");

        public override int DropWood()
        {
            return mod.ItemType("OroborosWood");
        }

        public override Texture2D GetTexture()
        {
            return mod.GetTexture("Tiles/Trees/OroborosTree");
        }

        public override Texture2D GetBranchTextures(int i, int j, int trunkOffset, ref int frame)
        {
            return mod.GetTexture("Tiles/Trees/OroborosBranches");
        }

        public override Texture2D GetTopTextures(int i, int j, ref int frame, ref int frameWidth, ref int frameHeight, ref int xOffsetLeft, ref int yOffset)
        {
            return mod.GetTexture("Tiles/Trees/OroborosTreeTop");
        }
    }
}
