namespace ShooterRPG
{
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Graphics;
    
    public interface IDrawable
    {
        Vector2 Position { get; set; }

        void Draw(SpriteBatch spriteBatch);
    }
}
