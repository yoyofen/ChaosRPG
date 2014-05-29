namespace ShooterRPG.Projectiles
{
    using Microsoft.Xna.Framework;

    using ShooterRPG.Items;

    public abstract class Projectile : DrawableObject, IMovable
    {   
        public int AtackPower { get; set; }
        
        public Projectile(Vector2 pos)
        {
            this.Position = pos;
            this.IsAlive = false;
            this.Scale = 0.01f;
        }

        public Projectile()
        {
            this.IsAlive = false;
            this.Scale = 0.01f;
        }

        public virtual bool Collision()
        {
            this.area.X = (int)Position.X;
            this.area.Y = (int)Position.Y;

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