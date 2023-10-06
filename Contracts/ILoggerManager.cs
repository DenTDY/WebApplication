namespace Contracts
{
    public interface ILoggerManager
    {
        void LogInfo(string message);
        void LogWar(string message);
        void LogDebug(string message);
        void LogError(string message);

    }
}
