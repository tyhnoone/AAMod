﻿using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.Graphics;
using Terraria.Graphics.Effects;
using Terraria.Utilities;
using BaseMod;
using AAMod.NPCs.Bosses.Athena;
using Terraria.Graphics.Shaders;

namespace AAMod.Backgrounds
{
    public class AthenaSky : CustomSky
    {
        private readonly UnifiedRandom random = new UnifiedRandom();

        private struct Bolt
        {
            public Vector2 Position;

            public float Depth;

            public int Life;

            public bool IsAlive;
        }

        public static Texture2D boltTexture;
        public static Texture2D flashTexture;
        private Bolt[] bolts;
        public bool Active;
        public int ticksUntilNextBolt;
        public float Intensity;

        public override void OnLoad()
        {
            boltTexture = TextureManager.Load("Backgrounds/AthenaBolt");
            flashTexture = TextureManager.Load("Backgrounds/AthenaFlash");
        }

        public override void Update(GameTime gameTime)
        {
            if (Active)
            {
                Intensity = Math.Min(1f, 0.01f + Intensity);
            }
            else
            {
                Intensity = Math.Max(0f, Intensity - 0.01f);
            }
            if (ticksUntilNextBolt <= 0)
            {
                ticksUntilNextBolt = random.Next(5, 20);
                int num = 0;
                while (bolts[num].IsAlive && num != bolts.Length - 1)
                {
                    num++;
                }
                bolts[num].IsAlive = true;
                bolts[num].Position.X = random.NextFloat() * (Main.maxTilesX * 16f + 4000f) - 2000f;
                bolts[num].Position.Y = random.NextFloat() * 500f;
                bolts[num].Depth = random.NextFloat() * 8f + 2f;
                bolts[num].Life = 30;
            }
            ticksUntilNextBolt--;
            for (int i = 0; i < bolts.Length; i++)
            {
                if (bolts[i].IsAlive)
                {
                    Bolt[] expr168cp0 = bolts;
                    int expr168cp1 = i;
                    expr168cp0[expr168cp1].Life = expr168cp0[expr168cp1].Life - 1;
                    if (bolts[i].Life <= 0)
                    {
                        bolts[i].IsAlive = false;
                    }
                }
            }
        }

        public override Color OnTileColor(Color inColor)
        {
            Vector4 value = inColor.ToVector4();
            return new Color(Vector4.Lerp(value, Vector4.One, Intensity * 0.5f));
        }
        public float asteroidPercent1 = 0f;
        public float asteroidPercent2 = 0f;
        public float asteroidPercent3 = 0f;
        public float Rotation = 0;
        public float LBRotation = 0;
        public NPC IZ;


        public Color infinityGlowRed = new Color(233, 53, 53);
        public Color GetGlowAlpha(bool aura)
        {
            return (aura ? infinityGlowRed : Color.White) * (Main.mouseTextColor / 255f);
        }

        public Color GetAlpha(Color newColor, float alph)
        {
            int alpha = 255 - (int)(255 * alph);
            float alphaDiff = (255 - alpha) / 255f;
            int newR = (int)(newColor.R * alphaDiff);
            int newG = (int)(newColor.G * alphaDiff);
            int newB = (int)(newColor.B * alphaDiff);
            int newA = newColor.A - alpha;
            if (newA < 0) newA = 0;
            if (newA > 255) newA = 255;
            return new Color(newR, newG, newB, newA);
        }


        readonly AthenaClouds clouds = new AthenaClouds(false);

        public override void Draw(SpriteBatch spriteBatch, float minDepth, float maxDepth)
        {
            if (maxDepth >= 3.40282347E+38f && minDepth < 3.40282347E+38f)
            {
                clouds.Update(AAMod.instance.GetTexture("Backgrounds/FogTex"));
                clouds.Draw(AAMod.instance.GetTexture("Backgrounds/FogTex"), false, Color.CornflowerBlue, true);
            }
            float scale = Math.Min(1f, (Main.screenPosition.Y - 1000f) / 1000f);
            Vector2 value3 = Main.screenPosition + new Vector2(Main.screenWidth >> 1, Main.screenHeight >> 1);
            Rectangle rectangle = new Rectangle(-1000, -1000, 4000, 4000);
            for (int i = 0; i < bolts.Length; i++)
            {
                if (bolts[i].IsAlive && bolts[i].Depth > minDepth && bolts[i].Depth < maxDepth)
                {
                    Vector2 value4 = new Vector2(1f / bolts[i].Depth, 0.9f / bolts[i].Depth);
                    Vector2 position = (bolts[i].Position - value3) * value4 + value3 - Main.screenPosition;
                    if (rectangle.Contains((int)position.X, (int)position.Y))
                    {
                        Texture2D texture = boltTexture;
                        int life = bolts[i].Life;
                        if (life > 26 && life % 2 == 0)
                        {
                            texture = flashTexture;
                        }
                        float scale2 = life / 30f;
                        spriteBatch.Draw(texture, position, null, Color.White * scale * scale2 * Intensity, 0f, Vector2.Zero, value4.X * 5f, SpriteEffects.None, 0f);
                    }
                }
            }
        }

        public override float GetCloudAlpha()
        {
            return 1f - Intensity;
        }

        public override void Activate(Vector2 position, params object[] args)
        {
            Intensity = 0.002f;
            Active = true;

            bolts = new Bolt[500];
            for (int i = 0; i < bolts.Length; i++)
            {
                bolts[i].IsAlive = false;
            }
        }

        public override void Deactivate(params object[] args)
        {
            Active = false;
        }

        public override void Reset()
        {
            Active = false;
        }

        public override bool IsActive()
        {
            return Active || Intensity > 0.001f;
        }
    }

    public class AthenaSkyData : ScreenShaderData
    {
        public AthenaSkyData(string passName) : base(passName)
        {
        }

        private void UpdateAthenaSky()
        {
            if (NPC.AnyNPCs(AAMod.instance.NPCType<Athena>()))
            {
                return;
            }
        }

        public override void Apply()
        {
            UpdateAthenaSky();
            base.Apply();
        }
    }

    public class AthenaClouds
    {
        public int fogOffsetX = 0;
        public float fadeOpacity = 0f;
        public float dayTimeOpacity = 0f;
        public bool backgroundClouds = false;

        public AthenaClouds(bool bg)
        {
            backgroundClouds = bg;
        }

        public void Update(Texture2D texture)
        {
            if (Main.netMode == 2 || Main.dedServ) return; //BEGONE SERVER HEATHENS! UPDATE ONLY CLIENTSIDE!

            bool athena = NPC.AnyNPCs(AAMod.instance.NPCType<Athena>());
            if (!backgroundClouds) athena = false;

            fogOffsetX += 1;
            if (fogOffsetX >= texture.Width) fogOffsetX = 0;
            if (athena)
            {
                fadeOpacity += 0.05f;
                if (fadeOpacity > 1f) fadeOpacity = 1f;
            }
            else
            {
                fadeOpacity -= 0.05f;
                if (fadeOpacity < 0f) fadeOpacity = 0f;
            }
            if (backgroundClouds)
            {
                dayTimeOpacity = BaseUtility.MultiLerp((float)Main.time / 52000f, 0.5f, 1f, 1f, 1f, 1f, 1f, 0.5f);
                dayTimeOpacity *= 0.7f; //make it fadier as it's in the background
            }
            else
            {
                dayTimeOpacity = BaseUtility.MultiLerp((float)Main.time / 52000f, 0.3f, 1f, 1f, 1f, 1f, 1f, 0.3f);
                dayTimeOpacity *= Main.dayTime ? 2f : 1f;
            }
        }

        public void Draw(Texture2D texture, bool dir, Color defaultColor, bool setSB = false)
        {
            if (fadeOpacity == 0f) return; //don't draw if no fog
            if (setSB) Main.spriteBatch.Begin();


            Color fogColor = GetAlpha(defaultColor, 0.4f * fadeOpacity * dayTimeOpacity);

            int minX = -texture.Width;
            int minY = -texture.Height;
            int maxX = Main.screenWidth + texture.Width;
            int maxY = Main.screenHeight + texture.Height;


            for (int i = minX; i < maxX; i += texture.Width)
            {
                for (int j = minY; j < maxY; j += texture.Height)
                {
                    Main.spriteBatch.Draw(texture, new Rectangle(i + (dir ? -fogOffsetX : fogOffsetX), j, texture.Width, texture.Height), null, fogColor, 0f, Vector2.Zero, SpriteEffects.None, 0f);
                }
            }
            if (setSB) Main.spriteBatch.End();
        }

        public Color GetAlpha(Color newColor, float alph)
        {
            int alpha = 255 - (int)(255 * alph);
            float alphaDiff = (255 - alpha) / 255f;
            int newR = (int)(newColor.R * alphaDiff);
            int newG = (int)(newColor.G * alphaDiff);
            int newB = (int)(newColor.B * alphaDiff);
            int newA = newColor.A - alpha;
            if (newA < 0) newA = 0;
            if (newA > 255) newA = 255;
            return new Color(newR, newG, newB, newA);
        }
    }
}