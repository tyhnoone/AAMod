using System;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.Audio;
using Terraria.ModLoader;

namespace AAMod.NPCs.Bosses.SoC.Bosses
{
    public class DeityRoseBarb : ModProjectile
    {

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Ei'Lor Barb");
            Main.npcFrameCount[projectile.type] = 2;
        }

        public override void SetDefaults()
        {
            projectile.CloneDefaults(276);
        }
    }
}