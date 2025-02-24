using Microsoft.Extensions.Logging;
using ScaffNet.Utils;

namespace ScaffNetConsole
{
    internal class Logger : ScaffLogger
    {
        private ILogger<Logger> _logger { get; set; }

        internal Logger(ILogger<Logger> logger, LogLevel minimalLevel) : base(minimalLevel)
        {
            _logger = logger;
        }

        sealed protected override void LogDebugBehaviour(string message) =>
            _logger.LogDebug($"[DEBUG] {message}");

        sealed protected override void LogInfoBehaviour(string message) =>
            _logger.LogInformation($"[INFO] {message}");

        sealed protected override void LogWarningBehaviour(string message) =>
            _logger.LogWarning($"[WARNING] {message}");

        sealed protected override void LogErrorBehaviour(string message) =>
            _logger.LogError($"[ERROR] {message}");

        sealed protected override void LogCriticalBehaviour(string message) =>
            _logger.LogCritical($"[CRITICAL] {message}");


        internal static void SetupScaffLogger()
        {
            var minimalLevel = LogLevel.Information;
            using var loggerFactory = LoggerFactory.Create(builder =>
            {
                builder
                .SetMinimumLevel(minimalLevel)
                .AddConsole();
            });

            var myCliLogger = new Logger(loggerFactory.CreateLogger<Logger>(), minimalLevel);
            Utility.SetLogger(myCliLogger);
        }
    }
}
