namespace ShooterRPG
{
    using System;
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Content;
    using Microsoft.Xna.Framework.Graphics;

    using ShooterRPG.Content;
    using ShooterRPG.Items;

    public abstract class DrawableObject : IDrawable
    {
        public float rotation = 0.0f;
        public Rectangle area;

        public Texture2D SpriteIndex { get; protected set; }

        public string SpriteName { get; protected set; }

        public float Scale { get; protected set; }
        
        public Vector2 Position { get; set; }

        public bool IsAlive { get; set; }

        public float Speed { get; set; }

        public string SpriteShadow { get; set; }

        public DrawableObject()
        {
            this.SpriteName = Sprites.Archer.ToString();
            this.Scale = 0.1f;
        }

        public virtual void Draw(SpriteBatch spriteBatch)
        {
            Vector2 center = new Vector2(this.SpriteIndex.Width / 2, this.SpriteIndex.Height / 2);
            if (IsAlive)
            {
                if (this.SpriteShadow != null)
                {
                    spriteBatch.Draw(this.SpriteIndex, this.Position + (new Vector2(5,5)), null, Color.DimGray, MathHelper.ToRadians(rotation), center, this.Scale, SpriteEffects.None, 0);
                }
                spriteBatch.Draw(this.SpriteIndex, this.Position, null, Color.White, MathHelper.ToRadians(rotation), center, this.Scale, SpriteEffects.None, 0);
            }
        }

        public virtual void LoadContent(ContentManager content, string newSpriteName)
        {
            this.SpriteName = newSpriteName;
            if (this.SpriteShadow != null)
            {
                this.SpriteIndex = content.Load<Texture2D>(this.SpriteShadow);
            }
            this.SpriteIndex = content.Load<Texture2D>(this.SpriteName);
        }

        public virtual void PushTo(float pix, float dir)
        {
            float newX = (float)Math.Cos(MathHelper.ToRadians(dir));
            float newY = (float)Math.Sin(MathHelper.ToRadians(dir));
            Position += new Vector2(pix * newX, pix * newY);
        }

        public virtual void Update()
        {
            PushTo(this.Speed, rotation);
        }

        public virtual bool Collision(Vector2 possition)
        {
            this.area.X = (int)possition.X;
            this.area.Y = (int)possition.Y;

            foreach (var item in ListOfDrawableObjects.AllDrawableItems)
            {
                Wall checkWall = item as Wall;
                if (checkWall != null)
                {
                    if (checkWall.area.Intersects(this.area))
                    {
                        return true;
                    }
                }
            }

            return false;
        }
    }
}