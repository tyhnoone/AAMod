﻿using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.GameContent.UI;
using Terraria.Localization;
using Microsoft.Xna.Framework.Graphics;

namespace AAMod.Items.Currency
{
    public class ChristmasCheer : BaseAAItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Christmas Cheer");
            Tooltip.SetDefault("Pure joy and minty fresh goodness");
            ItemID.Sets.ItemIconPulse[item.type] = true;
            ItemID.Sets.ItemNoGravity[item.type] = true;
        }


        public override void PostUpdate()
        {
            Lighting.AddLight(item.Center, Color.LightCyan.ToVector3() * 0.55f * Main.essScale);
        }

        public override void SetDefaults()
        {
            item.width = 16;
            item.height = 16;
            item.maxStack = 999;
            item.value = 1000;
            item.rare = ItemRarityID.Yellow;
        }

        int counter = 0;
        int cframe = 0;

        public override bool PreDrawInWorld(SpriteBatch spriteBatch, Color lightColor, Color alphaColor, ref float rotation, ref float scale, int whoAmI)
        {
            if (counter++ > 7)
            {
                cframe++;
                counter = 0;
                if (cframe > 3)
                {
                    cframe = 0;
                }
            }

            Texture2D itemTex = mod.GetTexture("Items/Currency/ChristmasCheerA");

            Rectangle iframe = BaseDrawing.GetFrame(cframe, itemTex.Width, itemTex.Height / 4, 0, 0);

            BaseDrawing.DrawTexture(spriteBatch, itemTex, 0, item.position, item.width, item.height, scale, rotation, item.direction, 4, iframe, lightColor, true);
            return false;
        }
    }
    public class CCheer : CustomCurrencySingleCoin
    {
        public static Color color = Color.LightCyan;

        public CCheer(int coinItemID) : base(coinItemID, 999L)
        {
        }

        public override void GetPriceText(string[] lines, ref int currentLine, int price)
        {
            Color color2 = color * (Main.mouseTextColor / 255f);
            lines[currentLine++] = string.Format("[c/{0:X2}{1:X2}{2:X2}:{3} {4} {5}]", new object[]
            {
                color2.R,
                color2.G,
                color2.B,
                Language.GetTextValue("Mods.AAMod.Common.PlayerBuyPrice"),
                price,
                price == 1 ? Language.GetTextValue("Mods.AAMod.Common.ChristmasCheer") : Language.GetTextValue("Mods.AAMod.Common.ChristmasCheers")
            });
        }
    }
}