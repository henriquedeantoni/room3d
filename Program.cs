using System;
using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;
using Room3d.Core;


namespace Room3d
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var game = new MainWindow())
            {
                game.Run();
            }
        }
    }
}