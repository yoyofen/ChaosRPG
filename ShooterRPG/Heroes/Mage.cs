namespace ShooterRPG.Heroes
{
    using System;

    using Microsoft.Xna.Framework;

    using ShooterRPG.Content;

    public class Mage : Hero
    {
        public Mage(string name)
            : base(600, 550, 15, 60, 5, 350, new Vector2(30, 30))
        {
            this.Name = name;
            this.SpriteName = Sprites.Mage.ToString();
            this.SpriteShadow = Sprites.MageShadow.ToString();
            this.Scale = 0.8f;
            this.area = new Rectangle((int)Position.X, (int)Position.Y, 80, 80);
        }
    }
}