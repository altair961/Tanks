

using OpenTK;
using Tanks.GameEngine.Client;

namespace Example
{
    public class Program
    {
        static void Main(string[] args)
        {
            int width = DisplayDevice.Default.Width; ;
            int height = DisplayDevice.Default.Height;

            using (Game game = new Game(width, height, "Tanks"))
            {
                game.WindowState = OpenTK.WindowState.Fullscreen;
                var framesPerSecondToStrive = 60.0;
                
                game.Run(framesPerSecondToStrive);
            }
            Console.WriteLine("width: " + width);
            Console.WriteLine("height: " + height);
        }
    }
}
