using Microsoft.Extensions.Logging;
using ScaffNet.Utils.EventHandling;

namespace ScaffNetConsole.Utils
{
    internal class EventHandler : IScaffEventHandler
    {
        public ILogger<Program> _logger { get; set; }
        public EventHandler(ILogger<Program> logger)
        {
            _logger = logger;
        }

        public void OnDebug(string message)
        {
            _logger.LogDebug(message);
        }

        public void OnInfo(string message)
        {
            _logger.LogInformation(message);
        }

        public void OnWarning(string message)
        {
            _logger.LogWarning(message);
        }

        public void OnError(string message)
        {
            _logger.LogError(message);
        }

        public void OnCritical(string message)
        {
            _logger.LogCritical(message);
        }
    }
}
