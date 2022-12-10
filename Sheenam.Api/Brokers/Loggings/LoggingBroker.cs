﻿// ---------------------------------------------------
// Copyright (c) Coalition of Good-hearted Engineers
// Free to use to find comfort and pease
// ---------------------------------------------------

namespace Sheenam.Api.Brokers.Loggings
{
    public class LoggingBroker : ILoggingBroker
    {
        private readonly ILogger<LoggingBroker> logger;

        public LoggingBroker(ILogger<LoggingBroker> logger) =>
            this.logger = logger;

        public void LogError(Exception exception)
        {
            this.logger.LogError(exception, message: exception.Message);
        }
        public void LogCritical(Exception exception)
        {
            this.logger.LogCritical(exception, message: exception.Message);
        }
    }
}
