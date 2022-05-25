using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;
using OpenTK.Input;
using System.Drawing;

namespace Tanks.GameEngine.Client
{
    public class Game : GameWindow
    {
        public Game(int width, int height, string title) : base(width, height, GraphicsMode.Default, title) 
        {
            
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
        }

        protected override void OnUpdateFrame(FrameEventArgs e)
        {
            var keyboardState = Keyboard.GetState();

            if (keyboardState.IsKeyDown(Key.Escape))
            {
                Exit();
            }
            base.OnUpdateFrame(e);
        }

        protected override void OnRenderFrame(FrameEventArgs e)
        {
            base.OnRenderFrame(e);

            GL.Clear(ClearBufferMask.ColorBufferBit);

            this.SwapBuffers();
            GL.ClearColor(Color.Coral);
        }
    }
}
