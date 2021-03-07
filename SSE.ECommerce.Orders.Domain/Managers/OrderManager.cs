using System.Linq;
using SSE.ECommerce.Orders.Domain.Interfaces;
using SSE.ECommerce.Orders.Domain.Models;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using SSE.ECommerce.Orders.Data.Interfaces;

namespace SSE.ECommerce.Orders.Domain.Managers
{
    public class OrderManager : IOrderManager
    {
        private readonly IOrderService _orderService;
        private readonly ILogger<OrderManager> _logger;

        public OrderManager(IOrderService orderService, ILogger<OrderManager> logger)
        {
            _orderService = orderService;
            _logger = logger;
        }

        public async Task<OrderDetails> GetOrderDetails(string customerId)
        {
            _logger.LogInformation("Start of GetOrderDetails()");
            var orderDetails = await _orderService.GetOrderDetails(customerId);
            return new OrderDetails
            {
                Order = orderDetails.Select(orderDetail => new Order()
                  {
                      OrderId = orderDetail.Order.OrderId,
                      CustomerId = orderDetail.Order.CustomerId,
                      OrderDate = orderDetail.Order.OrderDate,
                      DeliveryExpected = orderDetail.Order.DeliveryExpected,
                      ContainsGift = orderDetail.Order.ContainsGift,
                      ShippingMode = orderDetail.Order.ShippingMode,
                      OrderSource = orderDetail.Order.OrderSource
                  }).FirstOrDefault(),
                OrderItems = orderDetails.Select(orderDetail => new OrderItem()
                  {
                      OrderItemId = orderDetail.OrderItem.OrderItemId,
                      OrderId = orderDetail.OrderItem.OrderId,
                      ProductId = orderDetail.OrderItem.ProductId,
                      Quantity = orderDetail.OrderItem.Quantity,
                      Price = orderDetail.OrderItem.Price,
                      Returnable = orderDetail.OrderItem.Returnable,
                      Product = new Product
                      {
                          ProductId = orderDetail.Product.ProductId,
                          ProductName = orderDetail.Order.ContainsGift ? "Gift" : orderDetail.Product.ProductName,
                          PackHeight = orderDetail.Product.PackHeight,
                          PackWidth = orderDetail.Product.PackWidth,
                          PackWeight = orderDetail.Product.PackWeight,
                          Colour = orderDetail.Product.Colour,
                          Size = orderDetail.Product.Size
                      }
                  }).ToList()
            };
        }
    }
}
