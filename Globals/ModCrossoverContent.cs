using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;

using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using BaseMod;
using System.Reflection;
using Microsoft.Xna.Framework.Graphics;
using System.Collections;

namespace AAMod
{
    public class ModSupport
    {
        public static Mod thorium = null;

        public static bool ModInstalled(string name)
        {
            switch (name)
            {
                case "Thorium": return thorium != null;
                default: return false;
            }
        }

        public static bool forceBlackMapBG = false;
        public static Texture2D forceBlackMapTexture = null;

        public static Texture2D GetMapBackgroundImage()
        {
            return (forceBlackMapBG ? Main.mapTexture : (Texture2D)null);
        }

        public static void UnloadSupport()
        {
            #region jopo's large world enabler
            if (forceBlackMapBG)
            {
                forceBlackMapBG = false;
                Main.mapTexture = forceBlackMapTexture;
                forceBlackMapTexture = null;
            }
            #endregion
        }

        public static void SetupSupport()
        {
            Mod mod = AAMod.instance;
            thorium = ModLoader.GetMod("ThoriumMod");
        }
    }

    public abstract class CrossoverItem : ModItem
    {
        public string crossoverModName = "THORIUM";

        public override void ModifyTooltips(List<TooltipLine> list)
        {
            if (!ModSupport.ModInstalled(crossoverModName)) //this is to give a warning if they have the item and the mod is not enabled
            {
                TooltipLine error = new TooltipLine(mod, "Error", "WARNING: ITEM MAY NOT FUNCTION CORRECTLY WITHOUT " + crossoverModName.ToUpper() + " ENABLED!");
                error.overrideColor = new Color(255, 50, 50);
                list.Add(error);
            }
        }
    }

    public class ModSupportPlayer : ModPlayer
    {
        #region thorium variables
        public float thorium_radiantBoost
        {
            get
            {
                if (ModSupport.thorium != null)
                {
                    float? boost = (float?)ModSupport.thorium.Call("GetRadiantBoost", player.whoAmI);
                    if (boost != null) return (float)boost;
                }
                return 1f;
            }
            set
            {
                if (ModSupport.thorium != null)
                {
                    ModSupport.thorium.Call("SetRadiantBoost", player.whoAmI, value);
                }
            }
        }
        public int thorium_radiantCrit
        {
            get
            {
                if (ModSupport.thorium != null)
                {
                    int? boost = (int?)ModSupport.thorium.Call("GetRadiantCrit", player.whoAmI);
                    if (boost != null) return (int)boost;
                }
                return 0;
            }
            set
            {
                if (ModSupport.thorium != null)
                {
                    ModSupport.thorium.Call("SetRadiantCrit", player.whoAmI, value);
                }
            }
        }
        public int thorium_healBonus
        {
            get
            {
                if (ModSupport.thorium != null)
                {
                    int? boost = (int?)ModSupport.thorium.Call("GetHealBonus", player.whoAmI);
                    if (boost != null) return (int)boost;
                }
                return 0;
            }
            set
            {
                if (ModSupport.thorium != null)
                {
                    ModSupport.thorium.Call("SetHealBonus", player.whoAmI, value);
                }
            }
        }
        #endregion
    }

    /*public class ThoriumItem : ModItem
    {
        public bool radiant = false;

        public void RadiantDamage(Item item)
        {
            if (item.modItem is ThoriumItem && ((ThoriumItem)item.modItem).radiant)
            {
                int index = -1;
                for (int m = 0; m < list.Count; m++)
                {
                    if (list[m].Name.Equals("Damage")) { index = m; break; }
                }
                string oldTooltip = list[index].text;
                string[] split = oldTooltip.Split(' ');
                list.RemoveAt(index);
                list.Insert(index, new TooltipLine(mod, "Damage", split[0] + " radiant damage"));

                int index2 = -1;
                for (int m = 0; m < list.Count; m++)
                {
                    if (list[m].Name.Equals("CritChance")) { index2 = m; break; }
                }

                list.RemoveAt(index2);
                list.Insert(index2, new TooltipLine(mod, "CritChance", player.GetThoriumPlayer().radiantCrit + item.crit + "% critical strike chance"));
            }
        }
    }*/
}