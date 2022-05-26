using System.Drawing;
using Tanks.GameEngine.Interfaces;
using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;
using OpenTK.Input;
using System.Drawing;

namespace Tanks.GameEngine.Implementations
{
    public class GameEngine : GameWindow, IDrawOnePixel
    {
        public int ScrWidth { get; private set; }
        public int ScrHeight { get; private set; }

        public GameEngine(int scrWidth, int scrHeight, string title) : base(scrWidth, scrHeight, GraphicsMode.Default, title)
        {
            ScrWidth = scrWidth;
            ScrHeight = scrHeight;
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

            this.DrawOnePixel(0, 0, Color.Coral);


            GL.End();


            this.SwapBuffers();
            GL.ClearColor(Color.Black);
        }

        public void DrawOnePixel(int x, int y, Color pixelColor)
        {
            var originPoint = new Point(x, y);

            var pixelSide = 0.015;
            var xOffset = 1;
            var yOffset = 1;

            double ratio = (double)ScrWidth / ScrHeight;

            GL.Color3(pixelColor);

            // Upper-right
            GL.Vertex2(originPoint.X - xOffset + pixelSide / ratio, originPoint.Y + yOffset);

            // Upper-left
            GL.Vertex2(originPoint.X - xOffset, originPoint.Y + yOffset);

            // Bottom-left 
            GL.Vertex2(originPoint.X - xOffset, originPoint.Y - pixelSide + yOffset);

            // Bottom-right
            GL.Vertex2(originPoint.X - xOffset + pixelSide / ratio, originPoint.Y - pixelSide + yOffset);
        }

        public override void Dispose() 
        {
            base.Dispose();
        }
    }
}