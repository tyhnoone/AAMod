using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using BaseMod;

namespace AAMod.Walls
{
	public class RottedFence : ModWall
	{

		public override void SetDefaults()
		{
            Main.wallHouse[this.Type] = true;
			drop = mod.ItemType("Rotted Fence");
			AddMapEntry(new Color(39, 34, 8));
		}

        public override void KillWall(int i, int j, ref bool fail)
        {
            fail = true;
        }

        public override bool CanExplode(int i, int j)
        {
            return false;
        }
    }
}