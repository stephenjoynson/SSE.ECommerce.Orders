using System.Collections.Generic;
using System.Threading.Tasks;
using SSE.ECommerce.Orders.Data.DTO;

namespace SSE.ECommerce.Orders.Data.Interfaces
{
    public interface IOrderService
    {
        Task<List<OrderDetailsDto>> GetOrderDetails(string customerId);
    }
}