// Жига Никита

using System.Drawing;

namespace Asteroids.BackgroundObjects
{
    public class Bullet : Projectile
    {
        public override int hardness => 10;

        public Bullet(Point position, Point direction, Size size, int layer, Log logger, Destroyer destroy) : base(
            position, direction,
            size, layer, logger, destroy)
        {
        }

        public override void Draw()
        {
            Game.Buffer.Graphics.DrawLine(Pens.Orange, Position.X - Size.Width, Position.Y, Position.X, Position.Y);
        }

        public override void Update() => Position.X += Direction.X;
    }
}