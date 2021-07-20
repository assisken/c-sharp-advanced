// Жига Никита

using System.Drawing;
using System.Windows.Forms.DataVisualization.Charting;

namespace Asteroids.BackgroundObjects
{
    public abstract class BackgroundObject
    {
        public Point3D Position { get; protected set; }
        protected Point Direction;
        protected Size Size;

        protected BackgroundObject(Point3D position, Point direction, Size size)
        {
            Position = position;
            Direction = direction;
            Size = size;
        }

        public abstract void Draw();
        public abstract void Update();
    }
}