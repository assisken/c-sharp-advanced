using System;
using System.Drawing;
using Asteroids.Interfaces;

namespace Asteroids.BackgroundObjects
{
    public class Medkit : Projectile, IHeal
    {
        private readonly Bitmap _texture = TextureLoader.LoadTextureFromFile("../../Assets/medkit.png");
        public override int hardness => 0;
        private event IHeal.HealMessage HealMessage;

        public Medkit(Point position, Point direction, Size size, int layer, Log logger, Destroyer destroy,
            IHeal.HealMessage heal) : base(position, direction, size, layer, logger, destroy)
        {
            HealMessage += heal;
        }

        public override void Draw() =>
            Game.Buffer.Graphics.DrawImage(_texture, Position.X, Position.Y, Size.Width, Size.Height);

        public override void Update()
        {
            Position.X += Direction.X;
            if (Position.X <= -Size.Width)
                Destroy();
        }

        public override void Destroy()
        {
            var random = new Random();
            var y = random.Next(10, Game.Height - 10);

            Position = new Point(Game.Width, y);
        }

        public void Heal() => HealMessage?.Invoke(10);

        public override void Collide(Projectile obj)
        {
            if (obj.GetType() == typeof(Ship))
                Heal();
            
            base.Collide(obj);
        }
    }
}