using CoffeeBreakAPI.Models.Enum.Product.Coffee;
using CoffeeBreakAPI.Models.Order;

namespace CoffeeBreakAPI.Models.Product.Coffee
{
    public class Coffee : Product
    {
        public CoffeeType Type { get; private set; }
        public CoffeeSize Size { get; private set; }
        public bool HasMilk { get; private set; }

        private Coffee() { }

        public Coffee(
            string name,
            string description,
            Money price,
            string imageUrl,
            CoffeeType type,
            CoffeeSize size,
            bool hasMilk
        ) : base(name, description, price, imageUrl)
        {
            Type = type;
            Size = size;
            HasMilk = hasMilk;
        }
    }
}
