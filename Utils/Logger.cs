using Microsoft.Extensions.Logging;

namespace ScaffNetConsole.Utils
{
    internal class Logger
    {
        private ILogger<Logger> _logger { get; set; }

        internal Logger(ILogger<Logger> logger)
        {

            _logger = logger;
        }

        internal void LogDebug(string message) =>
            _logger.LogDebug(message);

        internal void LogInfo(string message) =>
            _logger.LogInformation(message);

        internal void LogWarning(string message) =>
            _logger.LogWarning(message);

        internal void LogError(string message) =>
            _logger.LogError(message);

        internal void LogCritical(string message) =>
            _logger.LogCritical(message);
    }
}
