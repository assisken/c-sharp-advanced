// Жига Никита

using System.Drawing;
using System.IO;

namespace Asteroids.BackgroundObjects
{
    public class Asteroid : BackgroundObject
    {
        private readonly Bitmap _texture;
        protected override bool CanCollide => true;

        public Asteroid(Point position, Point direction, Size size, int layer) : base(position, direction, size, layer)
        {
            using (var fs = new FileStream("../../Assets/asteroid.png", FileMode.Open))
                _texture = new Bitmap(fs);
        }

        public override void Draw()
        {
            Game.Buffer.Graphics.DrawImage(_texture, Position.X, Position.Y, Size.Width, Size.Height);
        }

        public override void Update()
        {
            if (Position.X < 0)
                Direction.X = -Direction.X;
            if (Position.X > Game.Width)
                Direction.X = -Direction.X;
            if (Position.Y < 0)
                Direction.Y = -Direction.Y;
            if (Position.Y > Game.Height)
                Direction.Y = -Direction.Y;

            Position.X += Direction.X;
            Position.Y += Direction.Y;
        }
    }
}