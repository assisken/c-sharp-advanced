// 1. Добавить свои объекты в иерархию объектов, чтобы получился красивый задний фон, похожий на полёт в звёздном пространстве.
// 2. *Заменить кружочки картинками, используя метод DrawImage.
//
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