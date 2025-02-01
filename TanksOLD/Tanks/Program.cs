using Autofac;
using Tanks;

namespace TanksSdl
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var app = IoCContainer.CompositionRoot().Resolve<Application>();
            
            //var game = CompositionRoot().Resolve<Game>();//.Run();


            //var game = new Game();
            //bool success = game.Initialize();

            //if (success)
            //{
            //    game.RunLoop();
            //}

            //game.Shutdown();
        }
    }
}
