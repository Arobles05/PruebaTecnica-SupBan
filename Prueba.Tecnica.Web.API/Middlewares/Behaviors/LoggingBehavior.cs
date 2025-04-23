using MediatR;

namespace Prueba.Tecnica.Web.API.Middlewares.Behaviors
{
    public class LoggingBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    {
        private readonly ILogger<LoggingBehavior<TRequest, TResponse>> logger;
        public LoggingBehavior(ILogger<LoggingBehavior<TRequest, TResponse>> logger)
        {
            this.logger = logger;
        }

        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            logger.LogInformation($"Consuming an EndPoint of Prueba Tecnica Web API");
            logger.LogInformation($"Requests {typeof(TRequest).Name}");
            var response = await next();
            logger.LogInformation($"Response {typeof(TResponse).Name}");
            return response;
        }
    }
}
