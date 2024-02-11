using SDL2;

namespace TanksSdl
{
    internal class Game
    {
        private IntPtr _window;
        private IntPtr _renderer;
        private bool _isRunning;

        public bool Initialize() 
        {
            int sdlResult = SDL.SDL_Init(SDL.SDL_INIT_VIDEO);

            if (sdlResult != 0)
            {
                Console.WriteLine("Unable to initialize SDL: %s", SDL.SDL_GetError());
                return false;
            }

            _window = SDL.SDL_CreateWindow(
                "Tanks", // Window title
                100,    // Top left x-coordinate of window
                100,    // Top left y-coordinate of window
                1024,   // Width of window
                768,    // Height of window
                0       // Flags (0 for no flags set)
            );

            if (_window == IntPtr.Zero)
            {
                Console.WriteLine("Failed to create window: %s", SDL.SDL_GetError());
                return false;
            }

            _renderer = SDL.SDL_CreateRenderer(
            _window, // Window to create renderer for
            -1,
            SDL.SDL_RendererFlags.SDL_RENDERER_ACCELERATED | 
            SDL.SDL_RendererFlags.SDL_RENDERER_PRESENTVSYNC
            );

            if (_renderer == IntPtr.Zero)
            {
                Console.WriteLine("Failed to create renderer: %s", SDL.SDL_GetError());
                return false;
            }
            _isRunning = true;

            return true;
        }

        internal void RunLoop()
        {
            while (_isRunning)
            {
                // Process Inputs
                ProcessInput();
                // Update Game World
                UpdateGame();
                // Generate Outputs
                GenerateOutput();
            }
        }

        private void GenerateOutput()
        {
            SDL.SDL_SetRenderDrawColor(_renderer, 57, 83, 164, 255);
            SDL.SDL_RenderClear(_renderer);

            var rect = new SDL.SDL_Rect
            {
                x = 300,
                y = 100,
                w = 50,
                h = 50
            };
            SDL.SDL_SetRenderDrawColor(_renderer, 255, 255, 0, 255);

            // Draw a filled in rectangle.
            SDL.SDL_RenderFillRect(_renderer, ref rect);

            SDL.SDL_RenderPresent(_renderer);

        }

        private void UpdateGame()
        {
        }

        internal void Shutdown()
        {
            SDL.SDL_DestroyRenderer(_renderer);
            SDL.SDL_DestroyWindow(_window);
            SDL.SDL_Quit();
        }

        private void ProcessInput()
        {
            while (SDL.SDL_PollEvent(out SDL.SDL_Event e) == 1)
            {
                switch (e.type)
                {
                    case SDL.SDL_EventType.SDL_QUIT:
                        _isRunning = false;
                        break;
                }
            }
        }
    }
}
