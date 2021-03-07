using System.Collections.Generic;
using System.Data;
using System.Linq;
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

        public async Task<List<OrderDetailsDto>> GetOrderDetails(string customerId)
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
                i.[PRODUCTID],
                i.[QUANTITY],
                i.[PRICE],
                i.[RETURNABLE],
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
                AND o.[ORDERID] = (SELECT TOP 1 [ORDERID] FROM [ORDERS] WHERE [CUSTOMERID] = @customerId ORDER BY [ORDERDATE] DESC)";

            var results = await DbConnection.QueryAsync<OrderDetailsDto, OrderDto, OrderItemDto, ProductDto, OrderDetailsDto>(query,
                (orderdetails, order, orderitem, product) =>
                {
                    orderdetails.Order = order;
                    orderdetails.OrderItem = orderitem;
                    orderdetails.Product = product;
                    return orderdetails;
                }, new
                {
                    customerId
                }, splitOn: "Id,OrderId,OrderItemId,ProductId");
             return results.ToList();
        }
    }
}