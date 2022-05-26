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

            GL.Begin(PrimitiveType.Quads);

        //    someObj.GetCurrentPicturePixelSet();

            DrawPixel(0, 0, Color.AliceBlue);


            GL.End();


            this.SwapBuffers();
            GL.ClearColor(Color.Black);
        }

        private void DrawPixel(int x, int y, Color pixelColor)
        {
            var originPoint = new Point(0, 0);

            var pixelSide = 0.08;

            double ratio = 1440.0 / 960;

            GL.Color3(pixelColor);

            // Upper-right
            GL.Vertex2(originPoint.X + pixelSide / ratio, originPoint.Y);

            // Upper-left
            GL.Vertex2(originPoint.X, originPoint.Y);

            // Bottom-left 
            GL.Vertex2(originPoint.X, originPoint.Y - pixelSide);

            // Bottom-right
            GL.Vertex2(originPoint.X + pixelSide / ratio, originPoint.Y - pixelSide);
        }
    }
}
