// Жига Никита

using System.Drawing;

namespace Asteroids.BackgroundObjects
{
    public class Bullet : BackgroundObject
    {
        protected override bool CanCollide => true;
    
        public Bullet(Point position, Point direction, Size size, int layer) : base(position, direction, size, layer)
        {
        }

        public override void Draw()
        {
            Game.Buffer.Graphics.DrawLine(Pens.Orange, Position.X - Size.Width, Position.Y, Position.X, Position.Y);
        }

        public override void Update()
        {
            Position.X += Direction.X;
        }
    }
}