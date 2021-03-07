using SSE.ECommerce.Orders.Proxy.Models;

namespace SSE.ECommerce.Orders.Proxy.Interfaces
{
    public interface IMostRecentOrderSummaryProxy
    {
        OrderSummaryResponse GetMostRecentOrderSummary(string email);
    }
}