// Жига Никита

using System.Drawing;
using System.Windows.Forms.DataVisualization.Charting;
using System.IO;

namespace Asteroids.BackgroundObjects
{
    public class Planet : BackgroundObject
    {
        private readonly Bitmap _texture;
        public Planet(Point3D position, Point direction, Size size) : base(position, direction, size)
        {
            using (var fs = new FileStream("../../Assets/planet.png", FileMode.Open))
                _texture = new Bitmap(fs);
        }

        public override void Draw()
        {
            Game.Buffer.Graphics.DrawImage(_texture, Position.X, Position.Y, Size.Width, Size.Height);
        }

        public override void Update()
        {
            if (Position.X + Size.Width < 0)
                Position.X = Game.Width + Size.Width;
            
            Position.X += Direction.X;
        }
    }
}