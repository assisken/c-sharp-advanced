using System.Drawing;
using System.Windows.Forms.DataVisualization.Charting;

namespace Asteroids.BackgroundObjects
{
    public class Bullet : BackgroundObject
    {
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