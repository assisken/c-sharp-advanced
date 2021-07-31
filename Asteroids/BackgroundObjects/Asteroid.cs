// Жига Никита

using System.Drawing;
using Asteroids.Interfaces;

namespace Asteroids.BackgroundObjects
{
    public class Asteroid : Projectile, ITarget
    {
        private readonly Bitmap _texture = TextureLoader.LoadTextureFromFile("../../Assets/asteroid.png");
        public override int hardness => 10;
        private event ITarget.HitMessage HitMessage;

        public Asteroid(Point position, Point direction, Size size, int layer, Log logger, Destroyer destroy,
            ITarget.HitMessage hit) : base(
            position, direction, size, layer, logger, destroy)
        {
            HitMessage += hit;
        }

        public override void Draw() =>
            Game.Buffer.Graphics.DrawImage(_texture, Position.X, Position.Y, Size.Width, Size.Height);

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

        public void Hit() => HitMessage?.Invoke(100);

        public override void Destroy()
        {
            Hit();
            base.Destroy();
        }
    }
}