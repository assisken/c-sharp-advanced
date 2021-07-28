// Жига Никита

using System;
using System.Drawing;

namespace Asteroids.BackgroundObjects
{
    public class Asteroid : TexturedBackgroundObject
    {
        protected override string TexturePath => "../../Assets/asteroid.png";
        public override bool CanCollide => true;

        public Asteroid(Point position, Point direction, Size size, int layer, Log logger, Destroyer destroy) : base(position, direction, size, layer, logger, destroy)
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
        
        public void Destroy()
        {
            var random = new Random();
            var y = random.Next(10, Game.Height - 10);
            
            Position = new Point(Game.Width, y);
        }
    }
}