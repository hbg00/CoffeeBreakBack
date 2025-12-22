using CoffeeBreakAPI.Dtos.Product;
using CoffeeBreakAPI.Interfaces;
using CoffeeBreakAPI.Models.Enum.Product.Cake;
using CoffeeBreakAPI.Models.Enum.Product.Coffee;
using CoffeeBreakAPI.Models.Enum.Product.Cookie;
using CoffeeBreakAPI.Models.Enum.Product.Tea;
using CoffeeBreakAPI.Models.Product.Cake;
using CoffeeBreakAPI.Models.Product.Coffee;
using CoffeeBreakAPI.Models.Product.Cookie;
using CoffeeBreakAPI.Models.Product.Tea;
using CoffeeBreakAPI.ProductMappings;
using Microsoft.AspNetCore.Mvc;

namespace CoffeeBreakAPI.Controllers
{
    [ApiController]
    [Route("api/products")]
    public class ProductsController : ControllerBase
    {
        private readonly IProductRepository _repository;

        public ProductsController(IProductRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductDto>>> GetAll()
        {
            var products = await _repository.GetAllAsync();

            var result = products.Select(p => p switch
            {
                Coffee c => (ProductDto)c.ToDto(),
                Tea t => t.ToDto(),
                Cake ca => ca.ToDto(),
                Cookie co => co.ToDto(),
                _ => new ProductDto
                {
                    Id = p.Id,
                    Name = p.Name,
                    Description = p.Description,
                    Price = p.Price.Amount,
                    Currency = p.Price.Currency,
                    ImageUrl = p.ImageUrl,
                    ProductType = "Unknown"
                }
            });

            return Ok(result);
        }

        [HttpGet("{id:guid}")]
        public async Task<ActionResult<ProductDto>> GetById(Guid id)
        {
            var product = await _repository.GetByIdAsync(id);
            if (product == null)
                return NotFound();

            ProductDto dto = product switch
            {
                Coffee c => c.ToDto(),
                Tea t => t.ToDto(),
                Cake ca => ca.ToDto(),
                Cookie co => co.ToDto(),
                _ => new ProductDto
                {
                    Id = product.Id,
                    Name = product.Name,
                    Description = product.Description,
                    Price = product.Price.Amount,
                    Currency = product.Price.Currency,
                    ImageUrl = product.ImageUrl,
                    ProductType = "Unknown"
                }
            };

            return Ok(dto);
        }

        [HttpGet("coffee")]
        public async Task<ActionResult<IEnumerable<CoffeeDto>>> GetCoffees([FromQuery] CoffeeType? type)
        {
            if (type.HasValue)
            {
                var coffeesByType = await _repository.GetCoffeesByTypeAsync(type.Value);
                return Ok(coffeesByType.Select(c => c.ToDto()));
            }

            var coffees = await _repository.GetCoffeesAsync();
            return Ok(coffees.Select(c => c.ToDto()));
        }

        [HttpGet("coffee/types")]
        public ActionResult<IEnumerable<string>> GetCoffeeTypes()
            => Ok(Enum.GetNames(typeof(CoffeeType)));

        [HttpGet("tea")]
        public async Task<ActionResult<IEnumerable<TeaDto>>> GetTeas([FromQuery] TeaType? type)
        {
            if (type.HasValue)
            {
                var teasByType = await _repository.GetTeasByTypeAsync(type.Value);
                return Ok(teasByType.Select(t => t.ToDto()));
            }

            var teas = await _repository.GetTeasAsync();
            return Ok(teas.Select(t => t.ToDto()));
        }

        [HttpGet("tea/types")]
        public ActionResult<IEnumerable<string>> GetTeaTypes()
            => Ok(Enum.GetNames(typeof(TeaType)));

        [HttpGet("cake")]
        public async Task<ActionResult<IEnumerable<CakeDto>>> GetCakes([FromQuery] CakeType? type)
        {
            if (type.HasValue)
            {
                var cakesByType = await _repository.GetCakesByTypeAsync(type.Value);
                return Ok(cakesByType.Select(c => c.ToDto()));
            }

            var cakes = await _repository.GetCakesAsync();
            return Ok(cakes.Select(c => c.ToDto()));
        }

        [HttpGet("cake/types")]
        public ActionResult<IEnumerable<string>> GetCakeTypes()
            => Ok(Enum.GetNames(typeof(CakeType)));

        [HttpGet("cookie")]
        public async Task<ActionResult<IEnumerable<CookieDto>>> GetCookies([FromQuery] CookieType? type)
        {
            if (type.HasValue)
            {
                var cookiesByType = await _repository.GetCookiesByTypeAsync(type.Value);
                return Ok(cookiesByType.Select(c => c.ToDto()));
            }

            var cookies = await _repository.GetCookiesAsync();
            return Ok(cookies.Select(c => c.ToDto()));
        }

        [HttpGet("cookie/types")]
        public ActionResult<IEnumerable<string>> GetCookieTypes()
            => Ok(Enum.GetNames(typeof(CookieType)));
    }
}
