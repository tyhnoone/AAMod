﻿using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.Graphics;
using Terraria.Graphics.Effects;
using Terraria.Utilities;

namespace AAMod.Backgrounds
{
    public class YamataSky : CustomSky
    {
        
        public static Texture2D PlanetTexture;
        public static Texture2D BGTexture;
        public static Texture2D SkyTex;
        public bool Active;
        public float Intensity;
        public static Texture2D BeamTexture;
        public static Texture2D[] RockTextures;
        private struct LightPillar
        {
            public Vector2 Position;

            public float Depth;
        }

        private LightPillar[] _pillars;

        private UnifiedRandom _random = new UnifiedRandom();

        public override void OnLoad()
        {
            PlanetTexture = TextureManager.Load("Backgrounds/YamataMoon");
            BeamTexture = TextureManager.Load("Backgrounds/YamataBeam");
            RockTextures = new Texture2D[3];
            for (int i = 0; i < RockTextures.Length; i++)
            {
                RockTextures[i] = TextureManager.Load("Backgrounds/YamataRock" + i);
            }

            SkyTex = TextureManager.Load("Backgrounds/YamataStars");
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
        }

        

        public override Color OnTileColor(Color inColor)
        {
            Vector4 value = inColor.ToVector4();
            return new Color(Vector4.Lerp(value, Vector4.One, Intensity * 0.5f));
        }

        public override void Draw(SpriteBatch spriteBatch, float minDepth, float maxDepth)
        {
            if (maxDepth >= 3.40282347E+38f && minDepth < 3.40282347E+38f)
            {
                if (!Main.dayTime)
                {
                    spriteBatch.Draw(SkyTex, new Rectangle(0, 0, Main.screenWidth, Main.screenHeight), Color.White);
                    double bgTop = (int)((-Main.screenPosition.Y) / (Main.worldSurface * 16.0 - 600.0) * 200.0);
                    Main.bgColor = Color.White;
                    if (Main.gameMenu || Main.netMode == 2)
                    {
                        bgTop = -200;
                    }
                    int num23 = (int)(Main.time / 32400.0 * (Main.screenWidth + Main.moonTexture[Main.moonType].Width * 2)) - Main.moonTexture[Main.moonType].Width;
                    int num24 = 0;
                    Color white2 = Color.White;
                    float num25 = 1f;
                    float rotation2 = (float)(Main.time / 32400.0) * 2f - 7.3f;
                    if (!Main.dayTime)
                    {
                        double num27;
                        if (Main.time < 16200.0)
                        {
                            num27 = Math.Pow(1.0 - Main.time / 32400.0 * 2.0, 2.0);
                            num24 = (int)(bgTop + num27 * 250.0 + 180.0);
                        }
                        else
                        {
                            num27 = Math.Pow((Main.time / 32400.0 - 0.5) * 2.0, 2.0);
                            num24 = (int)(bgTop + num27 * 250.0 + 180.0);
                        }
                        num25 = (float)(1.2 - num27 * 0.4);
                    }
                    float num65 = 1f - Main.cloudAlpha * 1.5f;
                    if (num65 < 0f)
                    {
                        num65 = 0f;
                    }
                    white2.R = (byte)(white2.R * num65);
                    white2.G = (byte)(white2.G * num65);
                    white2.B = (byte)(white2.B * num65);
                    white2.A = (byte)(white2.A * num65);
                    Main.spriteBatch.Draw(PlanetTexture, new Vector2(num23, num24 + Main.moonModY), new Rectangle?(new Rectangle(0, 0, PlanetTexture.Width, PlanetTexture.Width)), white2, rotation2, new Vector2(PlanetTexture.Width / 2, PlanetTexture.Width / 2), num25, SpriteEffects.None, 0f);
                }
            }
            int num = -1;
            int num2 = 0;
            for (int i = 0; i < _pillars.Length; i++)
            {
                float depth = _pillars[i].Depth;
                if (num == -1 && depth < maxDepth)
                {
                    num = i;
                }
                if (depth <= minDepth)
                {
                    break;
                }
                num2 = i;
            }
            if (num == -1)
            {
                return;
            }
            Vector2 value3 = Main.screenPosition + new Vector2(Main.screenWidth >> 1, Main.screenHeight >> 1);
            Rectangle rectangle = new Rectangle(-1000, -1000, 4000, 4000);
            float scale = Math.Min(1f, (Main.screenPosition.Y - 1000f) / 1000f);
            for (int j = num; j < num2; j++)
            {
                Vector2 value4 = new Vector2(1f / _pillars[j].Depth, 0.9f / _pillars[j].Depth);
                Vector2 vector = _pillars[j].Position;
                vector = (vector - value3) * value4 + value3 - Main.screenPosition;
                if (rectangle.Contains((int)vector.X, (int)vector.Y))
                {
                    float num3 = value4.X * 450f;
                    spriteBatch.Draw(BeamTexture, vector, null, Color.White * 0.2f * scale * Intensity, 0f, Vector2.Zero, new Vector2(num3 / 70f, num3 / 45f), SpriteEffects.None, 0f);
                    int num4 = 0;
                    for (float num5 = 0f; num5 <= 1f; num5 += 0.03f)
                    {
                        float num6 = 1f - (num5 + Main.GlobalTime * 0.02f + (float)Math.Sin((float)j)) % 1f;
                        spriteBatch.Draw(RockTextures[num4], vector + new Vector2((float)Math.Sin(num5 * 1582f) * (num3 * 0.5f) + num3 * 0.5f, num6 * 2000f), null, Color.White * num6 * scale * Intensity, num6 * 20f, new Vector2(RockTextures[num4].Width >> 1, RockTextures[num4].Height >> 1), 0.9f, SpriteEffects.None, 0f);
                        num4 = (num4 + 1) % RockTextures.Length;
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
            _pillars = new LightPillar[40];
            for (int i = 0; i < _pillars.Length; i++)
            {
                _pillars[i].Position.X = i / (float)_pillars.Length * (Main.maxTilesX * 16f + 20000f) + _random.NextFloat() * 40f - 20f - 20000f;
                _pillars[i].Position.Y = _random.NextFloat() * 200f - 2000f;
                _pillars[i].Depth = _random.NextFloat() * 8f + 7f;
            }
            Array.Sort(_pillars, new Comparison<LightPillar>(SortMethod));
        }

        private int SortMethod(LightPillar pillar1, LightPillar pillar2)
        {
            return pillar2.Depth.CompareTo(pillar1.Depth);
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
}