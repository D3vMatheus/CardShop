using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

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
            _logger.LogError("Exception error not treated.");
            context.Result = new ObjectResult("An error occured while your request was treated")
            {
                StatusCode = StatusCodes.Status500InternalServerError,
            };

        }
    }
}
