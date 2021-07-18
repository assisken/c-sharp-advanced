// 1. Добавить свои объекты в иерархию объектов, чтобы получился красивый задний фон, похожий на полёт в звёздном пространстве.
// 2. *Заменить кружочки картинками, используя метод DrawImage.
//
// Жига Никита

using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Drawing;
using System.Windows.Forms.DataVisualization.Charting;
using Asteroids.BackgroundObjects;

namespace Asteroids
{
    public static class Game
    {
        private static BufferedGraphicsContext _context;
        public static BufferedGraphics Buffer;
        public static int Width { get; private set; }
        public static int Height { get; private set; }

        private static List<BackgroundObject> _objects;

        public static void Init(Form form)
        {
            _context = BufferedGraphicsManager.Current;
            var g = form.CreateGraphics();

            Width = form.ClientSize.Width;
            Height = form.ClientSize.Height;

            Buffer = _context.Allocate(g, new Rectangle(0, 0, Width, Height));

            var timer = new Timer {Interval = 100};
            timer.Start();
            timer.Tick += Tick;

            Load();
        }

        private static void Tick(object sender, EventArgs e)
        {
            Update();
            Draw();
        }

        private static void Load()
        {
            var random = new Random();
            _objects = new List<BackgroundObject>();

            for (var i = 0; i < 15; i++)
            {
                var x = random.Next(0, Width);
                var y = random.Next(0, Height);
                var xDirection = random.Next(-25, 25);
                var yDirection = random.Next(-25, 25);
                var size = random.Next(10, 40);
                _objects.Add(
                    new Asteroid(new Point3D(x, y, 0), new Point(xDirection, yDirection), new Size(size, size))
                );
            }

            for (var i = 0; i < 15; i++)
            {
                var x = random.Next(0, Width);
                var y = random.Next(0, Height);
                var size = random.Next(1, 5);
                _objects.Add(
                    new Star(new Point3D(x, y, -3), new Point(-i, 0), new Size(size, size))
                );
            }

            for (var i = 0; i < 1; i++)
            {
                var x = random.Next(0, Width);
                var y = random.Next(0, Height);
                var size = random.Next(100, 1000);
                _objects.Add(
                    new Planet(new Point3D(x, y, -1), new Point(-10, 0), new Size(size, size))
                );
            }
            
            for (var i = 0; i < 1; i++)
            {
                var x = random.Next(0, Width);
                var y = random.Next(0, Height);
                var size = random.Next(10, 100);
                _objects.Add(
                    new Sun(new Point3D(x, y, -2), new Point(-5, 0), new Size(size, size))
                );
            }

            _objects.Sort((o1, o2) => o1.Position.Z.CompareTo(o2.Position.Z));
        }

        public static void Draw()
        {
            Buffer.Graphics.Clear(Color.Black);
            foreach (var obj in _objects)
                obj.Draw();
            Buffer.Render();
        }

        private static void Update()
        {
            foreach (var obj in _objects)
                obj.Update();
        }
    }
}