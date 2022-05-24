

using Tanks.GameEngine.Client;

namespace Example
{
    public class Program
    {
        static void Main(string[] args)
        {
            using (Game game = new Game(800, 600, "Tanks"))
            {
                var framesPerSecondToStrive = 60.0;
                game.Run(framesPerSecondToStrive);
            }
        }
    }
}
