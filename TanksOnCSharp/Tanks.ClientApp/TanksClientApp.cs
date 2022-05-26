using Tanks.GameEngine.Implementations;

namespace Example
{
    public class TanksClientApp
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Tanks");

            using (var gameEngine = new GameEngine("Tanks"))
            {
                gameEngine.Launch();
            }
        }
    }
}
