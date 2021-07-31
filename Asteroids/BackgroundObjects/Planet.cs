// Жига Никита

using System.Drawing;

namespace Asteroids.BackgroundObjects
{
    public class Planet : BackgroundObject
    {
        private readonly Bitmap _texture = TextureLoader.LoadTextureFromFile("../../Assets/planet.png");

        public Planet(Point position, Point direction, Size size, int layer, Log logger) : base(position, direction, size, layer, logger)
        {
        }

        public override void Draw() =>
            Game.Buffer.Graphics.DrawImage(_texture, Position.X, Position.Y, Size.Width, Size.Height);

        public override void Update()
        {
            if (Position.X + Size.Width < 0)
                Position.X = Game.Width + Size.Width;
            
            Position.X += Direction.X;
        }
    }
}