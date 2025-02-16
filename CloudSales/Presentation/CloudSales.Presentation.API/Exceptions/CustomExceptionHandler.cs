using CloudSales.Domain.Exceptions;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace CloudSales.Presentation.API.Exceptions
{
    public class CustomExceptionHandler : IExceptionHandler
    {
        private readonly ILogger<CustomExceptionHandler> _logger;

        public CustomExceptionHandler(ILogger<CustomExceptionHandler> logger)
        {
            _logger = logger;
        }

        public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
        {
            HttpStatusCode statusCode;
            string message;

            _logger.LogError(exception, "Unhandled exception occurred");

            if (exception is InvalidParameterException invalidParameterException)
            {
                statusCode = HttpStatusCode.BadRequest;
                message = invalidParameterException.Message;
            }
            else if (exception is NotFoundException notFoundException)
            {
                statusCode = HttpStatusCode.NotFound;
                message = notFoundException.Message;
            }
            else if (exception is ValidationException validationException)
            {
                statusCode = HttpStatusCode.BadRequest;
                message = validationException.Message;
            }
            else
            {
                statusCode = HttpStatusCode.InternalServerError;
                message = "Oops, something went wrong";
            }

            var problemDetails = new ProblemDetails()
            {
                Status = (int) statusCode,
                Detail = message
            };
            
            httpContext.Response.StatusCode = (int) statusCode;
            await httpContext.Response.WriteAsJsonAsync(problemDetails);

            return true;
        }
    }
}
