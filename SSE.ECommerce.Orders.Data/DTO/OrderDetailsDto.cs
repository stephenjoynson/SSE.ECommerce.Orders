using System.Collections.Generic;

namespace SSE.ECommerce.Orders.Data.DTO
{
    public class OrderDetailsDto
    {
        public OrderDto Order { get; set; }
        public OrderItemDto OrderItem { get; set; }
        public ProductDto Product { get; set; }
    }
}
