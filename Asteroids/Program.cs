// Жига Никита

using System;
using System.IO;
using System.Windows.Forms;

namespace Asteroids
{
    static class Program
    {
        [STAThread]
        static void Main()
        {
            var form = new Form {Width = 800, Height = 600};
            using (var fs = new FileStream("../../logs.txt", FileMode.Open))
            {
                Game.File = new StreamWriter(fs);
                Game.Init(form);
                form.Show();
                Game.Draw();
                Application.Run(form);
            }
        }
    }
}