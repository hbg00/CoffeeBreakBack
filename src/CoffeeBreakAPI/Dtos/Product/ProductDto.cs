namespace CoffeeBreakAPI.Dtos.Product
{
    public class ProductDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = default!;
        public string Description { get; set; } = default!;
        public decimal Price { get; set; }
        public string Currency { get; set; } = default!;
        public string ImageUrl { get; set; } = default!;
        public string ProductType { get; set; } = string.Empty;
    }
}
