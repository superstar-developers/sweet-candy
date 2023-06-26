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
        // private readonly ProductItemContext _context;

        private readonly ProductService _service;

        public ProductItemsController(ProductService productService)
        {
            // _context = context;
            _service = productService;
        }

        // GET: api/ProductItems
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductItemDTO>>> 
            GetProductItems()
        {
        //   if (_service.GetProductsAsync() == null)
        //   {
        //       return NotFound();
        //   }
            // return await _context.ProductItems.ToListAsync();
            return (await _service.GetProductsAsync())
                .Select(x => ItemToDTO(x)).ToList();
        }

        // GET: api/ProductItems/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ProductItemDTO>> GetProduct(string id)
        {
        //   if (_context.ProductItems == null)
        //   {
        //       return NotFound();
        //   }
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

            // _context.Entry(productItemDTO).State = EntityState.Modified;

            // try
            // {
            //     await _context.SaveChangesAsync();
            // }
            // catch (DbUpdateConcurrencyException)
            // {
            //     if (!ProductExists(id))
            //     {
            //         return NotFound();
            //     }
            //     else
            //     {
            //         throw;
            //     }
            // }

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

        //   if (_context.ProductItems == null)
        //   {
        //       return Problem("Entity set 'ProductItemContext.ProductItems'  is null.");
        //   }
        //     _context.ProductItems.Add(product);
        //     await _context.SaveChangesAsync();

            await _service.CreateAsync(product);

            // return CreatedAtAction("GetProduct", new { id = product.Id }, product);
            return CreatedAtAction(nameof(GetProduct), new { id = product.Id }, ItemToDTO(product));
        }

        // DELETE: api/ProductItems/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(string id)
        {
            // if (_context.ProductItems == null)
            // {
            //     return NotFound();
            // }
            // var product = await _context.ProductItems.FindAsync(id);
            // if (product == null)
            // {
            //     return NotFound();
            // }

            // _context.ProductItems.Remove(product);
            // await _context.SaveChangesAsync();
            var product = _service.GetProductAsync(id);

            if (product is null)
            {
                return NotFound();
            }

            await _service.RemoveAsync(id);
            return NoContent();
        }

        // private bool ProductExists(string id)
        // {
        //     return (_context.ProductItems?.Any(e => e.Id == id)).GetValueOrDefault();
        // }

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

// namespace Product_service.Controllers
// {
//     [Route("api/[ProductItems]")]
//     [ApiController]
//     public class ProductItemsController : ControllerBase
//     {
//         private readonly ProductItemContext _context;

//         public ProductItemsController(ProductItemContext context)
//         {
//             _context = context;
//         }

//         // GET: api/ProductItems
//         [HttpGet]
//         public async Task<ActionResult<IEnumerable<ProductItemDTO>>> 
//             GetProductItems()
//         {
//           if (_context.ProductItems == null)
//           {
//               return NotFound();
//           }
//             return await _context.ProductItems
//                 .Select(x => ItemToDTO(x))
//                 .ToListAsync();
//             // return await _context.ProductItems.ToListAsync();
//         }

//         // GET: api/ProductItems/5
//         [HttpGet("{id}")]
//         public async Task<ActionResult<ProductItemDTO>> GetProduct(string id)
//         {
//           if (_context.ProductItems == null)
//           {
//               return NotFound();
//           }
//             var product = await _context.ProductItems.FindAsync(id);

//             if (product == null)
//             {
//                 return NotFound();
//             }

//             return ItemToDTO(product);
//         }

//         // PUT: api/ProductItems/5
//         // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
//         [HttpPut("{id}")]
//         public async Task<IActionResult> PutProduct(string id, ProductItemDTO productItemDTO)
//         {
//             if (id != productItemDTO.Id)
//             {
//                 return BadRequest();
//             }

//             _context.Entry(productItemDTO).State = EntityState.Modified;

//             try
//             {
//                 await _context.SaveChangesAsync();
//             }
//             catch (DbUpdateConcurrencyException)
//             {
//                 if (!ProductExists(id))
//                 {
//                     return NotFound();
//                 }
//                 else
//                 {
//                     throw;
//                 }
//             }

//             return NoContent();
//         }

//         // POST: api/ProductItems
//         // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
//         [HttpPost]
//         public async Task<ActionResult<ProductItemDTO>> PostProduct(ProductItemDTO productDTO)
//         {
//             var product = new Product {
//                 IsComplete = productDTO.IsComplete,
//                 Name = productDTO.Name
//             };

//           if (_context.ProductItems == null)
//           {
//               return Problem("Entity set 'ProductItemContext.ProductItems'  is null.");
//           }
//             _context.ProductItems.Add(product);
//             await _context.SaveChangesAsync();

//             // return CreatedAtAction("GetProduct", new { id = product.Id }, product);
//             return CreatedAtAction(nameof(GetProduct), new { id = product.Id }, ItemToDTO(product));
//         }

//         // DELETE: api/ProductItems/5
//         [HttpDelete("{id}")]
//         public async Task<IActionResult> DeleteProduct(long id)
//         {
//             if (_context.ProductItems == null)
//             {
//                 return NotFound();
//             }
//             var product = await _context.ProductItems.FindAsync(id);
//             if (product == null)
//             {
//                 return NotFound();
//             }

//             _context.ProductItems.Remove(product);
//             await _context.SaveChangesAsync();

//             return NoContent();
//         }

//         private bool ProductExists(string id)
//         {
//             return (_context.ProductItems?.Any(e => e.Id == id)).GetValueOrDefault();
//         }

//         private static ProductItemDTO ItemToDTO (Product ProductItem)
//         {
//             return new ProductItemDTO
//             {
//                 Id = ProductItem.Id,
//                 Name = ProductItem.Name,
//                 IsComplete = ProductItem.IsComplete
//             };
//         }
//     }
// }
