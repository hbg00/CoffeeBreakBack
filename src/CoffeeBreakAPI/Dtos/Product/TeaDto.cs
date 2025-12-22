using CoffeeBreakAPI.Models.Enum.Product.Tea;

namespace CoffeeBreakAPI.Dtos.Product
{
    public class TeaDto : ProductDto
    {
        public TeaType TeaType { get; set; }

        public bool IsHerbal { get; set; }
    }
}
