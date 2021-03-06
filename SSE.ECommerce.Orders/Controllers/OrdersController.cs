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

        [HttpGet]
        public string Get()
        {
            _logger.LogInformation("Start of Get()");
            return string.Empty;
        }
    }
}
