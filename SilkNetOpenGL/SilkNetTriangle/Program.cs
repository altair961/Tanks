using Silk.NET.Maths;
using Silk.NET.Windowing;

namespace SilkNetTriangle
{
    internal class Program
    {
        private static IWindow window;

        private static void Main(string[] args)
        {
            Console.WriteLine("Hello, World!");
            var options = WindowOptions.Default;
            options.Size = new Vector2D<int>(800, 600);
            options.Title = "LearnOpenGL with Silk.NET";

            window = Window.Create(options);
            window.Run();
            window.Dispose();
        }
    }
}
