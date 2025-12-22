using CoffeeBreakAPI.Models.Enum.Product.Cake;

namespace CoffeeBreakAPI.Dtos.Product
{
    public class CakeDto: ProductDto
    {
        public CakeType CakeType { get; set; }

        public bool IsGlutenFree { get; set; }

        public int Calories { get; set; }
    }
}
