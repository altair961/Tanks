namespace Tanks.Interfaces
{
    public interface IShutDown
    {
        public bool IsShutDown { get; }
        void ShutDown();
    }
}