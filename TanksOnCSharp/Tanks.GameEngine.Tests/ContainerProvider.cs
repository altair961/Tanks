using Autofac;

namespace Tanks.GameEngine.Tests
{
    public class ContainerProvider
    {
        public IContainer GetContainer() 
        {
            var builder = new ContainerBuilder();

           // builder.RegisterInstance(new TaskRepository())
           //.As<ITaskRepository>();

            var container = builder.Build();
            return container;
        }
    }
}
