namespace Tanks.Interfaces
{
    public interface IRunLoop
    {
        public bool IsRunning { get; }
        void RunLoop();
    }
}