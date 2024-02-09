using Silk.NET.Core.Contexts;
using Silk.NET.OpenGL;
using Silk.NET.Windowing;

namespace Tanks
{
    public class MyGame
    {
        IWindow window;
        GL Gl;
        public MyGame()
        {
            window = Window.Create(WindowOptions.Default);
            window.Render += Window_Render;
            window.Load += Window_Load;
        }

        private void Window_Load()
        {
            Gl = GL.GetApi(window);
        }

        private void Window_Render(double obj)
        {
            Gl.ClearColor(0.5f, 0.5f, 0.2f, 0.1f);
            Gl.Clear(ClearBufferMask.ColorBufferBit);
        }

        public void Start() 
        {
            window.Run();
        }
    }
}
