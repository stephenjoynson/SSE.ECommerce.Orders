using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using SSE.ECommerce.Orders.Data.DTO;
using SSE.ECommerce.Orders.Data.Interfaces;

namespace SSE.ECommerce.Orders.Data.Services
{
    public class OrderService : IOrderService
    {
        private readonly ILogger<OrderService> _logger;

        public OrderService(ILogger<OrderService> logger)
        {
            _logger = logger;
        }

        public async Task<OrderDetailsDto> GetOrderDetails(string customerId, int limit)
        {
            _logger.LogInformation($"About to query database for {customerId}");
            throw new System.NotImplementedException();
        }
    }
}