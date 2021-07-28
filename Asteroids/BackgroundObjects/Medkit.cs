using System;
using System.Drawing;

namespace Asteroids.BackgroundObjects
{
    public class Medkit : TexturedBackgroundObject
    {
        protected override string TexturePath => "../../Assets/medkit.png";

        public Medkit(Point position, Point direction, Size size, int layer, Log logger) : base(position, direction, size, layer, logger)
        {
        }
        public override void Update() {
            Position.X += Direction.X;
            if (Position.X <= -Size.Width)
                Consume();
        }

        public void Consume()
        {
            var random = new Random();
            var y = random.Next(10, Game.Height - 10);
            
            Position = new Point(Game.Width, y);
        }

        public override bool CanCollide => true;
    }
}