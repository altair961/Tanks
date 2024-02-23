namespace Tanks.Interfaces
{
    public interface IGame : IIdempotentInitialize, IRunLoop, IShutdown
    {
    }
}
