using Tanks.GameEngine.Implementations;

namespace Example
{
    public class TanksClientApp
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Tanks");

            using (var gameEngine = new GameEngine(width, height, "Tanks"))
            {
                // game.WindowState = OpenTK.WindowState.Fullscreen;
                var framesPerSecondToStrive = 60.0;

                gameEngine.Run(framesPerSecondToStrive);
            }
        }
    }
}
