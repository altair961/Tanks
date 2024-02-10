using Silk.NET.Input;
using Silk.NET.OpenGL;
using Silk.NET.Windowing;

namespace Tanks2
{
    internal class Game
    {
        private IWindow? window;
        private GL? Gl;
        private IInputContext? inputContext;

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
            window = Window.Create(WindowOptions.Default);
            window.Load += Window_Load;
            window.Render += Window_Render;
            window.Initialize();
            inputContext = window.CreateInput();

        }

        private void Window_Render(double elapsedTime)
        {
            ProcessInput();
            GenerateOutput(elapsedTime);
        }

        private void ProcessInput()
        {
            if (inputContext == null)
                return;

            foreach (var keyboard in inputContext.Keyboards)
            {
                if (keyboard.IsKeyPressed(Key.Up))
                    Console.WriteLine("up has been pressed");
            }
        }

        private void Window_Load()
        {
            Gl = GL.GetApi(window);
        }

        private void GenerateOutput(double elapsedTime)
        {
            Gl?.ClearColor(0.5f, 0.5f, 0.2f, 0.1f);
            Gl?.Clear(ClearBufferMask.ColorBufferBit);
        }

        public void Start()
        {
            window?.Run();
        }
    }
}