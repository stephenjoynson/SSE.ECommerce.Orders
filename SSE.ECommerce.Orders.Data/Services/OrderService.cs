using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using SSE.ECommerce.Orders.Data.DTO;
using SSE.ECommerce.Orders.Data.Interfaces;
using Dapper;

namespace SSE.ECommerce.Orders.Data.Services
{
    public class OrderService : BaseDatabaseService, IOrderService
    {
        private readonly ILogger<OrderService> _logger;

        public OrderService(ILogger<OrderService> logger, IDbConnection dbConnection) : base(dbConnection)
        {
            _logger = logger;
            EnsureDbConnectionIsOpen();
        }

        public async Task<OrderDetailsDto> GetOrderDetails(string customerId, int limit)
        {
            _logger.LogInformation($"About to query database for {customerId}");
            var query = @"SELECT
                o.[ORDERID] As Id,
                o.[ORDERID],
                o.[CUSTOMERID],
                o.[ORDERDATE],
                o.[DELIVERYEXPECTED],
                o.[CONTAINSGIFT],
                o.[SHIPPINGMODE],
                o.[ORDERSOURCE],
                i.[ORDERITEMID],
                i.[ORDERID],
                i.[QUANTITY],
                i.[PRICE],
                i.[RETURNABLE]
                p.[PRODUCTID],
                p.[PRODUCTNAME],
                p.[PACKHEIGHT],
                p.[PACKWIDTH],
                p.[PACKWEIGHT],
                p.[COLOUR],
                p.[SIZE]
                FROM [ORDERS] o
                LEFT OUTER JOIN [ORDERITEMS] i ON o.[ORDERID] = i.[ORDERID]
                LEFT OUTER JOIN [PRODUCTS] p ON i.[PRODUCTID] = p.[PRODUCTID]
                WHERE [CUSTOMERID] = @customerId
                ORDER BY [ORDERDATE] DESC";

            var results = await DbConnection.QueryAsync<OrderDetailsDto, List<OrderDto>, List<OrderItemDto>, List<ProductDto>, OrderDetailsDto>(query,
                (orderdetails, orders, orderitems, products) =>
                {
                    orderdetails.Orders = orders;
                    orderdetails.OrderItems = orderitems;
                    orderdetails.Products = products;
                    return orderdetails;
                }, new
                {
                    customerId
                }, splitOn: "Id,OrderId,OrderItemId");
            return new OrderDetailsDto();
        }
    }
}