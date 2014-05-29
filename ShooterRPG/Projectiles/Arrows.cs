namespace ShooterRPG.Projectiles
{
    using Microsoft.Xna.Framework;

    using ShooterRPG.Content;
    using ShooterRPG.Heroes;

    public class Arrows : Projectile
    {
        public Arrows(Vector2 pos)
            : base()
        {
            this.Position = pos;
            this.SpriteName = Sprites.Arrow.ToString();
            this.Scale = 0.3f;
            this.Speed = 10;
            this.area = new Rectangle((int)Position.X, (int)Position.Y, 22, 5);
            this.AtackPower = 22;
        }

        public override void Update()
        {
            if (this.Position.X < 0 || this.Position.X > 800 ||
                this.Position.Y < 0 || this.Position.Y > 600 ||
                Vector2.Distance(this.Position,Archer.currentPlayerPosition)>Archer.Range ||
                this.Collision())
            {
                this.IsAlive = false;
                this.Position = new Vector2(-50, -50);
            }
            base.Update();
        }
    }
}
