using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SSE.ECommerce.Orders.Proxy.Interfaces;
using SSE.ECommerce.Orders.Proxy.Models;

namespace SSE.ECommerce.Orders.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class OrdersController : ControllerBase
    {
        private readonly IMostRecentOrderSummaryProxy _mostRecentOrderSummaryProxy;
        private readonly ILogger<OrdersController> _logger;

        public OrdersController(IMostRecentOrderSummaryProxy mostRecentOrderSummaryProxy, ILogger<OrdersController> logger)
        {
            _mostRecentOrderSummaryProxy = mostRecentOrderSummaryProxy;
            _logger = logger;
        }

        [HttpPost]
        [Route("api/v1/orders/mostrecentordersummary")]
        // TODO: Configure Production Ready Authorization
        // [Authorize]
        public IActionResult MostRecentOrderSummary([FromBody] OrderRequest orderRequest)
        {
            _logger.LogInformation("Start of MostRecentOrderSummary()");

            try
            {
                var response = _mostRecentOrderSummaryProxy.GetMostRecentOrderSummary(orderRequest).Result;

                if (response.Customer.CustomerId == orderRequest.CustomerId)
                {
                    return Ok(response);
                }

                _logger.LogWarning($"Bad Request for {orderRequest.User} {orderRequest.CustomerId}");
                return BadRequest();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Exception for {orderRequest.User} {orderRequest.CustomerId}", ex);
                return Problem(title: "There was a problem with your request");
            }
        }
    }
}
