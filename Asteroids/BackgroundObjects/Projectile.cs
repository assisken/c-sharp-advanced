using System.Drawing;
using Asteroids.Interfaces;

namespace Asteroids.BackgroundObjects
{
    public abstract class Projectile : BackgroundObject, IProjectile<Projectile>
    {
        public abstract int hardness { get; }
        public Rectangle Rectangle => new(Position, Size);

        public delegate void Destroyer(Projectile obj);

        protected event Destroyer DestroyMessage;

        public virtual void Destroy() => DestroyMessage?.Invoke(this);

        public bool IntersectsWith(Projectile obj) => Rectangle.IntersectsWith(obj.Rectangle);

        public virtual void Collide(Projectile obj)
        {
            if (hardness > obj.hardness)
                obj.Destroy();
            else if (hardness < obj.hardness)
                Destroy();
            else if (hardness == obj.hardness && GetType() != obj.GetType())
            {
                Destroy();
                obj.Destroy();
            }
        }

        protected Projectile(Point position, Point direction, Size size, int layer, Log logger, Destroyer destroyer) :
            base(position, direction, size, layer, logger)
        {
            DestroyMessage += destroyer;
        }
    }
}