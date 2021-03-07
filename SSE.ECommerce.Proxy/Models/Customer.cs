using System.Text.Json.Serialization;

namespace SSE.ECommerce.Orders.Proxy.Models
{
    public class Customer
    {
        [JsonIgnore]
        public string CustomerId { get; set; }
        [JsonPropertyName("firstName")]
        public string FirstName { get; set; }
        [JsonPropertyName("lastName")] 
        public string LastName { get; set; }
    }
}
