using CoffeeBreakAPI.Models.Enum.Product.Tea;
using CoffeeBreakAPI.Models.Order;

namespace CoffeeBreakAPI.Models.Product.Tea
{
    public class Tea : Product
    {
        public TeaType Type { get; private set; }
        public bool IsHerbal { get; private set; }

        private Tea() { }

        public Tea(
            string name,
            string description,
            Money price,
            string imageUrl,
            TeaType type,
            bool isHerbal
        ) : base(name, description, price, imageUrl)
        {
            Type = type;
            IsHerbal = isHerbal;
        }
    }
}
