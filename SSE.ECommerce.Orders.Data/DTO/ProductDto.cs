namespace SSE.ECommerce.Orders.Data.DTO
{
    public class ProductDto
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public decimal? PackHeight { get; set; }
        public decimal? PackWidth { get; set; }
        public decimal? PackWeight { get; set; }
        public string Colour { get; set; }
        public string Size { get; set; }
    }
}