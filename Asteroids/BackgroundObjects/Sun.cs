// Жига Никита

using System.Drawing;

namespace Asteroids.BackgroundObjects
{
    public class Sun : TexturedBackgroundObject
    {
        protected override string TexturePath => "../../Assets/sun.png";

        public Sun(Point position, Point direction, Size size, int layer, Log logger, Destroyer destroy) : base(position, direction, size, layer, logger, destroy)
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