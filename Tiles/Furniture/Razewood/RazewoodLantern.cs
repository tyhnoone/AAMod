using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.Enums;
using Terraria.ModLoader;
using Terraria.ObjectData;
using Microsoft.Xna.Framework.Graphics;

namespace AAMod.Tiles.Furniture.Razewood
{
    public class RazewoodLantern : ModTile
	{
		public override void SetDefaults()
		{
            Main.tileLighted[Type] = true;
            Main.tileFrameImportant[Type] = true;
            Main.tileLavaDeath[Type] = true;

            TileObjectData.newTile.CopyFrom(TileObjectData.Style1x2Top);
            TileObjectData.newSubTile.CopyFrom(TileObjectData.newTile);
            TileObjectData.newSubTile.LavaDeath = false;
            TileObjectData.newSubTile.LavaPlacement = LiquidPlacement.Allowed;
            TileObjectData.addTile(Type);
			ModTranslation name = CreateMapEntryName();
			name.SetDefault("Razewood Latern");
            AddMapEntry(new Color(205, 62, 12), name);
            dustType = mod.DustType("RazewoodDust");
            adjTiles = new int[] { TileID.HangingLanterns };
            AddToArray(ref TileID.Sets.RoomNeeds.CountsAsTorch);

        }
        public override void HitWire(int i, int j)
        {
            int left = i - (Main.tile[i, j].frameX / 18) % 1;
            int top = j - (Main.tile[i, j].frameY / 18) % 2;
            for (int x = left; x < left + 1; x++)
            {
                for (int y = top; y < top + 2; y++)
                {

                    if (Main.tile[x, y].frameX >= 18)
                    {
                        Main.tile[x, y].frameX -= 18;
                    }
                    else
                    {
                        Main.tile[x, y].frameX += 18;
                    }
                }
            }
            if (Wiring.running)
            {
                Wiring.SkipWire(left, top);
                Wiring.SkipWire(left, top + 1);
            }
            NetMessage.SendTileSquare(-1, left, top + 1, 2);

        }

        public override void NumDust(int i, int j, bool fail, ref int num)
		{
			num = fail ? 1 : 3;
		}
        public override void ModifyLight(int i, int j, ref float r, ref float g, ref float b)
        {
            Tile tile = Main.tile[i, j];
            if (tile.frameX < 18)
            {
                r = 0.9f;
                g = 0.9f;
                b = 0.9f;
            }
        }

        public override void KillMultiTile(int i, int j, int frameX, int frameY)
		{
			Item.NewItem(i * 16, j * 16, 48, 32, mod.ItemType("RazewoodLantern"));
			Chest.DestroyChest(i, j);
		}
        public override void PostDraw(int i, int j, SpriteBatch spriteBatch)
        {
            ulong randSeed = Main.TileFrameSeed ^ (ulong)((j<< 32) | (long)(ulong)i);
            Color color = new Color(100, 100, 100, 0);
            int frameX = Main.tile[i, j].frameX;
            int frameY = Main.tile[i, j].frameY;
            int width = 20;
            int offsetY = 2;
            int height = 20;
            int offsetX = 2;
            Vector2 zero = new Vector2(Main.offScreenRange, Main.offScreenRange);
            if (Main.drawToScreen)
            {
                zero = Vector2.Zero;
            }
            for (int k = 0; k < 7; k++)
            {
                Main.spriteBatch.Draw(mod.GetTexture("Tiles/Furniture/Razewood/RazewoodLantern_Glow"), new Vector2(i * 16 - (int)Main.screenPosition.X + offsetX - (width - 16f) / 2f, j * 16 - (int)Main.screenPosition.Y + offsetY) + zero, new Rectangle(frameX, frameY, width, height), color, 0f, default, 1f, SpriteEffects.None, 0f);
            }
        }
    }
}
