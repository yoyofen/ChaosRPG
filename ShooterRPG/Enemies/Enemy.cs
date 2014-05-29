namespace ShooterRPG.Enemies
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using ShooterRPG.Projectiles;

    using Microsoft.Xna.Framework;

    using ShooterRPG.Content;
    using ShooterRPG.Heroes;
    
    public abstract class Enemy : DrawableObject, IMovable
    {
        const int NumberOfSectors = 20;

        public int Health { get; set; }

        public int Armor { get; set; }

        public int Damage { get; set; }

        public int Range { get; set; }

        public Enemy(int health, int armor, int damage, int speed, int range, Vector2 position)
        {
            this.Scale = 0.7f;
            this.SpriteName = Sprites.Zombie.ToString();
            this.SpriteShadow = Sprites.ZombieShadow.ToString();
            this.Health = health;
            this.Armor = armor;
            this.Damage = damage;
            this.Speed = speed;
            this.Range = range;
            this.Position = position;
            this.IsAlive = true;
            this.area = new Rectangle((int)position.X, (int)position.Y, 30, 30);
        }

        public Enemy(Vector2 position)
            : this(100, 100, 10, 5, 0, position)
        {

        }

        public void Update(Hero hero)
        {
            if (IsAlive)
            {
                Vector2 nextStep = FindNextStep(hero);

                var killerBullet = CollisionWithArrow(this.Position);
                if (killerBullet != null)
                {
                    this.Health -= killerBullet.AtackPower;
                    killerBullet.IsAlive = false;
                    killerBullet.Position = new Vector2(-20,-20);
                }

                this.rotation = PointDirection(this.Position.X, this.Position.Y, hero.Position.X, hero.Position.Y);

                if (!this.Collision(nextStep))
                {
                    this.Position = nextStep;
                }

                if (this.Health <= 0)
                {
                    // if we implement a mothod to summon new enemies remove next three lines and uncomment the line after them
                    this.Health = 100;
                    this.IsAlive = true;
                    this.Position = new Vector2(ListOfDrawableObjects.rnd.Next(600,700),ListOfDrawableObjects.rnd.Next(300,400));
                    
                    //this.IsAlive = false;
                }
            }
        }

        private Vector2 FindNextStep(Hero hero)
        {
            bool[,] visited = new bool[40, 30];
            
            Queue<Tuple<int, int>> nextVertices = new Queue<Tuple<int, int>>();
            nextVertices.Enqueue(new Tuple<int, int>((int)hero.Position.X / NumberOfSectors, (int)hero.Position.Y / NumberOfSectors));
            Tuple<int, int> endPosition = new Tuple<int, int>((int)this.Position.X / NumberOfSectors, (int)this.Position.Y / NumberOfSectors);
            Tuple<int, int> currentPosition;

            while (!nextVertices.IsEmpty())
            {
                do
                {
                    currentPosition = nextVertices.Dequeue();
                }
                while (!nextVertices.IsEmpty() && visited[currentPosition.Item1, currentPosition.Item2]);

                visited[currentPosition.Item1, currentPosition.Item2] = true;
                List<Tuple<int, int>> neighbours = GetNeighbours(currentPosition.Item1, currentPosition.Item2, visited);
                if (neighbours.Any(p => AreInTheSameRange(endPosition, p)))
                {
                    return GetNextStep(currentPosition);
                }

                nextVertices.EnqueueList(neighbours);
            }

            return new Vector2(this.Position.X, this.Position.Y);
        }

        private static bool AreInTheSameRange(Tuple<int, int> lhs, Tuple<int, int> rhs)
        {
            return Math.Abs(lhs.Item1 - rhs.Item1) <= NumberOfSectors && Math.Abs(lhs.Item2 - rhs.Item2) <= NumberOfSectors;
        }

        private Vector2 GetNextStep(Tuple<int, int> currentPosition)
        {
            if (currentPosition.Item1 * NumberOfSectors < this.Position.X)
            {
                return new Vector2(this.Position.X - this.Speed, this.Position.Y);
            }
            else if (currentPosition.Item1 * NumberOfSectors > this.Position.X)
            {
                return new Vector2(this.Position.X + this.Speed, this.Position.Y);
            }
            else if (currentPosition.Item2 * NumberOfSectors < this.Position.Y)
            {
                return new Vector2(this.Position.X, this.Position.Y - this.Speed);
            }
            else
            {
                return new Vector2(this.Position.X, this.Position.Y + this.Speed);
            }
        }

        private static List<Tuple<int, int>> GetNeighbours(int x, int y, bool[,] visited)
        {
            List<Tuple<int, int>> neighbours = new List<Tuple<int, int>>();

            if (x + 1 < 40 && !visited[x + 1, y])
            {
                neighbours.Add(new Tuple<int, int>(x + 1, y));
            }
            if (x - 1 >= 0 && !visited[x - 1, y])
            {
                neighbours.Add(new Tuple<int, int>(x - 1, y));
            }
            if (y + 1 < 30 && !visited[x, y + 1])
            {
                neighbours.Add(new Tuple<int, int>(x, y + 1));
            }
            if (y - 1 >= 0 && !visited[x, y - 1])
            {
                neighbours.Add(new Tuple<int, int>(x, y - 1));
            }

            return neighbours;
        }

        public virtual Projectile CollisionWithArrow(Vector2 possition)
        {
            this.area.X = (int)possition.X;
            this.area.Y = (int)possition.Y;

            foreach (var item in ListOfDrawableObjects.AllDrawableItems)
            {
                Projectile bullet = item as Projectile;
                if (bullet != null)
                {
                    if (bullet.area.Intersects(this.area))
                    {
                        return item as Projectile;
                    }
                }
            }

            return null;
        }

        public float PointDirection(float x, float y, float x2, float y2)
        {
            float diffx = x - x2;
            float diffy = y - y2;
            float adj = diffx;
            float opp = diffy;
            float tan = opp / adj;
            float res = MathHelper.ToDegrees((float)Math.Atan2(opp, adj));
            res = (res - 180) % 360;
            if (res < 0)
                res += 360;
            return res;
        }
    }
}