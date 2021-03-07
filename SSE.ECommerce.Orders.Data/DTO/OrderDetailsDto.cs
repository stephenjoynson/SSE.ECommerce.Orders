using System.Collections.Generic;

namespace SSE.ECommerce.Orders.Data.DTO
{
    public class OrderDetailsDto
    {
        public List<OrderDto> Orders { get; set; }
        public List<OrderItemDto> OrderItems { get; set; }
        public List<ProductDto> Products { get; set; }
    }
}
