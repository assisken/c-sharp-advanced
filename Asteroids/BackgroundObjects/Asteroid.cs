// Жига Никита

using System.Drawing;
using System.IO;

namespace Asteroids.BackgroundObjects
{
    public class Asteroid : TexturedBackgroundObject
    {
        protected override string TexturePath => "../../Assets/asteroid.png";
        public override bool CanCollide => true;

        public Asteroid(Point position, Point direction, Size size, int layer, Log logger) : base(position, direction, size, layer, logger)
        {
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