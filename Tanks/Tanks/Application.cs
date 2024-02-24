using Tanks.Interfaces;

namespace Tanks
{
    public class Application
    {
        public Application(IGame game)
        {
            if (game == null)
                throw new ArgumentNullException("game", "Please provide game object when making instance of Application class.");

            game.Initialize();
        }
    }
}
