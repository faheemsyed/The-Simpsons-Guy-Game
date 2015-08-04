using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WindowsGame9
{
    class GameObject
    {
        public int health = 0;
        public Texture2D sprite;
        public Texture2D[] sprites;
        public int index = 0;
        public bool spritesBool = false;
        public Texture2D dummyTexture;
        public Vector2 position;
        public Vector2 center;
        public float rotation;
        public float speed;
        private float scale;
        public Vector2 moveInDirection;
        public bool isInScreen = false;
        public bool alive;
        public bool visible;
        public bool animation;
        private Rectangle objectRectangle;
        public SpriteEffects spriteEffects;
        private Rectangle sourceRectangle;
        public float layer;
        public int frameCountX = 8;
        public int frameCountY = 5;
        public int frameIndexX = 0;
        public int frameIndexY = 0;
        private TimeSpan AnimationUpdate = TimeSpan.Zero;
        public int animationUpdateFreq = 50;
        public Rectangle ObjectRectangle
        {
            get
            {
                objectRectangle.X = (int)(position.X - (objectRectangle.Width / 2));
                objectRectangle.Y = (int)(position.Y - (objectRectangle.Height / 2));
                return objectRectangle;
            }
        }
        public Rectangle SourceRectangle
        {
            set
            {
                sourceRectangle = value;
                objectRectangle = sourceRectangle;
                center = new Vector2(sourceRectangle.Width / 2, sourceRectangle.Height / 2);
                //objectRectangle.Width = sourceRectangle.Width;
                //objectRectangle.Height = sourceRectangle.Height;
            }
            get
            {
                return sourceRectangle;
            }
        }
        public float Scale
        {
            set
            {
                scale = value;
                objectRectangle.Width = (int)(sprite.Width * value);
                objectRectangle.Height = (int)(sprite.Height * value);
            }
            get
            {
                return scale;
            }
        }
        public GameObject(Texture2D texture, GraphicsDeviceManager graphics)
        {
            sprite = texture;
            scale = 1.0f;
            position = Vector2.Zero;
            rotation = 0;
            center = new Vector2(sprite.Width / 2, sprite.Height / 2);
            moveInDirection = Vector2.Zero;
            objectRectangle = new Rectangle(0, 0, sprite.Width, sprite.Height);
            sourceRectangle = new Rectangle(0, 0, sprite.Width, sprite.Height);
            speed = 5;
            alive = true;
            visible = true;
            spriteEffects = SpriteEffects.None;
            layer = 1.0f;
            dummyTexture = new Texture2D(graphics.GraphicsDevice, 1, 1);
            dummyTexture.SetData(new Color[] { Color.White });
            animation = false;
        }
        public bool IsInScreen()
        {
            if (visible && position.X >= -100 && position.X <= 1300)
            {
                isInScreen = true;
                return true;
            }
            else
            {
                isInScreen = false;
                return false;
            }
        }
        public void draw(SpriteBatch spriteBatch)
        {
            if (visible)
            {
                //spriteBatch.Draw(dummyTexture, ObjectRectangle, Color.White);
                if (animation)
                {
                    if (!spritesBool)
                        spriteBatch.Draw(sprite, position, sourceRectangle, Color.White, rotation, center, scale, spriteEffects, layer);
                    else
                        spriteBatch.Draw(sprites[index], position, null, Color.White, rotation, center, scale, spriteEffects, layer);
                    //spriteBatch.Draw(sprite,new Rectangle((int)position.X,(int)position.Y,128,128), sourceRectangle, Color.White, rotation, center, spriteEffects, layer);
                }
                else
                    spriteBatch.Draw(sprite, position, null, Color.White, rotation, center, scale, spriteEffects, layer);
            }
        }
        public bool checkCollideWith(GameObject object2)
        {
            return ObjectRectangle.Intersects(object2.ObjectRectangle);
        }
        public bool checkCollideWith(Rectangle rectangle2)
        {
            return ObjectRectangle.Intersects(rectangle2);
        }
        public void nextXFrame(TimeSpan currentTime)
        {
            AnimationUpdate += currentTime;
            if (AnimationUpdate > TimeSpan.FromMilliseconds(animationUpdateFreq))
            {
                // 15 seconds have elapsed handle what you wanted to do
                frameIndexX++;
                if (frameIndexX >= frameCountX)
                {
                    frameIndexX = 0;
                    frameIndexY++;
                    if (frameIndexY >= frameCountY)
                    {
                        frameIndexY = 0;
                    }
                }
                if (frameIndexX == 5 && frameIndexY == 4)
                {
                    frameIndexX = 0;
                    frameIndexY = 0;
                }
                SourceRectangle = new Rectangle(frameIndexX * 128, frameIndexY * 128, 128, 128);
                AnimationUpdate = TimeSpan.Zero;
            }
        }
        public void nextXFrameSprites(TimeSpan currentTime)
        {
            AnimationUpdate += currentTime;
            if (AnimationUpdate > TimeSpan.FromMilliseconds(animationUpdateFreq))
            {
                // 15 seconds have elapsed handle what you wanted to do
                index++;
                if (index == 37)
                    index = 0;
                AnimationUpdate = TimeSpan.Zero;
            }
        }
    }
}

