using Autofac;
using Tanks.Interfaces;

namespace Tanks.UnitTests
{
    internal static class IoCContainer
    {
        internal static IContainer CompositionRoot()
        {
            {
                var builder = new ContainerBuilder();
                builder.RegisterType<Application>();
                builder.RegisterType<Game>().As<IGame>();
                builder.RegisterType<Renderer>().As<IRender>();
                builder.RegisterType<Window>().As<IWindow>();
                return builder.Build();
            }
        }
    }
}
