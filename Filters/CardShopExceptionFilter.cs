using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Http.Timeouts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Build.Exceptions;

namespace CardShop.Filters
{
    public class CardShopExceptionFilter : IExceptionFilter
    {
        private readonly ILogger<CardShopExceptionFilter> _logger;

        public CardShopExceptionFilter(ILogger<CardShopExceptionFilter> logger)
        {
            _logger = logger;
        }
        public void OnException(ExceptionContext context)
        {
            var isDevelopment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") == "Development";

            _logger.LogError("Exception error not treated.");

            var exceptionMappings = new Dictionary<Type, (int StatusCode, string message)>
            {
                {typeof(ArgumentException), (StatusCodes.Status400BadRequest, "Invalid request parameter") },
                {typeof(TimeoutException), (StatusCodes.Status408RequestTimeout, "Operation exceeded time limit") },
                {typeof(UnauthorizedAccessException), (StatusCodes.Status403Forbidden, "Request limit exceeded") },
            };
            var exceptionType = context.Exception.GetType();
            if (exceptionMappings.TryGetValue(exceptionType, out var response))
            {
                context.Result = CreateResponse(response.StatusCode, response.message);
                return;
            }
            var errorMessage = isDevelopment
                ? $"{context.Exception.Message} - {context.Exception.StackTrace}"
                : "An error occurred while processing your request.";
            context.Result = CreateResponse(response.StatusCode, response.message);
        }

        private ObjectResult CreateResponse(int statusCode, string message)
        {
            return new ObjectResult(new { Message = message })
            {
                StatusCode = statusCode,
            };
        }
    }
}
