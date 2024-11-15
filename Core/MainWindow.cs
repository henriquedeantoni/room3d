using OpenTK.Graphics.OpenGL;
using OpenTK.Mathematics;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.Desktop;
using OpenTK.Windowing.GraphicsLibraryFramework;

namespace Room3d.Core
{
    public class MainWindow : GameWindow
    {
        private Vector3 lightPosition = new Vector3(0f, 1f, 0f);

        public MainWindow()
            : base(GameWindowSettings.Default, new NativeWindowSettings()
            {
                Size = new Vector2i(800, 600),
                Title = "Sala 3D"
            })
        {
        }

        protected override void OnLoad()
        {
            base.OnLoad();
            GL.ClearColor(Color4.CornflowerBlue);
            GL.Enable(EnableCap.DepthTest);
        }

        protected override void OnRenderFrame(FrameEventArgs args)
        {
            base.OnRenderFrame(args);

            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);
            GL.LoadIdentity();

            // Configurar iluminação
            float[] lightPos = { lightPosition.X, lightPosition.Y, lightPosition.Z, 1.0f };
            GL.Light(LightName.Light0, LightParameter.Position, lightPos);

            GL.Enable(EnableCap.Lighting);
            GL.Enable(EnableCap.Light0);

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

            SwapBuffers();
        }

        protected override void OnUpdateFrame(FrameEventArgs args)
        {
            base.OnUpdateFrame(args);

            // Lógica para mover a luz
            var input = KeyboardState;
            if (input.IsKeyDown(Keys.Up)) lightPosition.Y += 0.1f;
            if (input.IsKeyDown(Keys.Down)) lightPosition.Y -= 0.1f;
            if (input.IsKeyDown(Keys.Left)) lightPosition.X -= 0.1f;
            if (input.IsKeyDown(Keys.Right)) lightPosition.X += 0.1f;
        }
    }
}
