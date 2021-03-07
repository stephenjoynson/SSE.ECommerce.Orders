using Newtonsoft.Json;

namespace SSE.ECommerce.Orders.Proxy.Models
{
    public class OrderItem
    {
        [JsonProperty("product")] 
        public string Product { get; set; }
        [JsonProperty("quantity")] 
        public int Quantity { get; set; }
        [JsonProperty("priceEach")] 
        public decimal? PriceEach { get; set; }

    }
}
