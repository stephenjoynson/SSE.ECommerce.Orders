using System.Collections.Generic;

namespace SSE.ECommerce.Orders.Domain.Models
{
    public class OrderDetails
    {
        public List<Order> Orders { get; set; }
        public List<OrderItem> OrderItems { get; set; }
        public List<Product> Products { get; set; }
    }
}
