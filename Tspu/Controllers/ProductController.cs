using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Tspu.Models;
using Tspu.Contracts;

namespace Tspu.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {

        private static readonly List<Product> products = new List<Product>();

        [HttpGet]
        public IActionResult Get()
        {
            return Ok(products);
        }
        [HttpGet("{id}")]

        public IActionResult Get([FromRoute] Guid id)
        {
            foreach (var product in products)
            {
                if (product.Id == id)
                {
                    return Ok(product);
                }

            }
            return BadRequest($"Item {id} not found");
        }
        [HttpPost]

        public IActionResult Add([FromBody] AddProductRequestContract requestProduct)
        {
            var newProduct = new Product
            {
                Id = Guid.NewGuid(),
                Title = requestProduct.Title,
                Description = requestProduct.Description,
                Price = requestProduct.Price,
            };
            products.Add(newProduct);
            return Ok();
        }

        [HttpPut]
        public IActionResult Put()
        {
            return Ok(products);
        }

        [HttpPut("{id}")]
        public IActionResult Update([FromRoute] Guid id, [FromBody] Product updatedProduct)
        {
            var productToUpdate = products.FirstOrDefault(p => p.Id == id);
            if (productToUpdate != null)
            {
                productToUpdate.Title = updatedProduct.Title;
                productToUpdate.Description = updatedProduct.Description;
                productToUpdate.Price = updatedProduct.Price;
                return Ok(productToUpdate);
            }
            else
            {
                return BadRequest($"Item {id} not found");
            }
        }
    }
}
