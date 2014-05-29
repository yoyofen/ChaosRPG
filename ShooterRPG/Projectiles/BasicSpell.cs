namespace ShooterRPG.Projectiles
{
    using Microsoft.Xna.Framework;

    using ShooterRPG.Content;
    using ShooterRPG.Heroes;

    public class BasicSpell : Projectile
    {
        public BasicSpell(Vector2 pos)
            : base()
        {
            this.Position = pos;
            this.SpriteName = Sprites.BasicSpell.ToString();
            this.Scale = 0.7f;
            this.Speed = 5;
            this.area = new Rectangle((int)Position.X, (int)Position.Y, 40, 25);
            this.AtackPower = 35;
        }

        public override void Update()
        {
            if (this.Position.X < 0 || this.Position.X > 800 ||
                this.Position.Y < 0 || this.Position.Y > 600 ||
                Vector2.Distance(this.Position, Archer.currentPlayerPosition) > Archer.Range - 100 ||
                this.Collision()
                )
            {
                this.IsAlive = false;
            }
            base.Update();
        }
    }
}