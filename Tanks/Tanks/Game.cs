using SDL2;
using Tanks.Interfaces;

namespace Tanks
{
    public class Game : IGame
    {
        public Game(IWindow window)
        {
            if (window is null) throw new ArgumentNullException(nameof(window), 
                "Please provide window object when making an instance of Game class.");

            Window = window;
            IsInitialized = false;
            IsShutDown = true;
            IsRunning = false;
        }
        
        public bool IsInitialized { get; private set; }
        public bool IsRunning { get; private set; }
        public bool IsShutDown { get; private set; }
        public IWindow Window { get; private set; }

        public void Initialize()
        {
            if (IsInitialized) 
                throw new InvalidOperationException(
                    "Game class instance has already been initialized. " +
                    "Please, make sure, that you call Initialize method only once.");

            int sdlResult = SDL.SDL_Init(SDL.SDL_INIT_VIDEO);

            if (sdlResult != 0)
                throw new ApplicationException("SDL has failed to initialize.");

            Window.Initialize();

            if (!Window.IsInitialized)
                throw new ApplicationException("Window has failed to set IsInitialized flag.");

            IsInitialized = true;
            IsShutDown = false;
        }
        public void RunLoop()
        {
            if (!IsInitialized) 
                throw new InvalidOperationException(
                    "Game class hasn't been initialized yet. " +
                    "Please, make sure, that you invoke Initialize method " +
                    "before starting to run the game loop.");

            if (IsRunning) 
                throw new InvalidOperationException(
                    "The game loop has already been started." +
                    "Please, make sure, that you invoke RunLoop only once");

            IsRunning = true;
        }
        public void ShutDown()
        {
            if(IsShutDown) 
                throw new InvalidOperationException(
                    "Game class instance has already been shut down. " +
                    "Please, make sure, that you call ShutDown method only once.");
            
            IsShutDown = true;
        }
    }
}