// Жига Никита

using System.Drawing;
using Asteroids.Exceptions;
using Asteroids.Interfaces;

namespace Asteroids.BackgroundObjects
{
    public abstract class BackgroundObject : ICollision
    {
        public Point Position;
        protected Point Direction;
        protected Size Size;
        public readonly int Layer;

        protected const int minPositionX = Game.MinWidth;
        protected const int minPositionY = Game.MinHeight;
        protected const int maxPositionX = Game.MaxWidth;
        protected const int maxPositionY = Game.MaxHeight;
        protected const int minSpeed = -100;
        protected const int maxSpeed = 100;
        protected virtual bool CanCollide => false;

        protected BackgroundObject(Point position, Point direction, Size size, int layer)
        {
            if (
                position.X < minPositionX || maxPositionX < position.X ||
                position.Y < minPositionY || maxPositionY < position.Y
            )
                throw new ObjectValueError(
                    $"Wrong object position: {position}. " +
                    $"Allowed in: x ({minPositionX}, {maxPositionX}), y ({minPositionY}, {maxPositionY})"
                );

            if (
                direction.X < minSpeed || maxSpeed < direction.X ||
                direction.Y < minSpeed || maxSpeed < direction.Y
            )
                throw new ObjectValueError(
                    $"Wrong object speed: {direction}. " +
                    $"Allowed: min - {minSpeed}, max - {maxSpeed})"
                );
            if (size.Width < 0 || size.Height < 0)
                throw new ObjectValueError($"Negative size: {size}");

            Position = position;
            Direction = direction;
            Size = size;
            Layer = layer;
        }

        public abstract void Draw();
        public abstract void Update();
        public bool IsCollideWith(ICollision obj) => CanCollide && obj.Rectangle.IntersectsWith(Rectangle);
        public Rectangle Rectangle => new Rectangle(Position, Size);
    }
}