using System;
using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;
using OpenTK.Mathematics;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.Desktop;
using Room3d.Core;


namespace Room3d
{
    internal static class Program
    {
        public static void Main(string[] args)
        {
            // Configurações da janela do jogo
            var gameWindowSettings = GameWindowSettings.Default;

            var nativeWindowSettings = new NativeWindowSettings
            {
                ClientSize = new Vector2i(800, 600), // Largura e altura da janela
                Title = "Simulação 3D - Ambiente e Triângulo", // Título da janela
                // Define a versão do OpenGL e o perfil para compatibilidade
                API = ContextAPI.OpenGL,
                APIVersion = new Version(3, 3),
                Profile = ContextProfile.Core,
                Flags = ContextFlags.ForwardCompatible
            };

            // Inicializa a janela principal
            using (var window = new MainWindow(gameWindowSettings, nativeWindowSettings))
            {
                window.Run(); // Inicia o loop principal
            }
        }
    }
}

/*

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

*/