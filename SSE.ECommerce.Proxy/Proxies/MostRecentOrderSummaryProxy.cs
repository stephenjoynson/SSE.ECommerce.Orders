using System;
using System.Linq;
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

        public async Task<OrderSummaryResponse> GetMostRecentOrderSummary(OrderRequest orderRequest)
        {
            if (string.IsNullOrWhiteSpace(orderRequest.User))
            {
                throw new ArgumentNullException(nameof(orderRequest.User), "No value supplied for argument");
            }

            if (string.IsNullOrWhiteSpace(orderRequest.CustomerId))
            {
                throw new ArgumentNullException(nameof(orderRequest.CustomerId), "No value supplied for argument");
            }

            _logger.LogInformation("Start of GetMostRecentOrderSummary()");

            var customer = await _customerManager.GetCustomerDetails(orderRequest.User);
            var orderDetails = await _orderManager.GetOrderDetails(orderRequest.CustomerId);
            var order = orderDetails.Order;

            return new OrderSummaryResponse
            {
                Customer = new Customer
                {
                    CustomerId = customer.CustomerId,
                    FirstName = customer.FirstName,
                    LastName = customer.LastName,
                },
                Order = order != null ? new Order
                {
                    OrderNumber = order.OrderId,
                    OrderDate = order.OrderDate.ToString("dd-MMM-yyyy"),
                    DeliveryAddress = $"{customer.HouseNumber} {customer.Street}, {customer.Town}, {customer.Postcode}",
                    OrderItems = (from orderItem in orderDetails.OrderItems select new OrderItem()
                    {
                        Product = orderItem.Product.ProductName,
                        Quantity = orderItem.Quantity,
                        PriceEach = orderItem.Price
                    }).ToList(),
                    DeliveryExpected = order.DeliveryExpected.ToString("dd-MMM-yyyy")
                } : null
            };
        }
    }
}
