using MediatR;
using Microsoft.Extensions.Logging;

namespace TimeTrackingApp.Infrastructure.Behaviours
{
    public class LoggingBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> where TRequest : IRequest<TResponse>
    {
        private readonly ILogger<LoggingBehaviour<TRequest, TResponse>> _logger;

        public LoggingBehaviour(ILogger<LoggingBehaviour<TRequest, TResponse>> logger)
        {
            _logger = logger;
        }

        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            _logger.LogDebug("Start processing {TRequest}", typeof(TRequest));
            TResponse response = await next();
            _logger.LogDebug("End processing {TRequest}", typeof(TRequest));
            return response;
        }
    }
}
