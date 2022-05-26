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
        public GameEngine(string title)
        {
            int width = DisplayDevice.Default.Width;
            int height = DisplayDevice.Default.Height;


        }

        public void Launch()
        {
#if DEBUG
            Console.WriteLine("Mode=Debug");
#else
    this.WindowState = OpenTK.WindowState.Fullscreen;
#endif
            var framesPerSecondToStrive = 60.0;
            this.Run(framesPerSecondToStrive);
        }

        public void DrawOnePixel(int x, int y, Color color)
        {
            throw new NotImplementedException();
        }

        public override void Dispose() 
        {
            base.Dispose();
        }
    }
}