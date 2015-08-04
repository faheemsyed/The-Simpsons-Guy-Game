
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

namespace WindowsGame9
{
    public class Game1 : Microsoft.Xna.Framework.Game
    {

        #region Sounds
        Song BGM;
        SoundEffect tornadoSE;
        SoundEffect gunShotSE;
        SoundEffect pumpkinCollideSE;
        SoundEffect chickenCollideSE;
        SoundEffect popSE;
        SoundEffect HomerSE;
        SoundEffect PeterSE;
        SoundEffect bossDieExplosionSE;
        #endregion

        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        KeyboardState previousKeyboardState = Keyboard.GetState();
        Random r = new Random();
        int nextValue;
        Random BossRand;
        int BossInt;
        int loopInt;
        #region Screen
        enum GameState { MAIN, CONTROLS, PLAYING, UPGRADE, END };
        GameState Gamestate;
        GameObject Main;
        GameObject Control;
        GameObject End;
        GameObject Upgrade;
        Scrolling scrolling1;
        Scrolling scrolling2;
        Scrolling scrolling3;
        #endregion
        #region Players
        Player Homer1;
        Player2 Peter1;
        #endregion
        #region Enemies
        int totalNumOfPumpkins;
        GameObject[] pumpkin;
        GameObject ExplodeSprite;
        int totalNumOfEggCrackStages;
        int EggCrackStage;
        GameObject[] BossEgg;
        GiantChicken Boss;
        int totalNumOfWhiteChickens;
        WhiteChicken[] whiteChicken;
        #endregion
        

        Texture2D winMessage1;
        Texture2D winMessage2;
        Texture2D winMessage3;
        Texture2D loseMessage;

        Texture2D whiteTexture2D;   //used to draw collision rectangle
        Texture2D greenTexture2D;   //used to draw collision rectangle


        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            this.graphics.IsFullScreen = false;
            graphics.PreferredBackBufferWidth = 1280;
            graphics.PreferredBackBufferHeight = 720;
            Content.RootDirectory = "Content";
        }

        protected override void Initialize()
        {

            base.Initialize();
        }


        protected override void LoadContent()
        {
            BGM = Content.Load<Song>("Super Mario World Overworld");
            MediaPlayer.Play(BGM);
            MediaPlayer.IsRepeating = true;
            tornadoSE = Content.Load<SoundEffect>("TornadoSE");
            gunShotSE = Content.Load<SoundEffect>("GunShotSE");
            pumpkinCollideSE = Content.Load<SoundEffect>("pumpkinCollideSE");
            chickenCollideSE = Content.Load<SoundEffect>("chickenCollideSE");
            popSE = Content.Load<SoundEffect>("popSE");
            HomerSE = Content.Load<SoundEffect>("HomerSE");
            PeterSE = Content.Load<SoundEffect>("PeterSE");
            bossDieExplosionSE = Content.Load<SoundEffect>("bossDieExplosion");
            
            spriteBatch = new SpriteBatch(GraphicsDevice);

            whiteTexture2D = Content.Load<Texture2D>("whitePic");   //used to draw collision rectangle
            greenTexture2D = Content.Load<Texture2D>("greenPic");   //used to draw collision rectangle

            winMessage1 = Content.Load<Texture2D>("WinMessage1");
            winMessage2 = Content.Load<Texture2D>("WinMessage2");
            winMessage3 = Content.Load<Texture2D>("WinMessage3");
            loseMessage = Content.Load<Texture2D>("LoseMessage");

            #region Menu && Screens
            Gamestate = GameState.MAIN;


            Main = new GameObject(Content.Load<Texture2D>("MainMenu"), graphics);
            Main.Scale = 1.0f;
            Main.position = new Vector2(640, 360);
            Main.visible = false;

            Control = new GameObject(Content.Load<Texture2D>("Controls"), graphics);
            Control.Scale = 1.0f;
            Control.position = new Vector2(640, 360);
            Control.visible = false;

            End = new GameObject(Content.Load<Texture2D>("End Screen"), graphics);
            End.Scale = 1.0f;
            End.position = new Vector2(640, 360);
            End.visible = false;

            Upgrade = new GameObject(Content.Load<Texture2D>("UpgradeScreen"), graphics);
            Upgrade.Scale = 0.75f;
            Upgrade.position = new Vector2(640, 350);
            Upgrade.visible = false;
            #endregion
            #region Scrolling
            scrolling1 = new Scrolling(Content.Load<Texture2D>("Springfield1"), new Rectangle(0, 0, 1000, 720));
            scrolling2 = new Scrolling(Content.Load<Texture2D>("Springfield2"), new Rectangle(1000, 0, 1000, 720));
            scrolling3 = new Scrolling(Content.Load<Texture2D>("Springfield3"), new Rectangle(2000, 0, 1000, 720));
            #endregion
            #region Players
            Homer1 = new Player(Content.Load<Texture2D>("Homer"), Content.Load<Texture2D>("Bullet"), Content.Load<SpriteFont>("SpriteFont1"), graphics);
            Homer1.bulletScale = 0.75f;
            Peter1 = new Player2(Content.Load<Texture2D>("PeterSpriteSheetFull"), Content.Load<Texture2D>("Bullet"), Content.Load<SpriteFont>("SpriteFont1"), graphics);
            Peter1.bulletScale = 0.75f;
            #endregion
            #region Enemies
            totalNumOfPumpkins = 10;
            pumpkin = new GameObject[totalNumOfPumpkins];
            for (int i = 0; i < totalNumOfPumpkins; i++)
            {
                pumpkin[i] = new GameObject(Content.Load<Texture2D>("Pumpkin"), graphics);
                pumpkin[i].Scale = 0.35f;
                pumpkin[i].position = new Vector2(nextValue = r.Next(800, 2950), nextValue = r.Next(425, 700));
                pumpkin[i].visible = true;
                pumpkin[i].speed = 2f;
            }

            totalNumOfWhiteChickens = 10;
            whiteChicken = new WhiteChicken[totalNumOfWhiteChickens];
            for (int i = 0; i < totalNumOfWhiteChickens; i++)
            {
                whiteChicken[i] = new WhiteChicken(Content.Load<Texture2D>("WhiteChicken"), Content.Load<Texture2D>("HealthBar2"), graphics);
                whiteChicken[i].Scale = 1f;
                whiteChicken[i].position.X = (r.Next(800, 2950));
                whiteChicken[i].position.Y = (r.Next(275, 550));
                whiteChicken[i].spriteVisible = true;
                //whiteChicken[i].speed = 2f;   //by default speed is 2f in WhiteChicken.cs
            }

            ExplodeSprite = new GameObject(Content.Load<Texture2D>("Explode"), graphics);
            ExplodeSprite.visible = false;
            ExplodeSprite.Scale = 0.75f;
            ExplodeSprite.position = new Vector2();

            totalNumOfEggCrackStages = 3;
            BossEgg = new GameObject[totalNumOfEggCrackStages];
            for (int i = 0; i < totalNumOfEggCrackStages; i++)
            {
                BossEgg[i] = new GameObject(Content.Load<Texture2D>("Egg" + i), graphics);
                BossEgg[i].Scale = 1.5f;
                BossEgg[i].position = new Vector2(2850, 550);
                BossEgg[i].visible = false;
                BossEgg[i].speed = 2f;
            }
            EggCrackStage = 0; //will start at stage 0 and cause BossEgg[0].visible = true;

            loopInt = 0;
            Boss = new GiantChicken(Content.Load<Texture2D>("GiantChicken"), Content.Load<Texture2D>("Tornado"), Content.Load<Texture2D>("HealthBar2"), graphics);
            Boss.position.X = 2850;
            Boss.position.Y = 400;
            Boss.spriteVisible = false;

            #endregion
        }


        protected override void UnloadContent()
        {
        }


        protected override void Update(GameTime gameTime)
        {

            #region keys to Exit game
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                this.Exit();

            if (Keyboard.GetState().IsKeyDown(Keys.Escape))
                this.Exit();
            #endregion

            switch (Gamestate)
            {
                #region MAIN
                case GameState.MAIN:
                    Main.visible = true;
                    if (Keyboard.GetState().IsKeyDown(Keys.Space) && previousKeyboardState.IsKeyUp(Keys.Space))
                    {
                        Gamestate = GameState.CONTROLS;
                        Main.visible = false;
                        Control.visible = true;
                    }
                    break;
                #endregion
                #region CONTROLS
                case GameState.CONTROLS:
                    if (Keyboard.GetState().IsKeyDown(Keys.Space) && previousKeyboardState.IsKeyUp(Keys.Space))
                    {
                        Gamestate = GameState.PLAYING;
                        Control.visible = false;
                    }
                    break;
                #endregion
                #region PLAYING
                case GameState.PLAYING:

                    /* THIS IS WHERE ALL THE PLAYING CODE WILL GO !!!!!
                     * //
                     * //
                     * ///
                     * //
                     * */

                    #region Scrolling
                    #region Scrolling Forward
                    if (scrolling3.rectangle.X > 280)
                    {
                        //SpriteEffects.None == player is facing right
                        //SpriteEffects.FlipHorizontally == player is facing left
                        if (Homer1.spriteState != Player.playerState.DEAD && Peter1.spriteState != Player2.playerState.DEAD)
                        {
                            if ((Homer1.position.X >= 550 && Peter1.position.X >= 70) &&
                                (((Homer1.spriteState == Player.playerState.WALKING && Homer1.spriteEffects == SpriteEffects.None) && Peter1.spriteState == Player2.playerState.STANDING) ||
                                    (Peter1.spriteEffects == SpriteEffects.None && Homer1.spriteEffects == SpriteEffects.None)))
                            {
                                Homer1.position.X -= 2;
                                Peter1.position.X -= 2;
                                for (int i = 0; i < totalNumOfPumpkins; i++)
                                {
                                    pumpkin[i].position.X -= 2;
                                }
                                for (int i = 0; i < totalNumOfWhiteChickens; i++)
                                {
                                    whiteChicken[i].position.X -= 2;
                                }
                                for (int i = 0; i < totalNumOfEggCrackStages; i++)
                                {
                                    BossEgg[i].position.X -= 2;
                                }
                                Boss.position.X -= 2;
                                scrolling1.ScrollForward();
                                scrolling2.ScrollForward();
                                scrolling3.ScrollForward();
                            }
                            else if ((Peter1.position.X >= 550 && Homer1.position.X >= 70) &&
                                (((Peter1.spriteState == Player2.playerState.WALKING && Peter1.spriteEffects == SpriteEffects.None) && Homer1.spriteState == Player.playerState.STANDING) ||
                                    (Homer1.spriteEffects == SpriteEffects.None && Peter1.spriteEffects == SpriteEffects.None)))
                            {
                                Homer1.position.X -= 2;
                                Peter1.position.X -= 2;
                                for (int i = 0; i < totalNumOfPumpkins; i++)
                                {
                                    pumpkin[i].position.X -= 2;
                                }
                                for (int i = 0; i < totalNumOfWhiteChickens; i++)
                                {
                                    whiteChicken[i].position.X -= 2;
                                }
                                for (int i = 0; i < totalNumOfEggCrackStages; i++)
                                {
                                    BossEgg[i].position.X -= 2;
                                }
                                Boss.position.X -= 2;
                                scrolling1.ScrollForward();
                                scrolling2.ScrollForward();
                                scrolling3.ScrollForward();
                            }
                        }
                        else if (Homer1.spriteState == Player.playerState.DEAD || Peter1.spriteState == Player2.playerState.DEAD)
                        {
                            if (Homer1.spriteState == Player.playerState.DEAD && Peter1.spriteState == Player2.playerState.DEAD)
                            { 
                                //background won't scroll 
                            }
                            if (Peter1.spriteState == Player2.playerState.DEAD && Homer1.spriteState != Player.playerState.DEAD)
                            {
                                if (Homer1.position.X >= 550)
                                {
                                    Homer1.position.X -= 2;
                                    Peter1.position.X -= 2;
                                    for (int i = 0; i < totalNumOfPumpkins; i++)
                                    {
                                        pumpkin[i].position.X -= 2;
                                    }
                                    for (int i = 0; i < totalNumOfWhiteChickens; i++)
                                    {
                                        whiteChicken[i].position.X -= 2;
                                    }
                                    for (int i = 0; i < totalNumOfEggCrackStages; i++)
                                    {
                                        BossEgg[i].position.X -= 2;
                                    }
                                    Boss.position.X -= 2;
                                    scrolling1.ScrollForward();
                                    scrolling2.ScrollForward();
                                    scrolling3.ScrollForward();
                                }
                            }
                            else if (Homer1.spriteState == Player.playerState.DEAD && Peter1.spriteState != Player2.playerState.DEAD)
                            {
                                if (Peter1.position.X >= 550)
                                {
                                    Homer1.position.X -= 2;
                                    Peter1.position.X -= 2;
                                    for (int i = 0; i < totalNumOfPumpkins; i++)
                                    {
                                        pumpkin[i].position.X -= 2;
                                    }
                                    for (int i = 0; i < totalNumOfWhiteChickens; i++)
                                    {
                                        whiteChicken[i].position.X -= 2;
                                    }
                                    for (int i = 0; i < totalNumOfEggCrackStages; i++)
                                    {
                                        BossEgg[i].position.X -= 2;
                                    }
                                    Boss.position.X -= 2;
                                    scrolling1.ScrollForward();
                                    scrolling2.ScrollForward();
                                    scrolling3.ScrollForward();
                                }
                            }
                        }
                    }
                    #endregion
                    #region Scrolling Backward
                    if (scrolling1.rectangle.X < 0)
                    {
                        //SpriteEffects.None == player is facing right
                        //SpriteEffects.FlipHorizontally == player is facing left
                        if (Homer1.spriteState != Player.playerState.DEAD && Peter1.spriteState != Player2.playerState.DEAD)
                        {
                            if ((Homer1.position.X < 550 && Peter1.position.X < 1200) &&
                                (((Homer1.spriteState == Player.playerState.WALKING && Homer1.spriteEffects == SpriteEffects.FlipHorizontally) && Peter1.spriteState == Player2.playerState.STANDING) ||
                                    (Peter1.spriteEffects == SpriteEffects.FlipHorizontally && Homer1.spriteEffects == SpriteEffects.FlipHorizontally)))
                            {
                                Homer1.position.X += 2;
                                Peter1.position.X += 2;
                                for (int i = 0; i < totalNumOfPumpkins; i++)
                                {
                                    pumpkin[i].position.X += 2;
                                }
                                for (int i = 0; i < totalNumOfWhiteChickens; i++)
                                {
                                    whiteChicken[i].position.X += 2;
                                }
                                for (int i = 0; i < totalNumOfEggCrackStages; i++)
                                {
                                    BossEgg[i].position.X += 2;
                                }
                                Boss.position.X += 2;
                                scrolling1.ScrollBack();
                                scrolling2.ScrollBack();
                                scrolling3.ScrollBack();
                            }
                            else if ((Peter1.position.X < 550 && Homer1.position.X < 1200) &&
                                (((Peter1.spriteState == Player2.playerState.WALKING && Peter1.spriteEffects == SpriteEffects.FlipHorizontally) && Homer1.spriteState == Player.playerState.STANDING) ||
                                    (Homer1.spriteEffects == SpriteEffects.FlipHorizontally && Peter1.spriteEffects == SpriteEffects.FlipHorizontally)))
                            {
                                Homer1.position.X += 2;
                                Peter1.position.X += 2;
                                for (int i = 0; i < totalNumOfPumpkins; i++)
                                {
                                    pumpkin[i].position.X += 2;
                                }
                                for (int i = 0; i < totalNumOfWhiteChickens; i++)
                                {
                                    whiteChicken[i].position.X += 2;
                                }
                                for (int i = 0; i < totalNumOfEggCrackStages; i++)
                                {
                                    BossEgg[i].position.X += 2;
                                }
                                Boss.position.X += 2;
                                scrolling1.ScrollBack();
                                scrolling2.ScrollBack();
                                scrolling3.ScrollBack();
                            }
                        }
                        else if (Homer1.spriteState == Player.playerState.DEAD || Peter1.spriteState == Player2.playerState.DEAD)
                        {
                            if (Homer1.spriteState == Player.playerState.DEAD && Peter1.spriteState == Player2.playerState.DEAD)
                            {
                                //background won't scroll
                            }
                            if (Peter1.spriteState == Player2.playerState.DEAD && Homer1.spriteState != Player.playerState.DEAD)
                            {
                                if (Homer1.position.X < 550)
                                {
                                    Homer1.position.X += 2;
                                    Peter1.position.X += 2;
                                    for (int i = 0; i < totalNumOfPumpkins; i++)
                                    {
                                        pumpkin[i].position.X += 2;
                                    }
                                    for (int i = 0; i < totalNumOfWhiteChickens; i++)
                                    {
                                        whiteChicken[i].position.X += 2;
                                    }
                                    for (int i = 0; i < totalNumOfEggCrackStages; i++)
                                    {
                                        BossEgg[i].position.X += 2;
                                    }
                                    Boss.position.X += 2;
                                    scrolling1.ScrollBack();
                                    scrolling2.ScrollBack();
                                    scrolling3.ScrollBack();
                                }
                            }
                            else if (Homer1.spriteState == Player.playerState.DEAD && Peter1.spriteState != Player2.playerState.DEAD)
                            {
                                if (Peter1.position.X < 550)
                                {
                                    Homer1.position.X += 2;
                                    Peter1.position.X += 2;
                                    for (int i = 0; i < totalNumOfPumpkins; i++)
                                    {
                                        pumpkin[i].position.X += 2;
                                    }
                                    for (int i = 0; i < totalNumOfWhiteChickens; i++)
                                    {
                                        whiteChicken[i].position.X += 2;
                                    }
                                    for (int i = 0; i < totalNumOfEggCrackStages; i++)
                                    {
                                        BossEgg[i].position.X += 2;
                                    }
                                    Boss.position.X += 2;
                                    scrolling1.ScrollBack();
                                    scrolling2.ScrollBack();
                                    scrolling3.ScrollBack();
                                }
                            }
                        }
                    }


                    #endregion
                    #endregion

                    #region Players
                    #region Homer
                    #region keys for player1
                    if (Keyboard.GetState().IsKeyDown(Keys.Left) &&
                        Homer1.spriteState != Player.playerState.FIRING &&
                        Homer1.spriteState != Player.playerState.HURT &&
                        Homer1.spriteState != Player.playerState.DEAD)
                    {
                        Homer1.spriteState = Player.playerState.WALKING;
                        Homer1.position.X -= Homer.walking.speed;
                        Homer1.spriteEffects = SpriteEffects.FlipHorizontally;
                    }
                    if (Keyboard.GetState().IsKeyDown(Keys.Right) &&
                        Homer1.spriteState != Player.playerState.FIRING &&
                        Homer1.spriteState != Player.playerState.HURT &&
                        Homer1.spriteState != Player.playerState.DEAD)
                    {
                        Homer1.spriteState = Player.playerState.WALKING;
                        Homer1.position.X += Homer.walking.speed;
                        Homer1.spriteEffects = SpriteEffects.None;
                    }

                    if (Keyboard.GetState().IsKeyDown(Keys.Up) &&
                        Homer1.spriteState != Player.playerState.FIRING &&
                        Homer1.spriteState != Player.playerState.HURT &&
                        Homer1.spriteState != Player.playerState.DEAD)
                    {
                        Homer1.spriteState = Player.playerState.WALKING;
                        Homer1.position.Y -= Homer.walking.speed;
                    }
                    if (Keyboard.GetState().IsKeyDown(Keys.Down) &&
                        Homer1.spriteState != Player.playerState.FIRING &&
                        Homer1.spriteState != Player.playerState.HURT &&
                        Homer1.spriteState != Player.playerState.DEAD)
                    {
                        Homer1.spriteState = Player.playerState.WALKING;
                        Homer1.position.Y += Homer.walking.speed;
                    }


                    if (Keyboard.GetState().IsKeyDown(Keys.OemComma) &&
                        Homer1.spriteState != Player.playerState.FIRING &&
                        Homer1.spriteState != Player.playerState.HURT &&
                        Homer1.spriteState != Player.playerState.DEAD &&
                        !Homer1.bulletVisible)
                    {
                        Homer1.spriteState = Player.playerState.FIRING;
                        Homer1.bulletVisible = true;
                        gunShotSE.Play();
                        if (Homer1.spriteEffects == SpriteEffects.None)
                        {
                            Homer1.bulletPos.X = Homer1.position.X + 100;
                            Homer1.bulletPos.Y = Homer1.position.Y + 64;

                            Homer1.BulletspriteEffects = SpriteEffects.None;
                        }
                        if (Homer1.spriteEffects == SpriteEffects.FlipHorizontally)
                        {
                            Homer1.bulletPos.X = Homer1.position.X;
                            Homer1.bulletPos.Y = Homer1.position.Y + 64;
                            Homer1.BulletspriteEffects = SpriteEffects.FlipHorizontally;
                        }
                    }


                    if (Keyboard.GetState().IsKeyDown(Keys.OemPeriod) && previousKeyboardState.IsKeyUp(Keys.OemPeriod))
                    {
                        Gamestate = GameState.UPGRADE;
                        Upgrade.visible = true;
                    }


                    if (!(Keyboard.GetState().IsKeyDown(Keys.Left)) && !(Keyboard.GetState().IsKeyDown(Keys.Right)) &&
                        !(Keyboard.GetState().IsKeyDown(Keys.Up)) && !(Keyboard.GetState().IsKeyDown(Keys.Down)) &&
                        Homer1.spriteState != Player.playerState.FIRING &&
                        Homer1.spriteState != Player.playerState.HURT &&
                        Homer1.spriteState != Player.playerState.DEAD)
                    {
                        Homer1.spriteState = Player.playerState.STANDING;
                    }
                    #endregion
                    Homer1.Frames(gameTime);
                    Homer1.rectangles();
                    Homer1.clamp();
                    Homer1.Bullet();
                    Homer1.setCollisionPosition();
                    if (Homer1.PlayerHealth <= 0)
                        Homer1.spriteState = Player.playerState.DEAD;
                    #endregion
                    #region Peter
                    Peter1.Frames(gameTime);
                    Peter1.rectangles();
                    Peter1.clamp();
                    Peter1.Bullet();
                    Peter1.setCollisionPosition();
                    if (Peter1.PlayerHealth <= 0)
                        Peter1.spriteState = Player2.playerState.DEAD;
                    #region keys for player2
                    if (Keyboard.GetState().IsKeyDown(Keys.A) &&
                        Peter1.spriteState != Player2.playerState.FIRING &&
                        Peter1.spriteState != Player2.playerState.HURT &&
                        Peter1.spriteState != Player2.playerState.DEAD)
                    {
                        Peter1.spriteState = Player2.playerState.WALKING;
                        Peter1.position.X -= Peter.walking.speed;
                        Peter1.spriteEffects = SpriteEffects.FlipHorizontally;
                    }
                    if (Keyboard.GetState().IsKeyDown(Keys.D) &&
                        Peter1.spriteState != Player2.playerState.FIRING &&
                        Peter1.spriteState != Player2.playerState.HURT &&
                        Peter1.spriteState != Player2.playerState.DEAD)
                    {
                        Peter1.spriteState = Player2.playerState.WALKING;
                        Peter1.position.X += Peter.walking.speed;
                        Peter1.spriteEffects = SpriteEffects.None;
                    }

                    if (Keyboard.GetState().IsKeyDown(Keys.W) &&
                        Peter1.spriteState != Player2.playerState.FIRING &&
                        Peter1.spriteState != Player2.playerState.HURT &&
                        Peter1.spriteState != Player2.playerState.DEAD)
                    {
                        Peter1.spriteState = Player2.playerState.WALKING;
                        Peter1.position.Y -= Peter.walking.speed;
                    }
                    if (Keyboard.GetState().IsKeyDown(Keys.S) &&
                        Peter1.spriteState != Player2.playerState.FIRING &&
                        Peter1.spriteState != Player2.playerState.HURT &&
                        Peter1.spriteState != Player2.playerState.DEAD)
                    {
                        Peter1.spriteState = Player2.playerState.WALKING;
                        Peter1.position.Y += Peter.walking.speed;
                    }

                    if (Keyboard.GetState().IsKeyDown(Keys.E) &&
                        Peter1.spriteState != Player2.playerState.FIRING &&
                        Peter1.spriteState != Player2.playerState.HURT &&
                        Peter1.spriteState != Player2.playerState.DEAD &&
                        !Peter1.bulletVisible)
                    {
                        Peter1.spriteState = Player2.playerState.FIRING;
                        Peter1.bulletVisible = true;
                        gunShotSE.Play();
                        if (Peter1.spriteEffects == SpriteEffects.None)
                        {
                            Peter1.bulletPos.X = Peter1.position.X + 100;
                            Peter1.bulletPos.Y = Peter1.position.Y + 50;

                            Peter1.BulletspriteEffects = SpriteEffects.None;
                        }
                        if (Peter1.spriteEffects == SpriteEffects.FlipHorizontally)
                        {
                            Peter1.bulletPos.X = Peter1.position.X;
                            Peter1.bulletPos.Y = Peter1.position.Y + 50;
                            Peter1.BulletspriteEffects = SpriteEffects.FlipHorizontally;
                        }
                    }


                    if (Keyboard.GetState().IsKeyDown(Keys.Q) && previousKeyboardState.IsKeyUp(Keys.Q))
                    {
                        Gamestate = GameState.UPGRADE;
                        Upgrade.visible = true;
                    }


                    if (!(Keyboard.GetState().IsKeyDown(Keys.A)) && !(Keyboard.GetState().IsKeyDown(Keys.D)) &&
                        !(Keyboard.GetState().IsKeyDown(Keys.W)) && !(Keyboard.GetState().IsKeyDown(Keys.S)) &&
                        Peter1.spriteState != Player2.playerState.FIRING &&
                        Peter1.spriteState != Player2.playerState.HURT &&
                        Peter1.spriteState != Player2.playerState.DEAD)
                    {
                        Peter1.spriteState = Player2.playerState.STANDING;
                    }
                    #endregion

                    #endregion
                    #endregion

                    #region Enemies
                    #region BossEgg
                    BossEggCollision();
                    if (EggCrackStage >= 0 && EggCrackStage <= 2)
                        BossEgg[EggCrackStage].visible = true;
                    if (EggCrackStage >= 1 && EggCrackStage <= 3)
                        BossEgg[EggCrackStage - 1].visible = false;
                    if (EggCrackStage == 3)
                    {
                        Boss.spriteVisible = true;
                        EggCrackStage = 4; //so that "Boss.spriteVisible = true" will only trigger once
                    }
                    #endregion
                    #region Boss
                    #region KEYSSS

                    //if (Keyboard.GetState().IsKeyDown(Keys.NumPad8) &&
                    //   Boss.spriteState != GiantChicken.playerState.firing &&
                    //   Boss.spriteState != GiantChicken.playerState.hurt &&
                    //   Boss.spriteState != GiantChicken.playerState.dead)
                    //{
                    //    Boss.spriteState = GiantChicken.playerState.walking;
                    //    Boss.position.Y -= GiantChickenMeasurements.walking.speed;
                    //}
                    //if (Keyboard.GetState().IsKeyDown(Keys.NumPad2) &&
                    //    Boss.spriteState != GiantChicken.playerState.firing &&
                    //    Boss.spriteState != GiantChicken.playerState.hurt &&
                    //    Boss.spriteState != GiantChicken.playerState.dead)
                    //{
                    //    Boss.spriteState = GiantChicken.playerState.walking;
                    //    Boss.position.Y += GiantChickenMeasurements.walking.speed;
                    //}


                    //if (Keyboard.GetState().IsKeyDown(Keys.NumPad0) &&
                    //    Boss.spriteState != GiantChicken.playerState.firing &&
                    //    Boss.spriteState != GiantChicken.playerState.hurt &&
                    //    Boss.spriteState != GiantChicken.playerState.dead &&
                    //    !Boss.bulletVisible)
                    //{
                    //    Boss.spriteState = GiantChicken.playerState.firing;
                    //    Boss.bulletVisible = true;
                    //    if (Boss.spriteEffects == SpriteEffects.None)
                    //    {
                    //        Boss.bulletPos.X = Boss.position.X + 100;
                    //        Boss.bulletPos.Y = Boss.position.Y + 64;

                    //        Boss.bulletSpriteEffects = SpriteEffects.None;
                    //    }
                    //    if (Boss.spriteEffects == SpriteEffects.FlipHorizontally)
                    //    {
                    //        Boss.bulletPos.X = Boss.position.X;
                    //        Boss.bulletPos.Y = Boss.position.Y + 64;
                    //        Boss.bulletSpriteEffects = SpriteEffects.FlipHorizontally;
                    //    }
                    //}
                    #endregion
                    if (Boss.spriteVisible == true)
                    {
                        loopInt++;
                        if (loopInt >= 10)
                        {
                            BossAI();
                            loopInt = 0;
                        }
                        Boss.Frames(gameTime);
                        Boss.rectangles();
                        Boss.BulletFrames(gameTime);
                        Boss.Bullet();
                        Boss.clamp();
                        BossCollision();
                        Boss.setCollisionPosition();
                    }
                    #endregion
                    #region Pumpkins
                    PumpkinCollision();
                    for (int i = 0; i < totalNumOfPumpkins; i++)
                    {
                        //moving Pumpkins to players direction
                        if (pumpkin[i].visible && pumpkin[i].IsInScreen())
                        {
                            if (i % 2 == 0) //when the number is even, the pumpkins would follow Homer
                                pumpkin[i].moveInDirection = new Vector2(Homer1.position.X - pumpkin[i].position.X, Homer1.position.Y - pumpkin[i].position.Y);
                            else
                                pumpkin[i].moveInDirection = new Vector2(Peter1.position.X - pumpkin[i].position.X, Peter1.position.Y - pumpkin[i].position.Y);
                            pumpkin[i].moveInDirection.Normalize();
                            pumpkin[i].position.X += (int)(pumpkin[i].moveInDirection.X * pumpkin[i].speed);
                            pumpkin[i].position.Y += (int)(pumpkin[i].moveInDirection.Y * pumpkin[i].speed);
                        }
                    }
                    // PumpkinCollision();
                    #endregion
                    #region  WhiteChickens
                    for (int i = 0; i < totalNumOfWhiteChickens; i++)
                    {
                        // whiteChicken will keep moving left
                        if (whiteChicken[i].spriteVisible && whiteChicken[i].IsInScreen())
                        {
                            whiteChicken[i].position.X -= (int)(whiteChicken[i].speed);
                        }
                        whiteChicken[i].Frames(gameTime);
                        whiteChicken[i].rectangles();
                        whiteChicken[i].clamp();
                        //whiteChicken[i].BulletFrames(gameTime);
                        //whiteChicken[i].Bullet();
                    }
                    WhiteChickenCollision();
                    #endregion


                    #endregion

                    //key to continue when player(s) win
                    if (Boss.spriteState == GiantChicken.playerState.dead &&
                        Keyboard.GetState().IsKeyDown(Keys.Space) && previousKeyboardState.IsKeyUp(Keys.Space))
                    {
                        Gamestate = GameState.END;
                        End.visible = true;
                    }

                    //key to continue when players loses
                    if ((Homer1.spriteState == Player.playerState.DEAD && Peter1.spriteState == Player2.playerState.DEAD) && 
                        Keyboard.GetState().IsKeyDown(Keys.Space) && previousKeyboardState.IsKeyUp(Keys.Space))
                    {
                        Gamestate = GameState.END;
                        End.visible = true;
                    }


                    //extra upgrade key
                    if (Keyboard.GetState().IsKeyDown(Keys.U) && previousKeyboardState.IsKeyUp(Keys.U))
                    {
                        Gamestate = GameState.UPGRADE;
                        Upgrade.visible = true;
                    }

                    #region cheat keys
                    if (Keyboard.GetState().IsKeyDown(Keys.D1) && previousKeyboardState.IsKeyUp(Keys.D1))
                    {
                        Cheat_AllPumpkinsDie();
                    }
                    if (Keyboard.GetState().IsKeyDown(Keys.D2) && previousKeyboardState.IsKeyUp(Keys.D2))
                    {
                        Cheat_AllWhiteChickensDie();
                    }
                    if (Keyboard.GetState().IsKeyDown(Keys.D3) && previousKeyboardState.IsKeyUp(Keys.D3))
                    {
                        Cheat_BossDies();
                    }
                    if (Keyboard.GetState().IsKeyDown(Keys.D4) && previousKeyboardState.IsKeyUp(Keys.D4))
                    {
                        Cheat_AllEnemiesDies();
                    }
                    if (Keyboard.GetState().IsKeyDown(Keys.D5) && previousKeyboardState.IsKeyUp(Keys.D5))
                    {
                        Cheat_DisplayP1andP2WinMessage();
                    }
                    #endregion

                    break;
                #endregion
                #region UPGRADE
                case GameState.UPGRADE:
                    if (Keyboard.GetState().IsKeyDown(Keys.NumPad1) && previousKeyboardState.IsKeyUp(Keys.NumPad1))
                    {
                        if (Homer1.spriteState != Player.playerState.DEAD && (Homer1.PlayerScore - 50) >= 0)
                        {
                            Homer1.Power += 5;
                            Homer1.PlayerScore -= 50;
                        }
                        //Homer fire power upgrade
                    }
                    if (Keyboard.GetState().IsKeyDown(Keys.NumPad2) && previousKeyboardState.IsKeyUp(Keys.NumPad2))
                    {
                        if (Homer1.spriteState != Player.playerState.DEAD && (Homer1.PlayerScore - 50) >= 0)
                        {
                            Homer1.PlayerHealthMax += 10;
                            Homer1.PlayerHealth += 10;
                            Homer1.PlayerScore -= 50;
                        }
                        //Homer health upgrade
                    }
                    if (Keyboard.GetState().IsKeyDown(Keys.NumPad3) && previousKeyboardState.IsKeyUp(Keys.NumPad3))
                    {
                        if (Homer1.spriteState != Player.playerState.DEAD && (Homer1.PlayerScore - 100) >= 0 && Homer1.PlayerHealth != Homer1.PlayerHealthMax)
                        {
                            Homer1.PlayerHealth = Homer1.PlayerHealthMax;
                            Homer1.PlayerScore -= 100;
                        }
                        //Homer restore full health
                    }


                    if (Keyboard.GetState().IsKeyDown(Keys.Z) && previousKeyboardState.IsKeyUp(Keys.Z))
                    {
                        if (Peter1.spriteState != Player2.playerState.DEAD && (Peter1.PlayerScore - 50) >= 0)
                        {
                            Peter1.Power += 5;
                            Peter1.PlayerScore -= 50;
                        }
                        //Peter fire power upgrade
                    }
                    if (Keyboard.GetState().IsKeyDown(Keys.X) && previousKeyboardState.IsKeyUp(Keys.X))
                    {
                        if (Peter1.spriteState != Player2.playerState.DEAD && (Peter1.PlayerScore - 50) >= 0)
                        {
                            Peter1.PlayerHealthMax += 10;
                            Peter1.PlayerHealth += 10;
                            Peter1.PlayerScore -= 50;
                        }
                        //Peter health upgrade
                    }
                    if (Keyboard.GetState().IsKeyDown(Keys.C) && previousKeyboardState.IsKeyUp(Keys.C))
                    {
                        if (Peter1.spriteState != Player2.playerState.DEAD && (Peter1.PlayerScore - 100) >= 0 && Peter1.PlayerHealth != Peter1.PlayerHealthMax)
                        {
                            Peter1.PlayerHealth = Peter1.PlayerHealthMax;
                            Peter1.PlayerScore -= 100;
                        }
                        //Peter restore full health
                    }


                    if (Keyboard.GetState().IsKeyDown(Keys.Space) && previousKeyboardState.IsKeyUp(Keys.Space))
                    {
                        Gamestate = GameState.PLAYING;
                        Upgrade.visible = false;
                    }
                    break;
                #endregion
                #region END
                case GameState.END:
                    if (Keyboard.GetState().IsKeyDown(Keys.Space) && previousKeyboardState.IsKeyUp(Keys.Space))
                    {
                        Gamestate = GameState.MAIN;
                        End.visible = false;
                        LoadContent();
                        Main.visible = true;
                    }
                    break;
                #endregion
                default:
                    break;
            }

            previousKeyboardState = Keyboard.GetState();
            base.Update(gameTime);
        }

        #region Collision & AI functions
        public void PumpkinCollision()
        {
            for (int i = 0; i < totalNumOfPumpkins; i++)
            {
                if (pumpkin[i].visible && pumpkin[i].isInScreen)
                {
                    if (pumpkin[i].checkCollideWith(Homer1.bulletPos) && Homer1.bulletVisible)
                    {
                        pumpkinCollideSE.Play();
                        ExplodeSprite.visible = true;
                        ExplodeSprite.position.X = pumpkin[i].position.X;
                        ExplodeSprite.position.Y = pumpkin[i].position.Y;
                        pumpkin[i].position.X = -10;
                        pumpkin[i].position.Y = -10;
                        Homer1.bulletVisible = false;
                        Homer1.bulletPos.X = -100;
                        Homer1.bulletPos.Y = -100;
                        Homer1.PlayerScore += 5;
                        pumpkin[i].visible = false;

                    }
                    if (pumpkin[i].checkCollideWith(Peter1.bulletPos) && Peter1.bulletVisible)
                    {
                        pumpkinCollideSE.Play();
                        ExplodeSprite.visible = true;
                        ExplodeSprite.position.X = pumpkin[i].position.X;
                        ExplodeSprite.position.Y = pumpkin[i].position.Y;
                        pumpkin[i].visible = false;
                        pumpkin[i].position.X = -10;
                        pumpkin[i].position.Y = -10;
                        Peter1.bulletVisible = false;
                        Peter1.bulletPos.X = -100;
                        Peter1.bulletPos.Y = -100;
                        Peter1.PlayerScore += 5;
                    }
                    if ((pumpkin[i].checkCollideWith(Homer1.PositionNoGun) || pumpkin[i].checkCollideWith(Homer1.PositionOnlyGun)) &&
                        Homer1.spriteState != Player.playerState.DEAD)
                    {
                        pumpkinCollideSE.Play();
                        ExplodeSprite.position.X = pumpkin[i].position.X;
                        ExplodeSprite.position.Y = pumpkin[i].position.Y;
                        ExplodeSprite.visible = true;
                        pumpkin[i].visible = false;
                        if (Homer1.spriteEffects == SpriteEffects.FlipHorizontally)
                            Homer1.position.X += 50;
                        if (Homer1.spriteEffects == SpriteEffects.None)
                            Homer1.position.X -= 50;
                        pumpkin[i].position.X = -10;
                        pumpkin[i].position.Y = -10;
                        Peter1.bulletVisible = false;
                        Homer1.spriteState = Player.playerState.HURT;
                        Homer1.PlayerHealthAdd(-10);
                    }
                    if ((pumpkin[i].checkCollideWith(Peter1.PositionNoGun) || pumpkin[i].checkCollideWith(Peter1.PositionOnlyGun))&&
                        Peter1.spriteState != Player2.playerState.DEAD)
                    {
                        pumpkinCollideSE.Play();
                        ExplodeSprite.position.X = pumpkin[i].position.X;
                        ExplodeSprite.position.Y = pumpkin[i].position.Y;
                        ExplodeSprite.visible = true;
                        pumpkin[i].visible = false;
                        if (Peter1.spriteEffects == SpriteEffects.FlipHorizontally)
                            Peter1.position.X += 50;
                        if (Peter1.spriteEffects == SpriteEffects.None)
                            Peter1.position.X -= 50;
                        pumpkin[i].position.X = -10;
                        pumpkin[i].position.Y = -10;
                        Peter1.bulletVisible = false;
                        Peter1.spriteState = Player2.playerState.HURT;
                        Peter1.PlayerHealthAdd(-10);
                    }
                }
            }
        }
        public void BossEggCollision()
        {
            for (int i = 0; i < totalNumOfEggCrackStages; i++)
            {
                if (BossEgg[i].visible)
                {
                    if (BossEgg[i].checkCollideWith(Homer1.bulletPos) && Homer1.bulletVisible)
                    {
                        pumpkinCollideSE.Play();
                        BossEgg[i].visible = false;
                        ExplodeSprite.position.X = BossEgg[i].position.X;
                        ExplodeSprite.position.Y = BossEgg[i].position.Y;
                        BossEgg[i].position.X = -10;
                        BossEgg[i].position.Y = -10;
                        Homer1.bulletVisible = false;
                        Homer1.bulletPos.X = -100;
                        Homer1.bulletPos.Y = -100;
                        Homer1.PlayerScore += 5;
                        ExplodeSprite.visible = true;
                        EggCrackStage++;    //will range from 0-3
                    }
                    if (BossEgg[i].checkCollideWith(Peter1.bulletPos) && Peter1.bulletVisible)
                    {
                        pumpkinCollideSE.Play();
                        BossEgg[i].visible = false;
                        ExplodeSprite.position.X = BossEgg[i].position.X;
                        ExplodeSprite.position.Y = BossEgg[i].position.Y;
                        BossEgg[i].position.X = -10;
                        BossEgg[i].position.Y = -10;
                        Peter1.bulletVisible = false;
                        Peter1.bulletPos.X = -100;
                        Peter1.bulletPos.Y = -100;
                        Peter1.PlayerScore += 5;
                        ExplodeSprite.visible = true;
                        EggCrackStage++;    //will range from 0-3
                    }
                    if (BossEgg[i].checkCollideWith(Homer1.PositionNoGun) || BossEgg[i].checkCollideWith(Homer1.PositionOnlyGun))
                    {
                        pumpkinCollideSE.Play();
                        HomerSE.Play();
                        BossEgg[i].visible = false;
                        ExplodeSprite.position.X = BossEgg[i].position.X;
                        ExplodeSprite.position.Y = BossEgg[i].position.Y;
                        if (Homer1.spriteEffects == SpriteEffects.FlipHorizontally)
                            Homer1.position.X += 50;
                        if (Homer1.spriteEffects == SpriteEffects.None)
                            Homer1.position.X -= 50;
                        BossEgg[i].position.X = -10;
                        BossEgg[i].position.Y = -10;
                        Peter1.bulletVisible = false;
                        Homer1.spriteState = Player.playerState.HURT;
                        Homer1.PlayerHealthAdd(-5);
                        ExplodeSprite.visible = true;
                        EggCrackStage++;    //will range from 0-3
                    }
                    if (BossEgg[i].checkCollideWith(Peter1.PositionNoGun) || BossEgg[i].checkCollideWith(Peter1.PositionOnlyGun))
                    {
                        pumpkinCollideSE.Play();
                        PeterSE.Play();
                        BossEgg[i].visible = false;
                        ExplodeSprite.position.X = BossEgg[i].position.X;
                        ExplodeSprite.position.Y = BossEgg[i].position.Y;
                        if (Peter1.spriteEffects == SpriteEffects.FlipHorizontally)
                            Peter1.position.X += 50;
                        if (Peter1.spriteEffects == SpriteEffects.None)
                            Peter1.position.X -= 50;
                        BossEgg[i].position.X = -10;
                        BossEgg[i].position.Y = -10;
                        Peter1.bulletVisible = false;
                        Peter1.spriteState = Player2.playerState.HURT;
                        Peter1.PlayerHealthAdd(-5);
                        ExplodeSprite.visible = true;
                        EggCrackStage++;    //will range from 0-3
                    }
                }
            }
        }
        public void BossCollision()
        {
            if (Boss.spriteVisible)
            {
                if (Boss.checkCollideWith(Homer1.bulletPos) && Boss.spriteState != GiantChicken.playerState.dead)
                {
                    chickenCollideSE.Play();
                    Homer1.bulletVisible = false;
                    Homer1.bulletPos.X = -100;
                    Homer1.bulletPos.Y = -100;
                    Homer1.PlayerScore += 5;
                    Boss.spriteState = GiantChicken.playerState.hurt;
                    Boss.PlayerHealthAdd(-Homer1.Power);
                    if (Boss.spriteState == GiantChicken.playerState.dead)
                        bossDieExplosionSE.Play();
                }
                if (Boss.checkCollideWith(Peter1.bulletPos) && Boss.spriteState != GiantChicken.playerState.dead)
                {
                    chickenCollideSE.Play();
                    Peter1.bulletVisible = false;
                    Peter1.bulletPos.X = -100;
                    Peter1.bulletPos.Y = -100;
                    Peter1.PlayerScore += 5;
                    Boss.spriteState = GiantChicken.playerState.hurt;
                    Boss.PlayerHealthAdd(-Peter1.Power);
                    if (Boss.spriteState == GiantChicken.playerState.dead)
                        bossDieExplosionSE.Play();
                }
                if (Boss.checkCollideWithBullet(Homer1.bulletPos) && Boss.bulletVisible)
                {
                    popSE.Play();
                    ExplodeSprite.position.X = Boss.bulletPos.X;
                    ExplodeSprite.position.Y = Boss.bulletPos.Y;
                    ExplodeSprite.alive = true;
                    Boss.bulletVisible = false;
                    Homer1.bulletVisible = false;
                    Homer1.bulletPos.X = -100;
                    Homer1.bulletPos.Y = -100;
                    Homer1.PlayerScore += 2;
                }
                if (Boss.checkCollideWithBullet(Peter1.bulletPos) && Boss.bulletVisible)
                {
                    popSE.Play();
                    ExplodeSprite.position.X = Boss.bulletPos.X;
                    ExplodeSprite.position.Y = Boss.bulletPos.Y;
                    ExplodeSprite.alive = true;
                    Boss.bulletVisible = false;
                    Peter1.bulletVisible = false;
                    Peter1.bulletPos.X = -100;
                    Peter1.bulletPos.Y = -100;
                    Peter1.PlayerScore += 2;
                }

                if (Boss.checkCollideWithBullet(Homer1.PositionNoGun) && Homer1.spriteState != Player.playerState.DEAD)
                {
                    HomerSE.Play();
                    Boss.bulletVisible = false;
                    ExplodeSprite.position.X = Boss.bulletPos.X;
                    ExplodeSprite.position.Y = Boss.bulletPos.Y;
                    Boss.bulletPos.X = -10;
                    Boss.bulletPos.Y = -10;
                    if (Homer1.position.X > 50)
                    {
                        if (Homer1.spriteEffects == SpriteEffects.FlipHorizontally)
                            Homer1.position.X += 50;
                        if (Homer1.spriteEffects == SpriteEffects.None)
                            Homer1.position.X -= 50;
                    }
                    Homer1.spriteState = Player.playerState.HURT;
                    Homer1.PlayerHealthAdd(-5);
                    ExplodeSprite.visible = true;
                    Boss.PlayerScore += 2.5f;
                }
                if (Boss.checkCollideWithBullet(Peter1.PositionNoGun) && Peter1.spriteState != Player2.playerState.DEAD)
                {
                    PeterSE.Play();
                    Boss.bulletVisible = false;
                    ExplodeSprite.position.X = Boss.bulletPos.X;
                    ExplodeSprite.position.Y = Boss.bulletPos.Y;
                    Boss.bulletPos.X = -10;
                    Boss.bulletPos.Y = -10;
                    if (Peter1.position.X > 50)
                    {
                        if (Peter1.spriteEffects == SpriteEffects.FlipHorizontally)
                            Peter1.position.X += 50;
                        if (Peter1.spriteEffects == SpriteEffects.None)
                            Peter1.position.X -= 50;
                    }
                    Peter1.spriteState = Player2.playerState.HURT;
                    Peter1.PlayerHealthAdd(-5);
                    ExplodeSprite.visible = true;
                    Boss.PlayerScore += 2.5f;
                }
            }
        }
        public void WhiteChickenCollision()
        {
            for (int i = 0; i < totalNumOfWhiteChickens; i++)
            {
                if (whiteChicken[i].spriteVisible)
                {
                    if (whiteChicken[i].checkCollideWith(Homer1.bulletPos) && Homer1.bulletVisible)
                    {
                        chickenCollideSE.Play();
                        ExplodeSprite.position.X = whiteChicken[i].position.X + 50;
                        ExplodeSprite.position.Y = whiteChicken[i].position.Y + 75;
                        ExplodeSprite.visible = true;
                        Homer1.bulletVisible = false;
                        Homer1.bulletPos.X = -100;
                        Homer1.bulletPos.Y = -100;
                        Homer1.PlayerScore += 5;
                        whiteChicken[i].spriteState = WhiteChicken.playerState.hurt;
                        whiteChicken[i].position.X += 50;   //so chicken would move back
                        whiteChicken[i].PlayerHealthAdd(-50);
                    }
                    if (whiteChicken[i].checkCollideWith(Peter1.bulletPos) && Peter1.bulletVisible)
                    {
                        chickenCollideSE.Play();
                        ExplodeSprite.position.X = whiteChicken[i].position.X + 50;
                        ExplodeSprite.position.Y = whiteChicken[i].position.Y + 75;
                        ExplodeSprite.visible = true;
                        Peter1.bulletVisible = false;
                        Peter1.bulletPos.X = -100;
                        Peter1.bulletPos.Y = -100;
                        Peter1.PlayerScore += 5;
                        whiteChicken[i].spriteState = WhiteChicken.playerState.hurt;
                        whiteChicken[i].position.X += 50;   //so chicken would move back
                        whiteChicken[i].PlayerHealthAdd(-50);
                    }

                    if (whiteChicken[i].checkCollideWith(Homer1.PositionNoGun) && 
                        Homer1.position.X+50 < whiteChicken[i].position.X && 
                        Homer1.spriteState != Player.playerState.DEAD)
                    {
                        whiteChicken[i].spriteState = WhiteChicken.playerState.punching;
                        HomerSE.Play();
                        if (Homer1.position.X > 50)
                        {
                            if (Homer1.spriteEffects == SpriteEffects.FlipHorizontally)
                                Homer1.position.X += 50;
                            if (Homer1.spriteEffects == SpriteEffects.None)
                                Homer1.position.X -= 50;
                        }
                        Homer1.PlayerHealthAdd(-10);
                    }
                    if (whiteChicken[i].checkCollideWith(Peter1.PositionNoGun) && 
                        Peter1.position.X+50 < whiteChicken[i].position.X && 
                        Peter1.spriteState != Player2.playerState.DEAD)
                    {
                        whiteChicken[i].spriteState = WhiteChicken.playerState.punching;
                        PeterSE.Play();
                        if (Peter1.position.X > 50)
                        {
                            if (Peter1.spriteEffects == SpriteEffects.FlipHorizontally)
                                Peter1.position.X += 50;
                            if (Peter1.spriteEffects == SpriteEffects.None)
                                Peter1.position.X -= 50;
                        }
                        //Peter1.spriteState = Player2.playerState.HURT;
                        Peter1.PlayerHealthAdd(-10);
                    }
                }
            }
        }
        public void BossAI()
        {
            if (Boss.spriteState != GiantChicken.playerState.firing &&
                Boss.spriteState != GiantChicken.playerState.hurt &&
                Boss.spriteState != GiantChicken.playerState.dead)
            {

                BossRand = new Random();
                BossInt = BossRand.Next(0, 100);
                if (BossInt > 20) // WALK OR FIRE
                {
                    if (BossInt <= 75) // WALK
                    {
                        if (Peter1.spriteState != Player2.playerState.DEAD)
                        {
                            if (Boss.position.Y > Peter1.position.Y)
                            {
                                Boss.spriteState = GiantChicken.playerState.walking;
                                Boss.position.Y -= GiantChickenMeasurements.walking.speed;
                            }
                            else
                            {
                                Boss.spriteState = GiantChicken.playerState.walking;
                                Boss.position.Y += GiantChickenMeasurements.walking.speed;
                            }
                        }

                        if ((Homer1.spriteState != Player.playerState.DEAD) && (Peter1.spriteState == Player2.playerState.DEAD))
                        {
                            if (Boss.position.Y > Homer1.position.Y)
                            {
                                Boss.spriteState = GiantChicken.playerState.walking;
                                Boss.position.Y -= GiantChickenMeasurements.walking.speed;
                            }
                            else
                            {
                                Boss.spriteState = GiantChicken.playerState.walking;
                                Boss.position.Y += GiantChickenMeasurements.walking.speed;
                            }
                        }
                    }

                    else //FIRE IF CLOSE ENOUGH
                    {
                        if (((Boss.position.Y - Peter1.position.Y <= 10) || (Boss.position.Y - Homer1.position.Y <= 10)) && (!Boss.bulletVisible))
                        {
                            Boss.spriteState = GiantChicken.playerState.firing;
                            Boss.bulletVisible = true;
                            tornadoSE.Play();
                            if (Boss.spriteEffects != SpriteEffects.FlipHorizontally)
                            {
                                Boss.bulletPos.X = Boss.position.X + 500;
                                Boss.bulletPos.Y = Boss.position.Y + 200;
                                Boss.bulletSpriteEffects = SpriteEffects.None;
                            }
                            if (Boss.spriteEffects == SpriteEffects.FlipHorizontally)
                            {
                                Boss.bulletPos.X = Boss.position.X - 50;
                                Boss.bulletPos.Y = Boss.position.Y + 64;
                                Boss.bulletSpriteEffects = SpriteEffects.FlipHorizontally;
                            }
                        }
                        else
                        {
                            if (Peter1.spriteState != Player2.playerState.DEAD)
                            {
                                if (Boss.position.Y > Peter1.position.Y)
                                {
                                    Boss.spriteState = GiantChicken.playerState.walking;
                                    Boss.position.Y -= GiantChickenMeasurements.walking.speed;
                                }
                                else
                                {
                                    Boss.spriteState = GiantChicken.playerState.walking;
                                    Boss.position.Y += GiantChickenMeasurements.walking.speed;
                                }
                            }

                            if ((Homer1.spriteState != Player.playerState.DEAD) && (Peter1.spriteState == Player2.playerState.DEAD))
                            {
                                if (Boss.position.Y > Homer1.position.Y)
                                {
                                    Boss.spriteState = GiantChicken.playerState.walking;
                                    Boss.position.Y -= GiantChickenMeasurements.walking.speed;
                                }
                                else
                                {
                                    Boss.spriteState = GiantChicken.playerState.walking;
                                    Boss.position.Y += GiantChickenMeasurements.walking.speed;
                                }
                            }
                        }
                    }
                }
                else
                {
                    Boss.spriteState = GiantChicken.playerState.standing;
                }
            }
        }
        #endregion

        #region cheat functions
        public void Cheat_AllPumpkinsDie() 
        {
            for (int i = 0; i < totalNumOfPumpkins; i++)
                pumpkin[i].visible = false;
        }
        public void Cheat_AllWhiteChickensDie()
        {
            for (int i = 0; i < totalNumOfPumpkins; i++)
                whiteChicken[i].spriteVisible = false;
        }
        public void Cheat_BossDies()
        {
            Boss.spriteVisible = false;
        }
        public void Cheat_AllEnemiesDies()
        {
            Cheat_AllPumpkinsDie();
            Cheat_AllWhiteChickensDie();
            Cheat_BossDies();
        }
        public void Cheat_DisplayP1andP2WinMessage()
        { 
            //these arguments will trigger the winMessage to pop up;
            Boss.spriteState = GiantChicken.playerState.dead;
            Homer1.spriteState = Player.playerState.STANDING;
            Peter1.spriteState = Player2.playerState.STANDING;
        }
        #endregion

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            spriteBatch.Begin();
            if (Main.visible)
                Main.draw(spriteBatch);
            if (Control.visible)
                Control.draw(spriteBatch);
            if (End.visible)
                End.draw(spriteBatch);


            if (Gamestate == GameState.PLAYING || Gamestate == GameState.UPGRADE)
            {
                scrolling1.Draw(spriteBatch);
                scrolling2.Draw(spriteBatch);
                scrolling3.Draw(spriteBatch);
                /*
                 * 
                 * 
                 * ADD PLAYING DRAW HERE
                 * 
                 * 
                 * */
                if (Peter1.position.Y >= Homer1.position.Y)
                {
                    Homer1.draw(spriteBatch);
                    Peter1.draw(spriteBatch);
                }
                if (Homer1.position.Y >= Peter1.position.Y)
                {
                    Peter1.draw(spriteBatch);
                    Homer1.draw(spriteBatch);
                }
                for (int i = 0; i < totalNumOfPumpkins; i++)
                    pumpkin[i].draw(spriteBatch);
                for (int i = 0; i < totalNumOfWhiteChickens; i++)
                    whiteChicken[i].draw(spriteBatch);
                for (int i = 0; i < totalNumOfEggCrackStages; i++)
                    BossEgg[i].draw(spriteBatch);

                Boss.draw(spriteBatch);

                ExplodeSprite.draw(spriteBatch);
                ExplodeSprite.visible = false;

                if (Boss.spriteState == GiantChicken.playerState.dead)
                {
                    if (Homer1.spriteState != Player.playerState.DEAD && Peter1.spriteState != Player2.playerState.DEAD)
                        spriteBatch.Draw(winMessage3, new Vector2(100, 50), Color.AliceBlue);
                    else if (Peter1.spriteState != Player2.playerState.DEAD)
                        spriteBatch.Draw(winMessage2, new Vector2(100, 50), Color.AliceBlue);
                    else if (Homer1.spriteState != Player.playerState.DEAD)
                        spriteBatch.Draw(winMessage1, new Vector2(100, 50), Color.AliceBlue);
                }
                if (Homer1.spriteState == Player.playerState.DEAD && Peter1.spriteState == Player2.playerState.DEAD)
                    spriteBatch.Draw(loseMessage, new Vector2(100, 50), Color.AliceBlue);

                if (Upgrade.visible)
                    Upgrade.draw(spriteBatch); // KEEP AT END OF IF STATEMENT
            }

            // this will draw the collision rectangles
            //spriteBatch.Draw(whiteTexture2D, Homer1.PositionNoGun, Color.AliceBlue);
            //spriteBatch.Draw(greenTexture2D, Homer1.PositionOnlyGun, Color.AliceBlue);
            //spriteBatch.Draw(whiteTexture2D, Peter1.PositionNoGun, Color.AliceBlue);
            //spriteBatch.Draw(greenTexture2D, Peter1.PositionOnlyGun, Color.AliceBlue);
            //spriteBatch.Draw(whiteTexture2D, Boss.collisionPosition, Color.AliceBlue);
            
            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
