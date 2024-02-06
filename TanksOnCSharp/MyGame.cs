using Silk.NET.OpenGL;
using Silk.NET.Windowing;

namespace TanksOnCSharp
{
    public class MyGame
    {
        IWindow window;
        GL? Gl;
        public MyGame()
        {
            window = Window.Create(WindowOptions.Default with
            {
                Title = "Tanks"
            });

            window.Render += Window_Render;
            window.Load += Window_Load;
        }

        private void Window_Load()
        {
            Gl = GL.GetApi(window);
        }

        private void Window_Render(double obj)
        {
            Gl?.ClearColor(0.5f, 0.5f, 0.5f, 1.0f);
            Gl?.Clear(ClearBufferMask.ColorBufferBit);
        }

        public void Start()
        {
            window.Run();
        }
    }
}
