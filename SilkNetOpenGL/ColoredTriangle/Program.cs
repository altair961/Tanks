using System.Drawing;
using Silk.NET.Maths;
using Silk.NET.OpenGL;
using Silk.NET.Windowing;

namespace ColoredTriangle
{
    public enum VertexAttribute
    {
        Position,
        Color
    }
    public class VertexPositionColor
    {
        public VertexPositionColor(float x, float y, float z, Color color)
        {
            Position = [x, y, z];
            Color = color;
        }

        public IEnumerable<float> Position { get; set; }
        public Color Color { get; set; }
    }

    internal class Program
    {
        private static IWindow window;
        private static GL Gl;
        private static readonly float[] CornflowerBlue = { 0.392f, 0.584f, 0.929f, 1.0f };
        private static readonly string VertexShaderSource = @"
        #version 440 core

        layout (location = 0) in vec4 Position;
        layout (location = 1) in vec4 Color;

        out VS_OUTPUT
        {
	        vec4 Color;
        } OUT;

        void main()
        {
	        gl_Position = Position;
	        OUT.Color = Color;
        }
        ";

        private static readonly string FragmentShaderSource = @"
        #version 440 core

        in VS_OUTPUT
        {
	        vec4 Color;
        } IN;

        out vec4 Color;

        void main()
        {
	        Color = IN.Color;
        }
        ";
        private static uint Shader;
        private static uint Vao;
        private static uint VertexBuffer;

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

            // Create the vertex buffer
            // Vertex data, uploaded to the VBO.
            //VertexPositionColor[] vertices = [
            //    new VertexPositionColor(0.75f, -0.25f, 0.0f, Color.Red),
            //    new VertexPositionColor(0.0f, 0.5f, 0.0f, Color.Green),
            //    new VertexPositionColor(-0.75f, -0.25f, 0.0f, Color.Blue)
            //];

            float[] vertices = {
                0.75f, -0.25f, 0.0f,
                0.0f, 0.5f, 0.0f,
                -0.75f, -0.25f, 0.0f
            };

            VertexBuffer = Gl.GenBuffer();
            Gl.BindBuffer(BufferTargetARB.ArrayBuffer, VertexBuffer);
            fixed (void* v = &vertices[0])
            {
                Gl.BufferData(BufferTargetARB.ArrayBuffer, (nuint)(vertices.Length * sizeof(uint)), v, BufferUsageARB.StaticDraw);
            }
            Vao = Gl.GenVertexArray();
            Gl.BindVertexArray(Vao);

            // Our stride constant. The stride must be in bytes, so we take the first attribute (a vec3), multiply it
            // by the size in bytes of a float, and then take our second attribute (a vec2), and do the same.
            //Gl.VertexAttribPointer((uint)VertexAttribute.Position, 4, VertexAttribPointerType.Float, false, 3 * sizeof(float), 0);
            Gl.VertexAttribPointer(0, 3, VertexAttribPointerType.Float, false, 3 * sizeof(float), null);

            Gl.EnableVertexAttribArray((uint)VertexAttribute.Position);
            //Gl.VertexAttribPointer((uint)VertexAttribute.Color, 4, VertexAttribPointerType.Float, false, 3 * sizeof(float), 1);
            //Gl.EnableVertexAttribArray((uint)VertexAttribute.Color);

            Gl.EnableVertexAttribArray(0);
            
            Gl.BindVertexArray(0);
        }

        private static unsafe void OnRender(double obj) //Method needs to be unsafe due to draw elements.
        {
            fixed (float* ptr = CornflowerBlue)
            {
                Gl.ClearBuffer(Silk.NET.OpenGL.GLEnum.Color, 0, ptr);
            }

            //Bind the geometry and shader.
            Gl.BindVertexArray(Vao);
            Gl.UseProgram(Shader);
            //Draw the arrays.
            Gl.DrawArrays(GLEnum.Triangles, 0, 3);
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
