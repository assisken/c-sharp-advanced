using System.Drawing;

public interface IProjectile<in T>
{
    public void Collide(T obj);
    public void Destroy();
}

namespace Asteroids.Interfaces
{
}