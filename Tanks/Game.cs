using OpenTK.Graphics.OpenGL4;
using OpenTK.Mathematics;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.Desktop;

namespace Tanks
{//https://www.youtube.com/watch?v=LcHCygwIgLo
    public class Game : GameWindow
    {
        public Game()
            : base(GameWindowSettings.Default, NativeWindowSettings.Default)
        {
            CenterWindow(new Vector2i(1200, 768));
            GL.ClearColor(new Color4(0.3f, 0.4f, 0.5f, 1f));
        }

        protected override void OnUpdateFrame(FrameEventArgs atgs) 
        {
            base.OnUpdateFrame(atgs);
        }

        protected override void OnRenderFrame(FrameEventArgs args)
        {
            GL.Clear(ClearBufferMask.ColorBufferBit);

            Context.SwapBuffers();
            base.OnRenderFrame(args);
        }
    }
}
