using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using SSE.ECommerce.Orders.Domain.Interfaces;
using SSE.ECommerce.Orders.Proxy.Interfaces;
using SSE.ECommerce.Orders.Proxy.Models;

namespace SSE.ECommerce.Orders.Proxy.Proxies
{
    public class MostRecentOrderSummaryProxy : IMostRecentOrderSummaryProxy
    {
        private readonly ICustomerManager _customerManager;
        private readonly IOrderManager _orderManager;
        private readonly ILogger<MostRecentOrderSummaryProxy> _logger;

        public MostRecentOrderSummaryProxy(ICustomerManager customerManager, IOrderManager orderManager, ILogger<MostRecentOrderSummaryProxy> logger)
        {
            _customerManager = customerManager;
            _orderManager = orderManager;
            _logger = logger;
        }

        public OrderSummaryResponse GetMostRecentOrderSummary(OrderRequest orderRequest)
        {
            _logger.LogInformation("Start of GetMostRecentOrderSummary()");
            var customer = Task.Run(() => _customerManager.GetCustomerDetails(orderRequest.User)).Result;
            var orderDetails = Task.Run(() => _orderManager.GetOrderDetails(orderRequest.CustomerId, 1)).Result;
            return new OrderSummaryResponse
            {
                Customer = new Customer
                {
                    FirstName = customer.FirstName,
                    LastName = customer.LastName
                }
            };
        }
    }
}
