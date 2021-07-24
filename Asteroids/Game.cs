// Жига Никита

using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Drawing;
using Asteroids.BackgroundObjects;
using Asteroids.Exceptions;

namespace Asteroids
{
    public static class Game
    {
        private static BufferedGraphicsContext _context;
        public static BufferedGraphics Buffer;
        public static int Width { get; private set; }
        public static int Height { get; private set; }

        public const int MinWidth = 0;
        public const int MaxWidth = 800;
        public const int MinHeight = 0;
        public const int MaxHeight = 600;

        private static List<BackgroundObject> _objects;

        public static void Init(Form form)
        {
            if (form.Width < MinWidth || form.Width > MaxWidth)
                throw new UnsupportedWindowSize($"Unsupported width. Supported: from {MinWidth} to {MaxWidth}");

            if (form.Height < MinHeight || form.Height > MaxHeight)
                throw new UnsupportedWindowSize($"Unsupported height. Supported: from {MinHeight} to {MaxHeight}");

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
                    new Bullet(new Point(0 + i * 8, bulletsY), new Point(3, 0), new Size(5, 1), 0)
                );
            }

            _objects.Sort((o1, o2) => o1.Layer.CompareTo(o2.Layer));
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

            ProceedCollisions(new List<Type> {typeof(Bullet)});
        }

        private static void ProceedCollisions(ICollection<Type> whitelist)
        {
            for (var i = 0; i < _objects.Count; i++)
            {
                if (!whitelist.Contains(_objects[i].GetType()))
                    continue;

                for (var j = 0; j < _objects.Count; j++)
                {
                    if (j == i)
                        continue;

                    if (_objects[i].IsCollideWith(_objects[j]))
                    {
                        System.Media.SystemSounds.Hand.Play();
                        _objects[i] = SpawnNewBullet();
                    }
                }
            }
        }

        private static Bullet SpawnNewBullet()
        {
            var random = new Random();
            var x = random.Next(0, 2) == 0 ? 0 : Width;
            var speed = x > 0 ? -3 : 3;
            var y = random.Next(10, Height - 10);
            return new Bullet(new Point(x, y), new Point(speed, 0), new Size(5, 1), 0);
        }
    }
}