using System.Net;
using Taskly.Web.Exceptions;

namespace Taskly.Web.Application.Http.Client
{
    internal class TasklyHttpClient: HttpClient
    {
        public override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            HttpResponseMessage response = await base.SendAsync(request, cancellationToken);
            if(response.IsSuccessStatusCode) return response;
            HttpStatusCode respondeCodeStatus = response.StatusCode;
            switch (respondeCodeStatus)
            {
                case HttpStatusCode.NotFound:
                    throw new NotFoundException("Resource not found");
                default:
                    throw new UnExpectedHttpException("");
            }
        }
    }
}
