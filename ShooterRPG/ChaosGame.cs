namespace ShooterRPG
{
    using System;
    using System.Collections.Generic;

    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Graphics;
    using Microsoft.Xna.Framework.Input;

    using ShooterRPG.Content;
    using ShooterRPG.Enemies;
    using ShooterRPG.Heroes;
    using ShooterRPG.Projectiles;

    public class ChaosGame : Game
    {
        private static ChaosGame instance;

        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        Rectangle screen;
        Texture2D startScreen;
        Texture2D endScreen;
        Texture2D background;

        static bool mainMenu = true;
        public static bool gameOverMenu = false;
        public static int heroChoice;
        public static List<Projectile> Projectiles = new List<Projectile>();

        Texture2D mageButtonActive;
        Rectangle mageButtonRect;

        Texture2D archerButtonActive;
        Rectangle archerButtonRect;

        public static ChaosGame Instance
        {
            get
            {
                if(instance == null)
                {
                    instance = new ChaosGame();
                }

                return instance;
            }
        }

        private ChaosGame()
            : base()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            graphics.IsFullScreen = false;
            graphics.PreferredBackBufferHeight = 600;
            graphics.PreferredBackBufferWidth = 800;

            graphics.ApplyChanges();
        }

        protected override void Initialize()
        {
            ListOfDrawableObjects.Initialize();
            base.Initialize();
        }

        protected override void LoadContent()
        {
            try 
            {
                spriteBatch = new SpriteBatch(GraphicsDevice);

                mageButtonActive = Content.Load<Texture2D>(Sprites.MageButtonHover.ToString());
                mageButtonRect = new Rectangle(545, 450, 100, 100);
                archerButtonActive = Content.Load<Texture2D>(Sprites.ArcherButton.ToString());
                archerButtonRect = new Rectangle(655, 450, 100, 100);

                foreach (var item in ListOfDrawableObjects.AllDrawableItems)
                {
                    item.LoadContent(this.Content, item.SpriteName);
                }

                foreach (var item in ListOfDrawableObjects.AllEnemies)
                {
                    item.LoadContent(this.Content, item.SpriteName);
                }

                screen = new Rectangle(0, 0, GraphicsDevice.Viewport.Width, GraphicsDevice.Viewport.Height);

                background = this.Content.Load<Texture2D>(Sprites.Map.ToString());
                startScreen = this.Content.Load<Texture2D>(Sprites.StartOfGame.ToString());
                endScreen = this.Content.Load<Texture2D>(Sprites.EndOfGame.ToString());
            }
            catch(Exception e)
            {
                throw new ContentLoadException(e.Message);
            }
        }        

        protected override void UnloadContent()
        {

        }

        protected override void Update(GameTime gameTime)
        {
             KeyboardState keysForMenus = Keyboard.GetState();
             bool mouseOverMageButton = mageButtonRect.Contains(Mouse.GetState().X, Mouse.GetState().Y);

            if (mainMenu == true)
            {
                if (keysForMenus.IsKeyDown(Keys.D1))
                {
                    ListOfDrawableObjects.CurrentHero = new Mage(Sprites.Mage.ToString());
                    
                    try
                    {
                        ListOfDrawableObjects.CurrentHero.LoadContent(this.Content, ListOfDrawableObjects.CurrentHero.SpriteName);
                    }
                    catch (Exception e)
                    {
                        throw new ContentLoadException(e.Message);
                    }

                    mainMenu = false;
                   
                }
                else if (keysForMenus.IsKeyDown(Keys.D2))
                {
                    ListOfDrawableObjects.CurrentHero = new Archer(Sprites.Archer.ToString());
                    
                    try
                    {
                        ListOfDrawableObjects.CurrentHero.LoadContent(this.Content, ListOfDrawableObjects.CurrentHero.SpriteName);
                    }
                    catch (Exception e)
                    {
                        throw new ContentLoadException(e.Message);
                    }
                    
                    mainMenu = false;
                }
            }

            if (mainMenu == false && gameOverMenu == false)
            {
                foreach (var item in ListOfDrawableObjects.AllDrawableItems)
                {
                    item.Update();
                }

                foreach(Enemy enemy in ListOfDrawableObjects.AllEnemies)
                {
                    enemy.Update(ListOfDrawableObjects.CurrentHero);
                }

                ListOfDrawableObjects.CurrentHero.Update();
            }

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            spriteBatch.Begin();
            if (mainMenu == true)
            {
                spriteBatch.Draw(startScreen, screen, Color.AliceBlue);
                
                spriteBatch.Draw(mageButtonActive, mageButtonRect, Color.White);
                spriteBatch.Draw(archerButtonActive, archerButtonRect, Color.White);
               
            }

            if (mainMenu == false && gameOverMenu == false)
            {
                spriteBatch.Draw(background, screen, Color.White);
                foreach (var obj in ListOfDrawableObjects.AllDrawableItems)
                {
                    obj.Draw(spriteBatch);
                }
                
                foreach (var obj in ListOfDrawableObjects.AllEnemies)
                {
                    obj.Draw(spriteBatch);
                }

                ListOfDrawableObjects.CurrentHero.Draw(spriteBatch);
            }

            if (gameOverMenu == true)
            {
                spriteBatch.Draw(endScreen, screen, Color.AliceBlue);
            }

            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}