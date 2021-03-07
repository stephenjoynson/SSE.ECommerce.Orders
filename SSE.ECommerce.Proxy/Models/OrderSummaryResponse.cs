using Newtonsoft.Json;

namespace SSE.ECommerce.Orders.Proxy.Models
{
    public class OrderSummaryResponse
    {
        [JsonProperty("customer")] 
        public Customer Customer { get; set; }
        [JsonProperty("order")] 
        public Order Order { get; set; }
    }
}
