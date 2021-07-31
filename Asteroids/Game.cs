// Жига Никита

using System;
using System.Windows.Forms;
using System.Drawing;
using System.IO;
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

        private static readonly Timer _timer = new() {Interval = 100};
        private static ObjectPool _objectPool;
        public static int Score;

        public static void Init(Form form)
        {
            Log("Initializing game...");
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

            _objectPool = new ObjectPool(Log, Hit, Finish);
            _objectPool.Load();
        }

        private static void Form_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.ControlKey:
                    _objectPool.CreateBullet();
                    break;
                case Keys.Up:
                    _objectPool.Ship.Up();
                    break;
                case Keys.Down:
                    _objectPool.Ship.Down();
                    break;
            }
        }

        private static void Tick(object sender, EventArgs e)
        {
            Draw();
            Update();
        }

        public static void Draw()
        {
            Buffer.Graphics.Clear(Color.Black);
            _objectPool.DrawAll();
            if (_objectPool.Ship != null)
                Buffer.Graphics.DrawString(
                    $"Score: {Score}\nEnergy: {_objectPool.Ship.Energy}",
                    new Font(FontFamily.GenericSansSerif, 15),
                    Brushes.White, 0, 0
                );
            Buffer.Render();
        }

        public static void Finish()
        {
            _timer.Stop();
            const string gameOverMessage = "GAME OVER";
            const int fontSize = 60;
            var xCenter = Width / 2 - gameOverMessage.Length * fontSize / 2;
            var yCenter = Height / 2 - fontSize;
            Draw();
            Buffer.Graphics.DrawString(gameOverMessage, new Font(FontFamily.GenericSansSerif, 60, FontStyle.Underline),
                Brushes.White, xCenter, yCenter);
            Buffer.Render();
            Log("Game was finished");
        }

        private static void Update()
        {
            _objectPool.UpdateAll();
            _objectPool.ProceedCollisions();
        }

        public static StreamWriter File = null;

        private static void Log(string e)
        {
            Console.Write(e);
            File?.WriteLine(e);
            File?.Flush();
        }

        private static void Hit(int score) => Score += score;
    }
}