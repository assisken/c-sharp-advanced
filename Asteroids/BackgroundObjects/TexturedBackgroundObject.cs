using System.Drawing;
using System.IO;

namespace Asteroids.BackgroundObjects
{
    public abstract class TexturedBackgroundObject : BackgroundObject
    {
        private readonly Bitmap _texture;
        protected abstract string TexturePath { get; }

        public TexturedBackgroundObject(Point position, Point direction, Size size, int layer, Log logger, Destroyer destroy) : base(position, direction, size, layer, logger, destroy)
        {
            using (var fs = new FileStream(TexturePath, FileMode.Open))
                _texture = new Bitmap(fs);
        }

        public override void Draw() => Game.Buffer.Graphics.DrawImage(_texture, Position.X, Position.Y, Size.Width, Size.Height);
    }
}