namespace ShooterRPG
{
    using System;

    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Input;

    using ShooterRPG.Content;

    public class Cursor : DrawableObject, IMovable
    {
        MouseState mouse;

        public Cursor(Vector2 initialPosition)
        {
            this.Position = initialPosition;
            this.SpriteName = Sprites.Cursor.ToString();
            this.Scale = 0.5f;
            this.IsAlive = true;
        }

        public override void Update()
        {
            mouse = Mouse.GetState();
            this.Position = new Vector2(mouse.X, mouse.Y);
        }
    }
}