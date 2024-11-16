using OpenTK.Graphics.OpenGL;
using OpenTK.Mathematics;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.Desktop;
using OpenTK.Windowing.GraphicsLibraryFramework;
using ErrorCode = OpenTK.Graphics.OpenGL.ErrorCode;

namespace Room3d.Core
{
    public class MainWindow : GameWindow
    {
        private int _vertexArrayObject;
        private int _vertexBufferObject;
        private int _shaderProgram;

        public MainWindow(GameWindowSettings gameWindowSettings, NativeWindowSettings nativeWindowSettings)
            : base(gameWindowSettings, nativeWindowSettings) { }

        protected override void OnLoad()
        {
            base.OnLoad();

            // Configuração inicial do OpenGL
            GL.ClearColor(0.1f, 0.1f, 0.2f, 1.0f); // Cor de fundo

            // Compilar e linkar o programa de shaders
            string vertexShaderSource = @"
                #version 330 core
                layout(location = 0) in vec3 aPos;
                void main()
                {
                    gl_Position = vec4(aPos, 1.0);
                }";

            string fragmentShaderSource = @"
                #version 330 core
                out vec4 FragColor;
                void main()
                {
                    FragColor = vec4(1.0f, 0.0f, 0.0f, 1.0f); // Cor vermelha
                }";

            int vertexShader = CompileShader(ShaderType.VertexShader, vertexShaderSource);
            int fragmentShader = CompileShader(ShaderType.FragmentShader, fragmentShaderSource);
            _shaderProgram = LinkProgram(vertexShader, fragmentShader);

            // Definir os dados do vértice (triângulo)
            float[] vertices = new float[]
            {
                -0.5f, -0.5f, 0.0f,  // Esquina inferior esquerda
                 0.5f, -0.5f, 0.0f,  // Esquina inferior direita
                 0.0f,  0.5f, 0.0f   // Vértice superior
            };

            // Gerar VBO e VAO
            _vertexArrayObject = GL.GenVertexArray();
            _vertexBufferObject = GL.GenBuffer();

            GL.BindVertexArray(_vertexArrayObject);

            GL.BindBuffer(BufferTarget.ArrayBuffer, _vertexBufferObject);
            GL.BufferData(BufferTarget.ArrayBuffer, vertices.Length * sizeof(float), vertices, BufferUsageHint.StaticDraw);

            GL.VertexAttribPointer(0, 3, VertexAttribPointerType.Float, false, 3 * sizeof(float), 0);
            GL.EnableVertexAttribArray(0);

            GL.BindBuffer(BufferTarget.ArrayBuffer, 0);
            GL.BindVertexArray(0);
        }

        protected override void OnRenderFrame(FrameEventArgs args)
        {
            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);

            // Usar o programa de shaders
            GL.UseProgram(_shaderProgram);

            // Ativar o VAO e desenhar o triângulo
            GL.BindVertexArray(_vertexArrayObject);
            GL.DrawArrays(PrimitiveType.Triangles, 0, 3);
            GL.BindVertexArray(0);

            SwapBuffers();
        }

        protected override void OnUnload()
        {
            base.OnUnload();
            GL.DeleteBuffer(_vertexBufferObject);
            GL.DeleteVertexArray(_vertexArrayObject);
            GL.DeleteProgram(_shaderProgram);
        }

        private int CompileShader(ShaderType type, string source)
        {
            int shader = GL.CreateShader(type);
            GL.ShaderSource(shader, source);
            GL.CompileShader(shader);

            string infoLog = GL.GetShaderInfoLog(shader);
            if (!string.IsNullOrEmpty(infoLog))
            {
                Console.WriteLine($"Erro ao compilar shader: {infoLog}");
            }

            return shader;
        }

        private int LinkProgram(int vertexShader, int fragmentShader)
        {
            int program = GL.CreateProgram();
            GL.AttachShader(program, vertexShader);
            GL.AttachShader(program, fragmentShader);
            GL.LinkProgram(program);

            string infoLog = GL.GetProgramInfoLog(program);
            if (!string.IsNullOrEmpty(infoLog))
            {
                Console.WriteLine($"Erro ao linkar o programa: {infoLog}");
            }

            GL.DetachShader(program, vertexShader);
            GL.DetachShader(program, fragmentShader);
            GL.DeleteShader(vertexShader);
            GL.DeleteShader(fragmentShader);

            return program;
        }
    }
}
