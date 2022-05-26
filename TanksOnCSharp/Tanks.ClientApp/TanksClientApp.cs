using OpenTK;
using Tanks.GameEngine.Implementations;

namespace Example
{
    public class TanksClientApp
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Tanks");

            int scrWidth = DisplayDevice.Default.Width;
            int scrHeight = DisplayDevice.Default.Height;

        //    scrWidth = 800;
         //   scrHeight = 600;

            using (var gameEngine = new GameEngine(scrWidth, scrHeight, "Tanks"))
            {
#if DEBUG
                Console.WriteLine("Mode=Debug");
#else
                gameEngine.WindowState = OpenTK.WindowState.Fullscreen;
#endif
                var framesPerSecondToStrive = 60.0;
                gameEngine.Run(framesPerSecondToStrive);
            }
        }
    }
}
