using CoffeeBreakAPI.Models.Enum.Product.Cake;
using CoffeeBreakAPI.Models.Order;

namespace CoffeeBreakAPI.Models.Product.Cake
{
    public class Cake : Product
    {
        public CakeType Type { get; private set; }
        public bool IsGlutenFree { get; private set; }
        public int Calories { get; private set; }

        private Cake() { }

        public Cake(
            string name,
            string description,
            Money price,
            string imageUrl,
            CakeType type,
            bool isGlutenFree,
            int calories
        ) : base(name, description, price, imageUrl)
        {
            Type = type;
            IsGlutenFree = isGlutenFree;
            Calories = calories;
        }
    }
}
