using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace gamejam2014
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        KarmaWorld world;

        private bool buttonDown = false;


        public enum States
        {
            Paused,
            Playing,

            MainMenu,
            Instructions,

            HarmonyWin,
            DiscordWin,
        }
        public States current = States.Playing;


        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            Utilities.Graphics.TexturePrimitiveDrawer.MakeBlankDrawingTex(GraphicsDevice);
            WorldData.Initialize(GraphicsDevice);
            ArtAssets.Initialize(GraphicsDevice, Content);
            SoundAssets.Initialize(Content);
            Minigames.Minigame_1.ArtAssets1.Initialize(GraphicsDevice, Content);
            Minigames.Minigame_2.ArtAssets2.Initialize(GraphicsDevice, Content);
            Minigames.Minigame_3.ArtAssets3.Initialize(GraphicsDevice, Content);
            Minigames.Minigame_4.ArtAssets4.Initialize(GraphicsDevice, Content);
            Minigames.Minigame_5.ArtAssets5.Initialize(GraphicsDevice, Content);
            Minigames.Minigame_1.SoundAssets1.Initialize(Content);
            Minigames.Minigame_2.SoundAssets2.Initialize(Content);
            Minigames.Minigame_3.SoundAssets3.Initialize(Content);
            Minigames.Minigame_4.SoundAssets4.Initialize(Content);
            Minigames.Minigame_5.SoundAssets5.Initialize(Content);
            Minigames.Minigame_1.ParticleAssets1.Initialize(GraphicsDevice, Content);
            Minigames.Minigame_3.ParticleAssets3.Initialize(GraphicsDevice, Content);
            Minigames.Minigame_5.ParticleAssets5.Initialize(GraphicsDevice, Content);

            graphics.PreferredBackBufferWidth = ArtAssets.WorldBackgrounds[ZoomLevels.One].Width;
            graphics.PreferredBackBufferHeight = ArtAssets.WorldBackgrounds[ZoomLevels.One].Height;
            Utilities.OtherFunctions.SetWindowCoords(new Point(0, 0), this, graphics);
            graphics.ApplyChanges();

            Utilities.OtherFunctions.SetWindowCoords(new Point(0, 0), this, graphics);

            world = new KarmaWorld(GraphicsDevice, Content);
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            if (current != States.Playing)
            {
                world.KS = Keyboard.GetState();
            }

            switch (current)
            {
                case States.Playing:
                    world.Update(gameTime);
                    if (world.KS.IsKeyDown(Keys.Escape))
                    {
                        if (!buttonDown)
                        {
                            current = States.Paused;
                            buttonDown = true;
                        }
                    }
                    else buttonDown = false;
                    if (world.HarmonyWon) current = States.HarmonyWin;
                    else if (world.DiscordWon) current = States.DiscordWin;

                    break;
                case States.Paused:
                    if (world.KS.IsKeyDown(Keys.Escape))
                    {
                        if (!buttonDown)
                        {
                            current = States.Playing;
                            buttonDown = true;
                        }
                    }
                    else buttonDown = false;
                    break;
                case States.MainMenu:
                    if (world.KS.IsKeyDown(Keys.Enter))
                    {
                        if (!buttonDown)
                        {
                            current = States.Playing;
                            buttonDown = true;
                        }
                    }
                    else if (world.KS.IsKeyDown(Keys.Escape))
                    {
                        if (!buttonDown)
                        {
                            Exit();
                            buttonDown = true;
                        }
                    }
                    else if (world.KS.IsKeyDown(Keys.Space))
                    {
                        if (!buttonDown)
                        {
                            current = States.Instructions;
                            buttonDown = true;
                        }
                    }
                    else buttonDown = false;
                    break;
                case States.Instructions:
                    if (world.KS.IsKeyDown(Keys.Escape))
                    {
                        if (!buttonDown)
                        {
                            current = States.MainMenu;
                            buttonDown = true;
                        }
                    }
                    else buttonDown = false;
                    break;
                case States.HarmonyWin:
                    if (world.KS.IsKeyDown(Keys.Enter))
                    {
                        if (!buttonDown)
                        {
                            current = States.MainMenu;
                            buttonDown = false;
                        }
                    }
                    else buttonDown = false;
                    break;
                case States.DiscordWin:
                    if (world.KS.IsKeyDown(Keys.Enter))
                    {
                        if (!buttonDown)
                        {
                            current = States.MainMenu;
                            buttonDown = false;
                        }
                    }
                    else buttonDown = false;
                    break;

                default: throw new NotImplementedException();
            }

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            switch (current)
            {
                case States.Playing:
                    world.Draw(gameTime, spriteBatch);
                    break;
                case States.Paused:

                    world.Draw(gameTime, spriteBatch);

                    spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.NonPremultiplied);
                    Utilities.Graphics.TexturePrimitiveDrawer.DrawRect(new Rectangle(0, 0, graphics.PreferredBackBufferWidth, graphics.PreferredBackBufferHeight), spriteBatch, new Color(0, 0, 0, 128), 1);
                    spriteBatch.DrawString(ArtAssets.WorldFont, "Paused", new Vector2(graphics.PreferredBackBufferWidth * 0.5f, graphics.PreferredBackBufferHeight * 0.5f), Color.White);
                    spriteBatch.End();
                    break;

                default: throw new NotImplementedException();
            }

            base.Draw(gameTime);
        }
    }
}