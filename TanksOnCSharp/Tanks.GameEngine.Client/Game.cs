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

        //protected override void OnRenderFrame(FrameEventArgs e)
        //{
        //    base.OnRenderFrame(e);
        //    GL.Clear(ClearBufferMask.ColorBufferBit);

        //    GL.Begin(PrimitiveType.Points);
        //    GL.Color3(Color.Red);
        //    GL.Vertex2(0, 0);
        //    GL.End();

        //    this.SwapBuffers();
        //    GL.ClearColor(Color.CornflowerBlue);
        //}

        //protected override void OnRenderFrame(FrameEventArgs e)
        //{
        //    base.OnRenderFrame(e);
        //    GL.Clear(ClearBufferMask.ColorBufferBit);

        //    GL.Begin(PrimitiveType.Quads);
            
        //    GL.Color3(Color.GreenYellow);
        //    GL.Vertex2(0.5, 0.5);

        //    GL.Color3(Color.Olive);
        //    GL.Vertex2(-0.5, 0.5);

        //    GL.Color3(Color.Fuchsia);
        //    GL.Vertex2(-0.5, -0.5);

        //    GL.Color3(Color.Gold);
        //    GL.Vertex2(0.5, -0.5);

        //    GL.End();

        //    this.SwapBuffers();
        //    GL.ClearColor(Color.Black);
        //}

        protected override void OnRenderFrame(FrameEventArgs e)
        {
            base.OnRenderFrame(e);
            GL.Clear(ClearBufferMask.ColorBufferBit);

            GL.Begin(PrimitiveType.Quads);

            DrawPixel(0, 0);


            GL.End();



            this.SwapBuffers();
            GL.ClearColor(Color.Black);
        }

        private void DrawPixel(int x, int y)
        {
            var originPoint = new Point(-1, 1);

            // Upper-right
            GL.Color3(Color.Red);
            GL.Vertex2(0.5, 0.5);
            //GL.Vertex2(-1, 1);

            // Upper-left
            GL.Color3(Color.BlueViolet);
            //GL.Vertex2(-0.5, 0.5);
            GL.Vertex2(-1, 1);

            // Bottom-left
            GL.Color3(Color.Fuchsia);
            GL.Vertex2(-0.5, -0.5);

            // Bottom-right
            GL.Color3(Color.Honeydew);
            GL.Vertex2(0.5, -0.5);
        }
    }
}
