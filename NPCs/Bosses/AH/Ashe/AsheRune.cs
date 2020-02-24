using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using BaseMod;
using Microsoft.Xna.Framework.Graphics;
using Terraria.Graphics.Shaders;

namespace AAMod.NPCs.Bosses.AH.Ashe
{
    public class AsheRune : ModNPC
    {
        public override void SetDefaults()
        {
            npc.alpha = 255;
            npc.dontTakeDamage = true;
            npc.lifeMax = 1;
            npc.aiStyle = -1;
            npc.damage = Main.expertMode ? 50 : 84;
            npc.defense = Main.expertMode ? 1 : 1;
            npc.knockBackResist = 0.2f;
            npc.width = 82;
            npc.height = 82;
            npc.value = Item.buyPrice(0, 0, 0, 0);
            npc.lavaImmune = true;
            npc.noTileCollide = true;
            npc.noGravity = true;
            npc.scale = .001f;
            npc.friendly = false;
        }

        public int count = 0;
        public int Control = 0;

        public int SootProj = 0;

        public Vector2 Runeshootspeed = new Vector2();

        public override void AI()
        {
            if (Control == 1)
            {

                if (count == 0)
                {
                    if(Main.player[Main.npc[(int)npc.ai[3]].target].position - new Vector2(npc.ai[0], npc.ai[1]) == new Vector2(0f, 0f))
                    {
                        Runeshootspeed = new Vector2(0, 0);
                    }
                    else
                    {
                        Runeshootspeed = 10f * Vector2.Normalize(Main.player[Main.npc[(int)npc.ai[3]].target].position - new Vector2(npc.ai[0], npc.ai[1]));
                    }
                    SootProj = Projectile.NewProjectile(npc.Center.X + Runeshootspeed.X, npc.Center.Y + Runeshootspeed.Y, 0, 0, ModContent.ProjectileType<AsheShot>(), (int)npc.ai[2]/2, 0, Main.myPlayer, Runeshootspeed.X, Runeshootspeed.Y);
                    Main.projectile[SootProj].velocity = new Vector2(0,0);
                    Main.projectile[SootProj].alpha = 0;
                }
                else if(count < 30)
                {
                    Main.projectile[SootProj].alpha += 8;
                    if(Main.projectile[SootProj].alpha > 255) Main.projectile[SootProj].alpha = 255;
                    Main.projectile[SootProj].velocity = new Vector2(0,0);
                }
                else if(count >= 60)
                {
                    Main.projectile[SootProj].velocity = new Vector2(Runeshootspeed.X, Runeshootspeed.Y);
                    Control = 2;
                }
                count ++;
            }
            else if (Control == 2)
            {
                if (npc.alpha < 255)
                {
                    npc.alpha += 5;
                }
                else
                {
                    npc.active = false;
                }
                if (npc.scale < 1)
                {
                    npc.scale -= .04f;
                }
            }
            else
            {
                if (npc.alpha > 0)
                {
                    npc.alpha -= 5;
                }
                else
                {
                    npc.alpha = 0;
                    Control = 1;
                }
                if (npc.scale < 1)
                {
                    npc.scale += .04f;
                }
            }
        }

        public float auraPercent = 0f;
        public bool auraDirection = true;

        public override bool PreDraw(SpriteBatch sb, Color drawColor)
        {
            if (auraDirection) { auraPercent += 0.1f; auraDirection = auraPercent < 1f; }
            else { auraPercent -= 0.1f; auraDirection = auraPercent <= 0f; }
            BaseDrawing.DrawAura(sb, Main.npcTexture[npc.type], 0, npc.position, npc.width, npc.height, auraPercent, 1.4f, npc.scale, npc.rotation, npc.direction, 1, default, 0, 0, ColorUtils.COLOR_GLOWPULSE);
            int red = GameShaders.Armor.GetShaderIdFromItemId(ItemID.LivingFlameDye);
            BaseDrawing.DrawTexture(sb, Main.npcTexture[npc.type], red, npc.position, npc.width, npc.height, npc.scale, npc.rotation, npc.direction, 1, npc.frame, npc.GetAlpha(ColorUtils.COLOR_GLOWPULSE), true);
            return false;
        }
    }
}