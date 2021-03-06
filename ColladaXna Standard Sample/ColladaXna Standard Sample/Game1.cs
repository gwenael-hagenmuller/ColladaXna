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
using SkinnedModel;

namespace ColladaXna_Standard_Sample
{
    /// <summary>
    /// This sample shows how to use the ColladaXna.Base Standard Importer
    /// to use COLLADA models with the XNA standard Model class.
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        SpriteFont font;

        float aspectRatio;
        Matrix world;
        Matrix view;
        Matrix projection;

        Vector3 pos;
        Vector3 rot;
        bool showHints = true;

        List<Model> models = new List<Model>();
        Dictionary<Model, AnimatedModel> animatedModels = new Dictionary<Model, AnimatedModel>();

        int selectedModel = 0;

        string[] modelPaths = { "APC/apc-model", "Marcus/marcus", "Spore/Bulldogtopus", "Igor/igor"
                 };

        protected Model CurrentModel
        {
            get { return models[selectedModel]; }
        }

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            
            graphics.PreferMultiSampling = true;
            //graphics.PreferredBackBufferWidth = 1600;
            //graphics.PreferredBackBufferHeight = 900;
            //graphics.IsFullScreen = true;
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {            
            Window.AllowUserResizing = true;            

            aspectRatio = (float)GraphicsDevice.Viewport.Width /
                (float)GraphicsDevice.Viewport.Height;

            projection = Matrix.CreatePerspectiveFieldOfView(MathHelper.PiOver4, aspectRatio,
                1.0f, 10000.0f);

            view = Matrix.CreateLookAt(new Vector3(0, 0, -10), Vector3.Zero, Vector3.Up);

            pos = new Vector3(0, -40, 100);
            rot = new Vector3(-MathHelper.PiOver2, 0, MathHelper.Pi - MathHelper.PiOver4);

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

            // Load all specified models
            foreach (string path in modelPaths)
            {
                Model model = Content.Load<Model>(path);
                models.Add(model);

                // Distinguish between animated and static models
                if (model.Tag is SkinningData)
                {
                    // Store wrapper instance for animated model
                    animatedModels.Add(model, new AnimatedModel(model));

                    // Adjust shader parameters
                    foreach (var mesh in model.Meshes)
                    {
                        foreach (SkinnedEffect effect in mesh.Effects)
                        {
                            effect.EnableDefaultLighting();
                            effect.PreferPerPixelLighting = true;
                        }
                    }

                    // Start default animation clip
                    animatedModels[model].PlayFirstClip();
                }
                else
                {
                    // Adjust shader parameters
                    foreach (ModelMesh mesh in model.Meshes)
                    {
                        foreach (BasicEffect effect in mesh.Effects)
                        {
                            effect.EnableDefaultLighting();
                            effect.TextureEnabled = true;
                            effect.PreferPerPixelLighting = true;
                        }
                    }
                }
            }

            font = Content.Load<SpriteFont>("Segoe UI Mono");
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
            // Allows the game to exit
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed ||
                Keyboard.GetState().IsKeyDown(Keys.Escape))
                this.Exit();

            var keyboard = Keyboard.GetState();

            if (keyboard.IsKeyDown(Keys.Down))
                rot.X -= 0.015f;
            else if (keyboard.IsKeyDown(Keys.Up))
                rot.X += 0.015f;

            if (keyboard.IsKeyDown(Keys.Left))
                rot.Y += 0.015f;
            else if (keyboard.IsKeyDown(Keys.Right))
                rot.Y -= 0.020f;

            if (keyboard.IsKeyDown(Keys.S))
                pos.Z -= 1f;
            else if (keyboard.IsKeyDown(Keys.W))
                pos.Z += 1f;

            if (keyboard.IsKeyDown(Keys.A))
                pos.X += 1f;
            else if (keyboard.IsKeyDown(Keys.D))
                pos.X -= 1f;

            if (keyboard.IsKeyDown(Keys.PageUp))
                pos.Y += 1f;
            else if (keyboard.IsKeyDown(Keys.PageDown))
                pos.Y -= 1f;

            if (keyboard.IsKeyDown(Keys.F1))
                showHints = !showHints;

            for (int i = 0; i < 9; i++)
            {
                if (keyboard.IsKeyDown(Keys.D1 + i))
                {
                    if (models.Count > i)
                    {
                        selectedModel = i;
                        break;
                    }
                }
            }

            // Update animation
            if (animatedModels.ContainsKey(CurrentModel))
            {
                animatedModels[CurrentModel].Update(gameTime);
            }

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(new Color(240, 240, 240));

            GraphicsDevice.BlendState = BlendState.Opaque;
            GraphicsDevice.DepthStencilState = DepthStencilState.Default;
            GraphicsDevice.RasterizerState = RasterizerState.CullCounterClockwise;
            GraphicsDevice.SamplerStates[0] = SamplerState.LinearWrap;

            world = Matrix.CreateFromYawPitchRoll(rot.Y, rot.X, rot.Z) * Matrix.CreateTranslation(pos);

            if (animatedModels.ContainsKey(CurrentModel))
            {
                // Draw animated model 
                animatedModels[CurrentModel].Draw(world, view, projection);
            }
            else
            {
                // Draw static model
                models[selectedModel].Draw(world, view, projection);
            }

            if (showHints)
            {
                spriteBatch.Begin();

                spriteBatch.DrawString(font, "WASD - Move X/Z\nArrows - Rotate\nPgUp/Dn - Move Y\nDigits - Choose Model",
                    new Vector2(25, 25), Color.Black);

                spriteBatch.End();
            }

            base.Draw(gameTime);
        }
    }
}
