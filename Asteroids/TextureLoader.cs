using System.Drawing;
using System.IO;

namespace Asteroids
{
    public static class TextureLoader
    {
        public static Bitmap LoadTextureFromFile(string path)
        {
            using (var fs = new FileStream(path, FileMode.Open))
                return new Bitmap(fs);
        }

    }
}