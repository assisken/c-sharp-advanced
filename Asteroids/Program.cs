// Жига Никита

using System;
using System.Windows.Forms;

namespace Asteroids
{
    static class Program
    {
        [STAThread]
        static void Main()
        {
            var form = new Form {Width = 800, Height = 600};
            Game.Init(form);
            form.Show();
            Game.Draw();
            Application.Run(form);
        }
    }
}