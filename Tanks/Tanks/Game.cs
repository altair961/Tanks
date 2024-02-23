using Tanks.Interfaces;

namespace Tanks
{
    public class Game : IGame
    {
        public Game(IRender renderer)
        {
            ;
        }

        public void IdempotentInitialize()
        {
            throw new NotImplementedException();
        }

        public void RunLoop()
        {
            throw new NotImplementedException();
        }

        public void Shutdown()
        {
            throw new NotImplementedException();
        }
    }
}