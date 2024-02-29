using Tanks.Interfaces;

namespace Tanks
{
    public class Application
    {
        public Application(IGame game)
        {
            if (game is null)
                throw new ArgumentNullException(nameof(game), "Please provide game object when making an instance of Application class.");

            game.Initialize();
        }
    }
}
