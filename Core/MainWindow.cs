using OpenTK.Graphics.OpenGL;
using OpenTK.Mathematics;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.Desktop;
using OpenTK.Windowing.GraphicsLibraryFramework;

namespace Room3d.Core
{
    public class MainWindow : GameWindow
    {
        private Vector3 lightPosition = new Vector3(0.0f, 0.0f, 2.0f);

        public MainWindow()
            : base(GameWindowSettings.Default, new NativeWindowSettings()
            {
                ClientSize = new Vector2i(800, 600),
                Title = "Room 3D"
            })
        {
            //eventhandlers , assinatura deles
            Load += OnLoad;
            RenderFrame += OnRenderFrame;
            Resize += OnResize;
            UpdateFrame += OnUpdateFrame;
        }

        protected override void OnLoad()
        {
            //base.OnLoad();

            //cor de fundo azul
            GL.ClearColor(Color4.CornflowerBlue);

            //testa a profundidade
            GL.Enable(EnableCap.DepthTest);

            //ativa iluminação
            GL.Enable(EnableCap.Lighting);
            GL.Enable(EnableCap.Light0);

            // Configuração inicial da luz
            float[] ambientLight = { 0.2f, 0.2f, 0.2f, 1.0f };
            float[] diffuseLight = { 0.8f, 0.8f, 0.8f, 1.0f };

            GL.Light(LightName.Light0, LightParameter.Ambient, ambientLight);
            GL.Light(LightName.Light0, LightParameter.Ambient, diffuseLight);
        }

        protected override void OnRenderFrame(FrameEventArgs args)
        {
            //base.OnRenderFrame(args);

            //limpeza da tela e esvaziamento do buffer de profundidade
            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);
            
            //reset matriz
            GL.MatrixMode(MatrixMode.Modelview);
            GL.LoadIdentity();

            // Ajustar a posição da câmera
            GL.Translate(0.0f, 0.0f, -5.0f);

            // posição da luz
            float[] lightPos = { lightPosition.X, lightPosition.Y, lightPosition.Z, 1.0f };
            GL.Light(LightName.Light0, LightParameter.Position, lightPos);

            /*
            // Renderizar a sala (um cubo simples)
            GL.Begin(PrimitiveType.Quads);
            GL.Color3(0.8f, 0.8f, 0.8f);
            // Frente
            GL.Vertex3(-1, -1, 1);
            GL.Vertex3(1, -1, 1);
            GL.Vertex3(1, 1, 1);
            GL.Vertex3(-1, 1, 1);
            // Outras faces...
            GL.End();
            */

            DrawCube();

            SwapBuffers();
        }

        protected override void OnResize(ResizeEventArgs e)
        {
            //base.OnResize(e);

            // Ajustar o viewport
            GL.Viewport(0, 0, Size.X, Size.Y);

            // Configurar projeção em perspectiva
            GL.MatrixMode(MatrixMode.Projection);
            GL.LoadIdentity();
            Matrix4 perspective = Matrix4.CreatePerspectiveFieldOfView(MathHelper.DegreesToRadians(45f), Size.X / (float)Size.Y, 0.1f, 100f);
            GL.LoadMatrix(ref perspective);
        }

        protected override void OnUpdateFrame(FrameEventArgs args)
        {
            //base.OnUpdateFrame(args);

            // Controle da posição da luz usando teclas (Exemplo: W, S, A, D)
            var input = KeyboardState;
            if (input.IsKeyDown(OpenTK.Windowing.GraphicsLibraryFramework.Keys.W))
                lightPosition.Y += 0.1f;
            if (input.IsKeyDown(OpenTK.Windowing.GraphicsLibraryFramework.Keys.S))
                lightPosition.Y -= 0.1f;
            if (input.IsKeyDown(OpenTK.Windowing.GraphicsLibraryFramework.Keys.A))
                lightPosition.X -= 0.1f;
            if (input.IsKeyDown(OpenTK.Windowing.GraphicsLibraryFramework.Keys.D))
                lightPosition.X += 0.1f;
        }

        private void DrawCube()
        {
            // Define as cores para as faces
            GL.Begin(PrimitiveType.Quads);

            // Frente (branca)
            GL.Color3(1.0f, 1.0f, 1.0f);
            GL.Vertex3(-1.0f, -1.0f, 1.0f);
            GL.Vertex3(1.0f, -1.0f, 1.0f);
            GL.Vertex3(1.0f, 1.0f, 1.0f);
            GL.Vertex3(-1.0f, 1.0f, 1.0f);

            // Trás (vermelha)
            GL.Color3(1.0f, 0.0f, 0.0f);
            GL.Vertex3(-1.0f, -1.0f, -1.0f);
            GL.Vertex3(1.0f, -1.0f, -1.0f);
            GL.Vertex3(1.0f, 1.0f, -1.0f);
            GL.Vertex3(-1.0f, 1.0f, -1.0f);

            // Lateral esquerda (verde)
            GL.Color3(0.0f, 1.0f, 0.0f);
            GL.Vertex3(-1.0f, -1.0f, -1.0f);
            GL.Vertex3(-1.0f, -1.0f, 1.0f);
            GL.Vertex3(-1.0f, 1.0f, 1.0f);
            GL.Vertex3(-1.0f, 1.0f, -1.0f);

            // Lateral direita (azul)
            GL.Color3(0.0f, 0.0f, 1.0f);
            GL.Vertex3(1.0f, -1.0f, -1.0f);
            GL.Vertex3(1.0f, -1.0f, 1.0f);
            GL.Vertex3(1.0f, 1.0f, 1.0f);
            GL.Vertex3(1.0f, 1.0f, -1.0f);

            // Parte de baixo (amarela)
            GL.Color3(1.0f, 1.0f, 0.0f);
            GL.Vertex3(-1.0f, -1.0f, -1.0f);
            GL.Vertex3(1.0f, -1.0f, -1.0f);
            GL.Vertex3(1.0f, -1.0f, 1.0f);
            GL.Vertex3(-1.0f, -1.0f, 1.0f);

            // Parte de cima (ciano)
            GL.Color3(0.0f, 1.0f, 1.0f);
            GL.Vertex3(-1.0f, 1.0f, -1.0f);
            GL.Vertex3(1.0f, 1.0f, -1.0f);
            GL.Vertex3(1.0f, 1.0f, 1.0f);
            GL.Vertex3(-1.0f, 1.0f, 1.0f);

            GL.End();
        }

    }
}
