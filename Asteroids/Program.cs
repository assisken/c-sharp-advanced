// 1. Добавить свои объекты в иерархию объектов, чтобы получился красивый задний фон, похожий на полёт в звёздном пространстве.
// 2. *Заменить кружочки картинками, используя метод DrawImage.
//
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