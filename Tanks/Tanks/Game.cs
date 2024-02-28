using Tanks.Interfaces;

namespace Tanks
{
    public class Game : IGame
    {
        public Game(IRender renderer)
        {
            IsInitialized = false;
            IsShutDown = true;
            IsRunning = false;
        }
        public bool IsInitialized { get; private set; }
        public bool IsRunning { get; private set; }
        public bool IsShutDown { get; private set; }
        public void Initialize()
        {
            if (IsInitialized) throw new InvalidOperationException(
                "The Game class instance has already been initialized. " +
                "Please, make sure, that you call Initialize method only once.");
            
            IsInitialized = true;
        }
        public void RunLoop()
        {
            if (!IsInitialized) throw new InvalidOperationException(
                "The Game class hasn't been initialized yet. " +
                "Please, make sure, that you invoke Initialize method " +
                "before starting to run the game loop.");

            if (IsRunning) throw new InvalidOperationException(
                "The game loop has already been started." +
                "Please, make sure, that you invoke RunLoop only once");

            IsRunning = true;
        }
        public void ShutDown()
        {
            if(IsShutDown) throw new InvalidOperationException(
                "The Game class instance has already been shut down. " +
                "Please, make sure, that you call ShutDown method only once.");
            
            IsShutDown = true;
        }
    }
}