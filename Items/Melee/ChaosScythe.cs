using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Items.Melee   //where is located
{
    public class ChaosScythe : ModItem
    {
        public override void SetDefaults()
        {
            item.noUseGraphic = true;
            item.useStyle = 1;
            item.damage = 250;
            item.melee = true;           
            item.width = 56;              
            item.height = 56;          
            item.knockBack = 6;
            item.value = 100000;
            item.autoReuse = true;
            item.useTurn = false;
            item.expert = true;
            item.useAnimation = 30;
            item.useTime = 30;
            item.shootSpeed = 5;
            item.shoot = mod.ProjectileType("ChaosScythe");
        }

        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            int i = Main.myPlayer;
            int num74 = item.shoot;
            int num76 = item.damage;
            float num77 = item.knockBack;
            float mouseX = (Main.mouseX + Main.screenPosition.X) / 16;
            float mouseY = (Main.mouseY + Main.screenPosition.Y) / 16;
            Main.PlaySound(new LegacySoundStyle(2, 71, Terraria.Audio.SoundType.Sound), new Vector2(mouseX, mouseY));
            Projectile.NewProjectile(mouseX + 250, mouseY, -7, 0, mod.ProjectileType("ChaosScytheP"), 250, 1, item.owner, 0f, 0f);
            Projectile.NewProjectile(mouseX - 250, mouseY, 7, 0, mod.ProjectileType("ChaosScytheP"), 250, 1, item.owner, 0f, 0f);
            Projectile.NewProjectile(mouseX, mouseY + 250, 0, -7, mod.ProjectileType("ChaosScytheP"), 250, 1, item.owner, 0f, 0f);
            Projectile.NewProjectile(mouseX, mouseY - 250, 0, 7, mod.ProjectileType("ChaosScytheP"), 250, 1, item.owner, 0f, 0f);

            return false;
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Final Chaos");
            Tooltip.SetDefault(@"'I CAN DO ANYTHING'
Legendary Weapon");
        }

        public override void ModifyTooltips(List<TooltipLine> list)
        {
            float Eggroll = Math.Abs(Main.GameUpdateCount) / 7f;
            float Pie = 1f * (float)Math.Sin(Eggroll);
            Color color1 = Color.Lerp(new Color(85, 145, 93), new Color(64, 61, 99), Pie);
            foreach (TooltipLine line2 in list)
            {
                if (line2.mod == "Terraria" && line2.Name == "ItemName")
                {
                    line2.overrideColor = color1;
                }
            }
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.DeathSickle, 1);
            recipe.AddIngredient(ItemID.IceSickle, 1);
            recipe.AddIngredient(ItemID.Sickle, 1); ;
            recipe.AddIngredient(null, "Discord", 1);
            recipe.AddIngredient(null, "EXSoul", 1);
            recipe.AddTile(null, "QuantumFusionAccelerator");
            recipe.SetResult(this);
            recipe.AddRecipe();

        }
    }
}
