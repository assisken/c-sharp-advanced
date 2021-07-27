// Жига Никита

using System;
using System.Drawing;

namespace Asteroids.BackgroundObjects
{
    public class Bullet : BackgroundObject
    {
        public override bool CanCollide => true;
    
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

        public override void CollideWith(BackgroundObject obj)
        {
            var random = new Random();
            var x = random.Next(0, 2) == 0 ? 0 : Game.Width;
            var speed = x > 0 ? -3 : 3;
            var y = random.Next(10, Game.Height - 10);
            
            Position = new Point(x, y);
            Direction = new Point(speed, 0);
        }
    }
}