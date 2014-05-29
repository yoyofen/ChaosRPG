namespace ShooterRPG.Heroes
{
    using System;

    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Input;

    using ShooterRPG.Enemies;
    using ShooterRPG.Projectiles;
    
    public class Hero : DrawableObject, IMovable
    {
        public string Name { get; set; }

        float arrowSpeed = 10;
        
        public static Vector2 currentPlayerPosition;

        protected KeyboardState keyboard;
        protected KeyboardState prevKeyboard;

        protected MouseState mouse;
        protected MouseState prevMouse;

        public int Health { get; set; }
        
        public int Armor { get; set; }

        public int Damage { get; set; }
        
        public static int Range { get; set; }
        
        public int Mana { get; set; }
        
        public Spell Spell { get; set; }
   
        public float Rate { get; set; }

        public int FiringTimer { get; set; }      
        
        public Hero(int health, int mana, int armor, int damage, float speed, int range, Vector2 pos)
        {
            this.Health = health;
            this.Armor = armor;
            this.Damage = damage;
            this.Speed = speed;
            Range = range;
            this.Mana = mana;
     
            this.IsAlive = true;
            this.Position = pos;
            this.FiringTimer = 0;
            this.Rate = 20;
        }

        public Hero(Vector2 pos)
        {
            this.Position = pos;
        }

        public Hero()
        { 
        
        }

        public override void Update()
        {
            if (!this.IsAlive)
            {
                ChaosGame.gameOverMenu = true;
                return;
            }

            this.area.X = (int)this.Position.X;
            this.area.Y = (int)this.Position.Y;

            keyboard = Keyboard.GetState();
            mouse = Mouse.GetState();

            if (keyboard.IsKeyDown(Keys.W) && !this.Collision(this.Position - (new Vector2(0, (int)this.Speed))))
            {
                this.Position -= new Vector2(0, (int)this.Speed);
            }
            if (keyboard.IsKeyDown(Keys.A) && !this.Collision(this.Position - (new Vector2((int)(int)this.Speed, 0))))
            {
                this.Position -= new Vector2((int)this.Speed, 0);
            }
            if (keyboard.IsKeyDown(Keys.S) && !this.Collision(this.Position + (new Vector2(0, (int)this.Speed))))
            {
                this.Position += new Vector2(0, (int)this.Speed);
            }
            if (keyboard.IsKeyDown(Keys.D) && !this.Collision(this.Position + (new Vector2((int)this.Speed, 0))))
            {
                this.Position += new Vector2((int)this.Speed, 0);
            }

            FiringTimer++;

            if (mouse.LeftButton == ButtonState.Pressed)
            {
                this.CheckShooting((int)this.Rate);
            }
            if (keyboard.IsKeyDown(Keys.C))
            {
                this.CheckShootingSpell((int)Rate + 20);
            }

            this.rotation = PointDirection(this.Position.X, this.Position.Y, mouse.X, mouse.Y);

            var enemyAttack = EnemyAttack(this.Position);
            if (enemyAttack != null)
            {
                this.Health -= enemyAttack.Damage;
            }

            if (this.Health <= 0)
            {
                this.IsAlive = false;
            }

            prevKeyboard = keyboard;
            prevMouse = mouse;
        }

        public virtual Enemy EnemyAttack(Vector2 possition)
        {
            this.area.X = (int)possition.X;
            this.area.Y = (int)possition.Y;

            foreach (var item in ListOfDrawableObjects.AllEnemies)
            {
                Enemy enemy = item;
                if (enemy != null)
                {
                    if (enemy.area.Intersects(this.area))
                    {
                        return item;
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

        public void CheckShooting(int rate)
        {
            if (FiringTimer > rate)
            {
                FiringTimer = 0;
                Shoot();
            }
        }
        public void CheckShootingSpell(int rate)
        {
            if (FiringTimer > rate)
            {
                FiringTimer = 0;
                ShootSpell();
            }
        }

        private void ShootSpell()
        {
            foreach (var bullet in ListOfDrawableObjects.AllDrawableItems)
            {
                if (bullet.GetType() == typeof(BasicSpell) && !bullet.IsAlive)
                {
                    currentPlayerPosition = this.Position;
                    bullet.Position = this.Position;
                    bullet.rotation = this.rotation;
                    bullet.Speed = arrowSpeed;
                    bullet.IsAlive = true;
                    break;
                }
            }
        }

        public void Shoot()
        {
            foreach (var bullet in ListOfDrawableObjects.AllDrawableItems)
            {
                if (bullet.GetType() == typeof(MageSpell) && !bullet.IsAlive)
                {
                    currentPlayerPosition = this.Position;
                    bullet.Position = this.Position;
                    bullet.rotation = this.rotation;
                    bullet.Speed = arrowSpeed;
                    bullet.IsAlive = true;
                    break;
                }
            }
        }
    }
}