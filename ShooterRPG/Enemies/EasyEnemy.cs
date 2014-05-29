namespace ShooterRPG.Enemies
{
    using Microsoft.Xna.Framework;

    public class EasyEnemy : Enemy
    {
        public string Name { get; private set; }

        public EasyEnemy(Vector2 position)
            : base(100, 1, 30, 1, 0, position)
        {

        }
    }
}