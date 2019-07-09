using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria.ModLoader;
using AAMod.Items;

namespace AAMod
{
    /*public class Revengance : ModWorld
    {
        public static Mod CalamityMod = ModLoader.GetMod("CalamityMod");
        
        public static bool Revenge => CalamityMod.World.CalamityWorld.revenge;
        public static bool Death => CalamityMod.World.CalamityWorld.death;
        public static bool Defiled => CalamityMod.World.CalamityWorld.defiled;
    }


    public abstract class RogueItem : BaseAAItem
    {
        public string crossoverModName = "CalamityMod";

        public override void ModifyTooltips(List<TooltipLine> list)
        {
            if (!ModSupport.ModInstalled(crossoverModName))
            {
                TooltipLine error = new TooltipLine(mod, "Error", "WARNING: ITEM WILL NOT FUNCTION WITHOUT CALAMITY ENABLED!")
                {
                    overrideColor = new Color(255, 50, 50)
                };
                list.Add(error);
            }
        }
    }

    public class RoguePlayer : ModPlayer
    {
        public int RogueDamage => Calamity.Items.CalamityCustomThrowingDamage.CalamityCustomThrowingDamagePlayer.throwingDamage;
        public int RogueCrit => Calamity.Items.CalamityCustomThrowingDamage.CalamityCustomThrowingDamagePlayer.throwingCrit;
    }*/
}