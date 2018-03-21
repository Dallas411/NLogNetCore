using Core.OALogger;
using Microsoft.Extensions.Logging;
using System;

namespace Services.OALogger
{

    public class FooService: IFooService
    {
        private readonly ILogger _logger;

        public FooService(ILogger<FooService> logger)
        {
            _logger = logger;
        }

        public void DoSomethingWithLogging()
        {
            _logger.LogWarning("Text Log Test in Other Project method!");
        }
    }
}
