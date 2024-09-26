using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace CardShop.Filters
{
    public class CardShopLoggingFilter : IExceptionFilter
    {
        private readonly ILogger<CardShopLoggingFilter> _logger;

        public CardShopLoggingFilter(ILogger<CardShopLoggingFilter> logger)
        {
            _logger = logger;
        }
        public void OnException(ExceptionContext context)
        {
            _logger.LogError("Exception error not treated.");
            context.Result = new ObjectResult("An error occured while your request was treated")
            {
                StatusCode = StatusCodes.Status500InternalServerError,
            };

        }
    }
}
