namespace CoffeeBreakAPI.Models.Order
{
    public sealed class Money : IEquatable<Money>
    {
        public decimal Amount { get; }
        public string Currency { get; }

        private Money() { }

        public Money(decimal amount, string currency)
        {
            if (amount < 0)
                throw new ArgumentException("Amount cannot be negative");

            if (string.IsNullOrWhiteSpace(currency))
                throw new ArgumentException("Currency is required");

            Amount = decimal.Round(amount, 2);
            Currency = currency.ToUpperInvariant();
        }

        public static Money PLN(decimal amount) => new(amount, "PLN");
        public static Money EUR(decimal amount) => new(amount, "EUR");

        public Money Add(Money other)
        {
            EnsureSameCurrency(other);
            return new Money(Amount + other.Amount, Currency);
        }

        public Money Multiply(int quantity)
        {
            if (quantity <= 0)
                throw new ArgumentException("Quantity must be positive");

            return new Money(Amount * quantity, Currency);
        }

        private void EnsureSameCurrency(Money other)
        {
            if (Currency != other.Currency)
                throw new InvalidOperationException("Currencies must match");
        }

        public bool Equals(Money? other)
        {
            if (other is null) return false;
            return Amount == other.Amount && Currency == other.Currency;
        }

        public override bool Equals(object? obj) => Equals(obj as Money);
        public override int GetHashCode() => HashCode.Combine(Amount, Currency);
    }
}
