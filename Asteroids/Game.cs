// Жига Никита

using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Drawing;
using System.Threading;
using Asteroids.BackgroundObjects;
using Asteroids.Exceptions;
using Timer = System.Windows.Forms.Timer;

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
        private static Ship _ship = new Ship(new Point(10, Height / 2), new Point(5, 5), new Size(10, 10), 10);
        private static Timer _timer = new Timer {Interval = 100};

        public static void Init(Form form)
        {
            form.KeyDown += Form_KeyDown;

            if (form.Width < MinWidth || form.Width > MaxWidth)
                throw new UnsupportedWindowSize($"Unsupported width. Supported: from {MinWidth} to {MaxWidth}");

            if (form.Height < MinHeight || form.Height > MaxHeight)
                throw new UnsupportedWindowSize($"Unsupported height. Supported: from {MinHeight} to {MaxHeight}");

            _context = BufferedGraphicsManager.Current;
            var g = form.CreateGraphics();

            Width = form.ClientSize.Width;
            Height = form.ClientSize.Height;

            Buffer = _context.Allocate(g, new Rectangle(0, 0, Width, Height));

            _timer.Start();
            _timer.Tick += Tick;

            Load();
            _ship.MessageDie += Finish;
        }

        private static void Form_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.ControlKey:
                    _objects.Add(
                        new Bullet(
                            new Point(_ship.Rectangle.X + 10, _ship.Rectangle.Y + 4), 
                            new Point(4, 0), 
                            new Size(4, 1), 
                            0
                        )
                    );
                    break;
                case Keys.Up:
                    _ship.Up();
                    break;
                case Keys.Down:
                    _ship.Down();
                    break;
            }
        }

        private static void Tick(object sender, EventArgs e)
        {
            Draw();
            Update();
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

            _ship = new Ship(new Point(0, MaxHeight / 2), new Point(0, 5), new Size(5, 5), 1);
            _objects.Add(_ship);

            _objects.Sort((o1, o2) => o1.Layer.CompareTo(o2.Layer));
        }

        public static void Draw()
        {
            Buffer.Graphics.Clear(Color.Black);
            if (_ship != null)
                Buffer.Graphics.DrawString("Energy: " + _ship.Energy, new Font(FontFamily.GenericSansSerif, 15), Brushes.White, 0, 0);
            foreach (var obj in _objects)
                obj.Draw();
            Buffer.Render();
        }

        public static void Finish()
        {
            _timer.Stop();
            const string gameOverMessage = "GAME OVER";
            const int fontSize = 60;
            var xCenter = Width / 2 - gameOverMessage.Length * fontSize / 2;
            var yCenter = Height / 2 - fontSize;
            Buffer.Graphics.DrawString(gameOverMessage, new Font(FontFamily.GenericSansSerif, 60, FontStyle.Underline), Brushes.White, xCenter, yCenter);
            Buffer.Render();
        }

        private static void Update()
        {
            foreach (var obj in _objects)
                obj.Update();

            ProceedCollisions(new List<Type> {typeof(Bullet), typeof(Ship)});
        }

        private static void ProceedCollisions(ICollection<Type> whitelist)
        {
            foreach (var obj1 in _objects)
            {
                if (!whitelist.Contains(obj1.GetType()))
                    continue;

                foreach (var obj2 in _objects)
                {
                    if (obj1 == obj2)
                        continue;

                    if (obj1.IsCollideWith(obj2))
                    {
                        System.Media.SystemSounds.Hand.Play();
                        obj1.CollideWith(obj2);
                    }
                }
            }
        }
    }
}