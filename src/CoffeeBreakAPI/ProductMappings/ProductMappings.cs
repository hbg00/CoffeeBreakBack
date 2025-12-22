using CoffeeBreakAPI.Dtos.Product;
using CoffeeBreakAPI.Models.Product;
using CoffeeBreakAPI.Models.Product.Cake;
using CoffeeBreakAPI.Models.Product.Coffee;
using CoffeeBreakAPI.Models.Product.Cookie;
using CoffeeBreakAPI.Models.Product.Tea;

namespace CoffeeBreakAPI.ProductMappings
{
    public static class ProductMappings
    {
        private static ProductDto MapBase(Product source, string productType)
        {
            return new ProductDto
            {
                Id = source.Id,
                Name = source.Name,
                Description = source.Description,
                Price = source.Price.Amount,
                Currency = source.Price.Currency,
                ImageUrl = source.ImageUrl,
                ProductType = productType
            };
        }

        public static CoffeeDto ToDto(this Coffee coffee)
        {
            var baseDto = MapBase(coffee, "Coffee");

            return new CoffeeDto
            {
                Id = baseDto.Id,
                Name = baseDto.Name,
                Description = baseDto.Description,
                Price = baseDto.Price,
                Currency = baseDto.Currency,
                ImageUrl = baseDto.ImageUrl,
                ProductType = baseDto.ProductType,
                CoffeeType = coffee.Type,
                Size = coffee.Size,
                HasMilk = coffee.HasMilk
            };
        }

        public static TeaDto ToDto(this Tea tea)
        {
            var baseDto = MapBase(tea, "Tea");

            return new TeaDto
            {
                Id = baseDto.Id,
                Name = baseDto.Name,
                Description = baseDto.Description,
                Price = baseDto.Price,
                Currency = baseDto.Currency,
                ImageUrl = baseDto.ImageUrl,
                ProductType = baseDto.ProductType,
                TeaType = tea.Type,
                IsHerbal = tea.IsHerbal
            };
        }

        public static CakeDto ToDto(this Cake cake)
        {
            var baseDto = MapBase(cake, "Cake");

            return new CakeDto
            {
                Id = baseDto.Id,
                Name = baseDto.Name,
                Description = baseDto.Description,
                Price = baseDto.Price,
                Currency = baseDto.Currency,
                ImageUrl = baseDto.ImageUrl,
                ProductType = baseDto.ProductType,
                CakeType = cake.Type,
                IsGlutenFree = cake.IsGlutenFree,
                Calories = cake.Calories
            };
        }

        public static CookieDto ToDto(this Cookie cookie)
        {
            var baseDto = MapBase(cookie, "Cookie");

            return new CookieDto
            {
                Id = baseDto.Id,
                Name = baseDto.Name,
                Description = baseDto.Description,
                Price = baseDto.Price,
                Currency = baseDto.Currency,
                ImageUrl = baseDto.ImageUrl,
                ProductType = baseDto.ProductType,
                CookieType = cookie.Type,
                HasChocolate = cookie.HasChocolate
            };
        }
    }
}
