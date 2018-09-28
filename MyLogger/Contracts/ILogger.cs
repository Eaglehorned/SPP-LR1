namespace CustomLogger
{
    public interface ICustomLogger
    {
        void LogInfo(string message);

        void LogWarn(string message);

        void LogDebug(string message);

        void LogError(string message);

        void LogFatal(string message);
    }
}
