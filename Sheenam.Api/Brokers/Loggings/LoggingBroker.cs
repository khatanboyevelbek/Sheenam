// ---------------------------------------------------
// Copyright (c) Coalition of Good-hearted Engineers
// Free to use to find comfort and pease
// ---------------------------------------------------

namespace Sheenam.Api.Brokers.Loggings
{
    public class LoggingBroker : ILoggingBroker
    {
        private readonly ILogger<LoggingBroker> _logger;

        public LoggingBroker(ILogger<LoggingBroker> logger) =>
            this._logger = logger;

        public void LogError(Exception exception)
        {
            this._logger.LogError(exception, message: exception.Message);
        }
        public void LogCritical(Exception exception)
        {
            this._logger.LogCritical(exception, message: exception.Message);
        }
    }
}
