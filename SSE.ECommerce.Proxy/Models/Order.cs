using System.Collections.Generic;
using Newtonsoft.Json;

namespace SSE.ECommerce.Orders.Proxy.Models
{
    public class Order
    {
        [JsonProperty("orderNumber")] 
        public int OrderNumber { get; set; }
        [JsonProperty("orderDate")] 
        public string OrderDate { get; set; }
        [JsonProperty("deliveryAddress")] 
        public string DeliveryAddress { get; set; }
        [JsonProperty("orderItems")] 
        public List<OrderItem> OrderItems { get; set; }
        [JsonProperty("deliveryExpected")] 
        public string DeliveryExpected { get; set; }
    }
}
