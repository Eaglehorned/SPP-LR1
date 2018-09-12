using NLog;

namespace MyLogger
{
    public class MyLogger : ILogger
    {
        private Logger logger;
        private const string LOG_FILE = "C:/Logs/LogFile.txt";
 
        public MyLogger()
        {
            logger = LogManager.GetCurrentClassLogger();

            var config = new NLog.Config.LoggingConfiguration();
            var logFile = new NLog.Targets.FileTarget("logfile") { FileName = LOG_FILE };

            config.AddRule(LogLevel.Info, LogLevel.Fatal, logFile);
            LogManager.Configuration = config;
        }

        public void Debug(string message) => logger.Debug(message);

        public void Error(string message) => logger.Error(message);

        public void Fatal(string message) => logger.Fatal(message);

        public void Info(string message) => logger.Info(message);

        public void Warn(string message) => logger.Warn(message);
    }
}
