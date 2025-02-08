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
            // Vertex data, uploaded to the VBO
            float[] vertices = {
                0.75f, -0.25f,  0.0f, 1.0f, 0.0f, 0.0f,
                0.0f,   0.5f,   0.0f, 0.0f, 1.0f, 0.0f,
                -0.75f, -0.25f, 0.0f, 0.0f, 0.0f, 1.0f
            };

            VertexBuffer = Gl.GenBuffer();
            Gl.BindBuffer(BufferTargetARB.ArrayBuffer, VertexBuffer);

            fixed (void* v = &vertices[0])
            {
                Gl.BufferData(BufferTargetARB.ArrayBuffer, (nuint)(vertices.Length * sizeof(uint)), v, BufferUsageARB.StaticDraw);
            }

            Vao = Gl.GenVertexArray();
            Gl.BindVertexArray(Vao);

            // Buffer is a chunk of memory on the GPU. It is just bytes until we specify to pipline how to handle it
            // Vertex attribute is an input variable specified on per vertex basis
            // layout (location = 0) in vec2 position;
            //   x,y         x,y        x,y        x,y  x,y
            // {(a,b)       (c,d)      (e,f)      (g,h)(i,j)}
            //   position 0 position 1 position 2 ... 
            //   vertex 0   vertex 1
            //
            // if we want to specify colors it looks like this:
            // layout (location = 0) in vec2 position;
            // layout (location = 1) in vec3 color;
            //   x,y         r,g,b     x,y         r,g,b    x,y  r,g,b
            // {(a,b)       (c,d,e)   (f,g)       (g,h,i)  (i,j)(k,l,m)}
            //   position 0 color 0    position 1  color 1 ...
            //  [     vertex 0     ]  [     vertex 1     ]  

            // Stride specifies an interval in bytes from one entry to the next
            // So when we start with an index at 0 of vertex buffer stride specifies how many bytes 
            // it needs to advance each time to get to the start of the next vertices data

            // Our stride constant. The stride must be in bytes, so we take the first attribute (a vec3), multiply it
            // by the size in bytes of a float, and then take our second attribute (a vec2), and do the same.

            // here we tell pipline how to handle the bytes coming into vertex shader
            Gl.VertexAttribPointer(
                (int)VertexAttribute.Position, //we describe position, it corresponds to "layout (location = 0)" in shader  
                3, // position is 3 floats long, becuse position is represented by three values: x, y, z
                VertexAttribPointerType.Float, // vertices specified as floats
                false, // we don't want normalization for each vertices (applies to ints o
                6 * sizeof(float), // how big is one vertex (one vertex is one position-color) in each stride
                (void*)0); // this specifies where in each stride should we find the position. It is 0 because 
                           // position comes first in the stride
            Gl.EnableVertexAttribArray((uint)VertexAttribute.Position);

            Gl.VertexAttribPointer(
                (uint)VertexAttribute.Color, 
                3, 
                VertexAttribPointerType.Float, 
                false, 
                6 * sizeof(float), 
                (void*)(3 * sizeof(float))); // this specifies where in each stride should we find

            Gl.EnableVertexAttribArray((uint)VertexAttribute.Color);

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
