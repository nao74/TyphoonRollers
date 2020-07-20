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


namespace TyphoonRollers
{
    /// <summary>
    /// This is a game component that implements IUpdateable.
    /// </summary>
    public class TitleComponent : Microsoft.Xna.Framework.DrawableGameComponent
    {
        private SpriteBatch spriteBatch;

        private Texture2D titleTexture;
        private Texture2D titleLogoTexture;
        private Texture2D selectTexture;

        private SoundEffect up;
        private SoundEffect down;
        private SoundEffect select;

        private const int menuNum = 3;

        private Vector2[] position;

        public enum Menu
        {
            Start,
            Manual,
            Exit
        }

        Menu menu = Menu.Start;
        bool selected = false;

        public TitleComponent(Game1 game)
            : base(game)
        {
            // TODO: Construct any child components here
        }

        /// <summary>
        /// Allows the game component to perform any initialization it needs to before starting
        /// to run.  This is where it can query for any required services and load content.
        /// </summary>
        public override void Initialize()
        {
            // TODO: Add your initialization code here

            for (int i = 0; i < menuNum; i++)
            {
                position = new Vector2[menuNum];
            }

            position[0] = new Vector2(0.0f, 0.0f);
            position[1] = new Vector2(0.0f, 75.0f);
            position[2] = new Vector2(0.0f, 150.0f);

            base.Initialize();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);

            titleTexture = Game.Content.Load<Texture2D>("Texture/Title");
            titleLogoTexture = Game.Content.Load<Texture2D>("Texture/TitleLogo");
            selectTexture = Game.Content.Load<Texture2D>("Texture/Select");

            up = Game.Content.Load<SoundEffect>("Sound/selectTest4");
            down = Game.Content.Load<SoundEffect>("Sound/selectTest4");
            select = Game.Content.Load<SoundEffect>("Sound/selectTest");

            base.LoadContent();
        }

        /// <summary>
        /// Allows the game component to update itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        public override void Update(GameTime gameTime)
        {
            // TODO: Add your update code here

            if (selected)
                selected = false;

            if (InputManager.IsJustKeyDown(Keys.Up) || InputManager.IsJustButtonDown(PlayerIndex.One, Buttons.DPadUp))
            {
                if (Menu.Start < menu)
                    menu--;

                up.Play();
            }
            else if (InputManager.IsJustKeyDown(Keys.Down) || InputManager.IsJustButtonDown(PlayerIndex.One, Buttons.DPadDown))
            {
                if (menu < Menu.Exit)
                    menu++;

                down.Play();
            }
            else if (InputManager.IsJustKeyDown(Keys.Enter) || InputManager.IsJustButtonDown(PlayerIndex.One, Buttons.A))
            {
                select.Play();
                selected = true;
            }
            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            spriteBatch.Begin();

            spriteBatch.Draw(titleTexture, Vector2.Zero, Color.White);
            spriteBatch.Draw(titleLogoTexture, Vector2.Zero, Color.White);
            spriteBatch.Draw(selectTexture, position[(int)menu], Color.White);

            spriteBatch.End();

            base.Draw(gameTime);
        }

        public Menu selectedMenu
        {
            get { return menu; }
        }

        public bool IsSelected()
        {
            return selected;
        }
    }
}
