using System.Threading.Tasks;
using SSE.ECommerce.Orders.Proxy.Models;

namespace SSE.ECommerce.Orders.Proxy.Interfaces
{
    public interface IMostRecentOrderSummaryProxy
    {
        Task<OrderSummaryResponse> GetMostRecentOrderSummary(OrderRequest orderRequest);
    }
}