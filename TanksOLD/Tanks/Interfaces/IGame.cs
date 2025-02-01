namespace Tanks.Interfaces
{
    public interface IGame : IInitialize, IRunLoop, IShutDown
    {
        public IWindow Window { get; }
    }
}
