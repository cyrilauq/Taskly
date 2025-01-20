using Taskly.Client.Application.State.Interfaces;

namespace Taskly.Client.Application.Http.Handler
{
    public class HttpAuthorizationHandler(IAuthState authState) : DelegatingHandler
    {
        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            request.Headers.Add("Authorization", $"Bearer {authState.Token}");

            return await base.SendAsync(request, cancellationToken);
        }
    }
}
