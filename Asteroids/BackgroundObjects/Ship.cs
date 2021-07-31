using System;
using System.Drawing;

namespace Asteroids.BackgroundObjects
{
    public class Ship : Projectile
    {
        private readonly Bitmap _texture = TextureLoader.LoadTextureFromFile("../../Assets/ship.png");
        public override int hardness => 50;
        public event Message MessageDie;
        public int Energy { get; private set; } = 100;

        public void EnergyLow(int n) => Energy -= n;
        public override bool CanCollide => true;

        public Ship(Point position, Point direction, Size size, int layer, Log logger, Destroyer destroy) : base(
            position, direction, size,
            layer, logger, destroy)
        {
        }

        public override void Draw() =>
            Game.Buffer.Graphics.DrawImage(_texture, Position.X, Position.Y, Size.Width, Size.Height);

        public override void Update()
        {
        }

        public void Up()
        {
            if (Position.Y > 0)
                Position.Y -= Direction.Y;
        }

        public void Down()
        {
            if (Position.Y < Game.Height)
                Position.Y += Direction.Y;
        }

        public override void Collide(Projectile obj)
        {
            if (Energy <= 0)
                base.Collide(obj);

            var random = new Random();
            EnergyLow(obj.hardness);
        }

        public void Heal(int hp) => Energy += hp;

        public override void Destroy()
        {
            onEvent("Ship has died");
            MessageDie?.Invoke();
            base.Destroy();
        }
    }
}