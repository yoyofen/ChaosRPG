namespace ShooterRPG.Heroes
{
    using System;
 
    using Microsoft.Xna.Framework;
    
    using ShooterRPG.Content;

    public class Archer : Hero
    {
        public Archer(string name)
            : base(800, 450, 20, 50, 5, 300, new Vector2(40, 40))
        {
            this.Name = name;
            this.SpriteName = Sprites.Archer.ToString();
            this.SpriteShadow = Sprites.ArcherShadow.ToString();
            this.Scale = 0.8f;
            this.area = new Rectangle((int)Position.X, (int)Position.Y, 80, 80);
        }
    }
}