using Newtonsoft.Json;

namespace SSE.ECommerce.Orders.Data.DTO
{
    public class CustomerRequest
    {
        [JsonProperty("email")]
        public string Email { get; set; }
    }
}
