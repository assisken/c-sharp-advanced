using System;
using System.Drawing;

namespace Asteroids.BackgroundObjects
{
    public class Ship : TexturedBackgroundObject
    {
        public event Message MessageDie;
        private int _energy = 100;
        public int Energy => _energy;

        public void EnergyLow(int n) => _energy -= n;
        protected override string TexturePath => "../../Assets/ship.png";
        public override bool CanCollide => true;

        public Ship(Point position, Point direction, Size size, int layer, Log logger, Destroyer destroy) : base(position, direction, size,
            layer, logger, destroy)
        {
        }

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

        public void Die()
        {
            onEvent("Ship has died");
            MessageDie?.Invoke();
        }

        public override void CollideWith(BackgroundObject obj) => SelectCollisionStrategy(obj)();

        private delegate void CollisionStrategy();

        private CollisionStrategy SelectCollisionStrategy(BackgroundObject obj) => obj switch
        {
            Asteroid => () =>
            {
                var random = new Random();
                EnergyLow(random.Next(1, 11));
                if (_energy <= 0)
                {
                    _energy = 0;
                    Die();
                }
            },
            Medkit medkit => () =>
            {
                medkit.Consume();
                _energy += 10;
            },
            _ => () => { }
        };
    }
}