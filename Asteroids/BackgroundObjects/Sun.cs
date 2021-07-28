// Жига Никита

using System.Drawing;
using System.IO;

namespace Asteroids.BackgroundObjects
{
    public class Sun : TexturedBackgroundObject
    {
        protected override string TexturePath => "../../Assets/sun.png";

        public Sun(Point position, Point direction, Size size, int layer, Log logger) : base(position, direction, size, layer, logger)
        {
        }

        public override void Update()
        {
            if (Position.X + Size.Width < 0)
                Position.X = Game.Width + Size.Width;

            Position.X += Direction.X;
        }
    }
}