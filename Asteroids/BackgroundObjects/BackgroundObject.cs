// Жига Никита

using System.Drawing;
using Asteroids.Exceptions;

namespace Asteroids.BackgroundObjects
{
    public abstract class BackgroundObject
    {
        protected Point Position;
        protected Point Direction;
        public Size Size;
        public readonly int Layer;

        protected const int minPositionX = Game.MinWidth;
        protected const int minPositionY = Game.MinHeight;
        protected const int maxPositionX = Game.MaxWidth;
        protected const int maxPositionY = Game.MaxHeight;
        protected const int minSpeed = -100;
        protected const int maxSpeed = 100;
        public virtual bool CanCollide => false;

        public delegate void Message();

        public delegate void Log(string msg);

        protected event Log Event;

        protected void onEvent(string msg) => Event?.Invoke(msg);

        protected BackgroundObject(Point position, Point direction, Size size, int layer, Log logger)
        {
            Event += logger;
            Event?.Invoke($"Creating object {GetType().Name} at {position}");

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
    }
}