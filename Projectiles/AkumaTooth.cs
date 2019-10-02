﻿namespace AAMod.Projectiles
{
    public class AkumaTooth : ShenTooth
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Akuma Tooth");
        }

        public override void SetDefaults() // Clones the bullet defaults
        {
            projectile.CloneDefaults(mod.ProjectileType<ShenTooth>());
            type = 1;
        }
    }
}
