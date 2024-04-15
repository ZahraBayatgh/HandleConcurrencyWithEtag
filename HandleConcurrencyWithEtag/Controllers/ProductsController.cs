using HandleConcurrencyWithEtag.Dtos;
using HandleConcurrencyWithEtag.Repositories;

namespace HandleConcurrencyWithEtag.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Net.Http.Headers;
    using System.Linq;

    [ApiController]
    [Route("api/products")]
    public class ProductsController : ControllerBase
    {
        private readonly IProductRepository _productRepository;

        public ProductsController(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }


        // POST api/products
        [HttpPost]
        public IActionResult AddProduct([FromBody] CreateProductDto productDto)
        {
            if (productDto == null)
            {
                return BadRequest("Product data is required");
            }

            var newProductId = _productRepository.Add(productDto);

            return CreatedAtAction(nameof(GetProductById), new { id = newProductId }, newProductId);
        }

        [HttpGet("{id}")]
        public IActionResult GetProductById(int id)
        {
            var product = _productRepository.GetById(id);
            if (product == null)
            {
                return NotFound();
            }
 
            return Ok(product);
        }

        [HttpPut("{id}")]
        public IActionResult UpdateProduct(int id, [FromBody] CreateProductDto updatedProduct)
        {
            // Update repository or database
            _productRepository.Update(id,updatedProduct);

            // Return HTTP 204 No Content on successful update
            return NoContent();
        }
        // DELETE api/products/{id}
        [HttpDelete("{id}")]
        public IActionResult DeleteProduct(int id)
        {
            var existingProduct = _productRepository.GetById(id);
            if (existingProduct == null)
            {
                return NotFound();
            }

            _productRepository.Delete(id);

            return NoContent();
        }
    }



}
