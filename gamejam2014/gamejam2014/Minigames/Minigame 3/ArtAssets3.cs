﻿using System;
using System.Linq;
using System.Collections.Generic;
using Utilities.Graphics;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using V2 = Microsoft.Xna.Framework.Vector2;
using Col = Microsoft.Xna.Framework.Color;

namespace gamejam2014.Minigames.Minigame_3
{
    public static class ArtAssets3
    {
        public static Utilities.Math.Shape.Shape GetDoghouseShape(float zoomScale, V2 pos)
        {
            return new Utilities.Math.Shape.Polygon(Microsoft.Xna.Framework.Graphics.CullMode.CullClockwiseFace,
                                                    new List<V2>()
                                                    {
                                                        new V2(1.0f),
                                                        new V2(0.0f, -1.0f),
                                                        new V2(0.0f, 1.0f),
                                                    }.Select(v => (v * zoomScale) + pos).ToArray());
        }
        public static float HillRadius = 50.0f;

        public static AnimatedSprite HillSprite;
        public static AnimatedSprite DogHouseSprite;

        public static void Initialize(GraphicsDevice device, ContentManager content)
        {
            HillSprite = new AnimatedSprite(content.Load<Texture2D>("Art/Z3 Art/Hill"), 4, TimeSpan.FromSeconds(0.05), true, -1, 1);
            HillSprite.SetOriginToCenter();
            HillSprite.StartAnimation();
        }

        public static void DrawHillTimeBar(V2 playerPos, float playerTimeInHill, SpriteBatch sb)
        {
            float lerp = playerTimeInHill / PhysicsData3.TimeInHillToWin;

            V2 offset = new V2(-50.0f, -50.0f);

            Col backgroundCol = Col.Black;
            const int width = 20, height = 100;
            TexturePrimitiveDrawer.DrawRect(new Microsoft.Xna.Framework.Rectangle((int)(playerPos.X + offset.X) - width,
                                                                                  (int)(playerPos.Y + offset.Y) - height,
                                                                                  width, height),
                                            sb, backgroundCol, 1);

            Col foregroundCol = Col.White;
            foregroundCol.A = (byte)(255.0f * lerp);
            const int border = 5;
            int height2 = (byte)((height - (border * 2)) * lerp);
            TexturePrimitiveDrawer.DrawRect(new Microsoft.Xna.Framework.Rectangle((int)(playerPos.X + offset.X) - width + border,
                                                                                  (int)(playerPos.Y + offset.Y) - height2,
                                                                                  width - (2 * border),
                                                                                  height2 - border),
                                            sb, foregroundCol, 1);
        }
    }
}
