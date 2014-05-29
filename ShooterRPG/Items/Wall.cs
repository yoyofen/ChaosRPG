namespace ShooterRPG.Items
{
    using Microsoft.Xna.Framework;

    public class Wall : Item
    {
        public Wall(Rectangle area)
        {
            this.Scale = 1f;
            this.IsAlive = false;
            this.isSolid = true;
            this.area = area;
        }
    }
}