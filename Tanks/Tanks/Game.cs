using Silk.NET.Input;
using Silk.NET.Maths;
using Silk.NET.OpenGL;
using Silk.NET.Windowing;

namespace Tanks
{
    internal class Game
    {
        private IWindow? _window;
        private GL? _gl;
        private IInputContext? _inputContext;

        public Game()
        {
            try 
            {
                Initialize();
            } catch(Exception e) 
            {
                Console.WriteLine(e);
                //log(e);
            }
        }

        private void Initialize()
        {
            var options = WindowOptions.Default;
            options.Size = new Vector2D<int>(1400, 1100);
            options.Title = "Tanks";
            options.Position = new Vector2D<int>(450, 200);
            _window = Window.Create(options);
            _window.Load += Window_Load;
            _window.Render += Window_Render;
            _window.Initialize();
            _inputContext = _window.CreateInput();
        }

        private void Window_Render(double elapsedTime)
        {
            ProcessInput();
            GenerateOutput(elapsedTime);
        }

        private void ProcessInput()
        {
            if (_inputContext == null)
                return;

            foreach (var keyboard in _inputContext.Keyboards)
            {
                if (keyboard.IsKeyPressed(Key.Up))
                    Console.WriteLine("pupsia!");
            }
        }

        private void Window_Load()
        {
            _gl = GL.GetApi(_window);
        }

        private void GenerateOutput(double elapsedTime)
        {
            _gl?.ClearColor(0.5f, 0.5f, 0.5f, 0.1f);
            _gl?.Clear(ClearBufferMask.ColorBufferBit);
        }

        public void Start()
        {
            _window?.Run();
        }
    }
}