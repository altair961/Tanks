namespace Tanks.Interfaces
{
    public interface IInitialize
    {
        bool IsInitialized { get; }
        void Initialize();
    }
}