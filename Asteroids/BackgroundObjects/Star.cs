// Жига Никита

using System.Drawing;
using System.Windows.Forms.DataVisualization.Charting;

namespace Asteroids.BackgroundObjects
{
    public class Star : BackgroundObject
    {
        public Star(Point3D position, Point direction, Size size) : base(position, direction, size)
        {
        }

        public override void Draw()
        {
            Game.Buffer.Graphics.DrawLine(Pens.White, Position.X - Size.Width, Position.Y - Size.Height, Position.X + Size.Width, Position.Y + Size.Height);
            Game.Buffer.Graphics.DrawLine(Pens.White, Position.X + Size.Width, Position.Y - Size.Height, Position.X - Size.Width, Position.Y + Size.Height);
        }
        public override void Update()
        {
            if (Position.X < 0)
                Position.X = Game.Width + Size.Width;
            
            Position.X += Direction.X;
        }
    }
}