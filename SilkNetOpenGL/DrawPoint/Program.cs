using Silk.NET.Input;
using Silk.NET.Maths;
using Silk.NET.OpenGL;
using Silk.NET.Windowing;

namespace DrawPoint
{
    internal class Program
    {
        private static IWindow window;
        private static GL Gl;
        private static readonly float[] CornflowerBlue = { 0.392f, 0.584f, 0.929f, 1.0f };
        private static readonly string VertexShaderSource = @"
        #version 440 core
        
        void main()
        {
            gl_Position = vec4(0, 0, 0, 1);
        }
        ";

        private static readonly string FragmentShaderSource = @"
        #version 440 core
        out vec4 Color;

        void main()
        {
            Color = vec4(1.0, 0.0, 0.0, 1.0);
        }
        ";
        private static uint Shader;
        private static uint Vao;

        static void Main(string[] args)
        {
            var options = WindowOptions.Default;
            options.Size = new Vector2D<int>(800, 600);
            options.Title = "LearnOpenGL with Silk.NET";
            window = Window.Create(options);

            window.Load += OnLoad;
            window.Render += OnRender;
            window.Update += OnUpdate;
            window.FramebufferResize += OnFramebufferResize;
            window.Closing += OnClose;

            window.Run();

            window.Dispose();
        }

        private static unsafe void OnLoad()
        {
            //IInputContext input = window.CreateInput();
            //for (int i = 0; i < input.Keyboards.Count; i++)
            //{
            //    input.Keyboards[i].KeyDown += KeyDown;
            //}

            Gl = GL.GetApi(window);

            //Creating a vertex shader.
            uint vertexShader = Gl.CreateShader(ShaderType.VertexShader);
            Gl.ShaderSource(vertexShader, VertexShaderSource);
            Gl.CompileShader(vertexShader);

            //Checking the shader for compilation errors.
            string infoLog = Gl.GetShaderInfoLog(vertexShader);
            if (!string.IsNullOrWhiteSpace(infoLog))
            {
                Console.WriteLine($"Error compiling vertex shader {infoLog}");
            }

            //Creating a fragment shader.
            uint fragmentShader = Gl.CreateShader(ShaderType.FragmentShader);
            Gl.ShaderSource(fragmentShader, FragmentShaderSource);
            Gl.CompileShader(fragmentShader);

            //Checking the shader for compilation errors.
            infoLog = Gl.GetShaderInfoLog(fragmentShader);
            if (!string.IsNullOrWhiteSpace(infoLog))
            {
                Console.WriteLine($"Error compiling fragment shader {infoLog}");
            }

            //Combining the shaders under one shader program.
            Shader = Gl.CreateProgram();
            Gl.AttachShader(Shader, vertexShader);
            Gl.AttachShader(Shader, fragmentShader);
            Gl.LinkProgram(Shader);

            //Checking the linking for errors.
            Gl.GetProgram(Shader, GLEnum.LinkStatus, out var status);
            if (status == 0)
            {
                Console.WriteLine($"Error linking shader {Gl.GetProgramInfoLog(Shader)}");
            }

            //Delete the no longer useful individual shaders;
            Gl.DetachShader(Shader, vertexShader);
            Gl.DetachShader(Shader, fragmentShader);
            Gl.DeleteShader(vertexShader);
            Gl.DeleteShader(fragmentShader);

            Vao = Gl.GenVertexArray();
        }

        private static unsafe void OnRender(double obj) //Method needs to be unsafe due to draw elements.
        {
            fixed (float* ptr = CornflowerBlue)
            {
                Gl.ClearBuffer(Silk.NET.OpenGL.GLEnum.Color, 0, ptr);
            }
            Gl.BindVertexArray(Vao);
            Gl.UseProgram(Shader);
            Gl.PointSize(80.0f);
            Gl.DrawArrays(GLEnum.Points, 0, 1);
            Gl.BindVertexArray(0);
        }

        private static void OnUpdate(double obj)
        {

        }

        private static void OnFramebufferResize(Vector2D<int> newSize)
        {
            Gl.Viewport(newSize);
        }

        private static void OnClose()
        {
            //Remember to delete the buffers.
            //Gl.DeleteBuffer(Vbo);
            //Gl.DeleteBuffer(Ebo);
            //Gl.DeleteVertexArray(Vao);
            //Gl.DeleteProgram(Shader);
        }

    }
}
