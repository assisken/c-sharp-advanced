// Жига Никита

using System.Drawing;
using Asteroids.Interfaces;

namespace Asteroids.BackgroundObjects
{
    public abstract class BackgroundObject : ICollision
    {
        public Point Position;
        protected Point Direction;
        protected Size Size;
        public readonly int Layer;

        protected BackgroundObject(Point position, Point direction, Size size, int layer)
        {
            Position = position;
            Direction = direction;
            Size = size;
            Layer = layer;
        }

        public abstract void Draw();
        public abstract void Update();
        public bool IsCollideWith(ICollision obj) => obj.Rectangle.IntersectsWith(Rectangle);
        public Rectangle Rectangle => new Rectangle(Position, Size);
    }
}