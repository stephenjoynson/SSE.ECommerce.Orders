using System.Collections.Generic;

namespace SSE.ECommerce.Orders.Domain.Models
{
    public class OrderDetails
    {
        public Order Order { get; set; }
        public List<OrderItem> OrderItems { get; set; }
    }
}
