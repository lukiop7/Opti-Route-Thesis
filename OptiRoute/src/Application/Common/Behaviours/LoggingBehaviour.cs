﻿using MediatR.Pipeline;
using Microsoft.Extensions.Logging;
using OptiRoute.Application.Common.Interfaces;
using System.Threading;
using System.Threading.Tasks;

namespace OptiRoute.Application.Common.Behaviours
{
    public class LoggingBehaviour<TRequest> : IRequestPreProcessor<TRequest>
    {
        private readonly ILogger _logger;

        public LoggingBehaviour(ILogger<TRequest> logger)
        {
            _logger = logger;
        }

        public async Task Process(TRequest request, CancellationToken cancellationToken)
        {
            var requestName = typeof(TRequest).Name;

            _logger.LogInformation("OptiRoute Request: {Name} {@Request}",
                requestName, request);
        }
    }
}