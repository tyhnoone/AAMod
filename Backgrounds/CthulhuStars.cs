using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.Graphics;
using Terraria.Graphics.Effects;
using Terraria.Utilities;
using BaseMod;
using AAMod.NPCs.Bosses.Infinity;
using Terraria.ModLoader;

namespace AAMod.Backgrounds
{
    public class CthulhuStars : CustomSky
    {
        private UnifiedRandom random = new UnifiedRandom();

        private struct Bolt
        {
            public Vector2 Position;

            public float Depth;
            public float Rotation;

            public int Life;

            public bool IsAlive;
        }

        public static Texture2D boltTexture;
        public static Texture2D flashTexture;
        private Texture2D texture = AAMod.instance.GetTexture("Backgrounds/CthulhuStars");
        private Bolt[] bolts;
        public bool Active;
        public int ticksUntilNextBolt;
        public float Intensity; private Texture2D BeamTexture;
        private Texture2D[] RockTextures;
        private LightPillar[] _pillars;
        private struct LightPillar
        {
            public Vector2 Position;

            public float Depth;
        }

        public override void OnLoad()
        {
            BeamTexture = TextureManager.Load("Backgrounds/CthulhuBeam");
            RockTextures = new Texture2D[3];
            for (int i = 0; i < RockTextures.Length; i++)
            {
                RockTextures[i] = TextureManager.Load("Backgrounds/Rock_" + i);
            }

            boltTexture = TextureManager.Load("Backgrounds/CthulhuBolt");
            flashTexture = TextureManager.Load("Backgrounds/CthulhuFlash");
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
                bolts[num].Position.X = random.NextFloat() * 2000f;
                bolts[num].Position.Y = random.NextFloat() * 1000f;
                bolts[num].Rotation = random.NextFloat() * ((float)Math.PI * 2f);
                bolts[num].Depth = random.NextFloat() * 8f + 2f;
                bolts[num].Life = 30;
            }
            ticksUntilNextBolt--;
            for (int i = 0; i < bolts.Length; i++)
            {
                if (bolts[i].IsAlive)
                {
                    bolts[i].Life -= 1;
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

        public override void Draw(SpriteBatch spriteBatch, float minDepth, float maxDepth)
        {
            Mod mod = AAMod.instance;
            Vector2 SkyPos = new Vector2(Main.screenWidth / 2, Main.screenHeight / 2);
            if (maxDepth >= 3.40282347E+38f && minDepth < 3.40282347E+38f)
            {
                spriteBatch.Draw(texture, SkyPos, null, Color.White, 0f, new Vector2(texture.Width >> 1, texture.Height >> 1), 1f, SpriteEffects.None, 1f);
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
            Vector2 value3 = Main.screenPosition + new Vector2((float)(Main.screenWidth >> 1), (float)(Main.screenHeight >> 1));
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
                        float num6 = 1f - (num5 + Main.GlobalTime * 0.02f + (float)Math.Sin((double)((float)j))) % 1f;
                        spriteBatch.Draw(RockTextures[num4], vector + new Vector2((float)Math.Sin((double)(num5 * 1582f)) * (num3 * 0.5f) + num3 * 0.5f, num6 * 2000f), null, Color.White * num6 * scale * Intensity, num6 * 20f, new Vector2((float)(RockTextures[num4].Width >> 1), (float)(RockTextures[num4].Height >> 1)), 0.9f, SpriteEffects.None, 0f);
                        num4 = (num4 + 1) % RockTextures.Length;
                    }
                }
            }
            for (int i = 0; i < bolts.Length; i++)
            {
                if (bolts[i].IsAlive)
                {
                    Vector2 position = bolts[i].Position;
                    float scale1 = MathHelper.Lerp(0.5f, 0.25f, Math.Max(0f, Math.Min(1f, (position.X / 1000f))));
                    if (rectangle.Contains((int)position.X, (int)position.Y))
                    {
                        Vector2 value4 = new Vector2(1f / bolts[i].Depth, 0.9f / bolts[i].Depth);
                        Texture2D texture = boltTexture;
                        int life = bolts[i].Life;
                        if (life > 26 && life % 2 == 0)
                        {
                            texture = flashTexture;
                        }
                        float scale2 = (float)life / 30f;
                        spriteBatch.Draw(texture, position, null, Color.White * scale1 * scale2 * Intensity, bolts[i].Rotation, Vector2.Zero, scale, SpriteEffects.None, 0f);
                    }
                }
            }
        }

        public override void Activate(Vector2 position, params object[] args)
        {
            Intensity = 0.002f;
            Active = true;
            _pillars = new LightPillar[40];
            for (int i = 0; i < _pillars.Length; i++)
            {
                _pillars[i].Position.X = (float)i / (float)_pillars.Length * ((float)Main.maxTilesX * 16f + 20000f) + random.NextFloat() * 40f - 20f - 20000f;
                _pillars[i].Position.Y = random.NextFloat() * 200f - 2000f;
                _pillars[i].Depth = random.NextFloat() * 8f + 7f;
            }
            Array.Sort(_pillars, new Comparison<LightPillar>(SortMethod));

            bolts = new Bolt[500];
            for (int i = 0; i < bolts.Length; i++)
            {
                bolts[i].IsAlive = false;
            }
        }


        private int SortMethod(LightPillar pillar1, LightPillar pillar2)
        {
            return pillar2.Depth.CompareTo(pillar1.Depth);
        }

        public override void Deactivate(params object[] args)
        {
            Active = false;
        }

        public override float GetCloudAlpha()
        {
            return (1f - Intensity);
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