using Tanks.Interfaces;

namespace Tanks
{
    public class Game : IGame
    {
        public Game(IRender renderer)
        {
            IsInitialized = false;
        }

        public bool IsInitialized { get; private set; }

        public void Initialize()
        {
            if (IsInitialized)
                throw new InvalidOperationException("The Game class instance has already been initialized. Please make sure, that you call Initialize method only once.");
            
            IsInitialized = true;
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