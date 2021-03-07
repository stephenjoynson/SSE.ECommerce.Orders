using System.Threading.Tasks;
using SSE.ECommerce.Orders.Data.DTO;

namespace SSE.ECommerce.Orders.Data.Interfaces
{
    public interface IOrderService
    {
        Task<OrderDetailsDto> GetOrderDetails(string customerId, int limit);
    }
}