using CoffeeBreakAPI.Models.Order;

namespace CoffeeBreakAPI.Models.Product
{
    public abstract class Product
    {
        public Guid Id { get; protected set; }
        public string Name { get; protected set; }
        public string Description { get; protected set; }
        public Money Price { get; protected set; }
        public string ImageUrl { get; protected set; }
        public bool IsAvailable { get; protected set; }

        protected Product() { }

        protected Product(string name, string description, Money price, string imageUrl)
        {
            Id = Guid.NewGuid();
            Name = name;
            Description = description;
            Price = price;
            ImageUrl = imageUrl;
            IsAvailable = true;
        }

        public void Disable() => IsAvailable = false;
        public void Enable() => IsAvailable = true;
    }
}