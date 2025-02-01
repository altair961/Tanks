using Silk.NET.Maths;
using Silk.NET.OpenGL;
using Silk.NET.Windowing;

namespace SilkNetTriangle
{
    internal class Program
    {
        private static IWindow window;
        private static GL Gl;

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
        }

        private static void OnLoad()
        {
            Gl = GL.GetApi(window);
        }
    }
}
