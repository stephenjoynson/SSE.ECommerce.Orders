using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;
using SSE.ECommerce.Orders.Data.DTO;
using SSE.ECommerce.Orders.Data.Interfaces;
using SSE.ECommerce.Orders.Domain.Interfaces;
using SSE.ECommerce.Orders.Domain.Managers;

namespace SSE.ECommerce.Orders.Test.Domain
{
    [TestFixture]
    public class OrderManagerTests
    {
        private Mock<ILogger<OrderManager>> _mockLogger;
        private Mock<IOrderService> _mockOrderService;
        private IOrderManager _orderManager;
        private const string CustomerId = "X123456";

        private static readonly List<OrderDetailsDto> OrderDetails = new List<OrderDetailsDto>
        {
            new OrderDetailsDto
            {
                Order = new OrderDto
                {
                    OrderId = 123,
                    CustomerId = CustomerId
                },
                OrderItem = new OrderItemDto
                {
                    OrderItemId = 567,
                    Quantity = 5
                },
                Product = new ProductDto
                {
                    ProductId = 890,
                    ProductName = "Book"
                }
            }
        };

        private static readonly List<OrderDetailsDto> OrderDetailsForGift = new List<OrderDetailsDto>
        {
            new OrderDetailsDto
            {
                Order = new OrderDto
                {
                    OrderId = 123,
                    CustomerId = CustomerId,
                    ContainsGift = true
                },
                OrderItem = new OrderItemDto
                {
                    OrderItemId = 567,
                    Quantity = 5
                },
                Product = new ProductDto
                {
                    ProductId = 890,
                    ProductName = "Book"
                }
            },
            new OrderDetailsDto
            {
                Order = new OrderDto
                {
                    OrderId = 123,
                    CustomerId = CustomerId,
                    ContainsGift = true
                },
                OrderItem = new OrderItemDto
                {
                    OrderItemId = 891,
                    Quantity = 2
                },
                Product = new ProductDto
                {
                    ProductId = 891,
                    ProductName = "Paper"
                }
            }
        };

        [SetUp]
        public void SetUp()
        {
            _mockLogger = new Mock<ILogger<OrderManager>>();
            _mockOrderService = new Mock<IOrderService>();
            _mockOrderService.Setup(m => m.GetOrderDetails(CustomerId)).ReturnsAsync(OrderDetails);
            _orderManager = new OrderManager(_mockOrderService.Object, _mockLogger.Object);
        }

        [Test]
        [TestCase("")]
        [TestCase(" ")]
        [TestCase(null)]
        public void When_CustomerId_Is_Not_Supplied__Then_GetOrderDetails_Throws_ArgumentNullException(string customerId)
        {
            // Arrange

            // Act

            // Assert
            Assert.ThrowsAsync<ArgumentNullException>(() => _orderManager.GetOrderDetails(customerId));
        }

        [Test]
        public void When_CustomerId_Is_Supplied__Then_GetOrderDetails_Calls_Service_Once()
        {
            // Arrange

            // Act
            _orderManager.GetOrderDetails(CustomerId);

            // Assert
            _mockOrderService.Verify(m => m.GetOrderDetails(CustomerId), Times.Once);
        }

        [Test]
        public void When_CustomerId_Is_Supplied__Then_GetOrderDetails_Returns_OrderDetails()
        {
            // Arrange

            // Act
            var customerManagerResponse = _orderManager.GetOrderDetails(CustomerId).Result;

            // Assert
            Assert.Multiple(() =>
            {
                var expected = OrderDetails.First();
                Assert.AreEqual(expected.Order.CustomerId, customerManagerResponse.Order.CustomerId);
                Assert.AreEqual(expected.Order.OrderId, customerManagerResponse.Order.OrderId);
                Assert.AreEqual(expected.OrderItem.Quantity, customerManagerResponse.OrderItems.First().Quantity);
                Assert.AreEqual(expected.Product.ProductId, customerManagerResponse.OrderItems.First().Product.ProductId);
                Assert.AreEqual(expected.Product.ProductName, customerManagerResponse.OrderItems.First().Product.ProductName);
            });
        }

        [Test]
        public void When_Order_Contains_Gift__Then_GetOrderDetails_Returns_OrderDetails_With_Gift_For_All_ProductNames()
        {
            // Arrange
            _mockOrderService.Setup(m => m.GetOrderDetails(CustomerId)).ReturnsAsync(OrderDetailsForGift);

            // Act
            var customerManagerResponse = _orderManager.GetOrderDetails(CustomerId).Result;

            // Assert
            Assert.IsTrue(customerManagerResponse.OrderItems.All(i => string.Compare(i.Product.ProductName, "Gift", StringComparison.CurrentCultureIgnoreCase) == 0));
        }
    }
}