using Tanks.Interfaces;

namespace Tanks
{
    public class Game : IGame
    {
        public Game(IRender renderer)
        {
            IsInitialized = false;
            IsShutDown = false;
        }

        public bool IsInitialized { get; private set; }

        public bool IsShutDown { get; private set; }

        public void Initialize()
        {
            if (IsInitialized)
                throw new InvalidOperationException(
                    "The Game class instance has already been initialized. " +
                    "Please make sure, that you call Initialize method only once.");
            
            IsInitialized = true;
        }

        public void RunLoop()
        {
            throw new NotImplementedException();
        }

        public void ShutDown()
        {
            if(IsShutDown)
                throw new InvalidOperationException(
                    "The Game class instance has already been shut down. " +
                    "Please make sure, that you call ShutDown method only once.");
            IsShutDown = true;
        }
    }
}