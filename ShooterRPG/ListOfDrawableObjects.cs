namespace ShooterRPG
{
    using System;
    using System.Collections.Generic;

    using Microsoft.Xna.Framework;

    using ShooterRPG.Enemies;
    using ShooterRPG.Heroes;
    using ShooterRPG.Items;
    using ShooterRPG.Projectiles;
    
    public static class ListOfDrawableObjects
    {
        public static Random rnd = new Random();
        public static IList<DrawableObject> AllDrawableItems { get; private set; }
        
        public static IList<Enemy> AllEnemies { get; private set; }

        public static Hero CurrentHero { get; set; }

        public static void Initialize()
        {
            AllDrawableItems = new List<DrawableObject>();

            AllEnemies = new List<Enemy>();

            for (int i = 0; i < 6; i++)
            {
                var arrow = new Arrows(new Vector2(0, 0));
                arrow.IsAlive = false;
                AllDrawableItems.Add(arrow);
            }
            for (int i = 0; i < 6; i++)
            {
                var basicSpell = new BasicSpell(new Vector2(0, 0));
                basicSpell.IsAlive = false;
                AllDrawableItems.Add(basicSpell);
            }
            for (int i = 0; i < 6; i++)
            {
                var arrow = new MageSpell(new Vector2(0, 0));
                arrow.IsAlive = false;
                AllDrawableItems.Add(arrow);
            }

            AllDrawableItems.Add(new Wall(new Rectangle(180, 0, 16, 435)));
            AllDrawableItems.Add(new Wall(new Rectangle(180, 435, 32, 32)));
            AllDrawableItems.Add(new Wall(new Rectangle(445, 240, 8, 360)));
            AllDrawableItems.Add(new Wall(new Rectangle(708, 0, 13, 435)));
            AllDrawableItems.Add(new Wall(new Rectangle(692, 435, 25, 32)));

            AllDrawableItems.Add(new Wall(new Rectangle(0, 0, 800, 0)));
            AllDrawableItems.Add(new Wall(new Rectangle(0, 675, 800, 675)));
            AllDrawableItems.Add(new Wall(new Rectangle(875, 0, 875, 600)));
            AllDrawableItems.Add(new Wall(new Rectangle(0, 0, 0, 600)));

            AllDrawableItems.Add(new Cursor(new Vector2(150, 250)));

            AllEnemies.Add(new EasyEnemy(new Vector2(rnd.Next(600,700), rnd.Next(300,500))));
        }
    }
}