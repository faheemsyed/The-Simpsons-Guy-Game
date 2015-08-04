using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WindowsGame9
{
    class Player
    {
        public enum playerState { STANDING, WALKING, FIRING, HURT, DEAD };
        public playerState spriteState;
        public playerState prevSpriteState;
        public int PlayerHealthMax = 100;
        public int PlayerHealth = 100;
        public int PlayerScore = 0;
        public int Power = 5;
        public int PlayerHealthHurtAmount = 0;
        public SpriteEffects spriteEffects;
        public Texture2D sprite;
        public bool spriteVisible;
        public Rectangle position;
        public Rectangle PositionNoGun;
        public Rectangle PositionOnlyGun;
        public void setCollisionPosition()
        { 
            if (spriteEffects == SpriteEffects.None)
            {
                PositionNoGun = new Rectangle(position.X, position.Y, position.Width - 70, position.Height);
                PositionOnlyGun = new Rectangle(position.X + 50, position.Y + 55, position.Width - 55, position.Height - 110);
            }
            else if (spriteEffects == SpriteEffects.FlipHorizontally)
            {
                PositionNoGun = new Rectangle(position.X + 70, position.Y, position.Width - 70, position.Height);
                PositionOnlyGun = new Rectangle(position.X, position.Y + 55, position.Width - 55, position.Height - 110);
            }
        }
        public Rectangle sourceRectangle;
        public int frames = 0;
        public float elapsed = 0;
        private float scale;
        public Texture2D bullet;
        public float bulletScale;
        public bool bulletVisible = false;
        public SpriteEffects BulletspriteEffects;
        public Rectangle bulletPos;
        private GameTime gameTime;
        private SpriteFont font;
        public Rectangle healthBar;
        public Vector2 healthBarLocation = new Vector2(175, 10);
        public float healthBarScale = 3.5f;
        public Rectangle ObjectRectangle;
        public Player(Texture2D texture, SpriteFont font1,GraphicsDeviceManager graphics)
        {
            sprite = texture;
            spriteVisible = true;
            spriteState = playerState.STANDING;
            prevSpriteState = playerState.STANDING;
            position.X = 10;
            position.Y = 550;
            bulletScale = 1.0f;
            ObjectRectangle = new Rectangle();
        }
        public Player(Texture2D texture, Texture2D texture2, SpriteFont font1, GraphicsDeviceManager graphics)
        {
            font = font1;
            sprite = texture;
            bullet = texture2;
            position.X = 10;
            position.Y = 550;
            spriteVisible = true;
            spriteState = playerState.STANDING;
            prevSpriteState = playerState.STANDING;
            bulletScale = 1.0f;
            ObjectRectangle = new Rectangle();
        }
        public void Frames(GameTime time)
        {
            gameTime = time;
            switch (spriteState)
            {
                #region case: STANDING
                case playerState.STANDING:
                    if (spriteState != prevSpriteState)
                    {
                        frames = 0;
                        elapsed = 0;
                        //if (Player1PreviousState == playerState.PUNCHING && flip1)
                        //    DestinationRectangle1.X += 40;
                        prevSpriteState = spriteState;
                    }
                    elapsed += (float)gameTime.ElapsedGameTime.TotalMilliseconds;
                    if (elapsed >= Homer.standing.delay)
                    {
                        if (frames >= (Homer.standing.numOfFrames - 1))
                            frames = 0;
                        else
                            frames++;
                        elapsed = 0;
                    }
                    break;
                #endregion
                #region case: WALKING
                case playerState.WALKING:
                    if (spriteState != prevSpriteState)
                    {
                        frames = 0;
                        elapsed = 0;
                        prevSpriteState = spriteState;
                    }
                    elapsed += (float)gameTime.ElapsedGameTime.TotalMilliseconds;
                    if (elapsed >= Homer.walking.delay)
                    {
                        if (frames >= (Homer.walking.numOfFrames - 1))
                            frames = 0;
                        else
                            frames++;
                        elapsed = 0;
                    }
                    break;
                #endregion
                #region case: FIRING
                case playerState.FIRING:
                    if (spriteState != prevSpriteState)
                    {
                        //fireBall.Play();
                        //FireBall1_CurrentState_Active = true;
                        frames = 0;
                        elapsed = 0;
                        prevSpriteState = spriteState;
                    }
                    elapsed += (float)gameTime.ElapsedGameTime.TotalMilliseconds;
                    if (elapsed >= Homer.firing.delay)
                    {
                        if (frames != Homer.firing.numOfFrames - 1)
                        {
                            frames++;
                            elapsed = 0;
                        }
                    }
                    if ((frames == Homer.firing.numOfFrames - 1) && (elapsed >= Homer.firing.delay + 200f))
                    {
                        frames = 0;
                        spriteState = playerState.STANDING;
                    }
                    break;
                #endregion
                #region case: HURT
                case playerState.HURT:
                    if (spriteState != prevSpriteState)
                    {
                        frames = 0;
                        elapsed = 0;
                        prevSpriteState = spriteState;
                        // = hurtamount
                    }
                    elapsed += (float)gameTime.ElapsedGameTime.TotalMilliseconds;
                    if (elapsed >= Homer.hurt.delay)
                    {
                        if (frames != Homer.hurt.numOfFrames - 1)
                        {
                            if (spriteEffects == SpriteEffects.None)
                                position.X -= 30;
                            else
                                position.X += 30;
                            frames++;
                            elapsed = 0;
                        }
                    }
                    if ((frames == Homer.hurt.numOfFrames - 1) && (elapsed >= Homer.hurt.delay + 200f))
                    {
                        frames = 0;
                        spriteState = playerState.STANDING;
                    }
                    break;
                #endregion
                #region case: DEAD
                case playerState.DEAD:
                    if (spriteState != prevSpriteState)
                    {
                        frames = 0;
                        elapsed = 0;
                        prevSpriteState = spriteState;
                    }
                    elapsed += (float)gameTime.ElapsedGameTime.TotalMilliseconds;
                    if (elapsed >= Homer.dead.delay)
                    {
                        if (frames < (Homer.dead.numOfFrames - 1))
                        {
                            if (spriteEffects == SpriteEffects.None)
                                position.X -= 50;
                            if (spriteEffects == SpriteEffects.FlipHorizontally)
                                position.X += 50;
                            frames++;
                        }
                        elapsed = 0;
                    }
                    break;
                #endregion
                default:
                    break;
            }
            //switch (PlayerHealth)
            //{

            //}
        }
        public void rectangles()
        {
            switch (spriteState)
            {
                case playerState.STANDING:
                    sourceRectangle = new Rectangle(Homer.standing.X[frames], Homer.standing.Y[frames],
                        Homer.standing.Width[frames], Homer.standing.Height[frames]);
                    position = new Rectangle(position.X, position.Y + (Homer.standing.Height[0] - Homer.standing.Height[frames]),
                        Homer.standing.Width[frames] * 2, Homer.standing.Height[frames] * 2);
                    break;
                case playerState.WALKING:
                    sourceRectangle = new Rectangle(Homer.walking.X[frames], Homer.walking.Y[frames],
                        Homer.walking.Width[frames], Homer.walking.Height[frames]);
                    position = new Rectangle(position.X, position.Y,
                        Homer.walking.Width[frames] * 2, Homer.walking.Height[frames] * 2);
                    break;
                case playerState.FIRING:
                    sourceRectangle = new Rectangle(Homer.firing.X[frames], Homer.firing.Y[frames],
                        Homer.firing.Width[frames], Homer.firing.Height[frames]);
                    position = new Rectangle(position.X, position.Y + (Homer.standing.Height[0] - Homer.firing.Height[frames]),
                        Homer.firing.Width[frames] * 2, Homer.firing.Height[frames] * 2);
                    break;
                case playerState.HURT:
                    sourceRectangle = new Rectangle(Homer.hurt.X[frames], Homer.hurt.Y[frames],
                        Homer.hurt.Width[frames], Homer.hurt.Height[frames]);
                    position = new Rectangle(position.X, position.Y + (Homer.standing.Height[0] - Homer.hurt.Height[frames]),
                        Homer.hurt.Width[frames] * 2, Homer.hurt.Height[frames] * 2);
                    break;
                case playerState.DEAD:
                    sourceRectangle = new Rectangle(Homer.dead.X[frames], Homer.dead.Y[frames],
                        Homer.dead.Width[frames], Homer.dead.Height[frames]);
                    position = new Rectangle(position.X, position.Y,
                        Homer.dead.Width[frames] * 2, Homer.dead.Height[frames] * 2);
                    break;
                default:
                    break;
            }


            healthBar = new Rectangle(HomerHealth.Bar.X[-(PlayerHealth / (PlayerHealthMax/10)) + 10],
                                        HomerHealth.Bar.Y[-(PlayerHealth / (PlayerHealthMax / 10)) + 10],
                                        HomerHealth.Bar.Width[-(PlayerHealth / (PlayerHealthMax / 10)) + 10],
                                        HomerHealth.Bar.Height[-(PlayerHealth / (PlayerHealthMax/10)) + 10]);
        }
        public void PlayerHealthAdd(int num)
        {
            //this function will trigger hurt or dead
            if (PlayerHealth + num > 100)
                PlayerHealth += 100;
            else if (PlayerHealth + num < 0)
            {
                PlayerHealth = 0;
                spriteState = playerState.DEAD;
            }
            else
            {
                PlayerHealth += num;
                if (num < 0)    // if the number that's passed is negative
                    spriteState = playerState.HURT;
            }
        }
        public void Bullet()
        {
            if (bulletVisible)
            {
                if (BulletspriteEffects != SpriteEffects.FlipHorizontally)
                {
                    bulletPos.X += 10;
                    if (bulletPos.X > 1280)
                        bulletVisible = false;
                }
                if (BulletspriteEffects == SpriteEffects.FlipHorizontally)
                {
                    bulletPos.X -= 10;
                    if (bulletPos.X < 0)
                        bulletVisible = false;
                }
            }
        }
        public void clamp()
        {
            if (spriteState != playerState.DEAD)
            {
                switch (spriteState)
                {
                    case playerState.STANDING:
                        position.X = (int)MathHelper.Clamp(position.X, 35f, 1150f);
                        position.Y = (int)MathHelper.Clamp(position.Y, 325f, 600f);
                        break;
                    case playerState.WALKING:
                        position.X = (int)MathHelper.Clamp(position.X, 35f, 1150f);
                        position.Y = (int)MathHelper.Clamp(position.Y, 325f, 600f);
                        break;
                    case playerState.FIRING:
                        position.X = (int)MathHelper.Clamp(position.X, 35f, 1150f);
                        position.Y = (int)MathHelper.Clamp(position.Y, 325f, 600f);
                        break;
                    case playerState.HURT:
                        position.X = (int)MathHelper.Clamp(position.X, 35f, 1150f);
                        position.Y = (int)MathHelper.Clamp(position.Y, 325f, 600f);
                        break;
                    default:
                        break;
                }
            }
        }
        public Rectangle getObjectRectangle()
        {
            ObjectRectangle = new Rectangle(position.X, position.Y, sourceRectangle.Width, sourceRectangle.Height);
            return ObjectRectangle;
        }
        public void draw(SpriteBatch spriteBatch)
        {
            if (spriteVisible)
            {
                spriteBatch.Draw(sprite, position, sourceRectangle, Color.White, 0f, Vector2.Zero, spriteEffects, 0.8f);
                //spriteBatch.Draw(sprite, new Vector2(position.X, position.Y), sourceRectangle, Color.White, 0f, Vector2.Zero, scale, spriteEffects, 0.8f);
            }
            if (bulletVisible)
            {
                spriteBatch.Draw(bullet, new Vector2(bulletPos.X, bulletPos.Y), null, Color.White, 0f, Vector2.Zero, bulletScale, spriteEffects, 0.8f);
            }
            spriteBatch.DrawString(font, "Points: " + PlayerScore, new Vector2(325, 0), Color.Black);
            spriteBatch.DrawString(font, "Power: " + Power, new Vector2(325, 30), Color.Black);
            spriteBatch.DrawString(font, "Health: " + PlayerHealth + "/" + PlayerHealthMax, new Vector2(325, 60), Color.Black);
            spriteBatch.Draw(sprite, healthBarLocation, healthBar, Color.White, 0f, Vector2.Zero, healthBarScale, SpriteEffects.None, 0.8f);
        }
    }
}
