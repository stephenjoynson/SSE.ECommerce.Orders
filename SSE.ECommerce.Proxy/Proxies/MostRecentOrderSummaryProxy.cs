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
        private readonly ILogger<MostRecentOrderSummaryProxy> _logger;

        public MostRecentOrderSummaryProxy(ICustomerManager customerManager, ILogger<MostRecentOrderSummaryProxy> logger)
        {
            _customerManager = customerManager;
            _logger = logger;
        }

        public OrderSummaryResponse GetMostRecentOrderSummary(string email)
        {
            _logger.LogInformation("Start of GetMostRecentOrderSummary()");
            var customer = Task.Run(() => _customerManager.GetCustomerDetails(email)).Result;
            return new OrderSummaryResponse
            {
                Customer =
                {
                    FirstName = customer.FirstName,
                    LastName = customer.LastName
                }
            };
        }
    }
}
