using NLog;

namespace QuiZone.Common.LoggerService
{
    public class LoggerManager : ILoggerManager
    {
        private static readonly ILogger Logger = LogManager.GetCurrentClassLogger();

        /// <summary>
        ///  Debug messages
        /// </summary>
        public void Debug(string message)
        {
            Logger.Debug(message);
        }

        /// <summary>
        ///  Error messages
        /// </summary>
        public void Error(string message)
        {
            Logger.Error(message);
        }

        /// <summary>
        /// Info messages
        /// </summary>
        public void Info(string message)
        {
            Logger.Info(message);
        }

        /// <summary>
        ///  Warning messages
        /// </summary>
        public void Warn(string message)
        {
            Logger.Warn(message);
        }
    }
}