using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace SSE.ECommerce.Orders.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class OrdersController : ControllerBase
    {
        private readonly ILogger<OrdersController> _logger;

        public OrdersController(ILogger<OrdersController> logger)
        {
            _logger = logger;
        }

        [HttpPost]
        [Route("api/v1/orders/mostrecentordersummary")]
        // TODO: Configure Production Ready Authorization
        // [Authorize]
        public string MostRecentOrderSummary()
        {
            _logger.LogInformation("Start of MostRecentOrderSummary()");
            return string.Empty;
        }
    }
}
