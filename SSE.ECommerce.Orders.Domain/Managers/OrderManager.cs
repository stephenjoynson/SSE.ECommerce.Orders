using System.Collections.Generic;
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

        public async Task<OrderDetails> GetOrderDetails(string customerId, int limit)
        {
            var orderDetails = await _orderService.GetOrderDetails(customerId, limit);
            return new OrderDetails
            {
                Orders = (from order in orderDetails.Orders select new Order() {
                    OrderId = order.OrderId,
                    CustomerId = order.CustomerId,
                    OrderDate = order.OrderDate,
                    DeliveryExpected = order.DeliveryExpected,
                    ContainsGift = order.ContainsGift,
                    ShippingMode = order.ShippingMode,
                    OrderSource = order.OrderSource
                }).ToList(),
                //OrderItems = (from orderItem in orderDetails.OrderItems select new OrderItem() {
                //    OrderItemId = orderItem.OrderItemId,
                //    OrderId = orderItem.OrderId,
                //    ProductId = orderItem.ProductId,
                //    Quantity = orderItem.Quantity,
                //    Price = orderItem.Price,
                //    Returnable = orderItem.Returnable
                //}).ToList(),
                //Products = (from product in orderDetails.Products select new Product() {
                //    ProductId = product.ProductId,
                //    ProductName = product.ProductName,
                //    PackHeight = product.PackHeight,
                //    PackWidth = product.PackWidth,
                //    PackWeight = product.PackWeight,
                //    Colour = product.Colour,
                //    Size = product.Size
                //}).ToList()
            };
        }
    }
}
