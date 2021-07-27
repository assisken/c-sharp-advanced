using System.Drawing;

namespace Asteroids.Interfaces
{
    public interface ICollision
    {
        bool IsCollideWith(ICollision obj);
        bool CanCollide { get; }
        Rectangle Rectangle { get; }
    }
}