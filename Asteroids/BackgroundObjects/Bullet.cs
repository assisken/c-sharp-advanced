// Жига Никита

using System;
using System.Drawing;

namespace Asteroids.BackgroundObjects
{
    public class Bullet : BackgroundObject
    {
        public override bool CanCollide => true;

        public Bullet(Point position, Point direction, Size size, int layer, Log logger, Destroyer destroy) : base(position, direction,
            size, layer, logger, destroy)
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

        public override void CollideWith(BackgroundObject obj) => SelectCollisionStrategy(obj)();

        private delegate void CollisionStrategy();

        private CollisionStrategy SelectCollisionStrategy(BackgroundObject obj) => obj switch
        {
            Asteroid asteroid => () =>
            {
                Game.Score += 100;
                asteroid.Destroy();
                Destroy();
            },
            _ => () => { }
        };
    }
}