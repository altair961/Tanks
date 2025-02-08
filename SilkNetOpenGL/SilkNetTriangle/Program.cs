using Silk.NET.Maths;
using Silk.NET.OpenGL;
using Silk.NET.Windowing;

namespace SilkNetTriangle
{
    internal class Program
    {
        private static IWindow window;
        private static GL Gl;
        private static uint Vao;
        private static uint Vbo;
        //Vertex shaders are run on each vertex.
        private static readonly string VertexShaderSource = @"
        #version 330 core //Using version GLSL version 3.3
        layout (location = 0) in vec4 vPos;
        
        void main()
        {
            gl_Position = vec4(vPos.x, vPos.y, vPos.z, 1.0);
        }
        ";

        //Fragment shaders are run on each fragment/pixel of the geometry.
        private static readonly string FragmentShaderSource = @"
        #version 330 core
        out vec4 FragColor;

        void main()
        {
            FragColor = vec4(1.0f, 0.5f, 0.2f, 1.0f);
        }
        ";
        private static uint Shader;

        private static void Main(string[] args)
        {
            Console.WriteLine("Hello, World!");
            var options = WindowOptions.Default;
            options.Size = new Vector2D<int>(800, 600);
            options.Title = "LearnOpenGL with Silk.NET";

            window = Window.Create(options);
            window.Load += OnLoad;
            window.Render += OnRender;
            window.Run();
            window.Dispose();
        }

        private static void OnRender(double obj)
        {
            Gl.ClearColor(0.4f, 0.8f, 0.5f, 1.0f);
            Gl.Clear((uint)ClearBufferMask.ColorBufferBit);

            //Bind the geometry and shader.
            Gl.BindVertexArray(Vao);
            Gl.UseProgram(Shader);
            //Draw the arrays.
            Gl.DrawArrays(GLEnum.Triangles, 0, 3);
        }

        private static void OnLoad()
        {
            Gl = GL.GetApi(window);
            DrawTriangle();
        }

        private static unsafe void DrawTriangle() 
        {
            float[] vertices = { 
                -1.0f, -1.0f, 0.0f,
                1.0f, -1.0f, 0.0f,
                0.0f, 1.0f, 0.0f,
            };

            //Creating a vertex array.
            Vao = Gl.GenVertexArray();
            Gl.BindVertexArray(Vao);

            //Initializing a vertex buffer that holds the vertex data.
            Vbo = Gl.GenBuffer(); //Creating the buffer.
            Gl.BindBuffer(BufferTargetARB.ArrayBuffer, Vbo); //Binding the buffer.

            fixed (void* v = &vertices[0])
            {
                Gl.BufferData(BufferTargetARB.ArrayBuffer, (nuint)(vertices.Length * sizeof(uint)), v, BufferUsageARB.StaticDraw); //Setting buffer data.
            }

            //Tell opengl how to give the data to the shaders.
            Gl.VertexAttribPointer(0, 3, VertexAttribPointerType.Float, false, 3 * sizeof(float), null);
            Gl.EnableVertexAttribArray(0);
            Gl.BindBuffer(BufferTargetARB.ArrayBuffer, 0);
            Gl.BindVertexArray(0);
            CompileShader();
        }

        private static void CompileShader() 
        {
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
        }
    }
}
