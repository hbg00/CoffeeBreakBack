using CoffeeBreakAPI.Models.Enum.Product.Coffee;

namespace CoffeeBreakAPI.Dtos.Product
{
    public class CoffeeDto : ProductDto
    {
        public CoffeeType CoffeeType { get; set; }

        public CoffeeSize Size { get; set; }

        public bool HasMilk { get; set; }
    }
}
