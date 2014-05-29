namespace ShooterRPG.Enemies
{
    using Microsoft.Xna.Framework;

    public class MediumEnemy : Enemy
    {
        public string Name { get; private set; }

        public MediumEnemy(Vector2 position)
            : base(350, 1, 50, 2, 0, position)
        {

        }
    }
}
