using System;
using Silk.NET.Input;
using Silk.NET.Maths;
using Silk.NET.OpenGL;
using Silk.NET.Windowing;

namespace ScreenColorClear
{
    internal class Program
    {
        private static IWindow window;
        private static GL Gl;
        private static readonly float[] CornflowerBlue = { 0.392f, 0.584f, 0.929f, 1.0f };

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

            window.Run();

            window.Dispose();
        }

        private static unsafe void OnLoad()
        {
            IInputContext input = window.CreateInput();
            Gl = GL.GetApi(window);
        }

        private static unsafe void OnRender(double obj) //Method needs to be unsafe due to draw elements.
        {
            fixed (float* ptr = CornflowerBlue)
            {
                Gl.ClearBuffer(Silk.NET.OpenGL.GLEnum.Color, 0, ptr);
            }
        }

        private static void OnUpdate(double obj)
        {

        }

        private static void OnFramebufferResize(Vector2D<int> newSize)
        {
            Gl.Viewport(newSize);
        }
    }
}