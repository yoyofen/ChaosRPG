namespace ShooterRPG.Enemies
{
    using Microsoft.Xna.Framework;

    public class HardEnemy : Enemy
    {
        public string Name { get; private set; }

        public HardEnemy(Vector2 position)
            : base(450, 2, 60, 3, 0, position)
        {
        
        }
    }
}