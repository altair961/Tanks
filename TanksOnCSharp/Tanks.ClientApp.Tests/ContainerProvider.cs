using Autofac;

namespace Tanks.ClientApp.Tests
{
    internal class ContainerProvider
    {
        internal IContainer GetContainer()
        {
            var builder = new ContainerBuilder();

            builder.RegisterInstance(new TaskRepository()).As<ITaskRepository>();

            var container = builder.Build();
            return container;
        }
    }
}
