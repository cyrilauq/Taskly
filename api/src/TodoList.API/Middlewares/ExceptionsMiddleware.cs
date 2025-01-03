using Microsoft.AspNetCore.Mvc;
using TodoList.Application.Services.Exceptions;
using UnauthorizedAccessException = TodoList.Application.Services.Exceptions.UnauthorizedAccessException;

namespace TodoList.API.Middlewares
{
    public class ExceptionsMiddleware(ILogger<ExceptionsMiddleware> logger, IHostEnvironment environment) : IMiddleware
    {
        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            try
            {
                await next(context);
            }
            catch(Exception ex)
            {
                var traceId = Guid.NewGuid();
                logger.LogError($"Error occure while processing the request, TraceId : ${traceId}, Message : ${ex.Message}, StackTrace: ${ex.StackTrace}");

                var httpError = GetHttpErrorFromException(ex, traceId);

                context.Response.StatusCode = httpError.Item1;

                var problemDetails = new ProblemDetails
                {
                    Type = "https://tools.ietf.org/html/rfc7231#section-6.6.1",
                    Title = httpError.Item2,
                    Status = httpError.Item1,
                    Instance = context.Request.Path,
                    Detail = httpError.Item3,
                };
                await context.Response.WriteAsJsonAsync(problemDetails);
            }
        }

        public Tuple<int, string, string> GetHttpErrorFromException(Exception exception, Guid traceId)
        {
            var exceptionType = exception.GetType();
            if (exceptionType == typeof(ResourceAlreadyExists))
            {
                return Tuple.Create((int)StatusCodes.Status409Conflict, "Conflict", $"The resource already exists");
            }
            if (exceptionType == typeof(ResourceNotFoundException))
            {
                return Tuple.Create((int)StatusCodes.Status404NotFound, "Not Found", $"The resource doesn't exists");
            }
            if (exceptionType == typeof(UnauthorizedAccessException))
            {
                return Tuple.Create((int)StatusCodes.Status404NotFound, "Not Found", $"The resource doesn't exists");
            }
            return Tuple.Create((int)StatusCodes.Status500InternalServerError, "Internal Server Error", environment.IsDevelopment() ? exception.Message :  $"Internal server error occured, traceId : {traceId}");
        }
    }
}
