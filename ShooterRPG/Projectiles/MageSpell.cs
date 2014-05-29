namespace ShooterRPG.Projectiles
{
    using Microsoft.Xna.Framework;

    using ShooterRPG.Content;
    using ShooterRPG.Heroes;

    public class MageSpell : Projectile
    {
        public MageSpell(Vector2 pos)
            : base()
        {
            this.Position = pos;
            this.SpriteName = Sprites.MageSpell.ToString();
            this.Scale = 1f;
            this.Speed = 10;
            this.area = new Rectangle((int)Position.X, (int)Position.Y, 40, 40);
            this.AtackPower = 30;
        }

        public override void Update()
        {
            if (this.Position.X < 0 || this.Position.X > 800
                || this.Position.Y < 0 || this.Position.Y > 600
                || Vector2.Distance(this.Position, Mage.currentPlayerPosition) > Mage.Range ||
                this.Collision()
                )
            {
                this.IsAlive = false;
            }
            base.Update();
        }
    }
}