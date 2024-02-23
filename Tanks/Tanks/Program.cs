using Autofac;
using SDL2;
using Tanks;
using Tanks.Interfaces;

namespace TanksSdl
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var app = CompositionRoot().Resolve<Application>();
            
            //var game = CompositionRoot().Resolve<Game>();//.Run();


            //var game = new Game();
            //bool success = game.Initialize();

            //if (success)
            //{
            //    game.RunLoop();
            //}

            //game.Shutdown();
        }

        private static IContainer CompositionRoot()
        {
            var builder = new ContainerBuilder();
            builder.RegisterType<Application>();
            builder.RegisterType<Game>().As<IGame>();
            builder.RegisterType<Renderer>().As<IRender>();
            return builder.Build();
        }
    }
}
