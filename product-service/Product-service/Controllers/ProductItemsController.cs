using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

using Product_service.Models;
using Product_service.Services;

namespace Product_service.Controllers
{
    [Route("api/[ProductItems]")]
    [ApiController]
    public class ProductItemsController : ControllerBase
    {
        private readonly ProductService _service;

        public ProductItemsController(ProductService productService)
        {
            _service = productService;
        }

        // GET: api/ProductItems
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductItemDTO>>> 
            GetProductItems()
        {
            return (await _service.GetProductsAsync())
                .Select(x => ItemToDTO(x)).ToList();
        }

        // GET: api/ProductItems/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ProductItemDTO>> GetProduct(string id)
        {
            var product = await _service.GetProductAsync(id);

            if (product == null)
            {
                return NotFound();
            }

            return ItemToDTO(product);
        }

        // PUT: api/ProductItems/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutProduct(string id, ProductItemDTO productItemDTO)
        {
            if (id != productItemDTO.Id)
            {
                return BadRequest();
            }

            var product = await _service.GetProductAsync(id);
            if (product is null)
            {
                return NotFound();
            }

            await _service.UpdateAsync(id, new Product{
                Id = productItemDTO.Id,
                Name = productItemDTO.Name,
                IsComplete = productItemDTO.IsComplete
            });

            return NoContent();
        }

        // POST: api/ProductItems
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<ProductItemDTO>> PostProduct(ProductItemDTO productDTO)
        {
            var product = new Product {
                IsComplete = productDTO.IsComplete,
                Name = productDTO.Name
            };

            await _service.CreateAsync(product);

            return CreatedAtAction(nameof(GetProduct), new { id = product.Id }, ItemToDTO(product));
        }

        // DELETE: api/ProductItems/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(string id)
        {
            var product = _service.GetProductAsync(id);

            if (product is null)
            {
                return NotFound();
            }

            await _service.RemoveAsync(id);
            return NoContent();
        }

        private static ProductItemDTO ItemToDTO (Product ProductItem)
        {
            return new ProductItemDTO
            {
                Id = ProductItem.Id,
                Name = ProductItem.Name,
                IsComplete = ProductItem.IsComplete
            };
        }
    }
}
