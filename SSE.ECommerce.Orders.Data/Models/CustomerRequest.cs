using Newtonsoft.Json;

namespace SSE.ECommerce.Orders.Data.Models
{
    public class CustomerRequest
    {
        [JsonProperty("email")]
        public string Email { get; set; }
    }
}
