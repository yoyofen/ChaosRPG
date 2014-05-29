namespace ShooterRPG.Items
{
    using Microsoft.Xna.Framework;

    using ShooterRPG.Content;

    public class Health : Item
    {
        public bool isPicked = false;
        public int healthValue = 100;

        public Health (Vector2 poss)
        {
            this.Position = poss;
            this.SpriteName = Sprites.Health.ToString();
            this.Scale = 0.1f;
            this.IsAlive = false;
        }
    }
}
