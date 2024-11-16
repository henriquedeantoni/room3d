using OpenTK.Graphics.OpenGL;
using OpenTK.Mathematics;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.Desktop;
using OpenTK.Windowing.GraphicsLibraryFramework;

namespace Room3d.Core
{
    public class MainWindow : GameWindow
    {
        private Vector3 lightPosition = new Vector3(0.0f, 0.0f, -3.0f); // Posição inicial da luz

        public MainWindow(GameWindowSettings gameWindowSettings, NativeWindowSettings nativeWindowSettings)
            : base(gameWindowSettings, nativeWindowSettings)
        {
        }

        protected override void OnLoad()
        {
            base.OnLoad();

            Console.WriteLine("Versão do OpenGL: " + GL.GetString(StringName.Version));

            // Ativa o teste de profundidade para lidar com objetos 3D corretamente
            GL.Enable(EnableCap.DepthTest);
            GL.ClearDepth(1.0f);
            // Ativa a iluminação e a luz
            GL.Enable(EnableCap.Lighting);

            GL.Enable(EnableCap.Light0);

            // Configura a cor padrão para a luz
            float[] lightColor = { 1.0f, 1.0f, 1.0f, 1.0f }; // Luz branca
            GL.Light(LightName.Light0, LightParameter.Diffuse, lightColor);

            // Configura a posição inicial da luz
            float[] lightPositionArray = { lightPosition.X, lightPosition.Y, lightPosition.Z, 1.0f };
            GL.Light(LightName.Light0, LightParameter.Position, lightPositionArray);

            // Define a cor de fundo da tela (azul escuro)
            GL.ClearColor(0.1f, 0.1f, 0.2f, 1.0f); // RGBA
        }

        protected override void OnResize(ResizeEventArgs e)
        {
            base.OnResize(e);

            GL.Viewport(0, 0, Size.X, Size.Y);

            Matrix4 perspective = Matrix4.CreatePerspectiveFieldOfView(
                MathHelper.DegreesToRadians(45.0f),
                Size.X / (float)Size.Y,
                0.1f, // Distância mínima do corte do frustum
                100.0f // Distância máxima do corte do frustum
            );
            GL.MatrixMode(MatrixMode.Projection);
            GL.LoadMatrix(ref perspective);
        }

        protected override void OnRenderFrame(FrameEventArgs args)
        {
            Console.WriteLine("OnRenderFrame chamado");
            Console.WriteLine("Versão do OpenGL: " + GL.GetString(StringName.Version));


            // Limpa o buffer de cores e profundidade
            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);

            // Configura a câmera
            GL.MatrixMode(MatrixMode.Modelview);
            GL.LoadIdentity();
            GL.Translate(0.0f, 0.0f, -7.0f); // Posição da câmera

            // Configura a luz
            float[] lightPos = { lightPosition.X, lightPosition.Y, lightPosition.Z, 1.0f };
            GL.Light(LightName.Light0, LightParameter.Position, lightPos);

            // Desenha os elementos da cena
            DrawRoom();
            Console.WriteLine("Sala desenhada");
            DrawTriangle();
            Console.WriteLine("Triângulo desenhado");

            // Troca os buffers
            SwapBuffers();
        }

        private void DrawRoom()
        {
            Console.WriteLine("Desenhando a sala...");

            GL.Begin(PrimitiveType.Quads);

            // Parede de fundo (vermelho)
            GL.Color3(1.0f, 0.0f, 0.0f);
            GL.Vertex3(-5.0f, -5.0f, -10.0f);
            GL.Vertex3(5.0f, -5.0f, -10.0f);
            GL.Vertex3(5.0f, 5.0f, -10.0f);
            GL.Vertex3(-5.0f, 5.0f, -10.0f);

            // Parede esquerda (verde)
            GL.Color3(0.0f, 1.0f, 0.0f);
            GL.Vertex3(-5.0f, -5.0f, -10.0f);
            GL.Vertex3(-5.0f, -5.0f, 0.0f);
            GL.Vertex3(-5.0f, 5.0f, 0.0f);
            GL.Vertex3(-5.0f, 5.0f, -10.0f);

            // Parede direita (azul)
            GL.Color3(0.0f, 0.0f, 1.0f);
            GL.Vertex3(5.0f, -5.0f, -10.0f);
            GL.Vertex3(5.0f, -5.0f, 0.0f);
            GL.Vertex3(5.0f, 5.0f, 0.0f);
            GL.Vertex3(5.0f, 5.0f, -10.0f);

            // Chão (cinza)
            GL.Color3(0.5f, 0.5f, 0.5f);
            GL.Vertex3(-5.0f, -5.0f, 0.0f);
            GL.Vertex3(5.0f, -5.0f, 0.0f);
            GL.Vertex3(5.0f, -5.0f, -10.0f);
            GL.Vertex3(-5.0f, -5.0f, -10.0f);

            GL.End();
        }

        private void DrawTriangle()
        {
            Console.WriteLine("Desenhando o triângulo...");

            GL.Begin(PrimitiveType.Triangles);

            GL.Color3(1.0f, 0.0f, 0.0f); // Vermelho
            GL.Vertex3(-1.0f, -1.0f, -5.0f);

            GL.Color3(0.0f, 1.0f, 0.0f); // Verde
            GL.Vertex3(1.0f, -1.0f, -5.0f);

            GL.Color3(0.0f, 0.0f, 1.0f); // Azul
            GL.Vertex3(0.0f, 1.0f, -5.0f);

            GL.End();
        }
    }
}
