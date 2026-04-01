using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace AssetManagementBlazor.MessageHandlers
{
    public class CorrelationIdForwardingHandler : DelegatingHandler
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private const string HeaderKey = "X-Correlation-ID";

        public CorrelationIdForwardingHandler(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            var context = _httpContextAccessor.HttpContext;
            if (context != null && context.Request.Headers.TryGetValue(HeaderKey, out var correlationId))
            {
                if (!request.Headers.Contains(HeaderKey))
                    request.Headers.Add(HeaderKey, (string)correlationId);
            }

            return base.SendAsync(request, cancellationToken);
        }
    }
}
