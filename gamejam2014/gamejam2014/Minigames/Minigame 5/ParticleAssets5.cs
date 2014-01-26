using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Utilities.Graphics;

namespace gamejam2014.Minigames.Minigame_5
{
    public static class ParticleAssets5
    {
        private static float Scale { get { return WorldData.ZoomScaleAmount[ZoomLevels.Five]; } }

        public static AnimatedSprite DirtCloud { get { return Minigame_3.ParticleAssets3.DirtCloud; } }

        public static void Initialize(GraphicsDevice gd, Microsoft.Xna.Framework.Content.ContentManager content)
        {

        }

        public static ParticleEffect GetSunHitParticles(Vector2 fromSun, Vector2 pos, GameTime gt)
        {
            AnimatedSprite spr = new AnimatedSprite(DirtCloud);
            spr.DrawArgs.Scale = Vector2.One * 0.15f * Scale;
            spr.DrawArgs.Color = new Color(254, 216, 1);

            return new ParticleEffect(gt, ArtAssets.EmptySprite, spr, Vector2.Zero, pos, 30, false,
                                      TimeSpan.FromSeconds(0.5), TimeSpan.FromSeconds(0.4), Scale * 20.0f,
                                      new Vector2(0.25f), Vector2.Zero, 0.0f, 2.0f, 0.0f, 0.0f,
                                      fromSun * Scale * 600.0f, 0.5f, 0.0f, fromSun * Scale * -1000.0f,
                                      0.0f, 0.0f, true, true);
        }

        public static ParticleEffect GetPlanetHitParticles(Vector2 hitPos, Vector2 hitTangent, GameTime gt)
        {
            AnimatedSprite spr = new AnimatedSprite(DirtCloud);
            spr.DrawArgs.Scale = Vector2.One * 0.05f * Scale;
            spr.DrawArgs.Color = Color.White;

            return new ParticleEffect(gt, ArtAssets.EmptySprite, spr, Vector2.Zero, hitPos, 50, true,
                                      TimeSpan.FromSeconds(0.5), TimeSpan.FromSeconds(0.1),
                                      Scale * 1.0f, Vector2.Zero, Vector2.Zero, 0.0f, 0.0f, 0.0f, 0.0f,
                                      hitTangent * Scale * 0.1f, 0.0f, Scale * 100.0f, Vector2.Zero, 0.0f, 0.0f, true, true);
        }
    }
}
