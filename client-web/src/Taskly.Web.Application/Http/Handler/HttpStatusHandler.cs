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
                case HttpStatusCode.BadRequest:
                    throw new ValidationException("One or more field aren't valid");
                case HttpStatusCode.InternalServerError:
                    throw new InternalServerErrorException("An error occured in the server");
                case HttpStatusCode.Conflict:
                    throw new ResourceAlreadyExists("The given resource already exists");
                default:
                    throw new UnExpectedHttpException("");
            }
        }
    }
}
