using Silk.NET.Maths;
using Silk.NET.OpenGL;
using Silk.NET.Windowing;

namespace Cube
{
    internal class Program
    {
        private static IWindow window;
        private static GL gl;

        // Hardcoded cube model (vertices and indices)
        private static readonly float[] vertices = {
                // Positions         // Colors
        -0.5f, -0.5f, -0.5f, 1.0f, 0.0f, 0.0f, // Red
         0.5f, -0.5f, -0.5f, 0.0f, 1.0f, 0.0f, // Green
         0.5f,  0.5f, -0.5f, 0.0f, 0.0f, 1.0f, // Blue
        -0.5f,  0.5f, -0.5f, 1.0f, 1.0f, 0.0f, // Yellow
        -0.5f, -0.5f,  0.5f, 1.0f, 0.0f, 1.0f, // Magenta
         0.5f, -0.5f,  0.5f, 0.0f, 1.0f, 1.0f, // Cyan
         0.5f,  0.5f,  0.5f, 0.5f, 0.5f, 0.5f, // Gray
        -0.5f,  0.5f,  0.5f, 1.0f, 1.0f, 1.0f  // White
    };

        private static readonly uint[] indices = {
       // Front face
        0, 1, 2,
        2, 3, 0,

        // Back face
        4, 5, 6,
        6, 7, 4,

        // Left face
        0, 3, 7,
        7, 4, 0,

        // Right face
        1, 5, 6,
        6, 2, 1,

        // Top face
        3, 2, 6,
        6, 7, 3,

        // Bottom face
        0, 1, 5,
        5, 4, 0
    };

        // Vertex shader source code
        private static readonly string vertexShaderSource = @"
        #version 330 core
        layout (location = 0) in vec3 aPos;
        layout (location = 1) in vec3 aColor;
        out vec3 ourColor;
        void main()
        {
            gl_Position = vec4(aPos, 1.0);
            ourColor = aColor;
        }
    ";

        // Fragment shader source code
        private static readonly string fragmentShaderSource = @"
        #version 330 core
        in vec3 ourColor;
        out vec4 FragColor;
        void main()
        {
            FragColor = vec4(ourColor, 1.0);
        }
    ";

        private static uint vao;
        private static uint vbo;
        private static uint ebo;
        private static uint shaderProgram;

        private static void Main(string[] args)
        {
            Console.WriteLine("Hello, World!");
            var options = WindowOptions.Default;
            options.Size = new Vector2D<int>(800, 600);
            options.Title = "LearnOpenGL with Silk.NET";

            window = Window.Create(options);
            window.Load += OnLoad;
            window.Render += OnRender;
            window.Closing += OnClose;
            window.Run();
            window.Dispose();
        }

        private static unsafe void OnRender(double obj)
        {
            // Clear the screen
            gl.ClearColor(0.2f, 0.3f, 0.3f, 1.0f);
            gl.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);

            // Use the shader program
            gl.UseProgram(shaderProgram);

            // Bind the VAO
            gl.BindVertexArray(vao);

            // Draw the cube
            gl.DrawElements(PrimitiveType.Triangles, (uint)indices.Length, DrawElementsType.UnsignedInt, (void*)0);
        }

        private static unsafe void OnLoad()
        {
            // Initialize OpenGL
            gl = GL.GetApi(window);

            // Compile shaders and create shader program
            shaderProgram = CreateShaderProgram();

            // Set up VAO, VBO, and EBO
            vao = gl.GenVertexArray();
            gl.BindVertexArray(vao);

            vbo = gl.GenBuffer();
            gl.BindBuffer(BufferTargetARB.ArrayBuffer, vbo);

            fixed (void* v = &vertices[0])
            { 
                gl.BufferData<float>(BufferTargetARB.ArrayBuffer, (uint)(vertices.Length * sizeof(float)), vertices, BufferUsageARB.StaticDraw);
            }

            ebo = gl.GenBuffer();
            gl.BindBuffer(BufferTargetARB.ElementArrayBuffer, ebo);

            fixed (void* v = &indices[0])
            {
                gl.BufferData<uint>(BufferTargetARB.ElementArrayBuffer, (uint)(indices.Length * sizeof(uint)), indices, BufferUsageARB.StaticDraw);
            }

            // Set up vertex attribute pointers
            // Position attribute (location = 0)
            gl.VertexAttribPointer(0, 3, VertexAttribPointerType.Float, false, 6 * sizeof(float), (void*)0);
            gl.EnableVertexAttribArray(0);

            // Color attribute (location = 1)
            gl.VertexAttribPointer(1, 3, VertexAttribPointerType.Float, false, 6 * sizeof(float), (void*)(3 * sizeof(float)));
            gl.EnableVertexAttribArray(1);

            // Unbind the VAO (optional)
            gl.BindVertexArray(0);
        }

        private static void OnClose()
        {
            // Clean up resources
            gl.DeleteVertexArray(vao);
            gl.DeleteBuffer(vbo);
            gl.DeleteBuffer(ebo);
            gl.DeleteProgram(shaderProgram);
        }

        private static uint CreateShaderProgram()
        {
            // Compile vertex shader
            uint vertexShader = gl.CreateShader(ShaderType.VertexShader);
            gl.ShaderSource(vertexShader, vertexShaderSource);
            gl.CompileShader(vertexShader);
            CheckShaderCompileStatus(vertexShader);

            // Compile fragment shader
            uint fragmentShader = gl.CreateShader(ShaderType.FragmentShader);
            gl.ShaderSource(fragmentShader, fragmentShaderSource);
            gl.CompileShader(fragmentShader);
            CheckShaderCompileStatus(fragmentShader);

            // Link shaders into a program
            uint program = gl.CreateProgram();
            gl.AttachShader(program, vertexShader);
            gl.AttachShader(program, fragmentShader);
            gl.LinkProgram(program);
            CheckProgramLinkStatus(program);

            // Delete the shaders (they are no longer needed after linking)
            gl.DeleteShader(vertexShader);
            gl.DeleteShader(fragmentShader);

            return program;
        }

        private static void CheckShaderCompileStatus(uint shader)
        {
            gl.GetShader(shader, ShaderParameterName.CompileStatus, out int status);
            if (status == (int)GLEnum.False)
            {
                string infoLog = gl.GetShaderInfoLog(shader);
                throw new Exception($"Shader compilation failed: {infoLog}");
            }
        }

        private static void CheckProgramLinkStatus(uint program)
        {
            gl.GetProgram(program, ProgramPropertyARB.LinkStatus, out int status);
            if (status == (int)GLEnum.False)
            {
                string infoLog = gl.GetProgramInfoLog(program);
                throw new Exception($"Program linking failed: {infoLog}");
            }
        }
    }
}
