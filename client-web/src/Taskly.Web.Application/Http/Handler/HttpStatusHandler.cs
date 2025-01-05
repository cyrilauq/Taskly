using System.Net;
using Taskly.Web.Exceptions;

namespace Taskly.Web.Application.Http.Handler
{
    public class HttpStatusHandler: DelegatingHandler
    {
        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            HttpResponseMessage response = await base.SendAsync(request, cancellationToken);
            if (response.IsSuccessStatusCode) return response;
            HttpStatusCode respondeCodeStatus = response.StatusCode;
            switch (respondeCodeStatus)
            {
                case HttpStatusCode.NotFound:
                    throw new NotFoundException("Resource not found");
                case HttpStatusCode.InternalServerError:
                    throw new InternalServerErrorException("An error occured in the server");
                default:
                    throw new UnExpectedHttpException("");
            }
        }
    }
}
