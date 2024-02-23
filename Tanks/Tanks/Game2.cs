using SDL2;

namespace TanksSdl
{
    internal class Vector2 
    {
        double x;
        double y;
    }

    internal class Game2
    {
        private IntPtr _window;
        private IntPtr _renderer;
        private bool _isRunning;
        private IntPtr _texture;
        private int _frameNumber = 0;
        private float _deltaTime;
        private uint _ticksCount = 0;
        private int yPosition = 300;

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

            // initialize texture manager
            var pathToSpriteSheet = "./player's_tank_sprite_sheet.png";
            
            var surface = SDL_image.IMG_Load(pathToSpriteSheet);
            _texture = SDL.SDL_CreateTextureFromSurface(_renderer, surface);
            SDL.SDL_FreeSurface(surface);

  //          
            //SDL.SDL_RenderClear(_renderer);
            //SDL.SDL_RenderCopy(_renderer, texture,IntPtr.Zero, IntPtr.Zero);


            //            var surface = SDL_image.IMG_Load(pathToSpriteSheet);
            //          var texture = SDL.SDL_CreateTextureFromSurface(_renderer, surface);
            //        SDL.SDL_FreeSurface(surface);


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

            //var rect = new SDL.SDL_Rect
            //{
            //    x = 300,
            //    y = 100,
            //    w = 50,
            //    h = 50
            //};
            SDL.SDL_SetRenderDrawColor(_renderer, 255, 255, 0, 255);



            // Draw a filled in rectangle.
            //SDL.SDL_RenderFillRect(_renderer, ref rect);

            //int v = SDL.SDL_RenderCopy(_renderer, _texture, 0, ref rect);
            

            var rectSrc = new SDL.SDL_Rect
            {
                x = 0,
                y = 0,
                w = 16,
                h = 16
            };

            yPosition--;

            var rectDst = new SDL.SDL_Rect
            {
                x = 200,
                y = yPosition,
                w = 64,
                h = 64
            };

            // PlayFrame(0, 0, 16, 16, _frameNumber, );
            rectSrc.x = rectSrc.x + rectSrc.w * _frameNumber;

            SDL.SDL_RenderCopy(_renderer, _texture, ref rectSrc, ref rectDst);

            //_frameNumber++;

            _frameNumber++; // _frameNumber + (int)Math.Ceiling(_frameNumber * _deltaTime);

            if (_frameNumber > 1)
            {
                _frameNumber = 0;
            }


            SDL.SDL_RenderPresent(_renderer);
        }

        private void UpdateGame()
        {
            // Wait until 50ms has elapsed since last frame
            while (!SDL.SDL_TICKS_PASSED(SDL.SDL_GetTicks(), _ticksCount + 50))
                ;


            // Delta time is the difference in ticks from last frame
            // Converted to seconds
            _deltaTime = (SDL.SDL_GetTicks() - _ticksCount) / 1000.0f; // deltaTime is in seconds

            // Update tick counts (for next frame)
            _ticksCount = SDL.SDL_GetTicks();

            // Clamp maximum delta time value
            if (_deltaTime > 0.05f)
            {
                _deltaTime = 0.5f;
            }


//            Vector2 _tankPos;


        }

        internal void Shutdown()
        {
            SDL.SDL_DestroyTexture(_texture);
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
