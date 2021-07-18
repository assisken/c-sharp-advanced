using System.Drawing;

namespace Asteroids.Interfaces
{
    public interface ICollision
    {
        bool IsCollideWith(ICollision obj);
        Rectangle Rectangle { get; }
    }
}