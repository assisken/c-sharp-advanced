// Жига Никита

using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Drawing;
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
        private static Dictionary<string, List<BackgroundObject>> _objectsByName;

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
            _objectsByName = new Dictionary<string, List<BackgroundObject>>();

            for (var i = 0; i < 15; i++)
            {
                var x = random.Next(0, Width);
                var y = random.Next(0, Height);
                var xDirection = random.Next(-25, 25);
                var yDirection = random.Next(-25, 25);
                var size = random.Next(10, 40);
                _objects.Add(
                    new Asteroid(new Point(x, y), new Point(xDirection, yDirection), new Size(size, size), 0)
                );
            }

            for (var i = 0; i < 15; i++)
            {
                var x = random.Next(0, Width);
                var y = random.Next(0, Height);
                var size = random.Next(1, 5);
                _objects.Add(
                    new Star(new Point(x, y), new Point(-i, 0), new Size(size, size), -3)
                );
            }

            for (var i = 0; i < 1; i++)
            {
                var x = random.Next(0, Width);
                var y = random.Next(0, Height);
                var size = random.Next(100, 1000);
                _objects.Add(
                    new Planet(new Point(x, y), new Point(-10, 0), new Size(size, size), -1)
                );
            }

            for (var i = 0; i < 1; i++)
            {
                var x = random.Next(0, Width);
                var y = random.Next(0, Height);
                var size = random.Next(10, 100);
                _objects.Add(
                    new Sun(new Point(x, y), new Point(-5, 0), new Size(size, size), -2)
                );
            }

            var bulletsY = random.Next(10, Height - 10);
            for (var i = 0; i < 5; i++)
            {
                _objects.Add(
                    new Bullet(new Point(0 - i * 8, bulletsY), new Point(3, 0), new Size(5, 1), 0)
                );
            }

            _objects.Sort((o1, o2) => o1.Layer.CompareTo(o2.Layer));

            foreach (var obj in _objects)
            {
                if (!_objectsByName.TryGetValue(obj.GetType().Name, out var list))
                {
                    list = new List<BackgroundObject>();
                    _objectsByName[obj.GetType().Name] = list;
                }

                list.Add(obj);
            }
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

            if (
                _objectsByName.TryGetValue(nameof(Bullet), out var bullets)
                && _objectsByName.TryGetValue(nameof(Asteroid), out var asteroids)
            )
                foreach (var bullet in bullets)
                foreach (var asteroid in asteroids)
                    if (bullet.IsCollideWith(asteroid))
                        System.Media.SystemSounds.Hand.Play();
        }
    }
}