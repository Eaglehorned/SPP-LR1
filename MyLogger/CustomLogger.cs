using System.Configuration;
using NLog;

namespace CustomLogger
{
    public class CustomLogger : ICustomLogger
    {
        private Logger logger;
 
        public CustomLogger()
        {
             logger = LogManager.GetCurrentClassLogger();

            var config = new NLog.Config.LoggingConfiguration();
            var logFile = new NLog.Targets.FileTarget("logfile") { FileName = ConfigurationManager.AppSettings["LogFileName"] };

            config.AddRule(LogLevel.Info, LogLevel.Fatal, logFile);
            LogManager.Configuration = config;
        }
        
        public void LogDebug(string message) => logger.Debug(message);

        public void LogError(string message) => logger.Error(message);

        public void LogFatal(string message) => logger.Fatal(message);

        public void LogInfo(string message) => logger.Info(message);

        public void LogWarn(string message) => logger.Warn(message);
    }
}
