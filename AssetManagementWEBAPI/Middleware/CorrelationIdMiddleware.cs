using Serilog.Context;
using ILogger = Serilog.ILogger;

namespace AssetManagementWEBAPI.Middleware
{
    public class CorrelationIdMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger _logger;
        private const string HeaderKey = "X-Correlation-ID";

        public CorrelationIdMiddleware(RequestDelegate next, ILogger logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            if (!context.Request.Headers.TryGetValue(HeaderKey, out var correlationId) || string.IsNullOrWhiteSpace(correlationId))
            {
                correlationId = Guid.NewGuid().ToString();
                context.Request.Headers[HeaderKey] = correlationId;
                _logger.Information("Assigned new CorrelationId={CorrelationId} to incoming request {Method} {Path}", correlationId, context.Request.Method, context.Request.Path);
            }
            else
            {
                _logger.Information("Received incoming request with CorrelationId={CorrelationId} {Method} {Path}", correlationId.ToString(), context.Request.Method, context.Request.Path);
            }

            context.Response.Headers[HeaderKey] = correlationId;

            using (LogContext.PushProperty("CorrelationId", correlationId.ToString()))
            {
                var start = DateTime.UtcNow;
                try
                {
                    await _next(context);
                }
                finally
                {
                    var duration = DateTime.UtcNow - start;
                    _logger.Information("Completed request {Method} {Path} CorrelationId={CorrelationId} StatusCode={StatusCode} DurationMs={Duration}", context.Request.Method, context.Request.Path, correlationId.ToString(), context.Response.StatusCode, duration.TotalMilliseconds);
                }
            }
        }
    }
}
