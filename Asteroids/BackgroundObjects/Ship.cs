using System.Drawing;

namespace Asteroids.BackgroundObjects
{
    public class Ship : BackgroundObject
    {
        public event Message MessageDie;
        private int _energy = 100;
        public int Energy => _energy;

        public void EnergyLow(int n) => _energy -= n;
        public override bool CanCollide => true;

        public Ship(Point position, Point direction, Size size, int layer) : base(position, direction, size, layer)
        {
        }

        public override void Draw() => Game.Buffer.Graphics.FillEllipse(Brushes.Wheat, Position.X, Position.Y, Size.Width, Size.Height);

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

        public void Die() => MessageDie?.Invoke();
        public override void CollideWith(BackgroundObject obj) => Die();
    }
}