using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WindowsGame9
{
    class WhiteChicken
    {
        public enum playerState { standing, punching, hurt, walking, dead };
        public playerState spriteState;
        public playerState prevSpriteState;
        public int PlayerHealth = 100;
        public float PlayerScore = 0;
        public int Power = 5;
        public float speed = 1.1f;
        public SpriteEffects spriteEffects;
        public Texture2D sprite;
        public bool spriteVisible = false;
        public Rectangle position;
        public Rectangle sourceRectangle;
        public Rectangle ObjectRectangle;
        public int frames = 0;
        public float elapsed = 0;
        private float scale;
        public Vector2 moveInDirection;
        public bool isInScreen = false;
        public Texture2D bullet;
        public bool bulletVisible = false;
        public Rectangle bulletPos;
        public Rectangle bulletSR;
        public SpriteEffects bulletSpriteEffects;
        public int bulletFrames = 0;
        public float bulletElapsed = 0;
        private float bulletScale;
        private GameTime gameTime;
        public Texture2D healthBar;
        public Vector2 healthBarLocation;
        public Rectangle healthBarSource;
        public Rectangle bulletObjectRectangle;
        public float healthBarScale = 0.5f;
        public float Scale
        {
            set
            {
                scale = value;
            }
            get
            {
                return scale;
            }
        }
        public bool IsInScreen()
        {
            if (spriteVisible && position.X >= -100 && position.X <= 1300)
            {
                if (prevSpriteState == playerState.standing && spriteState != playerState.punching)
                    spriteState = playerState.walking;
                return true;
            }
            else
            {
                spriteState = playerState.standing;
                return false;
            }
        }

        public WhiteChicken(Texture2D texture, Texture2D texture3, GraphicsDeviceManager graphics)
        {
            sprite = texture;
            healthBar = texture3;
            spriteState = playerState.standing;
            prevSpriteState = playerState.standing;
            spriteEffects = SpriteEffects.FlipHorizontally;
        }
        //public WhiteChicken(Texture2D texture, Texture2D texture2, Texture2D texture3, GraphicsDeviceManager graphics)
        //{
        //    sprite = texture;
        //    bullet = texture2;
        //    healthBar = texture3;
        //    position.X = 10;
        //    position.Y = 550;
        //    spriteVisible = true;
        //    spriteState = playerState.standing;
        //    prevSpriteState = playerState.standing;
        //    bulletScale = 1.0f;
        //    spriteEffects = SpriteEffects.FlipHorizontally;
        //}
        public void Frames(GameTime time)
        {
            gameTime = time;
            switch (spriteState)
            {
                #region case: STANDING
                case playerState.standing:
                    if (spriteState != prevSpriteState)
                    {
                        frames = 0;
                        elapsed = 0;
                        //if (Player1PreviousState == playerState.PUNCHING && flip1)
                        //    DestinationRectangle1.X += 40;
                        prevSpriteState = spriteState;
                    }
                    elapsed += (float)gameTime.ElapsedGameTime.TotalMilliseconds;
                    if (elapsed >= WhiteChickenMeasurements.standing.delay)
                    {
                        if (frames >= (WhiteChickenMeasurements.standing.numOfFrames - 1))
                            frames = 0;
                        else
                            frames++;
                        elapsed = 0;
                    }
                    break;
                #endregion
                #region case: WALKING
                case playerState.walking:
                    if (spriteState != prevSpriteState)
                    {
                        frames = 0;
                        elapsed = 0;
                        prevSpriteState = spriteState;
                    }
                    elapsed += (float)gameTime.ElapsedGameTime.TotalMilliseconds;
                    if (elapsed >= WhiteChickenMeasurements.walking.delay)
                    {
                        if (frames >= (WhiteChickenMeasurements.walking.numOfFrames - 1))
                            frames = 0;
                        else
                            frames++;
                        elapsed = 0;
                    }
                    break;
                #endregion
                #region case: punching
                case playerState.punching:
                    if (spriteState != prevSpriteState)
                    {
                        //fireBall.Play();
                        //FireBall1_CurrentState_Active = true;
                        frames = 0;
                        elapsed = 0;
                        prevSpriteState = spriteState;
                    }
                    elapsed += (float)gameTime.ElapsedGameTime.TotalMilliseconds;
                    if (elapsed >= WhiteChickenMeasurements.punching.delay)
                    {
                        if (frames != WhiteChickenMeasurements.punching.numOfFrames - 1)
                        {
                            frames++;
                            elapsed = 0;
                        }
                    }
                    if ((frames == WhiteChickenMeasurements.punching.numOfFrames - 1) && (elapsed >= WhiteChickenMeasurements.punching.delay + 200f))
                    {
                        frames = 0;
                        spriteState = playerState.standing;
                    }
                    break;
                #endregion
                #region case: HURT
                case playerState.hurt:
                    if (spriteState != prevSpriteState)
                    {
                        frames = 0;
                        elapsed = 0;
                        prevSpriteState = spriteState;
                    }
                    elapsed += (float)gameTime.ElapsedGameTime.TotalMilliseconds;
                    if (elapsed >= WhiteChickenMeasurements.hurt.delay)
                    {
                        if (frames != WhiteChickenMeasurements.hurt.numOfFrames - 1)
                        {
                            if (spriteEffects == SpriteEffects.None)
                                position.X -= 30;
                            else
                                position.X += 30;
                            frames++;
                            elapsed = 0;
                        }
                    }
                    if ((frames == WhiteChickenMeasurements.hurt.numOfFrames - 1) && (elapsed >= WhiteChickenMeasurements.hurt.delay + 200f))
                    {
                        frames = 0;
                        spriteState = playerState.standing;
                    }
                    break;
                #endregion
                #region case: DEAD
                case playerState.dead:
                    spriteVisible = false;
                #endregion
                    break;
                default:
                    break;
            }
        }
        public void BulletFrames(GameTime time)
        {
            gameTime = time;
            if (bulletVisible)
            {
                bulletFrames = 0;
                bulletElapsed = 0;
                prevSpriteState = spriteState;
                bulletElapsed += (float)gameTime.ElapsedGameTime.TotalMilliseconds;
                if (bulletElapsed >= Tornado.Shot.delay)
                {
                    if (bulletFrames != Tornado.Shot.numOfFrames - 1)
                    {
                        bulletFrames++;
                        bulletElapsed = 0;
                    }
                }
                if ((bulletFrames == Tornado.Shot.numOfFrames - 1) && (bulletElapsed >= Tornado.Shot.delay + 200f))
                {
                    bulletFrames = 0;
                    bulletVisible = false;
                }
            }
        }
        public void rectangles()
        {
            switch (spriteState)
            {
                case playerState.standing:
                    sourceRectangle = new Rectangle(WhiteChickenMeasurements.standing.X[frames], WhiteChickenMeasurements.standing.Y[frames],
                        WhiteChickenMeasurements.standing.Width[frames], WhiteChickenMeasurements.standing.Height[frames]);
                    position = new Rectangle(position.X, position.Y,
                        (int)(WhiteChickenMeasurements.standing.Width[frames] * scale), (int)(WhiteChickenMeasurements.standing.Height[frames] * scale));
                    break;
                case playerState.walking:
                    sourceRectangle = new Rectangle(WhiteChickenMeasurements.walking.X[frames], WhiteChickenMeasurements.walking.Y[frames],
                        WhiteChickenMeasurements.walking.Width[frames], WhiteChickenMeasurements.walking.Height[frames]);
                    position = new Rectangle(position.X, position.Y,
                        (int)(WhiteChickenMeasurements.walking.Width[frames] * scale), (int)(WhiteChickenMeasurements.walking.Height[frames] * scale));
                    break;
                case playerState.punching:
                    sourceRectangle = new Rectangle(WhiteChickenMeasurements.punching.X[frames], WhiteChickenMeasurements.punching.Y[frames],
                        WhiteChickenMeasurements.punching.Width[frames], WhiteChickenMeasurements.punching.Height[frames]);
                    position = new Rectangle(position.X, position.Y ,
                        (int)(WhiteChickenMeasurements.punching.Width[frames] * scale), (int)(WhiteChickenMeasurements.punching.Height[frames] * scale));
                    break;
                case playerState.hurt:
                    sourceRectangle = new Rectangle(WhiteChickenMeasurements.hurt.X[frames], WhiteChickenMeasurements.hurt.Y[frames],
                        WhiteChickenMeasurements.hurt.Width[frames], WhiteChickenMeasurements.hurt.Height[frames]);
                    position = new Rectangle(position.X, position.Y,
                        (int)(WhiteChickenMeasurements.hurt.Width[frames] * scale), (int)(WhiteChickenMeasurements.hurt.Height[frames] * scale));
                    break;
                case playerState.dead:
                    spriteVisible = false;
                    break;
                default:
                    break;
            }
            if (PlayerHealth > 0)
            {
                if (PlayerHealth == 100)
                    healthBarSource = new Rectangle(HealthBar.Bar.X[0], HealthBar.Bar.Y[0], HealthBar.Bar.Width[0], HealthBar.Bar.Height[0]);
                else
                    healthBarSource = new Rectangle(HealthBar.Bar.X[-(PlayerHealth / 10) + 9], HealthBar.Bar.Y[-(PlayerHealth / 10) + 9],
                        HealthBar.Bar.Width[-(PlayerHealth / 10) + 9], HealthBar.Bar.Height[-(PlayerHealth / 10) + 9]);
                healthBarLocation = new Vector2(position.X, position.Y - 10);
            }
            if (PlayerHealth == 0)
                healthBarSource = new Rectangle(0, 0, 0, 0);
        }
        public void PlayerHealthAdd(int num)
        {
            //this function will trigger hurt or dead
            if (PlayerHealth + num > 100)
                PlayerHealth += 100;
            else if (PlayerHealth + num <= 0)
            {
                PlayerHealth = 0;
                spriteState = playerState.dead;
            }
            else
            {
                PlayerHealth += num;
                if (num < 0)    // if the number that's passed is negative
                    spriteState = playerState.hurt;
            }
        }
        public void Bullet()
        {
            if (bulletVisible)
            {
                if (bulletSpriteEffects == SpriteEffects.None)
                {
                    bulletPos.X += 10;
                    if (bulletPos.X > 1000)
                        bulletVisible = false;
                }
                if (bulletSpriteEffects == SpriteEffects.FlipHorizontally)
                {
                    bulletPos.X -= 10;
                    if (bulletPos.X < 0)
                        bulletVisible = false;
                }
            }

            if (bulletVisible)
            {
                bulletSR = new Rectangle(Tornado.Shot.X[frames], Tornado.Shot.Y[frames], Tornado.Shot.Width[frames], Tornado.Shot.Height[frames]);
            }

        }
        public bool checkCollideWithBullet(Rectangle rectangle2)
        {
            bulletObjectRectangle = new Rectangle(bulletPos.X, bulletPos.Y, bulletSR.Width, bulletSR.Height);
            return bulletObjectRectangle.Intersects(rectangle2);
        }
        public bool checkCollideWith(Rectangle rectangle2)
        {
            ObjectRectangle = new Rectangle(position.X, position.Y, sourceRectangle.Width, sourceRectangle.Height);
            return ObjectRectangle.Intersects(rectangle2);
        }
        public void clamp()
        {
            switch (spriteState)
            {
                case playerState.standing:
                    //position.X = (int)MathHelper.Clamp(position.X, 35f, 1050f);
                    position.Y = (int)MathHelper.Clamp(position.Y, 275f, 550f);
                    break;
                case playerState.walking:
                    //position.X = (int)MathHelper.Clamp(position.X, 35f, 1050f);
                    position.Y = (int)MathHelper.Clamp(position.Y, 275f, 550f);
                    break;
                case playerState.punching:
                    //position.X = (int)MathHelper.Clamp(position.X, 35f, 1050f);
                    position.Y = (int)MathHelper.Clamp(position.Y, 275f, 550f);
                    break;
                case playerState.hurt:
                    //position.X = (int)MathHelper.Clamp(position.X, 35f, 1050f);
                    position.Y = (int)MathHelper.Clamp(position.Y, 275f, 550f);
                    break;
                default:
                    break;
            }
        }
        public void draw(SpriteBatch spriteBatch)
        {
            if (spriteVisible)
            {
                spriteBatch.Draw(sprite, position, sourceRectangle, Color.White, 0f, Vector2.Zero, spriteEffects, 0.8f);
                //spriteBatch.Draw(sprite, new Vector2(position.X, position.Y), sourceRectangle, Color.White, 0f, Vector2.Zero, scale, spriteEffects, 0.8f);
                spriteBatch.Draw(healthBar, healthBarLocation, healthBarSource, Color.White, 0f, Vector2.Zero, healthBarScale, SpriteEffects.None, 0.8f);
            }
            if (bulletVisible)
            {
                spriteBatch.Draw(bullet, new Vector2(bulletPos.X, bulletPos.Y), bulletSR, Color.White, 0f, Vector2.Zero, bulletScale, spriteEffects, 0.8f);
            }
        }
    }
}
