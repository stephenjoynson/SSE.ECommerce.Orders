using System.Threading.Tasks;
using SSE.ECommerce.Orders.Domain.Models;

namespace SSE.ECommerce.Orders.Domain.Interfaces
{
    public interface IOrderManager
    {
        Task<OrderDetails> GetOrderDetails(string customerId, int limit);
    }
}