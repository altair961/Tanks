using SDL2;
using Tanks.Interfaces;

namespace Tanks
{
    public class Window : IWindow
    {
        private IntPtr _window;

        public Window()
        {
            IsInitialized = false;
        }
        public bool IsInitialized { get; private set; }

        public void Initialize()
        {
            if (IsInitialized) throw new InvalidOperationException(
                "Window class instance has already been initialized. " +
                "Please, make sure, that you call Initialize method only once.");

            _window = SDL.SDL_CreateWindow(
                "Tanks", // Window title
                100,    // Top left x-coordinate of window
                100,    // Top left y-coordinate of window
                1024,   // Width of window
                768,    // Height of window
                0       // Flags (0 for no flags set)
            );

            if (_window == IntPtr.Zero)
                throw new ApplicationException("Window has failed to itialize.");

            IsInitialized = true;
        }
    }
}
