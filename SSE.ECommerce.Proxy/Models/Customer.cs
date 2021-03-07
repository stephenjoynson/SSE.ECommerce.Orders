using Newtonsoft.Json;

namespace SSE.ECommerce.Orders.Proxy.Models
{
    public class Customer
    {
        [JsonProperty("firstName")]
        public string FirstName { get; set; }
        [JsonProperty("lastName")] 
        public string LastName { get; set; }
    }
}
