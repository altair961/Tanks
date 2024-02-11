using SDL2;

namespace TanksSdl
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var game = new Game();
            bool success = game.Initialize();

            if (success)
            {
                game.RunLoop();
            }
            //game.Shutdown();
        }
    }
}
